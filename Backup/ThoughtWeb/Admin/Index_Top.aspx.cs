using System;

using Mejoy.Library;

namespace Mejoy.WebSite.Admin
{
    public partial class Index_Top : BaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataInit();
        }


        /// <summary>
        /// 页面初始。
        /// </summary>
        private void DataInit()
        {
            string[] _Week = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            this.tdDate.InnerHtml = string.Format("{0} {1}", DateTime.Now.ToString("yyyy年M月d日"), _Week[(int)DateTime.Now.DayOfWeek]);
            this.labAdmin.Text = admin._LoginName;
        }
    }
}
