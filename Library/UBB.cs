using System.Text.RegularExpressions;

namespace Mejoy.Library
{
    /// <summary>
    /// 模块功能：UBB模块
    /// 开发人员：阿明
    /// 最后更新：2008.1.12
    /// </summary>
    public class UBB
    {
        /// <summary>
        /// UBB代码转换为HTML代码。
        /// 如使用了运行代码，就在客户端加上相应的js代码。
        /// </summary>
        /// <param name="str">被转换的UBB代码</param>
        /// <returns>返回转换后的代码</returns>
        public static string ToHtml(string str, Enums.UBBType type)
        {
            if (str == "" || str == null) return "";

            string pat;
            Regex r;
            Match m;

            //加粗：所有支持
            pat = @"\[b\](.[^\[]*)\[/b\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<strong>" + m.Groups[1].ToString() + "</strong>");
            }

            //斜体：所有支持
            pat = @"\[i\](.[^\[]*)\[/i\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<i>" + m.Groups[1].ToString() + "</i>");
            }

            //颜色：所有支持
            pat = @"\[color=(.[^\[]*)\](.[^\[]*)\[/color\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<font color=\"" + m.Groups[1].ToString() + "\">" + m.Groups[2] + "</font>");
            }

            //下划线：所有支持
            pat = @"\[u\](.[^\[]*)\[/u\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<u>" + m.Groups[1].ToString() + "</u>");
            }

            //字号
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                pat = @"\[size=(.[^\[]*)\](.[^\[]*)\[/size\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<font size=\"" + m.Groups[1].ToString() + "\">" + m.Groups[2] + "</font>");
                }
            }

            //字体
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                pat = @"\[face=(.[^\[]*)\](.[^\[]*)\[/face\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<font face=\"" + m.Groups[1].ToString() + "\">" + m.Groups[2] + "</font>");
                }
            }

            //图片
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                //格式一：自动高度、宽度
                pat = @"\[img\](.[^\[]*)\[\/img\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<img src=\"" + m.Groups[1].ToString() + "\" border=\"0\" id=\"Img" + m.Groups[1].Index + "\" title=\"新窗口查看全图\" onclick=\"window.open(this.src)\" style=\"cursor:hand\" align=\"absmiddle\" onload=\"resetArticleImageSize(this)\" />");
                }

                //格式二：设置高度、宽度
                pat = @"\[img(.[^\]]*)\](.[^\[]*)\[\/img\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<img src=\"" + m.Groups[2].ToString() + "\" " + m.Groups[1].ToString().Replace("&nbsp;", " ") + " border=\"0\" id=\"Img" + m.Groups[1].Index + "\" title=\"新窗口查看全图\" onclick=\"window.open(this.src)\" style=\"cursor:hand\" align=\"absmiddle\" onload=\"resetArticleImageSize(this)\" />");
                }
            }

            //附件
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                //格式一：[file]http://www.opennsp.com/up/filename.exe[/file]
                pat = @"\[file\](.[^\[]*)\[/file\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<a href=\"" + m.Groups[1] + "\" target=\"_blank\">" + m.Groups[1].ToString() + "</a>");
                }

                //格式二：[file=http://www.opennsp.com/up/filename.exe]描述内容[/file]
                pat = @"\[file=(.[^\[]*)\](.[^\[]*)\[/file\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<a href=\"" + m.Groups[1].ToString() + "\" target=\"_blank\">" + m.Groups[2] + "</a>");
                }
            }

            //链接
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                //格式:[url]http://www.opennsp.com[/url]
                pat = @"\[url\](.[^\[]*)\[/url\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<a href=\"" + m.Groups[1] + "\" target=\"_blank\">" + m.Groups[1].ToString() + "</a>");
                }

                //格式:[url=http://www.opennsp.com]文字[/url]
                pat = @"\[url=(.[^\[]*)\](.[^\[]*)\[/url\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<a href=\"" + m.Groups[1].ToString() + "\" target=\"_blank\">" + m.Groups[2] + "</a>");
                }
            }

            //对齐
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                pat = @"\[align=(center|left|right)\](.[^\[]*)\[\/align\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<div align=\"" + m.Groups[1].ToString() + "\">" + m.Groups[2] + "</div>");
                }
            }

            //飞行
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                pat = @"\[fly\](.[^\[]*)\[\/fly\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<marquee width=\"90%\" behavior=\"alternate\" scrollamount=\"3\">" + m.Groups[1].ToString() + "</marquee>");
                }
            }

            //移动
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                pat = @"\[move\](.[^\[]*)\[\/move\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<marquee scrollamount=\"3\">" + m.Groups[1].ToString() + "</marquee>");
                }
            }

            //引用
            if (type == Enums.UBBType.Normal || type == Enums.UBBType.Advance)
            {
                pat = @"\[quote\](.*?)\[\/quote\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<div width=\"width:100%\" align=\"left\" style=\"margin:15px; padding:10px; border: 1px solid #CCCCCC; background-color:#F5F5F5\">" + m.Groups[1].ToString() + "</div><br />");
                }
            }

            //代码
            if (type == Enums.UBBType.Advance)
            {
                pat = @"\[code\](.*?)\[\/code\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<div align=\"center\"><textarea id=\"Code" + m.Groups[1].Index.ToString() + "\" name=\"Code" + m.Groups[1].Index.ToString() + "\" rows=\"10\" style=\"width:90%\">" + m.Groups[1].ToString().Replace("<br />", "\n") + "</textarea><br /><input onclick=\"fnRunCode(Code" + m.Groups[1].Index.ToString() + ")\" type=\"button\" value=\"运行代码\" name=\"RunCode\" style=\"width:70px; height:24px\"/>&nbsp;<input onclick=\"fnCopyCode(Code" + m.Groups[1].Index.ToString() + ")\" type=\"button\" value=\"复制代码\" name=\"CopyCode\" style=\"width:70px; height:24px\"/>&nbsp;<input onclick=\"fnSaveCode(Code" + m.Groups[1].Index.ToString() + ")\" type=\"button\" value=\"保存代码\" name=\"CopyCode\" style=\"width:70px; height:24px\" /></div>");
                }
            }

            //flash
            if (type == Enums.UBBType.Advance)
            {
                //格式一：未设置宽度、高度
                pat = @"\[flash\](.[^\[]*(.swf))\[\/flash\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<object align=\"middle\" codeBase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"><param name=\"movie\" value=\"" + m.Groups[1].ToString() + "\"><param name=\"quality\" value=\"high\"><embed src=\"" + m.Groups[1].ToString() + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\">" + m.Groups[1].ToString() + "</embed></object>");
                }

                //格式二:[flash width=200 height=200]http://www.opennsp.com/flash.swf[/flash]
                pat = @"\[flash(.[^\]]*)\](.[^\[]*(.swf))\[\/flash\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<object align=\"middle\" codeBase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" " + m.Groups[1].ToString().Replace("&nbsp;", " ") + "><param name=\"movie\" value=\"" + m.Groups[2].ToString() + "\"><param name=\"quality\" value=\"high\"><embed src=\"" + m.Groups[2].ToString() + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" " + m.Groups[1].ToString().Replace("&nbsp;", " ") + ">" + m.Groups[2].ToString() + "</embed></object>");
                }
            }

            //iframe
            if (type == Enums.UBBType.Advance)
            {
                pat = @"\[iframe(.[^\]]*)\](.[^\[]*)\[\/iframe\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<iframe src=\"" + m.Groups[2].ToString() + "\" " + m.Groups[1].ToString().Replace("&nbsp;", " ") + "></iframe>");
                }
            }

            //html
            if (type == Enums.UBBType.Advance)
            {
                pat = @"\[html\](.*?)\[\/html\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), Function.HtmlDeCode(m.Groups[1].ToString()));
                }
            }

            //跳转
            if (type == Enums.UBBType.Advance)
            {
                pat = @"\[goto\](.*?)\[\/goto\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<script language=\"javascript\">window.location.replace('" + m.Groups[1].ToString() + "')</script>");
                }
            }

            //弹出窗口
            if (type == Enums.UBBType.Advance)
            {
                pat = @"\[open(.[^\]]*)\](.[^\[]*)\[\/open\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<script language=\"javascript\">window.open(\"" + m.Groups[2].ToString() + "\", \"\", \"" + m.Groups[1].ToString().Replace("&nbsp;", " ") + "\");</script>");
                }
            }

            //javascript代码
            if (type == Enums.UBBType.Advance)
            {
                pat = @"\[javascript\](.*?)\[\/javascript\]";
                r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                for (m = r.Match(str); m.Success; m = m.NextMatch())
                {
                    str = str.Replace(m.Groups[0].ToString(), "<script language=\"javascript\">" + m.Groups[1].ToString() + "</script>");
                }
            }

            return str;
        }

        /// <summary>
        /// 默认UBB样式代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHtml(string str)
        {
            return ToHtml(str, Enums.UBBType.Guest);
        }//End ToHtml();


        /// <summary>
        /// 默认UBB样式代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHtml(string str, string server)
        {
            if (str == "" || str == null) return "";

            string pat;
            Regex r;
            Match m;

            //表情
            pat = @"\[\:(\d{1,})\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<img src=\"" + server + "/images/exp/" + m.Groups[1].ToString() + ".gif\" border=\"0\" align=\"absmiddle\" />");
            }

            //图片格式一：自动高度、宽度
            pat = @"\[img\](.[^\[]*)\[\/img\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<img src=\"" + m.Groups[1].ToString() + "\" border=\"0\" id=\"Img" + m.Groups[1].Index + "\" title=\"新窗口查看全图\" onclick=\"window.open(this.src)\" style=\"cursor:hand\" align=\"absmiddle\" />");
            }

            //图片格式二：设置高度、宽度
            pat = @"\[img(.[^\]]*)\](.[^\[]*)\[\/img\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<img src=\"" + m.Groups[2].ToString() + "\" " + m.Groups[1].ToString().Replace("&nbsp;", " ") + " border=\"0\" id=\"Img" + m.Groups[1].Index + "\" title=\"新窗口查看全图\" onclick=\"window.open(this.src)\" style=\"cursor:hand\" align=\"absmiddle\" />");
            }

            //链接格式:[url]http://www.cscall.com[/url]
            pat = @"\[url\](.[^\[]*)\[/url\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<a href=\"" + m.Groups[1] + "\" target=\"_blank\">" + m.Groups[1].ToString() + "</a>");
            }

            //链接格式:[url=http://www.cscall.com]文字[/url]
            pat = @"\[url=(.[^\[]*)\](.[^\[]*)\[/url\]";
            r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "<a href=\"" + m.Groups[1].ToString() + "\" target=\"_blank\">" + m.Groups[2] + "</a>");
            }

            return str;
        }//End ToHtml();


        /// <summary>
        /// 清除UBB格式代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Clear(string str)
        {
            Match m;

            string pat = @"\[(.[^\]]*)\](.[^\[]*)\[/(.[^\]]*)\]";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), m.Groups[2].ToString());
            }
            return str;
        }//End Clear();
    }
}
