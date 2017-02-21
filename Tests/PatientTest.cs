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
    public void OverrideEquals_TwoSamePatients_Equal()
    {
      //Arrange,Act
      DateTime patientDate = new DateTime(2015,9,8);
      Patient firstPatient = new Patient("James", 1, patientDate);
      Patient secondPatient = new Patient("James", 1, patientDate);

      //Assert
      Assert.Equal(firstPatient,secondPatient);
    }

    [Fact]
    public void GetAllZero_DatabaseEmptyAtFirst_Zero()
    {
      //Arrange,Act
      int result = Patient.GetAll().Count;

      //Assert
      Assert.Equal (0, result);
    }

    [Fact]
    public void Save_OnePatientToDatabase_IdIsAssigned()
    {
      //Arrange
      DateTime patientDate = new DateTime(2015,9,8);
      Patient testPatient = new Patient("James", 1, patientDate);
      testPatient.Save();
      Patient savedPatient = Patient.GetAll()[0];

      //Act
      int outputId = savedPatient.GetId();
      int verifyId = testPatient.GetId();

      //Assert
      Assert.Equal(outputId,verifyId);
    }

    [Fact]
    public void GetAllAndSave_OnePatientToDatabase_PatientRecorded()
    {
      //Arrange
      DateTime patientDate = new DateTime(2015,9,8);
      Patient testPatient = new Patient("James", 1, patientDate);
      testPatient.Save();

      //Act
      List<Patient> output = Patient.GetAll();
      List<Patient> verify = new List<Patient>{testPatient};

      //Assert
      Assert.Equal(output,verify);
    }

    public void Dispose()
    {
      Patient.ClearAll();
    }
  }
}
