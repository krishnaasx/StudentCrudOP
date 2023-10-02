using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SbmsCrudOperation02.Pages.Students
{
    public class IndexModel : PageModel
    {

        public List<Students> ListStudents = new List<Students>();
        public void OnGet()
        {

            try
            {
                string connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=StudentCrudOP;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Student";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Students students = new Students();
                                students.StudentID = reader.GetString(0);
                                students.StudentName = reader.GetString(1);
                                students.StudentPhoneNumber =reader.GetString(2);

                                ListStudents.Add(students);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.ToString());
            }
        }
    }

    public class Students
    {

        public string StudentID;
        public string StudentName;
        public string StudentPhoneNumber;
    }
}
