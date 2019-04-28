using System;
using System.Data;
using System.Web.UI.WebControls;

using Mejoy.Library;

namespace Mejoy.WebSite.Admin.Menu
{
    /// <summary>
    /// 模块功能：菜单添加、编辑。
    /// 开发人员：阿明
    /// 最后更新：2008-6-19
    /// </summary>
    public partial class New : BaseAdmin
    {
        private Mejoy.DataAccess.Admin.AdminMenuData menuData = new Mejoy.DataAccess.Admin.AdminMenuData();

        private uint _UrlMenuId;

        private uint _MenuId, _ParentId;
        private int _Index;
        private string _Name, _Link;
        private byte _TreeClose, _Type,_Target;


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
		/// 参数初始化。
		/// </summary>
		private void VarInit()
		{
            //菜单编号
            this._UrlMenuId = Function.RequestQueryString<uint>("id");
		}//End VarInit()


		/// <summary>
		/// 数据初始。
		/// </summary>
		private void DataInit()
		{
			DataRow dr;
			DataTable dt = new DataTable();

			//菜单类型
			dt = menuData.FillTopMenu();
			this.ddlParentId.Items.Clear();
			this.ddlParentId.Items.Add(new ListItem("一级分类", "0"));
			for (int i=0; i<dt.Rows.Count; i++)
			{
				dr = dt.Rows[i];
				this._MenuId = Function.ConvertTo<uint>(dr["Menu_Id"]);
				this._Name   = Convert.ToString(dr["Name"]);
				this.ddlParentId.Items.Add(new ListItem(this._Name, this._MenuId.ToString()));
			}

			//编辑
			if (this._UrlAction.ToLower()=="edit" && this._MenuId>0)
			{
				dr = menuData.Detail(this._UrlMenuId);
				if (dr==null)
				{
					Message.Show("抱歉，无此记录！", "list.aspx", 1);
				}
				else
				{
                    this._MenuId   = Function.ConvertTo<uint>(dr["Menu_Id"]);
                    this._ParentId = Function.ConvertTo<uint>(dr["Parent_Id"]);
					this._Name     = Convert.ToString(dr["Name"]);
					this._Link     = Convert.ToString(dr["Link"]);
                    this._Index    = Convert.ToInt32(dr["Index"]);
					this._TreeClose= Convert.ToByte(dr["Tree_Close"]);
					this._Type     = Convert.ToByte(dr["Type"]);
                    this._Target   = Convert.ToByte(dr["Target"]);

					this.tbMenuId.Value = this._MenuId.ToString();
					this.tbName.Text    = this._Name;
					this.tbLink.Text    = this._Link;
					this.tbIndex.Text   = this._Index.ToString();

					Function.SetDropDownListDefault(this.ddlParentId, this._ParentId.ToString());
                    Function.SetRadioButtonListDefault(this.rblTreeClose, this._TreeClose.ToString());
                    Function.SetDropDownListDefault(this.ddlType, this._Type.ToString());
                    Function.SetDropDownListDefault(this.ddlTarget, this._Target.ToString());
				}
			}
			else
			{
				this.rblTreeClose.Items[0].Selected = true;
			}

			dt.Dispose();
		}//End DataInit()


		/// <summary>
		/// 检查输入
		/// </summary>
		private void CheckInput()
		{
			//编辑：编辑
            this._MenuId = Function.RequestForm<uint>("tbMenuId");
			//上级分类
            this._ParentId = Function.RequestForm<uint>("ddlParentId");
			//名称
			this._Name = Convert.ToString(Request.Form["tbName"]).Trim();
            if (Function.GetStringLength(this._Name) > 50)
			{
				this._Error = true;
				this._ErrorMsg += @"\n\n抱歉，链接名称太长，请修改！";
			}
			//链接
			this._Link = Convert.ToString(Request.Form["tbLink"]).Trim();
            if (Function.GetStringLength(this._Link) > 200)
			{
				this._Error = true;
				this._ErrorMsg += @"\n\n抱歉，链接地址太长，请修改！";
			}
			//排序
            this._Index = Function.RequestForm<int>("tbIndex");
			//树形状态
			this._TreeClose = (byte)((this.rblTreeClose.Items[0].Selected) ? 0 : 1);
			//类型
			this._Type = Convert.ToByte(Request.Form["ddlType"]);
            //打开窗口
            this._Target = Function.RequestForm<byte>(this.ddlTarget.ID);
		}//End CheckInput()


		/// <summary>
		/// 保存
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
				menuData._ParentId = this._ParentId;
				menuData._Name     = this._Name;
				menuData._Link     = this._Link;
				menuData._TreeClose= this._TreeClose;
				menuData._Index    = this._Index;
				menuData._Type     = this._Type;
                menuData._Target   = this._Target;

                if (this._MenuId == 0)
                {
                    menuData.Insert();
                    if (menuData._EventId != -1)
                    {
                        Message.Show("添加成功！继续添加吗？", "?", "list.aspx", 1);
                    }
                    else
                    {
                        Message.Show("未知错误，添加失败！");
                    }
                }
                else
                {
                    menuData.Update(this._MenuId);
                    if (menuData._EventId != -1)
                    {
                        Message.Show("更新资料成功！返回列表吗？", "list.aspx", "?do=edit&id=" + this._MenuId.ToString(), 1);
                    }
                    else if (menuData._EventId == 1)
                    {
                        Message.Show("抱歉，此一级分类菜单下还有其他子分类，无法移动！");
                    }
                    else
                    {
                        Message.Show("未知错误，更新资料失败！");
                    }
                }
			}
		}//End SaveData()
    }
}
