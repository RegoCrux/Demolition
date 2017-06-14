using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Demolition.Models;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    [TestFixture]
    public class ShutDownDemoTest : AcceptanceFixture
    {
        string demoName = "RebelScum";
        App payroll, timeoff;

        [SetUp]
        public void DemoAlreadyMade()
        {
            var chewie = CreateSalesperson("Chewie");
            payroll = CreateApp("Payroll", "payroll.zip");
            timeoff = CreateApp("TimeOff", "timeoff.zip");
            CreateIndustry();
            Demo.Create(demoName, Demo.States.Ready, chewie.Id);

            LogOn(chewie);
        }

        [Test]
        public void ShutDownDemo()
        {
            var detailsPath = new Uri(Browser.Link(Find.ByText(new Regex(demoName))).Url).PathAndQuery;
            Visit(detailsPath);

            // Hudson hates confirm dialogs. Just submit a post request.
            var link = Browser.Link(Find.ByText("Shut Down Demo"));
            Visit(new Uri(link.Url).PathAndQuery, new KeyValuePair<string, object> { });
            Visit(MvcApplication.DefaultRoute);
            Assert.AreEqual(1, Job.ListAll().Count);

            var demos = Demo.ListAll();
            Visit(detailsPath);
            Browser.Refresh();
            BrowserAssert.IsNotOnPage("Reset Data");
            BrowserAssert.IsNotOnPage("Shut Down Demo");
            BrowserAssert.IsNotOnPage("Demo Mode");
        }
    }
}
