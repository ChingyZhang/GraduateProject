
// ===================================================================
// 文件： IPT_UploadTemplateMessageDAL.cs
// 项目名称：
// 创建时间：2015/3/17
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.IPT;
using MCSFramework.SQLDAL.IPT;

namespace MCSFramework.BLL.IPT
{
	/// <summary>
	///IPT_UploadTemplateMessageBLL业务逻辑BLL类
	/// </summary>
	public class IPT_UploadTemplateMessageBLL : BaseSimpleBLL<IPT_UploadTemplateMessage>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.IPT.IPT_UploadTemplateMessageDAL";
        private IPT_UploadTemplateMessageDAL _dal;
		
		#region 构造函数
		///<summary>
		///IPT_UploadTemplateMessageBLL
		///</summary>
		public IPT_UploadTemplateMessageBLL()
			: base(DALClassName)
		{
			_dal = (IPT_UploadTemplateMessageDAL)_DAL;
            _m = new IPT_UploadTemplateMessage(); 
		}
		
		public IPT_UploadTemplateMessageBLL(int id)
            : base(DALClassName)
        {
            _dal = (IPT_UploadTemplateMessageDAL)_DAL;
            FillModel(id);
        }

        public IPT_UploadTemplateMessageBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (IPT_UploadTemplateMessageDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<IPT_UploadTemplateMessage> GetModelList(string condition)
        {
            return new IPT_UploadTemplateMessageBLL()._GetModelList(condition);
        }
		#endregion
	}
}