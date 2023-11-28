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
    /// Interaction logic for PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
        }

        private void Giris_Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GirisButtonClick");

            if(KullaniciAdi_TextBox.Text == "admin" && Sifre_TextBox.Password == "admin")
            {
                Application.Current.MainWindow.Content = new PageSorgula();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
            }
        }
    }
}
