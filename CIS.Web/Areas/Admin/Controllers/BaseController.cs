using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected void SetAlert(string type, string message)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
                TempData["AlertIcon"] = "glyphicon-ok-sign";
                TempData["AlertTitle"] = "Success";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
        }
    }
}