
// ===================================================================
// 文件： Rpt_DataSetDAL.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.RPT;
using MCSFramework.SQLDAL.RPT;
using MCSFramework.Model;

namespace MCSFramework.BLL.RPT
{
    /// <summary>
    ///Rpt_DataSetBLL业务逻辑BLL类
    /// </summary>
    public class Rpt_DataSetBLL : BaseSimpleBLL<Rpt_DataSet>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_DataSetDAL";
        private Rpt_DataSetDAL _dal;

        #region 构造函数
        ///<summary>
        ///Rpt_DataSetBLL
        ///</summary>
        public Rpt_DataSetBLL()
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetDAL)_DAL;
            _m = new Rpt_DataSet();
        }

        public Rpt_DataSetBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetDAL)_DAL;
            FillModel(id);
        }

        public Rpt_DataSetBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Rpt_DataSet> GetModelList(string condition)
        {
            return new Rpt_DataSetBLL()._GetModelList(condition);
        }
        #endregion

        #region 获取指定目录下的数据集
        /// <summary>
        /// 获取指定目录下的数据集
        /// </summary>
        /// <param name="Folder">目录</param>
        /// <param name="Enabled">True仅获取启用的数据源 False仅获取禁用的数据源</param>
        /// <returns></returns>
        public static IList<Rpt_DataSet> GetDataByFolder(int Folder, bool Enabled)
        {
            Rpt_DataSetDAL dal = (Rpt_DataSetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDataByFolder(Folder, Enabled);
        }
        #endregion

        #region	获取数据集包含的字段列表
        public IList<Rpt_DataSetFields> GetFields()
        {
            return Rpt_DataSetFieldsBLL.GetModelList("DataSet='" + _m.ID.ToString() + "'");
        }
        #endregion

        #region	获取数据集包含的参数列表
        public IList<Rpt_DataSetParams> GetParams()
        {
            return Rpt_DataSetParamsBLL.GetModelList("DataSet='" + _m.ID.ToString() + "'");
        }
        #endregion

        #region	获取数据集包含的数据表列表
        public IList<Rpt_DataSetTables> GetTables()
        {
            return Rpt_DataSetTablesBLL.GetModelList("DataSet='" + _m.ID.ToString() + "'");
        }
        #endregion

        #region	获取数据集包含的数据表关系列表
        public IList<Rpt_DataSetTableRelations> GetTableRelations()
        {
            return Rpt_DataSetTableRelationsBLL.GetModelList("DataSet='" + _m.ID.ToString() + "'");
        }
        #endregion

        #region 获取数据集访问SQL
        public string GetDataSetSQL()
        {
            if (_m.CommandType == 1 || _m.CommandType == 2)
                return _m.CommandText;
            else
            {
                Dictionary<string, Rpt_DataSetFields> dicTreeColumnList;
                return GenarateSelectSQL(out dicTreeColumnList);
            }
        }
        #endregion

        private SqlParameter[] MakeParams(Dictionary<string, object> ParamValue)
        {
            IList<Rpt_DataSetParams> paralist = GetParams();
            SqlParameter[] param = new SqlParameter[paralist.Count];

            for (int i = 0; i < paralist.Count; i++)
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = paralist[i].ParamName;
                p.Direction = ParameterDirection.Input;
                p.Value = ParamValue[paralist[i].ParamName];

                switch (paralist[i].DataType)
                {
                    case 1:     //整型(int)
                        p.SqlDbType = SqlDbType.Int;
                        break;
                    case 2:     //小数(decimal)
                        p.SqlDbType = SqlDbType.Decimal;
                        break;
                    case 3:     //字符串(varchar)
                        p.SqlDbType = SqlDbType.VarChar;
                        p.Size = p.Value.ToString().Length;
                        break;
                    case 4:     //日期(datetime)
                        p.SqlDbType = SqlDbType.DateTime;
                        break;
                    case 5:     //GUID(uniqueidentifier)
                        p.SqlDbType = SqlDbType.UniqueIdentifier;
                        break;
                    case 6:     //字符串(nvarchar)
                        p.SqlDbType = SqlDbType.NVarChar;
                        p.Size = p.Value.ToString().Length;
                        break;
                    default:
                        p.SqlDbType = SqlDbType.VarChar;
                        p.Size = p.Value.ToString().Length;
                        break;
                }
                param[i] = p;
            }
            return param;
        }

        private string GenarateSelectSQL(out Dictionary<string, Rpt_DataSetFields> dicTreeColumnList)
        {
            string _sqlstring = "";
            string _selectstr = "SELECT ";
            string _fromstr = "";
            string _selectfromstr = "";           //字段如果是关联时，临时生成的from sql

            dicTreeColumnList = new Dictionary<string, Rpt_DataSetFields>();

            if (_m.CommandType != 3) return "";

            #region create the select string
            IList<Rpt_DataSetFields> _fields = this.GetFields();

            foreach (Rpt_DataSetFields _field in _fields)
            {
                if (_field.IsComputeField == "Y")
                {
                    _selectstr += _field.Expression + " AS " + _field.FieldName + ",";
                }
                else
                {
                    UD_ModelFields _modelfield = new UD_ModelFieldsBLL(_field.FieldID, true).Model;
                    UD_TableList _tablemodel = new UD_TableListBLL(_modelfield.TableID, true).Model;

                    string _fieldfullname = _tablemodel.ModelClassName + "_" + _modelfield.FieldName;

                    #region 判断字段关联类型，决定如果创建SQL
                    switch (_modelfield.RelationType)
                    {
                        case 1://Relation to dic
                            if (_field.DisplayMode == 1)//Bound the id value of the field
                            {
                                if (_modelfield.Flag == "Y")
                                {
                                    _selectstr += _tablemodel.Name + "." + _modelfield.FieldName + " AS " + _fieldfullname + ",";
                                }
                                else//扩展字段
                                {
                                    _selectstr += "MCS_SYS.[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," +
                                        _modelfield.Position.ToString() + ") AS " + _fieldfullname + ",";
                                }
                            }
                            else//Bound the text value of the field
                            {
                                //计算出关联字典表的别名"_vname"
                                string _vname = "V_Dictionary_Data" + "_" + _modelfield.FieldName;
                                if (_modelfield.Flag == "Y")
                                {
                                    _selectstr += _vname + ".Name" + " AS " + _fieldfullname + ",";
                                    _selectfromstr += " left join V_Dictionary_Data AS " + _vname + " ON " + _tablemodel.Name + "." + _modelfield.FieldName + "=" + _vname + ".Code AND " + _vname + ".TableName='" + _modelfield.RelationTableName + "' ";
                                }
                                else//扩展字段
                                {
                                    _selectstr += _vname + ".Name" + " AS " + _fieldfullname + ",";
                                    _selectfromstr += " left join V_Dictionary_Data AS " + _vname + " ON MCS_SYS.[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") =" + _vname + ".Code AND " + _vname + ".TableName='" + _modelfield.RelationTableName + "' ";
                                }
                            }
                            break;
                        case 2://Relation to model table
                            if (_field.TreeLevel > 0 && new UD_TableListBLL(_modelfield.RelationTableName).Model.TreeFlag == "Y")
                            {
                                #region 字段关联到树形结构表，且要显示上层父结点信息
                                _fieldfullname += _field.TreeLevel.ToString();

                                if (_modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                                {
                                    string tmpfieldname = "";
                                    if (_modelfield.Flag == "Y")
                                        tmpfieldname = _tablemodel.Name + "." + _modelfield.FieldName;
                                    else//扩展字段
                                        tmpfieldname = "[MCS_SYS].[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ")";

                                    if (_field.DisplayMode == 1)
                                        _selectstr += "[MCS_SYS].[dbo].[UF_GetSuperOrganizeCityByLevel02](" + tmpfieldname + "," + _field.TreeLevel.ToString() + ")" + " AS " + _fieldfullname + ",";
                                    else
                                        _selectstr += "[MCS_SYS].[dbo].[UF_GetSuperOrganizeCityNameByLevel02](" + tmpfieldname + "," + _field.TreeLevel.ToString() + ")" + " AS " + _fieldfullname + ",";
                                }
                                else if (_modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                                {
                                    string tmpfieldname = "";
                                    if (_modelfield.Flag == "Y")
                                        tmpfieldname = _tablemodel.Name + "." + _modelfield.FieldName;
                                    else//扩展字段
                                        tmpfieldname = "[MCS_SYS].[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ")";

                                    if (_field.DisplayMode == 1)
                                        _selectstr += "[MCS_SYS].[dbo].[UF_GetSuperOfficialCityByLevel02](" + tmpfieldname + "," + _field.TreeLevel.ToString() + ")" + " AS " + _fieldfullname + ",";
                                    else
                                        _selectstr += "[MCS_SYS].[dbo].[UF_GetSuperOfficialCityNameByLevel02](" + tmpfieldname + "," + _field.TreeLevel.ToString() + ")" + " AS " + _fieldfullname + ",";
                                }
                                else
                                {
                                    if (_modelfield.Flag == "Y")
                                        _selectstr += _tablemodel.Name + "." + _modelfield.FieldName + " AS " + _fieldfullname + ",";
                                    else//扩展字段
                                        _selectstr += "MCS_SYS.[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") AS " + _fieldfullname + ",";

                                    dicTreeColumnList.Add(_fieldfullname, _field);
                                }
                                #endregion
                            }
                            else
                            {
                                #region 不需关联到树形结构表
                                if (_field.DisplayMode == 1)//Bound the id value of the field
                                {
                                    if (_modelfield.Flag == "Y")
                                        _selectstr += _tablemodel.Name + "." + _modelfield.FieldName + " AS " + _fieldfullname + ",";
                                    else//扩展字段
                                    {
                                        _selectstr += "MCS_SYS.[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") AS " + _fieldfullname + ",";
                                    }
                                }
                                else//Bound the text value of the field
                                {
                                    #region 计算出关联表的别名"_vname"
                                    string _vname = "";
                                    int pos = _modelfield.RelationTableName.LastIndexOf('.');
                                    if (pos >= 0)
                                        _vname = _modelfield.RelationTableName.Substring(pos + 1) + "_" + _modelfield.FieldName;
                                    else
                                        _vname = _modelfield.RelationTableName + "_" + _modelfield.FieldName;
                                    #endregion

                                    if (_modelfield.Flag == "Y")
                                    {
                                        _selectstr += _vname + "." + _modelfield.RelationTextField + " AS " + _fieldfullname + ",";
                                        _selectfromstr += " left join " + _modelfield.RelationTableName + " AS " + _vname + " ON " + _tablemodel.Name + "." + _modelfield.FieldName + "=" + _vname + "." + _modelfield.RelationValueField;
                                    }
                                    else//扩展字段
                                    {
                                        _selectstr += _vname + "." + _modelfield.RelationTextField + " AS " + _fieldfullname + ",";
                                        _selectfromstr += " left join " + _modelfield.RelationTableName + " AS " + _vname + " ON MCS_SYS.[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") =" + _vname + "." + _modelfield.RelationValueField;
                                    }
                                }
                                #endregion
                            }

                            break;
                        default://No relation
                            if (_modelfield.Flag == "Y")
                                _selectstr += _tablemodel.Name + "." + _modelfield.FieldName + " AS " + _fieldfullname + ",";
                            else
                            {
                                _selectstr += "MCS_SYS.[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") AS " + _fieldfullname + ",";
                            }
                            break;
                    }
                    #endregion
                }
            }
            _selectstr = _selectstr.Substring(0, _selectstr.Length - 1);


            #endregion

            #region create the from string of sql
            IList<Rpt_DataSetTableRelations> _tablerelations = this.GetTableRelations();
            IList<Rpt_DataSetTables> _tables = this.GetTables();
            if (_tables.Count == 1 || _tablerelations.Count == 0)
            {
                _fromstr = " FROM " + new UD_TableListBLL(_tables[0].TableID, true).Model.Name;
            }
            else
            {
                foreach (Rpt_DataSetTableRelations _tablerelation in _tablerelations)
                {
                    string _parenttablename = new UD_TableListBLL(_tablerelation.ParentTableID, true).Model.Name;
                    string _childtablename = new UD_TableListBLL(_tablerelation.ChildTableID, true).Model.Name;

                    UD_ModelFields _parentfield = new UD_ModelFieldsBLL(_tablerelation.ParentFieldID, true).Model;
                    UD_ModelFields _childfield = new UD_ModelFieldsBLL(_tablerelation.ChildFieldID, true).Model;

                    if (_tablerelation.JoinMode == "") _tablerelation.JoinMode = "LEFT JOIN";

                    if (_fromstr == "")
                        _fromstr = " FROM " + _parenttablename + " ";

                    _fromstr += " " + _tablerelation.JoinMode + " " + _childtablename + " ON ";

                    if (_parentfield.Flag == "Y" && _childfield.Flag == "Y")
                    {
                        _fromstr += _parenttablename + ".[" + _parentfield.FieldName + "] = " + _childtablename + ".[" + _childfield.FieldName + "] ";
                    }
                    else
                    {
                        //关联字段中，有一个字段为扩展字段
                        if (_parentfield.Flag == "Y")
                            _fromstr += "CONVERT(VarChar," + _parenttablename + "." + _parentfield.FieldName + ")";
                        else
                            _fromstr += "MCS_SYS.[dbo].[UF_Spilt](" + _parenttablename + ".ExtPropertys,'|'," + _parentfield.Position.ToString() + ")";

                        _fromstr += " = ";

                        if (_childfield.Flag == "Y")
                            _fromstr += "CONVERT(VarChar," + _childtablename + "." + _childfield.FieldName + ")";
                        else
                            _fromstr += "MCS_SYS.[dbo].[UF_Spilt](" + _childtablename + ".ExtPropertys,'|'," + _childfield.Position.ToString() + ")";

                    }
                   
                    if (_tablerelation.RelationCondition != "")
                    {
                        _fromstr += " AND " + _tablerelation.RelationCondition + " ";
                    }
                }
            }

            if (_selectfromstr != "")
                _fromstr += _selectfromstr;

            _sqlstring = _selectstr + _fromstr;
            #endregion

            if (_m.ConditionSQL != "")
                _sqlstring += " WHERE " + _m.ConditionSQL;

            if (_m.OrderString != "")
                _sqlstring += " ORDER BY " + _m.OrderString;

            return _sqlstring;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="ParamValue"></param>
        /// <param name="CacheTime"></param>
        /// <returns></returns>
        public DataTable GetData(Dictionary<string, object> ParamValue, bool LoadFromCache, out DateTime CacheTime)
        {
            DataTable dt = null;
            CacheTime = DateTime.Now;

            if (LoadFromCache) dt = Rpt_DataSet_DataCacheBLL.LoadDataSetCache(_m.ID, ParamValue, out CacheTime);

            if (dt == null || dt.Rows.Count == 0)
            {
                SqlParameter[] param = MakeParams(ParamValue);

                Rpt_DataSource source = new Rpt_DataSourceBLL(_m.DataSource).Model;
                string conn = "";

                if (source.ConnectionString != "")
                    conn = MCSFramework.Common.DataEncrypter.DecryptData(source.ConnectionString);

                switch (_m.CommandType)
                {
                    case 1:
                        dt = _dal.GetDataFromSQL(conn, _m.CommandText, param);
                        break;
                    case 2:
                        dt = _dal.GetDataFromStoreProcedure(conn, _m.CommandText, param);
                        break;
                    case 3:
                        Dictionary<string, Rpt_DataSetFields> dicTreeColumnList;
                        dt = _dal.GetDataFromSQL(conn, GenarateSelectSQL(out dicTreeColumnList), param);

                        #region 处理树形字段，显示指定层级的内容
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            DataColumn column = dt.Columns[i];

                            if (!dicTreeColumnList.ContainsKey(column.ColumnName)) continue;

                            DataColumn newcolumn = dt.Columns.Add("_" + column.ColumnName, Type.GetType("System.String"));

                            Rpt_DataSetFields field = dicTreeColumnList[column.ColumnName];
                            Dictionary<int, string> cachevalue = new Dictionary<int, string>();

                            foreach (DataRow row in dt.Rows)
                            {
                                int value = 0;
                                if (int.TryParse(row[column].ToString(), out value))
                                {
                                    if (cachevalue.ContainsKey(value))
                                    {
                                        row[newcolumn] = cachevalue[value];
                                    }
                                    else
                                    {
                                        if (field.TreeLevel == 100)
                                        {
                                            //显示全路径
                                            row[newcolumn] = TreeTableBLL.GetFullPathName(new UD_ModelFieldsBLL(field.FieldID, true).Model.RelationTableName, value);
                                        }
                                        else
                                        {
                                            //显示父级
                                            if (field.DisplayMode == 1)
                                                row[newcolumn] = TreeTableBLL.GetSuperIDByLevel(new UD_ModelFieldsBLL(field.FieldID, true).Model.RelationTableName, value, field.TreeLevel).ToString();
                                            else
                                                row[newcolumn] = TreeTableBLL.GetSuperNameByLevel(new UD_ModelFieldsBLL(field.FieldID, true).Model.RelationTableName, value, field.TreeLevel);
                                        }
                                        cachevalue.Add(value, row[newcolumn].ToString());
                                    }
                                }
                            }
                            int order = column.Ordinal;
                            dt.Columns.Remove(column);
                            newcolumn.SetOrdinal(order);
                            newcolumn.ColumnName = newcolumn.ColumnName.Substring(1);
                        }
                        #endregion

                        break;
                    default:
                        break;
                }

                if (_m.CommandType == 1 || _m.CommandType == 2)
                {
                    #region 加入计算列字段
                    IList<Rpt_DataSetFields> computefields = this.GetFields().Where(p => p.IsComputeField == "Y").ToList();
                    foreach (Rpt_DataSetFields field in computefields)
                    {
                        Type ColumnType;
                        switch (field.DataType)
                        {
                            case 1:     //整型(int)
                                ColumnType = Type.GetType("System.Int32");
                                break;
                            case 2:     //小数(decimal)
                                ColumnType = Type.GetType("System.Decimal");
                                break;
                            case 3:     //字符串(varchar)
                            case 6:     //字符串(nvarchar)
                                ColumnType = Type.GetType("System.String");
                                break;
                            case 4:     //日期(datetime)
                                ColumnType = Type.GetType("System.DateTime");
                                break;
                            case 5:     //GUID(uniqueidentifier)
                                ColumnType = Type.GetType("System.Guid");
                                break;
                            default:
                                ColumnType = Type.GetType("System.Decimal");
                                break;
                        }
                        try
                        {
                            dt.Columns.Add(field.FieldName, ColumnType, field.Expression);
                        }
                        catch { }
                    }
                    #endregion
                }

                if (dt != null && dt.Rows.Count < 200000)    //只对20万行以下的数据表进行缓存
                {
                    try
                    {
                        Rpt_DataSet_DataCacheBLL.SaveDataSetCache(_m.ID, ParamValue, dt);
                        CacheTime = DateTime.Now;
                    }
                    catch { System.GC.Collect(); }
                }
            }

            return dt;
        }

        /// <summary>
        /// 清除该数据集对应的所有缓存
        /// </summary>
        public void ClearCache()
        {
            Rpt_DataSet_DataCacheBLL.ClearDataSetCache(_m.ID);
        }
    }
}