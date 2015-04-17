
// ===================================================================
// 文件： INV_PutInStoreageDAL.cs
// 项目名称：
// 创建时间：2012-7-23
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
    ///INV_PutInStoreage数据访问DAL类
    /// </summary>
    public class INV_PutInStoreageDAL : BaseComplexDAL<INV_PutInStoreage, INV_PutInStoreageDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public INV_PutInStoreageDAL()
        {
            _ProcePrefix = "MCS_EBM.dbo.sp_INV_PutInStoreage";
        }
        #endregion

        #region 基本操作
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_PutInStoreage m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@WareHouseCell", SqlDbType.Int, 4, m.WareHouseCell),
				SQLDatabase.MakeInParam("@Source", SqlDbType.Int, 4, m.Source),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ConfirmTime", SqlDbType.DateTime, 8, m.ConfirmTime),
				SQLDatabase.MakeInParam("@ConfirmUser", SqlDbType.UniqueIdentifier, 16, m.ConfirmUser),
				SQLDatabase.MakeInParam("@UnPutInTime", SqlDbType.DateTime, 8, m.UnPutInTime),
				SQLDatabase.MakeInParam("@UnPutInUser", SqlDbType.UniqueIdentifier, 16, m.UnPutInUser),
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
        public override int Update(INV_PutInStoreage m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@WareHouseCell", SqlDbType.Int, 4, m.WareHouseCell),
				SQLDatabase.MakeInParam("@Source", SqlDbType.Int, 4, m.Source),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ConfirmTime", SqlDbType.DateTime, 8, m.ConfirmTime),
				SQLDatabase.MakeInParam("@ConfirmUser", SqlDbType.UniqueIdentifier, 16, m.ConfirmUser),
				SQLDatabase.MakeInParam("@UnPutInTime", SqlDbType.DateTime, 8, m.UnPutInTime),
				SQLDatabase.MakeInParam("@UnPutInUser", SqlDbType.UniqueIdentifier, 16, m.UnPutInUser),
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

        protected override INV_PutInStoreage FillModel(IDataReader dr)
        {
            INV_PutInStoreage m = new INV_PutInStoreage();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["WareHouse"].ToString())) m.WareHouse = (int)dr["WareHouse"];
            if (!string.IsNullOrEmpty(dr["WareHouseCell"].ToString())) m.WareHouseCell = (int)dr["WareHouseCell"];
            if (!string.IsNullOrEmpty(dr["Source"].ToString())) m.Source = (int)dr["Source"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["ConfirmTime"].ToString())) m.ConfirmTime = (DateTime)dr["ConfirmTime"];
            if (!string.IsNullOrEmpty(dr["ConfirmUser"].ToString())) m.ConfirmUser = (Guid)dr["ConfirmUser"];
            if (!string.IsNullOrEmpty(dr["UnPutInTime"].ToString())) m.UnPutInTime = (DateTime)dr["UnPutInTime"];
            if (!string.IsNullOrEmpty(dr["UnPutInUser"].ToString())) m.UnPutInUser = (Guid)dr["UnPutInUser"];
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

        public override int AddDetail(INV_PutInStoreageDetail m)
        {
            m.PutInID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PutInID", SqlDbType.Int, 4, m.PutInID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(INV_PutInStoreageDetail m)
        {
            m.PutInID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@PutInID", SqlDbType.Int, 4, m.PutInID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override INV_PutInStoreageDetail FillDetailModel(IDataReader dr)
        {
            INV_PutInStoreageDetail m = new INV_PutInStoreageDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["PutInID"].ToString())) m.PutInID = (int)dr["PutInID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["Quantity"].ToString())) m.Quantity = (int)dr["Quantity"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }

        #endregion

        #region 确认入库 与 撤销入库
        public int ConfirmPutIn(int ID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ConfirmPutIn", prams);

            return ret;
        }

        public int UndoPutIn(int ID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UndoPutIn", prams);

            return ret;
        }
        #endregion

        #region 逐码扫描产品
        public int PutInByOneCode(int PutInID, string Code, int Product, string LotNumber)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PutInID", SqlDbType.Int, 4, PutInID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, LotNumber)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_PutInByOneCode", prams);
        }
        #endregion

        #region 生成入库单号
        public string GenerateSheetCode(int WareHouse)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeOutParam("@SheetCode", SqlDbType.VarChar, 100)                
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GenerateSheetCode", prams);

            if (prams[1].Value != DBNull.Value)
                return prams[1].Value.ToString();
            else
                return "";
        }
        #endregion

        /// <summary>
        /// 查询已入库扫描的产品码
        /// </summary>
        /// <param name="PutInID">入库单号</param>
        /// <param name="DisplayMode">1:按产品统计 2:按箱码统计 3:产品码明细</param>
        /// <returns></returns>
        public DataTable GetPutInCodeLib(int PutInID, int DisplayMode)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PutInID", SqlDbType.Int, 4, PutInID),
                SQLDatabase.MakeInParam("@DisplayMode", SqlDbType.Int, 4, DisplayMode)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetPutInCodeLib", prams,out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        #region 退库操作

        #region 确认退库
        public int ConfirmPutOut(int ID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ConfirmPutOut", prams);

            return ret;
        }
        #endregion

        #region 逐码扫描产品
        public int PutOutByOneCode(int PutInID, string Code)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PutInID", SqlDbType.Int, 4, PutInID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_PutOutByOneCode", prams);
        }
        #endregion

        #endregion
    }
}

