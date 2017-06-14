using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demolition.Models
{
    public partial class Instance : Model
    {
        public void UpdateEC2Url(string host, string port)
        {
            this.EC2Url = String.Format("http://{0}:{1}", host, port);

            var context = GetDataContext();
            var updatedInstance = context.Instances.SingleOrDefault(i => i.Id == this.Id);
            updatedInstance.EC2Url = this.EC2Url;
            context.SubmitChanges();
        }

        public void Save()
        {
            var context = GetDataContext();
            context.Instances.InsertOnSubmit(this);
            context.SubmitChanges();
        }

        public static IList<Instance> ListAll()
        {
            var instances = from i in GetDataContext().Instances
                             select i;
            return instances.ToList();
        }

        public static Instance Find(int id)
        {
            return GetDataContext().Instances.SingleOrDefault(a => a.Id == id);
        }
    }
}
