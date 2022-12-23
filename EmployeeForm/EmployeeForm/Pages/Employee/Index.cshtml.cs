using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeForm.Pages.Employee
{
    public class IndexModel : PageModel
    {
        public List<EmployeeInfo> listemployee = new List<EmployeeInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=ADNAN;Initial Catalog=testDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT E_ID, ename, state, city, gender, contact, hobbies, cast(Convert(varchar(20),Emp_DOJ,120) as Date), Emp_Designation from employee";
                    using (SqlCommand command =  new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader =  command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                EmployeeInfo employeeInfo = new EmployeeInfo();
                                employeeInfo.E_ID = "" + reader.GetInt32(0);
                                employeeInfo.ename = reader.GetString(1);
                                employeeInfo.state = reader.GetString(2);
                                employeeInfo.city = reader.GetString(3);
                                employeeInfo.gender = reader.GetString(4);
                                employeeInfo.contact = reader.GetString(5);
                                employeeInfo.hobbies = reader.GetString(6);
                                employeeInfo.Emp_DOJ = reader.GetDateTime(7).ToString("yyyy-MM-dd");
                                employeeInfo.Emp_Designation = reader.GetString(8);

                                listemployee.Add(employeeInfo);
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " +  ex.ToString());

                throw;
            }
        }
    }
    public class EmployeeInfo
    {
        public string? E_ID;
        public string? ename;
        public string? state;
        public string? city;
        public string? gender;
        public string? contact;
        public string? hobbies;
        public string? Emp_DOJ;
        public string? Emp_Designation;

    }
}
