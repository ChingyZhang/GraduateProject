
// ===================================================================
// 文件： BBS_ForumAttachmentDAL.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.OA
{
	/// <summary>
	///BBS_ForumAttachmentBLL业务逻辑BLL类
	/// </summary>
	public class BBS_ForumAttachmentBLL : BaseSimpleBLL<BBS_ForumAttachment>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.BBS_ForumAttachmentDAL";
        private static string PUBDALClassName = "MCSFramework.SQLDAL.Pub.ATMT_AttachmentDAL";
        private BBS_ForumAttachmentDAL _dal;
		
		#region 构造函数
		///<summary>
		///BBS_ForumAttachmentBLL
		///</summary>
		public BBS_ForumAttachmentBLL()
			: base(DALClassName)
		{
			_dal = (BBS_ForumAttachmentDAL)_DAL;
            _m = new BBS_ForumAttachment(); 
		}
		
		public BBS_ForumAttachmentBLL(int id)
            : base(DALClassName)
        {
            _dal = (BBS_ForumAttachmentDAL)_DAL;
            FillModel(id);
        }

        public BBS_ForumAttachmentBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (BBS_ForumAttachmentDAL)_DAL;
            FillModel(id, bycache);
        }

        public BBS_ForumAttachmentBLL(Guid guid)
            : base(DALClassName)
        {
            _dal = (BBS_ForumAttachmentDAL)_DAL;
            _m = _dal.GetModelGUID(guid);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<BBS_ForumAttachment> GetModelList(string condition)
        {
            return new BBS_ForumAttachmentBLL()._GetModelList(condition);
        }
		#endregion

        public static int AddAttachFile(BBS_ForumAttachment att)
        {
            BBS_ForumAttachmentDAL dal = (BBS_ForumAttachmentDAL)DataAccess.CreateObject(DALClassName);
            return dal.Add(att);
        }

        public static int AddAttachFile(BBS_ForumAttachment att,byte[] buff)
        {
            BBS_ForumAttachmentBLL bll = new BBS_ForumAttachmentBLL();
            bll.Model = att;

            return bll.Add(buff);
        }

        #region 附件存至数据库
        /// <summary>
        /// 上传文件附件
        /// </summary>
        /// <param name="filedata"></param>
        /// <returns></returns>
        public int UploadFileData(byte[] filedata)
        {
            ATMT_AttachmentDAL dal = (ATMT_AttachmentDAL)DataAccess.CreateObject(PUBDALClassName);
            return dal.UploadData(_m.GUID, filedata);
        }

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            ATMT_AttachmentDAL dal = (ATMT_AttachmentDAL)DataAccess.CreateObject(PUBDALClassName);
            return dal.GetData(_m.GUID);
        }

        public int Add(byte[] filedata)
        {
            int id = base.Add();
            if (id > 0)
            {
                _m = _dal.GetModel(id);
                UploadFileData(filedata);
            }

            return id;
        }

        public override int Delete()
        {
            ATMT_AttachmentDAL dal = (ATMT_AttachmentDAL)DataAccess.CreateObject(PUBDALClassName);
            dal.DeleteData(_m.GUID);
            return base.Delete();
        }
        #endregion
	}
}