using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using CMSDAL;

namespace ClinicMSBAL
{
    public class Doctor
    {
        public int DoctorID;
        public string FirstName;
        public string LastName;
        public string Gender;
        public string Specialization;
        public TimeSpan VisitStartTime;
        public TimeSpan VisitEndTime;

        public static List<Doctor> AllDoctors = new();

        public Doctor()
        {

        }

        public Doctor(int did, string fname, string lname, string gen, string spec, TimeSpan vstart, TimeSpan vend)
        {
            DoctorID = did;
            FirstName = fname;
            LastName = lname;
            Gender = gen;
            Specialization = spec;
            VisitStartTime = vstart;
            VisitEndTime = vend;
        }

        public Doctor AddDoctorInDatabase(string fname, string lname, string gen, string spec, TimeSpan vstart, TimeSpan vend)
        {
            ManageDoctor manageDoctor = new ManageDoctor();
            ArrayList array = manageDoctor.AddNewDoctor(fname, lname, gen, spec, vstart, vend);
            return new Doctor(Convert.ToInt32(array[0]),
                               array[1].ToString(), 
                               array[2].ToString(), 
                               array[3].ToString(), 
                               array[4].ToString(), 
                               TimeSpan.Parse(array[5].ToString()), 
                               TimeSpan.Parse(array[6].ToString()));
        }

        public List<Doctor> GetAllDoctorDetails()
        {
            AllDoctors = new List<Doctor>();
            ManageDoctor manageDoctor = new();
            foreach(ArrayList array in manageDoctor.DoctorList())
            {
                AllDoctors.Add(new Doctor(Convert.ToInt32(array[0]), 
                                          array[1].ToString(), 
                                          array[2].ToString(), 
                                          array[3].ToString(), 
                                          array[4].ToString(), 
                                          TimeSpan.Parse(array[5].ToString()), 
                                          TimeSpan.Parse(array[6].ToString())));
            }
            return AllDoctors;
        }

        public List<Doctor> GetAllDoctorDetailsBySpecialization(string spec)
        {
            AllDoctors = new List<Doctor>();
            ManageDoctor manageDoctor = new();
            foreach (ArrayList array in manageDoctor.DoctorListBySpecialization(spec))
            {
                AllDoctors.Add(new Doctor(Convert.ToInt32(array[0]), 
                                          array[1].ToString(), 
                                          array[2].ToString(), 
                                          array[3].ToString(), 
                                          array[4].ToString(), 
                                          TimeSpan.Parse(array[5].ToString()), 
                                          TimeSpan.Parse(array[6].ToString())));
            }
            return AllDoctors;
        }

        public List<string> GetDoctorAvailableSlots(int did, DateTime VisitDate)
        {
            ManageDoctor manageDoctor = new();
            return manageDoctor.GetDoctorsAvailableTimeSlots(did, VisitDate);
        }

    }
}
