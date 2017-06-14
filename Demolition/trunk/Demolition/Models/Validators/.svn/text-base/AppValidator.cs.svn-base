using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Demolition.Models
{
    public class AppValidator
    {
        [Required(ErrorMessage="Please give this application a name.")]
        [NameFormat(ErrorMessage="Only characters A-Z and 0-9 allowed in App Names.")]
        public string Name { get; set; }

        [RequiredZip(ErrorMessage="Please add a .ZIP file to upload.")]
        public HttpPostedFileBase Zip { get; set; }
    }
}