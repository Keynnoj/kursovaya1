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
    public partial class LK : Page
    {
        public SqlConnection connection;
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Client;

        public static string login_ { get; set; }
        public static string password_ { get; set; }
        public LK()
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string sql;
            SqlConnection connection = null;
            sql = "Select  Name, Surname, Patronymic, Phone, City, Adres, Login,Password FROM Client WHERE Code_user='" + MainWindow.Code_user_ + "';"; 
            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Name.Text = reader[0].ToString();
                Surname.Text = reader[1].ToString();
                Patronymic.Text = reader[2].ToString();
                Phone.Text = reader[3].ToString();
                City.Text = reader[4].ToString();
                Adres.Text = reader[5].ToString();
                Login.Text = reader[6].ToString();
                Password.Text = reader[7].ToString();
                return;
            }
            reader.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditPage());
        }

        private void MyOrder_Click(object sender, RoutedEventArgs e)
        {
            new MyOrder().Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            Client = new DataTable();
            SqlConnection connection = null;
            sql = "DELETE FROM Client WHERE Code_user='" + MainWindow.Code_user_ + "'; ";
            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            int num = command.ExecuteNonQuery();
            new MainWindow().Show();
            Helper.CloseWindow(Window.GetWindow(this));
            connection.Close();
        }
    }
}