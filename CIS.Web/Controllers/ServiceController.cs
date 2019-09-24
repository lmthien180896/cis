using CIS.Common;
using CIS.Service;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Web.Controllers
{
    public class ServiceController : Controller
    {
        IPostService _postService;
        protected string domain = ConfigHelper.GetByKey("CurrentLink");

        public ServiceController(IPostService postService)
        {
            this._postService = postService;
        }

        //WEB
        public ActionResult Web()
        {
            var postInfo = _postService.GetById(CommonConstant.WebInfoPostId);            
            var postContact = _postService.GetById(CommonConstant.WebContactPostId);
            ServiceViewModel serviceViewModel = new ServiceViewModel();
            serviceViewModel.Info = postInfo;
            serviceViewModel.Contact = postContact;
            serviceViewModel.PriceURL = domain + "/Documents/Bang gia dich vu web-CIS.pdf";
            return View(serviceViewModel);
        }

        

        //DESIGN
        public ActionResult Design()
        {
            var postInfo = _postService.GetById(CommonConstant.DesignInfoPostId);
            var postContact = _postService.GetById(CommonConstant.DesignContactPostId);
            var postPrice = _postService.GetById(CommonConstant.DesignPricePostId);
            ServiceViewModel serviceViewModel = new ServiceViewModel();
            serviceViewModel.Info = postInfo;
            serviceViewModel.Contact = postContact;
            serviceViewModel.Price = postPrice;
            return View(serviceViewModel);            
        }
        
        //SYSTEM
        public ActionResult System()
        {
            var postInfo = _postService.GetById(CommonConstant.SystemInfoPostId);
            var postContact = _postService.GetById(CommonConstant.SystemContactPostId);
            var postPrice = _postService.GetById(CommonConstant.SystemPricePostId);
            ServiceViewModel serviceViewModel = new ServiceViewModel();
            serviceViewModel.Info = postInfo;
            serviceViewModel.Contact = postContact;
            serviceViewModel.Price = postPrice;
            return View(serviceViewModel);            
        }       
    }
}