using AutoMapper;
using CIS.Common;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Areas.Admin.Models;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class UserGroupController : BaseController
    {
        private IUserGroupService _userGroupService;
        private IRoleService _roleService;
        private ICredentialService _credentialService;
        private IUserService _userService;

        public UserGroupController(IUserService userService, ICredentialService credentialService, IRoleService roleService, IUserGroupService userGroupService)
        {
            this._userGroupService = userGroupService;
            this._roleService = roleService;
            this._credentialService = credentialService;
            this._userService = userService;
        }


        public ActionResult Index()
        {
            var listUserGroup = _userGroupService.GetAll();
            var listUserGroupViewModel = Mapper.Map<IEnumerable<UserGroup>, IEnumerable<UserGroupViewModel>>(listUserGroup);
            return View(listUserGroupViewModel);
        }

        [HasCredential(RoleID = "RU_ROLE")]
        public ActionResult ViewRoleDetail(int id)
        {
            var listRole = _roleService.GetAll();
            List<ViewRoleDetailViewModel> listViewRoleDetailVm = new List<ViewRoleDetailViewModel>();
            foreach (var role in listRole)
            {
                ViewRoleDetailViewModel viewRoleDetailVm = new ViewRoleDetailViewModel();
                viewRoleDetailVm.Role = role.Name;
                viewRoleDetailVm.UserGroupID = id;
                viewRoleDetailVm.RoleID = role.ID;
                if (id == CommonConstant.AdminId)
                    viewRoleDetailVm.Status = true;
                else
                    viewRoleDetailVm.Status = _credentialService.Check(id, role.ID);
                listViewRoleDetailVm.Add(viewRoleDetailVm);
            }
            ViewBag.UserGroup = _userGroupService.GetById(id).Name;
            return View(listViewRoleDetailVm);
        }

        [HttpPost]
        [HasCredential(RoleID = "RU_ROLE")]
        public JsonResult CreateOrDeleteRole(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<ViewRoleDetailViewModel>(model);
            if (data.UserGroupID == CommonConstant.AdminId)
            {
                SetAlert("info", "Không thể khoá quyền của admin");
                return Json(new
                {
                    status = true,
                });
            }
            var userGroup = _userGroupService.GetById(data.UserGroupID).DisplayName;
            var role = _roleService.GetById(data.RoleID).Name;
            if (_credentialService.Check(data.UserGroupID, data.RoleID))
            {
                var deleteID = _credentialService.GetCredential(data.UserGroupID, data.RoleID).ID;
                _credentialService.Delete(deleteID);
                _credentialService.SaveChanges();
                SetAlert("warning", "Đã khoá chức năng " + role + " đối với " + userGroup);
            }
            else
            {
                Credential credential = new Credential();
                credential.RoleID = data.RoleID;
                credential.UserGroupID = data.UserGroupID;
                _credentialService.Add(credential);
                _credentialService.SaveChanges();
                SetAlert("success", "Đã kích hoạt năng " + role + " đối với " + userGroup);
            }
            var listCredential = _userService.GetCredentials(data.UserGroupID);
            Session.Add(CommonConstant.CREDENTIAL_SESSION, listCredential); // Gán credentials vào session    
            return Json(new
            {
                status = true,
            });
        }
    }
}