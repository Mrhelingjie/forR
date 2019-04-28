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
using Mejoy.Library;
using System.Data;

namespace ThoughtWeb.Admin.System
{
    public partial class backup : BaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                Maticsoft.DBUtility.DbHelperSQL.ExecuteSql(@"Backup Database WuZi To disk='D:\WuZi.bak' ");
                MessageBox.Show(@"备份完成，备份路径为D:\WuZi.bak");
            }
            else
            {
             
            }
        }
    }
}