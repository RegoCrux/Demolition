using System.Web.Mvc;
using Demolition.Models;

namespace Demolition
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.IsInRole(Properties.Settings.Default.AdministratorRole))
                context.Result = new RedirectResult(MvcApplication.DefaultRoute);
        }
    }
}