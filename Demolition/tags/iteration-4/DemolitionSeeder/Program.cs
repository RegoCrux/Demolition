using System;
using System.Configuration;
using Demolition.Models;
using Demolition.Test;

namespace Demolition.Seeder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Clearing database..");

            Model.Root = Model.PathFromRoot(@"..\..\..\Demolition");
            Helper.ResetAll();

            // http://stackoverflow.com/questions/4738/using-configurationmanager-to-load-config-from-an-arbitrary-location
         //   ConfigurationFileMap fileMap = new ConfigurationFileMap(Helper.PathFromRoot("Web.config")); //Path to your config file
         //   Configuration configuration = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);


            Console.WriteLine("Seeding database...");
            User.Create("Admin", "sentprime", "admin@example.com", User.Roles.Administrator);
            User.Create("Sales", "sentprime", "sales@example.com", User.Roles.Salesperson);
        }
    }
}