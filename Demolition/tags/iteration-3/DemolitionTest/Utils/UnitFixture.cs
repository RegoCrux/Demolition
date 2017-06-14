#define DEMOLITION_TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using WatiN.Core;


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
