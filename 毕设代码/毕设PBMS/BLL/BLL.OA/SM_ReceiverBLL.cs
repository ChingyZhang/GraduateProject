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
    ///SM_ReceiverBLL业务逻辑BLL类
    /// </summary>
    public class SM_ReceiverBLL : BaseSimpleBLL<SM_Receiver>
    {

        private static string DALClassName = "MCSFramework.SQLDAL.OA.SM_ReceiverDAL";
        private SM_ReceiverDAL _dal;

        #region 构造函数
        /// <summary>
        /// SM_ReceiverBLL构造函数
        /// </summary>
        public SM_ReceiverBLL()
            : base(DALClassName)
        {
            _dal = (SM_ReceiverDAL)_DAL;
            _m = new SM_Receiver();
        }

        public SM_ReceiverBLL(int id)
            : base(DALClassName)
        {
            _dal = (SM_ReceiverDAL)_DAL;
            FillModel(id);
        }

        public SM_ReceiverBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SM_ReceiverDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public static DataTable GetMyMsg(string receiver)
        {
            SM_ReceiverDAL dal = (SM_ReceiverDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetMyMsg(receiver);
        }

        public static void DeleteAll(string receiver)
        {
            SM_ReceiverDAL dal = (SM_ReceiverDAL)DataAccess.CreateObject(DALClassName);
            dal.ReceiverDeleteAll(receiver);
        }

        /// <summary>
        /// 通过ID和receive设置已读
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receiver"></param>
        public static void IsRead(int MsgID, string receiver)
        {
            SM_ReceiverDAL dal = (SM_ReceiverDAL)DataAccess.CreateObject(DALClassName);
            dal.ReceiverIsRead(MsgID, receiver);
        }

        /// <summary>
        /// 是否有新信息
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public static int HasNewMsg(string receiver)
        {
            SM_ReceiverDAL dal = (SM_ReceiverDAL)DataAccess.CreateObject(DALClassName);
            return dal.HasNewMsg(receiver);
        }

        /// <summary>
        /// 通过当前ID获取下个未读ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public static int GetNextID(int id, string receiver)
        {
            SM_ReceiverDAL dal = (SM_ReceiverDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetNextID(id, receiver);
        }
    }
}
