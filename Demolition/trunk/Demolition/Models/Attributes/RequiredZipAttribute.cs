using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Demolition.Models
{
    public class RequiredZipAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = (HttpPostedFileBase)value;
						return file != null && !string.IsNullOrEmpty( file.FileName ) && file.ContentLength > 0 && file.FileName.EndsWith(".zip");
        }
    }
}