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
    public class JobController : BaseController
    {
        private IJobService _jobService;
        private IApplicantService _applicantService;

        public JobController(IJobService jobService, IApplicantService applicantService)
        {
            this._jobService = jobService;
            this._applicantService = applicantService;
        }
        public ActionResult Index()
        {
            var listJob = _jobService.GetAllAvailable();
            var listJobViewModel = Mapper.Map<IEnumerable<Job>, IEnumerable<JobViewModel>>(listJob);
            return View(listJobViewModel);
        }

        public ActionResult ApplyView(int id)
        {
            var job = _jobService.GetById(id);
            var jobViewModel = Mapper.Map<Job, JobViewModel>(job);
            return View(jobViewModel);
        }

        public ActionResult Apply(ApplicantViewModel applicantViewModel, HttpPostedFileBase file)
        {
            try
            {
                Applicant applicant = new Applicant();
                applicant.UpdateApplicant(applicantViewModel);
                applicant.CreatedDate = DateTime.Now;
                if (file == null)
                {
                    SetAlert("error", "Vui lòng đính kèm CV của bạn");
                    return RedirectToAction("Index");
                }
                else
                {
                    string extension = System.IO.Path.GetExtension(file.FileName);
                    if (extension == ".pdf")
                    {
                        string filename = System.IO.Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath("~/UploadedFiles/resume/" + filename);
                        file.SaveAs(physicalPath);
                        applicant.Resume = "/UploadedFiles/resume/" + filename;
                    }
                }

                TryValidateModel(applicant);
                if (ModelState.IsValid)
                {
                    _applicantService.Add(applicant);
                    _applicantService.SaveChanges();

                    var filepath = ConfigHelper.GetByKey("ConfirmApplicant");
                    var mes = System.IO.File.ReadAllText(Server.MapPath(filepath));
                    mes = mes.Replace("{{Applicant}}", applicant.Fullname);
                    mes = mes.Replace("{{Position}}", _jobService.GetById(applicant.JobID).Name);
                    MailHepler.SendMail(applicant.Email, "CIS tuyển dụng", mes);

                    SetAlert("success", "Nộp đơn thành công");
                }
                else
                {
                    SetAlert("error", "Đã có lỗi khi nộp đơn. Vui lòng kiểm tra lại thông tin");
                }
            }
            catch
            {
                SetAlert("error", "Đã có lỗi khi nộp đơn. Vui lòng kiểm tra lại thông tin");
            }
            return RedirectToAction("Index");
        }
    }
}