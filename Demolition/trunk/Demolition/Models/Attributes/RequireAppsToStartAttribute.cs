using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Demolition.Models
{
    public class RequireAppsToStartAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var appsToStart = (Dictionary<string, bool>)value;
            return appsToStart.All(app => App.Find(app.Key) != null) && appsToStart.Any(app => app.Value);
        }
    }
}