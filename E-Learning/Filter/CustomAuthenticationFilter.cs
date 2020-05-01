using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace E_Learning.Filter
{
    public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserId"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        void IAuthenticationFilter.OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["userId"])) )
            {
            //    if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["StudentId"])))
            //    {
            //        filterContext.Result = new RedirectToRouteResult(
            //   new RouteValueDictionary
            //   {
            //         { "controller", "Login" },
            //         { "action", "StudentLogin" }
            //   });
            //    }
            //    else
            //    {
                    filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                     { "controller", "Login" },
                     { "action", "TutorLogin" }
               });
                
               
            }
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
                    //new RouteValueDictionary {
                    //{ "controller", "Account" },
                    //{ "action", "Login" } });
            }
            
        }
    }
}