using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Hospital
{
  public class PatientTest : IDisposable
  {
    public PatientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hospital_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange,Act
      int result = Patient.GetAll().Count;

      //Assert
      Assert.Equal (0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTRueIfSame()
    {
      //Arrange,Act
      DateTime patientDate = new DateTime(2015,9,8);
      Patient firstPatient = new Patient("James", 1, patientDate);
      Patient secondPatient = new Patient("James", 1, patientDate);

      //Assert
      Assert.Equal(firstPatient,secondPatient);
    }

    public void Dispose()
    {
      Patient.ClearAll();
    }
  }
}
