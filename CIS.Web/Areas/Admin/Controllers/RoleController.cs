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
    public class RoleController : BaseController
    {
        private IRoleService _roleService;        

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
            
        }
        
        public ActionResult Index()
        {
            var listRole = _roleService.GetAll();
            var listRoleViewModel = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(listRole);            
            return View(listRoleViewModel);
        }
    }
}