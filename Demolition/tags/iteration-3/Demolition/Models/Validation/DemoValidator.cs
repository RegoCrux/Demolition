using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace Demolition.Models
{
    [Bind(Exclude="Id")]
    public class DemoValidator
    {
        [Required(ErrorMessage = "Please give this demo a name.")]
        public string Name { get; set; }


        [RequireAppsToStart(ErrorMessage = "Please choose at least one app to demo.")]
        public List<App> AppsToStart { get; set; }
    }

    public class RequireAppsToStartAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var appsToStart = (List<App>)value;
            return appsToStart.FindAll(app => app.Selected).Count > 0;
        }
    }
}