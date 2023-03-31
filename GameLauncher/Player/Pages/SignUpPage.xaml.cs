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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        }
    }
}
