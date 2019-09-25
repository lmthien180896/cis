using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Web.Controllers
{
    public class BaseController : Controller
    {
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