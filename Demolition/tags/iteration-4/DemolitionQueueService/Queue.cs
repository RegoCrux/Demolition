using System;
using Demolition.Models;
using Demolition.Workers;
using System.IO;

namespace DemolitionQueueService
{
    public class Queue
    {
        private  bool _running = false;
        public static string LogFileName = @"c:/LOGS/QueueLog.txt"; // todo: change

        public  void start()
        {
            _running = true;

            Log("start", "Starting queue loop.");
            while (_running)
            {
                Log("CHECK", DateTime.Now);
                var jobs = Job.ListAll();

                if (jobs.Count > 0)
                {
                    var currentJob = jobs[0];
                    var serializer = Worker.Serializer();
                    var reader = new StringReader(currentJob.Payload);
                    var worker = (Worker)serializer.Deserialize(reader);

                    Log("PERFORM", worker.ToString());
                    worker.Work();

                    currentJob.Destroy();
                }
                else
                {
                    Log("No Job, Sleeping", DateTime.Now);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        public   void stop()
        {
            _running = false;
        }

        void Log(string action, object message)
        {
            TextWriter writer = File.AppendText(LogFileName);
            writer.WriteLine(String.Format("[DemolitionQueue] {0} {1}", action, message.ToString()));
            writer.Close();
        }
    }
}