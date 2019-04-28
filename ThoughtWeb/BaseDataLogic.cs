using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Mejoy.Library;
namespace Mejoy.WebSite
{
    public class BaseDataLogic : System.Web.UI.Page
    {
        /// <summary>
        /// 管理帐号编号
        /// </summary>
        public uint _AdminId = 0;
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string _LoginName = "";
        /// <summary>
        /// 管理级别: 99-超级管理员
        /// </summary>
        public byte _Level = 0;
        /// <summary>
        /// 管理权限集
        /// </summary>
        public string _Rights = "";

        public BaseDataLogic()
        {
            this.CookieInit();
        }


        /// <summary>
        /// cookie初始化
        /// </summary>
        private void CookieInit()
        {
            HttpCookie cookie = Context.Request.Cookies[Mejoy.Common.Config.COOKIE_ADMIN_LOGIN_KEY];

            if (cookie != null)
            {
                //登录编号
                try
                {
                    this._AdminId = Convert.ToUInt16(cookie.Values["AdminId"].Trim());
                }
                catch { }

                //登录名
                try
                {
                    this._LoginName = Convert.ToString(cookie.Values["LoginName"].Trim());
                    if (!Function.IsValidLoginName(this._LoginName))
                    {
                        this._LoginName = "";
                    }
                }
                catch { }

                //级别
                try
                {
                    this._Level = Convert.ToByte(cookie.Values["Level"].Trim());
                    if (this._Level < 1)
                    {
                        this._Level = 0;
                    }
                }
                catch { }

                //权限集
                try
                {
                    this._Rights = Convert.ToString(cookie.Values["Rights"].Trim());
                    if (!Function.IsValidString(this._Rights))
                    {
                        this._Rights = "";
                    }
                }
                catch { }
            }
        }//End CookieInit()


        /// <summary>
        /// 检查当前用户登录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="lab">权限标记。</param>
        public void CheckLogin(string lab)
        {
            bool Error = false;

            if (this._AdminId < 1)
            {
                Error = true;
            }
            if (!Function.IsValidLoginName(this._LoginName))
            {
                Error = true;
            }

            if (Error)
            {
                Context.Response.Redirect(Mejoy.Common.Config.DIR_ADMIN + "login.aspx?do=logout&temp=." + DateTime.Now.Ticks.ToString());
                Context.Response.End();
            }
            else if (lab != "" && !this.IsRight(this._Level, this._Rights, lab))
            {
                Context.Response.Write("<script language=\"javascript\">alert(\"抱歉，您无此操作权限，请勿尝试无操作权的页面连接！\");</script>");
                Context.Response.End();
            }
        }
        /// <summary>
        /// 检查当前用户登录(重载)
        /// </summary>
        public void CheckLogin()
        {
            this.CheckLogin("");
        }


        /// <summary>
        /// 功能：检查是否具备指定的权限。
        /// </summary>
        /// <param name="lab">权限标志。</param>
        /// <returns></returns>
        public bool IsRight(int level, string right, string lab)
        {
            if (level == 99)
            {
                return true;
            }
            else
            {
                string[] Lab = lab.Split(",".ToCharArray());
                for (int i = 0; i < Lab.Length; i++)
                {
                    if (right.IndexOf(Lab[i]) != -1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
