using System.Web;

namespace Mejoy.Library
{
    public class JavaScript : System.Web.UI.Page
    {
        /// <summary>
        /// 功能：javascript alert功能
        /// </summary>
        /// <param name="Msg"></param>
        public static void Alert(string Msg)
        {
            Exec(string.Format("alert(\"{0}\");", Msg));
        }//End Alert();



        /// <summary>
        /// 功能：跳转
        /// </summary>
        /// <param name="Url"></param>
        public static void GoTo(string Url)
        {
            Exec(string.Format("window.location.href=\"{0}\";", Url));
        }//End Goto();


        /// <summary>
        /// 刷新窗口
        /// </summary>
        /// <param name="type">目标窗口类型</param>
        public static void Refresh(Enums.TargetType type)
        {
            string Code = "";
            switch (type)
            {
                case Enums.TargetType.Top:
                    Code = "top.";
                    break;

                case Enums.TargetType.Parent:
                    Code = "parent.";
                    break;

                default:
                case Enums.TargetType.Self:
                    Code = "";
                    break;
            }
            Code += "refresh();";

            Exec(Code);
        }//End Refresh();


        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            Exec("window.close();");
            HttpContext.Current.Response.End();
        }//End CloseWindow();



        /// <summary>
        /// 执行javascript代码
        /// </summary>
        /// <param name="Code"></param>
        public static void Exec(string Code)
        {
            HttpContext.Current.Response.Write(string.Format("<script language=\"javascript\" type=\"text/javascript\">{0}</script>", Code));
        }//End Exec();
    }
}
