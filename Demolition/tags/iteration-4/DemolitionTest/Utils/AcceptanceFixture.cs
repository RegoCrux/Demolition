using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Demolition.Models;
using MbUnit.Framework;
using SHDocVw;
using WatiN.Core;

namespace Demolition.Test
{
    [TestFixture]
    public class AcceptanceFixture
    {
        public static IE Browser { get; set; }

        [SetUp]
        public void SetUp()
        {
            Helper.ResetAll();
            Browser.ClearCache();
            Browser.ClearCookies();
            Visit("/Account/LogOn");

            var logoff = Browser.Link(Find.ByText("Log Off"));
            if (logoff.Exists)
                logoff.Click();
        }

        public void LogOn()
        {
            var admin = User.Create("Administrator", "secret", "admin@example.com", User.Roles.Administrator);
            LogOn(admin);
        }

        public void LogOn(User user)
        {
            Browser.TextField("UserName").TypeText(user.Name);
            Browser.TextField("Password").TypeText(user.Password);
            Browser.Button(Find.ByValue("Log On")).Click();
        }

        public void Visit(string relativePath)
        {
            Browser.GoTo(String.Format("http://localhost:8080{0}", relativePath));
        }

        /// <summary>
        /// Code from http://www.kongsli.net/nblog/2008/07/07/sending-post-requests-with-watin/
        /// </summary>
        public void Visit(string relativePath, params KeyValuePair<string, object>[] postData)
        {
            object flags = null;
            object targetFrame = null;
            object headers = "Content-Type: application/x-www-form-urlencoded" + Convert.ToChar(10) + Convert.ToChar(13);
            object postDataBytes = MakeByteStreamOf(postData);
            object resourceLocator = String.Format("http://localhost:8080{0}", relativePath);
            IWebBrowser2 browser = (IWebBrowser2)Browser.InternetExplorer;
            browser.Navigate2(ref resourceLocator, ref flags, ref targetFrame, ref postDataBytes, ref headers);
            Browser.WaitForComplete();
        }

        private static byte[] MakeByteStreamOf(KeyValuePair<string, object>[] postData)
        {
            StringBuilder sb = new StringBuilder();
            if (postData.Length > 0)
            {
                foreach (KeyValuePair<string, object> postDataEntry in postData)
                {
                    sb.Append(postDataEntry.Key).Append('=').Append(postDataEntry.Value).Append('&');
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return ASCIIEncoding.ASCII.GetBytes(sb.ToString());
        }


        public User CreateUser(string name)
        {
            var user = new User();
            user.Name = name;
            user.Email = "user@example.com";
            user.Password = "secret";
            user.Role = "Salesperson";
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            return user;
        }

        public Instance CreateInstance(App app, Demo demo)
        {
            var instance = new Instance();
            instance.AppID = app.Id;
            instance.DemoID = demo.Id;
            instance.EC2State = "Booting";
            instance.DataState = "Clean";
            instance.CreatedAt = DateTime.Now;
            instance.UpdatedAt = DateTime.Now;
            instance.EC2Url = String.Format("http://{0}.{1}.demolition.paychex.com/", app.Name, demo.Name).ToLower();
            instance.Save();
            return instance;
        }
    }
}
