using AutoMapper;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class JobController : Controller
    {
        private IJobService _jobService;

        public JobController(IJobService jobService)
        {
            this._jobService = jobService;
        }

        public ActionResult Index()
        {
            var listJob = _jobService.GetAll();
            var listJobViewModel = Mapper.Map<IEnumerable<Job>, IEnumerable<JobViewModel>>(listJob);
            return View(listJobViewModel);
        }

    }
}