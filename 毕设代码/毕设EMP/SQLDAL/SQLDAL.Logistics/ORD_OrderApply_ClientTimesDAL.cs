// ===================================================================
// 文件： ORD_OrderApply_ClientTimesDAL.cs
// 项目名称：
// 创建时间：2013-03-20
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Logistics;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.Logistics
{
    /// <summary>
    ///ORD_OrderApply_ClientTimes数据访问DAL类
    /// </summary>
    public class ORD_OrderApply_ClientTimesDAL : BaseSimpleDAL<ORD_OrderApply_ClientTimes>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_OrderApply_ClientTimesDAL()
        {
            _ProcePrefix = "MCS_Logistics.dbo.sp_ORD_OrderApply_ClientTimes";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_OrderApply_ClientTimes m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@OrderTimes", SqlDbType.Int, 4, m.OrderTimes)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(ORD_OrderApply_ClientTimes m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@OrderTimes", SqlDbType.Int, 4, m.OrderTimes)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ORD_OrderApply_ClientTimes FillModel(IDataReader dr)
        {
            ORD_OrderApply_ClientTimes m = new ORD_OrderApply_ClientTimes();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["OrderTimes"].ToString())) m.OrderTimes = (int)dr["OrderTimes"];

            return m;
        }

        public DataTable GetByOrganizeCity(int OrganizeCity, int Client)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetByOrganizeCity", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

