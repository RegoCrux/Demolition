using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Demolition.Models
{
    [MetadataType(typeof(AppValidator))]
    public partial class App : Model
    {
        public int RealID { get; set; }
        public bool Selected { get; set; }
        public HttpPostedFileBase Zip { get; set; }

        public void Save()
        {
            var paths = new string[] { Root, "Uploads", Zip.FileName };
            var filePath = System.IO.Path.GetFullPath(paths.Aggregate(System.IO.Path.Combine));

            Zip.SaveAs(filePath);
            this.Path = filePath;
            Create(this);
        }

        public static IList<App> ListAll()
        {
            var apps = from a in GetDataContext().Apps
                       select a;
            return apps.ToList();
        }

        public static void Create(App app)
        {
            DateTime now = DateTime.Now;
            app.UpdatedAt = now;
            app.CreatedAt = now;

            var context = GetDataContext();
            context.Apps.InsertOnSubmit(app);
            context.SubmitChanges();
        }

        public static App Find(int id)
        {
            return GetDataContext().Apps.SingleOrDefault(a => a.Id == id);
        }
    }
}
