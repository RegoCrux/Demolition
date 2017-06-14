using Demolition.Models;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    public class DataIntegrationTest : AcceptanceFixture
    {
        [SetUp]
        public void SignInAsAdmin()
        {
            LogOn();
        }

        [Test]
        public void AddNewIndustryTest()
        {
            Browser.Link(Find.ByText("Add New Industry")).Click();

            Browser.TextField(Find.ByName("Name")).TypeText("Construction");
            Browser.TextField(Find.ByName("Description")).TypeText("Workin' all night!");
            Browser.FileUpload(Find.ByName("Xml")).Set(Helper.DatabasePath());
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Construction");
        }

        [Test]
        public void AddNewIndustryWithErrors()
        {
            Browser.Link(Find.ByText("Add New Industry")).Click();

            Browser.TextField(Find.ByName("Description")).TypeText("Workin' all night!");
            Browser.FileUpload(Find.ByName("Xml")).Set(Helper.DatabasePath());
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Add New Industry");
            BrowserAssert.IsOnPage("Please give this industry a name");

            Browser.TextField(Find.ByName("Name")).TypeText("Construction");
            Browser.TextField(Find.ByName("Description")).Clear();
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Add New Industry");
            BrowserAssert.IsOnPage("Please give this industry a description");

            Browser.TextField(Find.ByName("Description")).TypeText("Workin' all night!");
            Browser.Button(Find.ByValue("Submit")).Click();

            BrowserAssert.IsOnPage("Add New Industry");
            BrowserAssert.IsOnPage("Please add a valid .XML file to upload");

            var badPath = Model.PathFromRoot(@"..\DemolitionTest\Xml\BadDatabase.xml");
            Browser.FileUpload(Find.ByName("Xml")).Set(badPath);
            Browser.Button(Find.ByValue("Submit")).Click();
            BrowserAssert.IsOnPage("Please add a valid .XML file to upload");
        }

    }
}
