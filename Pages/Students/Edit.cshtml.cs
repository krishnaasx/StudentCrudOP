using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace SbmsCrudOperation02.Pages.Students
{
    public class EditModel : PageModel
    {
        public Students students = new Students();
        public string errorMessage = "";
        public string seccessMessage = "";
        public void OnGet()
        {
            String StudentID = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=StudentCrudOP;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT*FROM Student WHERE StudentID=@StudentID;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID",StudentID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            { 
                                students.StudentID = reader.GetString(0);
                                students.StudentName = reader.GetString(1);
                                students.StudentPhoneNumber = reader.GetString(2);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }
        }
        public void OnPost()
        {
            students.StudentID = Request.Form["StudentID"];
            students.StudentName = Request.Form["StudentName"];
            students.StudentPhoneNumber = Request.Form["StudentPhoneNumber"];

            if (students.StudentID.Length == 0 || students.StudentName.Length == 0 || students.StudentPhoneNumber.Length == 0)
            {
                errorMessage = "All fields are required!!";
                return;
            }


            try
            {

                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=StudentCrudOP;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Student SET StudentID=@StudentID, StudentName=@StudentName, StudentPhoneNumber=@StudentPhoneNumber WHERE StudentID=@StudentID";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", students.StudentID);
                        command.Parameters.AddWithValue("@StudentName", students.StudentName);
                        command.Parameters.AddWithValue("@StudentPhoneNumber", students.StudentPhoneNumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            Response.Redirect("/Students/Index");
        }
    }
}
