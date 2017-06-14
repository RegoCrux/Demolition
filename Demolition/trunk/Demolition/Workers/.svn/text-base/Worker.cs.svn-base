using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using Demolition.Models;
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;


namespace Demolition.Workers
{
    [Serializable]
    public abstract class Worker
    {
        public abstract void Work();
        private static AmazonEC2 _EC2;
        private Demo _Demo;

        public static XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(Worker), new Type[] { typeof(CreateInstanceWorker), typeof(ShutDownDemoWorker), typeof(DataWorker) });
        }

        protected static void Queue(Worker worker, string data)
        {
            worker.Data = data;

            var serializer = Serializer();
            var writer = new StringWriter();
            serializer.Serialize(writer, worker);

            var job = new Job();
            job.Payload = writer.ToString();
            job.CreatedAt = DateTime.Now;
            job.Save();
        }

        public string Data { get; set; }

        public Demo CurrentDemo
        {
            get
            {
                if (_Demo == null)
                {
                    _Demo = Demo.Find(Int32.Parse(Data));
                }
                return _Demo;
            }
        }

        public static AmazonEC2 EC2
        {
            get
            {
                if (_EC2 == null)
                {
                    _EC2 = AWSClientFactory.CreateAmazonEC2Client(Properties.Settings.Default.AWSAccessKey, Properties.Settings.Default.AWSSecretKey);
                }
                return _EC2;
            }
        }
    }
}