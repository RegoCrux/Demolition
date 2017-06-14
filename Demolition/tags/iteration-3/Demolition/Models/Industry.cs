using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demolition.Models
{
    public partial class Industry : Model
    {
        public static IList<Industry> ListAll()
        {
            var industries = from i in GetDataContext().Industries
                             select i;
            return industries.ToList();
        }
    }
}
