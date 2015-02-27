using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.grf
{
    #region class  SqlReportData 产生提供给报表生成需要的 xml 或 JSON 数据
    public class SqlReportData
    {
        //连接SQL Server数据库的连接串
        public const string SqlConnStr = "server=srv.meichis.com,15433;User ID=sa;Password=meichis1!;";

        //定义在SQL中表示日期值的包围符号
        public const char DateSqlBracketChar = '\'';

        /// <summary>
        /// 根据查询SQL,产生提供给报表生成需要的 XML 数据，字段值为空也产生数据
        /// </summary>
        /// <param name="DataPage">需要处理的页面文件按</param>
        /// <param name="QuerySQL">获取报表数据的SQL查询</param>
        /// <param name="ToCompress">是否压缩为二进制文件</param>
        public static void FullGenNodeXmlData(System.Web.UI.Page DataPage, string QuerySQL, bool ToCompress)
        {
            SqlConnection ReportConn = new SqlConnection(SqlConnStr);
            SqlCommand ReportDataCommand = new SqlCommand(QuerySQL, ReportConn);
            ReportConn.Open();
            SqlDataReader ReportDataReader = ReportDataCommand.ExecuteReader();
            XMLReportData.GenNodeXmlDataFromReader(DataPage, ReportDataReader, ToCompress ? ResponseDataType.ZipBinary : ResponseDataType.PlainText);
            ReportDataReader.Close();
            ReportConn.Close();
        }


        /// <summary>
        /// 根据查询SQL,产生提供给报表生成需要的 XML 或 JSON 数据
        /// </summary>
        /// <param name="DataPage">需要处理的页面文件按</param>
        /// <param name="QuerySQL">获取报表数据的SQL查询</param>
        /// <param name="DataType">报表数据的格式类型</param>
        /// <param name="IsJSON">是否使用JSON格式</param>
        protected static void DoGenDetailData(System.Web.UI.Page DataPage, string QuerySQL, ResponseDataType DataType, bool IsJSON)
        {
            SqlConnection ReportConn = new SqlConnection(SqlConnStr);
            SqlDataAdapter ReportDataAdapter = new SqlDataAdapter(QuerySQL, ReportConn);
            DataSet ReportDataSet = new DataSet();
            ReportConn.Open();
            ReportDataAdapter.Fill(ReportDataSet);
            ReportConn.Close();

            if (IsJSON)
                JSONReportData.GenDataSet(DataPage, ReportDataSet, DataType);
            else
                XMLReportData.GenDataSet(DataPage, ReportDataSet, DataType);
        }

        /// <summary>
        /// 获取 SQL 查询到的数据行数
        /// </summary>
        /// <param name="QuerySQL">获取报表数据的SQL查询</param>
        /// <returns></returns>
        public static int BatchGetDataCount(string QuerySQL)
        {
            int Total = 0;

            SqlConnection ReportConn = new SqlConnection(SqlConnStr);
            SqlCommand ReportDataCommand = new SqlCommand(QuerySQL, ReportConn);
            ReportConn.Open();
            SqlDataReader ReportDataReader = ReportDataCommand.ExecuteReader();
            if (ReportDataReader.Read())
                Total = ReportDataReader.GetInt32(0);
            ReportDataReader.Close();
            ReportConn.Close();

            return Total;
        }


    }
    #endregion

    #region class  SqlXMLReportData 根据SQL产生报表需要的 XML 数据，采用 Sql 数据引擎
    public class SqlXMLReportData : SqlReportData
    {
        /// <summary>
        /// 获取单条SQL语句的结果集
        /// </summary>
        /// <param name="DataPage">需要处理的页面</param>
        /// <param name="QuerySQL">单条SQL语句</param>
        public static void GenOneRecordset(System.Web.UI.Page DataPage, string QuerySQL)
        {            
            SqlReportData.DoGenDetailData(DataPage, QuerySQL, ReportDataBase.DefaultDataType, false);
        }

        /// <summary>
        /// 获取SQL语句集的结果集
        /// </summary>
        /// <param name="DataPage">需要处理的页面</param>
        /// <param name="QueryList">SQL语句集合</param>
        public static void GenMultiRecordset(System.Web.UI.Page DataPage, ArrayList QueryList)
        {
            SqlConnection ReportConn = new SqlConnection(SqlConnStr);
            DataSet ReportDataSet = new DataSet();

            ReportConn.Open();

            foreach (ReportQueryItem item in QueryList)
            {
                SqlDataAdapter DataAdapter = new SqlDataAdapter(item.QuerySQL, ReportConn);
                DataAdapter.Fill(ReportDataSet, item.RecordsetName);
            }

            ReportConn.Close();

            XMLReportData.GenDataSet(DataPage, ReportDataSet, ReportDataBase.DefaultDataType);
        }
    }
    #endregion

    #region class  SqlJsonReportData 根据SQL产生报表需要的 JSON 数据，采用 Sql 数据引擎
    public class SqlJsonReportData : SqlReportData
    {
        public static void GenOneRecordset(System.Web.UI.Page DataPage, string QuerySQL)
        {
            SqlReportData.DoGenDetailData(DataPage, QuerySQL, ReportDataBase.DefaultDataType, true);
        }

        public static void GenMultiRecordset(System.Web.UI.Page DataPage, ArrayList QueryList)
        {
            SqlConnection ReportConn = new SqlConnection(SqlConnStr);
            DataSet ReportDataSet = new DataSet();

            ReportConn.Open();

            foreach (ReportQueryItem item in QueryList)
            {
                SqlDataAdapter DataAdapter = new SqlDataAdapter(item.QuerySQL, ReportConn);
                DataAdapter.Fill(ReportDataSet, item.RecordsetName);
            }

            ReportConn.Close();

            JSONReportData.GenDataSet(DataPage, ReportDataSet, ReportDataBase.DefaultDataType);
        }

    }
    #endregion
}