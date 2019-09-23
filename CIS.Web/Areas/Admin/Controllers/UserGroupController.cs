using AutoMapper;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class UserGroupController : Controller
    {
        private IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            this._userGroupService = userGroupService;

        }

        public ActionResult Index()
        {
            var listUserGroup = _userGroupService.GetAll();
            var listUserGroupViewModel = Mapper.Map<IEnumerable<UserGroup>, IEnumerable<UserGroupViewModel>>(listUserGroup);
            return View(listUserGroupViewModel);
        }
    }
}