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
    public class JobController : BaseController
    {
        private IJobService _jobService;
        private IApplicantService _applicantService;
        

        public JobController(IJobService jobService, IApplicantService applicantService)
        {
            this._jobService = jobService;
            this._applicantService = applicantService;
        }

        [HasCredential(RoleID = "CRUD_JOB")]
        public ActionResult Index()
        {
            var listJob = _jobService.GetAll();
            var listJobViewModel = Mapper.Map<IEnumerable<Job>, IEnumerable<JobViewModel>>(listJob);
            return View(listJobViewModel);
        }

        [HttpGet]
        [HasCredential(RoleID = "CRUD_JOB")]
        public JsonResult LoadDetail(int id)
        {
            var job = _jobService.GetById(id);
            return Json(new
            {
                status = true,
                data = job
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredential(RoleID = "CRUD_JOB")]
        public JsonResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Xoá job không thành công.");
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                _jobService.Delete(id);
                _jobService.SaveChanges();
                SetAlert("success", "Đã xoá thành công.");
                return Json(new
                {
                    status = true
                });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "CRUD_JOB")]
        public JsonResult AddOrUpdate(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var jobViewModel = serializer.Deserialize<JobViewModel>(model);
            jobViewModel.CreatedDate = DateTime.Now;
            Job job = new Job();
            job.UpdateJob(jobViewModel);
            if (job.ID == 0)
            {
                _jobService.Add(job);
                _jobService.SaveChanges();
                SetAlert("success", "Tạo thành công job.");
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                _jobService.Update(job);
                _jobService.SaveChanges();
                SetAlert("success", "Chỉnh sửa thành công job.");
                return Json(new
                {
                    status = true
                });
            }
        }

        [HasCredential(RoleID = "CRUD_JOB")]
        public ActionResult Applicants()
        {
            var listApplicant = _applicantService.GetAll();
            var listApplicantViewModel = Mapper.Map<IEnumerable<Applicant>, IEnumerable<ApplicantViewModel>>(listApplicant);
            foreach (var applicant in listApplicantViewModel)
            {
                applicant.JobName = _jobService.GetById(applicant.JobID).Name;
            }
            return View(listApplicantViewModel);
        }
    }
}