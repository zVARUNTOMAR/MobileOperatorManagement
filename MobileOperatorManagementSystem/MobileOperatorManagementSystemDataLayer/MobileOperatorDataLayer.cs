using CustomExceptions;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MobileOperatorManagementSystemDataLayer
{
    //change
    public class MobileOperatorDataLayer
    {
        private static string _connectionString = "Data Source=VARUN;Initial Catalog = CellPhoneDatabase; Integrated Security = True";

        //Adding New Mobile Operator to Database
        public bool AddMobileOperator(MobileOperator mobileOperator)
        {
            bool isAdded = false;
            try
            {
                SqlConnection sqlConn = new SqlConnection(_connectionString);
                sqlConn.Open();

                string query = @"insert into mobile_operator(name,rating) values(" + "\'" + mobileOperator.Name + "\'" + "," + mobileOperator.Rating + ");";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConn))
                {
                    int rowAffected = sqlCommand.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        isAdded = true;
                        return isAdded;
                    }
                    else
                    {
                        return isAdded;
                    }
                }
            }
            catch (Exception e)
            {
                throw new SqlCustomException("Some error Occurred");
            }
        }

        //To Get all Details of Mobile Operators from database
        public List<MobileOperator> GetMobileOperatorDeatils()
        {
            DataTable dtblMobileOperators = new DataTable();
            List<MobileOperator> mobileOperators = new List<MobileOperator>();

            try
            {
                SqlConnection sqlConn = new SqlConnection(_connectionString);
                sqlConn.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from mobile_operator;", sqlConn);
                sqlDataAdapter.Fill(dtblMobileOperators);
                sqlConn.Close();

                foreach (DataRow dataRow in dtblMobileOperators.Rows)
                {
                    MobileOperator mobileOperator = new MobileOperator();

                    mobileOperator.Id = (int)dataRow["id"];
                    mobileOperator.Name = (string)dataRow["name"];
                    mobileOperator.Rating = (int)dataRow["rating"];

                    mobileOperators.Add(mobileOperator);
                }

                return mobileOperators;
            }
            catch (Exception e)
            {
                throw new SqlCustomException("Some error Occured");
            }
        }

        //Get Top Two Mobile Operators with Best Ratings
        public List<MobileOperator> GetTopTwoMobileOperators()
        {
            DataTable dtblTopMobileOperators = new DataTable();

            List<MobileOperator> mobileOperators = new List<MobileOperator>();

            try
            {
                SqlConnection sqlConn = new SqlConnection(_connectionString);
                sqlConn.Open();

                SqlCommand sqlComm = new SqlCommand();

                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "spGetTwoHighestRatedMobileOperators";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlComm.CommandText, sqlConn);
                sqlDataAdapter.Fill(dtblTopMobileOperators);

                foreach (DataRow dataRow in dtblTopMobileOperators.Rows)
                {
                    MobileOperator mobileOperator = new MobileOperator();

                    mobileOperator.Name = (string)dataRow["name"];
                    mobileOperator.Rating = (int)dataRow["rating"];
                    mobileOperator.Id = (int)dataRow["id"];

                    mobileOperators.Add(mobileOperator);
                }

                sqlConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); //Change
            }

            return mobileOperators;
        }

        //Get All The Details of all Persons
        public List<Person> GetAllPersonDetails()
        {
            DataTable dtblAllPersons = new DataTable();

            List<Person> persons = new List<Person>();

            try
            {
                SqlConnection sqlConn = new SqlConnection(_connectionString);
                sqlConn.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Persons;", sqlConn);
                sqlDataAdapter.Fill(dtblAllPersons);

                foreach (DataRow dataRow in dtblAllPersons.Rows)
                {
                    Person person = new Person();

                    person.PersonId = (int)dataRow["id"];
                    person.Name = (string)dataRow["name"];
                    person.MobileOperatorId = (int)dataRow["mobileOperatorId"];

                    persons.Add(person);
                }
                sqlConn.Close();
            }
            catch (Exception e)
            {
                throw new SqlCustomException("Error with Sql Connection");
            }

            return persons;
        }

        //Add new Person to Database
        public bool AddPerson(Person person)
        {
            bool isAdded = false;

            try
            {
                SqlConnection sqlConn = new SqlConnection(_connectionString);
                sqlConn.Open();

                string query = @"insert into Persons(name,mobileOperatorId) values(" + "\'" + person.Name + "\'" + "," + person.MobileOperatorId + ");";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConn))
                {
                    int rowAffected = sqlCommand.ExecuteNonQuery();

                    if (rowAffected == 1)
                    {
                        isAdded = true;
                    }
                    else
                    {
                        isAdded = false;
                    }
                }
                sqlConn.Close();

                return isAdded;
            }
            catch (Exception e)
            {
                throw new SqlCustomException("Error in adding new Person");
            }
        }

        //To Get Record of a Person from id
        public Person GetPersonRecord(int id)
        {
            DataTable personRecord = new DataTable();

            Person person = new Person();

            try
            {
                SqlConnection sqlConn = new SqlConnection(_connectionString);
                sqlConn.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from persons where id = " + id + ";", sqlConn);
                sqlDataAdapter.Fill(personRecord);

                foreach (DataRow dataRow in personRecord.Rows)
                {
                    person.PersonId = (int)dataRow["id"];
                    person.Name = (string)dataRow["name"];
                    person.MobileOperatorId = (int)dataRow["mobileOperatorId"];
                }

                sqlConn.Close();
            }
            catch (Exception e)
            {
                throw new CustomExceptions.SqlCustomException("Error with Sql Connection");
            }

            return person;
        }

        //Write Details of Person in Txt File
        public bool WritePersonsDetailsToFile(List<Person> persons)
        {
            string row = "";

            string path = "C:\\Users\\R3CK3R\\Desktop";

            //Change That
            if (!(Directory.Exists(path)))
            {
                throw new InvalidPathException("Invalid Path");
            }

            using (TextWriter writer = File.CreateText(path + "\\MobileOperators2.txt"))
            {
                foreach (Person personIterator in persons)
                {
                    row = personIterator.MobileOperatorId + " " + personIterator.Name + " " + personIterator.PersonId;

                    writer.WriteLine(row);

                    row = "";
                }
            }

            return true;
        }
    }
}