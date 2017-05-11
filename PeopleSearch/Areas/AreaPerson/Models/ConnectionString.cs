using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.Areas.AreaPerson.Models
{
    public class ConnectionString
    {
        // "connectionString=Data Source=WIM-PC\SQLEXPRESS;Database=person;Integrated Security=True"
        public string Name { get; set; }
        public string ServerNameValue { get; set; }
        public string SqlServerInstanceValue { get; set; }
        public string DatabaseValue { get; set; }
        public bool IntegratedSecurityValue { get; set; }
        public string FinalConfigurationString { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string ProviderName { get; set; }


        public ConnectionString()
        {
            ServerNameValue = default(string);
            SqlServerInstanceValue = default(string);
            DatabaseValue = default(string);
            IntegratedSecurityValue = default(bool);
        }

        //public bool IsValidConnectionString(ConnectionString conString)
        //{
        //    try
        //    {
        //        string finalConnectionString = conString.ToString();
        //        string provider = conString.ProviderName;

        //        DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
        //        using (DbConnection conn = factory.CreateConnection())
        //        {
        //            conn.ConnectionString = finalConnectionString;
        //            conn.Open();
        //        }

        //        var settings = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName];
        //        var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        //        fi.SetValue(settings, false);

        //        settings.ConnectionString = finalConnectionString;
        //        settings.ProviderName = conString.ProviderName;


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public override string ToString()
        {
            if (FinalConfigurationString == null || FinalConfigurationString.Length == 0)
            {
                FinalConfigurationString = String.Format(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                    Constants.DataSource, Constants.Equal, ServerNameValue, Constants.ForwardSlash, SqlServerInstanceValue, Constants.SemiColon,
                    Constants.Database, Constants.Equal, DatabaseValue, Constants.SemiColon,
                    Constants.IntegratedSecurity, Constants.Equal, IntegratedSecurityValue.ToString());
            }

            return FinalConfigurationString;
        }
    }
}