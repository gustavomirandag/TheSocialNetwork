using System.Web;
using System.Web.Mvc;

namespace TheSocialNetwork.ProfileWebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
