using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CMSDAL
{
    public class ManagePatient
    {
        public static SqlConnection conn = null;
        public static SqlCommand cmd;

        //This returns all the patients details in database
        public List<ArrayList> PatientList()
        {
            List<ArrayList> Patients = new();
            conn = InitiateDB();
            cmd = new SqlCommand("SELECT * FROM Patients", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ArrayList arr = new();
                arr.Add(Convert.ToInt32(reader[0]));
                arr.Add(reader[1].ToString());
                arr.Add(reader[2].ToString());
                arr.Add(reader[3].ToString());
                arr.Add(Convert.ToInt32(reader[4]));
                arr.Add(DateTime.Parse(reader[5].ToString()));
                Patients.Add(arr);
            }
            conn.Close();
            return Patients;
        }

        //This method adds new patient into database
        public ArrayList AddNewPatient(string FName, string LName, string Gen, int Age, DateTime DOB)
        {
            ArrayList CreatedPatientDetails = new();
            try
            {
                conn = InitiateDB();
                cmd = new SqlCommand("AddPatient", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fname", FName);
                cmd.Parameters.AddWithValue("@lname", LName);
                cmd.Parameters.AddWithValue("@gender", Gen);
                cmd.Parameters.AddWithValue("@age", Age);
                cmd.Parameters.AddWithValue("@dob", DOB);
                int PatientID = Convert.ToInt32(cmd.ExecuteScalar());
                CreatedPatientDetails = GetPatientDetails(PatientID);
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return CreatedPatientDetails;
        }

        //This method return the Patient Details basing on Patient ID given
        public ArrayList GetPatientDetails(int Patientid)
        {
            ArrayList PatientDetails = new();
            conn = InitiateDB();
            cmd = new SqlCommand($"SELECT * FROM Patients WHERE PatientID = {Patientid}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            PatientDetails.Add(Convert.ToInt32(reader[0]));
            PatientDetails.Add(reader[1].ToString());
            PatientDetails.Add(reader[2].ToString());
            PatientDetails.Add(reader[3].ToString());
            PatientDetails.Add(Convert.ToInt32(reader[4]));
            PatientDetails.Add(DateTime.Parse(reader[5].ToString()));
            conn.Close();
            return PatientDetails;
        }

        //This initiates the connection with database and returns SqlConnection object
        private static SqlConnection InitiateDB()
        {
            try
            {
                conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = ClinicDB; Integrated Security = true;");
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Database Intiation Failed " + e);
            }
            return conn;
        }
    }
}
