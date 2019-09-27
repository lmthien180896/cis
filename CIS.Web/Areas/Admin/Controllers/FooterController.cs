using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Infrastructure.Extensions;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        private IFooterService _footerService;    

        public FooterController(IFooterService footerService)
        {
            this._footerService = footerService;           
        }

        public ActionResult ListFooter()
        {
            var listFooter = _footerService.GetAll();
            var listFooterViewModel = AutoMapper.Mapper.Map<IEnumerable<Footer>, IEnumerable<FooterViewModel>>(listFooter);
            return View(listFooterViewModel);
        }


        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateFooter(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var footerViewModel = serializer.Deserialize<FooterViewModel>(model);
            Footer footer = _footerService.GetById(footerViewModel.ID);
            footer.UpdateFooter(footerViewModel);
            _footerService.Update(footer);
            _footerService.SaveChanges();
            SetAlert("success", "Chỉnh sửa footer thành công");
            return Json(new
            {
                status = true
            });
        }
    }
}