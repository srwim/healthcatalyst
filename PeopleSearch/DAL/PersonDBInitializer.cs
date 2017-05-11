using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace PeopleSearch.DAL
{
    public class PersonDBInitializer : CreateDatabaseIfNotExists<PersonContext>
    {
        protected override void Seed(PersonContext context)
        {
            try
            {
                IList<Person> defaultPersons = GetSeedPersons();

                foreach (Person person in defaultPersons)
                    context.Persons.Add(person);

                base.Seed(context);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Person> GetSeedPersons()
        {
            // Virtual path (of a directory) on server which contains XML for persons' information.
            string seedDataXmlFilePath = HttpContext.Current.Server.MapPath(Constants.SeedDataXmlFilePath);
            List<Person> seedPersons = GetPersonsFromXmlFile(seedDataXmlFilePath);

            return seedPersons;
        }

        public List<Person> GetPersonsFromXmlFile(string path)
        {
            List<Person> persons = new List<Person>();

            // Load seed data file : ~/Resources/SeedData/Person.xml
            XDocument xmlDoc = XDocument.Load(Path.Combine(path, Constants.PersonXmlFileName));

            // Virtual path (of a directory) on server which contains images for persons.
            string imageDirectoryPath = HttpContext.Current.Server.MapPath(Constants.ImageDirectoryPath);

            // Iterate over each <detail> tag in xmlDoc to get person information.
            foreach (var DetailNode in xmlDoc.Descendants(Constants.DetailsNode))
            {
                Person person = new Person();
                person.FirstName = DetailNode.Element(Constants.FirstNameNode).Value;
                person.LastName = DetailNode.Element(Constants.LastNameNode).Value;
                person.Address = DetailNode.Element(Constants.AddressNode).Value;
                person.Hobbies = DetailNode.Element(Constants.HobbiesNode).Value;
                person.DateOfBirth = Convert.ToDateTime(DetailNode.Element(Constants.DateOfBirthNode).Value);
                person.Gender = DetailNode.Element(Constants.GenderNode).Value;

                string imageName = DetailNode.Element(Constants.ImageNameNode).Value;

                // Virtual path of image file on server for the given <imagename>
                string imagePath = Path.Combine(imageDirectoryPath, imageName);

                person.Image = GetImageBytes(imagePath);

                persons.Add(person);
            }

            return persons;
        }

        public byte[] GetImageBytes(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            byte[] data = File.ReadAllBytes(path);

            return data;
        }
    }
}