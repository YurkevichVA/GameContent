﻿using System.Windows;
using System.Windows.Controls;
using GameLauncher.Player.Pages;

namespace GameLauncher.Player
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        public PlayerWindow()
        {
            InitializeComponent();
            Main_Frm.Navigate(new LogInPage(this));
        }
        public void ChangePage(Page page)
        {
            Main_Frm.Navigate(page);
        }
    }
}
