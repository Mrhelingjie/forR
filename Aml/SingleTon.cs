using System;
using System.Collections.Generic;
using System.Text;

namespace Havsh.Application.Aml
{
    /// <summary>
    /// 单列模式
    /// </summary>
    public class SingleTon<T> where T : new()
    {
        private static object _sync = new object();

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
