using System;
using System.Configuration;

namespace Maticsoft.DBUtility
{
    
    public class PubConstant
    {        
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        public static string ConnectionString
        {           
            get 
            {
                //string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];       
                //string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                //if (ConStringEncrypt == "true")
                //{
                //    _connectionString = DESEncrypt.Decrypt(_connectionString);
                //}
                //return _connectionString; 
                Mejoy.DataAccess.BaseDataAccess sqlconnection = new Mejoy.DataAccess.BaseDataAccess();
                return sqlconnection.GetConnection;
            }
        }

        /// <summary>
        /// �õ�web.config������������ݿ������ַ�����
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            Mejoy.DataAccess.BaseDataAccess sqlconnection = new Mejoy.DataAccess.BaseDataAccess();
            return sqlconnection.GetConnection;
        }


    }
}
