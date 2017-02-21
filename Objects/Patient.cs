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
