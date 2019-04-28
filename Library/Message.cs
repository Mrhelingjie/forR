using System;
using System.Web;

namespace Mejoy.Library
{
    public class Message : System.Web.UI.Page
    {
        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        public static void Show(string Msg)
        {
            Message.Show(Msg, "", "", 0, "");
        }


        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        /// <param name="ResponseEnd">结束显示。Response.End()</param>
        public static void Show(string Msg, byte ResponseEnd)
        {
            Message.Show(Msg, "", "", ResponseEnd, "");
        }



        public static void Show(string Msg, int backStep)
        {
            string Js = "\n<script language=\"javascript\">\n";
            Js += string.Format("alert(\"{0}\");\n", Msg);
            Js += string.Format("history.back({0});\n", backStep);
            Js += "</script>";

            HttpContext.Current.Response.Write(Js);
        }


        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        /// <param name="Url">跳转地址。</param>
        public static void Show(string Msg, string TrueUrl)
        {
            Message.Show(Msg, TrueUrl, "", 0, "");
        }


        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        /// <param name="Url">跳转地址。</param>
        /// <param name="ResponseEnd">结束显示。Response.End()</param>
        public static void Show(string Msg, string TrueUrl, byte ResponseEnd)
        {
            Message.Show(Msg, TrueUrl, "", ResponseEnd, "");
        }


        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        /// <param name="Url">跳转地址。</param>
        /// <param name="ResponseEnd">结束显示。Response.End()</param>
        /// <param name="Target">跳转窗口</param>
        public static void Show(string Msg, string TrueUrl, byte ResponseEnd, string Target)
        {
            Message.Show(Msg, TrueUrl, "", ResponseEnd, Target);
        }


        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        /// <param name="TrueUrl">条件为true时的跳转地址。</param>
        /// <param name="FalseUrl">条件为false时的跳转地址。</param>
        public static void Show(string Msg, string TrueUrl, string FalseUrl)
        {
            Message.Show(Msg, TrueUrl, FalseUrl, 0, "");
        }


        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        /// <param name="TrueUrl">条件为true时的跳转地址。</param>
        /// <param name="FalseUrl">条件为false时的跳转地址。</param>
        /// <param name="Target">跳转窗口</param>
        public static void Show(string Msg, string TrueUrl, string FalseUrl, string Target)
        {
            Message.Show(Msg, TrueUrl, FalseUrl, 0, Target);
        }


        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        /// <param name="TrueUrl">条件为true时的跳转地址。</param>
        /// <param name="FalseUrl">条件为false时的跳转地址。</param>
        public static void Show(string Msg, string TrueUrl, string FalseUrl, byte ResponseEnd)
        {
            Message.Show(Msg, TrueUrl, FalseUrl, ResponseEnd, "");
        }



        /// <summary>
        /// 功能：Javascript信息提示框。
        /// </summary>
        /// <param name="Msg">提示信息。</param>
        /// <param name="TrueUrl">条件为true时的跳转地址。</param>
        /// <param name="FalseUrl">条件为false时的跳转地址。</param>
        /// <param name="ResponseEnd">结束显示。Response.End()</param>
        /// <param name="Target">跳转窗口</param>
        public static void Show(string Msg, string TrueUrl, string FalseUrl, byte ResponseEnd, string Target)
        {
            string Rnd = DateTime.Now.Ticks.ToString();
            Target = (Target == "" || Target == string.Empty) ? "" : Target + ".";

            if (TrueUrl == "?")
            {
                TrueUrl += Rnd;
            }
            else if (TrueUrl != "" && TrueUrl != "about:blank")
            {
                TrueUrl = (TrueUrl.IndexOf("?") == -1 && TrueUrl.IndexOf("=") == -1) ? TrueUrl + "?tmp=" + Rnd : TrueUrl + "&tmp=" + Rnd;
            }

            if (FalseUrl == "?")
            {
                FalseUrl += Rnd;
            }
            else if (FalseUrl != "" && FalseUrl != "about:blank")
            {
                FalseUrl = (FalseUrl.IndexOf("?") == -1 && FalseUrl.IndexOf("=") == -1) ? FalseUrl + "?rnd=" + Rnd : FalseUrl + "&rnd=" + Rnd;
            }

            string Js = "\n<script language=\"javascript\">\n";

            if (FalseUrl == "")
            {
                if (TrueUrl == "" || TrueUrl == string.Empty)
                {
                    //提示消息，无跳转
                    if (Msg != "" && Msg != string.Empty)
                    {
                        Js += string.Format("alert(\"{0}\");\n", Msg);
                    }
                }
                else
                {
                    //提示消息并跳转到指定页
                    if (Msg != "" && Msg != string.Empty)
                    {
                        Js += string.Format("alert(\"{0}\");\n", Msg);
                    }
                    Js += string.Format("{0}window.location.href=\"{1}\"\n", Target, TrueUrl);
                }
            }
            else
            {
                Js += string.Format("(confirm(\"{0}\")) ? {1}window.location.href=\"{2}\" : {1}window.location.href=\"{3}\";\n", Msg, Target, TrueUrl, FalseUrl);
            }

            Js += "</script>";

            HttpContext.Current.Response.Write(Js);
            if (ResponseEnd == 1)
            {
                HttpContext.Current.Response.End();
            }
        }
    }
}
