/*******************************************************************************
 * 作者：星星    
 * 描述：缓存操作接口   
 * 修改记录： 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Panda.Code.Cache
{
    public class MemoryCache : ICache
    {
        private static System.Runtime.Caching.MemoryCache cache = new System.Runtime.Caching.MemoryCache("Panda Cache");

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                new NullReferenceException("Get方法的{key}参数值为空。");

            return (T)cache[key];
        }


        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">缓存值</param>
        /// <param name="expireTime">超时时间（按分钟）,大于0有效</param>
        /// <returns></returns>
        public void Set<T>(string key, T data, int? expireTime) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
                new NullReferenceException("Set方法的{key}参数值为空。");

            if (data == null)
                new NullReferenceException("Set方法的{data}参数值为空。");

            CacheItemPolicy policy = null;
            if (expireTime.HasValue && expireTime > 0)
            {
                policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(expireTime.Value);
            }
            cache.Set(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// 缓存是否已存在
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public bool Exist(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                new NullReferenceException("Exist方法的{key}参数值为空。");
            return (cache.Contains(key));
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="prefix">前缀</param>
        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                new NullReferenceException("Remove方法的{key}参数值为空。");

            cache.Remove(key);
        }

        ///// <summary>
        ///// 删除所有缓存
        ///// </summary>
        //public void Remove()
        //{
        //    cache.Trim(100);
        //}

        /// <summary>
        /// 合并key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public string MergeKey(string key, string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                new NullReferenceException("MergeKey方法的{prefix}参数值为空。");

            return string.Format("{0}:{1}", prefix, key);
        }
    }
}
