using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class UGroup
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
    }
    public class CreateUserViewModel
    {
        public UserViewModel UserViewModel { get; set; }
        public IEnumerable<UGroup> UserGroups { get; set; }
    }
}