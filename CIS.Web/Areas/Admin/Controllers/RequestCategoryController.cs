using AutoMapper;
using CIS.Common;
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
    public class RequestCategoryController : BaseController
    {
        private IRequestCategoryService _requestCategoryService;

        public RequestCategoryController(IRequestCategoryService requestCategoryService)
        {
            this._requestCategoryService = requestCategoryService;
        }

        [HasCredential(RoleID = "R_REQUEST")]
        public ActionResult Index()
        {
            var listRequestCategory = _requestCategoryService.GetAll();
            var listRequestCategoryVm = Mapper.Map<IEnumerable<RequestCategory>, IEnumerable<RequestCategoryViewModel>>(listRequestCategory);
            return View(listRequestCategoryVm);
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult AddOrUpdate(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var requestCategoryViewModel = serializer.Deserialize<RequestCategoryViewModel>(model);
            if (requestCategoryViewModel.ID == 0)
            {
                RequestCategory requestCategory = new RequestCategory();
                requestCategory.UpdateRequestCategory(requestCategoryViewModel);
                requestCategory.CreatedDate = DateTime.Now;
                requestCategory.CreatedBy = currentUserName;
                TryValidateModel(requestCategory);
                if (ModelState.IsValid)
                {
                    var newRequestCategoryService = _requestCategoryService.Add(requestCategory);
                    if (newRequestCategoryService == null)
                    {
                        SetAlert("error", "Loại yêu cầu đã tồn tại.");
                        return Json(new
                        {
                            status = false
                        });
                    }
                    else
                    {
                        _requestCategoryService.SaveChanges();
                        SetAlert("success", "Tạo thành công loại yêu cầu.");
                        return Json(new
                        {
                            status = true
                        });
                    }
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
                var updatedRequestCategory = _requestCategoryService.GetById(requestCategoryViewModel.ID);
                updatedRequestCategory.UpdateRequestCategory(requestCategoryViewModel);
                updatedRequestCategory.UpdatedDate = DateTime.Now;
                updatedRequestCategory.UpdatedBy = currentUserName;
                TryValidateModel(updatedRequestCategory);
                if (ModelState.IsValid)
                {
                    _requestCategoryService.Update(updatedRequestCategory);
                    _requestCategoryService.SaveChanges();
                    SetAlert("success", "Chỉnh sửa thành công loại yêu cầu.");
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
        }

        [HttpGet]
        [HasCredential(RoleID = "R_REQUEST")]
        public JsonResult LoadDetail(int id)
        {
            var requestCategory = _requestCategoryService.GetById(id);
            return Json(new
            {
                status = true,
                data = requestCategory
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult Delete(int id)
        {
            _requestCategoryService.Delete(id);
            _requestCategoryService.SaveChanges();
            SetAlert("success", "Đã xoá thành công.");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult DeleteAll(string listId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Ids = serializer.Deserialize<string>(listId);
            int countDelete = 0;
            foreach (var id in Ids.Split(new[] { "-" }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _requestCategoryService.Delete(int.Parse(id));
                    countDelete++;
                }
            }
            _requestCategoryService.SaveChanges();
            SetAlert("success", "Tổng cộng " + countDelete + " bản ghi đã được xoá.");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult ChangeStatus(int id)
        {
            var requestCategory = _requestCategoryService.GetById(id);
            requestCategory.Status = !requestCategory.Status;
            requestCategory.UpdatedDate = DateTime.Now;
            requestCategory.UpdatedBy = currentUserName;
            _requestCategoryService.Update(requestCategory);
            _requestCategoryService.SaveChanges();
            if (requestCategory.Status)
                SetAlert("success", "Kích hoạt loại yêu cầu " + requestCategory.Name);
            else
                SetAlert("success", "Khoá tin " + requestCategory.Name);
            return Json(new
            {
                status = true
            });
        }
    }
}