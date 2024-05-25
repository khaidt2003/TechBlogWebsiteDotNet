using System.Linq;
using System.Web;
using System.Web.Routing;
using TechBlogWebsite.Models;

public class MenuRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values[parameterName] != null)
        {
            var meta = values[parameterName].ToString();
            // Logic to check meta and determine if it is valid
            var db = new TechBlogDBNetEntities();
            var menu = db.menus.FirstOrDefault(m => m.meta == meta);
            return menu != null;
        }
        return false;
    }
}
