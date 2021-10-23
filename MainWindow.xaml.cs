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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public SqlConnection connection;
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Client;

        public static string login_ { get; set; }
        public static string password_ { get; set; }
        public static string Code_user_ { get; set; }
        public static string Surname_ { get; set; }

        public static string Name_ { get; set; }
        public static string Patronymic_ { get; set; }
        public static string City_ { get; set; }
        public static string Adres_ { get; set; }
        public static string Phone_ { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
        connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConne
                connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                //Error.Text = "connect BD";

            }
            catch (SqlException)
            {
                Error.Text = "Ошибка подключения БД";
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        //private void Vivod_Click(object sender, RoutedEventArgs e)
        //{
         //   LoginVivod.Text = Login.Text;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Vhod_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            
            Client = new DataTable();
            password_ = Password.Text.Trim();
            login_ = Login.Text.Trim();
            
            Error.Text = "";
            if ((Login.Text == "") && (Password.Text == ""))
            {
                Error.Text = "Вы ничего не ввели";
            }
            else
            if (Login.Text == "")
            {
                Error.Text = "Вы не ввели логин";
            }
            else
            if (Password.Text == "")
            {
                Error.Text = "Вы не ввели пароль";
            }
            else
           
            {
               
                sql = "SELECT Code_user, Surname, Name, Patronymic, Phone, City, Adres, Login,Password from Client where ((Login='" + login_ + "') and (Password='" + password_ + "'))";
                
               
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Code_user_ = reader[0].ToString();
                    Surname_ = reader[1].ToString();
                    Name_ = reader[2].ToString();
                    Patronymic_ = reader[3].ToString();
                    Phone_ = reader[4].ToString();
                    City_ = reader[5].ToString();
                    Adres_ = reader[6].ToString();
                    login_ = reader[7].ToString();  
                    password_ = reader[8].ToString();

                    new MenuWindow().Show();
                    Helper.CloseWindow(Window.GetWindow(this));

                    // Error.Text = role_;
                    return;
                }

                Error.Text = "Неверный логин или пароль";
                Login.Text = "";
                Password.Text = "";
                connection.Close();   
            }
           
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().Show();
        }



        // new Window1().Show();
        //  this.Close();
    }
    }

