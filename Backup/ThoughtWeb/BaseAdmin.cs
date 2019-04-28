using System.Web;
using System.Security.Cryptography;
using System.Text;
using Mejoy.Library;
using System;
namespace Mejoy.WebSite.Admin
{
    public class BaseAdmin : System.Web.UI.Page
    {
      public  BaseDataLogic admin = new BaseDataLogic();

        #region  常用变量
        /// <summary>
        /// 操作出错标记
        /// </summary>
        protected bool _Error = false;
        /// <summary>
        /// 操作出错信息
        /// </summary>
        protected string _ErrorMsg = string.Empty;
        /// <summary>
        /// 操作标记
        /// </summary>
        protected string _UrlAction = string.Empty;
        /// <summary>
        /// 序号值
        /// </summary>
        protected uint _No = 0;
        /// <summary>
        /// 列表每页显示数
        /// </summary>
        protected uint _PerNum = 0;
        /// <summary>
        /// 当前页
        /// </summary>
        protected uint _Page = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        protected uint _TotalNum = 0;
        /// <summary>
        /// 操作记录ID
        /// </summary>
        protected uint _UrlId = 0;
        #endregion

        public  string GetMD5Hash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            System.Array.Copy(res, temp, res.Length);
            return new String(temp);
        }
        public BaseAdmin()
        {
            this.BaseAdminInit();

          string  Url = Convert.ToString(Context.Request.ServerVariables["Url"]);
          if (Url.Contains("US_DEPT") || Url.Contains("US_USER") || Url.Contains("Menu") || Url.Contains("Admin/List") || Url.Contains("Admin/ShenSu/allshensu.aspx") || Url.Contains("ApplyLeave/list.aspx") || Url.Contains("Standard/new.aspx") || Url.Contains("Admin/New") || Url.Contains("Admin/Salary/list.aspx") || Url.Contains("Admin/Salary/addsalary.aspx") || Url.Contains("Admin/QueQin/list.aspx") || Url.Contains("Admin/QueQin/new.aspx") || Url.Contains("Admin/KeBiao/list.aspx") || Url.Contains("Admin/KeBiao/new.aspx") || Url.Contains("Admin/HeSuan/list.aspx"))
          {
              if (Context.Request.Cookies[Mejoy.Common.Config.COOKIE_ADMIN_LOGIN_KEY].Values["UserType"].ToString() == "教师")
              {
                  Message.Show("抱歉，您不具此页面的操作权限！", Common.Config.DIR_ADMIN + "Document/MySendList.aspx", 1);
              }
          }
        }

       
            
                
                    
                        
                            
                                
                                    
                                        
                                            

        /// <summary>
        /// 初始加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseAdminInit()
        {
            #region 查检登录
            bool Found = false;
            string[] Urls = new string[] { "login.aspx", "verifycode.aspx" };
            string Url = "";
            try
            {
                Url = Context.Request.ServerVariables["URL"].ToLower();
            }
            catch { }
            foreach (string url in Urls)
            {
                if (Url.Substring(Common.Config.DIR_ADMIN.Length) == url)
                {
                    Found = true;
                    break;
                }
            }
            if (!Found)
            {
                admin.CheckLogin();
            }
            #endregion
            
            //操作标记
            this._UrlAction = Function.RequestQueryString<string>("do").Trim().ToLower();
            //当前页
            this._Page = Function.RequestQueryString<uint>("page");
            if (this._Page < 1)
            {
                this._Page = 1;
            }
            //操作ID
            this._UrlId = Function.RequestQueryString<uint>("id");
        }



        /// <summary>
        /// 检查访问许可/权限
        /// </summary>
        protected void CheckRight()
        {
            if (admin._Level == 100)
            {
                return;
            }
            else
            {
                using (Mejoy.DataAccess.Admin.AdminMenuData menuData = new Mejoy.DataAccess.Admin.AdminMenuData())
                {
                    if (!menuData.IsValidAdminLink(admin._AdminId, admin._Level, admin._Rights))
                    {
                        Message.Show("抱歉，您不具此页面的操作权限！", Common.Config.DIR_ADMIN + "index_main.aspx", 1);
                    }
                }
            }
        }//End CheckRight();



        /// <summary>
        /// 添加返回按钮列表事件
        /// </summary>
        /// <param name="page"></param>
        protected void AddBackButtonEvent(System.Web.UI.HtmlControls.HtmlInputButton btn)
        {
            if (btn != null)
            {
                if (this._UrlId > 0 || string.Compare(this._UrlAction, "edit", true) == 0)
                {
                    btn.Attributes.Add("onclick", "parent.hideFrame();");
                }
                else
                {
                    btn.Attributes.Add("onclick", "backList();");
                }
            }
        }//End AddBackButtonEvent();



        /// <summary>
        /// 隐藏iframe
        /// </summary>
        /// <param name="Msg"></param>
        protected void HideParentIFrame(string Msg)
        {
            if (!string.IsNullOrEmpty(Msg))
            {
                JavaScript.Alert(Msg);
            }
            JavaScript.Exec("parent.hideFrame();");
            HttpContext.Current.Response.End();
        }
        protected void HideParentIFrame()
        {
            this.HideParentIFrame("");
        }//End HideParentIFrame();



        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="Msg"></param>
        protected void ReloadParentIFrame(string Msg)
        {
            if (!string.IsNullOrEmpty(Msg))
            {
                JavaScript.Alert(Msg);
            }
            JavaScript.Exec("parent.hideFrame(); parent.window.location.reload();");
            HttpContext.Current.Response.End();
        }
        protected void ReloadParentIFrame()
        {
            this.ReloadParentIFrame("");
        }//End ReloadParentIFrame();
    }
}
