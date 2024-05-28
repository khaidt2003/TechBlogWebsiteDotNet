using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace TechBlogWebsite
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["RoleID"] != null && (int)httpContext.Session["RoleID"] == 2)
            {
                Debug.WriteLine("Tesst admin");
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }
}
