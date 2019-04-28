using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text.RegularExpressions;

[assembly: WebResource("Havsh.Application.Control.PagePilot.PagePilotControl.js", "application/x-javascript")]
namespace Havsh.Application.Control
{
    /// <summary>
    /// 分页导航条
    /// </summary>
    public class PagePilot : WebControl, INamingContainer
    {
        #region 字段
        private bool _hasDescription = true;
        private string _descriptionMessage = "当前页:{0}/共{1}页";
        private int _pageCount = 1;
        private int _recordCount;
        private int _pageSize;
        private int _separtorWidth = 5;
        private string _firstPageSymbol = "<<";
        private string _previousPageSymbol = "<";
        private string _nextPageSymbol = ">";
        private string _lastPageSymbol = ">>";
        private string _symbol = "Page";
        private int _pageNavigatorItemNumber = 5;
        private PagedMode _pagedMode = PagedMode.按钮分页;
        private int _start;
        private int _stop;
        private string _url;
        #endregion

        #region 属性
        [Description("设置页面描述开关")]
        public bool HasDescription
        {
            get { return _hasDescription; }
            set { _hasDescription = value; }
        }
        [Description("设置页面描述字符串格式")]
        public string DescriptionMessage
        {
            get { return _descriptionMessage; }
            set { _descriptionMessage = value; }
        }
        [Description("设置当前页面索引")]
        public int PageIndex
        {
            get
            {
                int pageIndex = Convert.ToInt32(ViewState["PageIndex"]);
                if (pageIndex == 0)
                {
                    return 1;
                }
                else
                {
                    return pageIndex;
                }
            }
            set { ViewState["PageIndex"] = value; }
        }
        [Description("设置页面总数")]
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }
        [Description("设置记录集总数")]
        public int RecordCount
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }
        [Description("设置每页显示多少条信息")]
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        [Description("设置导航条分隔间距宽度")]
        public int SepartorWidth
        {
            get { return _separtorWidth; }
            set { _separtorWidth = value; }
        }

        [Description("设置下一页文本样式")]
        public string NextPageSymbol
        {
            get { return _nextPageSymbol; }
            set { _nextPageSymbol = value; }
        }
        [Description("设置上一页文本样式")]
        public string PreviousPageSymbol
        {
            get { return _previousPageSymbol; }
            set { _previousPageSymbol = value; }
        }
        [Description("设置尾页文本样式")]
        public string LastPageSymbol
        {
            get { return _lastPageSymbol; }
            set { _lastPageSymbol = value; }
        }
        [Description("设置首页文本样式")]
        public string FirstPageSymbol
        {
            get { return _firstPageSymbol; }
            set { _firstPageSymbol = value; }
        }
        [Description("设置URL分页方式的符号")]
        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }
        [Description("设置页面导航栏数目")]
        public int PageNavigatorItemNumber
        {
            get { return _pageNavigatorItemNumber; }
            set { _pageNavigatorItemNumber = value; }
        }
        [Description("设置分页方式")]
        public PagedMode PageMode
        {
            get { return _pagedMode; }
            set { _pagedMode = value; }
        }
        #endregion

        #region 控件
        private Literal _description;
        private Label _itemContainer;
        private TextBox _txtDestinationPage;
        #endregion

        #region 构造函数
        public PagePilot() : base(HtmlTextWriterTag.Div) { }
        #endregion

        #region 生成子控件
        protected override void CreateChildControls()
        {
            CalculatePageCount();

            Controls.Clear();

            _description = new Literal();
            _itemContainer = new Label();

            if (_pagedMode == PagedMode.按钮分页)
            {
                CreateNavigator_ButtonMode();
            }
            else
            {
                _url = GetPageUrl();
                CreateNavigator_UrlMode();
            }

            _txtDestinationPage = new TextBox();
            _txtDestinationPage.ID = string.Format("{0}_Destination", ClientID);
            _txtDestinationPage.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;");
            _txtDestinationPage.Attributes.Add("style", "width:40px;height:12px");

            Controls.Add(_itemContainer);
            
        }
        #endregion

        #region 呈现
        protected override void OnPreRender(EventArgs e)
        {
            Page.Header.Controls.Add(new LiteralControl("<script type=\"text/javascript\" charset=\"gb2312\" src=\"" + this.Page.ClientScript.GetWebResourceUrl(typeof(PagePilot), "Havsh.Application.Control.PagePilot.PagePilotControl.js") + "\"></script>"));
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write("<!---------------XQ分页控件--------------->");
            base.RenderBeginTag(writer);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            EnsureChildControls();

            writer.RenderBeginTag("table");

            writer.RenderBeginTag("tr");

            if (_hasDescription)
            {
                writer.RenderBeginTag("td");
                _description.Text = string.Format(_descriptionMessage, PageIndex, _pageCount);
                _description.RenderControl(writer);

                writer.AddAttribute("id", string.Format("{0}_PageCount", ClientID));
                writer.AddAttribute("value", PageCount.ToString());
                writer.AddAttribute("type", "hidden");
                writer.RenderBeginTag("input");
                writer.RenderEndTag();

                writer.RenderEndTag();
            }

            if (_pagedMode == PagedMode.按钮分页)
            {
                //首页
                writer.RenderBeginTag("td");
                //重置按钮内容
                ControlReset(_itemContainer.Controls[0], _firstPageSymbol, 1);
                _itemContainer.Controls[0].RenderControl(writer);
                writer.RenderEndTag();

                //前一页
                writer.RenderBeginTag("td");
                int tempPageIndex = PageIndex - 1 > 0 ? PageIndex - 1 : 1;
                ControlReset(_itemContainer.Controls[1], _previousPageSymbol, tempPageIndex);
                _itemContainer.Controls[1].RenderControl(writer);
                writer.RenderEndTag();

                //中间导航Item
                GetStart_StopIndex(ref _start, ref _stop);
                int tempIndex = 2;
                for (int i = _start; i <= _stop; i++)
                {
                    writer.RenderBeginTag("td");
                    ControlReset(_itemContainer.Controls[tempIndex], i.ToString(), i);
                    _itemContainer.Controls[tempIndex].RenderControl(writer);
                    tempIndex++;
                    writer.RenderEndTag();
                }

                //下一页
                writer.RenderBeginTag("td");
                tempPageIndex = PageIndex + 1 > _pageCount ? _pageCount : PageIndex + 1;
                ControlReset(_itemContainer.Controls[_itemContainer.Controls.Count - 2], _nextPageSymbol, tempPageIndex);
                _itemContainer.Controls[_itemContainer.Controls.Count - 2].RenderControl(writer);
                writer.RenderEndTag();

                //尾页
                writer.RenderBeginTag("td");
                ControlReset(_itemContainer.Controls[_itemContainer.Controls.Count - 1], _lastPageSymbol, _pageCount);
                _itemContainer.Controls[_itemContainer.Controls.Count - 1].RenderControl(writer);
                writer.RenderEndTag();
            }
            else
            {
                int count = _itemContainer.Controls.Count;

                if (HttpContext.Current.Request.QueryString[_symbol] != null)
                {
                    PageIndex = int.Parse(HttpContext.Current.Request.QueryString[_symbol]);
                }
                else
                {
                    PageIndex = 1;
                }

                for (int i = 0; i < count; i++)
                {
                    writer.RenderBeginTag("td");
                    _itemContainer.Controls[i].RenderControl(writer);
                    writer.RenderEndTag();
                }

                writer.RenderBeginTag("td");
                writer.Write("跳转到");
                writer.RenderEndTag();

                writer.RenderBeginTag("td");
                _txtDestinationPage.RenderControl(writer);
                writer.RenderEndTag();

                writer.RenderBeginTag("td");

                writer.AddAttribute("value", "Go");
                writer.AddAttribute("type", "button");
                writer.AddAttribute("style", "width:30px;border:1px solid gray;height:18px;");
                writer.AddAttribute("onclick", "PagePilot_Redirect('" + _txtDestinationPage.ID + "','" + string.Format("{0}_PageCount", ClientID) + "','" + _symbol + "')");
                writer.RenderBeginTag("input");
                writer.RenderEndTag();

                writer.RenderEndTag();
            }

            writer.RenderEndTag();

            writer.RenderEndTag();
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
            writer.Write("<!-------------End XQ分页控件------------->");
        }
        #endregion

        #region LinkButton_按钮方式
        private void CreateNavigator_ButtonMode()
        {
            //首页
            CreateNavigatorItem_ButtonMode(_firstPageSymbol, 1);
            //前一页
            int tempPageIndex = PageIndex - 1 > 0 ? PageIndex - 1 : 1;
            CreateNavigatorItem_ButtonMode(_previousPageSymbol, tempPageIndex);

            GetStart_StopIndex(ref _start, ref _stop);

            for (int i = _start; i <= _stop; i++)
            {
                CreateNavigatorItem_ButtonMode(i.ToString(), i);
            }

            //下一页
            tempPageIndex = PageIndex + 1 > _pageCount ? _pageCount : PageIndex + 1;
            CreateNavigatorItem_ButtonMode(_nextPageSymbol, tempPageIndex);
            //尾页
            CreateNavigatorItem_ButtonMode(_lastPageSymbol, _pageCount);
        }
        private void CreateNavigatorItem_ButtonMode(string text, int pageIndex)
        {
            LinkButton item = new LinkButton();
            item.Text = text;
            item.CommandArgument = pageIndex.ToString();
            item.Click += new EventHandler(item_Click);
            _itemContainer.Controls.Add(item);
        }
        #endregion

        #region HyperLink_URL方式
        private void CreateNavigator_UrlMode()
        {
            if (!DesignMode)
            {
                int currentPageIndex = 1;

                if (HttpContext.Current.Request[_symbol] != null)
                {
                    currentPageIndex = int.Parse(HttpContext.Current.Request[_symbol]);
                }

                //首页
                CreateNavigatorItem_UrlMode(_firstPageSymbol, 1, currentPageIndex);
                //前一页
                int tempIndex = currentPageIndex - 1 > 0 ? currentPageIndex - 1 : 1;
                CreateNavigatorItem_UrlMode(_previousPageSymbol, tempIndex, currentPageIndex);

                int start;
                int stop;
                GetStart_StopIndex_Url(out start, out stop, currentPageIndex);

                for (int i = start; i <= stop; i++)
                {
                    CreateNavigatorItem_UrlMode(i.ToString(), i, currentPageIndex);
                }

                //下一页
                tempIndex = currentPageIndex + 1 > _pageCount ? _pageCount : currentPageIndex + 1;
                CreateNavigatorItem_UrlMode(_nextPageSymbol, tempIndex, currentPageIndex);
                //尾页
                CreateNavigatorItem_UrlMode(_lastPageSymbol, _pageCount, currentPageIndex);
            }
        }
        private void CreateNavigatorItem_UrlMode(string text, int pageIndex, int currentPageIndex)
        {
            HyperLink item = new HyperLink();
            item.Text = text;
            item.NavigateUrl = _url.Replace("{0}", string.Format("{0}={1}", _symbol, pageIndex));
            if (pageIndex == currentPageIndex)
            {
                item.Enabled = false;
            }
            _itemContainer.Controls.Add(item);
        }
        private
        #endregion

        #region 事件
        static readonly object PageChangeEventKey = new object();
        public delegate void PageChangeEventHandler(object sender, PageChangeEventArgs e);
        public event PageChangeEventHandler PageChange
        {
            add { Events.AddHandler(PageChangeEventKey, value); }
            remove { Events.RemoveHandler(PageChangeEventKey, value); }
        }
        private void OnPageChange(PageChangeEventArgs e)
        {
            PageChangeEventHandler handler = (PageChangeEventHandler)Events[PageChangeEventKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }
        private void item_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            PageChangeEventArgs e1 = new PageChangeEventArgs(int.Parse(lb.CommandArgument));
            OnPageChange(e1);
        }
        #endregion

        #region 方法
        private void CalculatePageCount()
        {
            if (_recordCount != 0 && _pageSize != 0)
            {
                int temp1 = _recordCount % _pageSize;
                int temp2 = _recordCount / _pageSize;
                if (temp1 > 0)
                {
                    temp2++;
                }
                _pageCount = temp2;
            }
        }
        private void GetStart_StopIndex(ref int _start, ref int _stop)
        {
            int avgItemNumber = _pageNavigatorItemNumber / 2;

            _start = PageIndex - avgItemNumber > 0 ? PageIndex - avgItemNumber : 1;

            int leftItemNumber = _start != 1 ? avgItemNumber : PageIndex - 1;

            int remainItemNumber = _pageNavigatorItemNumber - leftItemNumber;

            _stop = PageIndex + remainItemNumber - 1;

            while (_stop > _pageCount)
            {
                if (_start - 1 > 0)
                {
                    _start--;
                }
                _stop--;
            }
        }
        private void GetStart_StopIndex_Url(out int start, out int stop, int pageIndex)
        {
            int avgItemNumber = _pageNavigatorItemNumber / 2;

            start = pageIndex - avgItemNumber > 0 ? pageIndex - avgItemNumber : 1;

            int leftItemNumber = start != 1 ? avgItemNumber : pageIndex - 1;

            int remainItemNumber = _pageNavigatorItemNumber - leftItemNumber;

            stop = pageIndex + remainItemNumber - 1;

            while (stop > _pageCount)
            {
                if (start - 1 > 0)
                {
                    start--;
                }
                stop--;
            }
        }
        private string GetPageUrl()
        {
            if (!DesignMode)
            {

                Regex gex = new Regex(string.Format(@"{0}=\d*", _symbol));

                string input = HttpContext.Current.Request.RawUrl;

                Match matchObject = gex.Match(input);
                if (matchObject.Success)
                {
                    input = input.Replace(matchObject.Value, "{0}");
                }
                else
                {
                    if (input.Contains("?"))
                    {
                        input = input + "&{0}";
                    }
                    else
                    {
                        input = input + "?{0}";
                    }
                }
                return input;
            }
            return "";
        }
        private void ControlReset(System.Web.UI.Control c, string text, int pageIndex)
        {
            if (c is LinkButton)
            {
                LinkButton item = c as LinkButton;
                item.Text = text;
                item.CommandArgument = pageIndex.ToString();
                if (pageIndex == PageIndex)
                {
                    item.Enabled = false;
                }
            }
        }
        #endregion

        public class PageChangeEventArgs : EventArgs
        {
            private int _newPageIndex;

            public int NewPageIndex
            {
                get { return _newPageIndex; }
            }

            public PageChangeEventArgs(int newPageIndex)
            {
                _newPageIndex = newPageIndex;
            }
        }

        public enum PagedMode
        {
            URL分页,
            按钮分页
        }
    }
}