using CIS.Model.Models;
using System;

namespace CIS.Web.Models
{
    public class RequestReportViewModel
    {
        public int ID { get; set; }

        public int RequestID { get; set; }

        public int? SupporterID { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public bool Status { get; set; }

        public Request Request { get; set; }
    }
}