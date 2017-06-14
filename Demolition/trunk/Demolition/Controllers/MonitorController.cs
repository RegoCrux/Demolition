using System.Collections.Generic;
using System.Web.Mvc;
using Amazon.EC2.Model;
using Demolition.Workers;
using System.Linq;

namespace Demolition
{
    [AuthFilter(Order = 0)]
    [AdminFilter(Order = 1)]
    public class MonitorController : Controller
    {
        public ActionResult Index()
        {
            var instances = new List<RunningInstance>();

            var ec2Request = new DescribeInstancesRequest();
            var ec2Response = Worker.EC2.DescribeInstances(ec2Request);
            var reservations = ec2Response.DescribeInstancesResult.Reservation;

            foreach (Reservation res in reservations)
                foreach (RunningInstance curInst in res.RunningInstance)
                    instances.Add(curInst);

            return View(instances.OrderBy(i => i.InstanceState.Name).ToList());
        }
    }
}
