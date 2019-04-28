using System;
using System.Configuration;
using Maticsoft.DBUtility;
using System.Data;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/// <summary>
///Help 的摘要说明
/// </summary>
public class Help
{
    public static int GetPageSize()
    {
        return int.Parse(ConfigurationManager.AppSettings["PageSize"]);
    }
    public static int GetDataSize()
    {
        return int.Parse(ConfigurationManager.AppSettings["DataSize"]);
    }
    public static int GetActivitySize()
    {
        return int.Parse(ConfigurationManager.AppSettings["ActivitySize"]);
    }

    public static int GetRentSize()
    {
        return int.Parse(ConfigurationManager.AppSettings["RentSize"]);
    }



    public static string GetDefaultPassWord()
    {
        return ConfigurationManager.AppSettings["DefaultPassWord"].ToString() ;
    }

    /// <summary>
    /// 分页函数
    /// </summary>
    /// <param name="tableName">要进行分页的表，也可以用联接，如dbo.employee或dbo.employee INNER JOIN dbo.jobs ON (dbo.employee.job_id=dbo.jobs.job_id)</param>
    /// <param name="fields">表中的字段，可以使用*代替</param>
    /// <param name="orderFields">要排序的字段</param>
    /// <param name="strWhere">WHERE子句</param>
    /// <param name="pageSize">分页的大小</param>
    /// <param name="pageIndex">要显示的页的索引</param>
    /// <param name="count">总记录数</param>
    /// <returns>DataTable</returns>
    public static DataTable GetList(string tableName, string fields, string orderFields, string strWhere, int pageSize, int pageIndex, out int count)
    {
        return DbHelperSQL.GetList(tableName, fields, orderFields, strWhere, pageSize, pageIndex, out count);
    }

    public static void BindDdlFromData(DataSet ds, System.Web.UI.WebControls.DropDownList ddl, bool isSelect, string valueFiele, string textField)
    {
        ddl.Items.Clear();
        if (isSelect)
        {
            ddl.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "请选择"));
        }
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ddl.Items.Add(new System.Web.UI.WebControls.ListItem(dr[textField].ToString(), dr[valueFiele].ToString()));
        }
    }

    /// <summary>
    /// DataTable导出到Excel
    /// </summary>
    /// <param name="dt">导出源</param>
    /// <param name="fileName">文件名带后缀</param>
    //public static void ExportExcel(DataTable dt, string fileName)
    //{
    //    string filepath = HttpContext.Current.Server.MapPath(fileName);

    //    Excel.Application xlApp = new Excel.Application();

    //    Excel.Workbooks w = xlApp.Workbooks;

    //    Excel.Workbook workbook = w.Add(Excel.XlWBATemplate.xlWBATWorksheet);

    //    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

    //    //写入字段

    //    for (int i = 0; i < dt.Columns.Count; i++)
    //    {

    //        worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;

    //    }

    //    //写入数值

    //    for (int r = 0; r < dt.Rows.Count; r++)
    //    {

    //        for (int i = 0; i < dt.Columns.Count; i++)
    //        {

    //            worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i];

    //        }

    //    }

    //    worksheet.Columns.EntireColumn.AutoFit();//列宽自适应。

    //    workbook.Saved = true;

    //    workbook.SaveCopyAs(filepath);

    //    xlApp.Quit();

    //    GC.Collect();//强行销毁

    //    HttpContext.Current.Response.Buffer = true;

    //    HttpContext.Current.Response.Clear();

    //    HttpContext.Current.Response.ContentType = "application/ms-excel";

    //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filepath));

    //    HttpContext.Current.Response.WriteFile(filepath);

    //    HttpContext.Current.Response.Flush();

    //    HttpContext.Current.Response.End();
    //}

    #region Added by lyh
    /// <summary>
    /// 获得建议选项
    /// </summary>
    /// <param name="page">一般为this</param> 
    //public static void GetSuggestions(System.Web.UI.Page page)
    //{
    //    if (!string.IsNullOrEmpty(page.Request["keywords"])
    //        && !string.IsNullOrEmpty(page.Request["tableName"])
    //        && !string.IsNullOrEmpty(page.Request["columnName"])
    //        && !string.IsNullOrEmpty(page.Request["count"]))
    //    {
    //        int count;//获得前多少条数据
    //        if (!Int32.TryParse(page.Request["count"], out count))
    //        {
    //            count = 10;
    //        }
    //        //设置参数
    //        string keywords = GetConditions(page.Request["keywords"]);//关键字
    //        string tableName = page.Request["tableName"];//表名
    //        string columnName = page.Request["columnName"];//列名
    //        string chinese = GetChinese(page.Request["keywords"]);
    //        //SQL查询语句
    //        string sql = "SELECT TOP {0} {1} FROM {2} WHERE dbo.F_GetHelpCode({1}) LIKE '{3}' AND {1} LIKE '{4}' ORDER BY {1}";
    //        sql = string.Format(sql, count, columnName, tableName, keywords, chinese);

    //        StringBuilder sb = new StringBuilder();
    //        DataSet ds = DbHelperSQL.Query(sql);
    //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow row in ds.Tables[0].Rows)
    //            {
    //                sb.Append(row[0]);
    //                sb.Append("@@|@@");
    //            }
    //        }
    //        else
    //        {
    //            sb.Append("没有匹配项！");
    //        }
    //        page.Response.Clear();
    //        page.Response.Write(sb.ToString().TrimEnd('@', '|'));
    //        page.Response.End();
    //    }
    //}

    /// <summary>
    /// 获得字符串中汉字的首字母
    /// </summary>
    /// <param name="sug">字符串</param>
    /// <returns>首字母</returns>
    private static string GetConditions(string sug)
    {
        StringBuilder sb = new StringBuilder();
        //string sql = string.Format("SELECT dbo.F_GetHelpCode('{0}')", sug.Replace("'", ""));
        //DataSet ds = DbHelperSQL.Query(sql);
        //if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
        //{
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        string s = (string)row[0];
        //        foreach (char c in s)
        //        {
        //            sb.Append(c);
        //            sb.Append("%");
        //        }
        //    }
        //}
        return sb.ToString();
    }

    /// <summary>
    /// 获得字符串中的汉字
    /// </summary>
    /// <param name="sug">字符串</param>
    /// <returns>汉字</returns>
    private static string GetChinese(string sug)
    {
        StringBuilder sb = new StringBuilder();
        CharEnumerator ce = sug.GetEnumerator();
        Regex regex = new Regex("^[\u4E00-\u9FA5]{0,}$");
        while (ce.MoveNext())
        {
            if (regex.IsMatch(ce.Current.ToString(), 0))
            {
                sb.Append("%");
                sb.Append(ce.Current.ToString());
            }
        }
        sb.Append("%");
        return sb.ToString();
    }

    /// <summary>
    /// 截取指定长度的字符串
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="length">长度</param>
    /// <returns>字符串</returns>
    public static string CutStr(string str, int length)
    {
        if (str.Length > length)
        {
            return str.Substring(0, length) + "…";
        }
        else
        {
            return str;
        }
    }

    /// <summary>
    /// 根据用户名模糊查询，查询出匹配的用户工号
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    //public static string GetUserCodeListByUserName(string userName)
    //{
    //    Maticsoft.BLL.st_Employee bll = new Maticsoft.BLL.st_Employee();
    //    IList<Maticsoft.Model.st_Employee> lst = bll.GetModelList(string.Format("RealName LIKE '%{0}%'", userName));
    //    StringBuilder sb = new StringBuilder();
    //    if (lst != null && lst.Count > 0)
    //    {
    //        for (int i = 0; i < lst.Count; i++)
    //        {
    //            sb.Append(string.Format("'{0}',", lst[i].WorkCode));
    //        }
    //    }
    //    return sb.ToString().Trim().TrimEnd(',');
    //}

    /// <summary>
    /// 获得指定员工号的真实姓名
    /// </summary>
    /// <param name="workCode">员工号</param>
    /// <returns>真实姓名</returns>
    //public static string GetUserName(string workCode)
    //{
    //    Maticsoft.BLL.st_Employee bll = new Maticsoft.BLL.st_Employee();
    //    return bll.GetNameByID(workCode);
    //}
    #endregion
}

