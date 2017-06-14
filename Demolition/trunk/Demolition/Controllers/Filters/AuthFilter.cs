using System.Web.Mvc;
using Demolition.Models;

namespace Demolition
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            User user = null;
            var nameCookie = context.HttpContext.Request.Cookies["username"];
            var passCookie = context.HttpContext.Request.Cookies["password"];

            if (nameCookie != null && passCookie != null)
                user = User.Find(nameCookie.Value, User.DecryptPassword(passCookie.Value, Demolition.Properties.Settings.Default.PasswordEncryptString));

            if (user != null)
                context.HttpContext.User = user;
            else
                context.Result = new RedirectResult("/Account/LogOn");
        }
    }
}