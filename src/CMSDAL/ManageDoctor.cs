using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CMSDAL
{
    public class ManageDoctor
    {
        public static SqlConnection conn = null;
        public static SqlCommand cmd;

        //This Method returns the All Doctor details present in Database
        public List<ArrayList> DoctorList()
        {
            List<ArrayList> doctors = new();
            conn = InitiateDB();
            cmd = new SqlCommand("SELECT * FROM Doctors", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                ArrayList arr = new();
                arr.Add(Convert.ToInt32(reader[0]));
                arr.Add(reader[1].ToString());
                arr.Add(reader[2].ToString());
                arr.Add(reader[3].ToString());
                arr.Add(reader[4].ToString());
                arr.Add(TimeSpan.Parse(reader[5].ToString()));
                arr.Add(TimeSpan.Parse(reader[6].ToString()));
                doctors.Add(arr);
            }
            conn.Close();
            return doctors;
        }

        //This Method returns the All Doctor details present in Database basing on specialization given
        public List<ArrayList> DoctorListBySpecialization(string spec)
        {
            List<ArrayList> doctors = new();
            conn = InitiateDB();
            cmd = new SqlCommand($"SELECT * FROM Doctors WHERE Specialization = '{spec}'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ArrayList arr = new();
                arr.Add(Convert.ToInt32(reader[0]));
                arr.Add(reader[1].ToString());
                arr.Add(reader[2].ToString());
                arr.Add(reader[3].ToString());
                arr.Add(reader[4].ToString());
                arr.Add(TimeSpan.Parse(reader[5].ToString()));
                arr.Add(TimeSpan.Parse(reader[6].ToString()));
                doctors.Add(arr);
            }
            conn.Close();
            return doctors;
        }

        //This Method returns the Timeslots available for Doctor
        public List<string> GetDoctorsAvailableTimeSlots(int did, DateTime VisitDate)
        {
            List<string> timeSlots = new();
            ArrayList doctor = GetDoctorDetails(did);
            for(int start =  Convert.ToInt32(TimeSpan.Parse(doctor[5].ToString()).Hours); start< Convert.ToInt32(TimeSpan.Parse(doctor[6].ToString()).Hours); start++)
            {
                timeSlots.Add($"{start}:00");
            }
            conn = InitiateDB();
            cmd = new SqlCommand($"SELECT * FROM Appointments WHERE DID = {did} AND VisitDate = '{VisitDate}'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                timeSlots.Remove($"{TimeSpan.Parse(reader[5].ToString()).Hours}:00");
            }
            return timeSlots;
        } 

        //This method adds the new Doctor details into database
        public ArrayList AddNewDoctor(string FName, string LName, string Gen, string Spec, TimeSpan VStart, TimeSpan VEnd)
        {
            ArrayList CreatedDoctorDetails = new();
            try
            {
                conn = InitiateDB();
                cmd = new SqlCommand("AddDoctor", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fname", FName);
                cmd.Parameters.AddWithValue("@lname", LName);
                cmd.Parameters.AddWithValue("@gender", Gen);
                cmd.Parameters.AddWithValue("@spec", Spec);
                cmd.Parameters.AddWithValue("@vfrom", VStart);
                cmd.Parameters.AddWithValue("@vto", VEnd);
                int DoctorID = Convert.ToInt32(cmd.ExecuteScalar());
                CreatedDoctorDetails = GetDoctorDetails(DoctorID);
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return CreatedDoctorDetails;
        }

        //This method returns the Doctor Details basing on Doctor ID given
        public ArrayList GetDoctorDetails(int Doctorid)
        {
            ArrayList DoctorDetails = new();
            conn = InitiateDB();
            cmd = new SqlCommand($"SELECT * FROM Doctors WHERE DoctorID = {Doctorid}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            DoctorDetails.Add(Convert.ToInt32(reader[0]));
            DoctorDetails.Add(reader[1].ToString());
            DoctorDetails.Add(reader[2].ToString());
            DoctorDetails.Add(reader[3].ToString());
            DoctorDetails.Add(reader[4].ToString());
            DoctorDetails.Add(TimeSpan.Parse(reader[5].ToString()));
            DoctorDetails.Add(TimeSpan.Parse(reader[6].ToString()));
            conn.Close();
            return DoctorDetails;
        }

        //This initiates the connection with database and returns SqlConnection object
        private static SqlConnection InitiateDB()
        {
            try
            {
                conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = ClinicDB; Integrated Security = true;");
                conn.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("Database Intiation Failed " + e);
            }
            return conn;
        }
    }
}
