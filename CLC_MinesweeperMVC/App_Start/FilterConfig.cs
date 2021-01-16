using System.Web;
using System.Web.Mvc;

namespace CLC_MinesweeperMVC {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
