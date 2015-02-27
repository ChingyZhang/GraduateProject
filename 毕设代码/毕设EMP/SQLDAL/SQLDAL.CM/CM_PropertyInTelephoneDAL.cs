
// ===================================================================
// 文件： CM_PropertyInTelephoneDAL.cs
// 项目名称：
// 创建时间：2012/3/6
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.CM
{
    /// <summary>
    ///CM_PropertyInTelephone数据访问DAL类
    /// </summary>
    public class CM_PropertyInTelephoneDAL : BaseSimpleDAL<CM_PropertyInTelephone>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CM_PropertyInTelephoneDAL()
        {
            _ProcePrefix = "MCS_CM.dbo.sp_CM_PropertyInTelephone";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_PropertyInTelephone m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@TeleNumber", SqlDbType.VarChar, 50, m.TeleNumber),
				SQLDatabase.MakeInParam("@TeleCost", SqlDbType.Decimal, 9, m.TeleCost),
				SQLDatabase.MakeInParam("@NetCost", SqlDbType.Decimal, 9, m.NetCost),
				SQLDatabase.MakeInParam("@InstallDate", SqlDbType.DateTime, 8, m.InstallDate),
				SQLDatabase.MakeInParam("@UninstallDate", SqlDbType.DateTime, 8, m.UninstallDate),
				SQLDatabase.MakeInParam("@Owner", SqlDbType.VarChar, 50, m.Owner),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
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
        public override int Update(CM_PropertyInTelephone m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@TeleNumber", SqlDbType.VarChar, 50, m.TeleNumber),
				SQLDatabase.MakeInParam("@TeleCost", SqlDbType.Decimal, 9, m.TeleCost),
				SQLDatabase.MakeInParam("@NetCost", SqlDbType.Decimal, 9, m.NetCost),
				SQLDatabase.MakeInParam("@InstallDate", SqlDbType.DateTime, 8, m.InstallDate),
				SQLDatabase.MakeInParam("@UninstallDate", SqlDbType.DateTime, 8, m.UninstallDate),
				SQLDatabase.MakeInParam("@Owner", SqlDbType.VarChar, 50, m.Owner),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CM_PropertyInTelephone FillModel(IDataReader dr)
        {
            CM_PropertyInTelephone m = new CM_PropertyInTelephone();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["TeleNumber"].ToString())) m.TeleNumber = (string)dr["TeleNumber"];
            if (!string.IsNullOrEmpty(dr["TeleCost"].ToString())) m.TeleCost = (decimal)dr["TeleCost"];
            if (!string.IsNullOrEmpty(dr["NetCost"].ToString())) m.NetCost = (decimal)dr["NetCost"];
            if (!string.IsNullOrEmpty(dr["InstallDate"].ToString())) m.InstallDate = (DateTime)dr["InstallDate"];
            if (!string.IsNullOrEmpty(dr["UninstallDate"].ToString())) m.UninstallDate = (DateTime)dr["UninstallDate"];
            if (!string.IsNullOrEmpty(dr["Owner"].ToString())) m.Owner = (string)dr["Owner"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
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
        /// 获取指定管理片区范围内的所有已安装的电话目录
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <returns></returns>
        public IList<CM_PropertyInTelephone> GetListByOrganizeCity(int OrganizeCity)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity)
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetListByOrganizeCity", prams, out dr);

            return base.FillModelList(dr);
        }
    }
}

