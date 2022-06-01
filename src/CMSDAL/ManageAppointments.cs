using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSDAL
{
    public class ManageAppointments
    {
        public static SqlConnection conn = null;
        public static SqlCommand cmd;

        public List<ArrayList> AppointmentList()
        {
            List<ArrayList> Appointments = new();
            conn = InitiateDB();
            cmd = new SqlCommand("SELECT * FROM Appointments", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ArrayList arr = new();
                arr.Add(Convert.ToInt32(reader[0]));
                arr.Add(Convert.ToInt32(reader[1]));
                arr.Add(reader[2].ToString());
                arr.Add(Convert.ToInt32(reader[3]));
                arr.Add(DateTime.Parse(reader[4].ToString()));
                arr.Add(TimeSpan.Parse(reader[5].ToString()));
                Appointments.Add(arr);
            }
            conn.Close();
            return Appointments;
        }

        public List<ArrayList> AppointmentListByPatientIDAndVisitingDate(int pid, DateTime dov)
        {
            List<ArrayList> Appointments = new();
            conn = InitiateDB();
            cmd = new SqlCommand($"SELECT * FROM Appointments WHERE PID = {pid} AND VisitDate = '{dov}'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ArrayList arr = new();
                arr.Add(Convert.ToInt32(reader[0]));
                arr.Add(Convert.ToInt32(reader[1]));
                arr.Add(reader[2].ToString());
                arr.Add(Convert.ToInt32(reader[3]));
                arr.Add(DateTime.Parse(reader[4].ToString()));
                arr.Add(TimeSpan.Parse(reader[5].ToString()));
                Appointments.Add(arr);
            }
            conn.Close();
            return Appointments;
        }

        public ArrayList AddNewAppointment(int Pid, string Spec, int Did, DateTime VDate, TimeSpan AppTime)
        {
            ArrayList CreatedAppointmentDetails = new();
            try
            {
                conn = InitiateDB();
                cmd = new SqlCommand("AddAppointment", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pid", Pid);
                cmd.Parameters.AddWithValue("@spec", Spec);
                cmd.Parameters.AddWithValue("@did", Did);
                cmd.Parameters.AddWithValue("@vdate", VDate);
                cmd.Parameters.AddWithValue("@atime", AppTime);
                int AppointmentID = Convert.ToInt32(cmd.ExecuteScalar());
                CreatedAppointmentDetails = GetAppointmentDetails(AppointmentID);
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return CreatedAppointmentDetails;
        }

        public bool CancelAppointment(int AppID)
        {
            conn = InitiateDB();
            cmd = new SqlCommand("CancelAppointment", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@appID", AppID);
            int RowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            if (RowsAffected == 0)
                return false;
            else
                return true;
        }

        public ArrayList GetAppointmentDetails(int Appointmentid)
        {
            ArrayList AppointmentDetails = new();
            conn = InitiateDB();
            cmd = new SqlCommand($"SELECT * FROM Appointments WHERE AppointmentID = {Appointmentid}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            AppointmentDetails.Add(Convert.ToInt32(reader[0]));
            AppointmentDetails.Add(Convert.ToInt32(reader[1]));
            AppointmentDetails.Add(reader[2].ToString());
            AppointmentDetails.Add(Convert.ToInt32(reader[3]));
            AppointmentDetails.Add(DateTime.Parse(reader[4].ToString()).Date);
            AppointmentDetails.Add(TimeSpan.Parse(reader[5].ToString()).Hours);
            conn.Close();
            return AppointmentDetails;
        }

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
