using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mejoy.Library;
using Havsh.Application.Control;
using Havsh.Application.Dal;
using Mejoy.WebSite.Admin;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
namespace ThoughtWeb.Admin.TingChe
{
    public partial class list : BaseAdmin
    {
        Maticsoft.BLL.TingChe bll = new Maticsoft.BLL.TingChe();
        private int _UrlAdminId;
        private string _Key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            SetPageControl();
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
            this._UrlAdminId = Function.RequestQueryString<int>("id");
            //搜索字符
            this._Key = Server.UrlDecode(Function.RequestQueryString<string>("key"));
        }//End VarInit()
        //初始化分页控件参数
        private void SetPageControl()
        {
            pp1.PageMode = PagePilot.PagedMode.URL分页;
            pp1.FirstPageSymbol = "首页";
            pp1.PreviousPageSymbol = "上一页";
            pp1.NextPageSymbol = "下一页";
            pp1.LastPageSymbol = "末页";
        }



        /// <summary>
        /// 数据初始
        /// </summary>
        private void DataInit()
        {
            int cheweishu = Convert.ToInt32(Maticsoft.DBUtility.DbHelperSQL.Query("Select * from CheWeiNumber").Tables[0].Rows[0]["CheWeiShu"]);
            int ruchangshu = Convert.ToInt32(Maticsoft.DBUtility.DbHelperSQL.Query("Select * from TingChe where zhuangtai='入场'").Tables[0].Rows.Count);
            kkk.InnerHtml = "空闲车位" + (cheweishu - ruchangshu).ToString();
            //删除
            if (this._UrlAction == "del" && this._UrlAdminId > 0)
            {
                bll.Delete(this._UrlAdminId);
                Message.Show("删除成功！");
            }

            BindList();
        }

        /// <summary>
        /// 绑定数据列表
        /// </summary>
        protected void BindList()
        {
            string condition = "  1=1 and zhuangtai='入场'";

            int totalRecords;
            int pageIndex = 1;
            int pageSize = Help.GetPageSize();
            if (Request.QueryString["Page"] != null)
            {
                pageIndex = int.Parse(Request.QueryString["Page"]);
            }

            DataTable dt = Help.GetList("TingChe", "*", "ID desc", condition, pageSize, pageIndex, out totalRecords);
            ////当前页多少项
            //ltrAll.Text = dt.Rows.Count.ToString();

            this.rptList.DataSource = dt;
            this.rptList.DataBind();

            //设置分页控件

            ltrRecordCount.Text = "共有" + totalRecords.ToString() + "条记录";

            pp1.RecordCount = totalRecords;
            pp1.PageSize = Help.GetPageSize();
        }
    }
}

