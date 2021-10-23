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
using System.Text.RegularExpressions;

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        public SqlConnection connection;
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Client;

        public static string login_ { get; set; }
        public static string password_ { get; set; }
        public EditPage()
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

        private void SaveButton(object sender, RoutedEventArgs e)

        {
            Error.Text = "";
            string sql;
            string phone_ = Phone.Text;
            if ((Name.Text.Trim() == "") && (Surname.Text.Trim() == "")  && (Patronymic.Text.Trim() == "") && (Phone.Text.Trim() == "") && (City.Text.Trim() == "") && (Adres.Text.Trim() == "") && (Login.Text.Trim() == "") && (Password.Text.Trim() == ""))
            {
                Error.Text = "Вы ничего не ввели.";
            }
            else
            if (Name.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели имя.";
            }
            else
            if (Surname.Text.Trim() == "")
            {
                Error.Text = "Вы не фамилию.";
            }
            else
            if (Patronymic.Text.Trim() == "")
            {
                Error.Text = "Вы не отчество.";
            }
            else
            if (Phone.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели номер телефона.";
            }
            else
            if (City.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели свой город.";
            }
            else
            if (Adres.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели свой адрес.";
            }
            else
            if (Login.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели логин.";
            }
            else
            if (Password.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели пароль.";
            }
            else
            if ((Name.Text.Trim() != "") && (Surname.Text.Trim() != "") && (Patronymic.Text.Trim() != "") && (Phone.Text.Trim() != "") && (City.Text.Trim() != "") && (Adres.Text.Trim() != "") && (Login.Text.Trim() != "") && (Password.Text.Trim() != ""))
            {
                const string namePattern = @"^[А-Яа-яЁё\s]+$";
                const string surnamePattern = @"^[А-Яа-яЁё\s]+$";
                const string PatronymicPattern = @"^[А-Яа-яЁё\s]+$";
                const string phonePattern = @"[0-9]{11,}";
                const string cityPattern = @"^[А-Яа-яЁё\s]+$";
                const string adresPattern = @"^[А-Яа-яЁё\s]+$";
                const string loginPattern = @"^[A-Za-z0-9]{3,}$";
                const string passwordPattern = @"^[A-Za-z0-9]{3,}$";


                var newname = Name.Text.Trim().ToLowerInvariant();
                var newsurname = Surname.Text.Trim().ToLowerInvariant();
                var newPatronymic = Patronymic.Text.Trim().ToLowerInvariant();
                var newphone = Phone.Text.Trim().ToLowerInvariant();
                var newcity = City.Text.Trim().ToLowerInvariant();
                var newadres = Adres.Text.Trim().ToLowerInvariant();
                var newlogin = Login.Text.Trim().ToLowerInvariant();
                var newpassword = Password.Text.Trim().ToLowerInvariant();

                if (Regex.Match(newname, namePattern).Success)
                {
                    if (Regex.Match(newsurname, surnamePattern).Success)
                    {
                        if (Regex.Match(newPatronymic, PatronymicPattern).Success)
                        {
                            if (Regex.Match(newphone, phonePattern).Success)
                            {
                                if (Regex.Match(newcity, cityPattern).Success)
                                {
                                    if (Regex.Match(newadres, adresPattern).Success)
                                    {
                                        if (Regex.Match(newlogin, loginPattern).Success)
                                        {
                                            if (Regex.Match(newpassword, passwordPattern).Success)
                                            {
                                                Client = new DataTable();
                                                SqlConnection connection = null;
                                                sql = "UPDATE Client SET name ='" + Name.Text.Trim() + "',Surname ='" + Surname.Text.Trim() + "',Patronymic ='" + Patronymic.Text.Trim() + "',Phone ='" + Phone.Text.Trim() + "',City ='" + City.Text.Trim() + "',Adres ='" + Adres.Text.Trim() + "',Login ='" + Login.Text.Trim() + "',Password ='" + Password.Text.Trim() + "' WHERE Code_user = '" + MainWindow.Code_user_.Trim() + "'; ";
                                                //sql = "UPDATE Customers SET name='" + Name.Text.Trim() + "', date_of_birth='" + Surname.Text.Trim() + "',phonenumber='" + Phone.Text.Trim() + "', Patronymic='" + Patronymic.Text.Trim() + "', login='" + Login.Text.Trim() + "', password='" + Password.Text.Trim() + "' WHERE customer_id='" + MainWindow.id_.Trim() + "';";
                                                connection = new SqlConnection(connectionString);
                                                SqlCommand command = new SqlCommand(sql, connection);
                                                connection.Open();
                                                int num = command.ExecuteNonQuery();
                                                MainWindow.Name_ = Name.Text.Trim();
                                                MainWindow.Surname_ = Surname.Text.Trim();
                                                MainWindow.Patronymic_ = Patronymic.Text.Trim();
                                                MainWindow.Phone_ = Phone.Text.Trim();
                                                MainWindow.City_ = City.Text.Trim();
                                                MainWindow.Adres_ = Adres.Text.Trim();
                                                MainWindow.login_ = Login.Text.Trim();
                                                MainWindow.password_ = Password.Text.Trim();
                                                this.NavigationService.Navigate(new LK());
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

                private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (MainWindow.Name_ != "")
            {
                Name.Text = MainWindow.Name_;
            }
            if (MainWindow.Surname_ != "")
            {
                Surname.Text = MainWindow.Surname_;
            }
            if (MainWindow.Phone_ != "")
            {

                Phone.Text = MainWindow.Phone_;
            }
            if (MainWindow.Patronymic_ != "")
            {
                Patronymic.Text = MainWindow.Patronymic_;
            }
            if (MainWindow.City_ != "")
            {
                City.Text = MainWindow.City_;
            }
            if (MainWindow.Adres_ != "")
            {
                Adres.Text = MainWindow.Adres_;
            }
            if (MainWindow.login_ != "")
            {
                Login.Text = MainWindow.login_;
            }

            if (MainWindow.password_ != "")
            {
                Password.Text = MainWindow.password_;

            }
        }
    }
}
