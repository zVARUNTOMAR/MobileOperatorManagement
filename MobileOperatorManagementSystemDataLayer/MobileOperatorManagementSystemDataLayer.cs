using Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MobileOperatorManagementSystemDataLayer
{
    public class MobileOperatorManagementSystemDataLayerClass
    {
        //private static string _connectionString = Properties.Settings.Default.connectionString;

        public DataTable GetMobileOperatorDeatils()
        {
            DataTable dtbldtblMobileOperators = new DataTable();

            try
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=VARUN;Initial Catalog=CellPhoneDatabase;Integrated Security=True");
                sqlConn.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from mobile_operator;", sqlConn);
                sqlDataAdapter.Fill(dtbldtblMobileOperators);
                sqlConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dtbldtblMobileOperators;
        }

        public DataTable GetTopTwoMobileOperators()
        {
            DataTable dtbleTopMobileOperators = new DataTable();

            try
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=VARUN;Initial Catalog=CellPhoneDatabase;Integrated Security=True");
                sqlConn.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(@"select top 2 name from mobile_operator order by rating desc;", sqlConn);
                sqlDataAdapter.Fill(dtbleTopMobileOperators);
                sqlConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dtbleTopMobileOperators;
        }

        public DataTable GetAllPersonDetails()
        {
            DataTable dtblAllPersons = new DataTable();

            try
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=VARUN;Initial Catalog=CellPhoneDatabase;Integrated Security=True");
                sqlConn.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Persons;", sqlConn);
                sqlDataAdapter.Fill(dtblAllPersons);
                sqlConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dtblAllPersons;
        }

        public void AddNewMobileOperatorToDatabase(MobileOperator newMobileOperator)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=VARUN;Initial Catalog=CellPhoneDatabase;Integrated Security=True");
                sqlConn.Open();

                string query = @"insert into mobile_operator(name,rating) values(" + "\'" + newMobileOperator.Name + "\'" + "," + newMobileOperator.rating + ");";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConn))
                {
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("Done...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool AddNewPersonToDatabase(Person newPerson)
        {
            Boolean flag = true;
            try
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=VARUN;Initial Catalog=CellPhoneDatabase;Integrated Security=True");
                sqlConn.Open();

                string query = @"insert into Persons(name,mobileOperatorId) values(" + "\'" + newPerson.Name + "\'" + "," + newPerson.MobileOperatorId + ");";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConn))
                {
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("Done...");
                }
                sqlConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in Adding Person in Database");
                flag = false;
                return flag;
            }

            return flag;
        }

        public DataTable GetPersonRecord(int id)
        {
            DataTable personRecord = new DataTable();

            try
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=VARUN;Initial Catalog=CellPhoneDatabase;Integrated Security=True");
                sqlConn.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from persons where id = " + id + ";", sqlConn);
                sqlDataAdapter.Fill(personRecord);
                sqlConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return personRecord;
        }

        public void WritePersonDetailsToTxt(DataTable dtblAllPersons)
        {
            string txt = "";

            using (TextWriter writer = File.CreateText("C:\\Users\\R3CK3R\\Desktop\\MobileOperators2.txt"))
            {
                writer.WriteLine("Name || mobileOperatorId");
                foreach (DataRow dataRow in dtblAllPersons.Rows)
                {
                    txt = (int)dataRow["id"] + " " + (string)dataRow["Name"] + " " + (int)dataRow["mobileOperatorId"];

                    writer.WriteLine(txt);

                    txt = "";
                }
            }
        }
    }
}