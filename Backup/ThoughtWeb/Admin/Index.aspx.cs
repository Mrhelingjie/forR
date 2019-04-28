using System;

using Mejoy.Common;

namespace Mejoy.WebSite.Admin
{
    public partial class Index : BaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageInit();
        }


        /// <summary>
        /// 功能：页面初始。
        /// </summary>
        private void PageInit()
        {
            Response.Write("<html>\n");
            Response.Write("	<head>\n");
            Response.Write("		<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\n");
            Response.Write("		<title>" + Common.Config.WEB_NAME + " - 游泳馆信息管理系统 [ver:" + Common.Config.WEB_VERSION + "]</title>\n");
            Response.Write("        <script language=\"javascript\">window.status=\"" + Common.Config.WEB_NAME + "--游泳馆信息管理系统[ver:" + Common.Config.WEB_VERSION + "]\"</script>");
            Response.Write("	</head>\n");
            Response.Write("	<frameset rows=\"85,*\" cols=\"*\" frameborder=\"NO\" name=\"adminFrame\" border=\"0\" framespacing=\"0\">\n");
            Response.Write("		<frame src=\"" + Common.Config.DIR_ADMIN + "index_top.aspx?" + DateTime.Now.Ticks.ToString() + "\" name=\"topFrame\" scrolling=\"NO\" noresize>\n");
            Response.Write("		<frameset cols=\"180,*\" frameborder=\"NO\" name=\"footFrame\" border=\"0\" framespacing=\"0\">\n");
            Response.Write("			<frame src=\"" + Common.Config.DIR_ADMIN + "index_left.aspx?" + DateTime.Now.Ticks.ToString() + "\" name=\"leftFrame\" scrolling=\"YES\" noresize>\n");
            Response.Write("			<frame src=\"" + Common.Config.DIR_ADMIN + "index_main.aspx?" + DateTime.Now.Ticks.ToString() + "\" name=\"mainFrame\">\n");
            Response.Write("		</frameset>\n");
            Response.Write("	</frameset>\n");
            Response.Write("	<noframes>\n");
            Response.Write("		<body></body>\n");
            Response.Write("	</noframes>\n");
            Response.Write("</html>\n");
        }
    }
}
