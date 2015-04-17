
// ===================================================================
// 文件： CM_ClientManufactInfoDAL.cs
// 项目名称：
// 创建时间：2015-03-24
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;


namespace MCSFramework.SQLDAL.CM
{
    /// <summary>
    ///CM_ClientManufactInfo数据访问DAL类
    /// </summary>
    public class CM_ClientManufactInfoDAL : BaseSimpleDAL<CM_ClientManufactInfo>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CM_ClientManufactInfoDAL()
        {
            _ProcePrefix = "MCS_CM.dbo.sp_CM_ClientManufactInfo";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_ClientManufactInfo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Manufacturer", SqlDbType.Int, 4, m.Manufacturer),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 100, m.Code),
				SQLDatabase.MakeInParam("@ERPID", SqlDbType.Int, 4, m.ERPID),
				SQLDatabase.MakeInParam("@Channel", SqlDbType.Int, 4, m.Channel),
				SQLDatabase.MakeInParam("@MarketType", SqlDbType.Int, 4, m.MarketType),
				SQLDatabase.MakeInParam("@IsKeyAccount", SqlDbType.Int, 4, m.IsKeyAccount),
				SQLDatabase.MakeInParam("@VestKeyAccount", SqlDbType.Int, 4, m.VestKeyAccount),
				SQLDatabase.MakeInParam("@ClientLevel", SqlDbType.Int, 4, m.ClientLevel),
				SQLDatabase.MakeInParam("@BalanceMode", SqlDbType.Int, 4, m.BalanceMode),
				SQLDatabase.MakeInParam("@ClientClassfiy", SqlDbType.Int, 4, m.ClientClassfiy),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, m.ClientManager),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@GeoCode", SqlDbType.VarChar, 100, m.GeoCode),
				SQLDatabase.MakeInParam("@VisitRoute", SqlDbType.Int, 4, m.VisitRoute),
				SQLDatabase.MakeInParam("@VisitSequence", SqlDbType.Int, 4, m.VisitSequence),
				SQLDatabase.MakeInParam("@VisitTemplate", SqlDbType.Int, 4, m.VisitTemplate),
				SQLDatabase.MakeInParam("@VisitCycle", SqlDbType.Int, 4, m.VisitCycle),
				SQLDatabase.MakeInParam("@VisitDay", SqlDbType.Int, 4, m.VisitDay),
                SQLDatabase.MakeInParam("@SyncState", SqlDbType.Int, 4, m.SyncState),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(CM_ClientManufactInfo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Manufacturer", SqlDbType.Int, 4, m.Manufacturer),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 100, m.Code),
				SQLDatabase.MakeInParam("@ERPID", SqlDbType.Int, 4, m.ERPID),
				SQLDatabase.MakeInParam("@Channel", SqlDbType.Int, 4, m.Channel),
				SQLDatabase.MakeInParam("@MarketType", SqlDbType.Int, 4, m.MarketType),
				SQLDatabase.MakeInParam("@IsKeyAccount", SqlDbType.Int, 4, m.IsKeyAccount),
				SQLDatabase.MakeInParam("@VestKeyAccount", SqlDbType.Int, 4, m.VestKeyAccount),
				SQLDatabase.MakeInParam("@ClientLevel", SqlDbType.Int, 4, m.ClientLevel),
				SQLDatabase.MakeInParam("@BalanceMode", SqlDbType.Int, 4, m.BalanceMode),
				SQLDatabase.MakeInParam("@ClientClassfiy", SqlDbType.Int, 4, m.ClientClassfiy),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, m.ClientManager),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@GeoCode", SqlDbType.VarChar, 100, m.GeoCode),
				SQLDatabase.MakeInParam("@VisitRoute", SqlDbType.Int, 4, m.VisitRoute),
				SQLDatabase.MakeInParam("@VisitSequence", SqlDbType.Int, 4, m.VisitSequence),
				SQLDatabase.MakeInParam("@VisitTemplate", SqlDbType.Int, 4, m.VisitTemplate),
				SQLDatabase.MakeInParam("@VisitCycle", SqlDbType.Int, 4, m.VisitCycle),
				SQLDatabase.MakeInParam("@VisitDay", SqlDbType.Int, 4, m.VisitDay),
                SQLDatabase.MakeInParam("@SyncState", SqlDbType.Int, 4, m.SyncState),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CM_ClientManufactInfo FillModel(IDataReader dr)
        {
            CM_ClientManufactInfo m = new CM_ClientManufactInfo();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["Manufacturer"].ToString())) m.Manufacturer = (int)dr["Manufacturer"];
            if (!string.IsNullOrEmpty(dr["Code"].ToString())) m.Code = (string)dr["Code"];
            if (!string.IsNullOrEmpty(dr["ERPID"].ToString())) m.ERPID = (int)dr["ERPID"];
            if (!string.IsNullOrEmpty(dr["Channel"].ToString())) m.Channel = (int)dr["Channel"];
            if (!string.IsNullOrEmpty(dr["MarketType"].ToString())) m.MarketType = (int)dr["MarketType"];
            if (!string.IsNullOrEmpty(dr["IsKeyAccount"].ToString())) m.IsKeyAccount = (int)dr["IsKeyAccount"];
            if (!string.IsNullOrEmpty(dr["VestKeyAccount"].ToString())) m.VestKeyAccount = (int)dr["VestKeyAccount"];
            if (!string.IsNullOrEmpty(dr["ClientLevel"].ToString())) m.ClientLevel = (int)dr["ClientLevel"];
            if (!string.IsNullOrEmpty(dr["BalanceMode"].ToString())) m.BalanceMode = (int)dr["BalanceMode"];
            if (!string.IsNullOrEmpty(dr["ClientClassfiy"].ToString())) m.ClientClassfiy = (int)dr["ClientClassfiy"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["ClientManager"].ToString())) m.ClientManager = (int)dr["ClientManager"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];
            if (!string.IsNullOrEmpty(dr["GeoCode"].ToString())) m.GeoCode = (string)dr["GeoCode"];
            if (!string.IsNullOrEmpty(dr["VisitRoute"].ToString())) m.VisitRoute = (int)dr["VisitRoute"];
            if (!string.IsNullOrEmpty(dr["VisitSequence"].ToString())) m.VisitSequence = (int)dr["VisitSequence"];
            if (!string.IsNullOrEmpty(dr["VisitTemplate"].ToString())) m.VisitTemplate = (int)dr["VisitTemplate"];
            if (!string.IsNullOrEmpty(dr["VisitCycle"].ToString())) m.VisitCycle = (int)dr["VisitCycle"];
            if (!string.IsNullOrEmpty(dr["VisitDay"].ToString())) m.VisitDay = (int)dr["VisitDay"];
            if (!string.IsNullOrEmpty(dr["SyncState"].ToString())) m.SyncState = (int)dr["SyncState"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }


        /// <summary>
        /// 审核客户的厂商管理信息
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Manufacturer"></param>
        /// <param name="OpStaff"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public int Approve(int Client, int Manufacturer, int OpStaff, int State)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
				SQLDatabase.MakeInParam("@Manufacturer", SqlDbType.Int, 4, Manufacturer),
				SQLDatabase.MakeInParam("@OpStaff", SqlDbType.Int, 4, OpStaff),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);

            return ret;
        }
    }
}

