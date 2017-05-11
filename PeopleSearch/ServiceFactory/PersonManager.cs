using PeopleSearch.DAL;
using PeopleSearch.Models;
using PeopleSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.ServiceFactory
{
    public class PersonManager
    {
        PersonContext _db;
        PersonService _personService;

        public PersonManager()
        {
            _db = new PersonContext();
            _personService = new PersonService(_db);
        }
        
        // SAVE person information in DB
        public int Create(PersonViewModel personViewModel, byte[] imageBytes)
        {
            try
            {
                Person person = GetPerson(personViewModel, imageBytes);

                return _personService.Create(person);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Person GetPerson(PersonViewModel personViewModel, byte[] imageBytes)
        {
            try
            {
                if (string.IsNullOrEmpty(personViewModel.FirstName) && string.IsNullOrEmpty(personViewModel.LastName))
                {
                    return null;
                }
                else
                {
                    var person = new Person();
                    person.FirstName = personViewModel.FirstName;
                    person.LastName = personViewModel.LastName;
                    person.Address = personViewModel.Address;
                    person.DateOfBirth = personViewModel.DateOfBirth;
                    person.Image = imageBytes;
                    person.Hobbies = personViewModel.Hobbies;
                    person.Gender = personViewModel.Gender;

                    return person;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Find list of persons by given name.
        public List<PersonViewModel> Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    List<Person> persons = _personService.Search(name);
                    List<PersonViewModel> personViewModelList = PopulatePersonViewModelList(persons);

                    return personViewModelList;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return new List<PersonViewModel>();
        }
        
        public static List<PersonViewModel> PopulatePersonViewModelList(List<Person> persons)
        {
            List<PersonViewModel> personViewModelList = new List<PersonViewModel>();

            foreach (Person person in persons)
            {
                PersonViewModel personViewModel = new PersonViewModel();
                personViewModel.FirstName = person.FirstName;
                personViewModel.LastName = person.LastName;
                personViewModel.Address = person.Address;
                personViewModel.DateOfBirth = person.DateOfBirth;
                personViewModel.Hobbies = person.Hobbies;
                personViewModel.Gender = person.Gender;
                double diff = DateTime.Now.Subtract(person.DateOfBirth).Days / (365.25 / 12);
                personViewModel.Age = (float)Math.Round(diff / 12, 2);

                if (person.Image != null)
                {
                    // Convert byte[] to image
                    var base64 = Convert.ToBase64String(person.Image);
                    string imgSrc = String.Format("data:image/jpeg;base64,{0}", base64);

                    personViewModel.Image = imgSrc;
                }

                personViewModelList.Add(personViewModel);
            }

            return personViewModelList;
        }
        
        // Save 8 records of Seed data in database for valid connection string.
        public string SaveSeedData()
        {
            try
            {
                int recordsCount = _personService.RecordsCount();

                return "Success!! Connection established successfully. Number of person records in database :  " + recordsCount.ToString() + ".";
            }
            catch (Exception ex)
            {

                return String.Format("{0}{1}", "Error Occured while loading seed data. Please try again with correct connectionstring configurations", ex.Message);
            }
        }
    }
}