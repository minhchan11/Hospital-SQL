using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hospital
{
  public class Doctor
  {
    private int _id;
    private string _doctorName;
    private int _specialtyId;

    //New doctor constructor
    public Doctor(string doctorName, int specialtyId, int id = 0)
    {
      _id = id;
      _doctorName = doctorName;
      _specialtyId = specialtyId;
    }

    //Getters and Setters
    public int GetId()
    {
      return _id;
    }

    public int GetSpecialtyId()
    {
      return _specialtyId;
    }

    public string GetDoctorName()
    {
      return _doctorName;
    }

    public void SetDoctorName(string myDoctorName)
    {
      _doctorName = myDoctorName;
    }

    //Create override boolean
    public override bool Equals(System.Object randomDoctor)
    {
      if (!(randomDoctor is Doctor))
      {
        return false;
      }
      else
      {
        Doctor newDoctor = (Doctor) randomDoctor;
        //Call booleans on newDoctor instead of randomDoctor to make sure its type is Doctor
        bool idEquality = (this.GetId() == newDoctor.GetId());
        bool nameEquality = (this.GetDoctorName() == newDoctor.GetDoctorName());
        bool specialtyIdEquality = (this.GetSpecialtyId() == newDoctor.GetSpecialtyId());
        return (idEquality && nameEquality && specialtyIdEquality);
      }
    }

    //Static method for disposing and also for clearing the database
    public static void ClearAll()
    {
      //Establish connection
      SqlConnection conn = DB.Connection();
      conn.Open();
      //Type commands in Powershell
      SqlCommand cmd = new SqlCommand("DELETE FROM doctor;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
