using CIS.Model.Models;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS.Web.Areas.Admin.Models
{
    public class HomeAdminViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalPosts { get; set; }
        public int TotalRequests { get; set; }
        public int TotalFeedbacks { get; set; }
        public int TotalApplicants { get; set; }

        public IEnumerable<InternalDetailViewModel> InternalDetailViewModels { get; set; }

        public string RequestsPerMonth { get; set; }
        public List<int> CountByRequestCategoryValue { get; set; }
        public IEnumerable<RequestCategoryViewModel> RequestCategories { get; set; }
    }    
}