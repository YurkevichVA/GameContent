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
    /// Interaction logic for BuyContentWindow.xaml
    /// </summary>
    public partial class BuyContentWindow : Window
    {
        private Entities.Content _content;
        private Entities.Player _player;
        public BuyContentWindow(Entities.Content content, Entities.Player player)
        {
            InitializeComponent();
            _content = content;
            _player = player;
        }

        private void Purchase_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(RealCost_RdBtn.IsChecked == true)
            {
                if (_player.UnlockedContent == null)
                    _player.UnlockedContent = new();

                _player.UnlockedContent.Add(_content);

                DialogResult = true;
            }
            else if(CoinsCost_RdBtn.IsChecked == true)
            {
                if(_player.CoinsCount > _content.CostCoins)
                {
                    _player.CoinsCount -= _content.CostCoins;

                    if (_player.UnlockedContent == null)
                        _player.UnlockedContent = new();

                    _player.UnlockedContent.Add(_content);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Not enough coins!");
                    return;
                }
            }
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Avatar_Img.Source = new Uri(_content.InstallationFolder);
            Name_TxtBx.Text = _content.Name;
            RealCost_RdBtn.Content = _content.CostReal;
            RealCost_RdBtn.IsChecked = true;
            CoinsCost_RdBtn.Content = _content.CostCoins;
        }
    }
}
