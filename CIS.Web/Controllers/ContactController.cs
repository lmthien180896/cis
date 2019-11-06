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

namespace CIS.Web.Controllers
{
    public class ContactController : BaseController
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            ViewBag.Phone = ConfigHelper.GetByKey("CISPhone");
            ViewBag.Email = ConfigHelper.GetByKey("CenterEmail");            
            return View();
        }


        public ActionResult Feedback()
        {
            return View();
        }

        public ActionResult SubmitFeedback(FeedbackViewModel feedbackViewModel)
        {
            Feedback feedback = new Feedback();
            feedback.UpdateFeedback(feedbackViewModel);
            feedback.CreatedDate = DateTime.Now;
            _feedbackService.Add(feedback);
            _feedbackService.SaveChanges();
            SetAlert("success", "Góp ý của bạn đã được gửi đi");
            return RedirectToAction("Feedback");
        }
    }
}