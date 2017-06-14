using System;
using Demolition.Models;
using Demolition.Workers;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Security.Permissions;
using System.Collections.Generic;
using NLog;

namespace DemolitionQueueService
{
    public class QueueRunner
    {
        private bool _running = false;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public static string LogFileName = @"c:/LOGS/QueueLog.txt"; // 

        public void Start()
        {
            _running = true;

            logger.Debug("[QueueRunner]START " + DateTime.Now);

            while (_running)
            {
                try
                {
                    logger.Debug("[QueueRunner]CHECK " + DateTime.Now);

                    var jobs = Job.ListAll();

                    logger.Debug("[QueueRunner] Number of Jobs: " + jobs.Count);

                    if (jobs.Count > 0)
                    {
                        var currentJob = jobs[0];
                        var serializer = Worker.Serializer();
                        var reader = new StringReader(currentJob.Payload);
                        var worker = (Worker)serializer.Deserialize(reader);

                        logger.Debug("[QueueRunner]PERFORM " + DateTime.Now + " " + worker.Data);

                        // todo: add logs that print out which worker/thread is being logged
                        ThreadPool.QueueUserWorkItem(
                                delegate
                                {
                                    worker.Work();
                                }
                            );
                        currentJob.Destroy();
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                catch (Exception e)
                {
                    logger.Debug("[QueueRunner]Exception " + DateTime.Now + " " + e.Message);
                }
            }
        }


        public void Stop()
        {
            _running = false;
        }

    }
}