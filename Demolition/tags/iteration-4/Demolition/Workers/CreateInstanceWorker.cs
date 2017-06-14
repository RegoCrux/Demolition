using System;
using System.IO;
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Demolition.Models;
using Tamir.SharpSsh;
using NLog;

namespace Demolition.Workers
{
    public class CreateInstanceWorker : Worker
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public static void Queue(Demo demo)
        {
            Queue(new CreateInstanceWorker(), demo.Id.ToString());
        }

        public override void Work()
        {
            logger.Debug("[CreateInstanceWorker] Starting Work() " + DateTime.Now);

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
                    logger.Debug("[CreateInstanceWorker]Exception " + e.Message);
                    System.Threading.Thread.Sleep(1000);
                }
            }

            ready = false;

            while (!ready)
            {

                try
                {
                    // Find all uploaded apps
                    foreach (var currentInstance in CurrentDemo.Instances)
                    {
                        /* I can't actually run anything right now
                         * I'm not sure what's going on, but this is
                         * the fix for 'closing connections' We need
                         * a new connection for each app since it's 
                         * breaking without that
                         */
                        SshTransferProtocolBase sshCp = new Sftp(publicDNS, "SuperUser");
                        sshCp.Password = "sentprime";
                        logger.Debug("[CreateInstanceWorker]Work SSH client connecting");
                        sshCp.Connect(); 

                        logger.Debug("[CreateInstanceWorker]Work Curren Instance App Name: " 
                            + currentInstance.App.Name);

                        var absolutePath = currentInstance.App.Path; // Not absolute. Only relative.
                        var fileName = Path.GetFileName(absolutePath);
                        var applicationName = currentInstance.App.Name;

                        sshCp.Put(absolutePath, Path.Combine(@"/cygdrive/c/Windows/System32/", fileName));
                        SshExec shell = new SshExec(publicDNS, "SuperUser", "sentprime");
                        shell.Connect();

                        logger.Debug("[CreateInstanceWorker]Work Copying the files on the instance: " + fileName);
                        String moce = shell.RunCommand(String.Format(@"mkdir {0} && cp /cygdrive/c/Windows/System32/{1} {0}", applicationName, fileName));

                        logger.Debug("[CreateInstanceWorker]Work Unzipping " + fileName);
                        String unzip = shell.RunCommand(String.Format("cd {0} && ../../Administrator/7za x ./{1} -y", applicationName, fileName));

                        String listNumSites = shell.RunCommand("../Administrator/appcmd list site");
                        String portIncrement = (8000 + (listNumSites.Length - listNumSites.Replace("SITE", "").Length) / 4).ToString();
                        String cDrive = shell.RunCommand
                            ("/cygdrive/c/Windows/System32/inetsrv/appcmd.exe add site /name:" + applicationName + " /physicalPath:\'c:\\Program Files\\ICW\\home\\SuperUser\\" + applicationName + @"\' /bindings:http://*:" + portIncrement);

                        logger.Debug("[CreateInstanceWorker]Work Starting the app as a web server on IIS.");
                        String test = shell.RunCommand("net start W3SVC");

                        currentInstance.UpdateEC2Url(publicDNS, portIncrement);
                        // Closing the connection for each app
                        sshCp.Close();
                    }
                    
                    ready = true;
                } catch (System.Net.Sockets.SocketException se)
                {
                    logger.Debug("[CreateInstanceWorker]Work SocketException: Instance no ready for SSH client.");
                }
                catch (Exception e) {
                    logger.Debug("[CreateInstanceWorker]Work Unexpected failure: \n" + e.Message);
                    System.Threading.Thread.Sleep(1000);
                }

            }

            CurrentDemo.UpdateState(Demo.States.Ready);
        }
    }
}