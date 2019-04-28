using System;

using Mejoy.Library;

namespace Mejoy.WebSite.Admin.Admin
{
    public partial class Password : BaseAdmin
    {
        private Mejoy.DataAccess.Admin.AdminData adminData = new Mejoy.DataAccess.Admin.AdminData();

        private string _PwdA, _PwdB, _PwdC;


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                this.SaveData();
            }
            else
            {
                this.DataInit();
            }
        }


        /// <summary>
        /// 数据初始
        /// </summary>
        private void DataInit()
        {
            this.tdLoginName.InnerText = admin._LoginName;
        }//End DataInit()


        /// <summary>
        /// 检查输入
        /// </summary>
        private void CheckInput()
        {
            //旧密码
            this._PwdA = Convert.ToString(Request.Form["tbPwdA"]).Trim();
            if (this._PwdA == "")
            {
                this._Error = true;
                this._ErrorMsg += @"\n\n抱歉，请输入旧密码！";
            }
            //新密码
            this._PwdB = Convert.ToString(Request.Form["tbPwdB"]).Trim();
            this._PwdC = Convert.ToString(Request.Form["tbPwdC"]).Trim();
            if (this._PwdB == "" || this._PwdB != this._PwdC)
            {
                this._Error = true;
                this._ErrorMsg += @"\n\n抱歉，未输入完新密码或两次输入的新密码不相同！";
            }

            //新旧密码不能相同
            if (this._PwdA == this._PwdB)
            {
                this._Error = true;
                this._ErrorMsg += @"\n\n旧密码与新密码相同，请修改！";
            }
        }//End CheckInput()


        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            this.CheckInput();
            if (this._Error)
            {
                Message.Show(this._ErrorMsg);
            }
            else if (admin._Level == 100)
            {
                Message.Show("抱歉，此帐号密码不可修改！");
            }
            else
            {
                adminData.EditPassword(admin._AdminId, this._PwdA, this._PwdB);
                switch (adminData._EventId)
                {
                    case 2:
                        Message.Show("抱歉，旧密码不正确！", "?", 1);
                        break;
                    case 1:
                        Message.Show("密码修改成功，请用新密码重新登录！", string.Format("{0}login.aspx?do=logout", Common.Config.DIR_ADMIN), 1);
                        break;
                    default:
                        Message.Show("抱歉，出现系统无法告知错误程序，密码修改失败！", "?", 1);
                        break;
                }
            }
        }//End SaveData()
    }
}
