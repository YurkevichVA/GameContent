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

namespace GameLauncher.Admin
{
    /// <summary>
    /// Interaction logic for Content_CRUDWin.xaml
    /// </summary>
    public partial class Content_CRUDWin : Window
    {
        public Entities.Content? Content { get; set; }
        public Content_CRUDWin()
        {
            InitializeComponent();
            Content = null!;
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            var isConfirmed = MessageBox.Show("Delete content?", "Confirm", MessageBoxButton.YesNo);

            if(Content is null) return;

            if (isConfirmed == MessageBoxResult.Yes)
                Content.DeleteDt = DateTime.Now;
            else
                return;

            DialogResult = true;
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Content is null) return;

            if(Name_TxtBx.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter name for content!");
                Name_TxtBx.Focus();
                return;
            }

            if (Type_CmbBx.SelectedIndex == -1)
            {
                MessageBox.Show("Enter type of content!");
                Type_CmbBx.Focus();
                return;
            }

            if(CostReal_TxtBx.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter real money cost!");
                CostReal_TxtBx.Focus();
                return;
            }

            try
            {
                var priceReal = Convert.ToDouble(CostReal_TxtBx.Text);
            }
            catch
            {
                MessageBox.Show("Wrong input at real cost!");
                CostReal_TxtBx.Focus();
                return;
            }
            
            if (CostCoins_TxtBx.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter cost in coins!");
                CostCoins_TxtBx.Focus();
                return;
            }

            try
            {
                var priceCoins = Convert.ToInt32(CostCoins_TxtBx.Text);
            }
            catch
            {
                MessageBox.Show("Wrong input at coins cost!");
                CostCoins_TxtBx.Focus();
                return;
            }

            if (InstallationLink_TxtBx.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter installation link!");
                InstallationLink_TxtBx.Focus();
                return;
            }

            if (InstallationFolder_TxtBx.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter installation folder!");
                InstallationFolder_TxtBx.Focus();
                return;
            }

            Content.Name = Name_TxtBx.Text;
            Content.Type = (Entities.ContentType)Type_CmbBx.SelectedIndex;
            Content.CostReal = Convert.ToDouble(CostReal_TxtBx.Text);
            Content.CostCoins = Convert.ToInt32(CostCoins_TxtBx.Text);
            Content.InstallationLink = InstallationLink_TxtBx.Text;
            Content.InstallationFolder = InstallationFolder_TxtBx.Text;

            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Content == null) 
            {
                Content = new Entities.Content() { Id = Guid.NewGuid() };
                Delete_Btn.IsEnabled = false;
            }
            else
            {
                Name_TxtBx.Text = Content.Name;
                CostCoins_TxtBx.Text = Content.CostCoins.ToString();
                CostReal_TxtBx.Text = Content.CostReal.ToString();
                InstallationFolder_TxtBx.Text = Content.InstallationFolder;
                InstallationLink_TxtBx.Text = Content.InstallationLink;

                Type_CmbBx.SelectedIndex = (int)Content.Type;
            }
            Id_TxtBx.Text = Content.Id.ToString();
        }
    }
}
