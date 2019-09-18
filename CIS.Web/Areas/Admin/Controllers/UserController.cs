using AutoMapper;
using CIS.Common;
using CIS.Data;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIS.Web.Infrastructure.Extensions;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        IUserService _userService;
        IUserGroupService _userGroupService;

        public UserController(IUserService userService, IUserGroupService userGroupService)
        {
            this._userService = userService;
            this._userGroupService = userGroupService;
        }

        public ActionResult Index()
        {
            var userModel = _userService.GetAll();
            var userViewModel = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(userModel);
            foreach (var user in userViewModel)
            {
                user.GroupDisplayName = _userGroupService.GetById(user.GroupID).DisplayName;
            }
            return View(userViewModel);
        }
        
        public ActionResult CreateView()
        {
            var listUserGroup = _userGroupService.GetAll();
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            createUserViewModel.UserGroups = Mapper.Map<IEnumerable<UserGroup>, IEnumerable<UGroup>>(listUserGroup);        
            return View(createUserViewModel);
        }


        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            User newUser = new User();
            newUser.UpdateUser(userViewModel);
            _userService.Add(newUser);
            _userService.Save();
            SetAlert("success", newUser.Username + " đã được thêm mới.");
            return RedirectToAction("Index");
        }
    }
}