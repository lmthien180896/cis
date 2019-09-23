using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class UserGroupViewModel
    {
        public int ID { get; set; }

      
        public string Name { get; set; }

    
        public string DisplayName { get; set; }

        public IEnumerable<Credential> Credentials { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}