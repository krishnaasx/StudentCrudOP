using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SbmsCrudOperation02.Pages.Students
{
    public class CreateModel : PageModel
    {
        public Students students = new Students();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            students.StudentID = Request.Form["StudentID"];
            students.StudentName = Request.Form["StudentName"];
            students.StudentPhoneNumber = Request.Form["StudentPhoneNumber"];

            if(students.StudentID.Length == 0 || students.StudentName.Length == 0 || students.StudentPhoneNumber.Length ==0)
            {
                errorMessage = "All fields are required!!";
                return;
            }

            //save the new student into the database 


            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=StudentCrudOP;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Student" 
                        + "(StudentID , StudentName , StudentPhoneNumber) VALUES" 
                        + "(@StudentID , @StudentName,@StudentPhoneNumber);";

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

            students.StudentID = ""; students.StudentName = ""; students.StudentPhoneNumber = "";
            successMessage = "New student added correctly!";

            Response.Redirect("/Students/Index");
        }
    }
}
