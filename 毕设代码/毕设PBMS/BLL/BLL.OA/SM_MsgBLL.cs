using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;

namespace MCSFramework.BLL.OA
{
    /// <summary>
    ///SM_MsgBLL业务逻辑BLL类
    /// </summary>
    public class SM_MsgBLL : BaseSimpleBLL<SM_Msg>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.OA.SM_MsgDAL";
        private SM_MsgDAL _dal;

        #region 构造函数
        /// <summary>
        /// SM_MsgBLL构造函数
        /// </summary>
        public SM_MsgBLL()
            : base(DALClassName)
        {
            _dal = (SM_MsgDAL)_DAL;
            _m = new SM_Msg();
        }

        public SM_MsgBLL(int id)
            : base(DALClassName)
        {
            _dal = (SM_MsgDAL)_DAL;
            FillModel(id);
        }

        public SM_MsgBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SM_MsgDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        /// <summary>
        /// 删除所有已发送的信息
        /// </summary>
        public static void DeleteAll(string sender)
        {
            SM_MsgDAL dal = (SM_MsgDAL)DataAccess.CreateObject(DALClassName);
            dal.DeleteAll(sender);
        }


        /// <summary>
        /// 获取当前用户的已发信息
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DataTable GetSendMsg(string sender)
        {
            SM_MsgDAL dal = (SM_MsgDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSendMsg(sender);
        }
    }

}
