using System.Linq;
using System.IO;
using System.Web;
using MbUnit.Framework;
using Demolition.Models;

namespace Demolition.Test
{
    [TestFixture]
    public class AppTest : UnitFixture
    {
        [Test]
        public void SaveAndFindApp()
        {
            App app = new App();
            app.Name = "Optimus";
            app.Zip = new FakeFileWrapper(Helper.DemoZipPath());
            app.Save();

            Assert.IsNotNull(app.Id);
            Assert.IsTrue(File.Exists(Model.PathFromRoot(@"Uploads\Demo.zip")));

            var savedApp = App.Find(app.Id);
            Assert.IsNotNull(savedApp);
            Assert.AreEqual(app.Id, savedApp.Id);
        }

        [Test]
        public void FindNonexistentApp()
        {
            Assert.IsNull(App.Find(1337));
        }
    }
}
