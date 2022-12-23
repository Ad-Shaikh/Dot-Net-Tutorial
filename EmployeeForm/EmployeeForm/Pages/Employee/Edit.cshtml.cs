using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeForm.Pages.Employee
{
    public class EditModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string E_ID = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=ADNAN;Initial Catalog=testDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT E_ID, ename, state, city, gender, contact, hobbies, cast(Convert(varchar(20),Emp_DOJ,120) as Date) as Emp_DOJ, Emp_Designation from employee WHERE E_ID = @E_ID";    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@E_ID", E_ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                try
                                {
                                    employeeInfo.E_ID = "" + reader.GetInt32(0);
                                    employeeInfo.ename = reader.GetString(1);
                                    employeeInfo.state = reader.GetString(2);
                                    employeeInfo.city = reader.GetString(3);
                                    employeeInfo.gender = reader.GetString(4);
                                    employeeInfo.contact = reader.GetString(5);
                                    employeeInfo.Emp_DOJ = reader.GetDateTime(7).ToString("yyyy-MM-dd");
                                    employeeInfo.Emp_Designation = reader.GetString(8);
                                    employeeInfo.hobbies = reader.GetString(6);

                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                throw;
            }
        }
        public void OnPost()
        {
            employeeInfo.E_ID = Request.Form["id"];
            employeeInfo.ename = Request.Form["ename"];
            employeeInfo.state = Request.Form["state"];
            employeeInfo.city = Request.Form["city"];
            employeeInfo.gender = Request.Form["gender"];
            employeeInfo.contact = Request.Form["contact"];
            employeeInfo.Emp_DOJ = Request.Form["doj"];
            employeeInfo.Emp_Designation = Request.Form["desg"];
            employeeInfo.hobbies = Request.Form["hobbies"];

            if (employeeInfo.ename.Length == 0 || employeeInfo.state.Length == 0 ||  employeeInfo.city.Length == 0 || employeeInfo.gender.Length == 0 || employeeInfo.contact.Length == 0 ||
                employeeInfo.Emp_Designation.Length == 0 || employeeInfo.hobbies.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=ADNAN;Initial Catalog=testDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE employee " + 
                                 "SET ename=@ename, state=@state, city=@city, gender=@gender, contact=@contact, Emp_DOJ=@Emp_DOJ, Emp_Designation=@Emp_Designation, hobbies=@hobbies " +
                                 "WHERE E_ID=@E_ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@E_ID", employeeInfo.E_ID);
                        command.Parameters.AddWithValue("@ename", employeeInfo.ename);                       
                        command.Parameters.AddWithValue("@state", employeeInfo.state);
                        command.Parameters.AddWithValue("@city", employeeInfo.city);
                        command.Parameters.AddWithValue("@gender", employeeInfo.gender);
                        command.Parameters.AddWithValue("@contact", employeeInfo.contact);
                        command.Parameters.AddWithValue("@Emp_DOJ", employeeInfo.Emp_DOJ);
                        command.Parameters.AddWithValue("@Emp_Designation", employeeInfo.Emp_Designation);
                        command.Parameters.AddWithValue("@hobbies", employeeInfo.hobbies);

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception es)
            {
                errorMessage = es.Message;
                throw;
            }

            Response.Redirect("/Employee/Index");
        }

    }
}
