﻿using System;
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
namespace ThoughtWeb.Admin.ChuChang
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
         
            DataTable dt = Maticsoft.DBUtility.DbHelperSQL.Query("Select * from TingChe where zhuangtai='入场' and ChePai='"+tbChePai.Text+"'").Tables[0];
            if (dt.Rows.Count <= 0)
            {
                this._Error = true;
                this._ErrorMsg = "该车辆无入场记录！";
            }
            else
            {
                tbAdminId.Value = dt.Rows[0]["ID"].ToString();

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
                Model.ChuChangShiJian = DateTime.Now.ToString();
                Model.ZhuangTai = "出场";
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
                Model.ChuChangShiJian = DateTime.Now.ToString();
                Model.ZhuangTai = "出场";
                Maticsoft.BLL.ShouFeiType TBll = new Maticsoft.BLL.ShouFeiType();
                List<Maticsoft.Model.ShouFeiType> TList = TBll.GetModelList("1=1");
                Maticsoft.BLL.huiYuan HBll = new Maticsoft.BLL.huiYuan();
                List<Maticsoft.Model.huiYuan> HList = HBll.GetModelList("ChePai='"+Model.ChePai+"'");
                TimeSpan ts = Convert.ToDateTime(Model.ChuChangShiJian).Subtract(Convert.ToDateTime(Model.RuChangShiJian));
                double hours = ts.TotalHours;
                if (HList.Count > 0)
                {
                   double jin=Convert.ToDouble(TList.Find(m=>m.LeiXing=="包月").Jine);
                    Model.JinE = (jin * hours).ToString("0.0");
                    Model.ShouFeiXingShi = "包月";
                }
                else
                {
                    double jin = Convert.ToDouble(TList.Find(m => m.LeiXing == "临停").Jine);
                    Model.JinE = (jin * hours).ToString("0.0");
                    Model.ShouFeiXingShi = "临停";
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