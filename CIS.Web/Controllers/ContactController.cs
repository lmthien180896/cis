using AutoMapper;
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
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;

        public ContactController(IContactDetailService contactDetailService)
        {
            this._contactDetailService = contactDetailService;
        }

        public ActionResult Index()
        {
            var contactDetail = _contactDetailService.GetById(CommonConstant.CISInfoId);
            ContactDetailViewModel contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(contactDetail);
            return View(contactDetailViewModel);
        }

        public ActionResult Feedback()
        {
            return View();
        }
    }
}