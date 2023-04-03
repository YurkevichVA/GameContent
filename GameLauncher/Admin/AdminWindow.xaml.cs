using Azure;
using GameLauncher.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private static Random random = new Random();
        public AdminWindow(EFContext context)
        {
            InitializeComponent();
            Context = context;
            DataContext = Context;
        }

        private void Players_LstVw_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

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

        private void Transactions_LstVw_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

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
        }

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

        private void IncludeDeleted_ChkBx_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void IncludeDeleted_ChkBx_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
