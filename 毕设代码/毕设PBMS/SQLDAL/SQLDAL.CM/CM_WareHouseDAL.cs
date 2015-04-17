
// ===================================================================
// 文件： CM_WareHouseDAL.cs
// 项目名称：
// 创建时间：2012-7-21
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
    ///CM_WareHouse数据访问DAL类
    /// </summary>
    public class CM_WareHouseDAL : BaseSimpleDAL<CM_WareHouse>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CM_WareHouseDAL()
        {
            _ProcePrefix = "MCS_CM.dbo.sp_CM_WareHouse";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_WareHouse m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, m.Address),
                SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum),
                SQLDatabase.MakeInParam("@Capacity", SqlDbType.Int, 4, m.Capacity),
				SQLDatabase.MakeInParam("@Area", SqlDbType.Int, 4, m.Area),
				SQLDatabase.MakeInParam("@Keeper", SqlDbType.VarChar, 50, m.Keeper),
				SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, m.Mobile),
                SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
                SQLDatabase.MakeInParam("@RelateVehicle", SqlDbType.Int, 4, m.RelateVehicle),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Decimal, 9, m.Longitude),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Decimal, 9, m.Latitude),
				SQLDatabase.MakeInParam("@ActiveState", SqlDbType.Int, 4, m.ActiveState),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
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
        public override int Update(CM_WareHouse m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, m.Address),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum),
                SQLDatabase.MakeInParam("@Capacity", SqlDbType.Int, 4, m.Capacity),
				SQLDatabase.MakeInParam("@Area", SqlDbType.Int, 4, m.Area),
				SQLDatabase.MakeInParam("@Keeper", SqlDbType.VarChar, 50, m.Keeper),
				SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, m.Mobile),
                SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
                SQLDatabase.MakeInParam("@RelateVehicle", SqlDbType.Int, 4, m.RelateVehicle),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Decimal, 9, m.Longitude),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Decimal, 9, m.Latitude),
				SQLDatabase.MakeInParam("@ActiveState", SqlDbType.Int, 4, m.ActiveState),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CM_WareHouse FillModel(IDataReader dr)
        {
            CM_WareHouse m = new CM_WareHouse();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["Code"].ToString())) m.Code = (string)dr["Code"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString())) m.OfficialCity = (int)dr["OfficialCity"];
            if (!string.IsNullOrEmpty(dr["Address"].ToString())) m.Address = (string)dr["Address"];
            if (!string.IsNullOrEmpty(dr["TeleNum"].ToString())) m.TeleNum = (string)dr["TeleNum"];
            if (!string.IsNullOrEmpty(dr["Capacity"].ToString())) m.Capacity = (int)dr["Capacity"];
            if (!string.IsNullOrEmpty(dr["Area"].ToString())) m.Area = (int)dr["Area"];
            if (!string.IsNullOrEmpty(dr["Keeper"].ToString())) m.Keeper = (string)dr["Keeper"];
            if (!string.IsNullOrEmpty(dr["Mobile"].ToString())) m.Mobile = (string)dr["Mobile"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["RelateVehicle"].ToString())) m.RelateVehicle = (int)dr["RelateVehicle"];
            if (!string.IsNullOrEmpty(dr["Longitude"].ToString())) m.Longitude = (decimal)dr["Longitude"];
            if (!string.IsNullOrEmpty(dr["Latitude"].ToString())) m.Latitude = (decimal)dr["Latitude"];
            if (!string.IsNullOrEmpty(dr["ActiveState"].ToString())) m.ActiveState = (int)dr["ActiveState"];
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
    }
}

