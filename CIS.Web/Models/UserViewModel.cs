using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Fullname { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int GroupID { get; set; }

        public bool Status { get; set; }

        //thuộc tính khác
        public string GroupDisplayName { get; set; }
    }
}