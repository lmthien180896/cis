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

namespace CIS.Web.Controllers
{
    public class RequestController : BaseController
    {
        IRequestCategoryService _requestCategoryService;
        IRequestService _requestService;
        IRequestReportService _requestReportService;
        IPostService _postService;

        public RequestController(IPostService postService, IRequestCategoryService requestCategoryService, IRequestService requestService, IRequestReportService requestReportService)
        {
            this._postService = postService;
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

        [OutputCache(Duration = 3600)]
        public ActionResult RequestPolicy()
        {
            var requestPolicyPost = _postService.GetById(CommonConstant.RequestPolicyPostID);
            PostViewModel postViewModel = Mapper.Map<Post, PostViewModel>(requestPolicyPost);
            return View(postViewModel);
        }


        [OutputCache(Duration = 60)]
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
            var requestCategory = _requestCategoryService.GetById(requestViewModel.CategoryID).Name;
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
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".docx")
                    {
                        if (requestCategory == "Đăng tin lên IU Web")
                        {
                            string filename = System.IO.Path.GetFileName(file.FileName);
                            filename = filename.Replace(" ", "-");
                            string physicalPath = Server.MapPath("~/UploadedFiles/web_files/" + filename);
                            file.SaveAs(physicalPath);
                            fileUrls.Add(physicalPath);
                            requestViewModel.Files += "/UploadedFiles/web_files/" + filename;
                        }
                        else if (requestCategory == "Vấn đề khác")
                        {
                            string filename = System.IO.Path.GetFileName(file.FileName);
                            filename = filename.Replace(" ", "-");
                            string physicalPath = Server.MapPath("~/UploadedFiles/files/" + filename);
                            file.SaveAs(physicalPath);
                            fileUrls.Add(physicalPath);
                            requestViewModel.Files += "/UploadedFiles/files/" + filename;
                        }
                        else
                        {
                            string filename = System.IO.Path.GetFileName(file.FileName);
                            filename = filename.Replace(" ", "-");
                            string physicalPath = Server.MapPath("~/UploadedFiles/network_files/" + filename);
                            file.SaveAs(physicalPath);
                            fileUrls.Add(physicalPath);
                            requestViewModel.Files += "/UploadedFiles/network_files/" + filename;
                        }
                    }
                    countFiles++;
                }
            }

            try
            {
                Request request = new Request();
                request.UpdateRequest(requestViewModel);
                TryValidateModel(request);
                if (ModelState.IsValid)
                {
                    _requestService.Add(request);
                    _requestService.SaveChanges();

                    var filepath = ConfigHelper.GetByKey("ConfirmSendingRequest");
                    var mes = System.IO.File.ReadAllText(Server.MapPath(filepath));
                    mes = mes.Replace("{{SenderName}}", request.SenderName);
                    mes = mes.Replace("{{RequestCategory}}", requestCategory);
                    mes = mes.Replace("{{Detail}}", request.Detail);
                    mes = mes.Replace("{{Code}}", request.Code);
                    mes = mes.Replace("{{EnterCodeLink}}", ConfigHelper.GetByKey("CurrentLink") + "/RequestFunctions/EnterCode");
                    mes = mes.Replace("{{IssueID}}", request.ID.ToString());

                    var emails = request.Email.Replace(" ", "").Split(',');
                    foreach (var toEmail in emails)
                    {
                        MailHepler.SendMail(toEmail, requestCategory, mes);
                    }

                    var webTeamEmail = ConfigHelper.GetByKey("WebTeamEmail");
                    var networkTeamEmail = ConfigHelper.GetByKey("NetworkTeamEmail");
                    var centerEmail = ConfigHelper.GetByKey("CenterEmail");
                    filepath = ConfigHelper.GetByKey("SendingRequestToTeam");
                    mes = System.IO.File.ReadAllText(Server.MapPath(filepath));
                    mes = mes.Replace("{{Sender}}", request.SenderName);
                    mes = mes.Replace("{{Topic}}", requestCategory);
                    mes = mes.Replace("{{IssueDetails}}", request.Detail);
                    mes = mes.Replace("{{IssueID}}", request.ID.ToString());
                    mes = mes.Replace("{{Place}}", request.Place);
                    mes = mes.Replace("{{Email}}", request.Email);
                    mes = mes.Replace("{{Phone}}", request.Phone);
                    mes = mes.Replace("{{IssueID}}", request.ID.ToString());
                    mes = mes.Replace("{{SendRequestToTeamLink}}", ConfigHelper.GetByKey("CurrentLink") + "/Admin/Request/ViewDetail");
                    if (requestCategory == "Đăng tin lên IU Web")
                    {
                        mes = mes.Replace("{{Team}}", "Web Team");
                        MailHepler.SendMail(webTeamEmail, requestCategory, mes);
                    }
                    else if (requestCategory == "Vấn đề khác")
                    {
                        mes = mes.Replace("{{Team}}", "CIS Team");
                        MailHepler.SendMail(centerEmail, requestCategory, mes);
                    }
                    else
                    {
                        mes = mes.Replace("{{Team}}", "Network Team");
                        MailHepler.SendMail(networkTeamEmail, requestCategory, mes);
                    }

                    SetAlert("success", "Gửi yêu cầu thành công.");
                }
                else
                {
                    SetAlert("error", "Kiểm tra lại thông tin yêu cầu.");
                }
            }
            catch {
                SetAlert("error", "Đã xảy ra vấn đề khi gửi yêu cầu.");
            }
            return RedirectToAction("Index");
        }

    }
}