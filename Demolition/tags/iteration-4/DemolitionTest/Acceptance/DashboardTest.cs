using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MbUnit.Framework;
using WatiN.Core;
using Demolition.Models;

namespace Demolition.Test
{
    public class DashboardTest : AcceptanceFixture
    {
        User luke, obiwan;
        List<Demo> demos = new List<Demo>();
        List<Instance> instances = new List<Instance>();

        [SetUp]
        public void DashboardSetup()
        {
            luke = User.Create("LukeSkywalker", "leia", "luke@example.com", User.Roles.Salesperson);
            obiwan = User.Create("ObiWanKenobi", "obiwan", "obiwan@example.com", User.Roles.Salesperson);

            demos.Clear();
            demos.Add(Demo.Create("Shakeys", Demo.States.Ready, luke.Id));
            demos.Add(Demo.Create("Dino BBQ", Demo.States.Booting, luke.Id));
            demos.Add(Demo.Create("Old Toad", Demo.States.ShuttingDown, obiwan.Id));
            
            var tlo = new App();
            tlo.Name = "TLO";
            tlo.Path = "tlo.zip";
            App.Create(tlo);

            var hro = new App();
            hro.Name = "HRO";
            hro.Path = "hro.zip";
            App.Create(hro);
            
            instances.Clear();
            instances.Add(CreateInstance(tlo, demos.First()));
            instances.Add(CreateInstance(hro, demos.First()));
        }

        [Test]
        public void ListAllDemosForAdmins()
        {
            LogOn();

            foreach (Demo demo in demos)
            {
                var header = Browser.ElementWithTag("h3", Find.ByText(demo.Name));
                Assert.IsTrue(header.Exists);
                Assert.AreEqual(demo.User.Name, header.NextSibling.Text);
                Assert.AreEqual(demo.State, header.NextSibling.NextSibling.Text);
            }
        }

        [Test]
        public void ListOwnDemosForSales()
        {
            LogOn(luke);

            foreach (Demo demo in demos)
            {
                var header = Browser.ElementWithTag("h3", Find.ByText(demo.Name));

                if (demo.User.Id == luke.Id)
                {
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
            LogOn(luke);

            var otherDemo = demos.Last();
            Assert.AreEqual(obiwan.Id, otherDemo.User.Id);
                
            Visit(string.Format("/Demos/Details/{0}", otherDemo.Id));

            Assert.IsFalse(Browser.ContainsText(otherDemo.Name));
            Assert.IsTrue(Browser.ContainsText("All Running Demos"));
        }

        [Test]
        public void ListAllAppsForADemo()
        {
            LogOn();
            Browser.Link(Find.ByText(new Regex(demos.First().Name))).Click();

            foreach (Instance instance in instances)
            {
                var header = Browser.ElementWithTag("h3", Find.ByText(instance.App.Name));
                Assert.IsTrue(header.Exists);
            }
        }

        [Test]
        public void ViewDemoMode()
        {
            LogOn();
            var demo = demos.First();
            Browser.Link(Find.ByText(new Regex(demo.Name))).Click();
            Browser.Link(Find.ByText("Demo Mode")).Click();

            Assert.IsTrue(Browser.ContainsText(string.Format("Single Sign On - {0}", demo.Name)));

            Browser.TextField(Find.ByName("Username")).TypeText("Paul Atreides");
            Browser.TextField(Find.ByName("Password")).TypeText("spicemustflow");
            Browser.Button(Find.ByValue("Sign In")).Click();

            Assert.IsFalse(Browser.ElementWithTag("h1", Find.Any).Exists);

            foreach (Instance instance in instances)
            {
                var link = Browser.Link(Find.ByText(new Regex(instance.App.Name)));

                Assert.IsTrue(link.Exists);
                Assert.AreEqual(instance.EC2Url, link.Url);
            }

            Browser.Back();
        }
    }
}
