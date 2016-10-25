/*******************************************************************************
 * 作者：星星    
 * 描述：缓存操作接口   
 * 修改记录： 
*********************************************************************************/

using System;

namespace Panda.Code.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">缓存值</param>
        /// <param name="expireTime">超时时间（按分钟）,大于0有效</param>
        void Set(string key, dynamic data, int? expireTime); 

        /// <summary>
        /// 缓存是否已存在
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        bool Exist(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        ///// <summary>
        ///// 删除所有缓存
        ///// </summary>
        //void Remove();
    }
}
