using System;
using Amazon.EC2.Model;
using Demolition.Models;

namespace Demolition.Workers
{
    public class ShutDownDemoWorker : Worker
    {
        public static void Queue(Demo demo)
        {
            Queue(new ShutDownDemoWorker(), demo.Id.ToString());
        }

        public override void Work()
        {
            var stopRequest = new StopInstancesRequest();
            stopRequest.WithInstanceId(new string [] { CurrentDemo.EC2Id.ToString() } );
            EC2.StopInstances(stopRequest);

            // TODO: Spin and wait until instance is terminated?

            CurrentDemo.UpdateState(Demo.States.Terminated);
        }
    }
}