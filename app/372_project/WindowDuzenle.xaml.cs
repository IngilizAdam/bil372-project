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

namespace _372_project
{
    /// <summary>
    /// Interaction logic for WindowDuzenle.xaml
    /// </summary>
    public partial class WindowDuzenle : Window
    {
        private Window window;

        public WindowDuzenle(Window window)
        {
            InitializeComponent();

            this.window = window;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.IsEnabled = true;
        }
    }
}
