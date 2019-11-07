using CIS.Web.Areas.Admin.Models;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Areas.Admin.Infrastructure.Extensions
{
    public static class EntityExtensions
    {        
        public static void UpdateReportExcel(this ReportExcelViewModel reportExcelVm, RequestViewModel requestVm)
        {
            reportExcelVm.ID = requestVm.ID;
            reportExcelVm.SenderName = requestVm.SenderName;
            reportExcelVm.Category = requestVm.CategoryName;
            reportExcelVm.Email = requestVm.Email;
            reportExcelVm.Phone = requestVm.Phone;
            reportExcelVm.Place = requestVm.Place;
            reportExcelVm.Detail = requestVm.Detail;           
            reportExcelVm.SentDate = requestVm.SentDate;
            reportExcelVm.CompletedDate = requestVm.CompletedDate;
            reportExcelVm.Progress = requestVm.Progress;
            reportExcelVm.Supporter = requestVm.Supporter;
        }
    }
}