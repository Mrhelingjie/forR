using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Mejoy.DataAccess.Admin;
using Mejoy.Library;

namespace Mejoy.WebSite.Admin
{
    /// <summary>
    /// 模块功能：后台管理菜单
    /// 开发人员：阿明
    /// 最后更新：2008-6-19
    /// </summary>
    public partial class Index_Left : BaseAdmin
    {
        private Mejoy.DataAccess.Admin.AdminMenuData menuData = new Mejoy.DataAccess.Admin.AdminMenuData();

        DataTable _Menu = new DataTable("Menu");

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataInit();

            menuData.Dispose();
            menuData = null;

            this._Menu.Dispose();
            this._Menu = null;
        }


        /// <summary>
        /// 数据初始
        /// </summary>
        private void DataInit()
        {
            //所有菜单
            this._Menu = menuData.Fill(Session["UserType"].ToString());

            if (admin._Level == 0)
            {
                this.rptParentMenu.DataSource = this._Menu.Select("Parent_Id=0");
            }
            else
            {
                this.rptParentMenu.DataSource = this._Menu.Select("Parent_Id=0" + ((string.IsNullOrEmpty(admin._Rights)) ? "" : " AND Menu_Id IN(" + admin._Rights + ")"));
            }
            this.rptParentMenu.DataBind();
        }


        /// <summary>
        /// 一级菜单分类重建
        /// </summary>
        protected void ReParentMenuCreate(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow thisRow = (DataRow)e.Item.DataItem;
                if (thisRow != null)
                {
                    uint MenuId = Function.ConvertTo<uint>(thisRow["Menu_Id"]);
                    string Name = Convert.ToString(thisRow["Name"]);
                    byte TreeClose = Convert.ToByte(thisRow["Tree_Close"]);

                    HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl("tdMenuTitle");
                    if (cell != null)
                    {
                        cell.InnerHtml = Name;

                        if (TreeClose == 1)
                        {
                            HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("trLink");
                            if (row != null)
                            {
                                row.Style.Add("display", "none");
                            }
                        }

                        //二级菜单
                        Repeater rpt = (Repeater)e.Item.FindControl("rptChildMenu");
                        DataTable dt = new DataTable();

                        if (rpt != null)
                        {
                            DataRow[] dr = null;
                            if (admin._Level == 100)
                            {
                                dr = this._Menu.Select("Parent_Id=" + MenuId.ToString(), "[Index] DESC, Menu_Id ASC");
                            }
                            else
                            {
                                dr = this._Menu.Select("Parent_Id=" + MenuId.ToString() + ((string.IsNullOrEmpty(admin._Rights)) ? "" : " AND Menu_Id IN(" + admin._Rights + ")"), "[Index] DESC, Menu_Id ASC");
                            }
                            if (dr.Length < 1)
                            {
                                HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("trPMenu");
                                if (row != null)
                                {
                                    row.Visible = false;
                                }
                            }
                            else
                            {
                                rpt.DataSource = dr;
                                rpt.DataBind();
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 二级菜单分类重建
        /// </summary>
        protected void ReChildMenuCreate(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow thisRow = (DataRow)e.Item.DataItem;
                if (thisRow != null)
                {
                    string Name = Convert.ToString(thisRow["Name"]);
                    string Link = Convert.ToString(thisRow["Link"]);
                    byte Target = Convert.ToByte(thisRow["Target"]);

                    HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl("tdLink");
                    if (cell != null)
                    {
                        //添加刷新码
                        if (Link.Trim() != "")
                        {
                            Link = (Link.IndexOf("?") == -1) ? Link + "?" + DateTime.Now.Ticks.ToString() : Link + "&" + DateTime.Now.Ticks.ToString();
                        }

                        //打开窗口
                        string TargetCode = (Target == 1) ? " target=\"_blank\"" : "";

                        if (Link.Length >= 7)
                        {
                            if (Link.Substring(0, 7) != "http://")
                            {
                                Link = Common.Config.DIR_ADMIN + Link;
                            }
                        }

                        cell.InnerHtml = string.Format("<a href=\"{0}\" title=\"{1}\"{2}>{1}</a>", Link, Name, TargetCode);
                    }
                }
            }
        }
    }
}