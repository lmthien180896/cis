using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class InternalDetailViewModel
    {
        public int ID { get; set; }

        public string Name { set; get; }

        public string FileUrl { set; get; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public bool Status { get; set; }
    }
}