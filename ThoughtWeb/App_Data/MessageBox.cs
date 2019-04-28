using System;
using System.Collections.Generic;

using System.Text;
/*
 *  名称：MessageBox
 *  功能：显示消息框
 *  作者：lyh
 *  日期：2010-08-01
 */

namespace CommonLib.WebUtility
{
    /// <summary>
    /// 显示消息框。
    /// </summary>
    public class MessageBox
    {
        //英文标点符号
        private static string[] en = { "'", "\"" };
        //中文标点符号
        private static string[] cn = { "‘", "’", "“", "”" };
        //脚本
        private static string script = "";

        /// <summary>
        /// 显示消息提示对话框。
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="text">要在消息框中显示的文本</param>
        public static void Alert(System.Web.UI.Page page,string text)
        {
            script = string.Format("<script language='javascript' defer>alert('{0}');</script>", EnToCn(text));
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", script);
        }

        /// <summary>
        /// 点击控件，消息确认提示框。
        /// </summary>
        /// <param name="webControl">当前页面指针，一般为this</param>
        /// <param name="text">要在消息框中显示的文本</param>
        public static void Confirm(System.Web.UI.WebControls.WebControl webControl, string text)
        {
            script = string.Format("return confirm('{0}');", EnToCn(text));
            webControl.Attributes.Add("onclick", script);
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转。
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="url">跳转的目标url</param>
        public static void AlertAndRedirect(System.Web.UI.Page page, string text, string url)
        {
            script = string.Format("<script language='javascript' defer>alert('{0}');window.location='{1}';</script>", EnToCn(text), url);
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", script);
        }

        /// <summary>
        /// 显示消息确认提示框，并进行页面跳转。
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="url">跳转的目标url</param>
        public static void ConfirmAndRedirect(System.Web.UI.Page page, string text, string url)
        {
            script = string.Format("<script language='javascript' defer>if(confirm('{0}'))window.location='{1}';</script>", EnToCn(text), url);
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", script);
        }
        
        /// <summary>
        /// 输出自定义脚本信息。
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">要页面中输出的脚本</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            script = string.Format("<script language='javascript' defer>{0}</script>", script);
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", script);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="url">HttpHandler路径</param>
        /// <param name="param">参数</param>
        public static void IntervalGetMessage(System.Web.UI.Page page, string url, string param)
        {
            string script = @"function IntervalGetMessage(){{var object = new Class(Ajax,['POST','{0}',MessageCallBack,'{1}']);}}";
            script = string.Format(script, url, param);
            ResponseScript(page, script);
        }

        /// <summary>
        /// 关闭弹出页面，同时刷新父页面
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="text">提示消息，如果不需要提示则设置为空</param>
        public static void Close(System.Web.UI.Page page, string text)
        {
            string script =string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                script = string.Format("alert('{0}');", EnToCn(text));
            }
            script += "if(opener!=null && !opener.closed){opener.location.href=opener.location.href;opener=null;}window.close();";
            ResponseScript(page, script);
        }

        /// <summary>
        /// 刷新父页面
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="text">提示消息，如果不需要提示则设置为空</param>
        public static void Refresh(System.Web.UI.Page page, string text)
        {
            string script = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                script = string.Format("alert('{0}');", EnToCn(text));
            }
            script += "if(opener!=null && !opener.closed){opener.location.href=opener.location.href;opener=null;}";
            ResponseScript(page, script);
        }

        #region EnToCn
        /// <summary>
        /// 把英文标点符号转换为中文标点符号。
        /// </summary>
        /// <param name="text">转换前的文本</param>
        /// <returns>转换后的文本</returns>
        private static string EnToCn(string text)
        {          
            if (text != "" && text != null)
            {
                int index = text.IndexOf(en[0]);
                int flag = 0;
                while (index != -1)
                {
                    text = text.Remove(index, 1).Insert(index, cn[flag % 2]);
                    index = text.IndexOf(en[0]);
                    flag++;
                }

                index = text.IndexOf(en[1]);
                flag = 0;
                while (index != -1)
                {
                    text = text.Remove(index, 1).Insert(index, cn[flag % 2 + 2]);
                    index = text.IndexOf(en[1]);
                    flag++;
                }
            }
            return text;
        }
        #endregion
    }
}
