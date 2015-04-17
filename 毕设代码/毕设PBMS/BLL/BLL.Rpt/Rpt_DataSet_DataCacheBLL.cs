
// ===================================================================
// 文件： Rpt_DataSet_DataCacheDAL.cs
// 项目名称：
// 创建时间：2010/9/28
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using MCSFramework.Model.RPT;
using MCSFramework.SQLDAL.RPT;
using System.Collections;

namespace MCSFramework.BLL.RPT
{
    /// <summary>
    ///Rpt_DataSet_DataCacheBLL业务逻辑BLL类
    /// </summary>
    public class Rpt_DataSet_DataCacheBLL : BaseSimpleBLL<Rpt_DataSet_DataCache>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_DataSet_DataCacheDAL";
        private Rpt_DataSet_DataCacheDAL _dal;

        #region 构造函数
        ///<summary>
        ///Rpt_DataSet_DataCacheBLL
        ///</summary>
        public Rpt_DataSet_DataCacheBLL()
            : base(DALClassName)
        {
            _dal = (Rpt_DataSet_DataCacheDAL)_DAL;
            _m = new Rpt_DataSet_DataCache();
        }

        public Rpt_DataSet_DataCacheBLL(int id)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSet_DataCacheDAL)_DAL;
            FillModel(id);
        }

        public Rpt_DataSet_DataCacheBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSet_DataCacheDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Rpt_DataSet_DataCache> GetModelList(string condition)
        {
            return new Rpt_DataSet_DataCacheBLL()._GetModelList(condition);
        }
        #endregion

        #region 最初缓存写入方式
        /// <summary>
        /// 保存数据缓存
        /// </summary>
        /// <param name="DataSet"></param>
        /// <param name="ParamValues"></param>
        /// <param name="dt"></param>
        public static void SaveDataSetCache0(Guid DataSet, Dictionary<string, object> ParamValues, DataTable dt)
        {
            string values = "";

            foreach (string param in ParamValues.Keys)
            {
                if (ParamValues[param] != null)
                {
                    values += param + "=" + ParamValues[param].ToString();
                }
            }

            try
            {
                System.GC.Collect();
                MemoryStream memory = new MemoryStream();

                //dt.TableName = DataSet.ToString();
                //dt.WriteXml(memory, XmlWriteMode.WriteSchema, false);

                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(memory, dt);

                byte[] buff = memory.GetBuffer();

                Rpt_DataSet_DataCacheBLL bll = new Rpt_DataSet_DataCacheBLL();
                bll.Model.DataSet = DataSet;
                bll.Model.ParamValues = values;
                bll.Model.DataLen = buff.Length;
                bll.Model.Data = buff;

                bll.Add();

                memory.Close();
                memory.Dispose();
                buff = null;
            }
            catch { System.GC.Collect(); }
        }
        /// <summary>
        /// 载入数据缓存
        /// </summary>
        /// <param name="DataSet"></param>
        /// <param name="ParamValues"></param>
        /// <returns></returns>
        public static DataTable LoadDataSetCache0(Guid DataSet, Dictionary<string, object> ParamValues, out DateTime UpdateTime)
        {
            DataTable dt = null;
            string values = "";
            UpdateTime = new DateTime(1900, 1, 1);

            foreach (string param in ParamValues.Keys)
            {
                if (ParamValues[param] != null)
                {
                    values += param + "=" + ParamValues[param].ToString();
                }
            }
            try
            {
                System.GC.Collect();
                Rpt_DataSet_DataCacheDAL dal = (Rpt_DataSet_DataCacheDAL)DataAccess.CreateObject(DALClassName);
                Rpt_DataSet_DataCache cache = dal.Load(DataSet, values);

                if (cache != null)
                {
                    MemoryStream memory = new MemoryStream(cache.Data);

                    BinaryFormatter b = new BinaryFormatter();
                    dt = (DataTable)b.Deserialize(new MemoryStream(cache.Data));
                    //dt = new DataTable();
                    //dt.ReadXml(memory);

                    UpdateTime = cache.UpdateTime;

                    memory.Close();
                    memory.Dispose();
                    System.GC.Collect();
                }
            }
            catch { System.GC.Collect(); }
            return dt;
        }

        #endregion

        /// <summary>
        /// 保存数据缓存
        /// </summary>
        /// <param name="DataSet"></param>
        /// <param name="ParamValues"></param>
        /// <param name="dt"></param>
        public static void SaveDataSetCache(Guid DataSet, Dictionary<string, object> ParamValues, DataTable dt)
        {
            Thread th = new Thread(new ParameterizedThreadStart(thSaveCache));
            Hashtable arg = new Hashtable();
            arg.Add("DataSet", DataSet);
            arg.Add("ParamValues", ParamValues);
            arg.Add("DataTable", dt.Copy());

            th.Start(arg);
        }

        private static void thSaveCache(object arg)
        {
            Guid DataSet = (Guid)((Hashtable)arg)["DataSet"];
            Dictionary<string, object> ParamValues = (Dictionary<string, object>)((Hashtable)arg)["ParamValues"];
            DataTable dt = (DataTable)((Hashtable)arg)["DataTable"];

            string values = "";

            foreach (string param in ParamValues.Keys)
            {
                if (ParamValues[param] != null)
                {
                    values += param + "=" + ParamValues[param].ToString();
                }
            }

            try
            {
                System.GC.Collect();

                dt.TableName = DataSet.ToString();

                MemoryStream memory = new MemoryStream();
                dt.WriteXmlSchema(memory);
                int SchemaLength = (int)memory.Length;

                #region 数据表内容二进制化
                StringBuilder s = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] == DBNull.Value)
                            s.Append("|NULL|");
                        else
                        {
                            if (dt.Columns[j].DataType.Name == "DateTime")
                                s.Append(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss").Replace("|5A|", "").Replace("|5B|", "").Replace("|NULL|", ""));
                            else
                                s.Append(dt.Rows[i][j].ToString().Replace("|5A|", "").Replace("|5B|", "").Replace("|NULL|", ""));
                        }
                        s.Append("|5A|");
                    }
                    s.Append("|5B|");
                }
                byte[] databuff = System.Text.Encoding.UTF8.GetBytes(s.ToString());
                s.Remove(0, s.Length);
                memory.Write(databuff, 0, databuff.Length);
                #endregion

                byte[] buff = memory.GetBuffer();
                memory.Close();
                memory.Dispose();

                #region 写入数据库
                Rpt_DataSet_DataCacheBLL bll = new Rpt_DataSet_DataCacheBLL();
                bll.Model.DataSet = DataSet;
                bll.Model.ParamValues = values;
                bll.Model.DataLen = SchemaLength;
                bll.Model.Data = buff;
                bll.Model["TotalLen"] = buff.Length.ToString();
                bll.Add();
                bll = null;
                #endregion

                Array.Clear(buff, 0, buff.Length);
                buff = null;
                System.GC.Collect();
            }
            catch { System.GC.Collect(); }
        }
        /// <summary>
        /// 载入数据缓存
        /// </summary>
        /// <param name="DataSet"></param>
        /// <param name="ParamValues"></param>
        /// <returns></returns>
        public static DataTable LoadDataSetCache(Guid DataSet, Dictionary<string, object> ParamValues, out DateTime UpdateTime)
        {
            DataTable dt = null;
            string values = "";
            UpdateTime = new DateTime(1900, 1, 1);

            foreach (string param in ParamValues.Keys)
            {
                if (ParamValues[param] != null)
                {
                    values += param + "=" + ParamValues[param].ToString();
                }
            }
            try
            {
                System.GC.Collect();
                Rpt_DataSet_DataCacheDAL dal = (Rpt_DataSet_DataCacheDAL)DataAccess.CreateObject(DALClassName);
                Rpt_DataSet_DataCache cache = dal.Load(DataSet, values);

                if (cache != null)
                {
                    UpdateTime = cache.UpdateTime;

                    #region 获取表架构
                    MemoryStream memorySchema = new MemoryStream(cache.Data, 0, cache.DataLen);
                    dt = new DataTable();
                    dt.ReadXmlSchema(memorySchema);
                    memorySchema.Close();
                    memorySchema.Dispose();
                    #endregion

                    #region 获取表内容
                    string[] _rows = System.Text.Encoding.UTF8.GetString(cache.Data, cache.DataLen, cache.Data.Length - cache.DataLen).Split
                        (new string[] { "|5B|" }, StringSplitOptions.RemoveEmptyEntries);
                    cache = null;

                    System.GC.Collect();

                    for (int i = 0; i < _rows.Length; i++)
                    {
                        DataRow dr = dt.NewRow();

                        string[] _columns = _rows[i].Split(new string[] { "|5A|" }, StringSplitOptions.None);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (_columns[j] == "|NULL|")
                                dr[j] = DBNull.Value;
                            else
                                dr[j] = _columns[j];
                        }
                        dt.Rows.Add(dr);
                    }
                    #endregion

                    System.GC.Collect();
                }
            }
            catch { System.GC.Collect(); }
            return dt;
        }

        /// <summary>
        /// 清除某数据集的缓存
        /// </summary>
        /// <param name="DataSet"></param>
        /// <returns></returns>
        public static int ClearDataSetCache(Guid DataSet)
        {
            Rpt_DataSet_DataCacheDAL dal = (Rpt_DataSet_DataCacheDAL)DataAccess.CreateObject(DALClassName);
            return dal.Clear(DataSet);
        }
        /// <summary>
        /// 永久保存缓存
        /// </summary>
        /// <param name="DataSet"></param>
        /// <param name="ParamValues"></param>
        /// <returns></returns>
        public static int SaveForever(Guid DataSet, Dictionary<string, object> ParamValues)
        {
            string values = "";
            foreach (string param in ParamValues.Keys)
            {
                if (ParamValues[param] != null)
                {
                    values += param + "=" + ParamValues[param].ToString();
                }
            }
            Rpt_DataSet_DataCacheDAL dal = (Rpt_DataSet_DataCacheDAL)DataAccess.CreateObject(DALClassName);
            return dal.SaveForever(DataSet, values);
        }
    }
}