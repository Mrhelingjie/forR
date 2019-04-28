using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Mejoy.Library;

namespace Mejoy.WebSite.Admin.Menu
{
    /// <summary>
    /// 模块功能：管理菜单列表、删除
    /// 开发人员：阿明
    /// 最后更新：2008-1-12
    /// </summary>
    public partial class List : BaseAdmin
    {
        private Mejoy.DataAccess.Admin.AdminMenuData menuData = new Mejoy.DataAccess.Admin.AdminMenuData();

        private uint _UrlMenuId;

        private uint _PCount, _PIndex;
        private uint _CCount, _CIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VarInit();
            if (this._UrlAction == "up" || this._UrlAction == "down")
            {
                this.MoveMenu();
            }
            else
            {
                this.DataInit();
            }
        }


        /// <summary>
		/// 参数
		/// </summary>
		private void VarInit()
		{
            //菜单编号
			this._UrlMenuId = Function.RequestQueryString<uint>("id");
		}


		/// <summary>
		/// 数据
		/// </summary>
		private void DataInit()
		{
            if (this._UrlAction == "del" && this._UrlMenuId > 0)
            {
                menuData.Delete(this._UrlMenuId);

                if (menuData._EventId != -1)
                {
                    Message.Show("删除成功！");
                }
                else if (menuData._EventId == 1)
                {
                    Message.Show("抱歉，此菜单下还有其他子分类菜单，删除失败！");
                }
            }

			DataTable dt = menuData.FillTopMenu();
			this._PCount = (uint)dt.Rows.Count;
			this._PIndex = 0;

			this.rptPList.DataSource = dt;
			this.rptPList.DataBind();

			dt.Dispose();
		}


		/// <summary>
		/// 移动菜单
		/// </summary>
		private void MoveMenu()
		{
			menuData.MoveMenu(this._UrlMenuId, this._UrlAction);
			Message.Show("移动成功！", "?", 1);
		}


		/// <summary>
		/// 功能：重建一级菜单。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RePList(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{
				DataRowView thisRow = (DataRowView)e.Item.DataItem;

				if (thisRow!=null)
				{
					uint MenuId  = Function.ConvertTo<uint>(thisRow["Menu_Id"]);
					string Name = Convert.ToString(thisRow["Name"]);
					string Link = Convert.ToString(thisRow["Link"]);

					HtmlTableCell cell;

					this._PIndex++;

					//名称
					cell = (HtmlTableCell)e.Item.FindControl("tdPName");
					if (cell!=null)
					{
						cell.InnerHtml = string.Format("&nbsp;<strong>{0}</strong>", Name);
					}

					//上移
					cell = (HtmlTableCell)e.Item.FindControl("tdPUp");
					if (cell!=null)
					{
						if (this._PIndex>1)
						{
							cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"上移\" onclick=\"listEvent('up', {0})\">", MenuId);
						}
						else
						{
							cell.InnerHtml = "<input type=\"button\" name=\"btn\" class=\"btn\" value=\"上移\" disabled>";
						}
					}

					//下移
					cell = (HtmlTableCell)e.Item.FindControl("tdPDown");
					if (cell!=null)
					{
						if (this._PIndex<this._PCount)
						{
							cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"下移\" onclick=\"listEvent('down', {0})\">", MenuId);
						}
						else
						{
							cell.InnerHtml = "<input type=\"button\" name=\"btn\" class=\"btn\" value=\"下移\" disabled>";
						}
					}

					//编辑
					cell = (HtmlTableCell)e.Item.FindControl("tdPEdit");
					if (cell!=null)
					{
						cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"编辑\" onclick=\"listEvent('edit', {0})\">", MenuId);
					}

					//删除
					cell = (HtmlTableCell)e.Item.FindControl("tdPDel");
					if (cell!=null)
					{
						cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"删除\" onclick=\"listEvent('del', {0})\">", MenuId);
					}

					Repeater rpt = (Repeater)e.Item.FindControl("rptCList");
					if (rpt!=null)
					{
						DataTable dt = menuData.FillChildMenu(MenuId);
						this._CCount = (uint)dt.Rows.Count;

						rpt.DataSource = dt;
						rpt.DataBind();

						dt.Dispose();
					}
					this._CIndex = 0;
				}
			}
		}


		/// <summary>
		/// 功能：重建二级菜单。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ReCList(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
			{
				DataRowView thisRow = (DataRowView)e.Item.DataItem;

				if (thisRow!=null)
				{
					this._CIndex++;

					int MenuId  = Convert.ToInt32(thisRow["Menu_Id"]);
					string Name = Convert.ToString(thisRow["Name"]);
					string Link = Convert.ToString(thisRow["Link"]);

					HtmlTableCell cell;

					//名称
					cell = (HtmlTableCell)e.Item.FindControl("tdCName");
					if (cell!=null)
					{
                        cell.InnerHtml = string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"{0}\">{1}</a>", Common.Config.DIR_ADMIN + Link, Name);
					}

					//上移
					cell = (HtmlTableCell)e.Item.FindControl("tdCUp");
					if (cell!=null)
					{
						if (this._CIndex>1)
						{
							cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"上移\" onclick=\"listEvent('up', {0})\">", MenuId);
						}
						else
						{
							cell.InnerHtml = "<input type=\"button\" name=\"btn\" class=\"btn\" value=\"上移\" disabled>";
						}
					}

					//下移
					cell = (HtmlTableCell)e.Item.FindControl("tdCDown");
					if (cell!=null)
					{
						if (this._CIndex<this._CCount)
						{
							cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"下移\" onclick=\"listEvent('down', {0})\">", MenuId);
						}
						else
						{
							cell.InnerHtml = "<input type=\"button\" name=\"btn\" class=\"btn\" value=\"下移\" disabled>";
						}
					}

					//编辑
					cell = (HtmlTableCell)e.Item.FindControl("tdCEdit");
					if (cell!=null)
					{
						cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"编辑\" onclick=\"listEvent('edit', {0})\">", MenuId);
					}

					//删除
					cell = (HtmlTableCell)e.Item.FindControl("tdCDel");
					if (cell!=null)
					{
						cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"删除\" onclick=\"listEvent('del', {0})\">", MenuId);
					}
				}
			}
		}
    }
}
