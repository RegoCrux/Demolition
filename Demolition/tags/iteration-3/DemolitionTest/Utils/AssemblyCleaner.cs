using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    /// <summary>
    /// http://jamesmckay.net/2009/05/handling-exceptions-in-assembly-level-setup-methods-in-mbunit/
    /// http://groups.google.com/group/MbUnitUser/browse_thread/thread/128229db545b8c8a
    /// </summary>
    [AssemblyFixture]
    class AssemblyCleaner
    {
        [FixtureSetUp]
        public static void SetUp()
        {
            AcceptanceFixture.Browser = new WatiN.Core.IE(Environment.UserName == "Nick");
        }

        [FixtureTearDown]
        public static void TearDown()
        {
            AcceptanceFixture.Browser.Dispose();
        }
    }
}
