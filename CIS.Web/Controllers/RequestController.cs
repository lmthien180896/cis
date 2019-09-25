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

namespace CIS.Web.Controllers
{
    public class RequestController : BaseController
    {
        IRequestCategoryService _requestCategoryService;
        IRequestService _requestService;
        IRequestReportService _requestReportService;
        public RequestController(IRequestCategoryService requestCategoryService, IRequestService requestService, IRequestReportService requestReportService)
        {
            this._requestCategoryService = requestCategoryService;
            this._requestService = requestService;
            this._requestReportService = requestReportService;
        }

        public string GenerateCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[6];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var code = new String(stringChars);
            return code;
        }

        public ActionResult Index()
        {
            var listCategory = _requestCategoryService.GetAll();
            ViewBag.RequestCategories = listCategory;
            return View();
        }


        [HttpPost]
        public ActionResult SubmitRequest(RequestViewModel requestViewModel, HttpPostedFileBase[] files )
        {                             
            requestViewModel.Code = GenerateCode();
            requestViewModel.Progress = CommonConstant.WaitingProgress;
            requestViewModel.CreatedDate = DateTime.Now;
            requestViewModel.CreatedBy = requestViewModel.SenderName;
            // Xử lí file upload
            List<string> fileUrls = new List<string>();
            if (files[0] == null)  //
            {
                requestViewModel.Files = null;
            }
            else
            {
                requestViewModel.Files = "";
                var countFiles = 0; // dùng để tách nhiều file trong database thôi
                foreach (var file in files)
                {
                    if (countFiles > 0)
                        requestViewModel.Files += ",";  // split , và lọc /Data/files để tách files
                    string extension = System.IO.Path.GetExtension(file.FileName);
                    var category = _requestCategoryService.GetById(requestViewModel.CategoryID).Name;
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".docx")
                    {
                        if (category == "Đăng tin lên IU Web")
                        {
                            string filename = System.IO.Path.GetFileName(file.FileName);
                            string physicalPath = Server.MapPath("~/UploadedFiles/web_files/" + filename);
                            file.SaveAs(physicalPath);
                            fileUrls.Add(physicalPath);
                            requestViewModel.Files += "/UploadedFiles/web_files/" + filename;
                        }
                        else if (category == "Vấn đề khác")
                        {
                            string filename = System.IO.Path.GetFileName(file.FileName);
                            string physicalPath = Server.MapPath("~/UploadedFiles/files/" + filename);
                            file.SaveAs(physicalPath);
                            fileUrls.Add(physicalPath);
                            requestViewModel.Files += "/UploadedFiles/files/" + filename;
                        }
                        else
                        {
                            string filename = System.IO.Path.GetFileName(file.FileName);
                            string physicalPath = Server.MapPath("~/UploadedFiles/network_files/" + filename);
                            file.SaveAs(physicalPath);
                            fileUrls.Add(physicalPath);
                            requestViewModel.Files += "/UploadedFiles/network_files/" + filename;
                        }
                    }
                    countFiles++;
                }
            }

            Request request = new Request();
            request.UpdateRequest(requestViewModel);
            _requestService.Add(request);
            _requestService.SaveChanges();
            SetAlert("succes", "Gửi yêu cầu thành công");
            return RedirectToAction("Index");
        }

    }
}