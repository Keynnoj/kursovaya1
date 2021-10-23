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

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
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

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new MenuWindow().Show();
            this.Close();
        }
    }
}
    
