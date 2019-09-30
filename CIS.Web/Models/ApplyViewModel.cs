using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class ApplyViewModel
    {
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }
    }
}