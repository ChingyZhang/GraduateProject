
// ===================================================================
// 文件： SVM_UploadTemplateDAL.cs
// 项目名称：
// 创建时间：2012/6/19
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.SVM;
using MCSFramework.SQLDAL.SVM;

namespace MCSFramework.BLL.SVM
{
	/// <summary>
	///SVM_UploadTemplateBLL业务逻辑BLL类
	/// </summary>
	public class SVM_UploadTemplateBLL : BaseSimpleBLL<SVM_UploadTemplate>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_UploadTemplateDAL";
        private SVM_UploadTemplateDAL _dal;
		
		#region 构造函数
		///<summary>
		///SVM_UploadTemplateBLL
		///</summary>
		public SVM_UploadTemplateBLL()
			: base(DALClassName)
		{
			_dal = (SVM_UploadTemplateDAL)_DAL;
            _m = new SVM_UploadTemplate(); 
		}
		
		public SVM_UploadTemplateBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_UploadTemplateDAL)_DAL;
            FillModel(id);
        }

        public SVM_UploadTemplateBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_UploadTemplateDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<SVM_UploadTemplate> GetModelList(string condition)
        {
            return new SVM_UploadTemplateBLL()._GetModelList(condition);
        }
		#endregion
	}
}