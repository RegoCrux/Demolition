using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Demolition.Models;

namespace Demolition.Test
{
    public class Helper
    {
        static string _DatabaseXML;

        public static void ResetAll()
        {
            foreach (var file in Directory.GetFiles(Model.PathFromRoot(@"Uploads"), "*.zip"))
                File.Delete(file);

            DeleteAll("Apps", "Demos", "Instances", "Jobs", "Users", "Industries");
        }

        public static void DeleteAll(params string[] tables)
        {
            foreach (var table in tables)
                Model.ExecuteCommand(String.Format("delete from {0}", table));
        }

        public static string DatabaseXML
        {
            get
            {
                if (String.IsNullOrEmpty(_DatabaseXML))
                    _DatabaseXML = new StreamReader(DatabasePath()).ReadToEnd();

                return _DatabaseXML;
            }
        }

        public static string DatabasePath()
        {
            return Model.PathFromRoot(@"..\DemolitionTest\Xml\Database.xml");
        }

        public static string DemoZipPath()
        {
            return Model.PathFromRoot(@"Fixtures\Demo.zip");
        }
    }
}
