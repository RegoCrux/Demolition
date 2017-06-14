using System;
using MbUnit.Framework;
using Demolition.Models;

namespace Demolition.Test
{
    public class IndustryTest : UnitFixture
    {
        [Test]
        public void TestPayload()
        {
            var db = Database.Deserialize(Helper.DatabaseXML);

            var industry = new Industry();
            industry.Name = "Construction";
            industry.Description = "I sleep all night and I work all day";
            industry.CreatedAt = industry.UpdatedAt = DateTime.Now;
            industry.Xml = new FakeFileWrapper(Helper.DatabasePath());
            industry.Save();

            Assert.AreEqual(db.Name, industry.Database.Name);
            Assert.AreEqual(db.Tables.Count, industry.Database.Tables.Count);
        }
    }
}
