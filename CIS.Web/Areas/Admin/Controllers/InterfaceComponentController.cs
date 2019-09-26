using AutoMapper;
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
    public class InterfaceComponentController : BaseController
    {
        private IFooterService _footerService;
        private ISlideService _slideService;

        public InterfaceComponentController(IFooterService footerService, ISlideService slideService)
        {
            this._footerService = footerService;
            this._slideService = slideService;
        }

        public ActionResult ListFooter()
        {
            var listFooter = _footerService.GetAll();
            var listFooterViewModel = Mapper.Map<IEnumerable<Footer>, IEnumerable<FooterViewModel>>(listFooter);
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

        public ActionResult ListSlide()
        {
            var listSlide = _slideService.GetAll();
            var listSlideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(listSlide);
            return View(listSlideViewModel);
        }

        [HttpPost]
        public JsonResult UpdateSlide(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var slideViewModel = serializer.Deserialize<SlideViewModel>(model);
            Slide slide = _slideService.GetById(slideViewModel.ID);
            slide.UpdateSlide(slideViewModel);
            _slideService.Update(slide);
            _slideService.SaveChanges();
            SetAlert("success", "Chỉnh sửa slide thành công");
            return Json(new
            {
                status = true
            });
        }
    }
}