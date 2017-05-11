using Microsoft.Win32;
using PeopleSearch.Areas.AreaPerson.Models;
using PeopleSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace PeopleSearch.Helper
{
    public class ConnectionStringHelper
    {

        public static ConnectionStringViewModel GetDefaultConnectionString()
        {
            ConnectionStringViewModel conStringVM = new ConnectionStringViewModel();
            conStringVM.Name = Constants.ConnectionStringName;
            conStringVM.ServerNameValue = GetServerName();
            conStringVM.SqlServerInstances = GetSqlInstanceNames();
            if (conStringVM.SqlServerInstances.Count > 0)
            {
                conStringVM.SqlServerInstanceValue = conStringVM.SqlServerInstances.First();
            }
            conStringVM.DatabaseValue = Constants.PersonString;
            conStringVM.IntegratedSecurityValue = true;
            conStringVM.ProviderName = Constants.ProviderNameAsSqlClient;

            return conStringVM;
        }

        // Get local machine name as the servername what SqlServer instance is running on.
        public static string GetServerName()
        {
            string myServer = Environment.MachineName;
            return myServer;
        }

        // Find all sql server instances of local machine.
        // TODO: It fails if 32 bit is installed on 64-bit machine
        public static List<string> GetSqlInstanceNames()
        {
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;

            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey key = baseKey.OpenSubKey(Constants.RegistryDirectoryPath);
                foreach (string s in key.GetValueNames())
                {

                }
                var result = key.GetValueNames().ToList();
                return result;
            }
        }

        // Updates connectionstring value only in memory. Changes are not persisted in file.
        // Application stopped, changed in web-config are gone!
        public static void UpdateConnectionStringInWebConfig(ConnectionString conStringObj)
        {
            string finalConnectionString = conStringObj.ToString();
            string provider = conStringObj.ProviderName;

            var settings = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName];
            var fi = typeof(ConfigurationElement).GetField(Constants.ReadOnlyString, BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(settings, false);

            settings.ConnectionString = finalConnectionString;
            settings.ProviderName = conStringObj.ProviderName;
        }

    }
}