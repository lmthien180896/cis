using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CIS.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { get; set; }
       
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (UserLogin)HttpContext.Current.Session[CommonConstant.USER_SESSION]; 
            if (session == null) 
                return true;  
            if (session.GroupID == CommonConstant.AdminId) return true; 

            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName); 
            if (privilegeLevels.Contains(this.RoleID)) 
            {
                return true;
            }
            else return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/_AccessDenied.cshtml"
            }; 
        }

        private List<string> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<string>)HttpContext.Current.Session[CommonConstant.CREDENTIAL_SESSION];
            return credentials;
        }
    }
}