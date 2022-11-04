using System;

namespace MacrixPracticalTask.Models
{
    public class PersonModel
    {
        public PersonModel(string firstName,
            string lastName, 
            string phoneNumber,
            DateTime dateOfBirth,
            int age,
            AddressModel address)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Age = age;
            Address = address;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; }

        public AddressModel Address { get; set; }
    }
}
