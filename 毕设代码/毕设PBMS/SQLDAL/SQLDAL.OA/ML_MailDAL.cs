// ===================================================================
// 文件： ML_MailDAL.cs
// 项目名称：
// 创建时间：2009/2/19
// 作者:	  chen li
// ===================================================================
using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;
using MCSFramework.Model.OA;
using System.Data;
using System.Data.SqlClient;

namespace MCSFramework.SQLDAL.OA
{
    /// <summary>
    /// ML_Mail数据访问DAL类
    /// </summary>
    public class ML_MailDAL: BaseSimpleDAL<ML_Mail>
    {
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public ML_MailDAL()
        {
            _ProcePrefix = "MCS_OA.dbo.sp_ML_Mail";
        }
        #endregion

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ML_Mail m)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@MailType",SqlDbType.Int,4,m.MailType),
                 SQLDatabase.MakeInParam("@ReceiverStr",SqlDbType.VarChar,500,m.ReceiverStr),
                 //日期用getdate() 自动插入
                // SQLDatabase.MakeInParam("@SendTime",SqlDbType.DateTime,8,m.SendTime), 
                 SQLDatabase.MakeInParam("@Sender",SqlDbType.VarChar,10,m.Sender),
                 SQLDatabase.MakeInParam("@SendTags",SqlDbType.Int,4,m.SendTags),               
                 SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,m.Receiver),
                 SQLDatabase.MakeInParam("@Subject",SqlDbType.VarChar,100,m.Subject),
                 SQLDatabase.MakeInParam("@Content",SqlDbType.Text,5000,m.Content),
                 SQLDatabase.MakeInParam("@CcToAddr",SqlDbType.VarChar,500,m.CcToAddr),
                 SQLDatabase.MakeInParam("@BccToAddr",SqlDbType.VarChar,500,m.BccToAddr),
                 SQLDatabase.MakeInParam("@IsRead",SqlDbType.Char,1,m.IsRead),
                 SQLDatabase.MakeInParam("@IsDelete",SqlDbType.Char,1,m.IsDelete),
                 SQLDatabase.MakeInParam("@Type",SqlDbType.Int,4,m.Type),
                 SQLDatabase.MakeInParam("@Size",SqlDbType.Int,4,m.Size),
                 SQLDatabase.MakeInParam("@Importance",SqlDbType.Int,4,m.Importance),
                 SQLDatabase.MakeInParam("@Folder",SqlDbType.Int,4,m.Folder),
                 SQLDatabase.MakeInParam("@Extpropertys",SqlDbType.VarChar,8000,m.ExtPropertys)
            };
            #endregion
            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_ADD", prams);
            return m.ID;
        }

        public override int Update(ML_Mail m)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,m.ID),
                 SQLDatabase.MakeInParam("@MailType",SqlDbType.Int,4,m.MailType),
                 SQLDatabase.MakeInParam("@ReceiverStr",SqlDbType.VarChar,500,m.ReceiverStr),
                 SQLDatabase.MakeInParam("@SendTime",SqlDbType.DateTime,8,m.SendTime), 
                 SQLDatabase.MakeInParam("@Sender",SqlDbType.VarChar,10,m.Sender),
                 SQLDatabase.MakeInParam("@SendTags",SqlDbType.Int,4,m.SendTags),               
                 SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,m.Receiver),
                 SQLDatabase.MakeInParam("@Subject",SqlDbType.VarChar,100,m.Subject),
                 SQLDatabase.MakeInParam("@Content",SqlDbType.Text,5000,m.Content),
                 SQLDatabase.MakeInParam("@CcToAddr",SqlDbType.VarChar,500,m.CcToAddr),
                 SQLDatabase.MakeInParam("@MBccToAddr",SqlDbType.VarChar,500,m.BccToAddr),
                 SQLDatabase.MakeInParam("@IsRead",SqlDbType.Char,1,m.IsRead),
                 SQLDatabase.MakeInParam("@IsDelete",SqlDbType.Char,1,m.IsDelete),
                 SQLDatabase.MakeInParam("@Type",SqlDbType.Int,4,m.Type),
                 SQLDatabase.MakeInParam("@Size",SqlDbType.Int,4,m.Size),
                 SQLDatabase.MakeInParam("@Importance",SqlDbType.Int,4,m.Importance),
                 SQLDatabase.MakeInParam("@Folder",SqlDbType.Int,4,m.Folder),
                 SQLDatabase.MakeInParam("@Extpropertys",SqlDbType.VarChar,8000,m.ExtPropertys)
            };
            #endregion
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
            return ret;
        }

        protected override ML_Mail FillModel(IDataReader dr)
        {
            ML_Mail m = new ML_Mail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID= (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["MailType"].ToString())) m.MailType = (int)dr["MailType"];
            if (!string.IsNullOrEmpty(dr["ReceiverStr"].ToString())) m.ReceiverStr = (string)dr["ReceiverStr"];
            if (!string.IsNullOrEmpty(dr["SendTime"].ToString())) m.SendTime = (DateTime)dr["SendTime"];
            if (!string.IsNullOrEmpty(dr["Sender"].ToString())) m.Sender = (string)dr["Sender"];
            if (!string.IsNullOrEmpty(dr["SendTags"].ToString())) m.SendTags = (int)dr["SendTags"];          
            if (!string.IsNullOrEmpty(dr["Receiver"].ToString())) m.Receiver = (string)dr["Receiver"];
            if (!string.IsNullOrEmpty(dr["Subject"].ToString())) m.Subject = (string)dr["Subject"];
            if (!string.IsNullOrEmpty(dr["Content"].ToString())) m.Content = (string)dr["Content"];
            if (!string.IsNullOrEmpty(dr["CcToAddr"].ToString())) m.CcToAddr = (string)dr["CcToAddr"];
            if (!string.IsNullOrEmpty(dr["BccToAddr"].ToString())) m.BccToAddr = (string)dr["BccToAddr"];
            if (!string.IsNullOrEmpty(dr["IsRead"].ToString())) m.IsRead = (string)dr["IsRead"];
            if (!string.IsNullOrEmpty(dr["IsDelete"].ToString())) m.IsDelete = (string)dr["IsDelete"];
            if (!string.IsNullOrEmpty(dr["Type"].ToString())) m.Type = (int)dr["Type"];
            if (!string.IsNullOrEmpty(dr["Size"].ToString())) m.Size = (int)dr["Size"];
            if (!string.IsNullOrEmpty(dr["Importance"].ToString())) m.Importance = (int)dr["Importance"];
            if (!string.IsNullOrEmpty(dr["Folder"].ToString())) m.Folder = (int)dr["Folder"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = (string)dr["ExtPropertys"];
            return m;
        }

        #region 根据ID改变邮箱类型
        public int UpdateMailFolder(int id, int folder)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,id),
                 SQLDatabase.MakeInParam("@Folder",SqlDbType.Int,4,folder)
            };
            #endregion
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateMailFolder", prams);
            return ret;
        }
        #endregion

        #region 根据ID改变邮件IsDelete属性
        public int UpdateIsdeleteByID(int id)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,id),
            };
            #endregion
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateIsDeleteByID", prams);
            return ret;
        }
        #endregion

        #region 根据ID改变邮件IsRead属性
        public int UpdateIsReadByID(int id)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,id),
            };
            #endregion
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateIsReadByID", prams);
            return ret;
        }
        #endregion

        #region 根据Receiver获得未读邮件数
        public int GetNewMailCountByReceiver(string receiver)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,receiver),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetNewMailCountByReceiver", prams,out dr);
            int ret = 0;
            if (dr.Read())
                ret = (int)dr[0];

            dr.Close();
            return ret;
        }
        #endregion
    }
}
