using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Demolition.Models
{
    public class AppValidator
    {
        [Required(ErrorMessage="Please give this application a name.")]
        public string Name { get; set; }

        [RequiredFile(ErrorMessage="Please add a .ZIP file to upload.")]
        public HttpPostedFileBase Zip { get; set; }
    }

    public class RequiredFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = (HttpPostedFileBase)value;
            return file != null && !string.IsNullOrEmpty(file.FileName) && file.ContentLength > 0;
        }
    }
}