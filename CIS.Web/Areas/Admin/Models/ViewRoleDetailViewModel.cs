using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Areas.Admin.Models
{
    public class ViewRoleDetailViewModel
    {
        public int UserGroupID { get; set; }
        public string RoleID { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}