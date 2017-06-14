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

            Console.WriteLine("Seeding database...");
            User.Create("Admin", "sentprime", "admin@example.com", User.Roles.Administrator);
            User.Create("Sales", "sentprime", "sales@example.com", User.Roles.Salesperson);

            var app = new App();
            app.Name = "HRO";
            app.Zip = new FakeFileWrapper(Model.PathFromRoot(@"Fixtures\HRO.zip"));
            app.Save();

            var industry = new Industry();
            industry.Name = "Construction";
            industry.Description = "I sleep all night and I work all day";
            industry.CreatedAt = industry.UpdatedAt = DateTime.Now;
            industry.Xml = new FakeFileWrapper(Helper.DatabasePath());
            industry.Save();
        }
    }
}