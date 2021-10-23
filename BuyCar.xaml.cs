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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для BuyCar.xaml
    /// </summary>
    public partial class BuyCar : Page
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Brand;
        DataTable Car;
        public static string Brand1 { get; set; }
        public static string Model1 { get; set; }
        public BuyCar()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string sql;
            SqlConnection connection = null;
            Brand = new DataTable();
            sql = "SELECT Name_brand from Brand";
            connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(Brand);
            for (int i = 0; i < Brand.Rows.Count; i++)
            {
                ComboBoxBrand.Items.Add(Brand.Rows[i]["Name_brand"].ToString());
            }
            

        }

        private void ComboBoxModel_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
           
        }

        private void ComboBoxBrand_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void ComboBoxBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxModel.SelectedItem = null;
            ComboBoxModel.Items.Clear();
            if (ComboBoxBrand.SelectedItem.ToString() != "")
            {
                    string sql2;
                    SqlConnection connection = null;
                    Car = new DataTable();
                    sql2 = "SELECT Model from Model where Name_brand='" + ComboBoxBrand.SelectedItem.ToString() + "' ";
                    connection = new SqlConnection(connectionString);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, connection);

                    connection.Open();

                    adapter2.Fill(Car);
                    for (int i = 0; i < Car.Rows.Count; i++)
                    {
                        ComboBoxModel.Items.Add(Car.Rows[i]["Model"].ToString());
                    }
                    
                
            }
            else

                Error.Text = "ошибка";
        }

        private void Find_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Find_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            Brand1 = ComboBoxBrand.SelectedItem.ToString();
            if (ComboBoxModel.SelectedItem != null)
            {
                Model1 = ComboBoxModel.SelectedItem.ToString();
            }
           
            OutputAd.Content = new OutAd();
            
        }
    }
   // }
}
