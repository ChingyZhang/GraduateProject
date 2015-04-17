
// ===================================================================
// 文件： ORD_PublishFaceToClientDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;


namespace MCSFramework.SQLDAL.EBM
{
	/// <summary>
	///ORD_PublishFaceToClient数据访问DAL类
	/// </summary>
	public class ORD_PublishFaceToClientDAL : BaseSimpleDAL<ORD_PublishFaceToClient>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_PublishFaceToClientDAL()
		{
			_ProcePrefix = "MCS_EBM.dbo.sp_ORD_PublishFaceToClient";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_PublishFaceToClient m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, m.PublishID),
				SQLDatabase.MakeInParam("@FaceToClient", SqlDbType.Int, 4, m.FaceToClient),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(ORD_PublishFaceToClient m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, m.PublishID),
				SQLDatabase.MakeInParam("@FaceToClient", SqlDbType.Int, 4, m.FaceToClient),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override ORD_PublishFaceToClient FillModel(IDataReader dr)
		{
			ORD_PublishFaceToClient m = new ORD_PublishFaceToClient();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["PublishID"].ToString()))	m.PublishID = (int)dr["PublishID"];
			if (!string.IsNullOrEmpty(dr["FaceToClient"].ToString()))	m.FaceToClient = (int)dr["FaceToClient"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

