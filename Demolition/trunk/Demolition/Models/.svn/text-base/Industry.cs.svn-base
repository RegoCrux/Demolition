using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Demolition.Models
{
    [MetadataType(typeof(IndustryValidator))]
    public partial class Industry : Model
    {
        private Database _Database;

        public static IList<Industry> ListAll()
        {
            var industries = from i in GetDataContext().Industries
                             select i;
            return industries.ToList();
        }

        public void Save()
        {
            this.Payload = new StreamReader(Xml.InputStream).ReadToEnd();
            this.CreatedAt = this.UpdatedAt = DateTime.Now;

            var context = GetDataContext();
            context.Industries.InsertOnSubmit(this);
            context.SubmitChanges();
        }

        public void Update(string json)
        {
            this.UpdatedAt = DateTime.Now;
            this.Payload = Database.Parse(json).Serialize();

            var context = GetDataContext();
            var updatedIndustry = context.Industries.SingleOrDefault(i => i.Id == this.Id);
            updatedIndustry.Payload = this.Payload;
            context.SubmitChanges();
        }

        public static Industry Find(int id)
        {
            return GetDataContext().Industries.SingleOrDefault(a => a.Id == id);
        }

        public static Industry Find(string name)
        {
            return GetDataContext().Industries.SingleOrDefault(a => a.Name == name);
        }

        public HttpPostedFileBase Xml { get; set; }

        public Database Database
        {
            get
            {
                if (_Database == null)
                    _Database = Database.Deserialize(Payload);

                return _Database;
            }
        }
    }
}
