using Entities;
using MobileOperatorManagementSystemBussinessLayer;
using System;
using System.Collections.Generic;

namespace MobileOperatorManagementSystemPresentationLayer
{
    internal class MobileOperatorPresentationLayer //Change to public
    {
        private static void Main(string[] args)
        {
            int choiceMenu = 0;
            int choiceProg = 0;

            List<MobileOperator> mobileOperators = new List<MobileOperator>();  //To Store Mobile Operators
            List<Person> persons = new List<Person>();    //To Store Persons

            //Object to Bussiness Layer
            MobileOperatorBussinessLayer mobileOperatorBussinessLayer = new MobileOperatorBussinessLayer();

            do
            {
                //Menu
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
                        MobileOperator mobileOperator = new MobileOperator();

                        Console.WriteLine("Enter Mobile Operator Name :");
                        mobileOperator.Name = Console.ReadLine();

                        Console.WriteLine("Enter Rating :");
                        mobileOperator.Rating = Convert.ToInt32(Console.ReadLine());

                        MobileOperator newMobileOperator = null;

                        try
                        {
                            newMobileOperator = mobileOperatorBussinessLayer.AddMobileOperator(mobileOperator, mobileOperators);
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }

                        if (newMobileOperator != null)
                        {
                            mobileOperators.Add(newMobileOperator);
                            Console.WriteLine("New Mobile Operator Added");
                        }
                        else
                        {
                            Console.WriteLine("Error in Adding New Mobile Operator");
                        }
                        break;

                    case 2:

                        List<MobileOperator> mobileOperatorsDetails = new List<MobileOperator>();     //Get All Details of all Mobile Operators

                        try
                        {
                            mobileOperatorsDetails = mobileOperatorBussinessLayer.GetMobileOperatorDetails();
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }

                        //Printing the Mobile Operators details
                        foreach (MobileOperator mobileOperatorIterator in mobileOperatorsDetails)
                        {
                            Console.WriteLine("{0},{1},{2}", mobileOperatorIterator.Id, mobileOperatorIterator.Name, mobileOperatorIterator.Rating);
                        }

                        break;

                    case 3:

                        Person person = new Person();

                        Console.WriteLine("Enter Person Name:");
                        person.Name = Console.ReadLine();

                        Console.WriteLine("Enter Mobile Operator ID:");
                        person.MobileOperatorId = Convert.ToInt32(Console.ReadLine());

                        //To object to add new Person
                        Person newPerson = new Person();

                        try
                        {
                            newPerson = mobileOperatorBussinessLayer.AddPerson(person);
                            persons.Add(newPerson);
                            Console.WriteLine("New Person Added Successfully");
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }

                        break;

                    case 4:

                        List<MobileOperator> topMobileOperators = new List<MobileOperator>();

                        try
                        {
                            topMobileOperators = mobileOperatorBussinessLayer.GetTopTwoMobileOperators();
                            foreach (MobileOperator mobileOperatorIterator in topMobileOperators)
                            {
                                Console.WriteLine("{0},{1},{2}", mobileOperatorIterator.Id, mobileOperatorIterator.Name, mobileOperatorIterator.Rating,mobileOperatorIterator.Rating);
                            }
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }
                        
                        break;

                    case 5:

                        Console.WriteLine("Enter ID :");
                        int id = 0;

                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        Person personDetails = mobileOperatorBussinessLayer.GetPersonRecord(id);

                        if (personDetails.PersonId!=0)
                        {
                            Console.WriteLine("{0},{1},{2}", personDetails.PersonId, personDetails.Name, personDetails.MobileOperatorId);
                        }
                        else
                        {
                            Console.WriteLine("PERSON DOESN'T EXIST");
                        }
                        break;

                    case 6:

                        List<Person> personsDetails = new List<Person>();
                        try
                        {
                            personsDetails = mobileOperatorBussinessLayer.GetAllPersonDetails();
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }

                        foreach (Person personIterator in personsDetails)
                        {
                            Console.WriteLine("{0},{1},{2}", personIterator.PersonId, personIterator.Name, personIterator.MobileOperatorId);
                        }

                        try
                        {
                            mobileOperatorBussinessLayer.WritePersonsDetailsToFile(personsDetails);

                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }
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