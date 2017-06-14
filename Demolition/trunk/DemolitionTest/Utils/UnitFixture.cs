using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using WatiN.Core;
using System.IO;
using Demolition.Models;


namespace Demolition.Test
{
    [TestFixture]
    public class UnitFixture
    {
        [SetUp]
        public void SetUp()
        {
            Helper.ResetAll();
        }
    }
}
