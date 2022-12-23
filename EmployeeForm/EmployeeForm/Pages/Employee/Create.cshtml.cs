using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeForm.Pages.Employee
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        { 
            employeeInfo.ename = Request.Form["ename"];
            employeeInfo.state = Request.Form["state"];
            employeeInfo.city = Request.Form["city"];
            employeeInfo.gender = Request.Form["gender"];
            employeeInfo.contact = Request.Form["contact"];            
            employeeInfo.Emp_DOJ = Request.Form["doj"];
            employeeInfo.Emp_Designation = Request.Form["desg"];
            employeeInfo.hobbies = Request.Form["hobbies"];

            if (employeeInfo.ename.Length == 0 || employeeInfo.state.Length == 0 || employeeInfo.city.Length == 0 || employeeInfo.contact.Length == 0 ||
                employeeInfo.Emp_Designation.Length == 0 || employeeInfo.hobbies.Length == 0)
            {
                errorMessage = "All fields are required";
                return;

            }

            //insert data into database

            try
            {
                String connectionString = "Data Source=ADNAN;Initial Catalog=testDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT into employee" +
                                 "(ename, state, city, gender, contact, Emp_DOJ, Emp_Designation, hobbies ) VALUES " + 
                                 "(@ename, @state, @city, @gender, @contact, @Emp_DOJ, @Emp_Designation, @hobbies);";
                    using (SqlCommand command =  new SqlCommand(sql, connection))
                    {
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
            catch (Exception)
            {

                throw;
            }

            employeeInfo.ename = ""; employeeInfo.state = ""; employeeInfo.city = ""; employeeInfo.gender = ""; employeeInfo.contact = ""; employeeInfo.Emp_DOJ = ""; employeeInfo.Emp_Designation = ""; employeeInfo.hobbies = "";
            successMessage = "New Employee Added succesfully";

            Response.Redirect("/Employee/Index");
        }
    }
}
