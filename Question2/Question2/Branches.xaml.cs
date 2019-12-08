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
    /// Interaction logic for Branches.xaml
    /// </summary>
    public partial class Branches : Page
    {
        
        public Branches()
        {
            InitializeComponent();
        }

        private void Branchpage_loaded(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dunea\source\repos\Question2\Question2\testDb.mdf;Integrated Security=True;Connect Timeout=30";
            cnn = new SqlConnection(connetionString);
            
            
            //MessageBox.Show("Connection Open  !");

            SqlCommand creatcommand = new SqlCommand("selectBranch", cnn);
            creatcommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataadapt = new SqlDataAdapter(creatcommand);
            DataTable dt = new DataTable("Branches");
            dataadapt.Fill(dt);
            BranchGrid.ItemsSource = dt.DefaultView;
            dataadapt.Update(dt);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dunea\source\repos\Question2\Question2\testDb.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection cnn = new SqlConnection(connString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("selectBranch", cnn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                cnn.Open();

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                adapter.Update(((DataView)BranchGrid.ItemsSource).Table);

                BranchGrid.ItemsSource = null;
                dataTable.Clear();
                adapter.Fill(dataTable);
                adapter.Update(dataTable);
                BranchGrid.ItemsSource = dataTable.DefaultView;
                
                cnn.Close();


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dunea\source\repos\Question2\Question2\testDb.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection cnn = new SqlConnection(connString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("selectBranch", cnn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                cnn.Open();

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                
                
                adapter.Update(((DataView)BranchGrid.ItemsSource).Table);
              
               
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dunea\source\repos\Question2\Question2\testDb.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection cnn = new SqlConnection(connString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("selectBranch", cnn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                cnn.Open();

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                int selectedItem;

                DataRowView rowselected = BranchGrid.SelectedItem as DataRowView;
                if (rowselected != null)
                {
                    selectedItem = Int32.Parse(rowselected["BranchesId"].ToString());
                }
                else
                {
                    selectedItem = -1;
                }
                //var selectedItem = BranchGrid.SelectedItem[0];
                //int id = BranchGrid.Items.IndexOf(selectedItem);
                
                   
               
                if (selectedItem > -1)
                {
                    MessageBoxResult dialogResult =  MessageBox.Show("Are you sure you want to delete this entry?", "Confirm", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        using (SqlCommand deleteCommand = new SqlCommand("DeleteBranch", cnn))
                    {
                        deleteCommand.CommandType = CommandType.StoredProcedure;
                        SqlParameter param1 = new SqlParameter("@Id", SqlDbType.Int);
                        param1.Value = selectedItem;
                        deleteCommand.Parameters.Add(param1);
                        //deleteCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = selectedItem;
                        deleteCommand.ExecuteNonQuery();
                    }


                    MessageBox.Show("Deleted","Deleted");

                    }
                    
                }
                else
                {
                    MessageBox.Show("Please select an item to delete","No Item Selected");
                }

                //adapter.Update(((DataView)BranchGrid.ItemsSource).Table);
                BranchGrid.ItemsSource = null;
                dataTable.Clear();
                adapter.Fill(dataTable);
                adapter.Update(dataTable);
                BranchGrid.ItemsSource = dataTable.DefaultView;

                cnn.Close();

            }

            
        }
    }
}
