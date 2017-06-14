using System;
using System.IO;
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Demolition.Models;
using Tamir.SharpSsh;

namespace Demolition.Workers
{
    public class CreateInstanceWorker : Worker
    {
        public static void Queue(Demo demo)
        {
            Queue(new CreateInstanceWorker(), demo.Id.ToString());
        }

        public override void Work()
        {
            // Use for debugging with the console
            //System.Diagnostics.Debugger.Launch();
            //System.Diagnostics.Debugger.Break();

            Log("Work", "Starting Work()");

            // http://docs.amazonwebservices.com/sdkfornet/latest/apidocs/html/T_Amazon_EC2_Model_RunInstancesRequest.htm
            RunInstancesRequest instanceRequest = new RunInstancesRequest();
            instanceRequest.ImageId = "ami-fb846892"; // bone crusher
            instanceRequest.MinCount = 1;
            instanceRequest.MaxCount = 1;
            instanceRequest.KeyName = "general";
            instanceRequest.InstanceType = "m1.small";
            instanceRequest.UserData = "";

            RunInstancesResponse rRes = EC2.RunInstances(instanceRequest);

            string reservationID = rRes.RunInstancesResult.Reservation.ReservationId;
            string instanceID = null;
            string publicDNS = null;


            // Get status
            CurrentDemo.UpdateState(Demo.States.Launching);
            DescribeInstancesRequest describeRequest = new DescribeInstancesRequest();
            bool ready = false;

            while (!ready) {
                try
                {
                    DescribeInstancesResponse describeResponse = EC2.DescribeInstances(describeRequest);

                    Reservation res = describeResponse.DescribeInstancesResult.Reservation.Find(r => r.ReservationId == reservationID);

                    RunningInstance running = res.RunningInstance.Find(i => i.PublicDnsName != null);

                    if(running != null && !string.IsNullOrEmpty(running.PublicDnsName))
                    {
                        instanceID = running.InstanceId;
                        publicDNS = running.PublicDnsName;
                        CurrentDemo.UpdateEC2Id(instanceID);
                        ready = true;
                        break;
                    }
                } catch(Exception e)
                {
                    Log("Work", "Amazon instance not ready.");
                    System.Threading.Thread.Sleep(1000);
                }
            }

            ready = false;

            while (!ready)
            {

                try
                {
                    SshTransferProtocolBase sshCp = new Sftp(publicDNS, "SuperUser");

                    sshCp.Password = "sentprime";
                    Log("Work", "SSH client connecting.");
                    sshCp.Connect(); // Add try

                    // Find all uploaded apps
                    foreach (var currentInstance in CurrentDemo.Instances)
                    {
                        Log("Work", "Current Instance App Name: " + currentInstance.App.Name);

                        var absolutePath = currentInstance.App.Path;
                        var fileName = Path.GetFileName(absolutePath);
                        var applicationName = currentInstance.App.Name;

                        sshCp.Put(absolutePath, Path.Combine(@"/cygdrive/c/Windows/System32/", fileName));
                        SshExec shell = new SshExec(publicDNS, "SuperUser", "sentprime");
                        shell.Connect();

                        Log("Work", "Copying the files on the instance: " + fileName);
                        String moce = shell.RunCommand(String.Format(@"mkdir {0} && cp /cygdrive/c/Windows/System32/{1} {0}", applicationName, fileName));

                        Log("Work", "Unzipping " + fileName);
                        String unzip = shell.RunCommand(String.Format("cd {0} && ../../Administrator/7za x ./{1} -y", applicationName, fileName));

                        String listNumSites = shell.RunCommand("../Administrator/appcmd list site");
                        String portIncrement = (8000 + (listNumSites.Length - listNumSites.Replace("SITE", "").Length) / 4).ToString();
                        String cDrive = shell.RunCommand
                            ("/cygdrive/c/Windows/System32/inetsrv/appcmd.exe add site /name:" + applicationName + " /physicalPath:\'c:\\Program Files\\ICW\\home\\SuperUser\\" + applicationName + @"\' /bindings:http://*:" + portIncrement);

                        Log("Work", "Starting the app as a web server on IIS.");
                        String test = shell.RunCommand("net start W3SVC");

                        currentInstance.UpdateEC2Url(publicDNS, portIncrement);
                    }

                    ready = true;
                } catch (Exception e) {
                    Log("Work", "Amazon instance not ready for shell. Sleeping.");
                    System.Threading.Thread.Sleep(1000);
                }

            }

            CurrentDemo.UpdateState(Demo.States.Ready);
        }

        static void Log(string action, object message)
        {
            Console.WriteLine(String.Format("[CreateInstanceWorker] {0} {1}", action, message.ToString()));
        }
    }
}