using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mail;
using System.Net;

using System.Collections.Generic;
namespace Mejoy.Library
{
    public class Function : System.Web.UI.Page
    {
        /// <summary>
        /// 转换任意类型值为指定类型，若发生异常，返回此类型默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(object obj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }//End ConvertTo();



        /// <summary>
        /// 获取当前页面的URL地址
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrl()
        {
            //取本页URL地址
            string strTemp = "";
            if (HttpContext.Current.Request.ServerVariables["HTTPS"] == "off" || string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTPS"]))
            {
                strTemp = "http://";
            }
            else
            {
                strTemp = "https://";
            }

            strTemp = strTemp + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];

            if (HttpContext.Current.Request.ServerVariables["SERVER_PORT"] != "80")
            {
                strTemp = strTemp + ":" + HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            }

            strTemp = strTemp + HttpContext.Current.Request.ServerVariables["URL"];

            if (HttpContext.Current.Request.QueryString != null)
            {
                strTemp = HttpContext.Current.Server.UrlEncode(strTemp + "?" + HttpContext.Current.Request.QueryString);
            }

            return strTemp;
        }



        /// <summary>
        /// 创建目录(避免主机支持的错误)
        /// </summary>
        /// <param name="Path"></param>
        public static void CreateDirectory(string Path)
        {
            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(Path)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(Path));
                }
            }
            catch
            {
                Scripting.FileSystemObject fso = new Scripting.FileSystemObjectClass();
                fso.CreateFolder(HttpContext.Current.Server.MapPath(Path));
            }
        }//End CreateDirectory();



        /// <summary>
        /// 生成文件。
        /// 如原文件已存在，将进行重写
        /// </summary>
        /// <param name="FileCode">文件内容</param>
        /// <param name="FileName">文件名</param>
        public void CreateFile(string FileCode, string FileName, System.Text.Encoding encoding)
        {
            string Dir = FileName.Substring(0, FileName.LastIndexOf("/"));
            if (Dir != "" && !Directory.Exists(Context.Server.MapPath(Dir)))
            {
                Function.CreateDirectory(Dir);  //无需加Server.MapPath
            }
            if (System.IO.File.Exists(Context.Server.MapPath(FileName)))
            {
                System.IO.File.Delete(Context.Server.MapPath(FileName));
            }
            StreamWriter sw = new StreamWriter(Context.Server.MapPath(FileName), true, encoding);
            sw.Write(FileCode);
            sw.Flush();
            sw.Close();
        }
        public static void CreateFile(string FileCode, string FileName)
        {
            Function fn = new Function();
            fn.CreateFile(FileCode, FileName, System.Text.Encoding.Default);
        }//End CreateFile();



        /// <summary>
        /// 返回跨两个指定日期的日期和时间边界数，如果 StartDate 比 EndDate 晚，返回负值。
        /// </summary>
        /// <param name="DatePart">日期的哪一部分计算差额的参数</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">终止日期</param>
        /// <returns></returns>
        public static int DateDiff(Enums.DatePart DatePart, DateTime StartDate, DateTime EndDate)
        {
            TimeSpan ts = new TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (DatePart)
            {
                case Enums.DatePart.Years:  //年
                    return EndDate.Year - StartDate.Year;

                case Enums.DatePart.Months: //月
                    return (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month;

                case Enums.DatePart.Weeks:  //周
                    ts = new TimeSpan(EndDate.AddDays(-(int)EndDate.DayOfWeek).Date.Ticks - StartDate.AddDays(-(int)StartDate.DayOfWeek).Date.Ticks);
                    return ts.Days / 7;  //一定是整除的！

                case Enums.DatePart.Days:  //天
                    ts = new TimeSpan(EndDate.Date.Ticks - StartDate.Date.Ticks);
                    return ts.Days;

                case Enums.DatePart.Hours:
                    return (ts.TotalHours.ToString().IndexOf(".") == -1) ? ts.Hours : ts.Hours + 1;

                case Enums.DatePart.Minutes:
                    return (ts.TotalMinutes.ToString().IndexOf(".") == -1) ? ts.Minutes : ts.Minutes + 1;

                case Enums.DatePart.Seconds:
                    return (ts.TotalSeconds.ToString().IndexOf(".") == -1) ? ts.Seconds : ts.Seconds + 1;

                case Enums.DatePart.Milliseconds:
                    return (ts.TotalMilliseconds.ToString().IndexOf(".") == -1) ? ts.Milliseconds : ts.Milliseconds + 1;

                default:
                    return 0;
            }
        }//End DateDiff();



        /// <summary>
        /// 取一维数组中任意维的值
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="arr">数组</param>
        /// <param name="index">维，第一个为0</param>
        /// <returns></returns>
        public static T GetArray<T>(object[] arr, int index)
        {
            try
            {
                return (T)Convert.ChangeType(arr[index], typeof(T));
            }
            catch
            {
                return default(T);
            }
        }//End GetArray();



        /// <summary>
        /// 取一维数组中任意维的值
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="str">已分隔的字符</param>
        /// <param name="split">分隔符</param>
        /// <param name="index">维，第一个为0</param>
        /// <returns></returns>
        public static T GetArray<T>(string str, string split, int index)
        {
            string[] arr = str.Split(split.ToCharArray());
            try
            {
                return (T)Convert.ChangeType(arr[index], typeof(T));
            }
            catch
            {
                return default(T);
            }
        }//End GetArray();



        /// <summary>
        /// 返回日期格式的目录，格式：yyyy/M/d
        /// </summary>
        /// <returns></returns>
        public static string GetDateDirectory()
        {
            return string.Format("{0}/{1}/{2}/", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }//End GetDateDirectory();



        /// <summary>
        /// 返回当前时间格式目录：HHmmssffff
        /// </summary>
        /// <returns></returns>
        public static string GetTimeFileName()
        {
            return DateTime.Now.ToString("HHmmssffff");
        }//End GetDateTimeFileName()



        /// <summary>
        /// 根据编号，返回目录名。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetIdDir(uint Id)
        {
            if (Id < 1) return "";

            uint Step = 1000;
            uint DirNum = ((uint)((Id % Step == 0) ? Id / Step : Id / Step + 1)) * Step;
            return string.Format("{0}-{1}/", DirNum - Step + 1, DirNum);
        }//End GetIdDir();



        /// <summary>
        /// 根据编号，返回目录名。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetIdDir(UInt64 Id)
        {
            if (Id < 1) return "";

            UInt64 Step = 1000;
            UInt64 DirNum = ((UInt64)((Id % Step == 0) ? Id / Step : Id / Step + 1)) * Step;
            return string.Format("{0}-{1}/", DirNum - Step + 1, DirNum);
        }//End GetIdDir();



        /// <summary>
        /// 以字节计算的字符长度。
        /// </summary>
        /// <param name="str">测试的字符。</param>
        /// <returns>返回字节长度。</returns>
        public static int GetStringLength(string str)
        {
            return (string.IsNullOrEmpty(str)) ? 0 : System.Text.Encoding.Default.GetByteCount(str);
        }//End GetStringLength();



        /// <summary>
        /// 截取字符串（汉字算2个字符）
        /// </summary>
        /// <param name="stringToSub"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetFirstString(string stringToSub, int length)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            bool isCut = false;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength > length)
                {
                    isCut = true;
                    break;
                }
            }
            if (isCut)
                return sb.ToString() + "..";
            else
                return sb.ToString();
        }



        #region HTM代码截断和补全 
        ///  
        /// HTM代码截断和补全 
        ///  
        /// 需要截断的HTML代码 
        /// 字符长度 
        /// 存储的堆栈 
        ///  
        public static string HTMLSubString(string scr, int length, List<string> tagNameStack) 
        { 
            string retString = string.Empty; 
            if (scr.Length <= length) 
            { 
                retString = scr; 
            } 
            else 
            { 
                string contentTxt = string.Empty; 
                if (scr.Contains("<")) 
                { 
                    contentTxt = scr.Substring(0, scr.IndexOf('<'));//获取标签<前的内容 
                    if (contentTxt.Length < length)//如果<前的内容小于要求长度 
                    { 
                        scr = scr.Substring(scr.IndexOf('<')); 
                        retString += contentTxt;//前面的内容先填充到返回字符串中 
                        string tagContent = scr.Substring(0, scr.IndexOf('>') + 1); 
         
                        if (tagContent.Contains(" ")) 
                        { 
                            string tagName = tagContent.Substring(1, tagContent.IndexOf(" ") - 1); 
                            if (tagName.ToLower() != "br" && tagName.ToLower() != "img") 
                            { 
                                retString += tagContent; 
                                tagNameStack.Add(tagName); 
                            } 
                            else 
                            { 
                                retString += tagContent; 
                            } 
                        } 
                        else if (tagContent.Contains("/")) 
                        { 
                            string tagName = tagContent.Substring(2, tagContent.IndexOf(">") - 2); 
                            if (tagNameStack.Count > 0) 
                            { 
                                while (tagName != tagNameStack[tagNameStack.Count - 1]) 
                                { 
                                    retString += tagNameStack[tagNameStack.Count - 1] + ">";
                                    tagNameStack.Remove(tagNameStack[tagNameStack.Count - 1]); 
                                    if (tagNameStack.Count <= 0) 
                                    { 
                                        break; 
                                    } 
                                } 
                                if (tagNameStack.Count > 0 && tagName == tagNameStack[tagNameStack.Count - 1]) 
                                { 
                                    retString +=  tagName + ">"; 
                                    tagNameStack.Remove(tagName); 
                                } 
                            } 
                        } 
                        else 
                        { 
                            string tagName = tagContent.Substring(1, tagContent.IndexOf(">") - 1); 
                            if (tagName.ToLower() != "br" && tagName.ToLower() != "img") 
                            { 
                                retString += tagContent; 
                                tagNameStack.Add(tagName); 
                            } 
                        } 
         
                        scr = scr.Substring(scr.IndexOf('>') + 1); 
                        retString += HTMLSubString(scr, length - contentTxt.Length, tagNameStack); 
                    } 
                    else 
                    { 
                        //中文算2个 
                        retString += BreakStr(contentTxt, length);//contentTxt.Substring(0, length);  
                        if (tagNameStack.Count > 0) 
                        { 
                            for (int i = tagNameStack.Count - 1; i >= 0; i--) 
                            { 
                                retString += tagNameStack[i] + ">"; 
                            } 
                        } 
                    } 
                } 
                else 
                { 
                    retString = HtmlDeCode(scr); 
                    //中文算2个 
                    retString = BreakStr(retString, length); 
                } 
            } 
            return retString; 
        }

        private static string BreakStr(string content, int length)
        {
            return content.Substring(0, length);
        }
        #endregion 



        /// <summary>
        /// 获取当前用户的IP地址。
        /// </summary>
        public static string GetUserIP()
        {
            string IP = HttpContext.Current.Request.UserHostAddress;
            if (IP == null || IP == "")
            {
                IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (!Function.IsValidNumeric(IP.Replace(".", "")))
            {
                IP = "";
            }
            return IP;
        }//End GetUserIP();



        /// <summary>
        /// 转换回HtmlEnCode所转换过的代码。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlDeCode(string str)
        {
            if (str == "" || str == null) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(str);

            sb.Replace("<br />", "\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&nbsp;", " ");
            sb.Replace("&quot;", "\"");
            sb.Replace("&#39;", "\'");
            sb.Replace("&amp;", "&");

            return sb.ToString();
        }//End HtmlDeCode();



        public static string HtmlDeCodeWap(string str)
        {
            if (str == "" || str == null) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(str);

            sb.Replace("&amp;", "&");

            return sb.ToString();
        }


        /// <summary>
        /// 允许HTML。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlEnCode(string str)
        {
            if (str == "" || str == null) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(str);

            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\"", "&quot;");
            sb.Replace("\'", "&#39;");
            sb.Replace(" ", "&nbsp;");
            sb.Replace("\r", "");
            sb.Replace("\n", "<br />");

            return sb.ToString();
        }//End HtmlEnCode();



        /// <summary>
        /// 根据DataRow寻找表单框并初始数据
        /// </summary>
        /// <param name="dr"></param>
        public static void InputValueInit(System.Web.UI.Page page, DataRow dr)
        {
            System.Web.UI.WebControls.TextBox tb1;
            System.Web.UI.HtmlControls.HtmlInputText tb2;
            System.Web.UI.HtmlControls.HtmlInputHidden tb3;
            System.Web.UI.WebControls.DropDownList ddl;
            System.Web.UI.WebControls.RadioButtonList rbl;

            bool _Found = false;
            string _Name;
            foreach (DataColumn c in dr.Table.Columns)
            {
                _Found = false;

                //TextBox
                if (!_Found)
                {
                    _Name = "tb" + c.ColumnName.Replace("_", "");
                    try
                    {
                        tb1 = (System.Web.UI.WebControls.TextBox)page.FindControl(_Name);
                        if (tb1 != null)
                        {
                            tb1.Text = Convert.ToString(dr[c.ColumnName]);
                            _Found = true;
                        }
                    }
                    catch { }
                }

                //InputText
                if (!_Found)
                {
                    try
                    {
                        _Name = "tb" + c.ColumnName.Replace("_", "");
                        tb2 = (System.Web.UI.HtmlControls.HtmlInputText)page.FindControl(_Name);
                        if (tb2 != null)
                        {
                            tb2.Value = Convert.ToString(dr[c.ColumnName]);
                            _Found = true;
                        }
                    }
                    catch { }
                }

                //Hidden
                if (!_Found)
                {
                    try
                    {
                        _Name = "tb" + c.ColumnName.Replace("_", "");
                        tb3 = (System.Web.UI.HtmlControls.HtmlInputHidden)page.FindControl(_Name);
                        if (tb3 != null)
                        {
                            tb3.Value = Convert.ToString(dr[c.ColumnName]);
                            _Found = true;
                        }
                    }
                    catch { }
                }

                //DropDownList
                if (!_Found)
                {
                    try
                    {
                        _Name = "ddl" + c.ColumnName.Replace("_", "");
                        ddl = (System.Web.UI.WebControls.DropDownList)page.FindControl(_Name);
                        if (ddl != null)
                        {
                            SetDropDownListDefault(ddl, Convert.ToString(dr[c.ColumnName]));
                            _Found = true;
                        }
                    }
                    catch { }
                }

                //RadioButtonList
                if (!_Found)
                {
                    try
                    {
                        _Name = "rbl" + c.ColumnName.Replace("-", "");
                        rbl = (System.Web.UI.WebControls.RadioButtonList)page.FindControl(_Name);
                        if (rbl != null)
                        {
                            string val = Convert.ToString(dr[c.ColumnName]);
                            if (val.ToLower().Equals("true")) val = "1";
                            else if (val.ToLower().Equals("false")) val = "0";
                            SetRadioButtonListDefault(rbl, val);
                            _Found = true;
                        }
                    }
                    catch { }
                }
            }
        }//End TextBoxValueInit();



        /// <summary>
        /// 判断字符是否为合法有效的日期格式
        /// </summary>
        /// <param name="DateString"></param>
        /// <returns></returns>
        public static bool IsValidDateTime(string DateString)
        {
            try
            {
                DateTime dStr = Convert.ToDateTime(DateString);
                return true;
            }
            catch
            {
                return false;
            }
        }//End IsValidDateTime();



        /// <summary>
        /// 判断是否为合法的email格式
        /// </summary>
        /// <param name="Email">email地址</param>
        /// <returns></returns>
        public static bool IsValidEmail(string Email)
        {
            return (string.IsNullOrEmpty(Email)) ? false : Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.IgnoreCase);
        }//End IsValidEmail();



        /// <summary>
        /// 判断是否为guid格式字符
        /// </summary>
        /// <param name="Guid"></param>
        /// <returns></returns>
        public static bool IsValidGuid(string Guid)
        {
            return (string.IsNullOrEmpty(Guid)) ? false : Regex.IsMatch(Guid, @"^[\da-z]{8}\-[\da-z]{4}\-[\da-z]{4}-[\da-z]{4}-[\da-z]{12}$", RegexOptions.IgnoreCase);
        }//End IsValidGuid();



        /// <summary>
        /// 判断是否是一个合法的图片文件后缀格式
        /// </summary>
        /// <param name="Ext">包含分隔点(.)的后缀</param>
        /// <returns></returns>
        public static bool IsValidImageExt(string Ext)
        {
            string[] Exts = new string[] { ".gif", ".jpg", ".bmp", ".png", ".jpeg" };
            foreach (string ext in Exts)
            {
                if (ext.ToLower() == Ext.ToLower())
                {
                    return true;
                }
            }
            return false;
        }//End IsValidImageExt();



        /// <summary>
        /// 判断是否是一个由数字、字母组成的2-30位登录帐号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool IsValidLoginName(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(account, @"^[a-z0-9]{3,30}$", RegexOptions.IgnoreCase);
            }
        }//End IsValidLoginName();



        /// <summary>
        /// 判断编号是否为设定长度的数字或英文组成。
        /// </summary>
        /// <param name="No">编号</param>
        /// <returns></returns>
        public static bool IsValidNo(string No)
        {
            if (string.IsNullOrEmpty(No))
            {
                return false;
            }

            if (No.Length % (int)Common.Config.NO_LENGTH == 0)
            {
                string pat = "^[a-z0-9]{" + Common.Config.NO_LENGTH + ",}$";
                if (Regex.IsMatch(No.ToLower(), pat, RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }//End IsValidClassNo();



        /// <summary>
        /// 判断字符是否仅由0-9的数字组成。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidNumeric(string num)
        {
            return Regex.IsMatch(num, @"^\d{1,}$");
        }//End IsValidNumeric();



        /// <summary>
        /// 判断字符是否由RGB格式组成的颜色。当含有#时，自动消除#后进行进行检测
        /// </summary>
        /// <param name="color">被检查的字符，长度为6位或7位，如：#FFFFFF,CC3312</param>
        /// <returns></returns>
        public static bool IsValidRGBColor(string color)
        {
            color = color.Replace("#", "");
            return Regex.IsMatch(color, @"^[\dabcdef]{6}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }//End IsValidRGBColor();



        /// <summary>
        /// 判断字符中是否含有SQL特殊字符。不包特殊字符时返回true，否则反回false。为null时，返回false。
        /// 包含以下字符时为false：'、"、null、&、+
        /// </summary>
        /// <param name="str">要测试判断的字符</param>
        /// <returns>返回一个bool值</returns>
        public static bool IsValidString(string str)
        {
            if (str == null || str.IndexOf("'") != -1 || str.IndexOf("\"") != -1 || str.IndexOf("&") != -1 || str.IndexOf("+") != -1) return false;
            return true;
        }//End IsValidString();



        /// <summary>
        /// 判断当前返回的验证码是否正确
        /// </summary>
        /// <param name="SessionKey">session键值</param>
        /// <param name="CodeValue">验证码值</param>
        /// <returns></returns>
        public static bool IsValidVerifyCode(string SessionKey, string CodeValue)
        {
            string SessionValue = Convert.ToString(HttpContext.Current.Session[SessionKey]);

            if (!Function.IsValidString(CodeValue) || SessionValue == null || SessionValue == String.Empty || CodeValue == null || CodeValue == String.Empty)
            {
                return false;
            }
            else
            {
                return (string.Compare(CodeValue, SessionValue.Trim(), true) == 0) ? true : false;
            }
        }//End IsValidVerifyCode();



        /// <summary>
        /// 判断是否为有效的邮政编码
        /// </summary>
        /// <param name="Zipcode"></param>
        /// <returns></returns>
        public static bool IsValidZipcode(string Zipcode)
        {
            return Regex.IsMatch(Zipcode, @"^\d{6}$", RegexOptions.Singleline);
        }//End IsValidZipcode();



        /// <summary>
        /// 载入本地文件，路径为相对路径格式
        /// </summary>
        /// <param name="LocalFile">本地文件</param>
        /// <param name="encoding">编码</param>
        /// <returns>返回文件的代码，如HTML代码</returns>
        public static string LoadFileCode(string LocalFile, System.Text.Encoding encoding)
        {
            string Code = "";
            if (File.Exists(HttpContext.Current.Server.MapPath(LocalFile)))
            {
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(LocalFile), FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, encoding);
                Code = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            return Code;
        }
        /// <summary>
        /// 载入本地文件，路径为相对路径格式
        /// </summary>
        /// <param name="LocalFile">本地文件</param>
        /// <returns>返回文件的代码，如HTML代码</returns>
        public static string LoadFileCode(string LocalFile)
        {
            return LoadFileCode(LocalFile, System.Text.Encoding.UTF8);
        }//End LoadFileCode()



        /// <summary>
        /// 生成图片文件
        /// </summary>
        /// <param name="SessionKey"></param>
        /// <param name="Len"></param>
        public static void MakePhoneNumber(string phoneNumber)
        {
            int len = GetStringLength(phoneNumber);
            int ImgWidth = len * 10;

            System.Drawing.Bitmap img = new System.Drawing.Bitmap(ImgWidth, 16);
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.White);

            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Green, Color.Brown, Color.DarkCyan, Color.Blue };

            //定义字体
            string[] font = { "Arial", "Verdana", "宋体" };

            //随机输出噪点
            Random Rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                int x = Rnd.Next(img.Width);
                int y = Rnd.Next(img.Height);
                g.DrawRectangle(new Pen(Color.DarkGray, 0), x, y, 1, 1);
            }

            //输出不同字体和颜色的验证码字符
            //只输出Arial和Color.Black
            for (int i = 0; i < phoneNumber.Length; i++)
            {

                int cIndex = Rnd.Next(7);
                int fIndex = Rnd.Next(3);

                Font f = new System.Drawing.Font(font[0], 9);
                Brush b = new System.Drawing.SolidBrush(c[0]);

                g.DrawString(phoneNumber.Substring(i, 1), f, b, i * 9, 3);

            }

            //画一个边框
            //g.DrawRectangle(new Pen(Color.LightGray, 0), 0, 0, img.Width - 1, img.Height - 1);
            g.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Gif";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());

            img.Dispose();
            g.Dispose();
        }//End MakeVerifyCode();



        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="SessionKey"></param>
        /// <param name="Len"></param>
        public static void MakeVerifyCode(string SessionKey, sbyte Len)
        {
            //为了避免混淆，去了0和O、1和I
            string[] arrKey = new string[32] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "2", "3", "4", "5", "6", "7", "8", "9" };

            int NewLen = (DateTime.Now.Millisecond % 2 == 0) ? Len - 1 : Len;
            Random Rnd = new Random();
            string NewKey = "";
            for (int i = 1; i <= NewLen; i++)
            {
                NewKey += arrKey[Rnd.Next(arrKey.Length)];
            }
            NewKey = NewKey.ToUpper();
            if (NewKey.Length < Len)
            {
                NewKey += " ";
            }

            //保存session验证值
            HttpContext.Current.Session[SessionKey] = NewKey;

            int ImgWidth = (int)NewKey.Length * 13;

            System.Drawing.Bitmap img = new System.Drawing.Bitmap(ImgWidth, 22);
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.White);

            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Green, Color.Brown, Color.DarkCyan, Color.Blue };

            //定义字体
            string[] font = { "Arial", "Verdana", "宋体" };

            //随机输出噪点
            for (int i = 0; i < 30; i++)
            {
                int x = Rnd.Next(img.Width);
                int y = Rnd.Next(img.Height);
                g.DrawRectangle(new Pen(Color.DarkGray, 0), x, y, 1, 1);
            }

            //输出不同字体和颜色的验证码字符
            for (int i = 0; i < NewKey.Length; i++)
            {

                int cIndex = Rnd.Next(7);
                int fIndex = Rnd.Next(3);

                Font f = new System.Drawing.Font(font[fIndex], 10, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cIndex]);

                g.DrawString(NewKey.Substring(i, 1), f, b, 3 + i * 11, 3);

            }

            //画一个边框
            g.DrawRectangle(new Pen(Color.LightGray, 0), 0, 0, img.Width - 1, img.Height - 1);
            g.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Gif";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());

            img.Dispose();
            g.Dispose();
        }//End MakeVerifyCode();



        /// <summary>
        /// 将字符进行MD5加密，不可逆。
        /// </summary>
        /// <param name="str">将要初加密的字符串。</param>
        /// <returns>长度为32位的加密字符串。</returns>
        public static string MD5(string str)
        {
            if (str == "" || str == null)
            {
                return "";
            }
            else
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");	//可选:哈希密码-SHA1

            }
        }//End MD5();



        /// <summary>
        /// 功能：返回分页代码。
        /// </summary>
        /// <param name="PerNum">每页显示数。</param>
        /// <param name="TotalNum">总记录数。</param>
        /// <param name="Page">当前页码。</param>
        /// <param name="Url">跳转地址（不含刷新码）。</param>
        /// <returns></returns>
        public static string PageList(uint PerNum, uint TotalNum, uint Page, string Url)
        {
            if (PerNum == 0 || TotalNum == 0) return "";

            string Rnd = "." + DateTime.Now.Ticks.ToString();  //刷新码
            uint TotalPage = (uint)((TotalNum % PerNum == 0) ? TotalNum / PerNum : TotalNum / PerNum + 1);
            string Html = "";

            //纠正当前页错误
            if (Page > TotalPage)
            {
                Page = TotalPage;
            }

            if (TotalPage > 1)
            {
                //页面跳转js
                Html += "\n<script language=\"javascript\">\n";
                Html += "function fnGotoPage(page){\n";
                Html += @"	var re = /^\d{1,}$/gi;";
                Html += "\n	if (!re.test(page)){\n";
                Html += "		alert('抱歉，页码不正确！'); return;\n";
                Html += "	}else if (parseInt(page,10)<1 || parseInt(page,10)>" + TotalPage + "){\n";
                Html += "		alert('抱歉，页码范围不正确，应为在1至" + TotalPage + "页。'); return;\n";
                Html += "	}else if (parseInt(page,10)==" + Page + "){\n";
                Html += "		alert('抱歉，输入页码已为当前页码。'); return;\n";
                Html += "	}else{\n";
                Html += "		window.location.href='" + Url + "&page='+ page +'&temp='+Math.random();\n";
                Html += "	}\n";
                Html += "}\n";
                Html += "</script>\n";

                //第一页/上一页
                if (Page > 1)
                {
                    Html += string.Format("[<a href=\"{0}\">第一页</a>]&nbsp;", GetPageListUrl(Url, 1, Rnd));
                    Html += string.Format("[<a href=\"{0}\">上一页</a>]&nbsp;", GetPageListUrl(Url, Page - 1, Rnd));
                }
                //分页列表
                uint Start = 0;
                uint End = Page + 5;
                Start = (Page <= 5) ? 1 : Page - 5;
                if (End > TotalPage)
                {
                    End = TotalPage;
                }
                for (uint i = Start; i <= End; i++)
                {
                    if (i == Page)
                    {
                        Html += string.Format("<strong>{0}</strong>&nbsp;", i);
                    }
                    else
                    {
                        Html += string.Format("<a href=\"{0}\" title=\"第{1}页\">{1}</a>&nbsp;", GetPageListUrl(Url, i, Rnd), i);
                    }
                }
                //下一页/最后一页
                if (Page < TotalPage)
                {
                    Html += string.Format("[<a href=\"{0}\">下一页</a>]&nbsp;", GetPageListUrl(Url, Page + 1, Rnd));
                    Html += string.Format("[<a href=\"{0}\">最后一页</a>]&nbsp;", GetPageListUrl(Url, TotalPage, Rnd));
                }
            }

            //总数
            Html += "[共" + TotalNum + "条记录，第" + Page + "/" + TotalPage + "页]&nbsp;";

            if (TotalPage > 1)
            {
                Html += "<input type=\"text\" id=\"tbPageList\" name=\"tbPageList\" value=\"" + Page + "\" style=\"width:40px; text-align:center\" onKeyUp=\"if (event.keyCode==13) fnGotoPage(document.getElementById('tbPageList').value)\" />&nbsp;";
                Html += "<input type=\"button\" class=\"btn\" id=\"btnPageList\" name=\"btnPageList\" value=\"跳转\" onclick=\"fnGotoPage(document.getElementById('tbPageList').value)\" />\n";
            }

            return Html;
        }//End PageList();
        private static string GetPageListUrl(string Url, uint Page, string Rnd)
        {
            //页码
            if (Url.IndexOf("[page]") == -1)
            {
                Url += (Url.IndexOf("?") == -1) ? "?page=" + Page.ToString() : "&page=" + Page.ToString();
            }
            else
            {
                Url = Url.Replace("[page]", Page.ToString());
            }
            //刷新码
            if (Url.IndexOf("[rnd]") == -1)
            {
                Url += "&temp=" + Rnd;
            }
            else
            {
                Url = Url.Replace("[rnd]", Rnd);
            }
            return Url;
        }//End GetPageListUrl();



        /// <summary>
        /// 取表单项值
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="ParaName">表单名称</param>
        /// <returns></returns>
        public static T RequestForm<T>(string ParaName)
        {
            string ParaValue = string.Empty;
            //取值
            try
            {
                ParaValue = Convert.ToString(HttpContext.Current.Request.Form[ParaName]);
            }
            catch { }
            //转换
            try
            {
                return (T)Convert.ChangeType(ParaValue, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }//End RequestForm();




        /// <summary>
        /// 取地址栏参数值
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="ParaName">参数名称</param>
        /// <returns></returns>
        public static T RequestQueryString<T>(string ParaName, bool SafeString)
        {
            string ParaValue = string.Empty;
            //取值
            try
            {
                ParaValue = Convert.ToString(HttpContext.Current.Request.QueryString[ParaName]).Trim();
            }
            catch { }
            //转换
            try
            {
                if (typeof(T) == typeof(System.String))
                {
                    if (SafeString)
                    {
                        if (!Function.IsValidString(ParaValue))
                        {
                            ParaValue = "";
                        }
                    }
                }
                return (T)Convert.ChangeType(ParaValue, typeof(T));

            }
            catch
            {
                return default(T);
            }
        }
        public static T RequestQueryString<T>(string ParaName)
        {
            return RequestQueryString<T>(ParaName, true);
        }//End RequestQueryString();



        /// <summary>
        /// 选中DropDownList中的某项。
        /// </summary>
        /// <param name="name">DropDownList名称。</param>
        /// <param name="val">符合条件的值。</param>
        public static void SetDropDownListDefault(System.Web.UI.WebControls.DropDownList name, string val)
        {
            if (name.Items.FindByValue(val) != null)
            {
                name.Items.FindByValue(val).Selected = true;
            }
        }//End SetDropDownListDefault();



        /// <summary>
        /// 设置RadioButtonList的选定状态。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public static void SetRadioButtonListDefault(System.Web.UI.WebControls.RadioButtonList name, string val)
        {
            for (int i = 0; i < name.Items.Count; i++)
            {
                if (name.Items[i].Value == val)
                {
                    name.Items[i].Selected = true;
                    break;
                }
            }
        }//End SetRadioButtonListDefault();



        /// <summary>
        /// 分割内容。
        /// </summary>
        /// <param name="str">被分割的字符。</param>
        /// <param name="max">最大长度。</param>
        /// <returns></returns>
        public static DataTable SplitContent(string str, int max)
        {
            DataTable dt = new DataTable("Content");
            dt.Columns.Add("Content", typeof(string));

            DataRow dr;
            if (GetStringLength(str) <= max)
            {
                dr = dt.NewRow();
                dr["Content"] = str;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
            else
            {
                string StringSplit = "";
                string StringUnit = "";
                for (int i = 0; i < str.Length; i++)
                {
                    StringUnit = str.Substring(i, 1);
                    if (GetStringLength(StringSplit) + GetStringLength(StringUnit) > max)
                    {
                        dr = dt.NewRow();
                        dr["Content"] = StringSplit;
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();

                        StringSplit = StringUnit;
                    }
                    else
                    {
                        StringSplit += StringUnit;
                    }
                }
                if (StringSplit != "")
                {
                    dr = dt.NewRow();
                    dr["Content"] = StringSplit;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
            }

            return dt;
        }//End SplitContent();



        /// <summary>
        /// 以送smtp邮件
        /// </summary>
        /// <param name="From">发送人邮件地址</param>
        /// <param name="To">收件人邮件地下</param>
        /// <param name="Subject">邮件主题</param>
        /// <param name="Body">邮件内容</param>
        /// <param name="Host">smtp地址</param>
        /// <param name="Port">smtp端口</param>
        /// <param name="SmtpUserName">smtp验证帐号</param>
        /// <param name="SmtpUserPassword">smtp验证帐号密码</param>
        public static void SendSmtpMail(string From, string To, string Subject, string Body, string Host, int Port, string SmtpUserName, string SmtpUserPassword)
        {
            //邮件信息
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(From);
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.Default;

            //Smtp信息
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = Port;
            //smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(SmtpUserName, SmtpUserPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //发送邮件
            try
            {
                smtp.Send(mail);
            }
            catch { }

            //释放
            mail.Dispose();
        }//End SendSmtpMail()



        /// <summary>
        /// 检测客户端是否为手机浏览器
        /// </summary>
        /// <returns>True: 手机浏览器 False: Web浏览器</returns>
        public static Boolean isMobile()
        {
            HttpContext curcontext = HttpContext.Current;

            string user_agent = curcontext.Request.ServerVariables["HTTP_USER_AGENT"];
            user_agent = user_agent.ToLower();


            // Checks the user-agent   
            if (user_agent != null)
            {
                // Checks if its a Windows browser but not a Windows Mobile browser   
                if (user_agent.Contains("windows") && !user_agent.Contains("windows ce"))
                {
                    return false;
                }

                // Checks if it is a mobile browser   
                string pattern = "up.browser|up.link|windows ce|iphone|iemobile|mini|mmp|symbian|midp|wap|phone|pocket|mobile|pda|psp";
                MatchCollection mc = Regex.Matches(user_agent, pattern, RegexOptions.IgnoreCase);
                if (mc.Count > 0)
                    return true;

                // Checks if the 4 first chars of the user-agent match any of the most popular user-agents   
                string popUA = "|acs-|alav|alca|amoi|audi|aste|avan|benq|bird|blac|blaz|brew|cell|cldc|cmd-|dang|doco|eric|hipt|inno|ipaq|java|jigs|kddi|keji|leno|lg-c|lg-d|lg-g|lge-|maui|maxo|midp|mits|mmef|mobi|mot-|moto|mwbp|nec-|newt|noki|opwv|palm|pana|pant|pdxg|phil|play|pluc|port|prox|qtek|qwap|sage|sams|sany|sch-|sec-|send|seri|sgh-|shar|sie-|siem|smal|smar|sony|sph-|symb|t-mo|teli|tim-|tosh|tsm-|upg1|upsi|vk-v|voda|w3c |wap-|wapa|wapi|wapp|wapr|webc|winw|winw|xda|xda-|";
                if (popUA.Contains("|" + user_agent.Substring(0, 4) + "|"))
                    return true;
            }

            // Checks the accept header for wap.wml or wap.xhtml support   
            string accept = curcontext.Request.ServerVariables["HTTP_ACCEPT"];
            if (accept != null)
            {
                if (accept.Contains("text/vnd.wap.wml") || accept.Contains("application/vnd.wap.xhtml+xml"))
                {
                    return true;
                }
            }

            // Checks if it has any mobile HTTP headers   

            string x_wap_profile = curcontext.Request.ServerVariables["HTTP_X_WAP_PROFILE"];
            string profile = curcontext.Request.ServerVariables["HTTP_PROFILE"];
            string opera = curcontext.Request.Headers["HTTP_X_OPERAMINI_PHONE_UA"];

            if (x_wap_profile != null || profile != null || opera != null)
            {
                return true;
            }

            return false;
        }//end isMobile();
    }
}
