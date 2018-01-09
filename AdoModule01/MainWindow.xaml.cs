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
//for work with App.config/Web.config
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace AdoModule01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Equipment{
        public int EquipmentId { get; set; }
        public string GarageRoom { get; set; }
        
        
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private string connectionString = "";
        private string connectionStringOleDB = "";

        private void ConnectToServerButton_Click(object sender, RoutedEventArgs e)
        {
            #region exmpl1

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                ConnectMessage.Text += "Подключение открыто... \n";
            }

            catch (Exception ex)
            {
                ConnectMessage.Text += ex.Message + "\n";
            }
            finally
            {
                connection.Close();
                ConnectMessage.Text += "Подключение закрыто... \n";
            }

            #endregion

            #region exmpl2
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                ConnectMessage.Text += "Подключение открыто... \n";
                //Получим информацию о подключении
                ConnectMessage.Text += "Свойство подкючения: \n";
                ConnectMessage.Text += "\t --> строка подключения:  \n" + conn.ConnectionString + "\n";
                ConnectMessage.Text += "\t --> сервер: " + conn.ServerVersion + "\n";
                ConnectMessage.Text += "\t --> workstationId: " + conn.WorkstationId + "\n";
                ConnectMessage.Text += "\t --> состояние: " + conn.State + "\n";
            }
            ConnectMessage.Text += "Подключение закрыто... \n";

            #endregion

            #region exmpl3

            connectionStringOleDB = ConfigurationManager.ConnectionStrings["OleDbConnection"].ConnectionString;

            using (OleDbConnection con = new OleDbConnection(connectionStringOleDB))
            {
                con.Open();
                ConnectMessage.Text += "Подключение открыто... \n";
                ConnectMessage.Text += "Свойство подкючения: \n";
                ConnectMessage.Text += "\t --> строка подключения:  \n" + con.ConnectionString + "\n";
                ConnectMessage.Text += "\t --> сервер: " + con.ServerVersion + "\n";

                ConnectMessage.Text += "\t --> состояние: " + con.State + "\n";

               
            }
            ConnectMessage.Text += "Подключение закрыто... \n";
            #endregion
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            List<Equipment> equipments = new List<Equipment>();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from newEquipment";

              
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Equipment eq = new Equipment();
                    eq.EquipmentId = Int32.Parse(reader[0].ToString());
                    eq.GarageRoom = reader[1].ToString();
                    equipments.Add(eq);
                    
                }

            }
            ListViewEquipment.ItemsSource = equipments;
            



        }
    }
}












