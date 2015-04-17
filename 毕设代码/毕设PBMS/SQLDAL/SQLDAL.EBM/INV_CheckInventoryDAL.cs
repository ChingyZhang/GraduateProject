
// ===================================================================
// 文件： INV_CheckInventoryDAL.cs
// 项目名称：
// 创建时间：2012-8-8
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.EBM
{
    /// <summary>
    ///INV_CheckInventory数据访问DAL类
    /// </summary>
    public class INV_CheckInventoryDAL : BaseComplexDAL<INV_CheckInventory, INV_CheckInventoryDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public INV_CheckInventoryDAL()
        {
            _ProcePrefix = "MCS_EBM.dbo.sp_INV_CheckInventory";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_CheckInventory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, m.ConfirmDate),
				SQLDatabase.MakeInParam("@ConfirmUser", SqlDbType.UniqueIdentifier, 16, m.ConfirmUser),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
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
        public override int Update(INV_CheckInventory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, m.ConfirmDate),
				SQLDatabase.MakeInParam("@ConfirmUser", SqlDbType.UniqueIdentifier, 16, m.ConfirmUser),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override INV_CheckInventory FillModel(IDataReader dr)
        {
            INV_CheckInventory m = new INV_CheckInventory();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["WareHouse"].ToString())) m.WareHouse = (int)dr["WareHouse"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["ConfirmDate"].ToString())) m.ConfirmDate = (DateTime)dr["ConfirmDate"];
            if (!string.IsNullOrEmpty(dr["ConfirmUser"].ToString())) m.ConfirmUser = (Guid)dr["ConfirmUser"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertUser"].ToString())) m.InsertUser = (Guid)dr["InsertUser"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateUser"].ToString())) m.UpdateUser = (Guid)dr["UpdateUser"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(INV_CheckInventoryDetail m)
        {
            m.CheckID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, m.CheckID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@BookQuantity", SqlDbType.Int, 4, m.BookQuantity),
				SQLDatabase.MakeInParam("@ActCheckQuantity", SqlDbType.Int, 4, m.ActCheckQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(INV_CheckInventoryDetail m)
        {
            m.CheckID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, m.CheckID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@BookQuantity", SqlDbType.Int, 4, m.BookQuantity),
				SQLDatabase.MakeInParam("@ActCheckQuantity", SqlDbType.Int, 4, m.ActCheckQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override INV_CheckInventoryDetail FillDetailModel(IDataReader dr)
        {
            INV_CheckInventoryDetail m = new INV_CheckInventoryDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["CheckID"].ToString())) m.CheckID = (int)dr["CheckID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["LotNumber"].ToString())) m.LotNumber = (string)dr["LotNumber"];
            if (!string.IsNullOrEmpty(dr["BookQuantity"].ToString())) m.BookQuantity = (int)dr["BookQuantity"];
            if (!string.IsNullOrEmpty(dr["ActCheckQuantity"].ToString())) m.ActCheckQuantity = (int)dr["ActCheckQuantity"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }

        /// <summary>
        /// 盘点初始化
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public int CheckInit(int WareHouse, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_CheckInit", prams);
        }

        /// <summary>
        /// 确认仓库盘点
        /// </summary>
        /// <param name="CheckID"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public int Confirm(int CheckID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, CheckID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Confirm", prams);
        }

        /// <summary>
        /// 取消盘点
        /// </summary>
        /// <param name="CheckID"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public int Cancel(int CheckID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, CheckID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Cancel", prams);
        }

        /// <summary>
        /// 上传保存已扫描的盘点物流码
        /// </summary>
        /// <param name="CheckID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:盘点单不为盘点中状态 -2:该物流码已盘点 -3:物流码无效</returns>
        public int CheckByOneCode(int CheckID, string Code)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, CheckID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_CheckByOneCode", prams);
        }
        /// <summary>
        /// 查询已盘点扫描的产品码
        /// </summary>
        /// <param name="PutInID">入库单号</param>
        /// <param name="DisplayMode">1:按产品统计 2:按箱码统计 3:产品码明细</param>
        /// <returns></returns>
        public DataTable GetCheckCodeLib(int CheckID, int DisplayMode)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, CheckID),
                SQLDatabase.MakeInParam("@DisplayMode", SqlDbType.Int, 4, DisplayMode)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetCodeLib", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 撤销盘点
        /// </summary>
        /// <param name="CheckID"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public int UnConfirm(int CheckID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, CheckID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_UnConfirm", prams);
        }
    }
}

