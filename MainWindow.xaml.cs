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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PerexodBase(object sender, RoutedEventArgs e)
        {
            PageBase pb = new PageBase();
            frames.Navigate(pb);
        }

        private void PerexodManufact(object sender, RoutedEventArgs e)
        {
            PageNomenclatura pm = new PageNomenclatura();
            frames.Navigate(pm);
        }

        private void PerexodTovars(object sender, RoutedEventArgs e)
        {
            PageTovars pt = new PageTovars();
            frames.Navigate(pt);

        }
    }
}
