
// ===================================================================
// 文件： Org_StaffNumberLimitDAL.cs
// 项目名称：
// 创建时间：2010/4/30
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///Org_StaffNumberLimit数据访问DAL类
    /// </summary>
    public class Org_StaffNumberLimitDAL : BaseSimpleDAL<Org_StaffNumberLimit>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Org_StaffNumberLimitDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Org_StaffNumberLimit";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Org_StaffNumberLimit m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@NumberLimit", SqlDbType.Int, 4, m.NumberLimit),
				SQLDatabase.MakeInParam("@BudgetNumber", SqlDbType.Int, 4, m.BudgetNumber),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 50, m.Remark),
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
        public override int Update(Org_StaffNumberLimit m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@NumberLimit", SqlDbType.Int, 4, m.NumberLimit),
				SQLDatabase.MakeInParam("@BudgetNumber", SqlDbType.Int, 4, m.BudgetNumber),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 50, m.Remark),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Org_StaffNumberLimit FillModel(IDataReader dr)
        {
            Org_StaffNumberLimit m = new Org_StaffNumberLimit();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Position"].ToString())) m.Position = (int)dr["Position"];
            if (!string.IsNullOrEmpty(dr["NumberLimit"].ToString())) m.NumberLimit = (int)dr["NumberLimit"];
            if (!string.IsNullOrEmpty(dr["BudgetNumber"].ToString())) m.BudgetNumber = (int)dr["BudgetNumber"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public int Init(int OrganizeCity, int Level, int Position, bool IncludeChild, int InsertStaff)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4,Level),
				SQLDatabase.MakeInParam("@Position",SqlDbType.Int, 4,Position),
                SQLDatabase.MakeInParam("@IncludeChild", SqlDbType.Int, 4,IncludeChild ? 0 : 1),
                SQLDatabase.MakeInParam("@InsertStaff",SqlDbType.Int, 4,InsertStaff )
			};
            return SQLDatabase.RunProc(_ProcePrefix + "_Init", prams);
        }

        public int GetActualNumber(int OrganizeCity, int Position)
        {
            SqlParameter[] prams ={
                      SQLDatabase .MakeInParam ("@OrganizeCity",SqlDbType.Int ,4,OrganizeCity ),
                      SQLDatabase .MakeInParam ("@Position",SqlDbType.Int ,4,Position )
                  };
            int count = SQLDatabase.RunProc(_ProcePrefix + "_GetActualNumber", prams);
            return count;
        }

        /// <summary>
        /// 判断两个城市及类别是否同属于一条限制记录
        /// </summary>
        /// <param name="OrganizeCity1"></param>
        /// <param name="OrganizeCity2"></param>
        /// <returns></returns>
        public bool IsSameLimit(int OrganizeCity1, int OrganizeCity2, int Position1, int Position2)
        {
            SqlParameter[] parms ={
             SQLDatabase.MakeInParam("@OrganizeCity1",SqlDbType.Int,4,OrganizeCity1),
             SQLDatabase.MakeInParam("@OrganizeCity2",SqlDbType.Int,4,OrganizeCity2),
             SQLDatabase.MakeInParam("@Position1",SqlDbType.Int,4,Position1),
             SQLDatabase.MakeInParam("@Position2",SqlDbType.Int,4,Position2)
         };
            return SQLDatabase.RunProc(_ProcePrefix + "_IsSameLimit", parms) == 0;
        }

        /// <summary>
        /// 判断该城市限制了录入促销员的数量
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <returns>未限制返回100,否则返回还可增加人数</returns>
        public int CheckAllowAdd(int OrganizeCity, int Position)
        {
            SqlParameter[] parms ={
                                     SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity),
                                     SQLDatabase.MakeInParam("@Position",SqlDbType.Int,4,Position)
                                 };
            int surplusPM = SQLDatabase.RunProc(_ProcePrefix + "_CheckAllowAdd", parms);
            return surplusPM;
        }

        /// <summary>
        /// 判断该城市是否设定了促销员的预算人数
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="Position"></param>
        /// <returns>未限制返回100,否则返回预算内还可增加人数</returns>
        public int CheckOverBudget(int OrganizeCity, int Position)
        {
            SqlParameter[] parms ={
                                     SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity),
                                     SQLDatabase.MakeInParam("@Position",SqlDbType.Int,4,Position)
                                 };
            int surplusPM = SQLDatabase.RunProc(_ProcePrefix + "_CheckOverBudget", parms);
            return surplusPM;
        }
    }
}

