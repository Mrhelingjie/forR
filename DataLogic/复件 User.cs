using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Mejoy.Library;

namespace Mejoy.DataLogic
{
    public class User : BaseDataLogic
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public static uint _UserId = 0;
        /// <summary>
        /// 用户登录帐号
        /// </summary>
        public static string _LoginName = "";
        /// <summary>
        /// 用户昵称
        /// </summary>
        public static string _NickName = "";



        /// <summary>
        /// 登录Cookie初始
        /// </summary>
        public static void CookieInit()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[Common.Config.COOKIE_USER_LOGIN_KEY];
            if (cookie != null)
            {
                //ID
                try
                {
                    _UserId = Convert.ToUInt32(cookie.Values["UserId"]);
                }
                catch
                {
                    _UserId = 0;
                }
                //登录帐号
                try
                {
                    _LoginName = Convert.ToString(cookie.Values["LoginName"]);
                    if (!Function.IsValidLoginName(_LoginName))
                    {
                        _LoginName = "";
                    }
                }
                catch
                {
                    _LoginName = "";
                }
                //昵称
                try
                {
                    _NickName = HttpContext.Current.Server.UrlDecode(cookie.Values["NickName"]);
                }
                catch
                {
                    _NickName = "";
                }
            }
        }//End CookieInit();



        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            if (_UserId > 0 && Function.IsValidLoginName(_LoginName))
            {
                return true;
            }
            return false;
        }//End IsLogin();



        /// <summary>
        /// 检查登录并跳转
        /// </summary>
        /// <param name="RedirectUrl">若RedirectUrl为Null或Empty时，未登录时跳转到/user/login.aspx</param>
        public static void CheckLogin(string RedirectUrl)
        {
            CookieInit();

            if (!IsLogin())
            {
                if (string.IsNullOrEmpty(RedirectUrl))
                {
                    RedirectUrl = "/user/login.aspx?"+ DateTime.Now.Ticks.ToString();
                }

                HttpContext.Current.Response.Redirect(RedirectUrl);
            }
        }
    }
}
