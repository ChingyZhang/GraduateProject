// ===================================================================
// 文件： Org_Staff_DAL.cs
// 项目名称：
// 创建时间：2008-12-11
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.Model;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///Org_Staff数据访问DAL类
    /// </summary>
    public class Org_Staff_DAL : BaseSimpleDAL<Org_Staff>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Org_Staff_DAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Org_Staff";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Org_Staff m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
			  	SQLDatabase.MakeInParam("@RealName", SqlDbType.VarChar, 50, m.RealName),
				SQLDatabase.MakeInParam("@Sex", SqlDbType.Int, 4, m.Sex),
				SQLDatabase.MakeInParam("@StaffCode", SqlDbType.VarChar, 50, m.StaffCode),
				SQLDatabase.MakeInParam("@BeginWorkTime", SqlDbType.DateTime, 8, m.BeginWorkTime),
				SQLDatabase.MakeInParam("@EndWorkTime", SqlDbType.DateTime, 8, m.EndWorkTime),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@Dimission", SqlDbType.Int, 4, m.Dimission),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int,4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(Org_Staff m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@RealName", SqlDbType.VarChar, 50, m.RealName),
				SQLDatabase.MakeInParam("@Sex", SqlDbType.Int, 4, m.Sex),
				SQLDatabase.MakeInParam("@StaffCode", SqlDbType.VarChar, 50, m.StaffCode),
				SQLDatabase.MakeInParam("@BeginWorkTime", SqlDbType.DateTime, 8, m.BeginWorkTime),
				SQLDatabase.MakeInParam("@EndWorkTime", SqlDbType.DateTime, 8, m.EndWorkTime),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@Dimission", SqlDbType.Int, 4, m.Dimission),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Org_Staff FillModel(IDataReader dr)
        {
            Org_Staff m = new Org_Staff();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["RealName"].ToString())) m.RealName = (string)dr["RealName"];
            if (!string.IsNullOrEmpty(dr["Sex"].ToString())) m.Sex = (int)dr["Sex"];
            if (!string.IsNullOrEmpty(dr["StaffCode"].ToString())) m.StaffCode = (string)dr["StaffCode"];
            if (!string.IsNullOrEmpty(dr["BeginWorkTime"].ToString())) m.BeginWorkTime = (DateTime)dr["BeginWorkTime"];
            if (!string.IsNullOrEmpty(dr["EndWorkTime"].ToString())) m.EndWorkTime = (DateTime)dr["EndWorkTime"];
            if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString())) m.OfficialCity = (int)dr["OfficialCity"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Position"].ToString())) m.Position = (int)dr["Position"];
            if (!string.IsNullOrEmpty(dr["Dimission"].ToString())) m.Dimission = (int)dr["Dimission"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        #region 员工离、复职
        /// <summary>
        /// 员工复职
        /// </summary>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public int DoRehab(int StaffID)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int,4,StaffID)
            };
            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_Org_Staff_DoRehab", parms);
        }

        /// <summary>
        /// 员工离职
        /// </summary>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public int DoDimission(int StaffID)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int,4,StaffID)
            };
            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_Org_Staff_DoDimission", parms);
        }
        #endregion

        #region 获取员工所管辖的办事处
        /// <summary>
        /// 获取员工所管辖的办事处的ID字符串
        /// </summary>
        /// <param name="staffid"></param>
        /// <returns></returns>
        public string GetStaffOrganizeCityIDs(int staffid)
        {
            string ret = "";
            SqlDataReader dr = null;
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int,4,staffid)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_GetAllOrganizeCityIDs", parms, out dr);

            if (dr.Read())
            {
                ret = dr.GetString(0);
            }
            dr.Close();
            return ret;
        }

        /// <summary>
        /// 获取员工所管辖的办事处的ID字符串
        /// </summary>
        /// <param name="staffid"></param>
        /// <returns></returns>
        public DataTable GetStaffOrganizeCity(int staffid)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int,4,staffid)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_GetAllOrganizeCity", parms, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion

        /// <summary>
        /// 根据员工ID根据对应的用户帐户
        /// </summary>
        /// <param name="staffid"></param>
        /// <returns></returns>
        public DataTable GetUserListByStaffID(int staffid)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int,4,staffid)
            };
            SQLDatabase.RunProc("sp_User_GetUserByStaffID", parms, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 根据员工职位获取属于该职位的员工及登录用户名列表
        /// </summary>
        /// <param name="Positions"></param>
        /// <returns></returns>
        public DataTable GetRealNameAndUserNameByPosition(string Positions)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Positions", SqlDbType.VarChar,2000,Positions)
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetRealNameAndUserNameByPosition", parms, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public bool IsMyOrganizeCity(int OrganizeCity, int Staff)
        {

            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int,4,OrganizeCity),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int,4,Staff)
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_IsMyOrganizeCity", parms) == 1 ? true : false;
        }

        #region 读取当前操作员任务数据
        public DataTable GetTodayTask(string Username)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256 ,Username)
                                   };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTodayTask", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 统计数据填报进度情况
        /// </summary>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public DataTable GetFillDataProgress(int Staff, int Month)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4 ,Staff),
                SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4 ,Month)
                                   };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetFillDataProgress", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion

        #region 员工可兼管的管理片区
        public int StaffInOrganizeCity_Add(int staff, int organizecity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4 ,staff),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,organizecity),
            };
            #endregion

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_Org_Staff_StaffInOrganizeCity_Add", prams);
        }

        public int StaffInOrganizeCity_Delete(int staff, int organizecity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4 ,staff),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,organizecity),
            };
            #endregion

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_Org_Staff_StaffInOrganizeCity_Delete", prams);
        }

        public IList<Addr_OrganizeCity> StaffInOrganizeCity_GetOrganizeCitys(int staff)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4 ,staff),
            };
            #endregion

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_Org_Staff_StaffInOrganizeCity_GetByStaff", prams, out dr);

            return new Addr_OrganizeCityDAL().FillModelList(dr);
        }
        public int UpdateLoginTime(DateTime time)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Time", SqlDbType.DateTime, 8 , time)
            };
            #endregion

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_UpdateLoginTime", prams);
        }
        public DataTable GetMaxLoginTime()
        {
            SqlDataReader dr = null;
            DateTime time = DateTime.Now;
            SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_GetMaxLoginTime", out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion

        public static int GetStaffIDByUserName(string userName)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar,200,userName)
            };
            try
            {
                return SQLDatabase.RunProc("MCS_SYS.dbo.sp_GetStaffIDByUserName", parms);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public int GetPMSalaryApproveState(int Staff, int Month,int city)
        {

            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int,4,Staff),
                SQLDatabase.MakeInParam("@Month", SqlDbType.Int,4,Month),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,city)
            };
            return SQLDatabase.RunProc(_ProcePrefix + "_GetPMSalaryApproveState", parms);
        }
        public DataTable GetFillProcessDetail(int Staff, int Month, int type)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int,4,Staff),
                SQLDatabase.MakeInParam("@Month", SqlDbType.Int,4,Month),
                SQLDatabase.MakeInParam("@Type",SqlDbType.Int,4,type)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_GetFillProcessDetail", parms, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
        public DataTable GetLowerPositionTask(int staff, int type,int city)
        {
            SqlDataReader dr= null;
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@StaffID",SqlDbType.Int,4,staff),
                SQLDatabase.MakeInParam("@FeeType",SqlDbType.Int,4,type),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,city)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_GetLowerPositionTask", parms, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
        public DataTable GetLowerPositionTask(int staff, int type, int city,int month)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@StaffID",SqlDbType.Int,4,staff),
                SQLDatabase.MakeInParam("@FeeType",SqlDbType.Int,4,type),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,city),
                SQLDatabase.MakeInParam("@AccountMonth",SqlDbType.Int,4,month)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_GetLowerPositionTask", parms, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}
