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
    public class RequestController : BaseController
    {
        private IRequestService _requestService;
        private IRequestCategoryService _requestCategoryService;
        private IRequestReportService _requestReportService;
        private IUserService _userService;

        public RequestController(IRequestService requestService, IRequestCategoryService requestCategoryService, IRequestReportService requestReportService, IUserService userService)
        {
            this._requestService = requestService;
            this._requestCategoryService = requestCategoryService;
            this._requestReportService = requestReportService;
            this._userService = userService;
        }

        [HasCredential(RoleID = "R_REQUEST")]
        public ActionResult Index(DateTime? fromDate = null, DateTime? toDate = null)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            var listRequestCategory = _requestCategoryService.GetAll();
            var listRequestCategoryViewModel = Mapper.Map<IEnumerable<RequestCategory>, IEnumerable<RequestCategoryViewModel>>(listRequestCategory);
            ViewBag.ListRequestCategory = listRequestCategoryViewModel;
            var listRequest = _requestService.GetAllRequests(fromDate, toDate);
            var listRequestViewModel = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestViewModel>>(listRequest);
            foreach (var request in listRequestViewModel)
            {
                request.SentDate = request.CreatedDate.Value.ToString("dd/MM/yyyy");
                request.CategoryName = _requestCategoryService.GetById(request.CategoryID).Name;
            }
            ViewBag.WaitingProgress = CommonConstant.WaitingProgress;
            ViewBag.SupportingProgress = CommonConstant.SupportingProgress;
            return View(listRequestViewModel);
        }

        [HasCredential(RoleID = "R_REQUEST")]
        public ActionResult WaitingQueue()
        {
            var listRequestCategory = _requestCategoryService.GetAll();
            var listRequestCategoryViewModel = Mapper.Map<IEnumerable<RequestCategory>, IEnumerable<RequestCategoryViewModel>>(listRequestCategory);
            ViewBag.ListRequestCategory = listRequestCategoryViewModel;
            var listWaitingingRequest = _requestService.GetAllWaitingRequests();
            var listWaitingingRequestViewModel = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestViewModel>>(listWaitingingRequest);
            foreach (var request in listWaitingingRequestViewModel)
            {
                request.SentDate = request.CreatedDate.Value.ToString("dd/MM/yyyy");
                request.CategoryName = _requestCategoryService.GetById(request.CategoryID).Name;
            }
            return View(listWaitingingRequestViewModel);
        }

        [HasCredential(RoleID = "R_REQUEST")]
        public ActionResult SupportingQueue()
        {
            var listSupportingRequest = _requestService.GetAllSupportingRequests();
            var listSupportingRequestViewModel = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestViewModel>>(listSupportingRequest);
            foreach (var request in listSupportingRequestViewModel)
            {
                request.SentDate = request.CreatedDate.Value.ToString("dd/MM/yyyy");
            }
            return View(listSupportingRequestViewModel);
        }

        [HasCredential(RoleID = "R_REQUEST")]
        public ActionResult CompletedQueue()
        {
            var listSupportingRequest = _requestService.GetAllCompletedRequests();
            var listSupportingRequestViewModel = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestViewModel>>(listSupportingRequest);
            foreach (var request in listSupportingRequestViewModel)
            {
                request.SentDate = request.CreatedDate.Value.ToString("dd/MM/yyyy");
            }
            return View(listSupportingRequestViewModel);
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Xoá loại yêu cầu không thành công.");
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                _requestService.Delete(id);
                _requestService.SaveChanges();
                SetAlert("success", "Đã xoá thành công.");
                return Json(new
                {
                    status = true
                });
            }
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult DeleteAll(string listId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Ids = serializer.Deserialize<string>(listId);
            int countDelete = 0;
            foreach (var id in Ids.Split('-'))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _requestService.Delete(int.Parse(id));
                    countDelete++;
                }
            }
            _requestService.SaveChanges();
            SetAlert("success", "Tổng cộng " + countDelete + " bản ghi đã được xoá.");
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        [HasCredential(RoleID = "R_REQUEST")]
        public JsonResult LoadDetail(int id)
        {
            Request request = _requestService.GetById(id);
            RequestViewModel requestViewModel = Mapper.Map<Request, RequestViewModel>(request);
            requestViewModel.SentDate = requestViewModel.CreatedDate.Value.ToString("yyyy-MM-dd");
            return Json(new
            {
                status = true,
                data = requestViewModel
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult AddOrUpdate(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var requestViewModel = serializer.Deserialize<RequestViewModel>(model);
            requestViewModel.Progress = CommonConstant.WaitingProgress;
            if (requestViewModel.ID == 0)
            {
                Request request = new Request();
                request.UpdateRequest(requestViewModel);
                request.CreatedBy = currentUserName;
                TryValidateModel(request);
                if (ModelState.IsValid)
                {
                    _requestService.Add(request);
                    _requestService.SaveChanges();
                    SetAlert("success", "Tạo yêu cầu thành công.");
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
                var updatedRequest = _requestService.GetById(requestViewModel.ID);
                updatedRequest.UpdateRequest(requestViewModel);
                updatedRequest.UpdatedDate = DateTime.Now;
                updatedRequest.UpdatedBy = currentUserName;
                TryValidateModel(updatedRequest);
                if (ModelState.IsValid)
                {
                    _requestService.Update(updatedRequest);
                    _requestService.SaveChanges();
                    SetAlert("success", "Chỉnh sửa thành công yêu cầu.");
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

        [HasCredential(RoleID = "CUD_REQUEST")]
        public ActionResult ViewDetail(int id)
        {
            List<string> listFiles = new List<string>();
            var request = _requestService.GetById(id);
            var listReport = _requestReportService.GetAll(id);
            RequestViewModel requestViewModel = Mapper.Map<Request, RequestViewModel>(request);
            requestViewModel.SentDate = requestViewModel.CreatedDate.Value.ToString("dd MMMM yyyy");
            if (request.Files != null)
            {
                foreach (var file in request.Files.Split(','))
                {
                    listFiles.Add(file);
                }
                requestViewModel.ListFiles = listFiles;
            }
            requestViewModel.CategoryName = _requestCategoryService.GetById(request.CategoryID).Name;
            requestViewModel.RequestReports = Mapper.Map<IEnumerable<RequestReport>, IEnumerable<RequestReportViewModel>>(listReport);
            foreach (var report in requestViewModel.RequestReports)
            {
                report.SupporterName = _userService.GetById(report.SupporterID).Username;
            }
            return View(requestViewModel);
        }

        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult Support(int id)
        {
            _requestService.Support(id, currentUserName);
            SetAlert("info", "Chuyển yêu cầu " + id.ToString() + " sang Đang xử lí");
            return Json(new
            {
                status = true
            });
        }

        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult SendConfirm(int id)
        {
            var request = _requestService.GetById(id);
            var requestCategory = _requestCategoryService.GetById(request.CategoryID).Name;
            //Send Email
            var filepath = ConfigHelper.GetByKey("ConfirmClosingRequest");
            var mes = System.IO.File.ReadAllText(Server.MapPath(filepath));
            mes = mes.Replace("{{SenderName}}", request.SenderName);
            mes = mes.Replace("{{RequestCategory}}", requestCategory);
            mes = mes.Replace("{{Detail}}", request.Detail);
            mes = mes.Replace("{{IssueID}}", request.ID.ToString());
            mes = mes.Replace("{{ConfirmClosingRequestLink}}", ConfigHelper.GetByKey("CurrentLink") + "RequestFunctions/ConfirmClosing");
            mes = mes.Replace("{{Code}}", request.Code);

            var emails = request.Email.Replace(" ", "").Split(',');
            foreach (var toEmail in emails)
            {
                MailHepler.SendMail(toEmail, requestCategory, mes);
            }
            SetAlert("info", "Đã gửi email xác nhận yêu cầu " + id.ToString());
            return Json(new
            {
                status = true
            });
        }

        [HasCredential(RoleID = "CUD_REQUEST")]
        public JsonResult SubmitNote(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var reportViewModel = serializer.Deserialize<RequestReportViewModel>(model);
            reportViewModel.CreatedDate = DateTime.Now;
            reportViewModel.CreatedBy = currentUserName;
            reportViewModel.SupporterID = _userService.GetUserByUsername(currentUserName).ID;

            RequestReport requestReport = new RequestReport();
            requestReport.UpdateRequestReport(reportViewModel);

            TryValidateModel(requestReport);
            if (ModelState.IsValid)
            {
                _requestReportService.Add(requestReport);
                _requestReportService.SaveChanges();
                SetAlert("success", "Đã thêm ghi chú thành công");
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