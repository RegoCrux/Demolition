using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Text;
using System.IO;
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using System.Collections.Specialized;
using System.Configuration;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Tamir.SharpSsh;

namespace Demolition.Controllers
{
    public class MonitorController : Controller
    {
        //
        // For more inspiration, see http://developer.amazonwebservices.com/connect/entry.jspa?externalID=3051
        // API docs: http://docs.amazonwebservices.com/sdkfornet/latest/apidocs/Index.html
        // More: http://aws.amazon.com/documentation/


        // Cloudwatch fun: http://docs.amazonwebservices.com/AmazonCloudWatch/latest/DeveloperGuide/

        public ActionResult Index()
        {
            NameValueCollection appConfig = ConfigurationManager.AppSettings;

            #region whatever
           
            #endregion



            #region ec2 instances

            // Print the number of Amazon EC2 instances.
                AmazonEC2 ec2 = AWSClientFactory.CreateAmazonEC2Client(
                    appConfig["AWSAccessKey"],
                    appConfig["AWSSecretKey"]
                    );
                DescribeInstancesRequest ec2Request = new DescribeInstancesRequest();

                try
                {
                    
                    DescribeInstancesResponse ec2Response = ec2.DescribeInstances(ec2Request);
                    int numInstances = 0;
                    numInstances = ec2Response.DescribeInstancesResult.Reservation.Count;

                    ViewData["EC2Instances"] = numInstances + "\n";

                    numInstances = ec2Response.DescribeInstancesResult.Reservation.Count;
                    System.Collections.Generic.List<Reservation> jenlist = ec2Response.DescribeInstancesResult.Reservation;
                    List<RunningInstance> currentInstances;
                    List<String> JenBullet = new List<String>();
                    foreach (Reservation inst in jenlist)
                    {
                        currentInstances = inst.RunningInstance;
                        foreach (RunningInstance curInst in currentInstances)
                        {
                            JenBullet.Add(curInst.PublicDnsName);
                            
                        }
                    }
                    
                }
                catch (AmazonEC2Exception ex)
                {
                    if (ex.ErrorCode != null && ex.ErrorCode.Equals("AuthFailure"))
                    {
                        ViewData["ErrorData"] = "The account you are using is not signed up for Amazon EC2.\n";
                    }
                    else
                    {
                        string errorMessage = "Caught Exception: " + ex.Message + "\n"
                            + "Response Status Code: " + ex.StatusCode + "\n"
                            + "Error Code: " + ex.ErrorCode + "\n"
                            + "Error Type: " + ex.ErrorType + "\n"
                            + "Request ID: " + ex.RequestId + "\n"
                            + "XML: " + ex.XML + "\n";
                        ViewData["ErrorData"] = errorMessage;
                    }
                }
            #endregion
                #region simple db
                // Print the number of Amazon SimpleDB domains.
                AmazonSimpleDB sdb = AWSClientFactory.CreateAmazonSimpleDBClient(
                    appConfig["AWSAccessKey"],
                    appConfig["AWSSecretKey"]
                    );
                ListDomainsRequest sdbRequest = new ListDomainsRequest();

                try
                {
                    ListDomainsResponse sdbResponse = sdb.ListDomains(sdbRequest);

                    if (sdbResponse.IsSetListDomainsResult())
                    {
                        int numDomains = 0;
                        numDomains = sdbResponse.ListDomainsResult.DomainName.Count;
                        ViewData["SimpleDBDomains"] = numDomains + "\n";
                    }
                }
                catch (AmazonSimpleDBException ex)
                {
                    if (ex.ErrorCode != null && ex.ErrorCode.Equals("AuthFailure"))
                    {
                        ViewData["ErrorData"] = "The account you are using is not signed up for Amazon SimpleDB.\n";
 
                    }
                    else
                    {
                        string errorMessage = "Caught Exception: " + ex.Message + "\n"
                            + "Response Status Code: " + ex.StatusCode + "\n"
                            + "Error Code: " + ex.ErrorCode + "\n"
                            + "Error Type: " + ex.ErrorType + "\n"
                            + "Request ID: " + ex.RequestId + "\n"
                            + "XML: " + ex.XML + "\n";
                        ViewData["ErrorData"] = errorMessage;
                    }
                }
                #endregion simple db
                #region buckets

                // Print the number of Amazon S3 Buckets.
                AmazonS3 s3Client = AWSClientFactory.CreateAmazonS3Client(
                    appConfig["AWSAccessKey"],
                    appConfig["AWSSecretKey"]
                    );


                try
                {
                    ListBucketsResponse response = s3Client.ListBuckets();
                    int numBuckets = 0;
                    if (response.Bucket != null &&
                        response.Bucket.Count > 0)
                    {
                        numBuckets = response.Bucket.Count;
                    }
                    ViewData["EC2Buckets"] = numBuckets + "\n";
                }
                catch (AmazonS3Exception ex)
                {
                    if (ex.ErrorCode != null && (ex.ErrorCode.Equals("InvalidAccessKeyId") ||
                        ex.ErrorCode.Equals("InvalidSecurity")))
                    {
                        ViewData["ErrorData"] = "Please check the provided AWS Credentials.\n";
                    }
                    else
                    {
                        string errorMessage = "Caught Exception: " + ex.Message + "\n"
                           + "Response Status Code: " + ex.StatusCode + "\n"
                           + "Error Code: " + ex.ErrorCode + "\n"
                           + "Request ID: " + ex.RequestId + "\n"
                           + "XML: " + ex.XML + "\n";
                        ViewData["ErrorData"] = errorMessage;
                    }
                }

                #endregion buckets
                return View();
        }

        public ActionResult Delete(String id)
        {
            NameValueCollection appConfig = ConfigurationManager.AppSettings;
            AmazonEC2 ec2 = AWSClientFactory.CreateAmazonEC2Client(
                    appConfig["AWSAccessKey"],
                    appConfig["AWSSecretKey"]
                    );

            ViewData["inst"] = id;
            StopInstancesRequest sTest = new StopInstancesRequest();
            String[] instancesToStop = new String[1];
            instancesToStop[0] = id;
            sTest.WithInstanceId(instancesToStop);
            ec2.StopInstances(sTest);

            //showing some details of the 'stop' procedure
            this.List();
            return View();
        }
        public ActionResult List()
        {   
            NameValueCollection appConfig = ConfigurationManager.AppSettings;
            AmazonEC2 ec2 = AWSClientFactory.CreateAmazonEC2Client(
                    appConfig["AWSAccessKey"],
                    appConfig["AWSSecretKey"]
                    );
            DescribeInstancesRequest ec2Request = new DescribeInstancesRequest();
            List<RunningInstance> JenBullet = null;
            try
            {

                DescribeInstancesResponse ec2Response = ec2.DescribeInstances(ec2Request);
                int numInstances = 0;
                numInstances = ec2Response.DescribeInstancesResult.Reservation.Count;
                //Reservation res = ec2Response.DescribeInstancesResult.Reservation.
                List<Reservation> rList = new List<Reservation>();
                //rList.Add(res);
                //ec2Response.DescribeInstancesResult.WithReservation(res);

                ViewData["EC2Instances"] = numInstances + "\n";
                    
                System.Collections.Generic.List<Reservation> jenlist = ec2Response.DescribeInstancesResult.Reservation;
                List<RunningInstance> currentInstances;
                JenBullet = new List<RunningInstance>();
                foreach (Reservation inst in jenlist)
                {
                    currentInstances = inst.RunningInstance;
                    foreach (RunningInstance curInst in currentInstances)
                    {
                        JenBullet.Add(curInst);
                        //curInst.
                    }
                }
                ViewData["myList"] = JenBullet;

                /* TEST STOP */
                DescribeImagesRequest dReq = new DescribeImagesRequest();
                List<String> owners = new List<String>();
                owners.Add("513635588618");
               // dReq.Owner = owners;
                DescribeImagesResponse dRes= ec2.DescribeImages(dReq);
                DescribeImagesResult dImg = dRes.DescribeImagesResult;
                List<Amazon.EC2.Model.Image> amis = dImg.Image;
                List<String> amiInfo = new List<String>();
                foreach (Amazon.EC2.Model.Image im in amis)
                {
                   amiInfo.Add(im.Name + " " + im.Description);
                    //im.
                    
                }

                ViewData["ami"] = amiInfo;

                /*RunInstancesRequest rTest = new RunInstancesRequest();
                rTest.ImageId = "ami-a3ab47ca";
                rTest.MinCount = 1;
                rTest.MaxCount = 1;
                rTest.KeyName = "jen";
                rTest.InstanceType = "m1.small";

                RunInstancesResponse rRes = ec2.RunInstances(rTest);
                ViewData["Response"] = rRes.ResponseMetadata.ToString();
                
                StopInstancesRequest sTest = new StopInstancesRequest();
                String[] instancesToStop = new String[1];
                instancesToStop[0] = "i-d9c461b2";
                sTest.WithInstanceId(instancesToStop);
                ec2.StopInstances(sTest);*/
         
            }
            catch (AmazonEC2Exception ex)
            {
                if (ex.ErrorCode != null && ex.ErrorCode.Equals("AuthFailure"))
                {
                    ViewData["ErrorData"] = "The account you are using is not signed up for Amazon EC2.\n";
                }
                else
                {
                    string errorMessage = "Caught Exception: " + ex.Message + "\n"
                        + "Response Status Code: " + ex.StatusCode + "\n"
                        + "Error Code: " + ex.ErrorCode + "\n"
                        + "Error Type: " + ex.ErrorType + "\n"
                        + "Request ID: " + ex.RequestId + "\n"
                        + "XML: " + ex.XML + "\n";
                    ViewData["ErrorData"] = errorMessage;
                }
            }
            
            
            return View();
        }

        public ActionResult DetailedView()
        {
            try
            {
                NameValueCollection appConfig = ConfigurationManager.AppSettings;
                AmazonEC2 ec2 = AWSClientFactory.CreateAmazonEC2Client(
                        appConfig["AWSAccessKey"],
                        appConfig["AWSSecretKey"]
                        );
                RunInstancesRequest rTest = new RunInstancesRequest();
                rTest.ImageId = "ami-fb846892";
                rTest.MinCount = 1;
                rTest.MaxCount = 1;
                rTest.KeyName = "jen";
                rTest.InstanceType = "m1.small";

                RunInstancesResponse rRes = ec2.RunInstances(rTest);
                RunInstancesResult instance = rRes.RunInstancesResult;

                string reservationID = rRes.RunInstancesResult.Reservation.ReservationId;
                string instanceID = null;


                // Get status
                System.Threading.Thread.Sleep(200000);
                DescribeInstancesRequest describeRequest = new DescribeInstancesRequest();

                try
                {
                    DescribeInstancesResponse describeResponse = ec2.DescribeInstances(describeRequest);

                    foreach (Reservation res in describeResponse.DescribeInstancesResult.Reservation)
                    {
                        if (res.ReservationId == reservationID)
                        {
                            foreach (RunningInstance running in res.RunningInstance)
                            {
                                instanceID = running.PublicDnsName;
                                break;
                            }
                        }

                    } // outer foreach

                }
                catch (Exception e)
                {
                    // todo handle
                }

                ViewData["Response"] = rRes.ResponseMetadata.ToString();
                ViewData["SSH"] = instanceID;
                System.Threading.Thread.Sleep(500000);

                /* $ ./AppCmd.exe list site
                SITE “TestSite” (id:1,bindings:http/*:80:,state:Started)

                $ ./AppCmd.exe delete site TestSite
                SITE object “TestSite” deleted

                $ ./AppCmd.exe add site /name:TestSite /physicalPath:“C:\demolition\TestSite” /bindings:http://*:80
                SITE object “TestSite” added
                APP object “TestSite/” added
                VDIR object “TestSite/” added
                 */
                
                SshTransferProtocolBase sshCp = new Sftp(instanceID, "SuperUser");

                sshCp.Password = "sentprime";

                sshCp.Connect();
                //Put a zip file that exists here...this is obviously one I had....
                sshCp.Put("HRO.zip", "/cygdrive/c/Windows/System32/HRO.zip");
                SshExec shell = new SshExec(instanceID, "SuperUser", "sentprime");
                shell.Connect();
                
                //String output = shell.RunCommand("ls");
                String moce = shell.RunCommand("cp /cygdrive/c/Windows/System32/HRO.zip .");
                String unzip = shell.RunCommand("../Administrator/7za x HRO.zip -y");
                //String remo = shell.RunCommand
                String cDrive = shell.RunCommand("/cygdrive/c/Windows/System32/inetsrv/appcmd.exe add site /name:HRO /physicalPath:\"c:\\Program Files\\ICW\\home\\SuperUser\\HRO\" /bindings:http://*:80");
                String test = shell.RunCommand("net start W3SVC");
                
                List<String> listOfConnections = new List<String>();
                listOfConnections.Add(shell.Host);
                listOfConnections.Add("Jen test");
                ViewData["SSH"] = test;
                 
            }
            catch (Exception e)
            {
                ViewData["SSH"] = e.Message;
                Console.WriteLine(e.Message);
            }
            
            return View();
        }
        
    }
}
