/*******************************************************************************
 * 作者：星星    
 * 描述：json序列化操作   
 * 修改记录： 
*********************************************************************************/

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Panda.Code
{
    public static class JsonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize<T>(T data)
        {
            if (data == null)
                throw new NullReferenceException("Serialize 方法的{ data }参数值为空。");

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string data)
        {
            if (data == null)
                throw new NullReferenceException("Deserialize 方法的{ data }参数值为空。");

            return JsonConvert.DeserializeObject<T>(data);
        }


        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }

        public static T ToObject<T>(this string Json)
        {
            return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
        }

        public static List<T> ToList<T>(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }

    }
}