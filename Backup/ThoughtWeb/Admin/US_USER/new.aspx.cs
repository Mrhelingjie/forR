using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Mejoy.Library;
using System.IO;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using Mejoy.WebSite.Admin;
namespace ThoughtWeb.Admin.US_USER
{
    public partial class _new : BaseAdmin
    {
        Maticsoft.BLL.T_Admin Bll = new Maticsoft.BLL.T_Admin();
        Maticsoft.Model.T_Admin Model = new Maticsoft.Model.T_Admin();
        private int _UrlAdminId;

        private string Name = string.Empty;
        private string Number = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.VarInit();
            if (this.Page.IsPostBack)
            {
                this.SaveData();
            }
            else
            {
                InitDropDown();
                this.DataInit();
            }
        }


        private void InitDropDown()
        {
            this.ddl_usertype.Items.Add(new ListItem("管理员", "1"));
        }

        /// <summary>
        /// 参数初始
        /// </summary>
        private void VarInit()
        {
            //编号
            this._UrlAdminId = Function.RequestQueryString<int>("id");
            this.tbAdminId.Value = this._UrlAdminId.ToString();

            //事件
            this.AddBackButtonEvent(this.btnBack);
        }//End VarInit()

        /// <summary>
        /// 数据初始
        /// </summary>
        private void DataInit()
        {
            if (this._UrlAction == "edit" && this._UrlAdminId > 0)
            {
                //编辑
                Model = Bll.GetModel(_UrlAdminId);
                if (Model == null)
                {
                    Message.Show("抱歉，此用户不存在！", "list.aspx", 1);
                }
                else
                {
                    this.tbUserID.ReadOnly = true;
                    tbAddress.Text = Model.AddRess;
                    tbIdCard.Text = Model.IDCard;
                    Model.Login_Name = Model.Login_Name;
                    tbUserID.Text = Model.Login_Name;
                    tbRealName.Text = Model.RealName;
                    tbphone.Text = Model.Telephone;
                    ddl_usertype.Text = Model.UserType;
                    trPass.Visible = false;
                }
            }
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        private void CheckInput()
        {
            Number = Convert.ToString(Request.Form["tbUserID"]).Trim();
            Name = Convert.ToString(Request.Form["tbRealName"]).Trim();
            if (Number == string.Empty)
            {
                this._Error = true;
                this._ErrorMsg += @"\n\n抱歉，用户名不能为空！";
            }
            if (Name == string.Empty)
            {
                this._Error = true;
                this._ErrorMsg += @"\n\n抱歉，姓名不能为空！";
            }
            if (tbAdminId.Value == "0")
            {
                if (Convert.ToString(Request.Form["tbPassword"]).Trim() == string.Empty || Convert.ToString(Request.Form["tbPasswordConfirm"]).Trim() == string.Empty)
                {
                    this._Error = true;
                    this._ErrorMsg += @"\n\n抱歉，密码不能为空！";
                }
                if (Convert.ToString(Request.Form["tbPassword"]).Trim() != Convert.ToString(Request.Form["tbPasswordConfirm"]).Trim())
                {
                    this._Error = true;
                    this._ErrorMsg += @"\n\n抱歉，两次输入密码不一致！";
                }
            }
            if (Bll.GetModelList("Login_Name='" + Model.Login_Name + "'").Count > 0)
            {
                this._Error = true;
                this._ErrorMsg += @"\n\n抱歉，已存在该用户名的用户！";
            }
        }//End CheckInput()

        /// <summary>
        /// 保存记录
        /// </summary>
        private void SaveData()
        {
            this.CheckInput();
            if (this._Error)
            {
                Message.Show(this._ErrorMsg);
            }
            else if (tbAdminId.Value == "0")
            {
                Model.AddRess = tbAddress.Text;
                Model.IDCard = tbIdCard.Text;
                Model.Lock = false;
                Model.Login_Name = tbUserID.Text; 
                Model.Password =GetMD5Hash(tbPassword.Text);
                Model.RealName = tbRealName.Text;
                Model.Telephone = tbphone.Text;
                Model.UserType = ddl_usertype.Text;
                Bll.Add(Model);
                Message.Show("添加成功！继续添加用户吗？", "?", "list.aspx", 1);
            }
            else
            {
                Model = Bll.GetModel(PTool.String2Int(tbAdminId.Value));
                Model.AddRess = tbAddress.Text;
                Model.IDCard = tbIdCard.Text;
                Model.Lock = false;
                Model.Login_Name = tbUserID.Text; Model.Password = tbPassword.Text;
                Model.RealName = tbRealName.Text;
                Model.Telephone = tbphone.Text;
                Model.UserType = ddl_usertype.Text;
                Bll.Update(Model);
                Message.Show("编辑成功！返回用户列表吗？", "list.aspx", "?do=edit&id=" + this.tbAdminId.Value.ToString(), 1);
            }
        }

    }
}
