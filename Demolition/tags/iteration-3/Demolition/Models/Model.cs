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

        /// <summary>
        /// This is a MASSIVE hack since we're hitting the database for the
        /// tests. For some reason projects outside of the Demolition web
        /// site have issues loading/parsing Web.config.
        /// </summary>
        protected static DemolitionDataContext GetDataContext()
        {
            if (ConfigurationManager.ConnectionStrings.Count > 1)
            {
                return new DemolitionDataContext();
            }
            else
            {
                var document = new XmlDocument();
                var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\\..\\..\\Demolition\Web.config"));

#if DEMOLITION_TEST
            var connectionNum = 1;
#else
                var connectionNum = 0;
#endif

                document.Load(path);
                var nodeParent = document.SelectSingleNode("/configuration/connectionStrings");
                var connectionString = nodeParent.ChildNodes[connectionNum].Attributes["connectionString"].Value;
                return new DemolitionDataContext(connectionString);
            }
        }

        public static void ExecuteCommand(string command)
        {
            GetDataContext().ExecuteCommand(command);
        }

        public static string Root
        {
            get
            {
                if (string.IsNullOrEmpty(_Root))
                {
                    return AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {
                    return _Root;
                }
            }
            set
            {
                _Root = value;
            }
        }
    }
}