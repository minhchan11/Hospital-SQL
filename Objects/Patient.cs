using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hospital
{
  public class Patient
  {
    private int _id;
    private string _patientName;
    private int _doctorId;
    private DateTime _date;

    //Make object constructor
    public Patient(string patientName, int doctorId, DateTime date, int id = 0)
    {
      _id = id;
      _patientName = patientName;
      _doctorId = doctorId;
      _date = date;
    }

    //Getters and Setters
    public int GetId()
    {
      return _id;
    }

    public int GetDoctorId()
    {
      return _doctorId;
    }

    public string GetPatientName()
    {
      return _patientName;
    }

    public void SetPatientName(string myPatientName)
    {
      _patientName = myPatientName;
    }

    public DateTime GetDate()
    {
      return _date;
    }

    //Create override boolean
    public override bool Equals(System.Object randomPatient)
    {
      if (!(randomPatient is Patient))
      {
        return false;
      }
      else
      {
        Patient newPatient = (Patient) randomPatient;
        //Call booleans on newPatient instead of randomPatient to make sure its type is Patient
        bool idEquality = (this.GetId() == newPatient.GetId());
        bool nameEquality = (this.GetPatientName() == newPatient.GetPatientName());
        bool doctorIdEquality = (this.GetDoctorId() == newPatient.GetDoctorId());
        bool dateEquality = (this.GetDate() == newPatient.GetDate());
        return (idEquality && nameEquality && doctorIdEquality && dateEquality);
      }
    }

    //Create STATIC GetAll method to add instance to database
    public static List<Patient> GetAll()
    {
      //Make new empty list of instances
      List<Patient> instances = new List<Patient>{};
      //Establish connection
      SqlConnection conn = DB.Connection();
      conn.Open();
      //Type commands in Powershell
      SqlCommand cmd = new SqlCommand("SELECT * FROM patient ORDER BY cast([date] as datetime) asc;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      //Get data from Powershell
      while(rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        int doctorId = rdr.GetInt32(2);
        DateTime patientDate = rdr.GetDateTime(3);

        //make new instance
        Patient newPatient = new Patient(patientName, doctorId, patientDate, patientId);
        instances.Add(newPatient);
      }

        //Close Connection
        if (rdr != null)
        {
          rdr.Close();
        }

        if (conn != null)
        {
          conn.Close();
        }

        //Output list
        return instances;
    }

    //Create method called on each individual, new instance to add to the static list
    public void Save()
    {
      //Establish connection
      SqlConnection conn = DB.Connection();
      conn.Open();
      //Type commands in Powershell
      SqlCommand cmd = new SqlCommand("INSERT INTO patient (name, doctor_id, date) OUTPUT INSERTED.id VALUES (@PatientName, @PatientDoctorId, @PatientDate);", conn);
      //Define each parameters
      SqlParameter patientNameParameter = new SqlParameter();
      patientNameParameter.ParameterName = "@PatientName";
      patientNameParameter.Value = this.GetPatientName();

      SqlParameter DoctorIdParameter = new SqlParameter();
      DoctorIdParameter.ParameterName = "@PatientDoctorId";
      DoctorIdParameter.Value = this.GetDoctorId();

      SqlParameter PatientDateParameter = new SqlParameter();
      PatientDateParameter.ParameterName = "@PatientDate";
      PatientDateParameter.Value = this.GetDate();

      //Add the parameters onto Powershell
      cmd.Parameters.Add(patientNameParameter);
      cmd.Parameters.Add(DoctorIdParameter);
      cmd.Parameters.Add(PatientDateParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      //Make sure the id is not instance id but its id in the table patients
      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }
    }

    //Static method for disposing and also for clearing the database
    public static void ClearAll()
    {
      //Establish connection
      SqlConnection conn = DB.Connection();
      conn.Open();
      //Type commands in Powershell
      SqlCommand cmd = new SqlCommand("DELETE FROM patient;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
