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
    public class ML_AttachFileBLL : BaseSimpleBLL<ML_AttachFile>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.OA.ML_AttachFileDAL";
        private static string PUBDALClassName = "MCSFramework.SQLDAL.Pub.ATMT_AttachmentDAL";
        private ML_AttachFileDAL _dal;
		
		#region 构造函数
		///<summary>
		///ML_AttachFileBLL
		///</summary>
		public ML_AttachFileBLL()
			: base(DALClassName)
		{
            _dal = (ML_AttachFileDAL)_DAL;
            _m = new ML_AttachFile(); 
		}
		
		public ML_AttachFileBLL(int id)
            : base(DALClassName)
        {
            _dal = (ML_AttachFileDAL)_DAL;
            FillModel(id);
        }

        public ML_AttachFileBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ML_AttachFileDAL)_DAL;
            FillModel(id, bycache);
        }
        public ML_AttachFileBLL(Guid guid)
            : base(DALClassName)
        {
            _dal = (ML_AttachFileDAL)_DAL;
            _m = _dal.GetModelGUID(guid);
        }
		#endregion
		
		#region	静态GetModelList方法
        public static IList<ML_AttachFile> GetModelList(string condition)
        {
            return new ML_AttachFileBLL()._GetModelList(condition);
        }
		#endregion

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
