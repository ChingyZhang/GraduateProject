
// ===================================================================
// 文件： PM_SalaryDataObjectDAL.cs
// 项目名称：
// 创建时间：2011/1/11
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Promotor;
using System.Collections.Generic;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.Promotor
{
    /// <summary>
    ///PM_SalaryDataObject数据访问DAL类
    /// </summary>
    public class PM_SalaryDataObjectDAL : BaseSimpleDAL<PM_SalaryDataObject>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PM_SalaryDataObjectDAL()
        {
            _ProcePrefix = "MCS_Promotor.dbo.sp_PM_SalaryDataObject";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_SalaryDataObject m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@SalesBase", SqlDbType.Decimal, 9, m.SalesBase),
				SQLDatabase.MakeInParam("@ActWorkDays", SqlDbType.Int, 4, m.ActWorkDays),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
                SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Decimal, 9, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Decimal, 9, m.Data02),
				SQLDatabase.MakeInParam("@Data03", SqlDbType.Decimal, 9, m.Data03),
				SQLDatabase.MakeInParam("@Data04", SqlDbType.Decimal, 9, m.Data04),
				SQLDatabase.MakeInParam("@Data05", SqlDbType.Decimal, 9, m.Data05),
				SQLDatabase.MakeInParam("@Data06", SqlDbType.Decimal, 9, m.Data06),
				SQLDatabase.MakeInParam("@Data07", SqlDbType.Decimal, 9, m.Data07),
				SQLDatabase.MakeInParam("@Data08", SqlDbType.Decimal, 9, m.Data08),
				SQLDatabase.MakeInParam("@Data09", SqlDbType.Decimal, 9, m.Data09),
				SQLDatabase.MakeInParam("@Data10", SqlDbType.Decimal, 9, m.Data10),
				SQLDatabase.MakeInParam("@Data11", SqlDbType.Decimal, 9, m.Data11),
				SQLDatabase.MakeInParam("@Data12", SqlDbType.Decimal, 9, m.Data12),
				SQLDatabase.MakeInParam("@Data13", SqlDbType.Decimal, 9, m.Data13),
				SQLDatabase.MakeInParam("@Data14", SqlDbType.Decimal, 9, m.Data14),
				SQLDatabase.MakeInParam("@Data15", SqlDbType.Decimal, 9, m.Data15),
				SQLDatabase.MakeInParam("@Data16", SqlDbType.Decimal, 9, m.Data16),
				SQLDatabase.MakeInParam("@Data17", SqlDbType.Decimal, 9, m.Data17),
				SQLDatabase.MakeInParam("@Data18", SqlDbType.Decimal, 9, m.Data18),
				SQLDatabase.MakeInParam("@Data19", SqlDbType.Decimal, 9, m.Data19),
				SQLDatabase.MakeInParam("@Data20", SqlDbType.Decimal, 9, m.Data20),
				SQLDatabase.MakeInParam("@Data21", SqlDbType.Decimal, 9, m.Data21),
				SQLDatabase.MakeInParam("@Data22", SqlDbType.Decimal, 9, m.Data22),
				SQLDatabase.MakeInParam("@Data23", SqlDbType.Decimal, 9, m.Data23),
				SQLDatabase.MakeInParam("@Data24", SqlDbType.Decimal, 9, m.Data24),
				SQLDatabase.MakeInParam("@Data25", SqlDbType.Decimal, 9, m.Data25),
				SQLDatabase.MakeInParam("@Data26", SqlDbType.Decimal, 9, m.Data26),
				SQLDatabase.MakeInParam("@Data27", SqlDbType.Decimal, 9, m.Data27),
				SQLDatabase.MakeInParam("@Data28", SqlDbType.Decimal, 9, m.Data28),
				SQLDatabase.MakeInParam("@Data29", SqlDbType.Decimal, 9, m.Data29),
				SQLDatabase.MakeInParam("@Data30", SqlDbType.Decimal, 9, m.Data30)
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
        public override int Update(PM_SalaryDataObject m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@SalesBase", SqlDbType.Decimal, 9, m.SalesBase),
				SQLDatabase.MakeInParam("@ActWorkDays", SqlDbType.Int, 4, m.ActWorkDays),
                SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Decimal, 9, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Decimal, 9, m.Data02),
				SQLDatabase.MakeInParam("@Data03", SqlDbType.Decimal, 9, m.Data03),
				SQLDatabase.MakeInParam("@Data04", SqlDbType.Decimal, 9, m.Data04),
				SQLDatabase.MakeInParam("@Data05", SqlDbType.Decimal, 9, m.Data05),
				SQLDatabase.MakeInParam("@Data06", SqlDbType.Decimal, 9, m.Data06),
				SQLDatabase.MakeInParam("@Data07", SqlDbType.Decimal, 9, m.Data07),
				SQLDatabase.MakeInParam("@Data08", SqlDbType.Decimal, 9, m.Data08),
				SQLDatabase.MakeInParam("@Data09", SqlDbType.Decimal, 9, m.Data09),
				SQLDatabase.MakeInParam("@Data10", SqlDbType.Decimal, 9, m.Data10),
				SQLDatabase.MakeInParam("@Data11", SqlDbType.Decimal, 9, m.Data11),
				SQLDatabase.MakeInParam("@Data12", SqlDbType.Decimal, 9, m.Data12),
				SQLDatabase.MakeInParam("@Data13", SqlDbType.Decimal, 9, m.Data13),
				SQLDatabase.MakeInParam("@Data14", SqlDbType.Decimal, 9, m.Data14),
				SQLDatabase.MakeInParam("@Data15", SqlDbType.Decimal, 9, m.Data15),
				SQLDatabase.MakeInParam("@Data16", SqlDbType.Decimal, 9, m.Data16),
				SQLDatabase.MakeInParam("@Data17", SqlDbType.Decimal, 9, m.Data17),
				SQLDatabase.MakeInParam("@Data18", SqlDbType.Decimal, 9, m.Data18),
				SQLDatabase.MakeInParam("@Data19", SqlDbType.Decimal, 9, m.Data19),
				SQLDatabase.MakeInParam("@Data20", SqlDbType.Decimal, 9, m.Data20),
				SQLDatabase.MakeInParam("@Data21", SqlDbType.Decimal, 9, m.Data21),
				SQLDatabase.MakeInParam("@Data22", SqlDbType.Decimal, 9, m.Data22),
				SQLDatabase.MakeInParam("@Data23", SqlDbType.Decimal, 9, m.Data23),
				SQLDatabase.MakeInParam("@Data24", SqlDbType.Decimal, 9, m.Data24),
				SQLDatabase.MakeInParam("@Data25", SqlDbType.Decimal, 9, m.Data25),
				SQLDatabase.MakeInParam("@Data26", SqlDbType.Decimal, 9, m.Data26),
				SQLDatabase.MakeInParam("@Data27", SqlDbType.Decimal, 9, m.Data27),
				SQLDatabase.MakeInParam("@Data28", SqlDbType.Decimal, 9, m.Data28),
				SQLDatabase.MakeInParam("@Data29", SqlDbType.Decimal, 9, m.Data29),
				SQLDatabase.MakeInParam("@Data30", SqlDbType.Decimal, 9, m.Data30)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PM_SalaryDataObject FillModel(IDataReader dr)
        {
            PM_SalaryDataObject m = new PM_SalaryDataObject();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Promotor"].ToString())) m.Promotor = (int)dr["Promotor"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["SalesTarget"].ToString())) m.SalesTarget = (decimal)dr["SalesTarget"];
            if (!string.IsNullOrEmpty(dr["SalesBase"].ToString())) m.SalesBase = (decimal)dr["SalesBase"];

            if (!string.IsNullOrEmpty(dr["ActWorkDays"].ToString())) m.ActWorkDays = (int)dr["ActWorkDays"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            if (!string.IsNullOrEmpty(dr["Data01"].ToString())) m.Data01 = (decimal)dr["Data01"];
            if (!string.IsNullOrEmpty(dr["Data02"].ToString())) m.Data02 = (decimal)dr["Data02"];
            if (!string.IsNullOrEmpty(dr["Data03"].ToString())) m.Data03 = (decimal)dr["Data03"];
            if (!string.IsNullOrEmpty(dr["Data04"].ToString())) m.Data04 = (decimal)dr["Data04"];
            if (!string.IsNullOrEmpty(dr["Data05"].ToString())) m.Data05 = (decimal)dr["Data05"];
            if (!string.IsNullOrEmpty(dr["Data06"].ToString())) m.Data06 = (decimal)dr["Data06"];
            if (!string.IsNullOrEmpty(dr["Data07"].ToString())) m.Data07 = (decimal)dr["Data07"];
            if (!string.IsNullOrEmpty(dr["Data08"].ToString())) m.Data08 = (decimal)dr["Data08"];
            if (!string.IsNullOrEmpty(dr["Data09"].ToString())) m.Data09 = (decimal)dr["Data09"];
            if (!string.IsNullOrEmpty(dr["Data10"].ToString())) m.Data10 = (decimal)dr["Data10"];
            if (!string.IsNullOrEmpty(dr["Data11"].ToString())) m.Data11 = (decimal)dr["Data11"];
            if (!string.IsNullOrEmpty(dr["Data12"].ToString())) m.Data12 = (decimal)dr["Data12"];
            if (!string.IsNullOrEmpty(dr["Data13"].ToString())) m.Data13 = (decimal)dr["Data13"];
            if (!string.IsNullOrEmpty(dr["Data14"].ToString())) m.Data14 = (decimal)dr["Data14"];
            if (!string.IsNullOrEmpty(dr["Data15"].ToString())) m.Data15 = (decimal)dr["Data15"];
            if (!string.IsNullOrEmpty(dr["Data16"].ToString())) m.Data16 = (decimal)dr["Data16"];
            if (!string.IsNullOrEmpty(dr["Data17"].ToString())) m.Data17 = (decimal)dr["Data17"];
            if (!string.IsNullOrEmpty(dr["Data18"].ToString())) m.Data18 = (decimal)dr["Data18"];
            if (!string.IsNullOrEmpty(dr["Data19"].ToString())) m.Data19 = (decimal)dr["Data19"];
            if (!string.IsNullOrEmpty(dr["Data20"].ToString())) m.Data20 = (decimal)dr["Data20"];
            if (!string.IsNullOrEmpty(dr["Data21"].ToString())) m.Data21 = (decimal)dr["Data21"];
            if (!string.IsNullOrEmpty(dr["Data22"].ToString())) m.Data22 = (decimal)dr["Data22"];
            if (!string.IsNullOrEmpty(dr["Data23"].ToString())) m.Data23 = (decimal)dr["Data23"];
            if (!string.IsNullOrEmpty(dr["Data24"].ToString())) m.Data24 = (decimal)dr["Data24"];
            if (!string.IsNullOrEmpty(dr["Data25"].ToString())) m.Data25 = (decimal)dr["Data25"];
            if (!string.IsNullOrEmpty(dr["Data26"].ToString())) m.Data26 = (decimal)dr["Data26"];
            if (!string.IsNullOrEmpty(dr["Data27"].ToString())) m.Data27 = (decimal)dr["Data27"];
            if (!string.IsNullOrEmpty(dr["Data28"].ToString())) m.Data28 = (decimal)dr["Data28"];
            if (!string.IsNullOrEmpty(dr["Data29"].ToString())) m.Data29 = (decimal)dr["Data29"];
            if (!string.IsNullOrEmpty(dr["Data30"].ToString())) m.Data30 = (decimal)dr["Data30"];

            return m;
        }

        public SqlDataReader GetByOrganizeCity(int OrganizeCity, int AccountMonth, int Promotor, int ApproveFlag)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int,4,OrganizeCity),
                	SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int,4,AccountMonth),
                    SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int,4,Promotor) ,
                    SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int,4,ApproveFlag)
                                        };
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetByOrganizeCity", parameters, out dr);
            return dr;

        }

        public void Init(int OrganizeCity, int AccountMonth)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int,4,OrganizeCity),
                	SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int,4,AccountMonth)                      
                                        };
            SQLDatabase.RunProc(_ProcePrefix + "_Init", parameters);
        }

        public int Refresh(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID)};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_Refresh", prams);
        }
    }
}

