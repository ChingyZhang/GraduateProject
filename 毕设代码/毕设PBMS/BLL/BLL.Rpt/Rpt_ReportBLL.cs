
// ===================================================================
// 文件： Rpt_ReportDAL.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model.RPT;
using MCSFramework.SQLDAL.RPT;
using System.Web.Caching;

namespace MCSFramework.BLL.RPT
{
    /// <summary>
    ///Rpt_ReportBLL业务逻辑BLL类
    /// </summary>
    public class Rpt_ReportBLL : BaseSimpleBLL<Rpt_Report>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_ReportDAL";
        private Rpt_ReportDAL _dal;

        private DateTime _dataCacheTime = DateTime.Now;
        /// <summary>
        /// 数据集缓存时间
        /// </summary>
        public DateTime DataCacheTime
        {
            get { return _dataCacheTime; }
        }

        private DataTable _reportdata = null;

        #region 构造函数
        ///<summary>
        ///Rpt_ReportBLL
        ///</summary>
        public Rpt_ReportBLL()
            : base(DALClassName)
        {
            _dal = (Rpt_ReportDAL)_DAL;
            _m = new Rpt_Report();
        }

        public Rpt_ReportBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportDAL)_DAL;
            FillModel(id);
        }

        public Rpt_ReportBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Rpt_Report> GetModelList(string condition)
        {
            return new Rpt_ReportBLL()._GetModelList(condition);
        }
        #endregion

        #region	获取报表的表格字段列
        public IList<Rpt_ReportGridColumns> GetGridColumns()
        {
            return Rpt_ReportGridColumnsBLL.GetModelList("Report='" + _m.ID.ToString() + "'");
        }
        #endregion

        #region	获取报表的列组
        public IList<Rpt_ReportColumnGroups> GetColumnGroups()
        {
            return Rpt_ReportColumnGroupsBLL.GetModelList("Report='" + _m.ID.ToString() + "'");
        }
        #endregion

        #region	获取报表的行组
        public IList<Rpt_ReportRowGroups> GetRowGroups()
        {
            return Rpt_ReportRowGroupsBLL.GetModelList("Report='" + _m.ID.ToString() + "'");
        }
        #endregion

        #region	获取报表的数值组
        public IList<Rpt_ReportValueGroups> GetValueGroups()
        {
            return Rpt_ReportValueGroupsBLL.GetModelList("Report='" + _m.ID.ToString() + "'");
        }
        #endregion

        #region	获取报表的图表
        public IList<Rpt_ReportCharts> GetCharts()
        {
            return Rpt_ReportChartsBLL.GetModelList("Report='" + _m.ID.ToString() + "'");
        }
        #endregion

        public bool LoadData(Dictionary<string, object> ParamValues, bool LoadFromCache)
        {
            DataTable dt = null;

            Rpt_DataSetBLL datasetbll = new Rpt_DataSetBLL(_m.DataSet);
            dt = datasetbll.GetData(ParamValues, LoadFromCache, out _dataCacheTime);

            if (dt == null || dt.Rows.Count == 0) return false;

            try
            {
                #region 普通列表
                if (_m.ReportType == 1)
                {
                    //将字段列名替换为报表定义的显示名称
                    IList<Rpt_ReportGridColumns> GridColumnlists = GetGridColumns();
                    int ordinal = 0;
                    foreach (Rpt_ReportGridColumns column in GridColumnlists)
                    {
                        string fieldname = GetFieldName(column.DataSetField);
                        if (dt.Columns.Contains(fieldname))
                        {
                            dt.Columns[fieldname].SetOrdinal(ordinal++);
                            dt.Columns[fieldname].ColumnName = column.DisplayName;
                        }

                        #region 替换计算列表达式里的列名
                        foreach (DataColumn c in dt.Columns)
                        {
                            if (!string.IsNullOrEmpty(c.Expression) && c.Expression.Contains(fieldname))
                            {
                                c.Expression = c.Expression.Replace(fieldname, column.DisplayName);
                            }
                        }
                        #endregion
                    }

                    //去除报表定义中不包括的字段
                    for (int i = dt.Columns.Count - 1; i >= 0; i--)
                    {
                        if (GridColumnlists.FirstOrDefault(p => p.DisplayName == dt.Columns[i].ColumnName) == null)
                        {
                            dt.Columns.RemoveAt(i);
                        }
                    }

                    _reportdata = dt;
                    return true;
                }
                #endregion

                #region 矩阵表(透视表)
                if (_m.ReportType == 2)
                {
                    IList<Rpt_ReportRowGroups> RowGroups = GetRowGroups();
                    IList<Rpt_ReportColumnGroups> ColumnGroups = GetColumnGroups();
                    IList<Rpt_ReportValueGroups> ValueGroups = GetValueGroups();

                    _reportdata = RptMatrixTable(dt, RowGroups, ColumnGroups, ValueGroups);

                    return true;
                }
                #endregion
            }
            catch { }
            return false;
        }

        public DataTable GetReportData()
        {
            if (_reportdata == null) return null;
            return _reportdata.Copy();
        }
        public DataTable GetReportDataWithSummary()
        {
            if (_reportdata == null) return null;

            DataTable dt = _reportdata.Copy();

            if (_m.ReportType == 1)
            {
                #region 根据行组，加入普通列表的行小计
                IList<Rpt_ReportGridColumns> GridColumnlists = GetGridColumns();
                IList<Rpt_ReportRowGroups> RowGroups = GetRowGroups();
                IList<Rpt_ReportGridColumns> gridcolumnlists_addsummary = GridColumnlists.Where(p => p.AddSummary.ToUpper() == "Y").ToList();
                if (gridcolumnlists_addsummary.Count > 0)        //有需要求合计的列
                {
                    //将要加合计的列，转换为字符串数组，以便加行小计
                    string[] valuecolumns_addsummary = new string[gridcolumnlists_addsummary.Count];
                    for (int i = 0; i < gridcolumnlists_addsummary.Count; i++)
                    {
                        valuecolumns_addsummary[i] = gridcolumnlists_addsummary[i].DisplayName;
                    }

                    #region 仅加入在列表字段中的行组(以防止设定的行组不在报表输入的字段中)

                    IList<Rpt_ReportRowGroups> rowgrouplists = new List<Rpt_ReportRowGroups>();
                    foreach (Rpt_ReportRowGroups rowgroup in RowGroups)
                    {
                        if (GridColumnlists.FirstOrDefault(p => p.DataSetField == rowgroup.DataSetField) != null)
                            rowgrouplists.Add(rowgroup);
                    }
                    #endregion

                    if (rowgrouplists.Count == 0)
                    {
                        #region 无分行组
                        Hashtable _ht = new Hashtable();
                        CalSum(dt, valuecolumns_addsummary, ref _ht);
                        AddTotalRow(ref dt, valuecolumns_addsummary, _ht);
                        #endregion
                    }
                    else
                    {
                        #region 有行组

                        #region 对数据表按行组排序
                        string sort = "";
                        for (int i = 0; i < rowgrouplists.Count; i++)
                        {
                            sort += rowgrouplists[i].DisplayName + ",";
                        }
                        sort = sort.Substring(0, sort.Length - 1);
                        dt.DefaultView.Sort = sort;
                        #endregion

                        TableAddRowSubTotal(dt, rowgrouplists, valuecolumns_addsummary, true);
                        #endregion
                    }
                }
                #endregion
            }

            if (_m.ReportType == 2)
            {
                IList<Rpt_ReportRowGroups> RowGroups = GetRowGroups();
                IList<Rpt_ReportColumnGroups> ColumnGroups = GetColumnGroups();
                IList<Rpt_ReportValueGroups> ValueGroups = GetValueGroups();

                #region 加入列小计
                ColumnSummaryTotal(dt, RowGroups, ColumnGroups, ValueGroups, _m.AddColumnTotal == "Y");
                #endregion

                #region 求行小计
                TableAddRowSubTotal_Matrix(dt, RowGroups, _m.AddRowTotal == "Y");
                #endregion
            }

            return dt;
        }

        #region 将DataTable矩阵化
        /// <summary>
        /// 将DataTable矩阵化
        /// </summary>
        /// <param name="SourceTable">源表</param>
        /// <param name="RowGroups">行组,矩阵化后为目的表的主键</param>
        /// <param name="ColumnGroups">列组,矩阵化后根据列组数据自动生成列</param>
        /// <param name="ValueGroups">值组,矩阵化后自动将值累加到数据区中</param>
        /// <returns></returns>
        private DataTable RptMatrixTable(DataTable SourceTable, IList<Rpt_ReportRowGroups> RowGroups,
            IList<Rpt_ReportColumnGroups> ColumnGroups, IList<Rpt_ReportValueGroups> ValueGroups)
        {
            if (SourceTable.Rows.Count == 0) return SourceTable;
            DataTable DestTable = new DataTable();

            #region 给目的表加行主键
            DataColumn[] pkc = new DataColumn[RowGroups.Count];
            string sort = "";
            for (int i = 0; i < RowGroups.Count; i++)
            {
                string columnname = GetFieldName(RowGroups[i].DataSetField);
                DataColumn dc = new DataColumn(RowGroups[i].DisplayName, SourceTable.Columns[columnname].DataType);
                DestTable.Columns.Add(dc);
                pkc[i] = dc;
                sort += columnname + ",";
            }
            DestTable.PrimaryKey = pkc;
            sort = sort.Substring(0, sort.Length - 1);
            SourceTable.DefaultView.Sort = sort;

            #endregion

            #region 动态扩展目的表的列
            //获取列组的唯一值,根据数据决定有哪些列
            ArrayList list = new ArrayList();
            if (ColumnGroups.Count == 0)
            {
                list.Add("");
            }
            else
            {
                foreach (DataRow dr in SourceTable.Rows)
                {
                    string v = "";
                    foreach (Rpt_ReportColumnGroups dc in ColumnGroups)
                    {
                        string columnname = GetFieldName(dc.DataSetField);
                        v += (dr[columnname].ToString().Trim() == string.Empty ? "NULL" : dr[columnname].ToString().Trim()) + "→";
                    }
                    if (ColumnGroups.Count > 0) v = v.Substring(0, v.Length - 1);
                    if (!list.Contains(v)) list.Add(v);

                }
                if (_m["IsSort"] != "2")
                {
                    list.Sort();        //列排序
                }
            }

            //扩展目的表的列
            for (int i = 0; i < list.Count; i++)
            {
                foreach (Rpt_ReportValueGroups dv in ValueGroups)
                {
                    string columnname = "";

                    if (ColumnGroups.Count == 0)
                    {
                        columnname = dv.DisplayName;
                    }
                    else
                    {
                        if (ValueGroups.Count == 1)
                            columnname = list[i].ToString();
                        else
                            columnname = list[i] + "→" + dv.DisplayName;
                    }

                    DestTable.Columns.Add(columnname, SourceTable.Columns[GetFieldName(dv.DataSetField)].DataType).DefaultValue = 0;
                }
            }
            #endregion

            #region 增加数据
            Hashtable hs_prerowvalue = new Hashtable();
            foreach (DataRowView dr in SourceTable.DefaultView)
            {
                //判断是否已存在相同键值的行
                object[] pk = new object[RowGroups.Count];
                for (int i = 0; i < RowGroups.Count; i++)
                {
                    #region 主键为空行时处理
                    if (dr[GetFieldName(RowGroups[i].DataSetField)].GetType().FullName == "System.DBNull")
                    {
                        switch (new Rpt_DataSetFieldsBLL(RowGroups[i].DataSetField).Model.DataType)
                        {
                            case 1:
                            case 2:
                            case 7:
                                dr[GetFieldName(RowGroups[i].DataSetField)] = 0;
                                break;
                            case 3:
                            case 6:
                            case 8:
                                dr[GetFieldName(RowGroups[i].DataSetField)] = "";
                                break;
                            case 4:
                                dr[GetFieldName(RowGroups[i].DataSetField)] = new DateTime(1900, 1, 1);
                                break;
                            case 5:
                                dr[GetFieldName(RowGroups[i].DataSetField)] = Guid.Empty;
                                break;
                            default:
                                dr[GetFieldName(RowGroups[i].DataSetField)] = "";
                                break;
                        }
                    }
                    #endregion

                    pk[i] = dr[GetFieldName(RowGroups[i].DataSetField)];

                }
                if (!DestTable.Rows.Contains(pk))
                {
                    #region 新增该数据行
                    DataRow newrow = DestTable.NewRow();
                    foreach (Rpt_ReportRowGroups dc in RowGroups)
                    {
                        newrow[dc.DisplayName] = dr[GetFieldName(dc.DataSetField)];


                    }

                    string columnname_pre = "", columnname_fullname = "";
                    if (ColumnGroups.Count > 0)
                    {
                        foreach (Rpt_ReportColumnGroups dc in ColumnGroups)
                        {
                            columnname_pre += (dr[GetFieldName(dc.DataSetField)].ToString().Trim() == string.Empty ? "NULL" : dr[GetFieldName(dc.DataSetField)].ToString().Trim()) + "→";
                        }
                        columnname_pre = columnname_pre.Substring(0, columnname_pre.Length - 1);
                    }


                    foreach (Rpt_ReportValueGroups dv in ValueGroups)
                    {
                        if (ColumnGroups.Count == 0)
                            columnname_fullname = dv.DisplayName;
                        else
                        {
                            if (ValueGroups.Count == 1)
                                columnname_fullname = columnname_pre;
                            else
                                columnname_fullname = columnname_pre + "→" + dv.DisplayName;
                        }
                        decimal v = 0;
                        try
                        {
                            if (dr[GetFieldName(dv.DataSetField)].ToString() != "")
                            {
                                if (dv.StatisticMode == 2)  //计数模式
                                    v = 1;
                                else
                                    v = decimal.Parse(dr[GetFieldName(dv.DataSetField)].ToString());
                            }
                        }
                        catch { }
                        newrow[columnname_fullname] = v;
                    }
                    DestTable.Rows.Add(newrow);
                    #endregion
                }
                else
                {
                    //存在，累加行
                    DataRow row = DestTable.Rows.Find(pk);

                    string columnname_pre = "", columnname_fullname = "";
                    if (ColumnGroups.Count > 0)
                    {
                        foreach (Rpt_ReportColumnGroups dc in ColumnGroups)
                        {
                            columnname_pre += (dr[GetFieldName(dc.DataSetField)].ToString().Trim() == string.Empty ? "NULL" : dr[GetFieldName(dc.DataSetField)].ToString().Trim()) + "→";
                        }
                        columnname_pre = columnname_pre.Substring(0, columnname_pre.Length - 1);
                    }


                    foreach (Rpt_ReportValueGroups dv in ValueGroups)
                    {
                        if (ColumnGroups.Count == 0)
                            columnname_fullname = dv.DisplayName;
                        else
                        {
                            if (ValueGroups.Count == 1)
                                columnname_fullname = columnname_pre;
                            else
                                columnname_fullname = columnname_pre + "→" + dv.DisplayName;
                        }
                        decimal v = 0;

                        try
                        {
                            if (dr[GetFieldName(dv.DataSetField)].ToString() != "")
                            {
                                v = decimal.Parse(dr[GetFieldName(dv.DataSetField)].ToString());

                                switch (dv.StatisticMode)
                                {
                                    case 1:     //求和
                                        if (row[columnname_fullname].ToString() != "")
                                            row[columnname_fullname] = decimal.Parse(row[columnname_fullname].ToString()) + v;
                                        else
                                            row[columnname_fullname] = v;
                                        break;
                                    case 2:     //计数
                                        if (row[columnname_fullname].ToString() != "")
                                            row[columnname_fullname] = int.Parse(row[columnname_fullname].ToString()) + 1;
                                        else
                                            row[columnname_fullname] = 1;
                                        break;
                                    case 3:     //最大值
                                        if (row[columnname_fullname].ToString() != "")
                                            row[columnname_fullname] = (decimal.Parse(row[columnname_fullname].ToString()) > v ? decimal.Parse(row[columnname_fullname].ToString()) : v);
                                        else
                                            row[columnname_fullname] = v;
                                        break;
                                    case 4:     //最小值
                                        if (row[columnname_fullname].ToString() != "")
                                            row[columnname_fullname] = (decimal.Parse(row[columnname_fullname].ToString()) < v ? decimal.Parse(row[columnname_fullname].ToString()) : v);
                                        else
                                            row[columnname_fullname] = v;
                                        break;
                                    default:
                                        break;
                                }

                            }
                        }
                        catch { }


                    }
                }
            }
            #endregion

            #region 去除表行主键
            DestTable.PrimaryKey = null;
            foreach (DataColumn c in pkc)
            {
                c.AllowDBNull = true;
            }
            #endregion

            return DestTable;
        }

        #region 列小计相关
        /// <summary>
        /// 对指定列做小计
        /// </summary>
        /// <param name="SourceTable">要进行小计的数据表</param>
        /// <param name="indexgroup">在矩阵化中列组的索引</param>
        /// <returns>返回计算后的表格</returns>
        private void ColumnSummaryTotal(DataTable Table, IList<Rpt_ReportRowGroups> RowGroup, IList<Rpt_ReportColumnGroups> ColumnGroup, IList<Rpt_ReportValueGroups> ValueGroup, bool AddColumnTotal)
        {
            if (ColumnGroup.Count > 0) ColumnGroup[ColumnGroup.Count - 1].AddSummary = "N";

            string[] ColumnNames = new string[Table.Columns.Count];
            for (int i = 0; i < Table.Columns.Count; i++)
            {
                ColumnNames[i] = Table.Columns[i].ColumnName;
            }

            #region 加入分组列小计
            int index = 0;
            foreach (Rpt_ReportColumnGroups column in ColumnGroup)
            {
                index++;
                if (column.AddSummary.ToUpper() != "Y") continue;

                ArrayList al_colname = new ArrayList();
                string parentindexname = "";
                foreach (string columnname in ColumnNames)
                {
                    if (!columnname.Contains("→") || columnname.Contains("合计→"))
                        continue;
                    if (parentindexname == "")
                    {
                        parentindexname = columnname.Split(new char[] { '→' })[index - 1];
                        al_colname.Add(columnname);
                    }
                    else
                    {
                        if (parentindexname == columnname.Split(new char[] { '→' })[index - 1])
                        {
                            al_colname.Add(columnname);
                        }
                        else
                        {
                            InsertSubTotalColumn(Table, al_colname, index, ValueGroup);
                            parentindexname = columnname.Split(new char[] { '→' })[index - 1];
                            al_colname.Clear();
                            al_colname.Add(columnname);
                        }
                    }
                }
                InsertSubTotalColumn(Table, al_colname, index, ValueGroup);
            }
            #endregion

            #region 加入列总计
            if (AddColumnTotal && ColumnGroup.Count > 0)
            {
                foreach (Rpt_ReportValueGroups dv in ValueGroup)
                {
                    string expression = "";
                    Type columntype = null;

                    if (ColumnGroup.Count == 1)
                    {
                        foreach (string columnname in ColumnNames)
                        {
                            if (RowGroup.FirstOrDefault(p => p.DisplayName == columnname) != null) continue;

                            if (ValueGroup.Count == 1 || columnname.EndsWith(dv.DisplayName))
                            {
                                expression += "[" + columnname + "] +";
                                if (columntype == null) columntype = Table.Columns[columnname].DataType;
                            }
                        }
                    }
                    else
                    {
                        foreach (string columnname in ColumnNames)
                        {
                            if (!columnname.Contains("→")) continue;

                            if (ValueGroup.Count == 1 || columnname.EndsWith(dv.DisplayName))
                            {
                                expression += "[" + columnname + "] +";
                                if (columntype == null) columntype = Table.Columns[columnname].DataType;
                            }
                        }
                    }
                    if (expression == "") continue;

                    expression = expression.Substring(0, expression.Length - 1);
                    if (ValueGroup.Count == 1)
                        Table.Columns.Add("合计", columntype, expression);
                    else
                        Table.Columns.Add("合计→" + dv.DisplayName, columntype, expression);
                }
            }
            #endregion
            return;
        }

        /// <summary>
        /// 在指定的位置插入列小计
        /// </summary>
        private void InsertSubTotalColumn(DataTable dt, ArrayList al_colname, int index, IList<Rpt_ReportValueGroups> ValueGroup)
        {
            if (ValueGroup.Count == 1)
            {
                string addcolumnname = "";
                string[] colname = al_colname[0].ToString().Split(new char[] { '→' });
                #region 获取小计列名
                for (int j = 0; j < colname.Length; j++)
                {
                    if (j < index)
                        addcolumnname += colname[j] + "→";
                    else if (j == index)
                        addcolumnname += "小计→";
                    else
                        addcolumnname += "→";
                }

                addcolumnname = addcolumnname.Substring(0, addcolumnname.Length - 1);
                DataColumn addcolumn = new DataColumn(addcolumnname, typeof(string));
                #endregion

                dt.Columns.Add(addcolumn);

                #region 求插入列的位置
                int position = dt.Columns[al_colname[al_colname.Count - 1].ToString()].Ordinal;
                addcolumn.SetOrdinal(position + 1);
                #endregion

                #region 求小计值
                foreach (DataRow dr in dt.Rows)
                {
                    decimal totalvalue = 0;
                    for (int i = 0; i < al_colname.Count; i++)
                    {
                        totalvalue += decimal.Parse(dr[al_colname[i].ToString()].ToString());
                    }
                    dr[addcolumnname] = totalvalue.ToString();
                }
                #endregion
            }
            else
            {

                for (int i = ValueGroup.Count - 1; i >= 0; i--)
                {
                    string addcolumnname = "";
                    string[] colname = al_colname[0].ToString().Split(new char[] { '→' });

                    #region 获取小计列名
                    for (int j = 0; j < colname.Length; j++)
                    {
                        if (j < index)
                            addcolumnname += colname[j] + "→";
                        else if (j == index)
                            addcolumnname += "小计→";
                        else if (j == colname.Length - 1)
                            addcolumnname += ValueGroup[i].DisplayName + "→";
                        else
                            addcolumnname += "→";
                    }
                    addcolumnname = addcolumnname.Substring(0, addcolumnname.Length - 1);
                    DataColumn addcolumn = new DataColumn(addcolumnname, typeof(string));
                    #endregion

                    dt.Columns.Add(addcolumn);

                    #region 求插入列的位置
                    int position = dt.Columns[al_colname[al_colname.Count - 1].ToString()].Ordinal;
                    addcolumn.SetOrdinal(position + 1);
                    #endregion

                    #region 求小计值
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal totalvalue = 0;
                        for (int k = 0; k < al_colname.Count; k++)
                        {
                            string[] _s = al_colname[k].ToString().Split(new char[] { '→' });
                            if (_s[_s.Length - 1] == ValueGroup[i].DisplayName)
                            {
                                totalvalue += decimal.Parse(dr[al_colname[k].ToString()].ToString());
                            }
                        }
                        dr[addcolumnname] = totalvalue.ToString();
                    }
                    #endregion

                }
            }
        }
        #endregion

        #region 行小计相关
        /// <summary>
        /// 在矩阵表DataTable中增加小计行
        /// </summary>
        /// <param name="DestTable"></param>
        /// <param name="RowGroups"></param>
        /// <param name="AddTotalSummaryRow"></param>
        private void TableAddRowSubTotal_Matrix(DataTable DestTable, IList<Rpt_ReportRowGroups> RowGroups, bool AddTotalSummaryRow)
        {
            string[] valuecolumns = new string[DestTable.Columns.Count - RowGroups.Count];
            for (int i = 0; i < DestTable.Columns.Count - RowGroups.Count; i++)
            {
                valuecolumns[i] = DestTable.Columns[i + RowGroups.Count].ColumnName;
            }
            if (RowGroups.Count > 0) RowGroups[RowGroups.Count - 1].AddSummary = "N";
            TableAddRowSubTotal(DestTable, RowGroups, valuecolumns, AddTotalSummaryRow);
        }

        /// <summary>
        /// 在DataTable中增加小计行
        /// </summary>
        /// <param name="SourceDT">源DataTable</param>
        /// <param name="RowColumns">待小计的列（名称）数组</param>
        /// <param name="Values">待小计的值列（名称）数组</param>
        /// <param name="CalSumValue">是否求行总计</param>
        private void TableAddRowSubTotal(DataTable SourceDT, IList<Rpt_ReportRowGroups> RowGroups, string[] Values, bool AddTotalSummaryRow)
        {
            Hashtable _ht = new Hashtable();
            if (AddTotalSummaryRow) CalSum(SourceDT, Values, ref _ht);

            IList<Rpt_ReportRowGroups> RowGroup_AddTotal = RowGroups.Where(p => p.AddSummary.ToUpper() == "Y").ToList();
            if (RowGroup_AddTotal.Count > 0 && Values.Length > 0 && SourceDT != null && SourceDT.Rows.Count > 0)
            {
                //每个待小计的列
                foreach (Rpt_ReportRowGroups column in RowGroup_AddTotal)
                {
                    //当前列索引
                    int columnindex = 0;
                    while (columnindex < SourceDT.Columns.Count)
                    {
                        if (SourceDT.Columns[columnindex].ColumnName == column.DisplayName)
                            break;
                        else
                            columnindex++;
                    }

                    if (columnindex >= SourceDT.Columns.Count) continue;

                    int BeginPosition = -1;
                    int EndPosition = -1;

                    //遍历每个数据行
                    for (int i = 1; i < SourceDT.Rows.Count; i++)
                    {
                        //如果行和上一行的值不匹配
                        if (!IsSame(SourceDT, columnindex, i))
                        {
                            BeginPosition = GetBeginPosition(SourceDT, columnindex, i);
                            EndPosition = i - 1;

                            AddNewRow(ref SourceDT, columnindex, BeginPosition, EndPosition, Values);

                        }
                        else if (i == SourceDT.Rows.Count - 1 && !RowContainSummary(SourceDT, columnindex, i))
                        {
                            BeginPosition = GetBeginPosition(SourceDT, columnindex, i);
                            EndPosition = i;
                            AddNewRow(ref SourceDT, columnindex, BeginPosition, EndPosition, Values);
                            break;
                        }
                        else
                            continue;
                    }
                }
            }
            //如果要添加总计行
            if (AddTotalSummaryRow)
            {
                AddTotalRow(ref SourceDT, Values, _ht);
            }
        }

        /// <summary>
        /// 在最后添加总计行
        /// </summary>
        /// <param name="SourceDT"></param>
        /// <param name="cols"></param>
        /// <param name="values"></param>
        /// <param name="_ht"></param>
        private static void AddTotalRow(ref DataTable SourceDT, string[] values, Hashtable _ht)
        {
            DataRow dr_sum = SourceDT.NewRow();
            foreach (DataColumn column in SourceDT.Columns)
            {
                if (column.DataType.FullName == "System.String")
                {
                    dr_sum[column] = "总计";
                    break;
                }
            }

            foreach (string value in values)
            {
                dr_sum[value] = _ht[value];
            }
            SourceDT.Rows.Add(dr_sum);
        }

        /// <summary>
        /// 判断某行是否和上一行相同
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ColumnIndex"></param>
        /// <param name="CurrentInex"></param>
        /// <returns></returns>
        private bool IsSame(DataTable dt, int ColumnIndex, int CurrentInex)
        {
            string Flag = "Y";
            for (int i = 0; i <= ColumnIndex; i++)
            {
                if (!RowContainSummary(dt, ColumnIndex, CurrentInex - 1) && (dt.Rows[CurrentInex][i].ToString() != dt.Rows[CurrentInex - 1][i].ToString()))
                {
                    Flag = "N";
                    break;
                }
            }
            return Flag == "Y";
        }

        /// <summary>
        /// 获取第一次匹配特定行的位置
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ColumnIndex"></param>
        /// <param name="CurrentInex"></param>
        /// <returns></returns>
        private int GetBeginPosition(DataTable dt, int ColumnIndex, int CurrentInex)
        {
            int BeginPosition = 0;
            for (int i = CurrentInex - 1; i >= 0; i--)
            {
                if (RowContainSummary(dt, ColumnIndex, i))
                {
                    BeginPosition = i + 1;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return BeginPosition;
        }

        /// <summary>
        /// 添加一个小计行
        /// </summary>
        /// <param name="SourceDT"></param>
        /// <param name="ColumnIndex"></param>
        /// <param name="BeginPosition"></param>
        /// <param name="EndPosition"></param>
        /// <param name="Values"></param>
        private void AddNewRow(ref DataTable SourceDT, int ColumnIndex, int BeginPosition, int EndPosition, string[] Values)
        {
            SourceDT.BeginLoadData();
            DataRow dr_newrow = SourceDT.NewRow();
            //插入该列前面的列值
            for (int k = 0; k <= ColumnIndex; k++)
            {
                dr_newrow[k] = SourceDT.Rows[EndPosition][k];
            }
            dr_newrow[ColumnIndex + 1] = "小计";

            //插入该列后面列值
            for (int k = ColumnIndex + 2; k < SourceDT.Columns.Count; k++)
            {
                string flag_isvalue = "N";
                foreach (string s in Values)
                {
                    if (s == SourceDT.Columns[k].ColumnName)
                    {
                        flag_isvalue = "Y";
                        break;
                    }
                }
                if (flag_isvalue == "Y")//为小计列
                {
                    decimal sumvalue = 0;
                    for (int m = BeginPosition; m <= EndPosition; m++)
                    {
                        try
                        {
                            sumvalue += decimal.Parse(SourceDT.Rows[m][SourceDT.Columns[k].ColumnName].ToString());
                        }
                        catch { continue; }
                    }
                    dr_newrow[SourceDT.Columns[k].ColumnName] = sumvalue;
                }
            }
            SourceDT.Rows.InsertAt(dr_newrow, EndPosition + 1);
        }

        /// <summary>
        /// 判断某行是否为小计产生的
        /// </summary>
        /// <param name="SourceDT"></param>
        /// <param name="ColumnIndex"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        private bool RowContainSummary(DataTable SourceDT, int ColumnIndex, int RowIndex)
        {
            string Flag = "N";
            for (int i = 0; i <= ColumnIndex + 1; i++)
            {
                if (SourceDT.Rows[RowIndex][i].ToString().IndexOf("计") > -1)
                {
                    Flag = "Y";
                    break;
                }
            }
            return Flag == "Y";

        }

        /// <summary>
        ///处理前获取总计 
        /// </summary>
        /// <param name="SourceDT"></param>
        /// <param name="cols"></param>
        /// <param name="values"></param>
        private void CalSum(DataTable SourceDT, string[] values, ref Hashtable _ht)
        {
            foreach (string value in values)
            {
                _ht.Add(value, 0);
                foreach (DataRow dr in SourceDT.Rows)
                {
                    try
                    {
                        _ht[value] = decimal.Parse(_ht[value].ToString()) + decimal.Parse(dr[value].ToString());
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region 根据数据集字段ID获取字段列名
        private string GetFieldName(Guid DataSetFieldID)
        {
            Rpt_DataSetFields field = new Rpt_DataSetFieldsBLL(DataSetFieldID, true).Model;
            return field.FieldName;
        }
        #endregion

        #endregion

        public  int SaveForever(Dictionary<string, object> ParamValues)
        {
          return  Rpt_DataSet_DataCacheBLL.SaveForever(_m.DataSet, ParamValues);
        }

        #region 报表浏览记录维护
        /// <summary>
        /// 增加浏览次数
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="StaffID"></param>
        public static void AddViewTimes(Guid ReportID, int StaffID)
        {
            string CacheKey = "Cache-Rpt_ReportBLL-GetFrequentByStaff-" + StaffID.ToString();
            DataCache.RemoveCache(CacheKey);

            Rpt_ReportDAL dal = (Rpt_ReportDAL)DataAccess.CreateObject(DALClassName);
            dal.AddViewTimes(ReportID, StaffID);
        }

        /// <summary>
        ///清除浏览次数
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="StaffID"></param>
        public static void ClearViewTimes(Guid ReportID, int StaffID)
        {
            string CacheKey = "Cache-Rpt_ReportBLL-GetFrequentByStaff-" + StaffID.ToString();
            DataCache.RemoveCache(CacheKey);

            Rpt_ReportDAL dal = (Rpt_ReportDAL)DataAccess.CreateObject(DALClassName);
            dal.ClearViewTimes(ReportID, StaffID);

        }

        /// <summary>
        ///清除浏览记录
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="StaffID"></param>
        public static void DeleteViewTimes(Guid ReportID, int StaffID)
        {
            string CacheKey = "Cache-Rpt_ReportBLL-GetFrequentByStaff-" + StaffID.ToString();
            DataCache.RemoveCache(CacheKey);

            Rpt_ReportDAL dal = (Rpt_ReportDAL)DataAccess.CreateObject(DALClassName);
            dal.DeleteViewTimes(ReportID, StaffID);
        }

        /// <summary>
        /// 获取最常用的报表
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static DataTable GetFrequentByStaff(int staff)
        {
            string CacheKey = "Cache-Rpt_ReportBLL-GetFrequentByStaff-" + staff.ToString();
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);
            if (dt == null)
            {
                Rpt_ReportDAL dal = (Rpt_ReportDAL)DataAccess.CreateObject(DALClassName);
                dt = Tools.ConvertDataReaderToDataTable(dal.GetFrequentByStaff(staff));

                #region 写入缓存
                DataCache.SetCache(CacheKey, dt, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
                #endregion
            }
            return dt;
        }
        #endregion

    }
}