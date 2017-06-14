using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace Demolition.Models
{
    public class RequiredXmlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = (HttpPostedFileBase)value;
            if (file != null && !string.IsNullOrEmpty(file.FileName) && file.ContentLength > 0)
            {
                try
                {
                    var db = Database.Deserialize(new StreamReader(file.InputStream).ReadToEnd());
                    if (db != null)
                    {
                        file.InputStream.Seek(0, SeekOrigin.Begin);
                        return true;
                    }
                }
                catch (InvalidOperationException) { }
            }

            return false;
        }
    }
}