using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class RequestCategoryViewModel
    {
        public int ID { get; set; }
       
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public bool Status { get; set; }

        public virtual IEnumerable<Request> Requests { get; set; }
    }
}