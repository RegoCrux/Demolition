using System;
using Demolition.Models;
using Demolition.Workers;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Security.Permissions;
using System.Collections.Generic;
using NLog;
8
namespace DemolitionQueueService
{
    public class QueueRunner
    {
        private bool _running = false;
         
        Logger logger = LogManager.GetCurrentClassLogger();

        public void Start()
        {
            _running = true;

            //System.Diagnostics.Debugger.Launch();
            //System.Diagnostics.Debugger.Break();

            logger.Debug("[QueueRunner]Starting queue loop.");
            while (_running)
            {
                try
                {
                    logger.Debug("[QueueRunner]CHECK: " + DateTime.Now);
                    var jobs = Job.ListAll();

                    if (jobs.Count > 0)
                    {
                        var currentJob = jobs[0];
                        var serializer = Worker.Serializer();
                        var reader = new StringReader(currentJob.Payload);
                        var worker = (Worker)serializer.Deserialize(reader);

                        logger.Debug("[QueueRunner]PERFORM: " + DateTime.Now + " " + worker.ToString());

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
                    logger.Debug("[QueueRunner]Exception: " + DateTime.Now + " \n" + e.Message);
                }
            }
        }


        public void Stop()
        {
            _running = false;
        }

    }
}