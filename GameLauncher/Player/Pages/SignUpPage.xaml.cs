using GameLauncher.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace GameLauncher.Player.Pages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private PlayerWindow parent;
        public SignUpPage(PlayerWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void ToLogin_Btn_Click(object sender, RoutedEventArgs e)
        {
            parent.ChangePage(new LogInPage(parent));
        }

        private void Done_Btn_Click(object sender, RoutedEventArgs e)
        {
            Entities.Player player = new() { Id = Guid.NewGuid() };

            if(Login_TxtBx.Text == "") 
            {
                MessageBox.Show("Fill all fields");
                Login_TxtBx.Focus();
                return;
            }

            if(parent.Context.Players.Where(p => p.Login == Login_TxtBx.Text).Any())
            {
                MessageBox.Show("This login is already taken! Try another one!");
                Login_TxtBx.Focus();
                return;
            }

            if (Name_TxtBx.Text == "")
            {
                MessageBox.Show("Fill all fields");
                Name_TxtBx.Focus();
                return;
            }

            if (Surname_TxtBx.Text == "")
            {
                MessageBox.Show("Fill all fields");
                Surname_TxtBx.Focus();
                return;
            }

            if (Email_TxtBx.Text == "")
            {
                MessageBox.Show("Fill all fields");
                Email_TxtBx.Focus();
                return;
            }

            if(!Regex.IsMatch(Email_TxtBx.Text, "^[^@]+@[^@]+$"))
            {
                MessageBox.Show("Incorrect email");
                Email_TxtBx.Focus();
                return;
            }

            if(parent.Context.Players.Where(p => p.Email == Email_TxtBx.Text).Any())
            {
                MessageBox.Show("Player with this e-mail already existed!");
                Email_TxtBx.Focus();
                return;
            }

            if(Password_TxtBx.Text != PassRepeat_TxtBx.Text)
            {
                MessageBox.Show("Passwords do not match");
                PassRepeat_TxtBx.Focus();
                return;
            }

            player.Login = Login_TxtBx.Text;

            player.Name = Name_TxtBx.Text;
            player.Surname = Surname_TxtBx.Text;

            player.Email = Email_TxtBx.Text;

            player.HashPassword = PasswordHasher.Hash(Password_TxtBx.Text);

            player.AvatarLink = null!;
            player.CoinsCount = 0;
            player.RegistrationDt = DateTime.Now;

            parent.Context.Players.Add(player);
            parent.Context.SaveChanges();

            parent.ChangePage(new LoggedInPage(player, parent));
        }
    }
}
