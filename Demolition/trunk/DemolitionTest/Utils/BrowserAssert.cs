using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    public class BrowserAssert : Assert
    {
        public static void IsOnPage(string text)
        {
            CheckPage(true, text);
        }

        public static void IsNotOnPage(string text)
        {
            CheckPage(false, text);
        }

        static void CheckPage(bool on, string text)
        {
            AreEqual(on, AcceptanceFixture.Browser.ContainsText(text), "{0}", AcceptanceFixture.Browser.Html);
        }
    }
}
