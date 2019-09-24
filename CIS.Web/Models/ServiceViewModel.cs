using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Models
{
    public class ServiceViewModel
    {
        public Post Info { get; set; }
        public Post Price { get; set; }
        public Post Contact { get; set; }
        public string PriceURL { get; set; }

    }
}