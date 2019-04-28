using System;
using System.Data;
using System.Web;

namespace Mejoy.Library
{
    public class CheckInput : System.Web.UI.Page
    {
        private DataTable _Input;

        public CheckInput()
        {
            this.CheckInputInit();
        }


        /// <summary>
        /// 初始
        /// </summary>
        private void CheckInputInit()
        {
            this._Input = new DataTable("CheckItem");

            this._Input.Columns.Add("ItemName", typeof(System.String));
            this._Input.Columns.Add("ItemInput", typeof(System.String));
            this._Input.Columns.Add("MinLen", typeof(System.Int32));
            this._Input.Columns.Add("MaxLen", typeof(System.Int32));
            this._Input.Columns.Add("Type", typeof(System.Byte));

            this._Input.AcceptChanges();
        }//End CheckInputInit();


        /// <summary>
        /// 添加表单项
        /// </summary>
        /// <param name="ItemName">项名称</param>
        /// <param name="ItemInput">项输入</param>
        /// <param name="MinLen">最小长度</param>
        /// <param name="MaxLen">最大长度</param>
        public void Add(string ItemName, string ItemInput, int MinLen, int MaxLen, Enums.StringType Type)
        {
            DataRow dr = this._Input.NewRow();
            dr["ItemName"] = ItemName;
            dr["ItemInput"] = ItemInput;
            dr["MinLen"] = MinLen;
            dr["MaxLen"] = MaxLen;
            dr["Type"] = Type;
            this._Input.Rows.Add(dr);
            this._Input.AcceptChanges();
        }//End Add()
        public void Add(string ItemName, string ItemInput, int MinLen, int MaxLen)
        {
            this.Add(ItemName, ItemInput, MinLen, MaxLen, Enums.StringType.String);
        }//End Add();


        /// <summary>
        /// 检查表单字符输入
        /// </summary>
        /// <param name="ItemName">表单项名称</param>
        /// <param name="ItemInput">表单输入框名</param>
        /// <param name="MinLen">要求最小长度。为0不限制</param>
        /// <param name="MaxLen">要求最大长度。为0不限制</param>
        /// <param name="Err">返回错误信息</param>
        public void CheckInputValue(string ItemName, string ItemInput, int MinLength, int MaxLength, Enums.StringType ItemType, out bool Error, out string ErrorMsg)
        {
            Error = false;
            ErrorMsg = "";

            //取值
            string ItemValue = string.Empty;
            try
            {
                ItemValue = Convert.ToString(HttpContext.Current.Request.Form[ItemInput]);
            }
            catch { }

            //最小
            if (MinLength > 0)
            {
                if (Function.GetStringLength(ItemValue) < MinLength)
                {
                    Error = true;
                    ErrorMsg = string.Format("{0}太短！", ItemName);
                }
            }
            //最大
            if (MaxLength > 0)
            {
                if (Function.GetStringLength(ItemValue) > MaxLength)
                {
                    Error = true;
                    ErrorMsg = string.Format("{0}太长！", ItemName);
                }
            }

            #region 类型检测
            if (!string.IsNullOrEmpty(ItemValue))
            {
                switch (ItemType)
                {
                    case Enums.StringType.SByte:
                        try
                        {
                            sbyte _SByte = Convert.ToSByte(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Byte:
                        try
                        {
                            byte _Byte = Convert.ToByte(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Short:
                        try
                        {
                            short _Short = Convert.ToInt16(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.UShort:
                        try
                        {
                            ushort _UShort = Convert.ToUInt16(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Int:
                        try
                        {
                            int _Int = Convert.ToInt32(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.UInt:
                        try
                        {
                            uint _UInt = Convert.ToUInt32(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Long:
                        try
                        {
                            long _Long = Convert.ToInt64(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.ULong:
                        try
                        {
                            ulong _ULong = Convert.ToUInt64(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Float:
                        try
                        {
                            float _Float = Convert.ToSingle(ItemValue);
                        }
                        catch
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}值类型不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Email:
                        if (!Function.IsValidEmail(ItemValue))
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}格式不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Zipcode:
                        if (!Function.IsValidZipcode(ItemValue))
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}格式不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Date:
                        if (!Function.IsValidDateTime(ItemValue))
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}格式不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Numeric:
                        if (!Function.IsValidNumeric(ItemValue))
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}格式不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.ClassNo:
                        if (!Function.IsValidNo(ItemValue))
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}格式不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.LoginName:
                        if (!Function.IsValidLoginName(ItemValue))
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}格式不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.Url:
                        string[] Header = new string[] { "http://", "https://" };
                        bool Found = false;
                        foreach (string h in Header)
                        {
                            if (ItemValue.Length >= h.Length)
                            {
                                if (ItemValue.Substring(0, h.Length).ToLower() == h.ToLower())
                                {
                                    Found = true;
                                }
                            }
                        }
                        if (!Found)
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}格式不正确！", ItemName);
                        }
                        break;

                    case Enums.StringType.RGBColor:
                        if (!Function.IsValidRGBColor(ItemValue))
                        {
                            Error = true;
                            ErrorMsg = string.Format("{0}的颜色格式不正确！", ItemName);
                        }
                        break;

                    default:
                    case Enums.StringType.String:
                        break;
                }
            }
            #endregion
        }//Ednd CheckInputValue()


        /// <summary>
        /// 检查所有表单
        /// </summary>
        /// <param name="Err">错误标记</param>
        /// <param name="Msg">错误信息</param>
        public void CheckInputs(out bool Err, out string Msg)
        {
            Err = false;
            Msg = "";

            if (this._Input.Rows.Count > 0)
            {
                bool Error = false;
                string ErrMsg = "";

                foreach (DataRow dr in this._Input.Rows)
                {
                    this.CheckInputValue(Convert.ToString(dr["ItemName"]), Convert.ToString(dr["ItemInput"]), Convert.ToInt32(dr["MinLen"]), Convert.ToInt32(dr["MaxLen"]), (Enums.StringType)dr["Type"], out Error, out ErrMsg);
                    if (Error)
                    {
                        Err = true;
                        Msg += @"\n\n" + ErrMsg;
                    }
                }
            }
        }//End CheckInputs()
        public void CheckInputValue(string ItemName, string ItemInput, int MinLen, int MaxLen, out bool Err, out string Msg)
        {
            this.CheckInputValue(ItemName, ItemInput, MinLen, MaxLen, Enums.StringType.String, out Err, out Msg);
        }//End CheckInputs()
    }
}
