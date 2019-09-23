using AutoMapper;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Infrastructure.Extensions;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        private IUserGroupService _userGroupService;

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

        public ActionResult EditView(int id)
        {
            var userModel = _userService.GetById(id);
            UserViewModel userViewModel = new UserViewModel();
            userViewModel = Mapper.Map<User, UserViewModel>(userModel);
            userViewModel.GroupDisplayName = _userGroupService.GetById(userViewModel.GroupID).DisplayName;

            var listUserGroup = _userGroupService.GetAll();
            userViewModel.UserGroups = Mapper.Map<IEnumerable<UserGroup>, IEnumerable<UGroup>>(listUserGroup);
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Thêm mới không thành công.");
                return RedirectToAction("Index");
            }
            else
            {
                User newUser = new User();
                newUser.UpdateUser(userViewModel);
                _userService.Add(newUser);
                _userService.SaveChanges();
                SetAlert("success", newUser.Username + " đã được thêm mới.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Chỉnh sửa không thành công.");
                return RedirectToAction("Index");
            }
            User updatedUser = new User();
            updatedUser.UpdateUser(userViewModel);
            _userService.Update(updatedUser);
            _userService.SaveChanges();
            SetAlert("success", updatedUser.Username + " đã được chỉnh sửa.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "xoá không thành công.");
                return Json(new
                {
                    status = false
                });
            }
            var user = _userService.Delete(id);
            _userService.SaveChanges();
            SetAlert("success", user.Username + " đã xoá thành công.");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll(string listId)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "xoá không thành công.");
                return Json(new
                {
                    status = false
                });
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Ids = serializer.Deserialize<string>(listId);
            int countDelete = 0;
            foreach (var id in Ids.Split(new[] { "-" }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _userService.Delete(int.Parse(id));
                    countDelete++;
                }
            }
            _userService.SaveChanges();
            SetAlert("success", "Tổng cộng " + countDelete + " bản ghi đã được xoá.");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var user = _userService.GetById(id);
            user.Status = !user.Status;
            _userService.Update(user);
            _userService.SaveChanges();
            if (user.Status)
                SetAlert("success", "Kích hoạt tài khoản " + user.Username);
            else
                SetAlert("success", "Khoá tài khoản " + user.Username);
            return Json(new
            {
                status = true
            });
        }
    }
}