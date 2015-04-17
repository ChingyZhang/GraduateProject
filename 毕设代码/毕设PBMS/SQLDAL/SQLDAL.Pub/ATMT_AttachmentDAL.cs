
// ===================================================================
// 文件： ATMT_AttachmentDAL.cs
// 项目名称：
// 创建时间：2008-12-26
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO.Compression;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;
using System.IO;


namespace MCSFramework.SQLDAL.Pub
{
    /// <summary>
    ///ATMT_Attachment数据访问DAL类
    /// </summary>
    public class ATMT_AttachmentDAL : BaseSimpleDAL<ATMT_Attachment>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ATMT_AttachmentDAL()
        {
            _ConnectionStirng = "MCS_ATMT_ConnectionString";
            _ProcePrefix = "MCS_Pub.dbo.sp_ATMT_Attachment";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ATMT_Attachment m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@RelateType", SqlDbType.Int, 4, m.RelateType),
				SQLDatabase.MakeInParam("@RelateID", SqlDbType.Int, 4, m.RelateID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@Path", SqlDbType.VarChar, 500, m.Path),
				SQLDatabase.MakeInParam("@ExtName", SqlDbType.VarChar, 50, m.ExtName),
                SQLDatabase.MakeInParam("@FileSize", SqlDbType.Int, 4, m.FileSize),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@UploadUser", SqlDbType.VarChar, 50, m.UploadUser),
				SQLDatabase.MakeInParam("@UploadTime", SqlDbType.DateTime, 8, m.UploadTime),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.IsDelete),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(ATMT_Attachment m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@RelateType", SqlDbType.Int, 4, m.RelateType),
				SQLDatabase.MakeInParam("@RelateID", SqlDbType.Int, 4, m.RelateID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@Path", SqlDbType.VarChar, 500, m.Path),
				SQLDatabase.MakeInParam("@ExtName", SqlDbType.VarChar, 50, m.ExtName),
                SQLDatabase.MakeInParam("@FileSize", SqlDbType.Int, 4, m.FileSize),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@UploadUser", SqlDbType.VarChar, 50, m.UploadUser),
				SQLDatabase.MakeInParam("@UploadTime", SqlDbType.DateTime, 8, m.UploadTime),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.IsDelete),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ATMT_Attachment FillModel(IDataReader dr)
        {
            ATMT_Attachment m = new ATMT_Attachment();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["RelateType"].ToString())) m.RelateType = (int)dr["RelateType"];
            if (!string.IsNullOrEmpty(dr["RelateID"].ToString())) m.RelateID = (int)dr["RelateID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["Path"].ToString())) m.Path = (string)dr["Path"];
            if (!string.IsNullOrEmpty(dr["ExtName"].ToString())) m.ExtName = (string)dr["ExtName"];
            if (!string.IsNullOrEmpty(dr["FileSize"].ToString())) m.FileSize = int.Parse(dr["FileSize"].ToString());
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["UploadUser"].ToString())) m.UploadUser = (string)dr["UploadUser"];
            if (!string.IsNullOrEmpty(dr["UploadTime"].ToString())) m.UploadTime = (DateTime)dr["UploadTime"];
            if (!string.IsNullOrEmpty(dr["IsDelete"].ToString())) m.IsDelete = (string)dr["IsDelete"];
            if (!string.IsNullOrEmpty(dr["GUID"].ToString())) m.GUID = new Guid(dr["GUID"].ToString());
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        /// <summary>
        /// 根据GUID获取Model
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ATMT_Attachment GetModelGUID(Guid guid)
        {
            #region	设置参数集
            SqlParameter[] prams = { SQLDatabase.MakeInParam("@GUID", SqlDbType.UniqueIdentifier, 4, guid) };
            #endregion


            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetModelGUID", prams, out dr);

            ATMT_Attachment m = null;
            if (dr.Read()) m = FillModel(dr);
            dr.Close();

            return m;
        }


        #region 附件存至数据库
        public int UploadData(Guid guid, byte[] filedata)
        {
            #region 压缩附件
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(filedata, 0, filedata.Length);
                zip.Close();
                filedata = ms.ToArray();
                ms.Close();
            }
            catch { }
            #endregion

            #region	设置参数集
            SqlParameter[] prams = { 
                SQLDatabase.MakeInParam("@GUID", SqlDbType.UniqueIdentifier, 16, guid),
                SQLDatabase.MakeInParam("@FileData", SqlDbType.Image, filedata.Length, filedata)
                };
            #endregion

            return SQLDatabase.RunProc(_ConnectionStirng, "sp_ATMT_FileData_UploadData", prams);
        }

        public int UploadThumbnailData(Guid guid, byte[] thumbnaildata)
        {
            #region	设置参数集
            SqlParameter[] prams = { 
                SQLDatabase.MakeInParam("@GUID", SqlDbType.UniqueIdentifier, 16, guid),
                SQLDatabase.MakeInParam("@ThumbnailData", SqlDbType.Image, thumbnaildata.Length, thumbnaildata)
                };
            #endregion

            return SQLDatabase.RunProc(_ConnectionStirng, "sp_ATMT_FileData_UploadThumbnailData", prams);
        }

        public int DeleteData(Guid guid)
        {
            #region	设置参数集
            SqlParameter[] prams = { 
                SQLDatabase.MakeInParam("@GUID", SqlDbType.UniqueIdentifier, 16, guid)
                };
            #endregion

            return SQLDatabase.RunProc(_ConnectionStirng, "sp_ATMT_FileData_Delete", prams);
        }

        public byte[] GetData(Guid guid)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = { 
                SQLDatabase.MakeInParam("@GUID", SqlDbType.UniqueIdentifier, 16, guid)
                };
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, "sp_ATMT_FileData_GetModel", prams, out dr);

            byte[] data = null;

            if (dr.HasRows && dr.Read())
            {
                if (!DBNull.Value.Equals(dr["FileData"]))
                {
                    data = (byte[])dr["FileData"];

                    try
                    {
                        #region System.IO.Compression.GZipStream解压缩
                        MemoryStream ms = new MemoryStream(data);
                        //从压缩后的字节流的最后一个字节获取压缩前原始字节数,遵循于压缩规范
                        int orgLength = BitConverter.ToInt32(data, data.Length - 4);
                        const int buffsize = 256;
                        byte[] buffer = new byte[orgLength + buffsize];

                        GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);

                        int offset = 0;
                        int totalCount = 0;
                        while (true)
                        {
                            int bytesRead = zip.Read(buffer, offset, buffsize);
                            if (bytesRead == 0)
                            {
                                break;
                            }
                            offset += bytesRead;
                            totalCount += bytesRead;
                        }

                        data = new byte[totalCount];
                        Buffer.BlockCopy(buffer, 0, data, 0, totalCount);
                        #endregion
                    }
                    catch { }
                }
            }
            dr.Close();

            return data;
        }

        public byte[] GetThumbnailData(Guid guid)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = { 
                SQLDatabase.MakeInParam("@GUID", SqlDbType.UniqueIdentifier, 16, guid)
                };
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, "sp_ATMT_FileData_GetModel", prams, out dr);

            byte[] data = null;

            if (dr.HasRows && dr.Read())
            {
                if (!DBNull.Value.Equals(dr["ThumbnailData"])) data = (byte[])dr["ThumbnailData"];
            }
            dr.Close();

            return data;
        }
        #endregion

        /// <summary>
        /// 根据上传人获取CKEdit控件上已上传文件的日期
        /// </summary>
        /// <param name="UploadUser">上传人</param>
        /// <param name="OnlyImageFlag">是否仅图片</param>
        /// <returns></returns>
        public DataTable GetCKEditUploadDate(string UploadUser, bool OnlyImageFlag)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = { 
                SQLDatabase.MakeInParam("@UploadUser", SqlDbType.VarChar, 50, UploadUser) ,
                SQLDatabase.MakeInParam("@OnlyImageFlag", SqlDbType.Char, 1, OnlyImageFlag?"Y":"N") 
            };
            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetCKeditUploadDate", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

