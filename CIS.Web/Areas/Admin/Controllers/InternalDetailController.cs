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
    public class InternalDetailController : BaseController
    {
        private IInternalDetailService _internalDetailService;


        public InternalDetailController(IInternalDetailService internalDetailService)
        {
            this._internalDetailService = internalDetailService;
        }

        public int Internaldetail { get; private set; }

        [HasCredential(RoleID = "CRUD_INTDETAIL")]
        public ActionResult Index()
        {
            var listInternalDetail = _internalDetailService.GetAll();
            var listInternalDetailVm = Mapper.Map<IEnumerable<InternalDetail>, IEnumerable<InternalDetailViewModel>>(listInternalDetail);
            return View(listInternalDetailVm);
        }

        [HttpGet]
        public JsonResult LoadDetail(int id)
        {
            var detail = _internalDetailService.GetById(id);
            return Json(new
            {
                status = true,
                data = detail
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _internalDetailService.Delete(id);
            _internalDetailService.SaveChanges();
            SetAlert("success", "Đã xoá thành công.");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll(string listId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Ids = serializer.Deserialize<string>(listId);
            int countDelete = 0;
            foreach (var id in Ids.Split(new[] { "-" }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _internalDetailService.Delete(int.Parse(id));
                    countDelete++;
                }
            }
            _internalDetailService.SaveChanges();
            SetAlert("success", "Tổng cộng " + countDelete + " bản ghi đã được xoá.");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult AddOrUpdate(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var detailViewModel = serializer.Deserialize<InternalDetailViewModel>(model);
            if (detailViewModel.ID == 0)
            {
                InternalDetail detail = new InternalDetail();
                detail.UpdateInternalDetail(detailViewModel);
                detail.CreatedDate = DateTime.Now;
                detail.CreatedBy = currentUserName;
                TryValidateModel(detail);
                if (ModelState.IsValid)
                {
                    _internalDetailService.Add(detail);
                    _internalDetailService.SaveChanges();
                    SetAlert("success", "Tạo thành công thông tin.");
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    SetAlert("error", "ModelState is not valid.");
                    return Json(new
                    {
                        status = false

                    });
                }
            }
            else
            {

                var updatedDetail = _internalDetailService.GetById(detailViewModel.ID);
                updatedDetail.UpdateInternalDetail(detailViewModel);
                updatedDetail.UpdatedDate = DateTime.Now;
                updatedDetail.UpdatedBy = currentUserName;
                TryValidateModel(updatedDetail);
                if (ModelState.IsValid)
                {
                    _internalDetailService.Update(updatedDetail);
                    _internalDetailService.SaveChanges();
                    SetAlert("success", "Chỉnh sửa thành công thông tin.");
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