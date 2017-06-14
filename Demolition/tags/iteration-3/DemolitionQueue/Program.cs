using System;
using System.IO;
using Demolition.Models;
using Demolition.Workers;

namespace Demolition.Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Log("CHECK", DateTime.Now);
                var jobs = Job.ListAll();

                if (jobs.Count > 0)
                {
                    var currentJob = jobs[0];
                    var serializer = Worker.Serializer();
                    var reader = new StringReader(currentJob.Payload);
                    var worker = (Worker) serializer.Deserialize(reader);


                    Log("PERFORM", worker.ToString());
                    worker.Work();

                    currentJob.Destroy();
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        static void Log(string action, object message)
        {
            Console.WriteLine(String.Format("[DemolitionQueue] {0} {1}", action, message.ToString()));
        }
    }
}
