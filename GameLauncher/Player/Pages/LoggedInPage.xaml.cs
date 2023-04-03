using GameLauncher.Admin;
using GameLauncher.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameLauncher.Player.Pages
{
    /// <summary>
    /// Interaction logic for LoggedInPage.xaml
    /// </summary>
    public partial class LoggedInPage : Page
    {
        private Entities.Player _player;
        private PlayerWindow _parent;
        public LoggedInPage(Entities.Player player, PlayerWindow parent)
        {
            InitializeComponent();
            _player = player;
            _parent = parent;
            DataContext = _parent.Context;
            _parent.Context.Contents.Load();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetupProfile();
            SetupInventory();
            SetupShop();
        }

        private void SetupProfile()
        {
            Name_TxtBx.Text = _player.Name;
            Surname_TxtBx.Text = _player.Surname;
            Login_TxtBx.Text = _player.Login;
            Email_TxtBx.Text = _player.Email;

            ApplyChanges_Btn.IsEnabled = false;
            ResetChanges_Btn.IsEnabled = false;
        }

        private void SetupInventory()
        {
            Shop_LstVw.ItemsSource = _parent.Context.Contents.Local.ToObservableCollection();
        }

        private void SetupShop()
        {
            Shop_LstVw.ItemsSource = _parent.Context.Contents.Local.ToObservableCollection();
        }

        private void Upload_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangePass_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(OldPass_TxtBx.Text == "")
            {
                MessageBox.Show("Enter old password!");
                OldPass_TxtBx.Focus();
                return;
            }

            if (NewPass_TxtBx.Text == "")
            {
                MessageBox.Show("Enter new password!");
                NewPass_TxtBx.Focus();
                return;
            }

            if (RepeatNewPass_TxtBx.Text == "")
            {
                MessageBox.Show("Repeat new password!");
                RepeatNewPass_TxtBx.Focus();
                return;
            }

            if (Services.PasswordHasher.Verify(_player.HashPassword, OldPass_TxtBx.Text))
            {
                if(NewPass_TxtBx.Text == RepeatNewPass_TxtBx.Text)
                {
                    _player.HashPassword = Services.PasswordHasher.Hash(NewPass_TxtBx.Text);
                    MessageBox.Show("Password was successfully changed!", "Success!", MessageBoxButton.OK);
                    OldPass_TxtBx.Text = "";
                    NewPass_TxtBx.Text = "";
                    RepeatNewPass_TxtBx.Text = "";
                    _parent.Context.Players.Update(_player);
                    _parent.Context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Passwords are not match!");
                    NewPass_TxtBx.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Wrong password!");
                OldPass_TxtBx.Focus();
                return;
            }
        }

        private void DeleteAccount_Btn_Click(object sender, RoutedEventArgs e)
        {
            DeleteAccountWindow confirm = new DeleteAccountWindow(_player.HashPassword);
            if (confirm.ShowDialog() == true)
            {
                _player.DeleteDt = DateTime.Now;
                _parent.Context.Players.Update(_player);
                _parent.Context.SaveChanges();
                _parent.ChangePage(new LogInPage(_parent));
            }
        }

        private void ApplyChanges_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Name_TxtBx.Text == "")
            {
                MessageBox.Show("Enter name!");
                Name_TxtBx.Focus();
                return;
            }

            if (Surname_TxtBx.Text == "")
            {
                MessageBox.Show("Enter surname!");
                Surname_TxtBx.Focus();
                return;
            }
            
            if (Login_TxtBx.Text == "")
            {
                MessageBox.Show("Enter login!");
                Login_TxtBx.Focus();
                return;
            }

            if (_player.Login != Login_TxtBx.Text)
                if (_parent.Context.Players.Where(p => p.Login == Login_TxtBx.Text).Any())
                {
                    MessageBox.Show("This login is already taken! Try another one!");
                    Login_TxtBx.Focus();
                    return;
                }

            if (Email_TxtBx.Text == "")
            {
                MessageBox.Show("Enter e-mail!");
                Email_TxtBx.Focus();
                return;
            }

            if (_player.Email != Email_TxtBx.Text)
            {
                if (!Regex.IsMatch(Email_TxtBx.Text, "^[^@]+@[^@]+$"))
                {
                    MessageBox.Show("Incorrect email");
                    Email_TxtBx.Focus();
                    return;
                }

                if (_parent.Context.Players.Where(p => p.Email == Email_TxtBx.Text).Any())
                {
                    MessageBox.Show("Player with this e-mail already existed!");
                    Email_TxtBx.Focus();
                    return;
                }
            }

            _player.Login = Login_TxtBx.Text;
            _player.Name = Name_TxtBx.Text;
            _player.Surname = Surname_TxtBx.Text;
            _player.Email = Email_TxtBx.Text;

            _parent.Context.Players.Update(_player);
            _parent.Context.SaveChanges();

            ApplyChanges_Btn.IsEnabled = false;
            ResetChanges_Btn.IsEnabled = false;
        }

        private void LogOut_Btn_Click(object sender, RoutedEventArgs e)
        {
            _parent.ChangePage(new LogInPage(_parent));
        }

        private void EnableApplyResetButtons(object sender, TextChangedEventArgs e)
        {
            ApplyChanges_Btn.IsEnabled = true;
            ResetChanges_Btn.IsEnabled = true;
        }

        private void ResetChanges_Btn_Click(object sender, RoutedEventArgs e)
        {
            Name_TxtBx.Text = _player.Name;
            Surname_TxtBx.Text = _player.Surname;
            Login_TxtBx.Text = _player.Login;
            Email_TxtBx.Text = _player.Email;

            ApplyChanges_Btn.IsEnabled = false;
            ResetChanges_Btn.IsEnabled = false;
        }

        private void Shop_LstVw_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView item)
            {
                if (item.SelectedItem is Entities.Content content)
                {

                    if (_player.UnlockedContent is not null)
                    {
                        if (_player.UnlockedContent.Contains(content))
                        {
                            MessageBox.Show("You already have this item!");
                            return;
                        }
                    }
                    BuyContentWindow buyDialog = new(content, _player);
                    if(buyDialog.ShowDialog() == true)
                    {
                        Transaction transaction = new()
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Now,
                            Payer = _player,
                            Contents = new()
                        };
                        transaction.Contents.Add(content);

                        _parent.Context.Update(_player);
                        _parent.Context.Update(content); 
                        _parent.Context.Transactions.Add(transaction);
                        _parent.Context.SaveChanges();
                    }
                }
            }
        }
    }
}
