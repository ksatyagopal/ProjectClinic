using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CMSDAL
{
    public class Authentication
    {
        //This method Verifies whether the login credentials are valid or not
        public static bool ValidateCredentials(string userName, string password)
        {
            try
            {
                if (userName.Length > 10)
                {
                    return false;
                }
                if (!userName.All(char.IsLetterOrDigit))
                {
                    return false;
                }
                if (!password.Contains('@'))
                {
                    return false;
                }
                SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = ClinicDB; Integrated Security = true;");
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM OfficeStaff WHERE UserName = '{userName}' and Password = '{password}'", conn);
                if (Convert.ToInt32(cmd.ExecuteScalar()) == 1)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
