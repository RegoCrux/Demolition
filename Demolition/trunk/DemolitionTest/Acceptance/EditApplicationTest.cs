using System.Text.RegularExpressions;
using Demolition.Models;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    /// <summary>
    /// As an admin
    /// I want to add a new app
    /// In order to allow salespeople to demonstrate it
    /// </summary>
    public class EditApplicationTest : AcceptanceFixture
    {
        App hro, tlo;

        [SetUp]
        public void SignInAsAdmin()
        {
            hro = CreateApp("HRO", "HRO.zip");
            tlo = CreateApp("TLO", "TLO.zip");
            LogOn();
        }

        [Test]
        public void EditingAnApplication()
        {
            Browser.Link(Find.ByText("All Applications")).Click();

            BrowserAssert.IsOnPage("All Applications");
            BrowserAssert.IsOnPage(hro.Name);
            Browser.Link(Find.ByText(new Regex(hro.Name))).Click();

            BrowserAssert.IsOnPage(hro.Name);
            Browser.Link(Find.ByText("Edit Application")).Click();

            Browser.TextField(Find.ByName("Name")).TypeText("Payroll");
            Browser.TextField(Find.ByName("Description")).TypeText("Version 2");
            Browser.FileUpload(Find.ByName("Zip")).Set(Helper.DemoZipPath());
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Payroll");
            BrowserAssert.IsOnPage("Version 2");
            BrowserAssert.IsOnPage("All Applications");
        }

        [Test]
        public void ViewDemosForApplication()
        {
            var user1 = CreateSalesperson("Paul");
            var user2 = CreateSalesperson("Alia");
            var demo1 = Demo.Create("Shakeys", Demo.States.Ready, user1.Id);
            var demo2 = Demo.Create("Hots", Demo.States.Launching, user2.Id);
            CreateInstance(hro, demo1);
            CreateInstance(hro, demo2);

            Browser.Link(Find.ByText("All Applications")).Click();
            Browser.Link(Find.ByText(new Regex(hro.Name))).Click();

            BrowserAssert.IsOnPage("All Applications");
            BrowserAssert.IsOnPage(demo1.Name);
            BrowserAssert.IsOnPage(demo2.Name);
        }
    }
}
