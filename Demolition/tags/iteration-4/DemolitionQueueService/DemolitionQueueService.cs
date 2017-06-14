using System;
using System.ServiceProcess;
using System.Threading;
using Demolition.Models;
using NLog;

namespace DemolitionQueueService
{
    public class DemolitionQueueService : ServiceBase
    {
        public static string SERVICE_NAME = "DemolitionQueueService";
        private Thread _thread;
        private QueueRunner queue;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public DemolitionQueueService()
        {
            this.ServiceName = SERVICE_NAME;
            queue = new QueueRunner();
            logger.Debug("[DemolitionQueueService]Instantiate " + DateTime.Now);
        }

        static void Main(string[] args)
        {
            ServiceBase.Run(new DemolitionQueueService());
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            Model.Root = Model.PathFromRoot(@"..\..\..\Demolition");
            logger.Debug("[DemolitionQueueService]ON START PATH" + DateTime.Now + " " + Model.Root);
            _thread = new Thread(queue.Start);
            _thread.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            logger.Debug("[DemolitionQueueService]ON STOP " + DateTime.Now);
            queue.Stop();
        }

    }
}
