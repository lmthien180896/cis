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
    public class RequestController : BaseController
    {
        private IRequestService _requestService;
        private IRequestCategoryService _requesCategorytService;

        public RequestController(IRequestService requestService, IRequestCategoryService requestCategoryService)
        {
            this._requestService = requestService;
            this._requesCategorytService = requestCategoryService;
        }

        public ActionResult Index()
        {
            var listRequestCategory = _requesCategorytService.GetAll();
            var listRequestCategoryViewModel = Mapper.Map<IEnumerable<RequestCategory>, IEnumerable<RequestCategoryViewModel>>(listRequestCategory);
            ViewBag.ListRequestCategory = listRequestCategoryViewModel;
            return View();
        }

        [HttpGet]
        public JsonResult LoadAllRequests()
        {
            var listRequests = _requestService.GetAll();            
            var listRequestViewModel = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestViewModel>>(listRequests);            
            foreach (var request in listRequestViewModel)
            {
                request.SentDate = request.CreatedDate.Value.ToString("dd/MM/yyyy");
            }
            return Json(new
            {
                status = true,
                data = listRequestViewModel
            },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddOrUpdate(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var requestViewModel = serializer.Deserialize<RequestViewModel>(model);
            Request request = new Request();
            request.UpdateRequest(requestViewModel);
            if (request.ID == 0)
            {
                request.CreatedDate = DateTime.Now;
                var newRequestCategoryService = _requestService.Add(request);
                if (newRequestCategoryService == null)
                {
                    SetAlert("error", "Tạo yêu cầu không thành công");
                    return Json(new
                    {
                        status = false
                    });
                }
                else
                {
                    _requestService.SaveChanges();
                    SetAlert("success", "Tạo yêu cầu thành công.");
                    return Json(new
                    {
                        status = true
                    });
                }
            }
            else
            {
                //requestCategory.UpdatedDate = DateTime.Now;
                //_requestCategoryService.Update(requestCategory);
                //_requestCategoryService.SaveChanges();
                //SetAlert("success", "Chỉnh sửa thành công loại yêu cầu.");
                return Json(new
                {
                    status = true
                });
            }
        }
    }
}