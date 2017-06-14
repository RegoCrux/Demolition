using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Configuration;
using System.IO;
using Demolition.Models;
using octalforty.Wizardby.Core.Compiler;
using octalforty.Wizardby.Core.Db;

namespace Demolition.Seeder
{
    class Program
    {
        static void Main(string[] args)
        {
            var streamReader = new StreamReader("database.wdi");
            var scanner = new MdlScanner(new SourceReader(streamReader));
            scanner.RegisterKeyword("deployment");
            scanner.RegisterKeyword("environment");

            Console.WriteLine("Seeding the DB...");

            var db = new DemolitionDataContext("user id=sa;password=sentprime;Data Source=(local)\\sqlexpress;Initial Catalog=demolition_development;Integrated Security=True");

            var u = new User();
            u.Name = "Test";
            u.Email = "test@example.com";
            u.Password = "secret";
            u.Role = "Admin";
            db.Users.InsertOnSubmit(u);
            db.SubmitChanges();

            var users = from user in db.Users
                        select user;

            foreach (var user in users)
            {
                Console.WriteLine("<User#{0}: Name:{1}>",
                    user.Id,
                    user.Name);
            }
        }
    }
}