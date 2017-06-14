﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Demolition.Models;

namespace Demolition.Test
{
    public class Helper
    {
        public static void ResetAll()
        {
            foreach (var file in Directory.GetFiles(Model.PathFromRoot(@"Uploads"), "*.zip"))
                File.Delete(file);

            DeleteAll("Apps", "Demos", "Instances", "Jobs", "Users");
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