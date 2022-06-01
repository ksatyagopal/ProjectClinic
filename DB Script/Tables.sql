-- Creating a Clinic Database

-- CREATE DATABASE ClinicDB

-- use ClinicDB

-- Creating Tables

CREATE TABLE Doctors(
DoctorID INT not null PRIMARY KEY IDENTITY(1,1),
FirstName VARCHAR(30) not null,
LastName VARCHAR(30) not null,
Gender VARCHAR(10) not null CHECK (Gender in ('Male','Female','Others')),
Specialization VARCHAR(30) not null CHECK(Specialization in ('General', 'Internal Medicine', 'Pediatrics', 'Orthopedics', 'Ophthalmology')),
VisitFrom TIME not null,
VisitTo TIME not null,
)

CREATE TABLE Patients(
PatientID INT not null PRIMARY KEY IDENTITY(1000,1),
FirstName VARCHAR(30) not null,
LastName VARCHAR(30) not null,
Gender VARCHAR(10) not null CHECK (Gender in ('Male','Female','Others')),
Age INT CHECK(Age>0 AND Age<=120) not null,
DOB DATE not null
)

CREATE TABLE Appointments(
AppointmentID INT not null PRIMARY KEY IDENTITY(10000,1),
PID INT not null FOREIGN KEY REFERENCES Patients(PatientID),
SpecRequired VARCHAR(30) not null,
DID INT not null FOREIGN KEY REFERENCES Doctors(DoctorID),
VisitDate DATE not null,
AppointmnetTime TIME not null
)

CREATE TABLE OfficeStaff(
UserName VARCHAR(10) UNIQUE,
FirstName VARCHAR(30),
LastName VARCHAR(30),
Password VARCHAR(10)
)

delete from OfficeStaff

INSERT INTO OfficeStaff VALUES ('satya', 'Satyagopal', 'Kothuru', 'satya@123'),
								('anudeep', 'Anudeep', 'Addagada', 'anu@123'),
								('krishna', 'Krishna', 'Chimmili', 'krish@123')

