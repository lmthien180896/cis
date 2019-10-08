using CIS.Common;
using CIS.Service;
using System.Web.Mvc;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private IRequestService _requestService;
        private IFeedbackService _feedbackService;
        private IApplicantService _applicantService;

        public HomeController(IRequestService requestService, IFeedbackService feedbackService, IApplicantService applicantService)
        {
            this._requestService = requestService;
            this._feedbackService = feedbackService;
            this._applicantService = applicantService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult AdminMenu()
        {
            ViewBag.NoWaitingQueue = _requestService.CountByProgress(CommonConstant.WaitingProgress);            
            ViewBag.NoSupportingQueue = _requestService.CountByProgress(CommonConstant.SupportingProgress);            
            ViewBag.NoCompletedQueue = _requestService.CountByProgress(CommonConstant.CompletedProgress);
            ViewBag.NoFeedBack = _feedbackService.CountTotal();
            ViewBag.NoApplicant = _applicantService.CountTotal();
            return PartialView();
        }
    }
}