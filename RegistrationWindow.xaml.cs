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
using System.Text.RegularExpressions;

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public SqlConnection connection;
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Client;

        public static string login_ { get; set; }
        public static string password_ { get; set; }
        public RegistrationWindow()
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

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            string sql1;
            string sql2;
            string sql3;
            string sql4;

            Client = new DataTable();
            SqlConnection connection = null;
            string idUser = null;
            //проверка на не пустоту поля
            Error.Text = "";
            if ((RegName.Text.Trim() == "") && (RegSurname.Text.Trim() == "") && (RegPatronymic.Text.Trim() == "") && (RegPhone.Text.Trim() == "") && (RegCity.Text.Trim() == "") && (RegAdres.Text.Trim() == "") && (RegLogin.Text.Trim() == "") && (RegPassword.Text.Trim() == ""))
            {
                Error.Text = "Вы ничего не ввели.";
            }
            else
             if (RegName.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели имя.";
            }
            else
             if (RegSurname.Text.Trim() == "")
            {
                Error.Text = "Вы не фамилию.";
            }
            else
             if (RegPatronymic.Text.Trim() == "")
            {
                Error.Text = "Вы не отчество.";
            }
            else
             if (RegPhone.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели номер телефона.";
            }
            else
             if (RegCity.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели свой город.";
            }
            else
             if (RegAdres.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели свой адрес.";
            }
            else
             if (RegLogin.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели логин.";
            }
            else
             if (RegPassword.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели пароль.";
            }
            else
            if ((RegName.Text.Trim() != "") && (RegSurname.Text.Trim() != "") && (RegPatronymic.Text.Trim() != "") && (RegPhone.Text.Trim() != "") && (RegCity.Text.Trim() != "") && (RegAdres.Text.Trim() != "") && (RegLogin.Text.Trim() != "") && (RegPassword.Text.Trim() != ""))
            {
                const string namePattern = @"^[А-Яа-яЁё\s]+$";
                const string surnamePattern = @"^[А-Яа-яЁё\s]+$";
                const string PatronymicPattern = @"^[А-Яа-яЁё\s]+$";
                const string phonePattern = @"[0-9]{11,}";
                const string cityPattern = @"^[А-Яа-яЁё\s]+$";
                const string adresPattern = @"^[А-Яа-яЁё\s]+$";
                const string loginPattern = @"^[A-Za-z0-9]{3,}$";
                const string passwordPattern = @"^[A-Za-z0-9]{3,}$";


                var name = RegName.Text.Trim().ToLowerInvariant();
                var surname = RegSurname.Text.Trim().ToLowerInvariant();
                var Patronymic = RegPatronymic.Text.Trim().ToLowerInvariant();
                var phone = RegPhone.Text.Trim().ToLowerInvariant();
                var city = RegCity.Text.Trim().ToLowerInvariant();
                var adres = RegAdres.Text.Trim().ToLowerInvariant();
                var login = RegLogin.Text.Trim().ToLowerInvariant();
                var password = RegPassword.Text.Trim().ToLowerInvariant();


                if (Regex.Match(name, namePattern).Success)
                {
                    if (Regex.Match(surname, surnamePattern).Success)
                    {
                        if (Regex.Match(Patronymic, PatronymicPattern).Success)
                        {
                            if (Regex.Match(phone, phonePattern).Success)
                            {
                                if (Regex.Match(city, cityPattern).Success)
                                {
                                    if (Regex.Match(adres, adresPattern).Success)
                                    {
                                        if (Regex.Match(login, loginPattern).Success)
                                        {
                                            if (Regex.Match(password, passwordPattern).Success)
                                            {
                                                MainWindow.Name_ = RegName.Text.Trim();
                                                MainWindow.Surname_ = RegSurname.Text.Trim();
                                                MainWindow.Patronymic_ = RegPatronymic.Text.Trim();
                                                MainWindow.Phone_ = RegPhone.Text.Trim();
                                                MainWindow.City_ = RegCity.Text.Trim();
                                                MainWindow.Adres_ = RegAdres.Text.Trim();
                                                MainWindow.login_ = RegLogin.Text.Trim();
                                                MainWindow.password_ = RegPassword.Text.Trim();
                                                connection = new SqlConnection(connectionString);
                                                connection.Open();

                                                sql = "SELECT top(1) Code_user from Client Order by Code_user desc";
                                                SqlCommand command = new SqlCommand(sql, connection);
                                                SqlDataReader reader = command.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                    idUser = reader[0].ToString();
                                                    int id = int.Parse(idUser) + 1;
                                                    idUser = id.ToString();
                                                }
                                                MainWindow.Code_user_ = idUser;
                                                reader.Close();




                                                //connection = new SqlConnection(connectionString);
                                                //sql4 = "INSERT INTO Customers (customer_id, name, date_of_birth, phonenumber, email, login, password, role) VALUES ('" + idClient + "','" + MainWindow.name_ + "','" + MainWindow.dob_ + "','" + MainWindow.phone_ + "','" + MainWindow.email_ + "','" + MainWindow.login_ + "','" + MainWindow.password_ + "',1);";
                                                sql4 = "INSERT INTO Client (Code_user, Surname, Name, Patronymic, Phone, City, Adres, Login,Password)" + "VALUES ('" + idUser + "','" + MainWindow.Surname_ + "','" + MainWindow.Name_ + "','" + MainWindow.Patronymic_ + "','" + MainWindow.Phone_ + "','" + MainWindow.City_ + "','" + MainWindow.Adres_ + "','" + MainWindow.login_ + "','" + MainWindow.password_ + "')";

                                                sql1 = "Select * from Client where login='" + MainWindow.login_ + "';";
                                                SqlCommand command1 = new SqlCommand(sql1, connection);
                                                SqlDataReader reader1 = command1.ExecuteReader();
                                                while (reader1.Read())
                                                {
                                                    Error.Text = "Такой логин уже используется другим пользователем";
                                                    return;
                                                }
                                                reader1.Close();

                                                //sql2 = "Select * from Customers where email='" + MainWindow.email_ + "';";
                                                //SqlCommand command2 = new SqlCommand(sql2, connection);
                                                //SqlDataReader reader2 = command2.ExecuteReader();
                                                //while (reader2.Read())
                                                //{
                                                //    Error.Text = "Такая почта уже используется другим пользователем";
                                                //    return;
                                                //}
                                                //reader1.Close();

                                                sql2 = "Select * from Client where Phone='" + MainWindow.Phone_ + "';";
                                                SqlCommand command2 = new SqlCommand(sql2, connection);
                                                SqlDataReader reader2 = command2.ExecuteReader();
                                                while (reader2.Read())
                                                {
                                                    Error.Text = "Такой номер телефона уже используется другим пользователем ";
                                                    return;
                                                }
                                                reader2.Close();

                                                //SuccessfulFrame.Content = new RegSucPage();
                                                if ((SuccessfulFrame.Content = new RegSucPage()) != null)
                                                {
                                                    SqlCommand command4 = new SqlCommand(sql4, connection);
                                                    int num = command4.ExecuteNonQuery();
                                                    RegName.IsEnabled = false;
                                                    RegSurname.IsEnabled = false;
                                                    RegPatronymic.IsEnabled = false;
                                                    RegPhone.IsEnabled = false;
                                                    RegCity.IsEnabled = false;
                                                    RegAdres.IsEnabled = false;
                                                    RegLogin.IsEnabled = false;
                                                    RegPassword.IsEnabled = false;
                                                    Reg.IsEnabled = false;
                                                    MainWindow.login_ = RegLogin.Text.Trim();
                                                    MainWindow.password_ = RegPassword.Text.Trim();
                                                    connection.Close();

                                                }
                                                else
                                                    Error.Text = "Неверно изменён пароль. Пароль может содержать в себе только цифры латинские буквы, а так же быть не менее 3-х символов.";

                                            }
                                            else
                                                Error.Text = "Неверно изменён логин. Логин может содержать в себе только цифры латинские буквы, а так же быть не менее 3-х символов.";

                                        }
                                        else
                                            Error.Text = "Неверно изменен адрес. Пример ввода почты: ";
                                    }
                                    else
                                        Error.Text = "Неверно изменен город. Пример ввода почты: ";

                                }
                                else
                                    Error.Text = "Неверно изменён номер телефона.";
                            }
                            else
                                Error.Text = "Неверно изменено отчество. Используйте только буквы кириллицы.";
                        }
                        else
                            Error.Text = "Неверно изменено фамилия. Используйте только буквы кириллицы.";
                    }
                    else
                        Error.Text = "Неверно изменено имя. Используйте только буквы кириллицы.";
                }
            }



        }


        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }





    }
    }


