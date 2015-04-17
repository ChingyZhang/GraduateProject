using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//using MCSFramework.Common;
using MCSFramework.IFStrategy;
using MCSFramework.DBUtility;
using MCSFramework.Model;

namespace MCSFramework.SQLDAL
{
    public abstract class BaseSimpleDAL<T> : ISimpleDAL<T>
    {
        /// <summary>
        /// 存储过程前缀
        /// </summary>
        protected string _ProcePrefix = "";

        protected string _ConnectionStirng = null;

        #region ISimpleDAL<T> 成员

        /// <summary>
        /// 获取实例化后的Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetModel(int id)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.Int,4,id)				};
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetModel", parameters, out dr, SQLDatabase.DefaultTimeout);

            T m = default(T);
            if (dr.Read())
            {
                m = FillModel(dr);
            }
            dr.Close();

            return m;
        }

        /// <summary>
        /// 根据Code实例化Model
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual T GetModel(string code)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar,50,code)				};
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetModelByCode", parameters, out dr, SQLDatabase.DefaultTimeout);

            T m = default(T);
            if (dr.Read())
            {
                m = FillModel(dr);
            }
            dr.Close();

            return m;
        }

        /// <summary>
        /// 获取实例化后的Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetModel(Guid id)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier,16,id)				};
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetModel", parameters, out dr, SQLDatabase.DefaultTimeout);

            T m = default(T);
            if (dr.Read())
            {
                m = FillModel(dr);
            }
            dr.Close();

            return m;
        }

        /// <summary>
        /// 将Model信息新增到数据库中
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public abstract int Add(T m);

        /// <summary>
        /// 将Model信息更新到数据库中
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public abstract int Update(T m);

        /// <summary>
        /// 删除指定ID的记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete(int id)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.Int,4,id)				};
            return SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Delete", parameters, SQLDatabase.DefaultTimeout);
        }

        /// <summary>
        /// 删除指定ID的记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete(Guid id)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier,16,id)				};
            return SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Delete", parameters, SQLDatabase.DefaultTimeout);
        }
        /// <summary>
        /// 保存变更记录
        /// </summary>
        /// <param name="cur_M"></param>
        /// <returns></returns>
        public int SaveUpdateHistory(IModel cur_M)
        {
            try
            {



                IModel old_M = (IModel)GetModel(int.Parse(cur_M["ID"]));
                string tablename = _ProcePrefix.Substring(0, _ProcePrefix.IndexOf("dbo.") + 4) + cur_M.ModelName;
                IList<UD_TableList> tablelist = new UD_TableListDAL().GetModelList("Name='" + tablename + "'");
                if (tablelist.Count == 0) return 0;
                IList<UD_ModelFields> fieldlist = new UD_ModelFieldsDAL().GetModelList("Tableid='" + tablelist[0].ID + "'");
                Info_ChangeHistoryDAL _dal = new Info_ChangeHistoryDAL();
                Info_ChangeHistory _changemodel;
                foreach (UD_ModelFields field in fieldlist)
                {
                    if (field.FieldName == "UpdateTime" || field.FieldName == "InsertTime")
                        continue;
                    _changemodel = new Info_ChangeHistory();
                    if (!cur_M[field.FieldName].Equals(old_M[field.FieldName]))
                    {
                        _changemodel.InfoID = int.Parse(cur_M["ID"]);
                        _changemodel.TableID = tablelist[0].ID;
                        _changemodel.FieldID = field.ID;
                        _changemodel.OldValue = old_M[field.FieldName];
                        _changemodel.NewValue = cur_M[field.FieldName];
                        _changemodel.ChangeTime = DateTime.Now;

                        switch (field.RelationType)
                        {
                            case 1:     //关联字典表

                                IList<Dictionary_Data> dic = new Dictionary_DAL().GetModelList("Type=(SELECT ID FROM MCS_SYS.dbo.Dictionary_Type  WHERE TableName='" + field.RelationTableName + "')AND Code='" + old_M[field.FieldName] + "'");
                                if (dic.Count > 0) _changemodel.OldValue = dic[0].Name;
                                dic = new Dictionary_DAL().GetModelList("Type=(SELECT ID FROM MCS_SYS.dbo.Dictionary_Type  WHERE TableName='" + field.RelationTableName + "')AND Code='" + cur_M[field.FieldName] + "'");
                                if (dic.Count > 0) _changemodel.NewValue = dic[0].Name;
                                break;
                            case 2:     //关联实体表
                                _changemodel.OldValue = TreeTableDAL.GetRelationTableDataValue(field.RelationTableName, field.RelationValueField, field.RelationTextField, old_M[field.FieldName]);
                                _changemodel.NewValue = TreeTableDAL.GetRelationTableDataValue(field.RelationTableName, field.RelationValueField, field.RelationTextField, cur_M[field.FieldName]);
                                break;
                        }

                        if (!string.IsNullOrEmpty(cur_M["UpdateStaff"]))
                        {
                            _changemodel.ChangeStaff = int.Parse(cur_M["UpdateStaff"]);
                        }

                        #region	设置参数集
                        SqlParameter[] prams = {
				    SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, _changemodel.TableID),
				    SQLDatabase.MakeInParam("@FieldID", SqlDbType.UniqueIdentifier, 16, _changemodel.FieldID),
				    SQLDatabase.MakeInParam("@InfoID", SqlDbType.Int, 4, _changemodel.InfoID),
				    SQLDatabase.MakeInParam("@OldValue", SqlDbType.VarChar, 500, _changemodel.OldValue),
				    SQLDatabase.MakeInParam("@NewValue", SqlDbType.VarChar, 500, _changemodel.NewValue),
				    SQLDatabase.MakeInParam("@InfoType", SqlDbType.Int, 4, _changemodel.InfoType),
				    SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, _changemodel.ChangeStaff),
				    SQLDatabase.MakeInParam("@ChangeTime", SqlDbType.DateTime, 8, _changemodel.ChangeTime),
				    SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, _changemodel.Remark),
				    SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(_changemodel.ExtPropertys,_changemodel.ModelName))
			                               };
                        #endregion

                        SQLDatabase.RunProc("MCS_SYS.dbo.sp_Info_ChangeHistory_Add", prams);


                    }

                }
            }
            catch (Exception)
            {

                 ;
            }
            return 1;
        }
        /// <summary>
        /// 判断指定ID的记录在数据库是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Exists(int id)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.Int,4,id)				};
            return SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Exists", parameters, SQLDatabase.DefaultTimeout) == 1;
        }

        /// <summary>
        /// 判断指定ID的记录在数据库是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Exists(Guid id)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier,16,id)				};
            return SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Exists", parameters, SQLDatabase.DefaultTimeout) == 1;
        }

        /// <summary>
        /// 根据自定义SQL条件获取Model列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual IList<T> GetModelList(string condition)
        {
            if (condition == null) condition = "";

            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@Condition", SqlDbType.NVarChar,4000,condition)};
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetByCondition", parameters, out dr, SQLDatabase.DefaultTimeout);

            return FillModelList(dr);
        }
        #endregion

        /// <summary>
        /// 将Model里的值赋到列集合中
        /// </summary>
        /// <param name="m"></param>
        protected virtual SqlParameterDictionary GetParamsCollection(T m)
        { return new SqlParameterDictionary(); }

        /// <summary>
        /// 根据IDataReader的当前值来填充Model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected abstract T FillModel(IDataReader dr);

        /// <summary>
        /// 根据IDataReader来填充Model列表集
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public IList<T> FillModelList(IDataReader dr)
        {
            IList<T> list = new List<T>();
            while (dr.Read())
            {
                list.Add(FillModel(dr));
            }
            dr.Close();

            return list;
        }


        #region 扩展属性集操作
        /// <summary>
        /// 分解扩展属性值至集合
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="extfieldvalue"></param>
        /// <returns></returns>
        protected NameValueCollection SpiltExtProperty(string modelname, string extvalue)
        {
            NameValueCollection ExtPropertyCollection = new NameValueCollection();

            UD_ModelFieldsDAL dal = new UD_ModelFieldsDAL();
            IList<UD_ModelFields> extfields = dal.GetExtFieldsList(modelname);
            string[] vaules = extvalue.Split('|');

            for (int i = 0; i < extfields.Count; i++)
            {
                if (i < vaules.Length)
                {
                    ExtPropertyCollection.Add(extfields[i].FieldName, vaules[i]);
                }
                else
                {
                    ExtPropertyCollection.Add(extfields[i].FieldName, "");
                }
            }

            return ExtPropertyCollection;
        }

        /// <summary>
        /// 将扩展值集合组合为字符串
        /// </summary>
        /// <param name="extpropertycollection"></param>
        /// <returns></returns>
        protected string CombineExtProperty(NameValueCollection extpropertycollection, string modelname)
        {
            string str = "";
            UD_ModelFieldsDAL dal = new UD_ModelFieldsDAL();
            IList<UD_ModelFields> extfields = dal.GetExtFieldsList(modelname);

            if (extpropertycollection != null)
            {
                foreach (UD_ModelFields modelfield in extfields)
                {
                    str += extpropertycollection[modelfield.FieldName] + "|";
                }
            }

            return str;
        }
        #endregion
    }
}
