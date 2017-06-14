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

        [SetUp]
        public void DashboardSetup()
        {
            luke = User.Create("LukeSkywalker", "leia", "luke@example.com", User.Roles.Salesperson);
            obiwan = User.Create("ObiWanKenobi", "obiwan", "obiwan@example.com", User.Roles.Salesperson);

            demos.Clear();
            demos.Add(Demo.Create("Shakeys", Demo.States.Ready, luke.Id));
            demos.Add(Demo.Create("DinoBBQ", Demo.States.Booting, luke.Id));
            demos.Add(Demo.Create("OldToad", Demo.States.ShuttingDown, obiwan.Id));

            LogOn();
        }

        [Test]
        public void ListAllDemosForAdmins()
        {
            foreach (Demo demo in demos)
            {
                var header = Browser.ElementWithTag("h3", Find.ByText(demo.Name));
                Assert.IsTrue(header.Exists);
                Assert.AreEqual(demo.User.Name, header.NextSibling.Text);
                Assert.AreEqual(demo.State, header.NextSibling.NextSibling.Text);
            }
        }
    }
}
