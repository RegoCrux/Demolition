using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Demolition.Models;

namespace Demolition.Test
{
    class Helper
    {
        static Helper()
        {
            // Tests run in DemoTest/bin/Debug, and the Root should be the Demolition web root
            Model.Root = PathFromRoot(@"..\..\..\Demolition");
        }

        public static void ResetAll()
        {
            foreach (var file in Directory.GetFiles(PathFromRoot(@"Uploads"), "*.zip"))
            {
                File.Delete(file);
            }

            DeleteAll("Apps", "Demos", "Instances", "Jobs", "Users");
        }

        public static string PathFromRoot(string path)
        {
            return Path.GetFullPath(Path.Combine(Model.Root, path));
        }

        static void DeleteAll(params string[] tables)
        {
            foreach (var table in tables)
            {
                Model.ExecuteCommand(String.Format("delete from {0}", table));
            }
        }
    }
}
