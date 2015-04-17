using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.OA
{
    public class SM_MsgDAL : BaseSimpleDAL<SM_Msg>
    {
        /// <summary>
        ///OA_SMDAL数据访问DAL类
        /// </summary>
        public SM_MsgDAL()
        {
            _ProcePrefix = "MCS_OA.dbo.sp_SM_Msg";
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SM_Msg m)
        {
            SqlParameter[] parms1 ={
              //SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,m.ID),
              SQLDatabase.MakeInParam("@Sender",SqlDbType.VarChar,50,m.Sender),
              SQLDatabase.MakeInParam("@Content",SqlDbType.NVarChar,2000,m.Content),
              SQLDatabase.MakeInParam("@SendTime",SqlDbType.DateTime,8,m.SendTime),
              SQLDatabase.MakeInParam("@Type",SqlDbType.Int,4,m.Type),
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
        public override int Update(SM_Msg m)
        {
            SqlParameter[] parms ={
              SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,m.ID),
              SQLDatabase.MakeInParam("@Sender",SqlDbType.VarChar,50,m.Sender),
              SQLDatabase.MakeInParam("@Content",SqlDbType.NVarChar,2000,m.Content),
              SQLDatabase.MakeInParam("@SendTime",SqlDbType.DateTime,8,m.SendTime),
              SQLDatabase.MakeInParam("@Type",SqlDbType.Int,4,m.Type),
              SQLDatabase.MakeInParam("@IsDelete",SqlDbType.Char,1,m.IsDelete),
              SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
            };
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", parms);
            return ret;
        }

        protected override SM_Msg FillModel(IDataReader dr)
        {
            SM_Msg _m = new SM_Msg();

            if (!string.IsNullOrEmpty(dr["ID"].ToString())) _m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Sender"].ToString())) _m.Sender = (string)dr["Sender"];
            if (!string.IsNullOrEmpty(dr["Content"].ToString())) _m.Content = (string)dr["Content"];
            if (!string.IsNullOrEmpty(dr["SendTime"].ToString())) _m.SendTime = (DateTime)dr["SendTime"];
            if (!string.IsNullOrEmpty(dr["Type"].ToString())) _m.Type = (int)dr["Type"];
            if (!string.IsNullOrEmpty(dr["IsDelete"].ToString())) _m.IsDelete = (string)dr["IsDelete"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) _m.ExtPropertys = SpiltExtProperty(_m.ModelName, (string)dr["ExtPropertys"]);

            return _m;
        }

        /// <summary>
        /// 删除所有已发送的信息
        /// </summary>
        public void DeleteAll(string sender)
        {
            SqlParameter[] parms = {
              SQLDatabase.MakeInParam("@Sender",SqlDbType.VarChar,50,sender)       
            };
            SQLDatabase.RunProc(_ProcePrefix + "_DeleteAll", parms);
        }

        /// <summary>
        /// 过去当前用户已发信息
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public DataTable GetSendMsg(string sender)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms ={
              SQLDatabase.MakeInParam("@Sender",SqlDbType.VarChar,50,sender)                   
            };
            SQLDatabase.RunProc(_ProcePrefix + "_GetSendMsg", parms, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}
