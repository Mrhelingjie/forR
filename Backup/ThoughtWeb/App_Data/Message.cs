using System;    
using System.Data;    
using System.Configuration;   
using System.Web;    
using System.Web.Security;   
using System.Web.UI;   
using System.Web.UI.WebControls;   
using System.Web.UI.WebControls.WebParts;   
using System.Web.UI.HtmlControls;   
using System.Text;   
  
/// <summary>   
/// MessageBox 的摘要说明   
/// </summary>   
public class MessageBox   
{   
    /// <summary>   
    /// 显示消息提示对话框   
    /// </summary>   
    /// <param name="page">当前页面指针，一般为this</param>   
    /// <param name="msg">提示信息</param>   
    public static void Show(string msg)   
    {
        string script = "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>";
        (System.Web.HttpContext.Current.CurrentHandler as Page).ClientScript.RegisterClientScriptBlock(typeof(MessageBox), "AlertMessage", script);
    }

    public static void ShowandClose(string msg)
    {
        string script = "<script language='javascript' defer>alert('" + msg.ToString() + "');window.opener.location=window.opener.location;window.opener=null;window.open('','_self','');window.close();</script>";
        (System.Web.HttpContext.Current.CurrentHandler as Page).ClientScript.RegisterClientScriptBlock(typeof(MessageBox), "AlertMessage", script);
    }

    /// <summary>
    /// 刷新父页面强制关闭弹出页面
    /// </summary>
    public static void Close()
    {
        string script = "<script language='javascript' defer>window.opener.location=window.opener.location;window.opener=null;window.open('','_self','');window.close();</script>";
        (System.Web.HttpContext.Current.CurrentHandler as Page).ClientScript.RegisterClientScriptBlock(typeof(MessageBox), "CurrentWindowClose", script);
    }

    /// <summary>
    /// 显示消息框，并关闭本页面
    /// </summary>
    public static void Close(string msg)
    {
        string script = "<script language='javascript' defer>alert('"+msg+"');window.close();</script>";
        (System.Web.HttpContext.Current.CurrentHandler as Page).ClientScript.RegisterClientScriptBlock(typeof(MessageBox), "CurrentWindowClose", script);
    }



    /// <summary>
    /// 显示消息确认对话框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this.Page</param>
    /// <param name="msg">确认信息</param>
    /// <param name="sureUrl">点确定后跳转的URL</param>
    /// <param name="cancelUrl">点取消后跳转的URL</param>
    public static void Show(System.Web.UI.Page page, string msg, string sureUrl, string cancelUrl)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script language='javascript' defer>");
        sb.AppendFormat("if confirm('{0}');", msg);
        sb.AppendFormat("location.href='{0}';", sureUrl);
        sb.AppendFormat("else location.href='{0}'", cancelUrl);
        sb.Append("</script>");
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "message", sb.ToString());
        //page.RegisterStartupScript("message", sb.ToString());
    }
  
    /// <summary>   
    /// 显示消息提示对话框，并进行页面跳转   
    /// </summary>   
    /// <param name="page">当前页面指针，一般为this</param>   
    /// <param name="msg">提示信息</param>   
    /// <param name="url">跳转的目标URL</param>   
    public static void Show(System.Web.UI.Page page, string msg, string url)   
    {   
        StringBuilder Builder = new StringBuilder();   
        Builder.Append("<script language='javascript' defer>");   
        Builder.AppendFormat("alert('{0}');", msg);
        if (url.IndexOf("\\") == 0) url = "../" + url;
        Builder.AppendFormat("location.href='{0}'", url);   
        Builder.Append("</script>");   
        page.Response.Write(Builder.ToString());
        page.Response.End();
    }

    /// <summary>   
    /// 显示消息提示对话框，并进行页面跳转   
    /// </summary>   
    /// <param name="page">当前页面指针，一般为this</param>   
    /// <param name="msg">提示信息</param>   
    /// <param name="url">跳转的目标URL</param>   
    public static void ParentShow(System.Web.UI.Page page, string msg, string url)
    {
        StringBuilder Builder = new StringBuilder();
        Builder.Append("<script language='javascript' defer>");
        Builder.AppendFormat("alert('{0}');", msg);
        if (url.IndexOf("\\") == 0) url = "../" + url;
        Builder.AppendFormat("window.parent.location.href='{0}'", url);
        Builder.Append("</script>");
        page.Response.Write(Builder.ToString());
        page.Response.End();
    }   
  
    /// <summary>   
    /// 输出自定义脚本信息   
    /// </summary>   
    /// <param name="page">当前页面指针，一般为this</param>   
    /// <param name="script">输出脚本</param>   
    public static void ResponseScript(System.Web.UI.Page page, string script)   
    {   
        page.RegisterStartupScript("message", "<script language='javascript' defer>" + script + "</script>");   
    }   
}
