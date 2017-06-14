using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MbUnit.Framework;
using WatiN.Core;
using WatiN.Core.DialogHandlers;
using Demolition.Models;

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
        App payroll;
        App timeoff;
        string demoName = "Carbonite";

        [SetUp]
        public void SetUpAppSetUp()
        {
            var chewie = User.Create("Chewie", "falcon", "chewie@example.com", User.Roles.Salesperson);

            payroll = new App();
            payroll.Name = "Payroll";
            payroll.Path = "payroll.zip";
            App.Create(payroll);

            timeoff = new App();
            timeoff.Name = "TimeOff";
            timeoff.Path = "timeoff.zip";
            App.Create(timeoff);

            LogOn(chewie);
        }

        [Test]
        public void SetUpDemoOneAppOnly()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText(demoName);

            // This is necessary since model binding is horrendously stupid.
            var realId = Browser.TextField(Find.ByValue(payroll.Id.ToString())).Id.Replace("Id", "Selected");
            Browser.CheckBox(realId).Click();
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            Assert.IsTrue(Browser.ContainsText("All Running Demos"));
            Assert.IsTrue(Browser.ContainsText(demoName));
            Assert.AreEqual(1, Job.ListAll().Count);

            Browser.Link(Find.ByText(new Regex(demoName))).Click();
            Assert.IsTrue(Browser.ContainsText(payroll.Name));
            Assert.IsFalse(Browser.ContainsText(timeoff.Name));
            Assert.IsFalse(Browser.Link(Find.ByText("Demo Mode")).Exists);
        }

        [Test]
        public void SetUpDemoAllApps()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText(demoName);

            foreach (var checkbox in Browser.CheckBoxes)
                checkbox.Click();
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            Assert.IsTrue(Browser.ContainsText("All Running Demos"));
            Assert.IsTrue(Browser.ContainsText(demoName));
            Assert.AreEqual(1, Job.ListAll().Count);

            Browser.Link(Find.ByText(new Regex(demoName))).Click();
            Assert.IsTrue(Browser.ContainsText(payroll.Name));
            Assert.IsTrue(Browser.ContainsText(timeoff.Name));
            Assert.IsFalse(Browser.Link(Find.ByText("Demo Mode")).Exists);
        }

        [Test]
        public void SetUpDemoNoName()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            Assert.IsTrue(Browser.ContainsText("Please give this demo a name."));
            Assert.AreEqual(0, Job.ListAll().Count);
        }

        [Test]
        public void SetUpDemoNoApps()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText(demoName);
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            Assert.IsTrue(Browser.ContainsText("Please choose at least one app to demo."));
            Assert.AreEqual(0, Job.ListAll().Count);
        }

        [Test]
        public void ShutDownDemo()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click();
            Browser.TextField("Name").TypeText(demoName);

            foreach (var checkbox in Browser.CheckBoxes)
                checkbox.Click();
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            Assert.IsTrue(Browser.ContainsText("All Running Demos"));
            Assert.IsTrue(Browser.ContainsText(demoName));
            Assert.AreEqual(1, Job.ListAll().Count);

            // Quick hack here to fake out that the last demo was booted
            Demo.ListAll().Last().UpdateState(Demo.States.Ready);
            Browser.Link(Find.ByText(new Regex(demoName))).Click();

            ConfirmDialogHandler handler = new ConfirmDialogHandler();
            using (new UseDialogOnce(Browser.DialogWatcher, handler))
            {
                Browser.Link(Find.ByText("Shut Down Demo")).ClickNoWait();
                handler.WaitUntilExists();
                handler.OKButton.Click();
            }

            Browser.WaitUntilContainsText("All Running Demos");
            Assert.IsTrue(Browser.ContainsText("ShuttingDown"));
            Assert.AreEqual(2, Job.ListAll().Count);

            Browser.Link(Find.ByText(new Regex(demoName))).Click();
            Assert.IsFalse(Browser.Link(Find.ByText("Shut Down Demo")).Exists);
            Assert.IsFalse(Browser.Link(Find.ByText("Demo Mode")).Exists);
        }
    }
}
