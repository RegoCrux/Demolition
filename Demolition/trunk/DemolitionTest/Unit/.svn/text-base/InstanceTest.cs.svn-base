using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Demolition.Models;

namespace Demolition.Test
{
    public class InstanceTest : UnitFixture
    {
        [Test]
        public void TestUpdateUrl()
        {
            var instance = new Instance();
            instance.EC2State = "Booting";
            instance.CreatedAt = instance.UpdatedAt = DateTime.Now;
            instance.Save();

            instance.UpdateEC2Url("demolition.paychex.com", "8080");

            var newInstance = Instance.Find(instance.Id);
            Assert.AreEqual("http://demolition.paychex.com:8080", instance.EC2Url);
        }
    }
}
