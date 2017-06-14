using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Demolition.Models
{
    [Bind(Exclude="Id")]
    public class IndustryValidator
    {
        [Required(ErrorMessage = "Please give this industry a name.")]
        [NameFormat(ErrorMessage = "Only characters A-Z and 0-9 allowed in Industry Names.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please give this industry a description.")]
        public string Description { get; set; }

        [RequiredXml(ErrorMessage = "Please add a valid .XML file to upload.")]
        public HttpPostedFileBase Xml { get; set; }
    }
}