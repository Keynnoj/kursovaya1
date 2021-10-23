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
    /// Логика взаимодействия для RegSucPage.xaml
    /// </summary>
    public partial class RegSucPage : Page
    {
        public RegSucPage()
        {
            InitializeComponent();
        }

        private void RegOkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MenuWindow());
        }
    }
}
