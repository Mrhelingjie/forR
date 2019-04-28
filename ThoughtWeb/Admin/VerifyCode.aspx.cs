using System;
using Mejoy.Common;
using Mejoy.Library;

namespace Mejoy.WebSite.Admin
{
    public partial class VerifyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            Function.MakeVerifyCode(Common.Config.VERIFY_CODE_ADMIN_LOGIN_KEY, 5);
        }
    }
}
