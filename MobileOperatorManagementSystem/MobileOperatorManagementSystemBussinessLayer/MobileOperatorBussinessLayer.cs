using CustomExceptions;
using Entities;
using MobileOperatorManagementSystemDataLayer;
using System;
using System.Collections.Generic;

namespace MobileOperatorManagementSystemBussinessLayer
{
    //Change
    public class MobileOperatorBussinessLayer
    {
        public MobileOperatorDataLayer mobileOperatorDataLayer = new MobileOperatorDataLayer();

        public void IsDuplicateName(string name, List<MobileOperator> mobileOperators)
        {
            foreach (MobileOperator temp in mobileOperators)
            {
                if (name.Equals(temp.Name))
                {
                    throw new DuplicateNameException("Name Already Exist");
                }
            }
        }

        public void IsRatingValid(int rating)
        {
            if (rating > 5)
            {
                throw new InvalidRatingException("Invalid Rating");
            }
        }

        public MobileOperator AddMobileOperator(MobileOperator mobileOperator, List<MobileOperator> mobileOperators)
        {
            try
            {
                IsDuplicateName(mobileOperator.Name, mobileOperators);
            }
            catch (Exception e)
            {
                throw new DuplicateNameException("Name Already Exist");
            }

            try
            {
                IsRatingValid(mobileOperator.Rating);
            }
            catch (Exception e)
            {
                throw new InvalidRatingException("Invalid Rating Exception");
            }

            if (mobileOperatorDataLayer.AddMobileOperator(mobileOperator))
            {
                return mobileOperator;
            }
            else
                throw new EmptyObjectException("Object can't be null");
        } 
     

        public List<MobileOperator> GetMobileOperatorDetails()
        {
            List<MobileOperator> mobileOperators = new List<MobileOperator>();

            try
            {
                mobileOperators = mobileOperatorDataLayer.GetMobileOperatorDeatils();
            }
            catch (Exception e)
            {
                throw new EmptyCollectionException("Mobile Operators can't be Empty");
            }

            return mobileOperators;
        }

        public Person AddPerson(Person person)
        {
            if (mobileOperatorDataLayer.AddPerson(person))
            {
                return person;
            }
            else
            {
                throw new SqlCustomException("Some error occurred");
            }
        }

        public List<MobileOperator> GetTopTwoMobileOperators()
        {
            List<MobileOperator> mobileOperators = mobileOperatorDataLayer.GetTopTwoMobileOperators();

            if (mobileOperators.Count < 1)
            {
                throw new EmptyCollectionException("Mobile Operator can't be empty");
            }
            else
            {
                return mobileOperators;
            }
        }

        public Person GetPersonRecord(int id)
        {
            Person person = mobileOperatorDataLayer.GetPersonRecord(id);

            if (person != null)
            {
                return person;
            }
            else
            {
                throw new EmptyObjectException("Object Can't be Null");
            }
        }

        public List<Person> GetAllPersonDetails()
        {
            List<Person> persons = mobileOperatorDataLayer.GetAllPersonDetails();

            if (persons.Count == 0)
            {
                throw new EmptyCollectionException("Collections can't be Empty");
            }
            else
            {
                return persons;
            }
        }

        public bool WritePersonsDetailsToFile(List<Person> person)
        {
            bool isWriteSuccess = true;

            try {

                mobileOperatorDataLayer.WritePersonsDetailsToFile(person);

            }
            catch (Exception e) {
                throw new FileIOException("Invalid I/O Operation");
            }
            return isWriteSuccess;
        }
    }
}