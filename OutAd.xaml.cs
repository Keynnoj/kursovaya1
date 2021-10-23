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
    /// Логика взаимодействия для OutAd.xaml
    /// </summary>
    public partial class OutAd : Page
    {
        static string connectionString;
        SqlDataAdapter adapter;
        static DataTable Car;
        public static string Code_car { get; set; }
        
        public OutAd()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                //Error.Text = "connect BD";

            }
            catch (SqlException)
            {
                //Error.Text = "Ошибка подключения БД";
            }
        }


        static DataTable ExecuteSql(string sql)
        {
            //string sql;
            Car = new DataTable();
            SqlConnection connection = null;

            connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    Car.Load(reader);
                }
            }
            return Car;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (BuyCar.Model1 != null)
            {
                string sql = "SELECT * from FindAd where (( Name_brand='" + BuyCar.Brand1 + "') AND ( Model='" + BuyCar.Model1 + "')) ;";
                //Name_brand = combobox ;";
                DataTable Car = ExecuteSql(sql);
                LViewAd.ItemsSource = Car.DefaultView;
            }
            else
            {
                string sql = "SELECT * from FindAd where (Name_brand = '" + BuyCar.Brand1 + "');";
                DataTable Car = ExecuteSql(sql);
                LViewAd.ItemsSource = Car.DefaultView;
            }
        }


        private void LViewAd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Code_car = Car.Rows[LViewAd.SelectedIndex]["Code_car"].ToString().Trim();
            
            new ViewAd().Show();
            Application.Current.MainWindow.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
