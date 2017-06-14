using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections;
using MbUnit.Framework;
using Demolition.Models;
using Demolition.Test;
using Demolition.Workers;


namespace Demolition.Test
{
    public class JobTest : UnitFixture
    {
        [Test]
        public void DestroyJob()
        {
            var job = new Job();
            job.Payload = "";
            job.CreatedAt = DateTime.Now;
            job.Save();

            Assert.AreEqual(1, Job.ListAll().Count);

            job.Destroy();
            Assert.AreEqual(0, Job.ListAll().Count);
        }


        [Test]
        [Pending]
        public void QueueUp()
        {
            IList<Job> jobs = Job.ListAll();

            // Add to queue
           // CreateInstanceWorker.Queue();
            //Demolition.Jobs.UnzipAppJob.Queue();
            //CreateInstanceWorker.Queue();

            Assert.AreEqual(2+jobs.Count, jobs.Count);

            var currentJob = jobs[0];
            //Assert.AreEqual("CreateInstance", currentJob.Name);
            
            var nextJob = jobs[1];
           // Assert.AreEqual("CreateInstance", nextJob.Name); // 

           // IJob CreateInstanceJob = (IJob)Activator.CreateInstance("Demolition", String.Format("Demolition.Jobs.{0}Job", currentJob.Name)).Unwrap();
            //IJob UnzipAppJob =(IJob)Activator.CreateInstance("Demolition", String.Format("Demolition.Jobs.{0}Job", nextJob.Name)).Unwrap();

            //Assert.IsTrue(CreateInstanceJob.GetType() == (new CreateInstanceJob()).GetType());
            //Assert.IsTrue(UnzipAppJob.GetType() == (new UnzipAppJob()).GetType());

            //job.Perform();

           // Job.DataContext.Jobs.DeleteOnSubmit(currentJob);
            //Job.DataContext.SubmitChanges();

            //Job.DataContext.Jobs.DeleteOnSubmit(nextJob);
            //Job.DataContext.SubmitChanges();

            jobs = Job.ListAll();
            Assert.AreEqual(0, jobs.Count);
        }
    }
}
