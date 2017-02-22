using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Hospital
{
  public class DoctorTest : IDisposable
  {
    public DoctorTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hospital_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void OverrideEquals_TwoSameDoctors_Equal()
    {
      //Arrange,Act
      Doctor firstDoctor = new Doctor("Doctor Mike",1);
      Doctor secondDoctor = new Doctor("Doctor Mike",1);

      //Assert
      Assert.Equal(firstDoctor,secondDoctor);
    }

    public void Dispose()
    {
      Patient.DeleteAll();
      Doctor.ClearAll();
    }
  }
}
