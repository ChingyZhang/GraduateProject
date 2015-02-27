using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCSFramework.DBUtility;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.Common;

namespace SyncERPJXC.SQLDAL
{
    public class ERPIF
    {
        private string _ProcePrefix = "";

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ERPIF()
        {
            _ProcePrefix = "MCS_ERPIF.dbo.sp_Sync";
        }
        #endregion

        /// <summary>
        /// 营销系统与ERP同步产品目录
        /// </summary>
        /// <returns></returns>
        public static int Sync_Product()
        {
            return SQLDatabase.RunProc("MCS_ERPIF.dbo.sp_Sync_Product");
        }

        /// <summary>
        /// 获取指定日期范围内的发货单
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable GetShipHeader(DateTime begin, DateTime end)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, begin),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, end)
			};
            #endregion

            SQLDatabase.RunProc("MCS_ERPIF.dbo.sp_Sync_SHIPHEADER_GetBySDATA", prams, out dr, 3600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 按经销商同步
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="ClientCode"></param>
        /// <returns></returns>
        public static DataTable GetShipHeader(DateTime begin, DateTime end,string ClientCode)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, begin),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, end),
                SQLDatabase.MakeInParam("@ClientCode", SqlDbType.VarChar, 50, ClientCode)
			};
            #endregion

            SQLDatabase.RunProc("MCS_ERPIF.dbo.sp_Sync_SHIPHEADER_GetBySDATA", prams, out dr, 3600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取指定发货单的发货明细
        /// </summary>
        /// <param name="DELIVERY_ID"></param>
        /// <returns></returns>
        public static DataTable GetShipDetail(string DELIVERY_ID)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@DELIVERY_ID", SqlDbType.NVarChar, 255, DELIVERY_ID)
			};
            #endregion

            SQLDatabase.RunProc("MCS_ERPIF.dbo.sp_Sync_SHIPDETAIL_GetByDELIVERYID", prams, out dr, 900);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}
