using System;

namespace CIS.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Email { set; get; }

        public string Message { set; get; }

        public string Phone { set; get; }

        public string Unit { get; set; }

        public DateTime CreatedDate { set; get; }

        public bool Status { set; get; }
    }
}