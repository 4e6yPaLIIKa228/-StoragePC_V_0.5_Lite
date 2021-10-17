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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DisplayData();
            DGAllEmp.Columns[0].IsReadOnly = true;
            DGAllEmp.Columns[1].IsReadOnly = true;
            DGAllEmp.Columns[2].IsReadOnly = true;
            DGAllEmp.Columns[3].IsReadOnly = true;
            DGAllEmp.Columns[4].IsReadOnly = true;
            DGAllEmp.Columns[5].IsReadOnly = true;
            DGAllEmp.Columns[6].IsReadOnly = true;
            //DGAllEmp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DGAllEmp.AllowUserToAddRows = false;


        }

        public void DisplayData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT Devices.ID, Types.Class, Devices.Title, Devices.Number, Conditions.Condition ,NumberKabs.NumKab ,Devices.StartWork 
                                      FROM Devices JOIN  Types
                                      ON Devices.IDType = Types.ID
                                      JOIN  Conditions
                                      ON Devices.IDCondition = Conditions.ID
                                      JOIN  NumberKabs
                                      ON Devices.IDKabuneta = NumberKabs.ID;";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("Devices");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGAllEmp.ItemsSource = DT.DefaultView;

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }


            }
        }
        public void UpdateDG()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT Devices.ID, Types.Class, Devices.Title, Devices.Number, Conditions.Condition ,NumberKabs.NumKab ,Devices.StartWork 
                                    FROM Devices JOIN  Types
                                    ON Devices.IDType = Types.ID
                                    JOIN  Conditions
                                    ON Devices.IDCondition = Conditions.ID
                                    JOIN  NumberKabs
                                    ON Devices.IDKabuneta = NumberKabs.ID;";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("Devices");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGAllEmp.ItemsSource = DT.DefaultView;

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }


            }
        }
        public void Delete()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {

                    foreach (var item in DGAllEmp.SelectedItems.Cast<DataRowView>())
                    {
                        string query1 = $@"DELETE FROM Devices WHERE id = " + item["ID"];
                        connection.Open();

                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        DataTable DT = new DataTable("Devices");
                        cmd1.ExecuteNonQuery();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }


            }
        }
        private void BtnUpd_Click(object sender, RoutedEventArgs e)
        {
            UpdateDG();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            UpdateDG();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTechnic AddTec = new AddTechnic();
            AddTec.Owner = this;
            AddTec.ShowDialog();


        }


    }
}

