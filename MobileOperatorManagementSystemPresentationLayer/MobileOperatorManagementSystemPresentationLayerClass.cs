using Entities;
using MobileOperatorManagementSystemBussinessLayer;
using System;
using System.Collections.Generic;
using System.Data;

namespace MobileOperatorManagementSystemPresentationLayer
{
    internal class MobileOperatorManagementSystemPresentationLayerClass
    {
        private static void Main(string[] args)
        {
            int choiceMenu = 0;
            int choiceProg = 0;

            List<MobileOperator> mobileOperators = new List<MobileOperator>();
            List<Person> persons = new List<Person>();

            //Object to Bussiness Layer

            MobileOperatorManagementSystemBussinessLayerClass MobileOperatorManagementSystemBussinessLayerObj = new MobileOperatorManagementSystemBussinessLayerClass();

            do
            {
                Console.WriteLine("====================================");
                Console.WriteLine("  MOBILE OPERATOR MANAGEMENT SYSTEM  ");
                Console.WriteLine("=====================================");
                Console.WriteLine("Press 1 to add New Mobile Operator");
                Console.WriteLine("Press 2 to Display all Mobile Operators");
                Console.WriteLine("Press 3 to add New Person");
                Console.WriteLine("Press 4 to Display two mobile Operators by Rating");
                Console.WriteLine("Press 5 to Serach Person with mobile Operator using Person Id");
                Console.WriteLine("Press 6 to display person with id,name,operator name and write it to text file");
                Console.WriteLine("Enter Your Choice:");

                try
                {
                    choiceMenu = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                switch (choiceMenu)
                {
                    case 1:

                        //Getting Mobile Operators Details
                        Console.WriteLine("Enter Mobile Operator Name :");
                        string mobileOperatorName = Console.ReadLine();

                        Console.WriteLine("Enter Rating :");
                        int rating = Convert.ToInt32(Console.ReadLine());

                        MobileOperator newMobileOperator = MobileOperatorManagementSystemBussinessLayerObj.AddMobileOperator(mobileOperatorName, rating, mobileOperators);

                        if (newMobileOperator != null)
                        {
                            mobileOperators.Add(newMobileOperator);
                            Console.WriteLine("New Mobile Operator Added");
                            mobileOperators.Add(newMobileOperator);
                        }
                        else
                        {
                            Console.WriteLine("Error in Adding New Mobile Operator");
                        }
                        break;

                    case 2:

                        DataTable dtblMobileOperators = MobileOperatorManagementSystemBussinessLayerObj.GetMobileOperatorDetails();

                        foreach (DataRow dataRow in dtblMobileOperators.Rows)
                        {
                            Console.WriteLine("|{0}\t|{1}|\t{2}", (int)dataRow["id"], (string)dataRow["name"], (int)dataRow["rating"]);

                        }

                        break;

                    case 3:

                        Console.WriteLine("Enter Person Name:");
                        string personName = Console.ReadLine();

                        Console.WriteLine("Enter Mobile Operator ID:");
                        int mobileOperatorId = Convert.ToInt32(Console.ReadLine());

                        Person newPerson = MobileOperatorManagementSystemBussinessLayerObj.AddPerson(personName, mobileOperatorId);

                        if (newPerson != null)
                        {
                            Console.WriteLine("New Person Added Successfully");
                            persons.Add(newPerson);
                        }
                        else
                        {
                            Console.WriteLine("Error in Adding New Person");
                        }

                        break;

                    case 4:

                        DataTable dtbleTopMobileOperators = MobileOperatorManagementSystemBussinessLayerObj.GetTopTwoMobileOperators();

                        foreach (DataRow dataRow in dtbleTopMobileOperators.Rows)
                        {
                            Console.WriteLine("|{0}",(string)dataRow["name"]);
                        }

                        break;

                    case 5:

                        Console.WriteLine("Enter ID :");
                        int id = -1;
                        Boolean flag = false;

                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        DataTable personRecord = MobileOperatorManagementSystemBussinessLayerObj.SearchPerson(id);

                        foreach (DataRow dataRow in personRecord.Rows)
                        {
                            flag = true;
                            Console.WriteLine("|{0}\t|{1}|\t{2}", (int)dataRow["id"], (string)dataRow["Name"], (int)dataRow["mobileOperatorId"]);
                        }

                        if (flag != true) {
                            Console.WriteLine("No pErson Found.....");
                        }

                        break;

                    case 6:

                        DataTable dtblAllPersons = MobileOperatorManagementSystemBussinessLayerObj.GetAllPersonDetails();

                        foreach (DataRow dataRow in dtblAllPersons.Rows)
                        {
                            Console.WriteLine("|{0}\t|{1}|\t{2}", (int)dataRow["id"], (string)dataRow["Name"], (int)dataRow["mobileOperatorId"]);
                        }

                        MobileOperatorManagementSystemBussinessLayerObj.WritePersonsDetailsTotxt(dtblAllPersons);

                        break;

                    default:
                        Console.WriteLine("Wrong Choice");
                        break;
                }

                Console.WriteLine("Press 1 to continue ");
                try
                {
                    choiceProg = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (choiceProg == 1);
        }
    }
}