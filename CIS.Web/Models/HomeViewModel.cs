using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class HomeViewModel
    {
        public List<Post> HotNews { get; set; }
        public IEnumerable<Post> News { get; set; }
    }
}