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
    public class ApplicantController : BaseController
    {
        private IJobService _jobService;
        private IApplicantService _applicantService;


        public ApplicantController(IJobService jobService, IApplicantService applicantService)
        {
            this._jobService = jobService;
            this._applicantService = applicantService;
        }

        [HasCredential(RoleID = "CRUD_JOB")]
        public ActionResult Index()
        {
            var listApplicant = _applicantService.GetAll();
            var listApplicantViewModel = Mapper.Map<IEnumerable<Applicant>, IEnumerable<ApplicantViewModel>>(listApplicant);
            foreach (var applicant in listApplicantViewModel)
            {
                applicant.JobName = _jobService.GetById(applicant.JobID).Name;
            }
            return View(listApplicantViewModel);
        }

        [HttpPost]
        [HasCredential(RoleID = "CRUD_JOB")]
        public JsonResult Delete(int id)
        {
            var applicant = _applicantService.Delete(id);
            _applicantService.SaveChanges();
            SetAlert("success", "Đã xoá ứng cử viên " + applicant.Fullname);
            return Json(new
            {
                status= true
            });
        }

        [HttpPost]
        [HasCredential(RoleID = "CRUD_JOB")]
        public JsonResult DeleteAll(string listId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Ids = serializer.Deserialize<string>(listId);
            int countDelete = 0;
            foreach (var id in Ids.Split(new[] { "-" }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _applicantService.Delete(int.Parse(id));
                    countDelete++;
                }
            }
            _applicantService.SaveChanges();
            SetAlert("success", "Tổng cộng " + countDelete + " bản ghi đã được xoá.");
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        [HasCredential(RoleID = "CRUD_JOB")]
        public JsonResult GetEmail(int id)
        {
            var email = _applicantService.GetById(id).Email;
            return Json(new
            {
                status = true,
                data = email
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "CRUD_JOB")]
        public JsonResult SendEmail(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var mailViewModel = serializer.Deserialize<MailViewModel>(model);
            MailHepler.SendMail(mailViewModel.ToEmail, "CIS Tuyển dụng", mailViewModel.Message);
            SetAlert("success", "Đã gửi mail tới ứng cử viên");
            return Json(new
            {
                status = true
            });
        }
    }
}