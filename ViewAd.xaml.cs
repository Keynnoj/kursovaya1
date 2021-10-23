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
    /// Логика взаимодействия для ViewAd.xaml
    /// </summary>
    public partial class ViewAd : Window
    {
        static string connectionString;
        SqlDataAdapter adapter;
        static DataTable Car;
        static DataTable Ad;
        static DataTable Status;
        public ViewAd()
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
        
        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new Window1().Show();
            this.Close();


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
            string sql = "Select * from Car where Code_car = '"+OutAd.Code_car+"'";
            //string sq2 = "Select * from Ad "; //Name_brand = combobox ;";
            DataTable Car = ExecuteSql(sql);
            //DataTable Ad = ExecuteSql2(sq2);
            LView.ItemsSource = Car.DefaultView;
           // LViewWindowAd.ItemsSource = Ad.DefaultView;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // new DKP().Show();
            //this.Close();


            
                string addplace = "";
                string mainplace = "";
                string idOrder = "0";
                mainplace = $"INSERT INTO Orders(Code_user, order_id, code_car) VALUES ";
                //for (int i = 0; i < OutAd.Code_car; i++)
                {
                    //if (PlacesPage.places_id_[i] != null)
                    {
                        string sql;
                        SqlConnection connection1 = null;
                        sql = "SELECT top(1) order_id from Orders Order by order_id desc;";
                        connection1 = new SqlConnection(connectionString);
                        SqlCommand command1 = new SqlCommand(sql, connection1);
                        connection1.Open();
                        SqlDataReader reader = command1.ExecuteReader();
                        int id = int.Parse(idOrder) + 1;
                        idOrder = id.ToString();
                        while (reader.Read())
                        {

                            idOrder = reader[0].ToString();
                            int id2 = int.Parse(idOrder) + 1;
                            idOrder = id2.ToString();
                        }
                        reader.Close();
                        addplace = $"({MainWindow.Code_user_},{idOrder}, {OutAd.Code_car})";
                        mainplace = String.Concat(mainplace, addplace);
                    }
                }
                //mainplace = mainplace.Remove(mainplace.Length - 1);
                SqlConnection connection = null;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(mainplace, connection);
                int num = command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Покупка прошла успешно.");
                new MenuWindow().Show();
                Helper.CloseWindow(Window.GetWindow(this));
            


            //SqlConnection connection = null;
            //string sql4 = "Update Car set Status = 1 where  Code_car = "+OutAd.Code_car+" ";
            // " @id='" + idClient + "',@Patronymic='" + MainWindow.Patronymic + "',@login='" + MainWindow.login_ + "',@password='" + MainWindow.password_ + "';";
            //Code_ad,Code_car, Name_brand, Model, Equipment,Date_release,Engine_volume,VIN,Transmission_type,Engine_powe,Color,Number_of_doors,Body_type,Drive_type,
            //SqlCommand command3 = new SqlCommand(sql4, connection);
            //int num = command3.ExecuteNonQuery();


        }
    }
}
