﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace BasicLearn.Javascript.Part1
{
    /// <summary>
    /// 用于方便使用Cookie的扩展工具类
    /// </summary>
    public static class CookieExtension
    {
        // 我们可以为一些使用频率高的类型写专门的【读取】方法

        /// <summary>
        /// 从一个Cookie中读取字符串值。
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static string GetString(this HttpCookie cookie)
        {
            if (cookie == null)
                return null;

            return cookie.Value;
        }

        /// <summary>
        /// 从一个Cookie中读取 Int 值。
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        /*public static int ToInt(this HttpCookie cookie, int defaultVal)
        {
            if (cookie == null)
                return defaultVal;

            return cookie.Value.TryToInt(defaultVal);
        }
        */
        /// <summary>
        /// 从一个Cookie中读取值并转成指定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static T ConverTo<T>(this HttpCookie cookie)
        {
            if (cookie == null)
                return default(T);

            return (T)Convert.ChangeType(cookie.Value, typeof(T));
        }

        /// <summary>
        /// 从一个Cookie中读取【JSON字符串】值并反序列化成一个对象，用于读取复杂对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static T FromJson<T>(this HttpCookie cookie)
        {
            if (cookie == null)
                return default(T);

            return cookie.Value.FromJson<T>();
        }


        /// <summary>
        /// 将一个对象写入到Cookie
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="expries"></param>
        public static void WriteCookie(this object obj, string name, DateTime? expries)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");


            HttpCookie cookie = new HttpCookie(name, obj.ToString());

            if (expries.HasValue)
                cookie.Expires = expries.Value;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 删除指定的Cookie
        /// </summary>
        /// <param name="name"></param>
        public static void DeleteCookie(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            HttpCookie cookie = new HttpCookie(name);

            // 删除Cookie，其实就是设置一个【过期的日期】
            cookie.Expires = new DateTime(1900, 1, 1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 将一个对象序列化成 JSON 格式字符串
        /// </summary>
        public static string ToJson(this object obj)
        {
            if (obj == null)
                return string.Empty;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        /// <summary>
        /// 从JSON字符串中反序列化对象
        /// </summary>
        public static T FromJson<T>(this string cookie)
        {
            if (string.IsNullOrEmpty(cookie))
                return default(T);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(cookie);
        }
    }

}