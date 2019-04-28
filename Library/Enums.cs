namespace Mejoy.Library
{
    public class Enums : System.Web.UI.Page
    {
        /// <summary>
        /// 日期比较类型
        /// </summary>
        public enum DatePart : byte
        {
            Years = 1,
            Months,
            Weeks,
            Days,
            Hours,
            Minutes,
            Seconds,
            Milliseconds
        }


        /// <summary>
        /// 字符类型
        /// </summary>
        public enum StringType : byte
        {
            String = 1,  //字符型
            SByte,
            Byte,
            Short,
            UShort,
            Int,
            UInt,
            Long,
            ULong,
            Float,
            Email,
            Zipcode,
            Date,
            Numeric,
            ClassNo,
            LoginName,
            Url,
            RGBColor
        }


        /// <summary>
        /// 枚举UBB类型
        /// </summary>
        public enum UBBType : byte
        {
            Guest = 0,   //支持最简单的功能
            Normal,      //标准模式，支持常见功能
            Advance      //支持所有功能
        }



        /// <summary>
        /// 目标窗口类型
        /// </summary>
        public enum TargetType : byte
        {
            Self = 1,
            Top,
            Parent,
            Child
        }
    }
}
