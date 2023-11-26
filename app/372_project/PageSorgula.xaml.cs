using System;
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
    /// Interaction logic for PageSorgula.xaml
    /// </summary>
    public partial class PageSorgula : Page
    {
        public PageSorgula()
        {
            InitializeComponent();
        }

        private void Geri_Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GeriButtonClick");
            Application.Current.MainWindow.Content = new PageMain();
        }
    }
}
