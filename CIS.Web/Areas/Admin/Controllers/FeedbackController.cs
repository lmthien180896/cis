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
    public class FeedbackController : BaseController
    {        
        private IFeedbackService _feedbackService;


        public FeedbackController(IFeedbackService feedbackService)
        {            
            this._feedbackService = feedbackService;
        }

        [HasCredential(RoleID = "RUD_FEEDBACK")]
        public ActionResult Index()
        {
            var listFeedback = _feedbackService.GetAll();
            var listFeedbackViewModel = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(listFeedback);           
            return View(listFeedbackViewModel);
        }

        [HttpPost]
        [HasCredential(RoleID = "RUD_FEEDBACK")]
        public JsonResult Delete(int id)
        {
            var feedback = _feedbackService.Delete(id);
            _feedbackService.SaveChanges();
            SetAlert("success", "Đã xoá góp ý của " + feedback.Name);
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        [HasCredential(RoleID = "RUD_FEEDBACK")]
        public JsonResult DeleteAll(string listId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Ids = serializer.Deserialize<string>(listId);
            int countDelete = 0;
            foreach (var id in Ids.Split(new[] { "-" }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _feedbackService.Delete(int.Parse(id));
                    countDelete++;
                }
            }
            _feedbackService.SaveChanges();
            SetAlert("success", "Tổng cộng " + countDelete + " bản ghi đã được xoá.");
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        [HasCredential(RoleID = "RUD_FEEDBACK")]
        public JsonResult GetEmail(int id)
        {
            var email = _feedbackService.GetById(id).Email;
            return Json(new
            {
                status = true,
                data = email
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "RUD_FEEDBACK")]
        public JsonResult SendEmail(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var mailViewModel = serializer.Deserialize<MailViewModel>(model);
            MailHepler.SendMail(mailViewModel.ToEmail, "CIS Hộp thư góp ý", mailViewModel.Message);
            SetAlert("success", "Đã gửi mail thành công");
            return Json(new
            {
                status = true
            });
        }
    }
}