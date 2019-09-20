using CIS.Model.Models;
using System;
using System.Collections.Generic;

namespace CIS.Web.Models
{
    public class RequestViewModel
    {
        public int ID { get; set; }

        public int CategoryID { get; set; }

        public string SenderName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Detail { get; set; }

        public string Place { get; set; }

        public string Files { get; set; }

        public string Code { get; set; }

        public string Progress { get; set; }

        public DateTime? ClosedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public bool Status { get; set; }

        public virtual RequestCategory RequestCategory { get; set; }

        public virtual IEnumerable<RequestReport> RequestReports { get; set; }


        //Extra
        public string SentDate { get; set; }
    }
}