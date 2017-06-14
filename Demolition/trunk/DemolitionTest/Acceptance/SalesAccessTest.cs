using System;
using System.Collections.Generic;
using Demolition.Models;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    /// <summary>
    /// As a salesperson
    /// I should not be able to access admin functionality
    /// In order to not drive the sysadmin crazy
    /// </summary>
    public class SalesAccessTest : AcceptanceFixture
    {
        App app;

        [SetUp]
        public void LogOnAsSalesPerson()
        {
            app = CreateApp("HRO", "hro.zip");
        }

        [Test]
        public void CannotCreateApps()
        {
            LogOn(CreateSalesperson("Jerry"));
            Visit("/Apps/Create");
            Browser.Refresh();
            BrowserAssert.IsNotOnPage("Create New App");
        }

        [Test]
        public void CannotListApps()
        {
            LogOn(CreateSalesperson("George"));
            Visit("/Apps");
            Browser.Refresh();
            BrowserAssert.IsNotOnPage("All Applications");
        }

        [Test]
        public void CannotEditApp()
        {
            LogOn(CreateSalesperson("Kramer"));
            Visit(string.Format("/Apps/Edit/{0}", app.Id));
            Browser.Refresh();
            BrowserAssert.IsNotOnPage(app.Name);
        }

        [Test]
        public void CannotSeeAppDetails()
        {
            LogOn(CreateSalesperson("Eliane"));
            Visit(string.Format("/Apps/Details/{0}", app.Id));
            Browser.Refresh();
            BrowserAssert.IsNotOnPage(app.Name);
        }

        [Test]
        public void CannotHackAppCreateForm()
        {
            LogOn(CreateSalesperson("Newman"));

            // This actually needs multipart/form-data to submit the zip, not going to bother for now
            var formData = new KeyValuePair<string, object>[] {
                new KeyValuePair<string, object>("Name", "HackedApp"),
                new KeyValuePair<string, object>("Zip", new FakeFileWrapper(Helper.DemoZipPath())),
            };

            Visit("/Apps/Create", formData);
            Browser.Refresh();

            BrowserAssert.IsNotOnPage("Create New App");
            BrowserAssert.IsNotOnPage("Please add a .ZIP file to upload.");
            BrowserAssert.IsOnPage("All Running Demos");
        }
    }
}
