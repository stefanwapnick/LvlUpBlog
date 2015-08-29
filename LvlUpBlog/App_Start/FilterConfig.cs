using System.Web;
using System.Web.Mvc;
using LvlUpBlog.Infrastructure;

namespace LvlUpBlog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TransactionFilter());       // Add custom transaction filter

            filters.Add(new HandleErrorAttribute());
        }
    }
}