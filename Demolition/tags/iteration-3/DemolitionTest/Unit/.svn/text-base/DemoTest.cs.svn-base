using System.Data.Linq;
using System.Linq;
using Demolition.Models;
using MbUnit.Framework;

namespace Demolition.Test
{
    public class DemoTest : UnitFixture
    {
        [Test]
        public void StatusColorShouldMatchState()
        {
            var demo = new Demo();

            demo.State = Demo.States.Ready.ToString();
            Assert.AreEqual("green", demo.StatusColor());

            demo.State = Demo.States.Booting.ToString();
            Assert.AreEqual("yellow", demo.StatusColor());

            demo.State = Demo.States.Launching.ToString();
            Assert.AreEqual("yellow", demo.StatusColor());

            demo.State = Demo.States.ShuttingDown.ToString();
            Assert.AreEqual("red", demo.StatusColor());

            demo.State = Demo.States.Terminated.ToString();
            Assert.AreEqual("red", demo.StatusColor());
        }

        [Test]
        public void CreateDemo()
        {
            var demo = Demo.Create("Wegmans", Demo.States.Ready, 1337);

            Assert.IsNotNull(demo.Id);
            Assert.AreEqual(demo.State, "Ready");
            Assert.AreEqual(demo.UserID, 1337);
        }

        [Test]
        public void FindDemo()
        {   
            var demo = Demo.Create("Five Guys", Demo.States.ShuttingDown, 42);

            var foundDemo = Demo.Find(demo.Id);
            Assert.IsNotNull(foundDemo);
            Assert.AreEqual(demo.Id, foundDemo.Id);
        }

        [Test]
        public void FindNonexistentApp()
        {
            Assert.IsNull(Demo.Find(1));
        }
    }
}
