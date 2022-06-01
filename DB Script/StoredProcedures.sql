-- USE ClinicDB

CREATE PROCEDURE AddDoctor(@fname varchar(30), @lname varchar(30), @gender varchar(10), @spec varchar(30), @vfrom TIME, @vto TIME)
AS
BEGIN
	INSERT INTO Doctors 
	OUTPUT INSERTED.DoctorID
	VALUES (@fname, @lname, @gender, @spec, @vfrom, @vto)
END

CREATE PROCEDURE AddPatient(@fname varchar(30), @lname varchar(30), @gender varchar(10), @age int, @dob DATE)
AS
BEGIN
	INSERT INTO Patients 
	OUTPUT INSERTED.PatientID
	VALUES (@fname, @lname, @gender, @age, @dob)
END

CREATE PROCEDURE AddAppointment(@pid INT, @spec VARCHAR(30), @did INT, @vdate DATE, @atime TIME)
AS
BEGIN
	INSERT INTO Appointments 
	OUTPUT INSERTED.AppointmentID
	VALUES (@pid, @spec, @did, @vdate, @atime)
END

CREATE PROCEDURE CancelAppointment(@appID INT)
AS
BEGIN
	DELETE FROM Appointments WHERE AppointmentID = @appID
END

-- exec AddDoctor 'satyagopal', 'kothuru', 'Male', 'General', '10:00', '13:00'

-- select * from Doctors
-- select * from Patients
-- select * from Appointments

-- delete from Patients
exec AddAppointment 1007, 'General', 3, '02/06/2022', '10:00'

