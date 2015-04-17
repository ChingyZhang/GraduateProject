
// ===================================================================
// 文件： ATMT_AttachmentDAL.cs
// 项目名称：
// 创建时间：2008-12-26
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.Pub
{
    /// <summary>
    ///ATMT_AttachmentBLL业务逻辑BLL类
    /// </summary>
    public class ATMT_AttachmentBLL : BaseSimpleBLL<ATMT_Attachment>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.ATMT_AttachmentDAL";
        private ATMT_AttachmentDAL _dal;

        #region 构造函数
        ///<summary>
        ///ATMT_AttachmentBLL
        ///</summary>
        public ATMT_AttachmentBLL()
            : base(DALClassName)
        {
            _dal = (ATMT_AttachmentDAL)_DAL;
            _m = new ATMT_Attachment();
        }

        public ATMT_AttachmentBLL(int id)
            : base(DALClassName)
        {
            _dal = (ATMT_AttachmentDAL)_DAL;
            FillModel(id);
        }

        public ATMT_AttachmentBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ATMT_AttachmentDAL)_DAL;
            FillModel(id, bycache);
        }

        public ATMT_AttachmentBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (ATMT_AttachmentDAL)_DAL;
            _m = _dal.GetModelGUID(id);
        }
        #endregion

        /// <summary>
        /// 获取指定条件的附件列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IList<ATMT_Attachment> GetModelList(string condition)
        {
            ATMT_AttachmentBLL bll = new ATMT_AttachmentBLL();
            return bll._GetModelList(condition);
        }

        /// <summary>
        /// 获取指定附件类型与指定对象关联的附件列表
        /// </summary>
        /// <param name="RelateType"></param>
        /// <param name="RelateID"></param>
        /// <returns></returns>
        public static IList<ATMT_Attachment> GetAttachmentList(int RelateType, int RelateID, DateTime Begintime, DateTime Endtime)
        {
            return GetAttachmentList(RelateType, RelateID, Begintime, Endtime, "");
        }

        public static IList<ATMT_Attachment> GetAttachmentList(int RelateType, int RelateID, DateTime Begintime, DateTime Endtime, string extcondition)
        {
            string condition = " RelateType=" + RelateType.ToString() + " AND RelateID=" + RelateID.ToString() + " AND UploadTime Between '" + Begintime.ToString("yyyy-MM-dd") + "' AND '" + Endtime.ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(extcondition))
            {
                condition += " AND " + extcondition;
            }
            return GetModelList(condition);
        }

        /// <summary>
        /// 获取指定附件类型与指定关联对象的首要图片
        /// </summary>
        /// <param name="RelateType"></param>
        /// <param name="RelateID"></param>
        /// <returns></returns>
        public static string GetFirstPicture(int RelateType, int RelateID)
        {
            string condition = " RelateType=" + RelateType.ToString() + " AND RelateID=" + RelateID.ToString() + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',1)='Y'";

            IList<ATMT_Attachment> lists = GetModelList(condition);

            if (lists.Count > 0)
            {
                ATMT_Attachment m = lists[0];

                if (IsImage(m.ExtName))
                    return "~/SubModule/DownloadAttachment.aspx?GUID=" + m.GUID.ToString();
                else
                    return "AA";
            }
            else
                return "";
        }

        /// <summary>
        /// 获取指定附件类型与指定关联对象的首要图片的缩略图路径
        /// </summary>
        /// <param name="RelateType"></param>
        /// <param name="RelateID"></param>
        /// <returns></returns>
        public static string GetFirstPreviewPicture(int RelateType, int RelateID)
        {
            string condition = " RelateType=" + RelateType.ToString() + " AND RelateID=" + RelateID.ToString() + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',1)='Y'";

            IList<ATMT_Attachment> lists = GetModelList(condition);

            if (lists.Count > 0)
            {
                ATMT_Attachment m = lists[0];

                if (IsImage(m.ExtName))
                    return "~/SubModule/DownloadAttachment.aspx?GUID=" + m.GUID.ToString() + "&PreViewImage=Y";
            }
            return "";
        }

        /// <summary>
        /// 根据上传人获取CKEdit控件上已上传文件的日期
        /// </summary>
        /// <param name="UploadUser">上传人</param>
        /// <param name="OnlyImageFlag">是否仅图片</param>
        /// <returns></returns>
        public static List<DateTime> GetCKEditUploadDate(string UploadUser, bool OnlyImageFlag)
        {
            ATMT_AttachmentDAL dal = (ATMT_AttachmentDAL)DataAccess.CreateObject(DALClassName);
            DataTable dt = dal.GetCKEditUploadDate(UploadUser, OnlyImageFlag);

            if (dt == null) return null;

            List<DateTime> list = new List<DateTime>(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add((DateTime)dr["UploadDate"]);
            }

            if (list.IndexOf(DateTime.Today) < 0) list.Add(DateTime.Today);
            return list;
        }

        /// <summary>
        /// 获取指定日期内已上传的CKEdit附件
        /// </summary>
        /// <param name="UploadDate"></param>
        /// <param name="UploadUser"></param>
        /// <param name="OnlyImageFlag"></param>
        /// <returns></returns>
        public static IList<ATMT_Attachment> GetCKEditAttachmentList(DateTime UploadDate, string UploadUser, bool OnlyImageFlag)
        {
            string condition = " RelateType=75 AND CONVERT(DATETIME,CONVERT(VARCHAR,UploadTime,111))='" + UploadDate.ToString("yyyy/MM/dd") +
                "' AND UploadUser = '" + UploadUser + "' AND ISNULL(IsDelete,'N') = 'N'";
            if (OnlyImageFlag) condition += " AND ExtName IN ('jpg','bmp','gif','png')";

            return GetModelList(condition);
        }

        /// <summary>
        /// 判断是否是图片类型
        /// </summary>
        /// <param name="ExtName"></param>
        /// <returns></returns>
        public static bool IsImage(string ExtName)
        {
            switch (ExtName.ToLower())
            {
                case "jpg":
                case "bmp":
                case "gif":
                case "png":
                    return true;
                default:
                    return false;
            }
        }

        #region 附件存至数据库
        /// <summary>
        /// 上传文件附件
        /// </summary>
        /// <param name="filedata"></param>
        /// <returns></returns>
        public int UploadFileData(byte[] filedata)
        {
            return _dal.UploadData(_m.GUID, filedata);
        }

        /// <summary>
        /// 上传图片预览图
        /// </summary>
        /// <param name="thumbnaildata"></param>
        /// <returns></returns>
        public int UploadThumbnailData(byte[] thumbnaildata)
        {
            return _dal.UploadThumbnailData(_m.GUID, thumbnaildata);
        }

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            return _dal.GetData(_m.GUID);
        }

        /// <summary>
        /// 获取预览图
        /// </summary>
        /// <returns></returns>
        public byte[] GetThumbnailData()
        {
            return _dal.GetThumbnailData(_m.GUID);
        }

        public int Add(byte[] filedata)
        {
            Guid guid = Guid.Empty;
            return Add(filedata, out guid);
        }

        public int Add(byte[] filedata, out Guid guid)
        {
            guid = Guid.Empty;

            int id = base.Add();
            if (id > 0)
            {
                _m = _dal.GetModel(id);
                _dal.UploadData(_m.GUID, filedata);
                guid = _m.GUID;
            }

            return id;
        }

        public override int Delete()
        {
            return base.Delete(_m.ID);
        }
        #endregion

    }
}
