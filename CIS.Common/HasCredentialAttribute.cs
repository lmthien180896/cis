using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CIS.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { get; set; }

        // KIỂM TRA QUYỀN BẰNG SESSION
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (UserLogin)HttpContext.Current.Session[CommonConstant.USER_SESSION];  // Kiểm tra session user
            if (session == null) // chưa đăng nhập
                return true;   // ==> ở đây sẽ đi tiếp vào BaseController và dẫn về trang login
            if (session.GroupID == "ADMIN") return true; // Nếu là admin thì không cần kiểm tra quyền

            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName); // Lấy tất cả quyền của user
            if (privilegeLevels.Contains(this.RoleID)) // Nếu trang truy cập có quyền nằm trong list tất cả quyền thì cho đăng nhập
            {
                return true;
            }
            else return false;
        }

        // KIỂM TRA QUYỀN KHÔNG BẰNG SESSION
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            }; 
        }

        private List<string> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<string>)HttpContext.Current.Session[CommonConstant.CREDENTIAL_SESSION];
            return credentials;
        }
    }
}