using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MCSFramework.Common;
using MCSFramework.IFStrategy;

namespace MCSFramework.BLL
{
    public sealed class DataAccess
    {
        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(string FullClassName)
        {
            return CreateObject(FullClassName, true);
        }

        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(string FullClassName, bool byCache)
        {
            object objType = null;
            string CacheKey = "DataAccessCache-" + FullClassName;

            if (byCache)
                objType = DataCache.GetCache(CacheKey);//从缓存读取

            if (objType == null)
            {
                try
                {
                    string AssemblyPath = FullClassName.Substring(0, FullClassName.LastIndexOf('.'));
                    objType = Assembly.Load(AssemblyPath).CreateInstance(FullClassName);//反射创建
                    if (byCache) DataCache.SetCache(CacheKey, objType);// 写入缓存
                }
                catch
                { }
            }
            return objType;
        }
    }
}
