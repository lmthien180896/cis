using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class PostTagViewModel
    {
        public int PostID { set; get; }
       
        public string TagID { set; get; }
    
        public virtual Post Post { set; get; }
    }
}