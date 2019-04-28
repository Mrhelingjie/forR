using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Havsh.Application.Dal
{
    /// <summary>
    /// ��װִ��SQL���������
    /// </summary>
    public class DbParam
    {
        #region �ֶ�
        private readonly string _name;
        private readonly string _type;
        private object _value;
        #endregion

        #region ����
        /// <summary>
        /// ������
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// ��������:��String,DateTime��
        /// </summary>
        public string Type
        {
            get { return _type; }
        }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        #region ���캯��
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
