using System;
using System.Data;

using Mejoy.Library;

namespace Mejoy.WebSite.Admin.Admin
{
    public partial class New : BaseAdmin
    {
        private Mejoy.DataAccess.Admin.AdminData adminData = new Mejoy.DataAccess.Admin.AdminData();
        private Mejoy.DataAccess.Admin.AdminMenuData menuData = new Mejoy.DataAccess.Admin.AdminMenuData();
        //Maticsoft.BLL.Area AreaBll = new Maticsoft.BLL.Area();

        private uint _UrlAdminId;

        private uint _AdminId;
        private string _LoginName, _Password, _Note, _Rights, _Area;
        private byte _Level, _Lock;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VarInit();
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
		/// 参数初始
		/// </summary>
		private void VarInit()
		{
			//编号
			this._UrlAdminId = Function.RequestQueryString<uint>("id");
			this.tbAdminId.Value = this._UrlAdminId.ToString();

			//事件
            this.AddBackButtonEvent(this.btnBack);
            this.ddlLevel.Attributes.Add("onchange", "setRights(this.form, this.value, '');setArea(this.form, this.value, '');");
		}//End VarInit()


		/// <summary>
		/// 数据初始
		/// </summary>
		private void DataInit()
		{
			#region  菜单权限
			uint TMenuId  = 0;
			string TName = "";
			uint CMenuId  = 0;
			string CName = "";

			DataRow drTop,drChild;

			DataTable dtChild = new DataTable();
			DataTable dtTop = menuData.FillTopMenu();

			string Html = "";
			for (int i=0; i<dtTop.Rows.Count; i++)
			{
				Html += "\n<ul>\n";
				drTop = dtTop.Rows[i];
				TMenuId = Function.ConvertTo<uint>(drTop["Menu_Id"]);
				TName   = Convert.ToString(drTop["Name"]);

				Html += string.Format("    <li class=\"perTitle\">{0}</li>\n", TName);

				dtChild = menuData.FillChildMenu(TMenuId);
				for (int j=0; j<dtChild.Rows.Count; j++)
				{
					drChild = dtChild.Rows[j];
					CMenuId = Function.ConvertTo<uint>(drChild["Menu_Id"]);
					CName   = Convert.ToString(drChild["Name"]);

					Html += string.Format("        <li class=\"perItem\"><input type=\"checkbox\" name=\"cbRights\" value=\"{0}\" class=\"nostyle\">{1}</li>\n", CMenuId, CName);
				}
				Html += "</ul>\n";
			}
			this.tdRights.InnerHtml = Html;
			#endregion


            //#region 地区初始
            //string _AreaHtml = "";
            //DataSet dsArea = AreaBll.GetAllList();
            //foreach (DataRow dr in dsArea.Tables[0].Rows)
            //{
            //    _AreaHtml += "\n<ul>\n";
            //    _AreaHtml += string.Format("        <li class=\"perItem\"><input type=\"checkbox\" name=\"cbArea\" value=\"{0}\" class=\"nostyle\">{1}</li>\n", dr["ID"].ToString(), dr["Area_Name"].ToString());
            //    _AreaHtml += "</ul>\n";
            //}
            //this.tdArea.InnerHtml = _AreaHtml;
            //#endregion

            if (this._UrlAction=="edit" && this._UrlAdminId>0)
			{
				//编辑
				DataRow dr = adminData.Detail(this._UrlAdminId);
				if (dr==null)
				{
					Message.Show("抱歉，此帐号不存在！", "list.aspx", 1);
				}
				else
				{
					this._Rights    = Convert.ToString(dr["Rights"]);
                    this._Area = Convert.ToString(dr["Areas"]);

					this.tbLoginName.Text = Convert.ToString(dr["Login_Name"]);
					this.tbNote.Text  = Convert.ToString(dr["Note"]);

					Function.SetRadioButtonListDefault(this.rblLock, Convert.ToByte(dr["Lock"]).ToString());
					Function.SetDropDownListDefault(this.ddlLevel, Convert.ToInt16(dr["Level"]).ToString());

                    this.labJsInit.Text = string.Format("<script language=\"javascript\" type=\"text/javascript\">setRights(document.getElementById(\"frmAdmin\"), {0}, \"{1}\");</script>", Convert.ToInt16(dr["Level"]), this._Rights);
                    this.labJsInit.Text += string.Format("<script language=\"javascript\" type=\"text/javascript\">setArea(document.getElementById(\"frmAdmin\"), {0}, \"{1}\");</script>", Convert.ToInt16(dr["Level"]), this._Area);
                }
			}
			else
			{
				//初始
				this.rblLock.Items[0].Selected = true;
			}
		}//End DataInit()


		/// <summary>
		/// 检查输入
		/// </summary>
		private void CheckInput()
		{
			//编号
			this._AdminId = Function.RequestForm<uint>("tbAdminId");
			//帐号
			this._LoginName = Convert.ToString(Request.Form["tbLoginName"]).ToLower().Trim();
			if (!Function.IsValidLoginName(this._LoginName))
			{
				this._Error = true;
				this._ErrorMsg += @"\n\n抱歉，登录帐号不正确！";
			}
			//密码
			this._Password = Convert.ToString(Request.Form["tbPassword"]).Trim();
			if (this._AdminId==0)
			{
				//新增必须输入密码
				if (this._Password=="")
				{
					this._Error = true;
					this._ErrorMsg += @"\n\n抱歉，请输入登录密码！";
				}
			}
			//帐号说明
			this._Note = Convert.ToString(Request.Form["tbNote"]);
			if (Function.GetStringLength(this._Note)>20)
			{
				this._Error = true;
				this._ErrorMsg += @"\n\n抱歉，真实姓名太长！";
			}
			//级别
			this._Level = Convert.ToByte(Request.Form["ddlLevel"]);
			//锁定
			this._Lock = (byte)((this.rblLock.Items[0].Selected) ? 0 : 1);
			//权限
			if (this._Level==99)
			{
				this._Rights = "";
                this._Area = "";
			}
			else
			{
                this._Rights = Convert.ToString(Request.Form["cbRights"]);
                this._Area = Convert.ToString(Request.Form["cbArea"]);
				if (this._Rights==null)
				{
					this._Rights = "";
				}
                if (this._Area == null)
                {
                    this._Area = "";
                }
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
			else
			{
				adminData._LoginName = this._LoginName;
                adminData._Note = this._Note;
				adminData._Password  = this._Password;
				adminData._Level     = this._Level;
				adminData._Lock      = this._Lock;
				adminData._Rights    = this._Rights;
                adminData._Area = this._Area;
				if (this._AdminId<1)
				{
					//新增
					adminData.Insert();
					switch(adminData._EventId)
					{
						case 11:
							Message.Show(string.Format("抱歉，帐号“{0}”已经存在，请选用其他帐号！", this._LoginName));
							break;
						case 1:
							Message.Show("添加帐号成功！继续添加新帐号吗？", "?", "list.aspx", 1);
							break;
						default:
							Message.Show("抱歉，出现系统无法告知错误程序，添加失败！", "?", 1);
							break;
					}
				}
				else
				{
					//编辑
					adminData.Update(this._AdminId);
					switch(adminData._EventId)
					{
						case 11:
							Message.Show(string.Format("抱歉，帐号“{0}”已经存在，请选用其他帐号！", this._LoginName));
							break;
						case 12:
							Message.Show("抱歉，不能将最后一个可用超级管理员设为其它级别类管理员！");
							break;
						case 1:
							Message.Show("编辑帐号成功！返回帐号列表吗？", "list.aspx", "?do=edit&id="+this._AdminId.ToString(), 1);
							break;
						default:
							Message.Show("抱歉，出现系统无法告知错误程序，添加失败！", "list.aspx", 1);
							break;
					}
				}
			}
		}//End SaveData()
    }
}
