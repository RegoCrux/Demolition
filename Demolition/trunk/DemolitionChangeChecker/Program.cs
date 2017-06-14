using System;
using Demolition.Models;
using Demolition.Workers;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Security.Permissions;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;

namespace Demolition.ChangeChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Model.Root = Model.PathFromRoot(@"..\..\..\Demolition");

            var checkIt = new ChangeChecker();
            checkIt.Start();
        }
    }

    class ChangeChecker
    {
        bool _running = false;
        const int ONE_MINUTE = 60000;

        public void Start()
        {
            _running = true;

            while (_running)
            {
                Log("Checking demos");
                checkForUpdates();
                System.Threading.Thread.Sleep(ONE_MINUTE);
            }
        }

        void Log(string message)
        {
            Console.WriteLine("[{0}] {1}", DateTime.Now, message);
        }

        /// <summary>
        /// Compare last time the database on the instance was updated to UpdatedAt time on instance
        /// </summary>
        private void checkForUpdates()
        {
            foreach (var demo in Demo.ListAll())
            {
                if (!demo.IsReady())
                    continue;

                Log("Demo ready: " + demo.Name);

                try
                {
                    var connectionString = String.Format("User ID=sa;Password=sentprime;Data Source={0};",
                        new Uri(demo.Instances.First().EC2Url).Host);

                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        var myCommand = new SqlCommand(Demo.CHECKSUM_QUERY, conn);
                        var checksum = (int)myCommand.ExecuteScalar();

                        if (demo.Checksum != checksum)
                        {
                            Log("Demo dirty: " + demo.Name);
                            demo.UpdateDataState(Demo.DataStates.Dirty);
                        }
                        else
                        {
                            Log("Demo clean: " + demo.Name);
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Stop()
        {
            _running = false;
        }
    }
}
