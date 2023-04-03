using System;
using System.Collections.Generic;
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

namespace GameLauncher.Player
{
    /// <summary>
    /// Interaction logic for DeleteAccountWindow.xaml
    /// </summary>
    public partial class DeleteAccountWindow : Window
    {
        private string _pass;
        public DeleteAccountWindow(string pass)
        {
            InitializeComponent();
            _pass = pass;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Services.PasswordHasher.Verify(_pass, Pass_TxtBx.Text))
                DialogResult = true;
            else
                MessageBox.Show("Wrong password!");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
