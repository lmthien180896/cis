using AutoMapper;
using CIS.Common;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Areas.Admin.Models;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private IRequestService _requestService;
        private IRequestCategoryService _requestCategoryService;
        private IFeedbackService _feedbackService;
        private IApplicantService _applicantService;
        private IUserService _userService;
        private IPostService _postService;
        private IInternalDetailService _internalDetailService;

        public HomeController(IRequestService requestService, IRequestCategoryService requestCateogoryService, IInternalDetailService internalDetailService, IPostService postService,IUserService userService, IFeedbackService feedbackService, IApplicantService applicantService)
        {
            this._requestService = requestService;
            this._requestCategoryService = requestCateogoryService;
            this._feedbackService = feedbackService;
            this._applicantService = applicantService;
            this._userService = userService;
            this._postService = postService;
            this._internalDetailService = internalDetailService;
        }

        public ActionResult Index()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            HomeAdminViewModel homeVm = new HomeAdminViewModel();

            homeVm.TotalUsers = _userService.CountTotal();
            homeVm.TotalPosts = _postService.CountTotal();
            homeVm.TotalRequests = _requestService.CountTotal();
            homeVm.TotalFeedbacks = _feedbackService.CountTotal();
            homeVm.TotalApplicants = _applicantService.CountTotal();

            var internalDetails = _internalDetailService.GetAll();
            homeVm.InternalDetailViewModels = Mapper.Map<IEnumerable<InternalDetail>, IEnumerable<InternalDetailViewModel>>(internalDetails);

            List<int> requestsPerMonth = new List<int>();
            for (int month = 1; month < 13; month++)
            {                
                requestsPerMonth.Add(_requestService.CountRequestPerMonth(month));
            }                      
            homeVm.RequestsPerMonth = serializer.Serialize(requestsPerMonth);

            List<int> countRequestCategoryValue = new List<int>();                                  
            var ListRequestCategory = _requestCategoryService.GetAll();
            foreach (var category in ListRequestCategory)
            {
                var NoOccuranceByCategory = _requestService.CountByCategory(category.ID);
                var occurancePercentage = (int)Math.Ceiling((decimal)NoOccuranceByCategory / (decimal)homeVm.TotalRequests * 100);
                countRequestCategoryValue.Add(occurancePercentage);       
            }
            homeVm.CountByRequestCategoryValue = countRequestCategoryValue;
            homeVm.RequestCategories = Mapper.Map<IEnumerable<RequestCategory>,IEnumerable<RequestCategoryViewModel>>(ListRequestCategory);

            return View(homeVm);
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