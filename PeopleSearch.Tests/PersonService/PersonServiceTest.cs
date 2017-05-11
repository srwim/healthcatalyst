using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleSearch.DAL;
using PeopleSearch.Models;
using PeopleSearch.ServiceFactory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PeopleSearch.Tests
{
    [TestClass]
    public class PersonServiceTestMethods
    {
        [TestMethod]
        public void TestCreateMethod_PersonWithoutImage()
        {
            // Arrange
            Person inputPerson = new Person();
            inputPerson.FirstName = Constants.VikasFirstString;
            inputPerson.LastName = Constants.SangwanLastName;
            inputPerson.Address = Constants.AddressString;
            inputPerson.Gender = Constants.FemaleGenderString;
            inputPerson.Hobbies = Constants.HobbiesString;
            inputPerson.DateOfBirth = DateTime.Now;

            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }

        [TestMethod]
        public void TestCreateMethod_PersonWithNullObject()
        {
            // Arrange
            Person inputPerson = null;
            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void TestCreateMethod_PersonWithEmptyInformation()
        {
            // Arrange
            Person inputPerson = new Person();
            inputPerson.FirstName = string.Empty;
            inputPerson.LastName = string.Empty;
            inputPerson.Address = string.Empty;
            inputPerson.Gender = string.Empty;
            inputPerson.Hobbies = string.Empty;
            inputPerson.DateOfBirth = DateTime.Now;
            inputPerson.Image = null;

            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());

        }

        [TestMethod]
        public void TestCreateMethod_CreateFourPersons()
        {
            // Arrange
            Person inputPerson1 = new Person();
            inputPerson1.FirstName = "Robert";
            inputPerson1.LastName = "Smith";
            inputPerson1.Address = Constants.AddressString;
            inputPerson1.Gender = Constants.FemaleGenderString;
            inputPerson1.Hobbies = Constants.HobbiesString;
            inputPerson1.DateOfBirth = DateTime.Now;

            Person inputPerson2 = new Person();
            inputPerson2.FirstName = "Willard";
            inputPerson2.LastName = "Jolly";
            inputPerson2.Address = Constants.AddressString;
            inputPerson2.Gender = Constants.FemaleGenderString;
            inputPerson2.Hobbies = Constants.HobbiesString;
            inputPerson2.DateOfBirth = DateTime.Now;


            Person inputPerson3 = new Person();
            inputPerson3.FirstName = "Samantha";
            inputPerson3.LastName = "B.";
            inputPerson3.Address = Constants.AddressString;
            inputPerson3.Gender = Constants.FemaleGenderString;
            inputPerson3.Hobbies = Constants.HobbiesString;
            inputPerson3.DateOfBirth = DateTime.Now;

            Person inputPerson4 = new Person();
            inputPerson4.FirstName = "Bob";
            inputPerson4.LastName = "Marley";
            inputPerson4.Address = Constants.AddressString;
            inputPerson4.Gender = Constants.FemaleGenderString;
            inputPerson4.Hobbies = Constants.HobbiesString;
            inputPerson4.DateOfBirth = DateTime.Now;


            var mockSet = new Mock<DbSet<Person>>();

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);


            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson1);
            service.Create(inputPerson2);
            service.Create(inputPerson3);
            service.Create(inputPerson4);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.AtLeast(4));
            mockContext.Verify(m => m.SaveChanges(), Times.AtLeast(4));
        }

        [TestMethod]
        public void TestSearchMethodWithSearchString()
        {
            var data = new List<Person>
            {
                new Person { FirstName = "Alice" ,LastName="Wonderland" },
                new Person { FirstName = "Keeth",LastName="R" },
                new Person { FirstName = "Steve",LastName="Smith" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSet.Object);

            var service = new PersonService(mockContext.Object);

            List<Person> searchresults = service.Search("e");

            Assert.AreEqual(3, searchresults.Count());
            Assert.AreEqual("Alice", searchresults[0].FirstName);
            Assert.AreEqual("Keeth", searchresults[1].FirstName);
            Assert.AreEqual("Steve", searchresults[2].FirstName);
        }

        [TestMethod]
        public void TestSearchMethodWithEmptySearchString()
        {
            // Arrange
            var data = new List<Person>().AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            string searchStr = string.Empty;
            List<Person> searchresults = service.Search(searchStr);

            // Assert
            Assert.AreEqual(null, searchresults);
        }

        [TestMethod]
        public void TestSearchMethodWithNullAsSearchString()
        {
            // Arrange
            var data = new List<Person>().AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            string searchStr = null;
            List<Person> searchresults = service.Search(searchStr);

            // Assert
            Assert.AreEqual(null, searchresults);
        }

        [TestMethod]
        public void TestSearchMethodWithSpecialCharsAsSearchString()
        {
            // Arrange
            var data = new List<Person>().AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            string searchStr = "*>>>>{}/";
            List<Person> searchresults = service.Search(searchStr);

            // Assert
            Assert.AreEqual(0, searchresults.Count);

        }

        [TestMethod]
        public void TestCreateMethod_PersonWithImage()
        {
            // Arrange
            Person inputPerson = new Person();
            inputPerson.FirstName = Constants.VikasFirstString;
            inputPerson.LastName = Constants.SangwanLastName;
            inputPerson.Address = Constants.AddressString;
            inputPerson.Gender = Constants.FemaleGenderString;
            inputPerson.Hobbies = Constants.HobbiesString;
            inputPerson.DateOfBirth = DateTime.Now;


            Image img = System.Drawing.Image.FromFile("../../Resources/Images/Albert_Einstein.jpg");
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            inputPerson.Image = ms.ToArray();

            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }
    }
}