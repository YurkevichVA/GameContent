using Azure;
using GameLauncher.Entities;
using GameLauncher.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameLauncher.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public EFContext Context { get; set; }

        private ICollectionView _playersView;
        private ICollectionView _contentView;
        private ICollectionView _transactionsView;

        private static readonly Random random = new Random();
        public AdminWindow(EFContext context)
        {
            InitializeComponent();
            Context = context;
            DataContext = Context;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }
        private void UpdateView()
        {
            Context.Players.Load();
            Context.Contents.Load();
            Context.Transactions.Load();
            Players_LstVw.ItemsSource = Context.Players.Local.ToObservableCollection();
            Content_LstVw.ItemsSource = Context.Contents.Local.ToObservableCollection();
            Transactions_LstVw.ItemsSource = Context.Transactions.Local.ToObservableCollection();

            TransactionPlayer_CmbBx.ItemsSource = Context.Players.Local.Where(p => p.Transactions != null);

            _playersView = CollectionViewSource.GetDefaultView(Players_LstVw.ItemsSource);
            _contentView = CollectionViewSource.GetDefaultView(Content_LstVw.ItemsSource);
            _transactionsView = CollectionViewSource.GetDefaultView(Transactions_LstVw.ItemsSource);
        }

        private void Content_LstVw_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView item)
            {
                if (item.SelectedItem is Entities.Content content)
                {
                    Content_CRUDWin dialog = new Content_CRUDWin();
                    dialog.Content = content;
                    if (dialog.ShowDialog() == true)
                    {
                        Context.Contents.Update(dialog.Content);
                        Context.SaveChanges();
                        UpdateView();
                    }
                }
            }
        }

        #region Players
        private bool[] playersFilters = new[] { false, false, false };
        private void GeneratePlayers_Btn_Click(object sender, RoutedEventArgs e)
        {
            string[] names = new string[]
            {
                "Jake", "John", "Alyaska", "Sonya", "Kateryna", "Volodymyr", "Francheska", "Franko", "Sarah", "Kurt",
                "Ariella", "Gustavo", "Frances", "Dayanara", "Gaige", "Benjamin", "Turner", "Brianna", "Beatrice", "Lara",
                "Alexia", "Brodie", "Kennedi", "Ally", "Marquise", "Nevaeh", "Haylie", "Nelson", "Bradyn", "Anahi"
            };
            string[] surnames = new string[]
            {
                "Liu", "Reynolds", "Watkins", "Durham", "Love", "Nichols", "Peck", "Contreras", "Duran", "Leon",
                "Vincent", "Gomez", "Cantrell", "Crawford", "Garrett", "Yang", "Simpson", "Bridges", "Carrillo", "Morales",
                "Moreno", "Price", "Olson", "Duke", "Lara", "Baxter", "Carey", "Galloway", "Moore", "Mcmahon"
            };
            string login_symbols = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            string[] mails = new string[] { "@gmail.com", "@outlook.com", "@mail.com" };

            List<string> logins = new();

            for(int i = 0; i < 100; i++)
            {
                Entities.Player player = new();
                
                player.Id = Guid.NewGuid();
                player.Name = names[random.Next(names.Length)];
                player.Surname = surnames[random.Next(surnames.Length)];

                string login = player.Name;
                for (int j = 0; j < random.Next(1, 5); j++)
                    login += login_symbols[random.Next(login_symbols.Length)];

                while(logins.Contains(login))
                {
                    login = player.Name;
                    for (int j = 0; j < random.Next(1, 5); j++)
                        login += login_symbols[random.Next(login_symbols.Length)];
                }

                player.Login = login;

                player.Email = login + mails[random.Next(mails.Length)];

                player.HashPassword = PasswordHasher.Hash("qwerty");

                player.RegistrationDt = DateTime.Now;

                player.CoinsCount = random.Next(10000);

                Context.Players.Add(player);
            }

            Context.SaveChanges();
            UpdateView();
        }
        private bool RegistrationDateFilter(object item)
        {
            if(item is Entities.Player player)
            {
                return player.RegistrationDt.Date == RegDate_DtPckr.SelectedDate;
            }
            return false;
        }
        private bool DeletedPlayersFilter(object item)
        {
            if(item is Entities.Player player)
            {
                if (Deleted_CmbBx.SelectedIndex == 0)
                    return true;
                else if (Deleted_CmbBx.SelectedIndex == 1)
                    return player.DeleteDt == null;
                else if (Deleted_CmbBx.SelectedIndex == 2)
                    return player.DeleteDt != null;
            }
            return false;
        }
        private bool LoginSearchFilter(object item)
        {
            if(item is Entities.Player player)
            {
                return player.Login.Contains(LoginSearch_TxtBx.Text);
            }
            return false;
        }
        private bool PlayersMultiFilter(object item)
        {
            if(item is Entities.Player player)
            {
                bool[] filtersResult = new[] { false, false, false };

                if (playersFilters[0])
                    filtersResult[0] = RegistrationDateFilter(player);
                else
                    filtersResult[0] = true;

                if (playersFilters[1])
                    filtersResult[1] = DeletedPlayersFilter(player);
                else
                    filtersResult[1] = true;

                if (playersFilters[2])
                    filtersResult[2] = LoginSearchFilter(player);
                else
                    filtersResult[2] = true;

                return filtersResult[0] && filtersResult[1] && filtersResult[2];
            }
            return false;
        }
        private void ResetFiltersPlayers_Btn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
                playersFilters[i] = false;

            RegDate_DtPckr.SelectedDate = null;
            Deleted_CmbBx.SelectedIndex = 0;
            LoginSearch_TxtBx.Text = "";

            _playersView.Filter = null;
        }
        private void RegDate_DtPckr_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            playersFilters[0] = true;
            _playersView.Filter = PlayersMultiFilter;
        }
        private void Deleted_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            playersFilters[1] = true;
            _playersView.Filter = PlayersMultiFilter;
        }
        private void LoginSearch_TxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            playersFilters[2] = true;
            _playersView.Filter = PlayersMultiFilter;
        }
        #endregion

        #region Content
        private bool[] contentFilters = new[] { false, false, false };
        private void AddContent_Btn_Click(object sender, RoutedEventArgs e)
        {
            Content_CRUDWin crudDialog = new();
            if(crudDialog.ShowDialog() == true)
            {
                Context.Contents.Add(crudDialog.Content);
                Context.SaveChanges();
                UpdateView();
            }
        }
        private bool TypeContentFilter(object item)
        {
            if(item is Entities.Content content)
            {
                if (TypeSort_CmbBx.SelectedIndex == 0)
                    return true;
                else
                    return (int)content.Type == TypeSort_CmbBx.SelectedIndex - 1;
            }
            return false;
        }
        private bool NameContentSearchFilter(object item)
        {
            if(item is Entities.Content content)
            {
                return content.Name.Contains(NameContentSearch_TxtBx.Text);
            }
            return false;
        }
        private bool DeletedContentFilter(object item)
        {
            if(item is Entities.Content content)
            {
                if (DeletedContent_CmbBx.SelectedIndex == 0)
                    return true;
                else if (DeletedContent_CmbBx.SelectedIndex == 1)
                    return content.DeleteDt == null;
                else if (DeletedContent_CmbBx.SelectedIndex == 2)
                    return content.DeleteDt != null;
            }
            return false;
        }
        private bool ContentMultiFilter(object item)
        {
            if (item is Entities.Content content)
            {
                bool[] filtersResult = new[] { false, false, false };

                if (contentFilters[0])
                    filtersResult[0] = TypeContentFilter(content);
                else
                    filtersResult[0] = true;

                if (contentFilters[1])
                    filtersResult[1] = NameContentSearchFilter(content);
                else
                    filtersResult[1] = true;

                if (contentFilters[2])
                    filtersResult[2] = DeletedContentFilter(content);
                else
                    filtersResult[2] = true;

                return filtersResult[0] && filtersResult[1] && filtersResult[2];
            }
            return false;
        }
        private void ResetContentFilters_Btn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
                playersFilters[i] = false;

            NameContentSearch_TxtBx.Text = "";
            TypeSort_CmbBx.SelectedIndex = 0;
            DeletedContent_CmbBx.SelectedIndex = 0;

            _contentView.Filter = null;
        }
        private void TypeSort_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contentFilters[0] = true;
            _contentView.Filter = ContentMultiFilter;
        }
        private void NameContentSearch_TxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            contentFilters[1] = true;
            _contentView.Filter = ContentMultiFilter;
        }
        private void DeletedContent_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contentFilters[2] = true;
            _contentView.Filter = ContentMultiFilter;
        }
        #endregion

        #region Transactions
        private bool TransactionsDateFilter(object item)
        {
            if(item is Entities.Transaction transaction)
            {
                return transaction.Date.Date == TransactionDate_DtPckr.SelectedDate;
            }
            return false;
        }
        private bool TransactionPlayerFilter(object item)
        {
            if(item is Entities.Transaction transaction)
            {
                return transaction.Payer == TransactionPlayer_CmbBx.SelectedItem as Entities.Player;
            }
            return false;
        }
        private bool TransactionMultiFilter(object item)
        {
            if (item is Entities.Transaction transaction)
            {
                return transaction.Payer == TransactionPlayer_CmbBx.SelectedItem as Entities.Player && transaction.Date.Date == TransactionDate_DtPckr.SelectedDate;
            }
            return false;
        }
        private void ResetFiltersTransactions_Btn_Click(object sender, RoutedEventArgs e)
        {
            TransactionDate_DtPckr.SelectedDate = null;
            TransactionPlayer_CmbBx.SelectedItem = null;
            _transactionsView.Filter = null;
        }
        private void TransactionDate_DtPckr_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_transactionsView.Filter != null)
                _transactionsView.Filter = TransactionMultiFilter;
            else
                _transactionsView.Filter = TransactionsDateFilter;
        }
        private void TransactionPlayer_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_transactionsView.Filter != null)
                _transactionsView.Filter = TransactionMultiFilter;
            else
                _transactionsView.Filter = TransactionPlayerFilter;
        }


        #endregion

    }
}
