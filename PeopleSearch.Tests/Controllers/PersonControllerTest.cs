using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearch.Models;
using PeopleSearch.ServiceFactory;
using PeopleSearch.DAL;
using System.Data.Entity;
using Moq;
using PeopleSearch.Areas.AreaPerson.Controllers;

namespace PeopleSearch.Tests.Controllers
{

    [TestClass]
    public class PersonControllerTest
    {
        PersonController controller;
        [TestInitialize]
        public void Initialize()
        {
            controller = new PersonController();
        }

        [TestMethod]
        public void Index()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAction_Register()
        {
            var result = controller.Register() as ViewResult;
            Assert.AreEqual("Register", result.ViewName);

        }

        [TestMethod]
        public void TestAction_Search()
        {
            var result = controller.Search() as ViewResult;
            Assert.AreEqual("Search", result.ViewName);

        }
    }
}
