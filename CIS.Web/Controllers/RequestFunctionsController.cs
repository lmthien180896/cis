﻿using AutoMapper;
using CIS.Common;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Web.Controllers
{
    public class RequestFunctionsController : BaseController
    {
        private IRequestService _requestService;
        private IRequestCategoryService _requestCategoryService;
        private IRequestReportService _requestReportService;
        private IUserService _userService;

        public RequestFunctionsController(IRequestService requestService, IRequestCategoryService requestCategoryService, IRequestReportService requestReportService, IUserService userService)
        {
            _requestService = requestService;
            _requestCategoryService = requestCategoryService;
            _requestReportService = requestReportService;
            _userService = userService;
        }

        public ActionResult ViewDetail(int id)
        {
            List<string> listFiles = new List<string>();
            var request = _requestService.GetById(id);
            var listReport = _requestReportService.GetAllByRequestID(id);
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
        
        public ActionResult EnterCode(int id, bool iscorrect = true)
        {
            ViewBag.ID = id;
            if (iscorrect)
                ViewBag.Check = 1;
            else
                ViewBag.Check = 0;
            return View();
        }

        [HttpPost]
        public ActionResult ValidateCode(Request request)
        {
            var correctCode = _requestService.GetById(request.ID).Code;
            if (request.Code == correctCode)
            {
                return RedirectToAction("ViewDetail", new { id = request.ID });
            }
            else
            {
                return RedirectToAction("EnterCode", new { id = request.ID, iscorrect = false });
            }
        }

        public ActionResult ConfirmClosing(int id)
        {
            var request = _requestService.GetById(id);
            if (request.Progress == CommonConstant.CompletedProgress)
            {
                var requestCategory = _requestCategoryService.GetById(request.CategoryID).Name;
                var filepath = ConfigHelper.GetByKey("WarningClosingRequest");
                var mes = System.IO.File.ReadAllText(Server.MapPath(filepath));
                var emails = request.Email.Replace(" ", "").Split(',');
                foreach (var toEmail in emails)
                {
                    MailHepler.SendMail(toEmail, requestCategory, mes);
                }
                SetAlert("error", "Yêu cầu của bạn đã đóng rồi");
            }
            else
            {
                _requestService.Close(id);
                SetAlert("success", "Đã xác nhận đóng yêu cầu");
            }            
            return RedirectToAction("Index","Home");
        }
    }
}