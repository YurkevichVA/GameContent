using GameLauncher.Admin;
using GameLauncher.Player;
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

namespace GameLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayerWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new PlayerWindow().ShowDialog();
            Show();
        }

        private void AdminWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new AdminWindow().ShowDialog();
            Show();
        }
    }
}
