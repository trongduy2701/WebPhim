using WebsitePhimHoatHinh18.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsitePhimHoatHinh18.Areas.Admin.Models
{
    [AdminAuthorize]
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.Session["User"] as Users;
            var role = httpContext.Session["Role"] as string;

            return user != null && role == "Admin";
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "HomeAdmin",
                    action = "Login"
                }));
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Unauthorized"
                };
            }
        }
    }
}