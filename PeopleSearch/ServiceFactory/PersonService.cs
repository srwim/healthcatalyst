using PeopleSearch.DAL;
using PeopleSearch.Models;
using PeopleSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PeopleSearch.ServiceFactory
{
    public class PersonService
    {
        PersonProvider _provider;

        public PersonService(PersonContext db)
        {
            _provider = new PersonProvider(db);
        }

        public int Create(Person person)
        {
            if (person != null)
            {
                if (string.IsNullOrEmpty(person.FirstName) && string.IsNullOrEmpty(person.LastName))

                    return -1;
                else

                    return _provider.Create(person);
            }
            else
            {
                return 0;
            }
        }

        public List<Person> Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return _provider.SearchByName(name);
            else
                return null;
        }

        public int RecordsCount()
        {
            return _provider.RecordsCount();
        }

    }
}