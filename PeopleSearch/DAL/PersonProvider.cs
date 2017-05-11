using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PeopleSearch.DAL
{
    public class PersonProvider
    {
        private PersonContext _db;

        public PersonProvider()
        {
        }

        public PersonProvider(PersonContext db)
        {
            _db = db;
        }

        // SAVE a person in DB
        public int Create(Person person)
        {
            _db.Persons.Add(person);
            _db.SaveChanges();

            return person.Id;
        }

        // Get list of persons by given name.
        public List<Person> SearchByName(string name)
        {
            List<Person> persons = _db.Persons
                                    .Where(p => p.FirstName.ToLower().Trim().Contains(name.ToLower().Trim())
                                        || p.LastName.ToLower().Trim().Contains(name.ToLower().Trim()))
                                    .Select(p => p).ToList();

            return persons.ToList();
        }

        // Get number of records in database.
        public int RecordsCount()
        {
            int count = _db.Persons
                         .Select (p=> p).Count();

            return count;
        }
    }
}