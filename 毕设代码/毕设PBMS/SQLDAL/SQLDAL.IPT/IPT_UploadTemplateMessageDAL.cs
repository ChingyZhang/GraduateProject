
// ===================================================================
// 文件： IPT_UploadTemplateMessageDAL.cs
// 项目名称：
// 创建时间：2015/3/17
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.IPT;


namespace MCSFramework.SQLDAL.IPT
{
	/// <summary>
	///IPT_UploadTemplateMessage数据访问DAL类
	/// </summary>
	public class IPT_UploadTemplateMessageDAL : BaseSimpleDAL<IPT_UploadTemplateMessage>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public IPT_UploadTemplateMessageDAL()
		{
			_ProcePrefix = "MCS_IPT.dbo.sp_IPT_UploadTemplateMessage";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(IPT_UploadTemplateMessage m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TemplateID", SqlDbType.Int, 4, m.TemplateID),
				SQLDatabase.MakeInParam("@MessageType", SqlDbType.Int, 4, m.MessageType),
				SQLDatabase.MakeInParam("@Content", SqlDbType.VarChar, 2000, m.Content),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@DINT01", SqlDbType.Int, 4, m.DINT01),
				SQLDatabase.MakeInParam("@DINT02", SqlDbType.Int, 4, m.DINT02),
				SQLDatabase.MakeInParam("@DINT03", SqlDbType.Int, 4, m.DINT03),
				SQLDatabase.MakeInParam("@DDEC01", SqlDbType.Decimal, 9, m.DDEC01),
				SQLDatabase.MakeInParam("@DDEC02", SqlDbType.Decimal, 9, m.DDEC02),
				SQLDatabase.MakeInParam("@DDEC03", SqlDbType.Decimal, 9, m.DDEC03),
				SQLDatabase.MakeInParam("@DSTR01", SqlDbType.VarChar, 50, m.DSTR01),
				SQLDatabase.MakeInParam("@DSTR02", SqlDbType.VarChar, 50, m.DSTR02),
				SQLDatabase.MakeInParam("@DSTR03", SqlDbType.VarChar, 50, m.DSTR03),
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
        public override int Update(IPT_UploadTemplateMessage m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@TemplateID", SqlDbType.Int, 4, m.TemplateID),
				SQLDatabase.MakeInParam("@MessageType", SqlDbType.Int, 4, m.MessageType),
				SQLDatabase.MakeInParam("@Content", SqlDbType.VarChar, 2000, m.Content),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@DINT01", SqlDbType.Int, 4, m.DINT01),
				SQLDatabase.MakeInParam("@DINT02", SqlDbType.Int, 4, m.DINT02),
				SQLDatabase.MakeInParam("@DINT03", SqlDbType.Int, 4, m.DINT03),
				SQLDatabase.MakeInParam("@DDEC01", SqlDbType.Decimal, 9, m.DDEC01),
				SQLDatabase.MakeInParam("@DDEC02", SqlDbType.Decimal, 9, m.DDEC02),
				SQLDatabase.MakeInParam("@DDEC03", SqlDbType.Decimal, 9, m.DDEC03),
				SQLDatabase.MakeInParam("@DSTR01", SqlDbType.VarChar, 50, m.DSTR01),
				SQLDatabase.MakeInParam("@DSTR02", SqlDbType.VarChar, 50, m.DSTR02),
				SQLDatabase.MakeInParam("@DSTR03", SqlDbType.VarChar, 50, m.DSTR03),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override IPT_UploadTemplateMessage FillModel(IDataReader dr)
		{
			IPT_UploadTemplateMessage m = new IPT_UploadTemplateMessage();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["TemplateID"].ToString()))	m.TemplateID = (int)dr["TemplateID"];
			if (!string.IsNullOrEmpty(dr["MessageType"].ToString()))	m.MessageType = (int)dr["MessageType"];
			if (!string.IsNullOrEmpty(dr["Content"].ToString()))	m.Content = (string)dr["Content"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["DINT01"].ToString()))	m.DINT01 = (int)dr["DINT01"];
			if (!string.IsNullOrEmpty(dr["DINT02"].ToString()))	m.DINT02 = (int)dr["DINT02"];
			if (!string.IsNullOrEmpty(dr["DINT03"].ToString()))	m.DINT03 = (int)dr["DINT03"];
			if (!string.IsNullOrEmpty(dr["DDEC01"].ToString()))	m.DDEC01 = (decimal)dr["DDEC01"];
			if (!string.IsNullOrEmpty(dr["DDEC02"].ToString()))	m.DDEC02 = (decimal)dr["DDEC02"];
			if (!string.IsNullOrEmpty(dr["DDEC03"].ToString()))	m.DDEC03 = (decimal)dr["DDEC03"];
			if (!string.IsNullOrEmpty(dr["DSTR01"].ToString()))	m.DSTR01 = (string)dr["DSTR01"];
			if (!string.IsNullOrEmpty(dr["DSTR02"].ToString()))	m.DSTR02 = (string)dr["DSTR02"];
			if (!string.IsNullOrEmpty(dr["DSTR03"].ToString()))	m.DSTR03 = (string)dr["DSTR03"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

