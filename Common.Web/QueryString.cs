﻿using System.Text.RegularExpressions;

namespace System.Web
{
    /// <summary>
    /// QueryString 地址栏参数
    /// </summary>
    public class QueryString
    {
        /// <summary>
        /// 等于Request.QueryString;如果为null 返回 空“” ，否则返回Request.QueryString[name]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Q(string name)
        {
            return Request.QueryString[name] ?? "";
        }

        /// <summary>
        /// 等于  Request.Form  如果为null 返回 空“” ，否则返回 Request.Form[name]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string F(string name)
        {
            return Request.Form[name] == null ? "" : Request.Form[name].ToString();
        }

        /// <summary>
        /// 获取url中的id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int QId(string name)
        {
            return StrToId(Q(name));
        }

        /// <summary>
        /// 获取正确的Id，如果不是正整数，返回0
        /// </summary>
        /// <param name="_value"></param>
        /// <returns>返回正确的整数ID，失败返回0</returns>
        public static int StrToId(string _value)
        {
            return IsNumberId(_value) ? int.Parse(_value) : 0;
        }

        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumberId(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null)
            {
                return false;
            }

            var myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }

        /// <summary>
        /// HttpContext Current
        /// </summary>
        public static HttpContext Current => HttpContext.Current;

        /// <summary>
        /// HttpContext Current  HttpRequest Request   get { return Current.Request;
        /// </summary>
        public static HttpRequest Request => Current.Request;

        /// <summary>
        ///  HttpContext Current  HttpRequest Request   get { return Current.Request; HttpResponse Response  return Current.Response;
        /// </summary>
        public static HttpResponse Response => Current.Response;
    }
}