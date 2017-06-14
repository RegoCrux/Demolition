using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Demolition.Models;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    public class SalesDashboardTest : AcceptanceFixture
    {
        User luke, obiwan;
        List<Demo> demos = new List<Demo>();
        List<Instance> instances = new List<Instance>();

        [SetUp]
        public void DashboardSetup()
        {
            luke = CreateSalesperson("Luke");
            obiwan = CreateSalesperson("Obiwan");

            demos.Clear();
            demos.Add(Demo.Create("Shakeys", Demo.States.Ready, luke.Id));
            demos.Add(Demo.Create("Dino BBQ", Demo.States.Booting, luke.Id));
            demos.Add(Demo.Create("Old Toad", Demo.States.ShuttingDown, obiwan.Id));

            var tlo = CreateApp("TLO", "tlo.zip");
            var hro = CreateApp("HRO", "hro.zip");

            instances.Clear();
            instances.Add(CreateInstance(tlo, demos.First()));
            instances.Add(CreateInstance(hro, demos.First()));

            LogOn(luke);
        }

        [Test]
        public void ListOwnDemos()
        {
            foreach (Demo demo in demos)
            {
                var header = Browser.ElementWithTag("h3", Find.ByText(demo.Name));

                if (demo.User.Id == luke.Id)
                {
                    BrowserAssert.IsOnPage(demo.Name);
                    Assert.IsTrue(header.Exists);
                    Assert.AreEqual(demo.User.Name, header.NextSibling.Text);
                    Assert.AreEqual(demo.State, header.NextSibling.NextSibling.Text);
                }
                else
                {
                    Assert.IsFalse(header.Exists);
                }
            }
        }

        [Test]
        public void ViewAnotherSalesRepDemoAndGetKickedBackToDashboard()
        {
            var otherDemo = demos.Last();
            Assert.AreEqual(obiwan.Id, otherDemo.User.Id);
                
            Visit(string.Format("/Demos/Details/{0}", otherDemo.Id));

            BrowserAssert.IsNotOnPage(otherDemo.Name);
            BrowserAssert.IsOnPage("All Running Demos");
        }

        [Test]
        public void ListAllAppsForADemo()
        {
            var demo = demos.First();
            BrowserAssert.IsOnPage(demo.Name);
            Browser.Link(Find.ByText(new Regex(demo.Name))).Click();

            foreach (Instance instance in instances)
            {
                var header = Browser.ElementWithTag("h3", Find.ByText(instance.App.Name));
                Assert.IsTrue(header.Exists);
            }
        }

        [Test]
        public void ViewDemoMode()
        {
            var demo = demos.First();
            BrowserAssert.IsOnPage(demo.Name);
            Browser.Link(Find.ByText(new Regex(demo.Name))).Click();
            Browser.Link(Find.ByText("Demo Mode")).Click();

            BrowserAssert.IsOnPage(string.Format("Single Sign On - {0}", demo.Name));

            Browser.TextField(Find.ByName("Username")).TypeText("Paul");
            Browser.TextField(Find.ByName("Password")).TypeText("spice");
            Browser.Button(Find.ByValue("Sign In")).Click();

            Assert.IsFalse(Browser.ElementWithTag("h1", Find.Any).Exists);

            foreach (Instance instance in instances)
            {
                var link = Browser.Link(Find.ByText(new Regex(instance.App.Name)));

                Assert.IsTrue(link.Exists);
                Assert.AreEqual(instance.EC2Url, link.Url);
            }
        }
    }
}
