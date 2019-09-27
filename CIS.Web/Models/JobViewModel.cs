using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class JobViewModel
    {
        public int ID { set; get; }
       
        public string Name { get; set; }
       
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}