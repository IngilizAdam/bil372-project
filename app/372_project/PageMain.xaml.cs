﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace _372_project
{
    /// <summary>
    /// Interaction logic for PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
        }

        private void Sorgula_Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("SorgulaButtonClick");
            Application.Current.MainWindow.Content = new PageSorgula();
        }

        private void Ekle_Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("EkleButtonClick");
            Application.Current.MainWindow.Content = new PageEkle();
        }
    }
}
