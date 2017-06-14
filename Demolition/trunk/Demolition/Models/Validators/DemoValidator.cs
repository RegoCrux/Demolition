using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Demolition.Models
{
    [Bind(Exclude="Id")]
    public class DemoValidator
    {
        [Required(ErrorMessage = "Please give this demo a name.")]
        [NameFormat(ErrorMessage = "Only characters A-Z and 0-9 allowed in Demo Names.")]
        public string Name { get; set; }

        [RequireAppsToStart(ErrorMessage = "Please choose at least one app to demo.")]
        public IDictionary<string, bool> AppsToStart { get; set; }
    }
}