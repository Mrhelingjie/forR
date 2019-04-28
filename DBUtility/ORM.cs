using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Havsh.Application.Dal
{
    /// <summary>
    /// ���ݿ�����ϵӳ�䣬�Ѳ�֧��MSSQL��Access
    /// <para>���ó���</para>
    /// <para>1.��������ѯ������Ϣ</para>
    /// <para>2.���ݲ���</para>
    /// <para>3.���������������޸ģ�</para>
    /// <para>4.����������/�ࣩ������ɾ��</para>
    /// </summary>
    public class ORM
    {
        #region �ֶ�
        private readonly string _name;
        private DbParamCollection _parameters;
        private DataTable _schema;
        private int _keyIndex;             //�����ã�Ĭ�ϴ�����ݱ�Ĺ��ܻ�ȡ��������
        private string _keyName;
        private bool _isExist;
        #endregion

        #region ����
        /// <summary>
        /// ӳ��ı���ͼ��
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public DbParamCollection Parameters
        {
            get { return _parameters; }
        }
        /// <summary>
        /// ӳ�����ͼ�ļܹ�
        /// </summary>
        public DataTable Schema
        {
            get { return _schema; }
        }
        /// <summary>
        /// ����������λ��
        /// </summary>
        public int KeyIndex
        {
            get { return _keyIndex; }
            set { _keyIndex = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string KeyName
        {
            get { return _keyName; }
            set { _keyName = value; }
        }
        /// <summary>
        /// ͨ�����캯���д��ݵ�value�ж��Ƿ���ڸ�����Ϣ
        /// </summary>
        public bool IsExist
        {
            get { return _isExist; }
        }
        #endregion

        #region ˽�з���
        private void GetSchema()
        {
            _schema = Database.Instance.GetTableReflectionSchema(_name);
            InitialParameters();

        }
        private void InitialParameters()
        {
            _parameters = new DbParamCollection();
            bool hasKey = false;//����һ������
            foreach (DataRow dr in _schema.Rows)
            {
                //��������
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
            //��������,Ĭ��Ϊ���Ϊ0����
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

        #region ���캯��
        /// <summary>
        /// ӳ�����ͼ�ܹ�
        /// </summary>
        /// <param name="name">����ͼ��</param>
        public ORM(string name)
        {
            _name = name;
            GetSchema();
        }
        /// <summary>
        /// ӳ�������ȡ�õ�����Ϣ
        /// </summary>
        /// <param name="name">����ͼ��</param>
        /// <param name="value">����ֵ</param>
        public ORM(string name, object value)
            : this(name)
        {
            SetParameters(value);
        }
        /// <summary>
        /// �Զ�����ڱ��е�����λ��(��1��ʼ)ȡ�õ�����Ϣ
        /// </summary>
        /// <param name="name">����ͼ��</param>
        /// <param name="value">��ֵ</param>
        /// <param name="keyIndex">������(��0��ʼ)</param>
        public ORM(string name, object value, int keyIndex)
            : this(name)
        {
            _keyIndex = keyIndex;
            _keyName = _parameters[_keyIndex].Name;
            SetParameters(value);
        }
        /// <summary>
        /// �Զ������ȡ�õ�����Ϣ
        /// </summary>
        /// <param name="name">����ͼ��</param>
        /// <param name="value">��ֵ</param>
        /// <param name="keyName">����</param>
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

        #region ����
        /// <summary>
        /// ��������
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
                //�Ǳ�ʶ��
                if (_schema.Rows[index]["IsAutoIncrement"].ToString() != "True")
                {
                    //��ֵ���ֶβ������ݿ�
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
        /// �޸�����
        /// </summary>
        public int Update()
        {
            StringBuilder sentence = new StringBuilder(131);
            DbParamCollection updateParameters = new DbParamCollection();
            sentence.AppendFormat("update {0} set ", _name);
            int index = 0;
            foreach (DbParam parameter in _parameters)
            {
                //�Ǳ�ʶ�л�������
                if (_schema.Rows[index]["IsAutoIncrement"].ToString() != "True" && _keyIndex != index)
                {
                    //��ֵ���ֶβ������ݿ�
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
        /// ɾ������
        /// </summary>
        /// <param name="id">����ֵ</param>
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
        /// ɾ������
        /// </summary>
        /// <param name="id">����ֵ</param>
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
