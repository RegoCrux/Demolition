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
        [SetUp]
        public void SignInAsAdmin()
        {
            LogOn();
        }

        [Test]
        public void AddADotNETApplication()
        {
            Browser.Link(Find.ByText("All Applications")).Click();
            Browser.Link(Find.ByText("Create New App")).Click();

            Browser.TextField(Find.ByName("Name")).TypeText("HRO");
            Browser.TextField(Find.ByName("Description")).TypeText("Version 1");
            Browser.FileUpload(Find.ByName("Zip")).Set(Helper.DemoZipPath());
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("HRO");
            BrowserAssert.IsOnPage("Version 1");
        }

        [Test]
        public void ForgetToAddFiles()
        {
            Browser.Link(Find.ByText("All Applications")).Click();
            Browser.Link(Find.ByText("Create New App")).Click();

            Browser.TextField(Find.ByName("Name")).TypeText("NETDemoApplication");
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Please add a .ZIP file to upload.");
        }

        [Test]
        public void ForgetToAddName()
        {
            Browser.Link(Find.ByText("All Applications")).Click();
            Browser.Link(Find.ByText("Create New App")).Click();

            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Please give this application a name.");
        }

        [Test]
        public void AddInvalidNameSpaces()
        {
            Browser.Link(Find.ByText("All Applications")).Click();
            Browser.Link(Find.ByText("Create New App")).Click();

            Browser.TextField(Find.ByName("Name")).TypeText("Demo Application");
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Only characters A-Z and 0-9 allowed in App Names.");
        }

        [Test]
        public void AddInvalidNameDashes()
        {
            Browser.Link(Find.ByText("All Applications")).Click();
            Browser.Link(Find.ByText("Create New App")).Click();

            Browser.TextField(Find.ByName("Name")).TypeText("Demo-Application");
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Only characters A-Z and 0-9 allowed in App Names.");
        }
    }
}