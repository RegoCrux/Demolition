using System;
using System.IO;
using Demolition.Models;
using Demolition.Workers;
using System.Threading;
using DemolitionQueueService;

namespace Demolition.Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Model.Root = Model.PathFromRoot(@"..\..\..\Demolition");

            QueueRunner runner = new QueueRunner();
            runner.Start();
        }
    }
}
