using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeopleSearch.ViewModels
{
    public class ConnectionStringViewModel
    {
        // "connectionString=Data Source=LENOVO-PC\SQLEXPRESS;Database=person;Integrated Security=True"
        [Required]
        [StringLength(100, ErrorMessage = "Connection name can't be empty")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Server name can't be empty")]
        public string ServerNameValue { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Sql server instance can't be empty", MinimumLength =1)]
        public List<string> SqlServerInstances { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Custom server instance can't be empty", MinimumLength = 1)]
        public bool isCustomInstance { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Custom server instance can't be empty", MinimumLength = 1)]
        public string SqlServerInstanceValue { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Database name can't be empty", MinimumLength = 1)]
        public string DatabaseValue { get; set; }
                
        public bool IntegratedSecurityValue { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "This field can't be empty", MinimumLength = 1)]
        public string FinalConfigurationString { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "This field can't be empty", MinimumLength = 1)]
        public string ProviderName { get; set; }


        public ConnectionStringViewModel()
        {
            ServerNameValue = default(string);
            SqlServerInstances = new List<string>();
            DatabaseValue = default(string);
            IntegratedSecurityValue = default(bool);
        }

        public override string ToString()
        {
            string connectionStringValue = String.Format(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                Constants.DataSource, Constants.Equal, ServerNameValue, Constants.ForwardSlash, SqlServerInstanceValue, Constants.SemiColon,
                Constants.Database, Constants.Equal, DatabaseValue, Constants.SemiColon,
                Constants.IntegratedSecurity, Constants.Equal, IntegratedSecurityValue.ToString());

            return connectionStringValue;
        }
    }
}