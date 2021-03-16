using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using Exceptions;
using MobileOperatorManagementSystemDataLayer;

namespace MobileOperatorManagementSystemBussinessLayer
{
    public class MobileOperatorManagementSystemBussinessLayerClass
    {
        static int Id = 200;
        MobileOperatorManagementSystemDataLayerClass MobileOperatorManagementSystemDataLayerObj = new MobileOperatorManagementSystemDataLayerClass();
        public void IsDuplicateName(string name, List<MobileOperator> mobileOperators) {

            foreach (MobileOperator temp in mobileOperators) {
                if (name.Equals(temp.Name)) {
                    throw new Exceptions.DuplicateNameException("Name AlreadyExist");
                }
            }
        }

        public void IsRatingValid(int rating)
        {
            if (rating > 5) {
                throw new InvalidRatingException("Invalid Rating");
            }
        }

        public MobileOperator AddMobileOperator(string mobileOperatorName, int rating, List<MobileOperator> mobileOperators)
        {

            
            try
            {
                IsDuplicateName(mobileOperatorName, mobileOperators);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            try {

                IsRatingValid(rating);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            MobileOperator newMobileOperator = new MobileOperator();

            newMobileOperator.Id = 200;
            newMobileOperator.Name = mobileOperatorName;
            newMobileOperator.rating = rating;

            MobileOperatorManagementSystemDataLayerObj.AddNewMobileOperatorToDatabase(newMobileOperator);

            Id++;
            return newMobileOperator;

        }

        public DataTable GetMobileOperatorDetails()
        {
            DataTable dtblMobileOperators = MobileOperatorManagementSystemDataLayerObj.GetMobileOperatorDeatils();

            return dtblMobileOperators;
        }

        public Person AddPerson(string personName,int mobileOperatorId)
        {

            Person newPerson = new Person();

            newPerson.Name = personName;
            newPerson.MobileOperatorId = mobileOperatorId;

            if (MobileOperatorManagementSystemDataLayerObj.AddNewPersonToDatabase(newPerson))
            {
                return newPerson;
            }
            else {
                return null;
            }
        }

        public DataTable GetTopTwoMobileOperators()
        {

            DataTable dtbleTopMobileOperators = MobileOperatorManagementSystemDataLayerObj.GetTopTwoMobileOperators();

            return dtbleTopMobileOperators;
        }

        public DataTable SearchPerson(int id)
        {
            DataTable personRecord = MobileOperatorManagementSystemDataLayerObj.GetPersonRecord(id);

            return personRecord;
        }

        public DataTable GetAllPersonDetails()
        {
            DataTable dtblTopMobileOperators = MobileOperatorManagementSystemDataLayerObj.GetAllPersonDetails();

            return dtblTopMobileOperators;
            
        }

        public void WritePersonsDetailsTotxt(DataTable dtblAllPersons)
        {
            MobileOperatorManagementSystemDataLayerObj.WritePersonDetailsToTxt(dtblAllPersons);
        }
    }
}
