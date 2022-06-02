using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSDAL;


namespace ClinicMSBAL
{
    public class Appointment
    {
        public int AppointmentID;
        public int PatientID;
        public string Specialization;
        public int DoctorID;
        public DateTime VisitDate;
        public TimeSpan AppointmentTime;
        public static List<Appointment> AllAppointments = new();
        public Appointment()
        {

        }

        public Appointment(int appid, int pid, string spec, int did, DateTime vdate, TimeSpan apptime)
        {
            AppointmentID = appid;
            PatientID = pid;
            Specialization = spec;
            DoctorID = did;
            VisitDate = vdate;
            AppointmentTime = apptime;
        }

        public Appointment AddAppointmentInDatabase(int pid, string spec, int did, DateTime vdate, TimeSpan apptime)
        {
            ManageAppointments manageAppointment = new ManageAppointments();
            ArrayList array = manageAppointment.AddNewAppointment(pid, spec, did, vdate, apptime);
            return new Appointment(Convert.ToInt32(array[0]), 
                                   Convert.ToInt32(array[1]), 
                                   array[2].ToString(), 
                                   Convert.ToInt32(array[3]), 
                                   DateTime.Parse(array[4].ToString()), 
                                   TimeSpan.Parse(array[5].ToString()));
        }

        public bool CancelAppointment(int AppID)
        {
            ManageAppointments manageAppointment = new();
            return manageAppointment.CancelAppointment(AppID);
        }

        public List<Appointment> GetAppointmentsByPatientAndDate(int pid, DateTime dov)
        {
            ManageAppointments manageAppointments = new();
            AllAppointments = new List<Appointment>();
            foreach (ArrayList array in manageAppointments.AppointmentListByPatientIDAndVisitingDate(pid, dov))
            {
                AllAppointments.Add(new Appointment(Convert.ToInt32(array[0]),
                                          Convert.ToInt32(array[1]),
                                          array[2].ToString(),
                                          Convert.ToInt32(array[3]),
                                          DateTime.Parse(array[4].ToString()),
                                          TimeSpan.Parse(array[5].ToString())));
            }
            return AllAppointments;
        }

        public List<Appointment> GetAppointmentsByDoctorAndDate(int did, DateTime dov)
        {
            ManageAppointments manageAppointments = new();
            AllAppointments = new List<Appointment>();
            foreach (ArrayList array in manageAppointments.AppointmentListByDoctorIDAndVisitingDate(did, dov))
            {
                AllAppointments.Add(new Appointment(Convert.ToInt32(array[0]),
                                          Convert.ToInt32(array[1]),
                                          array[2].ToString(),
                                          Convert.ToInt32(array[3]),
                                          DateTime.Parse(array[4].ToString()),
                                          TimeSpan.Parse(array[5].ToString())));
            }
            return AllAppointments;
        }
    }
}
