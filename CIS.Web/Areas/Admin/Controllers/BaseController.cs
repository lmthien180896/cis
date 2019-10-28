using CIS.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected int groupId;

        public string currentUserName;        

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            else
            {
                currentUserName = session.UserName;
                DisplayUser(session.UserName);
                groupId = session.GroupID;
            }
            base.OnActionExecuting(filterContext);
        }

        protected void DisplayUser(string userName)
        {
            TempData["UserName"] = userName;          
        }

        protected void SetAlert(string type, string message)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "toast-success";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "toast-error";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "toast-warning";
            }
            else if (type == "info")
            {
                TempData["AlertType"] = "toast-info";
            }
        }

    }
}