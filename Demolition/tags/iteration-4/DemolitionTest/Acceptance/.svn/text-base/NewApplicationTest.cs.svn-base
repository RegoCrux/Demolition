using System;
using System.Collections.Generic;
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
    public class NewApplicationTest : AcceptanceFixture
    {
        string fixturePath = Model.PathFromRoot(@"Fixtures\Demo.zip");

        [Test]
        public void AddADotNETApplication()
        {
            LogOn();
            Browser.Link(Find.ByText("Create New App")).Click();

            Browser.TextField(Find.ByName("Name")).TypeText(".NET Demo Application");
            Browser.FileUpload(Find.ByName("Zip")).Set(fixturePath);
            Browser.Button(Find.ByValue("Submit")).Click();

            Assert.IsTrue(Browser.ContainsText(".NET Demo Application"));
        }

        [Test]
        public void ForgetToAddFiles()
        {
            LogOn();
            Browser.Link(Find.ByText("Create New App")).Click();

            Browser.TextField(Find.ByName("Name")).TypeText(".NET Demo Application");
            Browser.Button(Find.ByValue("Submit")).Click();

            Assert.IsTrue(Browser.ContainsText("Please add a .ZIP file to upload."));
        }

        [Test]
        public void ForgetToAddName()
        {
            LogOn();
            Browser.Link(Find.ByText("Create New App")).Click();

            Browser.Button(Find.ByValue("Submit")).Click();

            Assert.IsTrue(Browser.ContainsText("Please give this application a name."));
        }
    }

    public class NewApplicationFailureTest : AcceptanceFixture
    {
        string fixturePath = Model.PathFromRoot(@"Fixtures\Demo.zip");
        User luke;

        [SetUp]
        public void NewAppFailSetUp()
        {
            luke = User.Create("LukeSkywalker", "leia", "luke@example.com", User.Roles.Salesperson);
            LogOn(luke);
        }

        [Test]
        public void CannotSeeNewAppLinkAsSalesPerson()
        {
            Assert.IsFalse(Browser.ContainsText("Create New App"));
        }

        [Test]
        public void CannotVisitCreateAppsPage()
        {
            Visit("/Apps/Create");
            Assert.IsFalse(Browser.ContainsText("Create New App"));
            Assert.IsTrue(Browser.ContainsText("All Running Demos"));
        }

        [Test]
        public void CannotHackAppsForm()
        {
            // This actually needs multipart/form-data to submit the zip, not going to bother for now
            var formData = new KeyValuePair<string, object>[] {
                new KeyValuePair<string, object>("Name", "HackedApp"),
                new KeyValuePair<string, object>("Zip", new FakeFileWrapper(fixturePath)),
            };

            Visit("/Apps/Create", formData);
            Assert.IsFalse(Browser.ContainsText("Create New App"));
            Assert.IsFalse(Browser.ContainsText("Please add a .ZIP file to upload."));
            Assert.IsTrue(Browser.ContainsText("All Running Demos"));

            var hro = new App();
            hro.Name = "HRO";
            hro.Path = "hro.zip";
            App.Create(hro);

            Visit(string.Format("/Apps/Details/{0}", hro.Id));
            Assert.IsFalse(Browser.ContainsText(hro.Name));
            Assert.IsTrue(Browser.ContainsText("All Running Demos"));
        }
    }
}
