using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private PlayerWindow parent;
        public LogInPage(PlayerWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void ToSignUp_Btn_Click(object sender, RoutedEventArgs e)
        {
            parent.ChangePage(new SignUpPage(parent));
        }

        private void Done_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(Login_TxtBx.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter login");
                Login_TxtBx.Focus();
                return;
            }

            if(Password_TxtBx.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter password");
                Password_TxtBx.Focus();
                return;
            }

            var player = 
                parent.Context.Players.Where(p => p.Login == Login_TxtBx.Text & p.DeleteDt == null).Any()   ? 
                parent.Context.Players.Where(p => p.Login == Login_TxtBx.Text & p.DeleteDt == null).First() : 
                null;

            if (player != null)
            {
                if (Services.PasswordHasher.Verify(player.HashPassword, Password_TxtBx.Text))
                    parent.ChangePage(new LoggedInPage(player, parent));
                else
                    MessageBox.Show("Incorrect password!");
            }
            else
            {
                MessageBox.Show("Incorrect login!");
            }
        }
    }
}
