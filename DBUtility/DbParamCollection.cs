using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Havsh.Application.Dal
{
    /// <summary>
    /// 封装执行SQL的命令参数集合
    /// </summary>
    public class DbParamCollection
    {
        #region 字段
        /// <summary>
        /// 参数名集合
        /// </summary>
        private List<string> _nameCollection;
        /// <summary>
        /// 参数集合
        /// </summary>
        private Dictionary<string, DbParam> _parameters;
        #endregion

        #region 属性
        /// <summary>
        /// 参数个数
        /// </summary>
        public int Count
        {
            get { return _nameCollection.Count; }
        }
        #endregion

        #region 构造函数
        public DbParamCollection()
        {
            _nameCollection = new List<string>();
            _parameters = new Dictionary<string, DbParam>();
        }
        #endregion

        #region 方法
        public void Add(DbParam parameter)
        {
            _nameCollection.Add(parameter.Name.ToLower());
            _parameters.Add(parameter.Name.ToLower(), parameter);
        }
        #endregion

        #region 索引
        public DbParam this[int index]
        {
            get { return _parameters[_nameCollection[index]]; }
        }
        public DbParam this[string key]
        {
            get { return _parameters[key.ToLower()]; }
        }
        #endregion

        #region 迭代
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
