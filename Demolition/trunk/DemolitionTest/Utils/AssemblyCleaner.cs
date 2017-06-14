using System;
using System.Diagnostics;
using System.IO;
using Demolition.Models;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    /// <summary>
    /// http://jamesmckay.net/2009/05/handling-exceptions-in-assembly-level-setup-methods-in-mbunit/
    /// http://groups.google.com/group/MbUnitUser/browse_thread/thread/128229db545b8c8a
    /// </summary>
    [AssemblyFixture]
    class AssemblyCleaner
    {
        static Process server;

        [FixtureSetUp]
        public static void SetUp()
        {
            Model.Root = Model.PathFromRoot(@"..\..\..\Demolition");
            Model.Test = true;
            
            var webServerExePath = Path.Combine(Environment.GetEnvironmentVariable("PROGRAMFILES"), @"Common Files\microsoft shared\DevServer\9.0\WebDev.WebServer.exe");
            
            // Only start the server if it exists, otherwise let IIS run it
            if(File.Exists(webServerExePath))
                server = Process.Start(webServerExePath, String.Format("/port:{0} /path:\"{1}\"", 8080, Model.Root));

            AcceptanceFixture.Browser = new IE(Environment.UserName == "Nick");
        }

        [FixtureTearDown]
        public static void TearDown()
        {
            AcceptanceFixture.Browser.Dispose();

            // Only kill the server if we started it!
            if(server != null)
                server.Kill();
        }
    }
}
