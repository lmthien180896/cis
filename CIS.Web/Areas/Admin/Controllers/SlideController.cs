using AutoMapper;
using CIS.Common;
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
    public class SlideController : BaseController
    {
        private ISlideService _slideService;

        public SlideController(ISlideService slideService)
        {
            this._slideService = slideService;
        }

        public ActionResult ListSlide()
        {
            var listSlide = _slideService.GetAll();
            var listSlideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(listSlide);
            return View(listSlideViewModel);
        }

        [HttpGet]
        [HasCredential(RoleID = "CRUD_UI")]
        public JsonResult LoadDetail(int id)
        {
            var slide = _slideService.GetById(id);
            return Json(new
            {
                status = true,
                data = slide
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "CRUD_UI")]
        public JsonResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Xoá slide không thành công.");
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                _slideService.Delete(id);
                _slideService.SaveChanges();
                SetAlert("success", "Đã xoá thành công.");
                return Json(new
                {
                    status = true
                });
            }
        }

        [HttpPost]
        [HasCredential(RoleID = "CRUD_UI")]
        public JsonResult AddOrUpdate(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var slideViewModel = serializer.Deserialize<SlideViewModel>(model);           
            if (slideViewModel.ID == 0)
            {
                Slide slide = new Slide();
                slide.UpdateSlide(slideViewModel);
                TryValidateModel(slide);
                if (ModelState.IsValid)
                {
                    _slideService.Add(slide);
                    _slideService.SaveChanges();
                    SetAlert("success", "Tạo thành công slide.");
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    SetAlert("error", "ModelState is not valid");
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            else
            {
                var updatedSlide = _slideService.GetById(slideViewModel.ID);
                updatedSlide.UpdateSlide(slideViewModel);
                TryValidateModel(updatedSlide);
                if (ModelState.IsValid)
                {
                    _slideService.Update(updatedSlide);
                    _slideService.SaveChanges();
                    SetAlert("success", "Chỉnh sửa thành công slide.");
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    SetAlert("error", "ModelState is not valid");
                    return Json(new
                    {
                        status = false
                    });
                }
            }
        }
    }
}