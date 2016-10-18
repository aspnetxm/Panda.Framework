/*******************************************************************************
 * 作者：星星    
 * 描述：config 文件读取   
 * 修改记录： 
*********************************************************************************/
using System;
using System.Configuration;

namespace Panda.Code.Configs
{
    public class Config
    {
        #region 得到Connection中配置字符串信息
        /// <summary>
        /// 得到Connection中配置字符串信息
        /// </summary>
        /// <param name="key">Connection中name的值</param>
        /// <returns>Connection中name的值</returns>
        public static string GetConnection(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }
        #endregion

        #region 得到AppSettings中的配置字符串信息
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key">AppSetting中关键字KEY</param>
        /// <returns>AppSettings中的配置字符串信息</returns>
        public static string GetString(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        #endregion



        #region 得到AppSettings中的配置Bool信息
        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBool(string key)
        {
            bool result = false;
            string cfgVal = GetString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        #endregion

        #region 得到AppSettings中的配置Decimal信息
        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        #endregion

        #region 得到AppSettings中的配置int信息
        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            int result = 0;
            string cfgVal = GetString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        #endregion
    }
}
