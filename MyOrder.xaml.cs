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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для MyOrder.xaml
    /// </summary>
    public partial class MyOrder : Window
    {
        static string connectionString;
        SqlDataAdapter adapter;
        static DataTable Car;
        static DataTable Ad;
        static DataTable Orders;
        public MyOrder()
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
                //SqlCommand commandd = new SqlCommand(sq2, connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    Car.Load(reader);
                    //Ad.Load(reader);
                }
            }
            return Car;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Orders = ExecuteSql("SELECT * from OrderCar where (Code_user ='" + MainWindow.Code_user_ + "')");
            LOrder.ItemsSource = Orders.DefaultView;
            LOrder.Items.Refresh();
        }

        
    }
    
}
