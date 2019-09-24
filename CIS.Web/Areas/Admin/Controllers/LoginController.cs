using CIS.Common;
using CIS.Model.Models;
using CIS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        private IUserService _userService;
        private IUserGroupService _userGroupService;
       

        public LoginController(IUserService userService, IUserGroupService userGroupService)
        {
            
            this._userService = userService;
            this._userGroupService = userGroupService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckAuthen(User entity) {
            var checkUser = _userService.CheckAuthen(entity);
            if (checkUser != null)
            {                
                var userSession = new UserLogin();  // tạo user session
                userSession.UserID = checkUser.ID; // gán userID vào session
                userSession.UserName = checkUser.Username; // gán userName vào session
                userSession.GroupID = checkUser.GroupID; // gán GroupID vào session
                var listCredentials = _userService.GetCredentials(checkUser.GroupID);
                Session.Add(CommonConstant.CREDENTIAL_SESSION, listCredentials); // Gán credentials vào session                
                Session.Add(CommonConstant.USER_SESSION, userSession); // Gán user vào session                
                TempData["Username"] = checkUser.Username;
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout() {
            Session[CommonConstant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}