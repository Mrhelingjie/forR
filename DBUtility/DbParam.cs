using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Havsh.Application.Dal
{
    /// <summary>
    /// 封装执行SQL的命令参数
    /// </summary>
    public class DbParam
    {
        #region 字段
        private readonly string _name;
        private readonly string _type;
        private object _value;
        #endregion

        #region 属性
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// 参数类型:如String,DateTime等
        /// </summary>
        public string Type
        {
            get { return _type; }
        }
        /// <summary>
        /// 参数值
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        #region 构造函数
        public DbParam(string name, string type)
        {
            _name = name;
            _type = type;
        }
        public DbParam(string name, DbType type)
        {
            _name = name;
            _type = type.ToString();
        }
        public DbParam(string name, string type, object value)
            : this(name, type)
        {
            _value = value;
        }
        public DbParam(string name, DbType type, object value)
            : this(name, type)
        {
            _value = value;
        }
        #endregion
    }
}
