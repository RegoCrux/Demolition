using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demolition.Models
{
    public partial class Job : Model
    {
        public static IList<Job> ListAll()
        {
            var jobs = from j in GetDataContext().Jobs
                        select j;
            return jobs.ToList();
        }

        public void Save()
        {
            var context = GetDataContext();
            context.Jobs.InsertOnSubmit(this);
            context.SubmitChanges();
        }

        public void Destroy()
        {
            var context = GetDataContext();
            var deadJob = context.Jobs.SingleOrDefault(j => j.ID == this.ID);
            context.Jobs.DeleteOnSubmit(deadJob);
            context.SubmitChanges();
        }
    }
}
