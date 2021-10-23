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
    /// Логика взаимодействия для CostCar.xaml
    /// </summary>
    /// public static string Code_user_ { get; set; }
    public partial class CostCar : Page
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Brand;
        DataTable Car;
        DataTable Transmission;
        DataTable Body;
        DataTable Drive;
        public static string Brand2 { get; set; }
        public static string Model2 { get; set; }
      


        public CostCar()
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
            string sql2;
            string sq3;
            string sql4;
            string sql5;
            SqlConnection connection = null;
            Brand = new DataTable();
            Transmission = new DataTable();
            Body = new DataTable();
            Drive = new DataTable();
            sql = "SELECT Name_brand from Brand";
            sql2 = "SELECT Drive_type from Drive";
            sq3 = "SELECT Transmission_type from Transmission";
            sql4 = "SELECT Body_type from Body";
            sql5 = "INSERT INTO Car (Code_ad,Code_car, Name_brand, Model, Equipment,Date_release,Engine_volume,VIN,Transmission_type,Engine_powe,Color,Number_of_doors,Body_type,Photo,Drive)" +
                " VALUES ("+Code_car+",'"+ComboBoxBrand1+"','"+ComboBoxModel1+"', '"+Equipmnet+"','"+Date_release+"','"+Engine_volume+"','"+VIN+"','"+ComboBoxTransmission+"',"+Engine_powe+",'"+Color+"',"+Number_of_doors+",'"+ComboBoxBody+"','"+Photo+"','"+Drive+"')";
            connection = new SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, connection);
            SqlDataAdapter adapter3 = new SqlDataAdapter(sq3, connection);
            SqlDataAdapter adapter4 = new SqlDataAdapter(sql4, connection);
            SqlDataAdapter adapter5 = new SqlDataAdapter(sql5, connection);
            adapter.Fill(Brand);
            for (int i = 0; i < Brand.Rows.Count; i++)
            {
                ComboBoxBrand1.Items.Add(Brand.Rows[i]["Name_brand"].ToString());
            }


            adapter3.Fill(Transmission);
            for (int i = 0; i < Transmission.Rows.Count; i++)
            {
                ComboBoxTransmission.Items.Add(Transmission.Rows[i]["Transmission_type"].ToString());
            }

            adapter4.Fill(Body);
            for (int i = 0; i < Body.Rows.Count; i++)
            {
                ComboBoxBody.Items.Add(Body.Rows[i]["Body_type"].ToString());
            }
            adapter2.Fill(Drive);
            for (int i = 0; i < Drive.Rows.Count; i++)
            {
                ComboBoxDrive.Items.Add(Drive.Rows[i]["Drive_type"].ToString());
            }
            //else

            // Error.Text = "ошибка";


        }

        private void ComboBoxModel_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void ComboBoxBrand_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void ComboBoxBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxModel1.SelectedItem = null;
            ComboBoxModel1.Items.Clear();
            if (ComboBoxBrand1.SelectedItem.ToString() != "")
            {
                string sql2;
                SqlConnection connection = null;
                Car = new DataTable();
                sql2 = "SELECT Model from Model where Name_brand='" + ComboBoxBrand1.SelectedItem.ToString() + "' ";
                connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, connection);

                connection.Open();

                adapter2.Fill(Car);
                for (int i = 0; i < Car.Rows.Count; i++)
                {
                    ComboBoxModel1.Items.Add(Car.Rows[i]["Model"].ToString());
                }


            }
            //else

            // Error.Text = "ошибка";
        }

    private void ComboBoxTransmission_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

        private void AddAd_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            string sql;
            string sql2;
            string sql5;
            string sql6;
            string sql7;
            string CodeAd = null;
            string CodeCar = null;
            connection = new SqlConnection(connectionString);
            connection.Open();

            sql = "SELECT top(1) Code_ad from Ad Order by Code_ad desc;";
            sql2 = "SELECT top(1) Code_car from Ad Order by Code_car desc;";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlCommand command2 = new SqlCommand(sql2, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CodeAd = reader[0].ToString();
                int id = int.Parse(CodeAd) + 1;
                CodeAd = id.ToString();
            }
            reader.Close();
            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                CodeCar = reader2[0].ToString();
                int id = int.Parse(CodeCar) + 1;
                CodeCar = id.ToString();
            }
            // MainWindow() = idUser;
           //reader.Close();
            reader2.Close();

            sql6 = "INSERT INTO Car (Code_ad,Code_car, Name_brand, Model, Equipment,Date_release,Engine_volume,VIN,Transmission_type,Engine_powe,Color,Number_of_doors,Body_type,Drive_type)" +
                " VALUES ('" + CodeAd + "'," + CodeCar + ",'" + ComboBoxBrand1.SelectedItem.ToString() + "','" + ComboBoxModel1.SelectedItem.ToString() + "', '" + Equipmnet.Text + "','" + Date_release.Text + "','" + Engine_volume.Text + "','" + VIN.Text + "','" + ComboBoxTransmission.SelectedItem.ToString() + "'," + Engine_powe.Text + ",'" + Color.Text + "'," + Number_of_doors.Text + ",'" + ComboBoxBody.SelectedItem.ToString() + "','" + ComboBoxDrive.SelectedItem.ToString() + "')";
            sql5 = "INSERT INTO Ad (Code_ad,Code_car,Code_user,Date_ad,Cost)" + " VALUES ('"+CodeAd+"','"+CodeCar+"','"+MainWindow.Code_user_+"','"+DateTime.Now+"','"+Cost.Text+"')";
            sql7 = "INSERT INTO STS (Code_user,Code_car,Gos_number,VIN,Number,Series)" + "VALUES ('" + MainWindow.Code_user_ + "','"+CodeAd+"','" + Gos_number.Text + "','" + VIN.Text + "','" + Number.Text + "','" + Series.Text + "')";
            SqlCommand command3 = new SqlCommand(sql5, connection);
            SqlCommand command4 = new SqlCommand(sql6, connection);
            SqlCommand command5 = new SqlCommand(sql7, connection);
            int num = command3.ExecuteNonQuery();
            int num1 = command4.ExecuteNonQuery();
            int num2 = command5.ExecuteNonQuery();

            if (num != 0)
            {
                Suc.Text = "Успешно добавлено";
            }
            else
                Error.Text = "Ошибка добавления";
        }

        private void Number_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
 
