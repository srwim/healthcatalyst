using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.Areas.AreaPerson.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Hobbies { get; set; }
        public byte[] Image { get; set; }
        public string Gender { get; set; }

        public Person()
        {
            FirstName = default(string);
            LastName = default(string);
            DateOfBirth = default(DateTime);
            Address = default(string);
            Hobbies = default(string);
            Gender = default(string);
            Image = null;
        }

        public Person(string firstName, string lastName, string address, string hobbies, DateTime dateOfBirth, string gender)
        {
            FirstName = default(string);
            LastName = default(string);
            DateOfBirth = default(DateTime);
            Address = default(string);
            Hobbies = default(string);
            Gender = default(string);
            Image = null;
        }

        public bool Equals(Person person)
        {

            return FirstName.Equals(person.FirstName)
                && LastName.Equals(person.LastName)
                && Address.Equals(person.Address)
                && DateOfBirth.Equals(person.DateOfBirth)
                && Hobbies.Equals(person.Hobbies)
                && Gender.Equals(person.Gender)
                && Image.Equals(person.Image);
        }

    }
}