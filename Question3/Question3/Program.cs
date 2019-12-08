using System;
using System.Data.SqlClient;
namespace Question3
{
    class Program
    {
        static void Main(string[] args)
        {
            String UserInput;
            int branchCode, shiftCode;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dunea\source\repos\Question2\Question2\testDb.mdf;Integrated Security=True;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connection Open  !");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.WriteLine("Enter a Branch ID: ");
            UserInput = Console.ReadLine();
            branchCode = Convert.ToInt32(UserInput);
            Console.WriteLine("Enter a Shift ID: ");
            UserInput = Console.ReadLine();
            shiftCode = Convert.ToInt32(UserInput);

            SqlCommand command = new SqlCommand("Select u.Username, u.Fullname, s.ShiftDesc, b.BranchDesc  from Users u " +
                "INNER JOIN Branches b on b.BranchesId = u.BranchesId " +
                "INNER JOIN  Shifts s on s.ShiftsId = u.ShiftsID where b.BranchesId=@BranchesId AND s.ShiftsID=@ShiftsID", cnn);
            command.Parameters.AddWithValue("@ShiftsId", shiftCode); 
            command.Parameters.AddWithValue("@BranchesId", branchCode);
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("UserName - FullName - Shift - Branch");
                while (reader.Read())
                {
                    
                    Console.WriteLine(String.Format("{0} - {1} - {2} - {3}", reader[0],
                        reader[1], reader[2], reader[3]));
                }
            }
            cnn.Close();

            SqlConnection sqlcon = new SqlConnection();
        }
    }
}
