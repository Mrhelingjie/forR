using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Mejoy.Library;

namespace Mejoy.WebSite.Admin.Admin
{
    public partial class List : BaseAdmin
    {
        private Mejoy.DataAccess.Admin.AdminData ad = new Mejoy.DataAccess.Admin.AdminData();

        private uint _UrlAdminId;
        private string _Key;

        private uint _AdminId;
        private string _LoginName, _Note;
        private byte _Level, _Lock;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VarInit();
            this.DataInit();
        }


        /// <summary>
        /// 参数初始
        /// </summary>
        private void VarInit()
        {
            //每页显示数
            this._PerNum = 50;
            //编号
            this._UrlAdminId = Function.RequestQueryString<uint>("id");
            //搜索字符
            this._Key = Server.UrlDecode(Function.RequestQueryString<string>("key"));
        }//End VarInit()


        /// <summary>
        /// 数据初始
        /// </summary>
        private void DataInit()
        {
            //删除
            if (this._UrlAction == "del" && this._UrlAdminId > 0)
            {
                ad.Delete(this._UrlAdminId);
                if (ad._EventId == 1)
                {
                    Message.Show("抱歉，不能删除最后一个可用超级管理员！");
                }
                else
                {
                    Message.Show("删除成功！");
                }
            }

            //列表
            DataTable dt = ad.Fill(this._PerNum, this._Page, this._Key, out this._TotalNum);
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
            dt.Dispose();

            //分页
            string Url = string.Format("?key={0}", this._Key);
            this.tdPage.InnerHtml = Function.PageList(this._PerNum, this._TotalNum, this._Page, Url);
        }


        /// <summary>
        /// 功能：重建列表。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReList(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView thisRow = (DataRowView)e.Item.DataItem;

                if (thisRow != null)
                {
                    this._No++;
                    HtmlTableCell cell;

                    this._AdminId = Function.ConvertTo<uint>(thisRow["Admin_Id"]);
                    this._LoginName = Convert.ToString(thisRow["Login_Name"]);
                    this._Note = Convert.ToString(thisRow["Note"]);
                    this._Level = Convert.ToByte(thisRow["Level"]);
                    this._Lock = Convert.ToByte(thisRow["Lock"]);

                    //序号
                    cell = (HtmlTableCell)e.Item.FindControl("tdNo");
                    if (cell != null)
                    {
                        cell.InnerText = Convert.ToString((this._Page - 1) * this._PerNum + this._No);
                    }
                    //登录帐号
                    cell = (HtmlTableCell)e.Item.FindControl("tdLoginName");
                    if (cell != null)
                    {
                        cell.InnerText = this._LoginName;
                    }
                    //帐号说明
                    cell = (HtmlTableCell)e.Item.FindControl("tdNote");
                    if (cell != null)
                    {
                        cell.InnerText = this._Note;
                    }
                    //级别
                    cell = (HtmlTableCell)e.Item.FindControl("tdLevel");
                    if (cell != null)
                    {
                        switch (this._Level)
                        {
                            case 0:
                                cell.InnerText = "普通管理员";
                                break;
                            case 99:
                                cell.InnerHtml = "<font color=red>超级管理员</font>";
                                break;
                            default:
                                cell.InnerText = "未知";
                                break;
                        }
                    }
                    //状态
                    cell = (HtmlTableCell)e.Item.FindControl("tdLock");
                    if (cell != null)
                    {
                        cell.InnerHtml = (this._Lock == 1) ? "<font color=red>锁定</font>" : "正常";
                    }
                    //编辑
                    cell = (HtmlTableCell)e.Item.FindControl("tdEdit");
                    if (cell != null)
                    {
                        cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"编辑\" onclick=\"listEvent('edit', {0})\"/>", this._AdminId);
                    }
                    //删除
                    cell = (HtmlTableCell)e.Item.FindControl("tdDel");
                    if (cell != null)
                    {
                        cell.InnerHtml = string.Format("<input type=\"button\" name=\"btn\" class=\"btn\" value=\"删除\" onclick=\"listEvent('del', {0})\"/>", this._AdminId);
                    }
                }
            }
        }
    }
}
