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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Page
    {
       public String connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dunea\source\repos\Question2\Question2\testDb.mdf;Integrated Security=True;Connect Timeout=30";
        public Users()
        {
            InitializeComponent();
        }

        private void Userpage_loaded(object sender, RoutedEventArgs e)
        {
           
            SqlConnection cnn;
            
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //MessageBox.Show("Connection Open  !");

            SqlCommand creatcommand = new SqlCommand("selectUsers", cnn);
            creatcommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataadapt = new SqlDataAdapter(creatcommand);
            DataTable dt = new DataTable("Shifts");
            dataadapt.Fill(dt);
            UserGrid.ItemsSource = dt.DefaultView;
            dataadapt.Update(dt);

            cnn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            using (SqlConnection cnn = new SqlConnection(connetionString))
            {

                cnn.Open();

                String Username, FullName, Branch, Shift;
                // var BranchId, ShiftId;
                int BId, SId;

                DataRowView rowselected = UserGrid.SelectedItem as DataRowView;
                if (rowselected != null)
                {

                    Username = rowselected["Username"].ToString();
                    FullName = rowselected["Fullname"].ToString();
                    Branch = rowselected["BranchDesc"].ToString();
                    Shift = rowselected["ShiftDesc"].ToString();

                    
                    if (Username != "" & FullName != "" & Branch != "" & Shift != "")
                    {

                  

                using (SqlCommand GetBCommand = new SqlCommand("GetBranchId", cnn))
                {
                    GetBCommand.CommandType = CommandType.StoredProcedure;
                    GetBCommand.Parameters.Add("@BranchDesc", SqlDbType.VarChar, 50).Value = Branch;
                    var BranchId = GetBCommand.ExecuteScalar();
                            if (BranchId != null)
                            {
                                GetBCommand.ExecuteNonQuery();
                                BId = Int32.Parse(BranchId.ToString());
                            }
                            else
                            {
                                BId = 0;
                                MessageBox.Show("This Branch Doesnt Exist", "Retry");
                                return;
                            }
                    
                    

                    // MessageBox.Show(BranchId.ToString());

                }
                using (SqlCommand GetSCommand = new SqlCommand("GetShiftId", cnn))
                {
                    GetSCommand.CommandType = CommandType.StoredProcedure;
                    GetSCommand.Parameters.Add("@ShiftDesc", SqlDbType.VarChar, 50).Value = Shift;
                    var ShiftId = GetSCommand.ExecuteScalar();
                            if (ShiftId != null)
                            {
                                GetSCommand.ExecuteNonQuery();
                                SId = Int32.Parse(ShiftId.ToString());

                            }
                            else
                            {
                                SId = 0;
                                MessageBox.Show("This Shift Doesnt Exist", "Retry");
                                return;
                            }
                            

                    //MessageBox.Show(BranchId.ToString());


                }

                using (SqlCommand InsertCommand = new SqlCommand("Insertuser", cnn))
                {
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = new SqlCommand("selectUsers", cnn);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                            DataTable dataTable = new DataTable();

                            InsertCommand.CommandType = CommandType.StoredProcedure;
                    SqlParameter param1 = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                    param1.Value = Username;
                    InsertCommand.Parameters.Add(param1);
                    SqlParameter param2 = new SqlParameter("@FullName", SqlDbType.VarChar, 50);
                    param2.Value = FullName;
                    InsertCommand.Parameters.Add(param2);
                    SqlParameter param3 = new SqlParameter("@BranchId", SqlDbType.Int);
                    param3.Value = BId;
                    InsertCommand.Parameters.Add(param3);
                    SqlParameter param4 = new SqlParameter("@ShiftId", SqlDbType.Int);
                    param4.Value = SId;
                    InsertCommand.Parameters.Add(param4);
                    InsertCommand.ExecuteNonQuery();
                            MessageBox.Show("New User Added", "Success");

                        UserGrid.ItemsSource = null;
                        dataTable.Clear();
                        adapter.Fill(dataTable);
                        adapter.Update(dataTable);
                        UserGrid.ItemsSource = dataTable.DefaultView;


                }
                        

                        
                        cnn.Close();  
                    }
                    else
                    {
                        MessageBox.Show("Make sure to Fill All Fields", "Field Required");
                    }
                    
                }
                else
                {

                    Username = null;
                    FullName = null;
                    Branch = null;
                    Shift = null;
                    MessageBox.Show("Please Make an entry to add", "No Entry Given");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
                

                using (SqlConnection cnn = new SqlConnection(connetionString))
                {

                    cnn.Open();

                    String id,Username, FullName, Branch, Shift;
                    // var BranchId, ShiftId;
                    int BId, SId;

                    DataRowView rowselected = UserGrid.SelectedItem as DataRowView;
                    if (rowselected != null)
                    {
                        id = rowselected["UsersId"].ToString();
                        Username = rowselected["Username"].ToString();
                        FullName = rowselected["Fullname"].ToString();
                        Branch = rowselected["BranchDesc"].ToString();
                        Shift = rowselected["ShiftDesc"].ToString();


                        if (id != "" & Username != "" & FullName != "" & Branch != "" & Shift != "")
                        {



                            using (SqlCommand GetBCommand = new SqlCommand("GetBranchId", cnn))
                            {
                                GetBCommand.CommandType = CommandType.StoredProcedure;
                                GetBCommand.Parameters.Add("@BranchDesc", SqlDbType.VarChar, 50).Value = Branch;
                                var BranchId = GetBCommand.ExecuteScalar();
                                if (BranchId != null)
                                {
                                    GetBCommand.ExecuteNonQuery();
                                    BId = Int32.Parse(BranchId.ToString());
                                }
                                else
                                {
                                    BId = 0;
                                    MessageBox.Show("This Branch Doesnt Exist", "Retry");
                                    return;
                                }



                                // MessageBox.Show(BranchId.ToString());

                            }
                            using (SqlCommand GetSCommand = new SqlCommand("GetShiftId", cnn))
                            {
                                GetSCommand.CommandType = CommandType.StoredProcedure;
                                GetSCommand.Parameters.Add("@ShiftDesc", SqlDbType.VarChar, 50).Value = Shift;
                                var ShiftId = GetSCommand.ExecuteScalar();
                                if (ShiftId != null)
                                {
                                    GetSCommand.ExecuteNonQuery();
                                    SId = Int32.Parse(ShiftId.ToString());

                                }
                                else
                                {
                                    SId = 0;
                                    MessageBox.Show("This Shift Doesnt Exist", "Retry");
                                    return;
                                }


                                //MessageBox.Show(BranchId.ToString());


                            }

                            using (SqlCommand InsertCommand = new SqlCommand("UpdateUser", cnn))
                            {
                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = new SqlCommand("selectUsers", cnn);
                                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                                DataTable dataTable = new DataTable();

                                InsertCommand.CommandType = CommandType.StoredProcedure;
                                SqlParameter param1 = new SqlParameter("@UsersId", SqlDbType.Int);
                                param1.Value = Int32.Parse(id);
                                InsertCommand.Parameters.Add(param1);
                                SqlParameter param2 = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                                param2.Value = Username;
                                InsertCommand.Parameters.Add(param2);
                                SqlParameter param3 = new SqlParameter("@FullName", SqlDbType.VarChar, 50);
                                param3.Value = FullName;
                                InsertCommand.Parameters.Add(param3);
                                SqlParameter param4 = new SqlParameter("@BranchId", SqlDbType.Int);
                                param4.Value = BId;
                                InsertCommand.Parameters.Add(param4);
                                SqlParameter param5 = new SqlParameter("@ShiftId", SqlDbType.Int);
                                param5.Value = SId;
                                InsertCommand.Parameters.Add(param5);
                                InsertCommand.ExecuteNonQuery();
                                MessageBox.Show("User Updated", "Success");

                                UserGrid.ItemsSource = null;
                                dataTable.Clear();
                                adapter.Fill(dataTable);
                                adapter.Update(dataTable);
                                UserGrid.ItemsSource = dataTable.DefaultView;


                            }



                            cnn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Make sure to Fill All Fields or that you have selected an entry to edit", "Field Required");
                        }

                    }
                    else
                    {

                   
                      

                        Username = null;
                        FullName = null;
                        Branch = null;
                        Shift = null;
                        MessageBox.Show("Please Make an entry to add", "No Entry Given");

                }
                }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {



            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("selectUsers", cnn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                cnn.Open();

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);




                int selectedItem;

                DataRowView rowselected = UserGrid.SelectedItem as DataRowView;
                if (rowselected != null)
                {
                    selectedItem = Int32.Parse(rowselected["UsersId"].ToString());
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
                        using (SqlCommand deleteCommand = new SqlCommand("DeleteUser", cnn))
                        {
                            deleteCommand.CommandType = CommandType.StoredProcedure;
                            SqlParameter param1 = new SqlParameter("@Id", SqlDbType.Int);
                            param1.Value = selectedItem;
                            deleteCommand.Parameters.Add(param1);
                            //deleteCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = selectedItem;
                            deleteCommand.ExecuteNonQuery();
                        }


                        MessageBox.Show("Deleted", "Deleted");

                    }
                }
                else
                {
                    MessageBox.Show("Please select an item to delete","No Item Selected");
                }

                //adapter.Update(((DataView)BranchGrid.ItemsSource).Table);
                UserGrid.ItemsSource = null;
                dataTable.Clear();
                adapter.Fill(dataTable);
                adapter.Update(dataTable);
                UserGrid.ItemsSource = dataTable.DefaultView;

                cnn.Close();

            }
        }
    }
}
