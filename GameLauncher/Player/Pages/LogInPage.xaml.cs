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

        }
    }
}
