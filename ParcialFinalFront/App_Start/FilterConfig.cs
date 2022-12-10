using System.Web;
using System.Web.Mvc;

namespace ParcialFinalFront
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new filter.VerifySession());
        }
    }
}
