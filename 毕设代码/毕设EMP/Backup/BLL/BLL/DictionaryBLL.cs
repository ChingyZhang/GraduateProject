using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.IFStrategy;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using MCSFramework.Common;
using System.Web.Caching;
using System.Linq;

namespace MCSFramework.BLL
{
    public class DictionaryBLL : BaseSimpleBLL<Dictionary_Data>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Dictionary_DAL";
        private Dictionary_DAL _dal;

        #region 构建BLL
        public DictionaryBLL()
            : base(DALClassName)
        {
            _dal = (Dictionary_DAL)_DAL;
            _m = new Dictionary_Data();    //实例化Model
        }

        public DictionaryBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Dictionary_DAL)_DAL;
            FillModel(id);
        }

        public DictionaryBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Dictionary_DAL)_DAL;
            FillModel(id, bycache);
        }

        #endregion

        #region 获取指定类别的字典内容
        /// <summary>
        /// 获取指定类别的字典内容
        /// </summary>
        /// <param name="tablename">字典名称</param>
        /// <param name="onlyenbaled">是否仅获取启用项</param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary_Data> GetDicCollections(string tablename, bool onlyenbaled)
        {
            string CacheKey = "Cache-Dictionary_Data-Enabled-" + onlyenbaled.ToString();

            Dictionary<string, Dictionary<string, Dictionary_Data>> dicdatas =
                (Dictionary<string, Dictionary<string, Dictionary_Data>>)DataCache.GetCache(CacheKey);

            if (dicdatas == null)
            {
                #region 从数据库中获取所有字典数据
                IList<Dictionary_Type> dictypelist = DictionaryBLL.Dictionary_Type_GetAllList();
                IList<Dictionary_Data> models = DictionaryBLL.Dictionary_Data_GetAlllList().OrderBy(p => int.Parse(p.Code)).ToList();

                dicdatas = new Dictionary<string, Dictionary<string, Dictionary_Data>>();

                //将IList队列转为字典型队列
                foreach (Dictionary_Data m in models)
                {
                    if (onlyenbaled && m.Enabled == "N") continue;
                    string key = m.TableName;
                    if (!string.IsNullOrEmpty(key))
                    {
                        if (dicdatas.ContainsKey(key))
                        {
                            dicdatas[key].Add(m.Code, m);
                        }
                        else
                        {
                            Dictionary<string, Dictionary_Data> subdic = new Dictionary<string, Dictionary_Data>();
                            subdic.Add(m.Code, m);

                            dicdatas.Add(key, subdic);
                        }
                    }
                }
                #endregion

                #region 写入缓存

                //创建缓存SQL依赖
                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Dictionary_Data"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Dictionary_Type"));

                DataCache.SetCache(CacheKey, dicdatas, cachedependency);
                #endregion
            }

            if (!dicdatas.ContainsKey(tablename))
                return new Dictionary<string, Dictionary_Data>();

            return dicdatas[tablename];
        }

        /// <summary>
        /// 获取指定类别的仅为启用的字典内容
        /// </summary>
        /// <param name="tablename">字典名称</param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary_Data> GetDicCollections(string tablename)
        {
            return GetDicCollections(tablename, true);
        }

        public static Dictionary<string, Dictionary_Data> GetDicCollections(int dictype, bool onlyenbaled)
        {
            string tablename = Dictionary_Type_GetModel(dictype).TableName;

            return GetDicCollections(tablename, onlyenbaled);
        }

        public static Dictionary<string, Dictionary_Data> GetDicCollections(int dictype)
        {
            return GetDicCollections(dictype, true);
        }

        #endregion

        #region 返回字典表类别
        /// <summary>
        /// 返回当前所有的字典表类别
        /// </summary>
        /// <returns></returns>
        public static IList<Dictionary_Type> Dictionary_Type_GetAllList()
        {
            Dictionary_DAL dal = (Dictionary_DAL)DataAccess.CreateObject(DALClassName);
            return dal.Dictionary_Type_GetAll();
        }

        /// <summary>
        /// 返回指定ID的字典表类别Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Dictionary_Type Dictionary_Type_GetModel(int id)
        {
            Dictionary_DAL dal = (Dictionary_DAL)DataAccess.CreateObject(DALClassName);
            return dal.Dictionary_Type_GetModel(id);
        }
        #endregion

        #region 获取字典项目数据
        /// <summary>
        /// 按条件获取字典项目数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IList<Dictionary_Data> Dictionary_Data_GetAlllList(string condition)
        {
            DictionaryBLL bll = new DictionaryBLL();
            return bll._GetModelList(condition);
        }

        /// <summary>
        /// 获取所有字典项目数据
        /// </summary>
        /// <returns></returns>
        public static IList<Dictionary_Data> Dictionary_Data_GetAlllList()
        {
            return Dictionary_Data_GetAlllList("");
        }
        #endregion

    }

}
