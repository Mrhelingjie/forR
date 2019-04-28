using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mejoy.Library;
using System.IO;
using Mejoy.WebSite.Admin;
using System.Reflection;
using System.Data;
namespace ThoughtWeb.Admin.TingChe
{
    public partial class _new : BaseAdmin
    {
        Maticsoft.BLL.TingChe Bll = new Maticsoft.BLL.TingChe();
        Maticsoft.Model.TingChe Model = new Maticsoft.Model.TingChe();
        private int _UrlAdminId;

        private string Name = string.Empty;
        private string Number = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.VarInit();
            if (this.Page.IsPostBack)
            {
                if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].ToString() == "ddl_FangNumber")
                {
                    ddl_BaoXiuNumber_SelectedIndexChanged(null, null);
                }
                else
                {
                    this.SaveData();
                }
            }
            else
            {
                InitDropDown();
                ddl_BaoXiuNumber_SelectedIndexChanged(null, null);
                this.DataInit();
                
            }
        }


        private void InitDropDown()
        {
           
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
                    Message.Show("抱歉，此记录不存在！", "list.aspx", 1);
                }
                else
                {
                    PropertyInfo[] info = Model.GetType().GetProperties();
                    int i = 0;
                    foreach (PropertyInfo temp in info)
                    {

                        Control con = FindControl("tb" + temp.Name);
                        if (con == null)
                        {
                            con = FindControl("ddl_" + temp.Name);
                            if (con != null)
                            {
                                ((DropDownList)con).Text = temp.GetValue(Model, null).ToString();
                            }
                        }
                        else
                        {
                            ((TextBox)con).Text = temp.GetValue(Model, null).ToString();

                        }
                        i++;
                    }
                }
            }
        }
        /// <summary>
        /// 检查输入
        /// </summary>
        private void CheckInput()
        {

            int cheweishu =Convert.ToInt32(Maticsoft.DBUtility.DbHelperSQL.Query("Select * from CheWeiNumber").Tables[0].Rows[0]["CheWeiShu"]);
            int ruchangshu = Convert.ToInt32(Maticsoft.DBUtility.DbHelperSQL.Query("Select * from TingChe where zhuangtai='入场'").Tables[0].Rows.Count);
            if (cheweishu- ruchangshu <= 0)
            {
                this._Error = true;
                this._ErrorMsg = "停车位已满！";
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
                PropertyInfo[] info = Model.GetType().GetProperties();
                foreach (PropertyInfo temp in info)
                {

                    Control con = FindControl("tb" + temp.Name);
                    if (con == null)
                    {
                        con = FindControl("ddl_" + temp.Name);

                        if (con != null)
                        {
                            temp.SetValue(Model, ((DropDownList)con).Text, null);
                        }
                    }
                    else
                    {
                        temp.SetValue(Model, ((TextBox)con).Text, null);

                    }
                }
                Model.RuChangShiJian = DateTime.Now.ToString();
                Model.ZhuangTai = "入场";
                Bll.Add(Model);
                Message.Show("添加成功！继续添加数据吗？", "?", "list.aspx", 1);
            }
            else
            {
                Model = Bll.GetModel(PTool.String2Int(tbAdminId.Value));
                PropertyInfo[] info = Model.GetType().GetProperties();
                foreach (PropertyInfo temp in info)
                {

                    Control con = FindControl("tb" + temp.Name);
                    if (con == null)
                    {
                        con = FindControl("ddl_" + temp.Name);

                        if (con != null)
                        {
                            temp.SetValue(Model, ((DropDownList)con).Text, null);
                        }
                    }
                    else
                    {
                        temp.SetValue(Model, ((TextBox)con).Text, null);

                    }
                }
                
                Bll.Update(Model);
                Message.Show("编辑成功！返回数据列表吗？", "list.aspx", "?do=edit&id=" + this.tbAdminId.Value.ToString(), 1);
            }
        }

        protected void ddl_BaoXiuNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                           }
            catch { }
        }
    }
}