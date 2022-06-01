using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSDAL;

namespace ClinicMSBAL
{
    public class Patient
    {
        public int PatientID;
        public string FirstName;
        public string LastName;
        public string Gender;
        public int Age;
        public DateTime DateOfBirth;

        public Patient()
        {

        }

        public Patient(int pid, string fname, string lname, string gen, int age, DateTime dob)
        {
            PatientID = pid;
            FirstName = fname;
            LastName = lname;
            Gender = gen;
            Age = age;
            DateOfBirth = dob;
        }

        public Patient AddPatientInDatabase(string fname, string lname, string gen, int age, DateTime dob)
        {
            ManagePatient managePatient = new ManagePatient();
            ArrayList array = managePatient.AddNewPatient(fname, lname, gen, age, dob);
            return new Patient(Convert.ToInt32(array[0]), 
                               array[1].ToString(), 
                               array[2].ToString(), 
                               array[3].ToString(), 
                               Convert.ToInt32(array[4]), 
                               DateTime.Parse(dob.ToString()));
        }
    }
}
