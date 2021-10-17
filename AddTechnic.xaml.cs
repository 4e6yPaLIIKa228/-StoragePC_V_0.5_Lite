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
using System.Data.SQLite;
using YchetPer.Connection;
using System.Data;


namespace YchetPer
{
    /// <summary>
    /// Логика взаимодействия для AddTechnic.xaml
    /// </summary>
    public partial class AddTechnic : Window
    {
        public AddTechnic()
        {
            InitializeComponent();
            CbFill();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void CbFill()  //Данные для комбобоксов 
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT * FROM Types"; // Типы
                    string query2 = $@"SELECT * FROM Conditions"; // Состояние
                    string query3 = $@"SELECT * FROM NumberKabs"; // Кабинеты
                    string query4 = $@"SELECT  Devices.Title FROM Devices"; // Названия
                    //----------------------------------------------
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
                    SQLiteCommand cmd3 = new SQLiteCommand(query3, connection);
                    SQLiteCommand cmd4 = new SQLiteCommand(query4, connection);
                    //----------------------------------------------
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    SQLiteDataAdapter SDA2 = new SQLiteDataAdapter(cmd2);
                    SQLiteDataAdapter SDA3 = new SQLiteDataAdapter(cmd3);
                    SQLiteDataAdapter SDA4 = new SQLiteDataAdapter(cmd4);
                    //----------------------------------------------
                    DataTable dt1 = new DataTable("Types");
                    DataTable dt2 = new DataTable("Conditions");
                    DataTable dt3 = new DataTable("NumberKabs");
                    DataTable dt4 = new DataTable("Devices");
                    //----------------------------------------------
                    SDA1.Fill(dt1);
                    SDA2.Fill(dt2);
                    SDA3.Fill(dt3);
                    SDA4.Fill(dt4);
                    //----------------------------------------------
                    CbClass.ItemsSource = dt1.DefaultView;
                    CbClass.DisplayMemberPath = "Class";
                    CbClass.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CbCondition.ItemsSource = dt2.DefaultView;
                    CbCondition.DisplayMemberPath = "Condition";
                    CbCondition.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CbNumKabn.ItemsSource = dt3.DefaultView;
                    CbNumKabn.DisplayMemberPath = "NumKab";
                    CbNumKabn.SelectedValuePath = "ID";
                    ////----------------------------------------------
                    CbTitle.ItemsSource = dt4.DefaultView;
                    CbTitle.DisplayMemberPath = "Title";
                    CbTitle.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }




            private void BtnAdd_Click(object sender, RoutedEventArgs e) //Добавление
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                connection.Open();
                string query = $@"INSERT INTO Devices(IDType,IDKabuneta,Title,Number,IDCondition) values (@IDType,@IDKabuneta,@Title,@Number,@IDCondition);";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    //cmd.Parameters.AddWithValue("@SN", TbSN.Text);
                    //cmd.Parameters.AddWithValue("@FN", TbFN.Text);
                    //cmd.Parameters.AddWithValue("@MN", TbMN.Text);
                    //cmd.Parameters.AddWithValue("@Date", DpB.Text);
                    //cmd.Parameters.AddWithValue("@Phone", TbPhone.Text);
                    //cmd.Parameters.AddWithValue("@Post", );
                    //cmd.Parameters.AddWithValue("@Stat", );
                    //cmd.ExecuteNonQuery();
                    this.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
