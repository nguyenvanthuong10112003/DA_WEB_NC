using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ProgramWEB.Models;
using ProgramWEB.Define;
using ProgramWEB.Models.Data;
namespace ProgramWEB.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Lay du lieu tu session
            var userSession = Session[DefineSession.userSession];
            if (Session[DefineSession.beforeUrlSession] == null)
                Session.Add(DefineSession.beforeUrlSession, 
                    new string[] { filterContext.RouteData.Values["action"].ToString(),
                        filterContext.RouteData.Values["controller"].ToString()
                    });
            if (userSession == null)
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "User", action = "Login" }));
            base.OnActionExecuting(filterContext);
        }
    }
}