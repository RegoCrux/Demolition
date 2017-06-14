using System.Text.RegularExpressions;
using Demolition.Models;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    /// <summary>
    /// As a sales-person
    /// I want to be able to set up a demo environment tailored to the potential customer
    /// So that they can view the products in a more relevant manner.
    /// </summary>
    [TestFixture]
    public class SetUpDemoTest : AcceptanceFixture
    {
        App payroll, timeoff;

        [SetUp]
        public void SetUpAppSetUp()
        {
            var chewie = User.Create("Chewie", "falcon", "chewie@example.com", User.Roles.Salesperson);
            payroll = CreateApp("Payroll", "payroll.zip");
            timeoff = CreateApp("TimeOff", "timeoff.zip");
            CreateIndustry();
            LogOn(chewie);
        }

        [Test]
        public void SetUpDemoOneAppOnly()
        {
            var demoName = "Carbonite";
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText(demoName);

            Browser.CheckBoxes[0].Click();
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            BrowserAssert.IsOnPage("All Running Demos");
            BrowserAssert.IsOnPage(demoName);
            Assert.AreEqual(1, Job.ListAll().Count);

            Browser.Link(Find.ByText(new Regex(demoName))).Click();
            BrowserAssert.IsOnPage(payroll.Name);
            BrowserAssert.IsNotOnPage(timeoff.Name);
            Assert.IsFalse(Browser.Link(Find.ByText("Demo Mode")).Exists);
        }

        [Test]
        public void SetUpDemoAllApps()
        {
            var demoName = "Lightsaber";
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText(demoName);

            foreach (var checkbox in Browser.CheckBoxes)
                checkbox.Click();
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            BrowserAssert.IsOnPage("All Running Demos");
            BrowserAssert.IsOnPage(demoName);
            Assert.AreEqual(1, Job.ListAll().Count);

            Browser.Link(Find.ByText(new Regex(demoName))).Click();
            BrowserAssert.IsOnPage(payroll.Name);
            BrowserAssert.IsOnPage(timeoff.Name);
            Assert.IsFalse(Browser.Link(Find.ByText("Demo Mode")).Exists);
        }

        [Test]
        public void SetUpDemoNoName()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            BrowserAssert.IsOnPage("Please give this demo a name.");
            Assert.AreEqual(0, Job.ListAll().Count);
        }

        [Test]
        public void SetUpDemoInvalidNameSpaces()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText("Car bonite");
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            BrowserAssert.IsOnPage("Only characters A-Z and 0-9 allowed in Demo Names.");
            Assert.AreEqual(0, Job.ListAll().Count);
        }

        [Test]
        public void SetUpDemoInvalidNameDashes()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText("Car-bonite");
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            BrowserAssert.IsOnPage("Only characters A-Z and 0-9 allowed in Demo Names.");
            Assert.AreEqual(0, Job.ListAll().Count);
        }

        [Test]
        public void SetUpDemoNoApps()
        {
            var demoName = "Force";
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText(demoName);
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            BrowserAssert.IsOnPage("Please choose at least one app to demo.");
            Assert.AreEqual(0, Job.ListAll().Count);
        }
    }
}
