using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Areas.Admin.Models
{
    public class ReportExcelViewModel
    {
        public int ID { get; set; }

        public string Category { get; set; }

        public string SenderName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Detail { get; set; }

        public string Place { get; set; }              

        public string Progress { get; set; }
                     
        public string SentDate { get; set; }

        public string CompletedDate { get; set; }

        public string Supporter { get; set; }          
    }
}