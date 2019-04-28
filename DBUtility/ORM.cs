using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Havsh.Application.Dal
{
    /// <summary>
    /// 数据库对象关系映射，已测支持MSSQL，Access
    /// <para>适用场合</para>
    /// <para>1.单主键查询单条信息</para>
    /// <para>2.数据插入</para>
    /// <para>3.单主键单条数据修改，</para>
    /// <para>4.单主键（单/多）条数据删除</para>
    /// </summary>
    public class ORM
    {
        #region 字段
        private readonly string _name;
        private DbParamCollection _parameters;
        private DataTable _schema;
        private int _keyIndex;             //不设置，默认代表根据表的构架获取主键索引
        private string _keyName;
        private bool _isExist;
        #endregion

        #region 属性
        /// <summary>
        /// 映射的表，视图名
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// 命令参数集合
        /// </summary>
        public DbParamCollection Parameters
        {
            get { return _parameters; }
        }
        /// <summary>
        /// 映射表，视图的架构
        /// </summary>
        public DataTable Schema
        {
            get { return _schema; }
        }
        /// <summary>
        /// 主键列索引位置
        /// </summary>
        public int KeyIndex
        {
            get { return _keyIndex; }
            set { _keyIndex = value; }
        }
        /// <summary>
        /// 主键列名
        /// </summary>
        public string KeyName
        {
            get { return _keyName; }
            set { _keyName = value; }
        }
        /// <summary>
        /// 通过构造函数中传递的value判断是否存在该条信息
        /// </summary>
        public bool IsExist
        {
            get { return _isExist; }
        }
        #endregion

        #region 私有方法
        private void GetSchema()
        {
            _schema = Database.Instance.GetTableReflectionSchema(_name);
            InitialParameters();

        }
        private void InitialParameters()
        {
            _parameters = new DbParamCollection();
            bool hasKey = false;//设置一次主键
            foreach (DataRow dr in _schema.Rows)
            {
                //表有主键
                if (!hasKey)
                {
                    if (dr["IsKey"].ToString() == "True")
                    {
                        hasKey = true;
                        _keyIndex = int.Parse(dr["ColumnOrdinal"].ToString());
                        _keyName = dr["ColumnName"].ToString();
                    }
                }
                _parameters.Add(new DbParam(dr["ColumnName"].ToString(), dr["DataType"].ToString().Replace("System.", "")));
            }
            //表无主键,默认为序号为0的列
            if (!hasKey)
            {
                _keyIndex = 0;
                _keyName = _parameters[0].Name;
            }
        }
        private void SetParameters(object value)
        {
            string strSql = string.Format("select * from {0} where {1}=@{1}", _name, _parameters[_keyIndex].Name);
            DbParamCollection parameters = new DbParamCollection();
            parameters.Add(new DbParam(_parameters[_keyIndex].Name, _parameters[_keyIndex].Type, value));
            DataSet ds = Database.Instance.ExecuteSelect(strSql, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                _isExist = true;
                int index = 0;
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    _parameters[index].Value = ds.Tables[0].Rows[0][dc];
                    index++;
                }
            }
            else
            {
                _isExist = false;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 映射表，视图架构
        /// </summary>
        /// <param name="name">表，视图名</param>
        public ORM(string name)
        {
            _name = name;
            GetSchema();
        }
        /// <summary>
        /// 映射的主键取得单条信息
        /// </summary>
        /// <param name="name">表，视图名</param>
        /// <param name="value">主键值</param>
        public ORM(string name, object value)
            : this(name)
        {
            SetParameters(value);
        }
        /// <summary>
        /// 自定义键在表中的索引位置(以1开始)取得单条信息
        /// </summary>
        /// <param name="name">表，视图名</param>
        /// <param name="value">键值</param>
        /// <param name="keyIndex">键索引(从0开始)</param>
        public ORM(string name, object value, int keyIndex)
            : this(name)
        {
            _keyIndex = keyIndex;
            _keyName = _parameters[_keyIndex].Name;
            SetParameters(value);
        }
        /// <summary>
        /// 自定义键名取得单条信息
        /// </summary>
        /// <param name="name">表，视图名</param>
        /// <param name="value">键值</param>
        /// <param name="keyName">键名</param>
        public ORM(string name, object value, string keyName)
            : this(name)
        {
            _keyName = keyName;
            for (int i = 0; i < _parameters.Count; i++)
            {
                if (_parameters[i].Name.ToLower() == keyName.ToLower())
                {
                    _keyIndex = i;
                    break;
                }
            }
            SetParameters(value);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 插入数据
        /// </summary>
        public int Insert()
        {
            StringBuilder first_Sentence = new StringBuilder(61);
            StringBuilder last_Sentence = new StringBuilder(61);
            DbParamCollection insertParameters = new DbParamCollection();
            first_Sentence.AppendFormat("insert into {0}(", _name);
            last_Sentence.Append(" values (");
            int index = 0;
            foreach (DbParam parameter in _parameters)
            {
                //非标识列
                if (_schema.Rows[index]["IsAutoIncrement"].ToString() != "True")
                {
                    //赋值的字段插入数据库
                    if (parameter.Value != null && parameter.Value.ToString() != string.Empty)
                    {
                        first_Sentence.AppendFormat("[{0}],", parameter.Name);
                        last_Sentence.AppendFormat("@{0},", parameter.Name);
                        insertParameters.Add(_parameters[index]);
                    }
                }
                index++;
            }
            if (first_Sentence[first_Sentence.Length - 1] == ',')
            {
                first_Sentence.Remove(first_Sentence.Length - 1, 1);
                first_Sentence.Append(")");
                last_Sentence.Remove(last_Sentence.Length - 1, 1);
                last_Sentence.Append(")");
                first_Sentence.Append(last_Sentence.ToString());
                return Database.Instance.ExecuteNonQuery(first_Sentence.ToString(), insertParameters);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        public int Update()
        {
            StringBuilder sentence = new StringBuilder(131);
            DbParamCollection updateParameters = new DbParamCollection();
            sentence.AppendFormat("update {0} set ", _name);
            int index = 0;
            foreach (DbParam parameter in _parameters)
            {
                //非标识列或主键列
                if (_schema.Rows[index]["IsAutoIncrement"].ToString() != "True" && _keyIndex != index)
                {
                    //赋值的字段插入数据库
                    if (parameter.Value != null && parameter.Value.ToString() != string.Empty)
                    {
                        sentence.AppendFormat("[{0}]=@{0},", parameter.Name);
                        updateParameters.Add(_parameters[index]);
                    }
                }
                index++;
            }
            if (sentence[sentence.Length - 1] == ',')
            {
                sentence.Remove(sentence.Length - 1, 1);
                sentence.AppendFormat(" where [{0}]=@{0}", _parameters[_keyIndex].Name);
                updateParameters.Add(_parameters[_keyIndex]);
                return Database.Instance.ExecuteNonQuery(sentence.ToString(), updateParameters);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键值</param>
        public int Delete(params object[] id)
        {
            StringBuilder sentence = new StringBuilder(131);
            DbParamCollection deleteParameters = new DbParamCollection();
            sentence.AppendFormat("delete from [{0}] where [{1}] in (", _name, _parameters[_keyIndex].Name);
            for (int i = 0; i < id.Length; i++)
            { 
                string tempParam = string.Format("@{0}{1}",_parameters[_keyIndex].Name,i);
                sentence.AppendFormat("{0},", tempParam);
                deleteParameters.Add(new DbParam(tempParam, _parameters[_keyIndex].Type, id[i]));
            }
            if (sentence[sentence.Length - 1] == ',')
            {
                sentence.Remove(sentence.Length - 1, 1);
                sentence.Append(")");
                return Database.Instance.ExecuteNonQuery(sentence.ToString(), deleteParameters);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键值</param>
        public int Delete(string ids)
        {
            StringBuilder sentence = new StringBuilder(131);
            DbParamCollection deleteParameters = new DbParamCollection();
            sentence.AppendFormat("delete from [{0}] where [{1}] in (", _name, _parameters[_keyIndex].Name);
            string[] id = ids.Split(',');
            for (int i = 0; i < id.Length; i++)
            {
                string tempParam = string.Format("@{0}{1}", _parameters[_keyIndex].Name, i);
                sentence.AppendFormat("{0},", tempParam);
                deleteParameters.Add(new DbParam(tempParam, _parameters[_keyIndex].Type, id[i]));
            }
            if (sentence[sentence.Length - 1] == ',')
            {
                sentence.Remove(sentence.Length - 1, 1);
                sentence.Append(")");
                return Database.Instance.ExecuteNonQuery(sentence.ToString(), deleteParameters);
            }
            else
            {
                return -1;
            }
        }
        #endregion
    }
}
