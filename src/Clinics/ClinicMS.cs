using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Data.SqlClient;
using CMSDAL;
using ClinicMSBAL;

namespace Clinics
{
    public class ClinicMS
    {
        ClinicMS()
        {

        }
        public static void Main()
        {
            Console.Title = "Clinic - Login Page";
            string UserName, Password;
            Console.WriteLine("Hello! Welcome to Clinic.");
            Thread.Sleep(200);
            Console.WriteLine("Please Login Here...");
            Thread.Sleep(200);
            while(true)
            {
                Console.WriteLine("Enter Your UserName");
                UserName = Console.ReadLine();
                Console.WriteLine("Enter Your Password");
                Password = Console.ReadLine();
                if(Authentication.ValidateCredentials(UserName, Password))
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.Title = "Clinic - Home Page";
                    Console.WriteLine("Hello! Welcome to Clinic.");
                    Console.WriteLine("Please Login Here...");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You’ve entered an incorrect user name or password.\nTry Again...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(200);
                }
            }
            Console.Clear();
            Console.Title = "Clinic Home Page";
            Console.WriteLine($"Hello {UserName}!\nWelcome to Clinic.");
            Thread.Sleep(100);
            int choice;
            do
            {
                Console.WriteLine("You can select from the options below.");
                Thread.Sleep(100);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  ______________________________________  \n");
                Console.WriteLine("|\t1.  View Doctors                 |");
                Thread.Sleep(100); 
                Console.WriteLine("|\t2.  Add Doctor                   |");
                Thread.Sleep(100);
                Console.WriteLine("|\t3.  Add Patient                  |");
                Thread.Sleep(100);
                Console.WriteLine("|\t4.  Schedule Appointment         |");
                Thread.Sleep(100);
                Console.WriteLine("|\t5.  Cancel Appointment           |");
                Thread.Sleep(100);
                Console.WriteLine("|\t6.  LogOut                       |");
                Console.WriteLine("  ______________________________________  \n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("---Your Choice---");
                choice = Convert.ToInt32(Console.ReadLine());
                string[] gender = new string[] { "Male", "Female", "Others" };
                string[] specializations = new string[] { "General", "Internal Medicine", "Pediatrics", "Orthopedics", "Ophthalmology" };
                int specSelected, genderselected;
                Doctor d;
                Patient p;
                Appointment a;
                try
                {
                    switch (choice)
                    {
                        case 1:
                            //View Doctors
                            for (int countdown = 1; countdown < 6; countdown++)
                            {
                                Console.Write("Scanning");
                                for (int i = 0; i < countdown % 6 + 1; i++)
                                    Console.Write(".");
                                Console.Write("\r");
                                Thread.Sleep(200);
                            }
                            Console.WriteLine("All Doctors Details Present in the Clinic.");
                            d = new Doctor();
                            foreach(Doctor doctor in d.GetAllDoctorDetails())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("***********************************");
                                Console.WriteLine("Doctor ID:                 "+ doctor.DoctorID.ToString());
                                Console.WriteLine("First Name:                "+ doctor.FirstName);
                                Console.WriteLine("Last Name:                 "+ doctor.LastName);
                                Console.WriteLine("Gender:                    "+ doctor.Gender);
                                Console.WriteLine("Specialization:            "+ doctor.Specialization);
                                Console.WriteLine("Available from {0} to {1} ", doctor.VisitStartTime, doctor.VisitEndTime);
                                Console.WriteLine("***********************************\n\n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;

                        case 2:
                            //Add Doctor
                            Console.WriteLine("Enter Doctor First Name:");
                            string firstName = Console.ReadLine();
                            Console.WriteLine("Enter Doctor Last Name:");
                            string lastName = Console.ReadLine();
                            Console.WriteLine("\n1. Male\n2. Female\n3. Others\nSelect Doctor Gender:\n");
                            genderselected = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("\n1. General\n2. Internal Medicine\n3. Pediatrics\n4. Orthopedics\n5. Ophthalmology\nSelect Doctor Gender:\n");
                            specSelected = Convert.ToInt32(Console.ReadLine());
                            
                            Console.WriteLine("Enter When Doctor is Available at Clinic:");
                            TimeSpan vstart = TimeSpan.Parse(Console.ReadLine());
                            Console.WriteLine("Enter When Doctor Leaves Clinic:");
                            TimeSpan vend = TimeSpan.Parse(Console.ReadLine());
                            d = new Doctor();
                            d = d.AddDoctorInDatabase(firstName, lastName, gender[genderselected-1], specializations[specSelected-1], vstart, vend);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Doctor details successfully added to the system.\nDoctor Details:");
                            Console.WriteLine("***********************************");
                            Console.WriteLine("Doctor ID:                 " + d.DoctorID);
                            Console.WriteLine("First Name:                " + d.FirstName);
                            Console.WriteLine("Last Name:                 " + d.LastName);
                            Console.WriteLine("Gender:                    " + d.Gender);
                            Console.WriteLine("Specialization:            " + d.Specialization);
                            Console.WriteLine("Available from {0} to {1} ", d.VisitStartTime, d.VisitEndTime);
                            Console.WriteLine("***********************************\n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case 3:
                            //Add Patient
                            Console.WriteLine("Enter Patient First Name:");
                            string pfirstName = Console.ReadLine();
                            Console.WriteLine("Enter Patient Last Name:");
                            string plastName = Console.ReadLine();
                            Console.WriteLine("\n1. Male\n2. Female\n3. Others\nSelect Patient Gender:");
                            int pgenderselected = Convert.ToInt32(Console.ReadLine());
                            
                            Console.WriteLine("Enter patient age:");
                            int page = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter patient Date Of Birth:");
                            DateTime pdob = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                            p = new Patient();
                            p = p.AddPatientInDatabase(pfirstName,
                                                             plastName,
                                                             gender[pgenderselected - 1],
                                                             page,
                                                             pdob);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Patient successfully added to the system.\nPatient Details:");
                            Console.WriteLine("***********************************");
                            Console.WriteLine("Patient ID:    " + p.PatientID);
                            Console.WriteLine("First Name:    " + p.FirstName);
                            Console.WriteLine("Last Name:     " + p.LastName);
                            Console.WriteLine("Gender:        " + p.Gender);
                            Console.WriteLine("Age:           " + p.Age);
                            Console.WriteLine("Date Of Birth: {0}/{1}/{2}", p.DateOfBirth.Day, p.DateOfBirth.Month, p.DateOfBirth.Year);
                            Console.WriteLine("***********************************\n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case 4:
                            //Schedule Appointment
                            Console.WriteLine("Enter Patient ID:");
                            int pid = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("\n1. General\n2. Internal Medicine\n3. Pediatrics\n4. Orthopedics\n5. Ophthalmology\nSelect Doctor Gender:\n");
                            specSelected = Convert.ToInt32(Console.ReadLine());
                            d = new Doctor();
                            foreach (Doctor doctor in d.GetAllDoctorDetailsBySpecialization(specializations[specSelected - 1]))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("***********************************");
                                Console.WriteLine("Doctor ID:                 " + doctor.DoctorID.ToString());
                                Console.WriteLine("First Name:                " + doctor.FirstName);
                                Console.WriteLine("Last Name:                 " + doctor.LastName);
                                Console.WriteLine("Gender:                    " + doctor.Gender);
                                Console.WriteLine("Specialization:            " + doctor.Specialization);
                                Console.WriteLine("Available from {0} to {1} ", doctor.VisitStartTime, doctor.VisitEndTime);
                                Console.WriteLine("***********************************\n\n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine("Enter Doctor ID from Above:");
                            int did = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Visiting Date:");
                            DateTime dov = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                            d = new Doctor();
                            List<string> timeslots = d.GetDoctorAvailableSlots(did, dov);
                            if(timeslots.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"No Slots Available to Book Appointment on {dov} for Doctor ID {did}");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }
                            for(int index = 0; index<timeslots.Count; index++)
                            {
                                Console.Write($"|{index+1} - {timeslots[index]} |");
                            }
                            Console.WriteLine("\nChoose a timeslot from Above:");
                            int tslot = Convert.ToInt32(Console.ReadLine());
                            a = new Appointment();
                            a = a.AddAppointmentInDatabase(pid, specializations[specSelected-1], did, dov, TimeSpan.Parse(timeslots[tslot-1]));
                            for (int countdown = 1; countdown < 6; countdown++)
                            {
                                Console.Write("Processing Your Appointment");
                                for (int i = 0; i < countdown % 6 + 1; i++)
                                    Console.Write(".");
                                Console.Write("\r");
                                Thread.Sleep(200);
                            }
                            Console.WriteLine("Congratulation, Your Appointment is booked.\n Your Appointment Details:");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("***********************************");
                            Console.WriteLine("Appointment ID:         " + a.AppointmentID.ToString());
                            Console.WriteLine("Patient ID:             " + a.PatientID);
                            Console.WriteLine("Doctor ID:              " + a.DoctorID);
                            Console.WriteLine("Doctot Specialization:  " + a.Specialization);
                            Console.WriteLine("Appointment Date:       " + a.VisitDate.ToString("dd/MM/yyyy"));
                            Console.WriteLine("Appointment Time:       " + a.AppointmentTime.ToString());
                            Console.WriteLine("***********************************\n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case 5:
                            Console.WriteLine("Enter Patient ID:");
                            int patientid = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Visiting Date:");
                            DateTime visitingdate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                            a = new Appointment();
                            List<Appointment> allApps = a.GetAppointmentsByPatientAndDate(patientid, visitingdate);
                            foreach (Appointment apps in allApps)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("***********************************");
                                Console.WriteLine("Appointment ID:         " + apps.AppointmentID.ToString());
                                Console.WriteLine("Patient ID:             " + apps.PatientID);
                                Console.WriteLine("Doctor ID:              " + apps.DoctorID);
                                Console.WriteLine("Doctot Specialization:  " + apps.Specialization);
                                Console.WriteLine("Appointment Date:       " + apps.VisitDate.ToString("dd/MM/yyyy"));
                                Console.WriteLine("Appointment Time:       " + apps.AppointmentTime.ToString());
                                Console.WriteLine("***********************************\n\n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            if(allApps.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"\nNo Appointments on {visitingdate.ToString("dd/MM/yyyy")} to Cancel.\n");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }
                            Console.WriteLine("Enter Appointment ID from Above:");
                            int appid = Convert.ToInt32(Console.ReadLine());
                            if(a.CancelAppointment(appid))
                            {
                                for (int countdown1 = 1; countdown1 <= 10; countdown1++)
                                {
                                    Console.Write($"{countdown1 * 10}% |");
                                    for (int i = 0; i < countdown1; i++)
                                        Console.Write("#");
                                    for (int i = 0; i < 10 - countdown1; i++)
                                        Console.Write(" ");
                                    Console.Write("| 100% \r");
                                    Thread.Sleep(100);
                                }
                                Console.WriteLine("\rAppointment Cancelled   ");
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Sorry, Unable to Cancel Appointment");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }
                            

                        case 6:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Thank you...:)");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Wrong Choice\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (choice != 6);

        }
    }
}



