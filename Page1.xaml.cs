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

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
                ochko.Content = new BuyCar();
        }

        private void Cost_Click(object sender, RoutedEventArgs e)
        {
            ochko.Content = new CostCar();
        }

        private void Lichniy_kab_click(object sender, RoutedEventArgs e)
        {
            ochko.Content = new LK();
        }
    }
}
