using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Question2
{
    /// <summary>
    /// Interaction logic for Shifts.xaml
    /// </summary>
    public partial class Shifts : Page
    {
        public String connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dunea\source\repos\Question2\Question2\testDb.mdf;Integrated Security=True;Connect Timeout=30";

        public Shifts()
        {
            InitializeComponent();
        }

        private void Shiftpage_Loaded(object sender, RoutedEventArgs e)
        {
        SqlConnection cnn;
        cnn = new SqlConnection(connetionString);
        cnn.Open();
        //MessageBox.Show("Connection Open  !");

            SqlCommand creatcommand = new SqlCommand("selectShift", cnn);
            creatcommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataadapt = new SqlDataAdapter(creatcommand);
            DataTable dt = new DataTable("Shifts");
            dataadapt.Fill(dt);
            ShiftGrid.ItemsSource = dt.DefaultView;
            dataadapt.Update(dt);

        cnn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowselected = ShiftGrid.SelectedItem as DataRowView;
            if (rowselected != null)
            {
                String ShiftDesct = rowselected["ShiftDesc"].ToString();
                if (ShiftDesct != "")
                {
                    using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("selectShift", cnn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                cnn.Open();

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                adapter.Update(((DataView)ShiftGrid.ItemsSource).Table);

                ShiftGrid.ItemsSource = null;
                dataTable.Clear();
                adapter.Fill(dataTable);
                adapter.Update(dataTable);
                ShiftGrid.ItemsSource = dataTable.DefaultView;
                        MessageBox.Show("New Entry Added", "Success");

                        cnn.Close();


            }
                }
                else
                {
                    MessageBox.Show("Please enter a Shift Description", "No Value");
                }
            }
            else
            {
                MessageBox.Show("Please Add an item", "Nothing to add");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataRowView rowselected = ShiftGrid.SelectedItem as DataRowView;
            if (rowselected != null)
            {
                String ShiftDesct = rowselected["ShiftDesc"].ToString();
                if (ShiftDesct != "")
                {
                    using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("selectShift", cnn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                cnn.Open();

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);



                adapter.Update(((DataView)ShiftGrid.ItemsSource).Table);
                        MessageBox.Show("Shift Edited", "Success");



                    }
                }
                else
                {
                    MessageBox.Show("Please enter a Shift Description", "No Value");
                }
            }
            else
            {
                MessageBox.Show("Please Edit an item", "Nothing selected");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("selectShift", cnn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                cnn.Open();

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                int selectedItem;

                DataRowView rowselected = ShiftGrid.SelectedItem as DataRowView;
                if (rowselected != null)
                {
                    selectedItem = Int32.Parse(rowselected["ShiftsId"].ToString());
                }
                else
                {
                    selectedItem = -1;
                }



                if (selectedItem > -1)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete this entry?", "Confirm", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        using (SqlCommand deleteCommand = new SqlCommand("DeleteShift", cnn))
                        {
                            deleteCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter param1 = new SqlParameter("@Id", SqlDbType.Int);
                            param1.Value = selectedItem;
                            deleteCommand.Parameters.Add(param1);
                            deleteCommand.ExecuteNonQuery();
                        }


                        MessageBox.Show("Deleted", "Deleted");

                    }

                }
                else
                {
                    MessageBox.Show("Please select an item to delete", "No Item Selected");
                }

                //adapter.Update(((DataView)BranchGrid.ItemsSource).Table);
                ShiftGrid.ItemsSource = null;
                dataTable.Clear();
                adapter.Fill(dataTable);
                adapter.Update(dataTable);
                ShiftGrid.ItemsSource = dataTable.DefaultView;

                cnn.Close();

            }


        }
    }
       
    }
