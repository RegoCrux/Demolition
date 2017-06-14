using System.Web.Mvc;
using Amazon.EC2.Model;

namespace Demolition
{
    public static class EC2Helper
    {
        public static string EC2Color(this HtmlHelper helper, RunningInstance instance)
        {
            if (instance.InstanceState.Name.Equals("terminated"))
                return "red";
            else
                return "green";
        }
    }
}