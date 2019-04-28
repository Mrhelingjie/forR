using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
///PTool 的摘要说明
/// </summary>
public class PTool : System.Web.UI.Page
{
    public enum ImageBelong 
    {
        /// <summary>
        /// JobOrderForm
        /// </summary>
        Default = 0,

        /// <summary>
        /// StockOrderForm
        /// </summary>
        StockOrderForm = 1
    }

    public PTool()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 获取url参数值
    /// </summary>
    /// <param name="val"></param>
    /// <param name="currentPage"></param>
    /// <param name="isInteger"></param>
    /// <returns>int</returns>
    public static int RequestQueryString(string val, Page currentPage, bool isInteger)
    {
        return String2Int(currentPage.Request.QueryString[val]);
    }

    /// <summary>
    /// 获取url参数值
    /// </summary>
    /// <param name="val"></param>
    /// <param name="currentPage"></param>
    /// <returns>string</returns>
    public static string RequestQueryString(string val, Page currentPage)
    {
        string tmp = currentPage.Request.QueryString[val];
        if (!string.IsNullOrEmpty(tmp))
        {
            return tmp.Trim();
        }
        else
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 字符串转int
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static int String2Int(string val)
    {
        int result = -1;
        bool success = int.TryParse(val, out result);
        if (success)
        {
            return result;
        }
        else
        {
            return -1;
        }
    }

    /// <summary>
    /// 字符串转浮点值
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static float String2Float(string val)
    {
        float result = -1;
        bool success = float.TryParse(val, out result);
        if (success)
        {
            return result;
        }
        else
        {
            return -1;
        }
    }

    /// <summary>
    /// 验证字符串是否为DateTime格式
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static bool IsValidDateTime(string val)
    { 
        DateTime dtTmp = DateTime.MinValue;
        return DateTime.TryParse(val, out dtTmp);
    }

    /// <summary>
    /// 字符串转DateTime格式
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static DateTime String2DateTime(string val)
    {
        if (IsValidDateTime(val))
        {
            return DateTime.Parse(val);
        }
        else
        {
            return DateTime.MaxValue;
            //throw new Exception("\"" + val + "\"不是一个有效的日期数据格式！");
        }
    }

    /// <summary>
    /// DateTime转字符串
    /// </summary>
    /// <param name="dt"></param>
    /// <returns>带时间的长日期格式</returns>
    public static string DateTime2String(DateTime dt)
    {
        if (dt.Equals(DateTime.MinValue))
            return string.Empty;
        return dt.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// DateTime转字符串
    /// </summary>
    /// <param name="dt"></param>
    /// <returns>短日期格式</returns>
    public static string Date2String(DateTime dt)
    {
        if (dt.Equals(DateTime.MinValue))
            return string.Empty;
        return dt.ToString("yyyy-MM-dd");
    }

    /// <summary>
    /// DateTime转小时：分格式
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string Time2String(DateTime dt)
    {
        return dt.ToString("HH:mm");
    }

    /// <summary>
    /// 字符串转Decimal
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static decimal String2Decimal(string val)
    {
        decimal result = 0;
        bool success = decimal.TryParse(val, out result);
        if (success)
        {
            return result;
        }
        return 0;
    }

    /// <summary>
    /// 验证字符串是否为常用图片格式
    /// </summary>
    /// <param name="ext">文件扩展名，如：.gif</param>
    /// <returns></returns>
    public static bool IsValidImageExt(string ext)
    {
        ext = ext.ToLower();
        return (ext != ".gif" && ext != ".jpg" && ext != ".bmp" && ext != ".png") ? false : true;
    }
}
