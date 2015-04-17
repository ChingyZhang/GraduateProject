
// ===================================================================
// 文件： PN_NoticeDAL.cs
// 项目名称：
// 创建时间：2009-3-11
// 作者:	   zhousongqin
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.OA
{
    /// <summary>
    ///PN_Notice数据访问DAL类
    /// </summary>
    public class PN_NoticeDAL : BaseSimpleDAL<PN_Notice>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PN_NoticeDAL()
        {
            _ProcePrefix = "MCS_OA.dbo.sp_PN_Notice";
        }
        #endregion


        public override int Add(PN_Notice m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Topic", SqlDbType.VarChar, 200, m.Topic),
				SQLDatabase.MakeInParam("@KeyWord", SqlDbType.VarChar, 500, m.KeyWord),
				SQLDatabase.MakeInParam("@Content", SqlDbType.Text, 0, m.Content),
				SQLDatabase.MakeInParam("@ToAllStaff", SqlDbType.Char, 1, m.ToAllStaff),
                SQLDatabase.MakeInParam("@ToAllOrganizeCity", SqlDbType.Char, 1, m.ToAllOrganizeCity),
				SQLDatabase.MakeInParam("@CanComment", SqlDbType.Char, 1, m.CanComment),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.IsDelete),
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
        public override int Update(PN_Notice m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Topic", SqlDbType.VarChar, 200, m.Topic),
				SQLDatabase.MakeInParam("@KeyWord", SqlDbType.VarChar, 500, m.KeyWord),
				SQLDatabase.MakeInParam("@Content", SqlDbType.Text, 0, m.Content),
				SQLDatabase.MakeInParam("@ToAllStaff", SqlDbType.Char, 1, m.ToAllStaff),
                SQLDatabase.MakeInParam("@ToAllOrganizeCity", SqlDbType.Char, 1, m.ToAllOrganizeCity),
				SQLDatabase.MakeInParam("@CanComment", SqlDbType.Char, 1, m.CanComment),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.IsDelete),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PN_Notice FillModel(IDataReader dr)
        {
            PN_Notice m = new PN_Notice();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Topic"].ToString())) m.Topic = (string)dr["Topic"];
            if (!string.IsNullOrEmpty(dr["KeyWord"].ToString())) m.KeyWord = (string)dr["KeyWord"];
            if (!string.IsNullOrEmpty(dr["Content"].ToString())) m.Content = (string)dr["Content"];
            if (!string.IsNullOrEmpty(dr["ToAllStaff"].ToString())) m.ToAllStaff = (string)dr["ToAllStaff"];
            if (!string.IsNullOrEmpty(dr["ToAllOrganizeCity"].ToString())) m.ToAllOrganizeCity = (string)dr["ToAllOrganizeCity"];
            if (!string.IsNullOrEmpty(dr["CanComment"].ToString())) m.CanComment = (string)dr["CanComment"];
            if (!string.IsNullOrEmpty(dr["IsDelete"].ToString())) m.IsDelete = (string)dr["IsDelete"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }


        #region  通过ID删除公告
        public int IsDeleteByID(int id)
        {
            SqlParameter[] parms = {
              SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,id)    
            };
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_IsDeleteByID", parms);
            return ret;
        }
        #endregion

        /// <summary>
        /// 设置公告审核标志
        /// </summary>
        /// <param name="ApproveFlag"></param>
        /// <param name="Staff"></param>
        public void Approve(int ID, int ApproveFlag, int Staff)
        {
            SqlParameter[] parms ={
                                    SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,ID),
                                    SQLDatabase.MakeInParam("@ApproveFlag",SqlDbType.Int,4,ApproveFlag),
                                    SQLDatabase.MakeInParam("@Staff",SqlDbType.Int,4,Staff)
                                };
            SQLDatabase.RunProc(_ProcePrefix + "_Approve", parms);
        }

        public IList<PN_Notice> GetNoticeByStaff(int Staff, DateTime BeginDate, DateTime EndDate, int ApproveFlag)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms ={
                                    SQLDatabase.MakeInParam("@Staff",SqlDbType.Int,4,Staff),
                                    SQLDatabase.MakeInParam("@BeginDate",SqlDbType.DateTime,8,BeginDate),
                                    SQLDatabase.MakeInParam("@EndDate",SqlDbType.DateTime,8,EndDate),
                                    SQLDatabase.MakeInParam("@ApproveFlag",SqlDbType.Int,4,ApproveFlag),
                                };
            SQLDatabase.RunProc(_ProcePrefix + "_GetNoticeByStaff", parms, out dr);

            return FillModelList(dr);
        }
    }
}

