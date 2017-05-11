using System.Linq;
using System.Web.Mvc;
using System.IO;
using PeopleSearch.ViewModels;
using PeopleSearch.ServiceFactory;
using System.Collections.Generic;
using System;
using PeopleSearch.Helper;
using Microsoft.Win32;
using System.Web.Configuration;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using PeopleSearch.Areas.AreaPerson.Models;

namespace PeopleSearch.Areas.AreaPerson.Controllers
{
    public class PersonController : Controller
    {
        public static byte[] _imageBytes;
        PersonManager _personManager;

        public PersonController()
        {
            _personManager = new PersonManager();
        }

        public ActionResult Index()
        {
            try
            {
                ConnectionStringViewModel conStringVM = new ConnectionStringViewModel();
                conStringVM = ConnectionStringHelper.GetDefaultConnectionString();

                return View(conStringVM);
            }
            catch (Exception)
            {

                return View(new ConnectionStringViewModel());
            }

        }

        #region ConnectionString
        [HttpGet]
        public ActionResult ConfigurationResult(ConnectionString conStringObj)
        {
            // updated "PersonContext" connectionString in web config only for this 
            // execution.
            ConnectionStringHelper.UpdateConnectionStringInWebConfig(conStringObj);

            // Check validity of the connection string. If turns out valid then save seed data in Database.
            // Seed data don't get saved over if you are running this application with a connectionstring,
            // which you have already used to run this application before.
            _personManager = new PersonManager();
            string successOrErrorMsg = _personManager.SaveSeedData();
            conStringObj.Message = successOrErrorMsg;

            return View(conStringObj);
        }
        #endregion

        #region Navigation buttons
        public ActionResult Register()
        {
            return View("Register");
        }

        public ActionResult Search()
        {
            return View("Search");
        }

        #endregion

        #region Search and Results

        [HttpGet]
        public ActionResult Result(string name)
        {
            List<PersonViewModel> resultPersons = _personManager.Search(name);

            return View(resultPersons);
        }

        #endregion

        #region Create record in DB
        [HttpPost]
        public ActionResult Create(PersonViewModel personViewModel)
        {
            int id = _personManager.Create(personViewModel, _imageBytes);

            return Json(new Person());
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            Stream stream = ImageHelper.GetAttachmentStramFromHttpFiles(Request.Files);
            _imageBytes = ImageHelper.ConvertStramToByeArray(stream);

            return Json("File uploaded successfully");
        }

        #endregion
    }
}