using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeForm.Pages.Employee
{
    public class DeleteModel : PageModel
    {
        public void OnGet()
        {
            try
            {
                string E_ID = Request.Query["id"];
                string connectionString = "Data Source=ADNAN;Initial Catalog=testDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "DELETE from employee WHERE E_ID = @E_ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@E_ID", E_ID);

                        command.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            Response.Redirect("/Employee/Index");
        }
    }
}
