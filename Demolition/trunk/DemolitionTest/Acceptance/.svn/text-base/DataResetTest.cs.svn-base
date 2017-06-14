using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using WatiN.Core;
using Demolition.Models;
using System.Text.RegularExpressions;

namespace Demolition.Test
{
    public class DataResetTest : AcceptanceFixture
    {
        string demoName = "Carbonite";

        [SetUp]
        public void SetUpDataReset()
        {
            CreateIndustry();
            CreateApp("App", Helper.DemoZipPath());
            LogOn();
        }

        [Test]
        public void ResetTest()
        {
            Browser.Link(Find.ByText("Run New Demo")).Click(); // goes to Demo/Create which calls Demo.Save
            Browser.TextField("Name").TypeText(demoName);

            Browser.CheckBoxes[0].Click();
            Browser.Button(Find.ByValue("Launch Demo")).Click();

            Demo dummyDemo = Demo.FindByName(demoName);
            Demo.FindByName(demoName).UpdateState(Demo.States.Ready);
            Assert.IsTrue(Demo.FindByName(demoName).IsReady());

            Browser.Link(Find.ByText(new Regex(demoName))).Click();

            BrowserAssert.IsOnPage("Clean");

            ChangeData(demoName);

            Browser.Refresh();

            BrowserAssert.IsOnPage("Dirty");

            ResetData(demoName);

            Browser.Refresh();

            BrowserAssert.IsOnPage("Clean");
        }
    }
}
