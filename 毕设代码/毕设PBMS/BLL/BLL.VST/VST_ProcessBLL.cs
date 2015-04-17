
// ===================================================================
// 文件： VST_ProcessBLL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   ChingyZhang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.VST;
using MCSFramework.SQLDAL.VST;

namespace MCSFramework.BLL.VST
{
	/// <summary>
	///VST_ProcessDAL业务逻辑BLL类
	/// </summary>
	public class VST_ProcessBLL : BaseSimpleBLL<VST_Process>
	{
        private static string DALClassName = "MCSFramework.SQLDAL.VST.VST_ProcessDAL";
        private VST_ProcessDAL _dal;
		
		#region 构造函数
		///<summary>
        ///VST_ProcessBLL
		///</summary>
		public VST_ProcessBLL()
			: base(DALClassName)
		{
            _dal = (VST_ProcessDAL)_DAL;
            _m = new VST_Process(); 
		}
		
		public VST_ProcessBLL(int id)
            : base(DALClassName)
        {
            _dal = (VST_ProcessDAL)_DAL;
            FillModel(id);
        }

        public VST_ProcessBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (VST_ProcessDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<VST_Process> GetModelList(string condition)
        {
            return new VST_ProcessBLL()._GetModelList(condition);
        }
		#endregion

        public VST_ProcessBLL(string Code)
            : base(DALClassName)
        {
            _dal = (VST_ProcessDAL)_DAL;
            IList<VST_Process> list = VST_ProcessBLL.GetModelList("Code='" + Code + "'");
            if (list.Count > 0) _m = list[0];
        }
	}
}