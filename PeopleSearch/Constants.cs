using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PeopleSearch
{
    public class Constants
    {
        // To load seed data (Location ~/Resources)
        public static string ImageDirectoryPath = @"~/Resources/Images/";
        public static string SeedDataXmlFilePath = @"~/Resources/SeedData/";
        public static string PersonXmlFileName = @"Persons.xml";
        public static string PersonNode = @"person";
        public static string DetailsNode = @"detail";
        public static string FirstNameNode = @"firstname";
        public static string LastNameNode = @"lastname";
        public static string AddressNode = @"address";
        public static string DateOfBirthNode = @"dataofbirth";
        public static string HobbiesNode = @"hobbies";
        public static string GenderNode = @"gender";
        public static string ImageNameNode = @"imagename";

        // For connection string
        public static string ConnectionStringName = @"PersonContext";
        public static string ConnectionString = @"ConnectionString";
        public static string Equal = @"=";
        public static string DataSource = "Data Source";
        public static string Database = "Database";
        public static string IntegratedSecurity = @"Integrated Security";
        public static string SemiColon = @";";
        public static string ForwardSlash = @"\";
        public static string PersonString = @"person";
        public static string ProviderNameAsSqlClient = @"System.Data.SqlClient";
        public static string RegistryDirectoryPath = @"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL";
        public static string ReadOnlyString = @"_bReadOnly";




    }
}