using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.OA
{
    /// <summary>
    ///OA_ReceiverDAL数据访问DAL类
    /// </summary>
    public class SM_ReceiverDAL : BaseSimpleDAL<SM_Receiver>
    {
        /// <summary>
        /// 
        /// </summary>
        public SM_ReceiverDAL()
        {
            _ProcePrefix = "MCS_OA.dbo.sp_SM_Receiver";
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SM_Receiver m)
        {
            SqlParameter[] parms1 ={
              SQLDatabase.MakeInParam("@MsgID",SqlDbType.Int,4,m.MsgID),
              SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,m.Receiver),
              SQLDatabase.MakeInParam("@MobileNo",SqlDbType.VarChar,50,m.MobileNo),
              //SQLDatabase.MakeInParam("@Type",SqlDbType.Int,4,m.Type),
              SQLDatabase.MakeInParam("@IsRead",SqlDbType.Char,1,m.IsRead),
              SQLDatabase.MakeInParam("@IsDelete",SqlDbType.Char,1,m.IsDelete),
              SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
            };
            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", parms1);
            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(SM_Receiver m)
        {
            SqlParameter[] parms1 ={
              SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,m.ID),
              SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,m.Receiver),
              //SQLDatabase.MakeInParam("@MobileNo",SqlDbType.VarChar,50,m.MobileNo),
              //SQLDatabase.MakeInParam("@Type",SqlDbType.Int,4,m.Type),
              //SQLDatabase.MakeInParam("@IsRead",SqlDbType.Int,4,m.IsRead),
              //SQLDatabase.MakeInParam("@IsDelete",SqlDbType.Char,1,m.IsDelete)
              //SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
            };
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", parms1);
            return ret;
        }

        protected override SM_Receiver FillModel(IDataReader dr)
        {
            SM_Receiver _m = new SM_Receiver();

            if (!string.IsNullOrEmpty(dr["ID"].ToString())) _m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["MsgID"].ToString())) _m.MsgID = (int)dr["MsgID"];
            if (!string.IsNullOrEmpty(dr["Receiver"].ToString())) _m.Receiver = (string)dr["Receiver"];
            if (!string.IsNullOrEmpty(dr["MobileNo"].ToString())) _m.MobileNo = (string)dr["MobileNo"];
            if (!string.IsNullOrEmpty(dr["IsRead"].ToString())) _m.IsRead = (string)dr["IsRead"];
            if (!string.IsNullOrEmpty(dr["IsDelete"].ToString())) _m.IsDelete = (string)dr["IsDelete"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) _m.ExtPropertys = SpiltExtProperty(_m.ModelName, (string)dr["ExtPropertys"]);

            return _m;
        }

        /// <summary>
        /// 拿到当前用户信息
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public DataTable GetMyMsg(string receiver)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms ={
              SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,receiver)
            };
            SQLDatabase.RunProc("MCS_OA.dbo.sp_SM_Msg_GetIMsgByReceiver", parms, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 删除所有接受的信息
        /// </summary>
        public void ReceiverDeleteAll(string receiver)
        {
            SqlParameter[] parms ={
                SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,receiver)
            };
            SQLDatabase.RunProc(_ProcePrefix+"_DeleteAll", parms);
        }

        /// <summary>
        /// 设置为已读
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receiver"></param>
        public void ReceiverIsRead(int MsgID, string receiver)
        {

            SqlParameter[] parms ={
              SQLDatabase.MakeInParam("@MsgID",SqlDbType.Int,4,MsgID),
              SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,receiver)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_IsRead", parms);

        }

        /// <summary>
        /// 是否还有新信息
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public int HasNewMsg(string receiver)
        {

            SqlParameter[] parms = {
               SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,receiver)                    
            };
            return SQLDatabase.RunProc(_ProcePrefix + "_HasNewMsg", parms);

        }

        /// <summary>
        /// 根据当前ID获取下个未读ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public int GetNextID(int id, string receiver)
        {

            SqlParameter[] parms ={
              SQLDatabase.MakeInParam("@ID",SqlDbType.VarChar,50,id),
              SQLDatabase.MakeInParam("@Receiver",SqlDbType.VarChar,50,receiver)
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_GetNextID", parms);
        }
    }
}
