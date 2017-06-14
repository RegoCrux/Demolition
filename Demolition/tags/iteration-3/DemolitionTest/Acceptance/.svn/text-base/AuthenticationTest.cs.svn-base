using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using WatiN.Core;

namespace Demolition.Test
{
    public class AuthenticationTest : AcceptanceFixture
    {
        [Test]
        public void BasicSignInAndOut()
        {
            Visit("/Account/Register");
            Browser.TextField("UserName").TypeText("Jabba");
            Browser.TextField("Email").TypeText("jabba@example.com");
            Browser.TextField("Password").TypeText("sand");
            Browser.TextField("ConfirmPassword").TypeText("sand");
            Browser.Button(Find.ByValue("Register")).Click();

            Browser.TextField("UserName").TypeText("Jabba");
            Browser.TextField("Password").TypeText("sand");
            Browser.Button(Find.ByValue("Log On")).Click();
            Assert.IsTrue(Browser.ContainsText("Jabba"));
            Assert.IsTrue(Browser.ContainsText("All Running Demos"));

            Browser.Link(Find.ByText("Log Off")).Click();
            Assert.IsTrue(Browser.ContainsText("Log On"));
            Assert.IsFalse(Browser.ContainsText("Jabba"));
        }

        [Test]
        public void FailSignInWithBadUserName()
        {
            Browser.TextField("UserName").TypeText("Darth");
            Browser.TextField("Password").TypeText("sith");
            Browser.Button(Find.ByValue("Log On")).Click();

            Assert.IsTrue(Browser.ContainsText("Login was unsuccessful."));
            Assert.IsTrue(Browser.ContainsText("The user name or password provided is incorrect."));
        }

        [Test]
        public void FailSignInWithBadPassword()
        {
            Visit("/Account/Register");
            Browser.TextField("UserName").TypeText("Han");
            Browser.TextField("Email").TypeText("han@example.com");
            Browser.TextField("Password").TypeText("solo");
            Browser.TextField("ConfirmPassword").TypeText("solo");
            Browser.Button(Find.ByValue("Register")).Click();

            Browser.TextField("UserName").TypeText("Han");
            Browser.TextField("Password").TypeText("jabba");
            Browser.Button(Find.ByValue("Log On")).Click();

            Assert.IsTrue(Browser.ContainsText("Login was unsuccessful."));
            Assert.IsTrue(Browser.ContainsText("The user name or password provided is incorrect."));
        }
    }
}
