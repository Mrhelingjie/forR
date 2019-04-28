using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Havsh.Application.Dal
{
    /// <summary>
    /// ��װִ��SQL�������������
    /// </summary>
    public class DbParamCollection
    {
        #region �ֶ�
        /// <summary>
        /// ����������
        /// </summary>
        private List<string> _nameCollection;
        /// <summary>
        /// ��������
        /// </summary>
        private Dictionary<string, DbParam> _parameters;
        #endregion

        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        public int Count
        {
            get { return _nameCollection.Count; }
        }
        #endregion

        #region ���캯��
        public DbParamCollection()
        {
            _nameCollection = new List<string>();
            _parameters = new Dictionary<string, DbParam>();
        }
        #endregion

        #region ����
        public void Add(DbParam parameter)
        {
            _nameCollection.Add(parameter.Name.ToLower());
            _parameters.Add(parameter.Name.ToLower(), parameter);
        }
        #endregion

        #region ����
        public DbParam this[int index]
        {
            get { return _parameters[_nameCollection[index]]; }
        }
        public DbParam this[string key]
        {
            get { return _parameters[key.ToLower()]; }
        }
        #endregion

        #region ����
        public IEnumerator GetEnumerator()
        {
            foreach (KeyValuePair<string, DbParam> param in _parameters)
            {
                yield return param.Value;
            }
        }
        #endregion
    }
}
