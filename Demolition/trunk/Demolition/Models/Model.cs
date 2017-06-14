using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Configuration;

namespace Demolition.Models
{
    public class Model
    {
        static string _Root;

        protected static DemolitionDataContext GetDataContext()
        {
            if (Test || (HttpContext.Current != null && HttpContext.Current.Request.Url.Port == 8080))
            {
                return new DemolitionDataContext(ParseConnectionString("DemolitionTestDatabaseConnectionString"));
            }
            else
            {
                return new DemolitionDataContext(ParseConnectionString("DemolitionDatabaseConnectionString"));
            }
        }

        private static string ParseConnectionString(string which)
        {
            var document = new XmlDocument();
            document.Load(Path.Combine(Root, "Web.config"));

            return document.SelectSingleNode(string.Format("/configuration/connectionStrings/add[@name='{0}']/@connectionString", which)).Value;
        }

        public static void ExecuteCommand(string command)
        {
            GetDataContext().ExecuteCommand(command);
        }


        public static string PathFromRoot(string path)
        {
            return Path.GetFullPath(Path.Combine(Model.Root, path));
        }

        public static string Root
        {
            get
            {
                if (string.IsNullOrEmpty(_Root))
                    return AppDomain.CurrentDomain.BaseDirectory;
                else
                    return _Root;
            }
            set
            {
                _Root = value;
            }
        }

        public static bool Test { get; set; }
    }
}