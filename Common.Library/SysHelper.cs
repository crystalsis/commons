﻿using System.Diagnostics;
using System.Threading;
using System.Web;

namespace System
{
    /// <summary>
    /// 系统操作相关的公共类
    /// </summary>
    public static class SysHelper
    {
        #region 获取文件相对路径映射的物理路径

        /// <summary>
        /// 获取文件相对路径映射的物理路径
        /// </summary>
        /// <param name="virtualPath">文件的相对路径</param>
        public static string GetPath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        #endregion 获取文件相对路径映射的物理路径

        #region 获取指定调用层级的方法名

        /// <summary>
        /// 获取指定调用层级的方法名
        /// </summary>
        /// <param name="level">调用的层数</param>
        public static string GetMethodName(int level)
        {
            //创建一个堆栈跟踪
            StackTrace trace = new StackTrace();

            //获取指定调用层级的方法名
            return trace.GetFrame(level).GetMethod().Name;
        }

        #endregion 获取指定调用层级的方法名

        #region 获取GUID值

        /// <summary>
        /// 获取GUID值
        /// </summary>
        public static string NewGUID
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        #endregion 获取GUID值

        #region 获取换行字符

        /// <summary>
        /// 获取换行字符
        /// </summary>
        public static string NewLine
        {
            get
            {
                return Environment.NewLine;
            }
        }

        #endregion 获取换行字符

        #region 获取当前应用程序域

        /// <summary>
        /// 获取当前应用程序域
        /// </summary>
        public static AppDomain CurrentAppDomain
        {
            get
            {
                return Thread.GetDomain();
            }
        }

        #endregion 获取当前应用程序域
    }
}