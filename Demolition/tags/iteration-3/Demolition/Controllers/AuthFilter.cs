using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demolition.Models;

public class AuthFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var nameCookie = context.HttpContext.Request.Cookies["username"];
        var passCookie = context.HttpContext.Request.Cookies["password"];
        User user = null;

        if (nameCookie != null && passCookie != null)
            user = User.Find(nameCookie.Value, passCookie.Value);

        if (user != null)
            context.HttpContext.User = user;
        else
            context.Result = new RedirectResult("/Account/LogOn");
    }
}