
// ===================================================================
// 文件： User_RegisterMACDAL.cs
// 项目名称：
// 创建时间：2011/11/18
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;

namespace MCSFramework.BLL
{
    /// <summary>
    ///User_RegisterMACBLL业务逻辑BLL类
    /// </summary>
    public class User_RegisterMACBLL : BaseSimpleBLL<User_RegisterMAC>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.User_RegisterMACDAL";
        private User_RegisterMACDAL _dal;

        #region 构造函数
        ///<summary>
        ///User_RegisterMACBLL
        ///</summary>
        public User_RegisterMACBLL()
            : base(DALClassName)
        {
            _dal = (User_RegisterMACDAL)_DAL;
            _m = new User_RegisterMAC();
        }

        public User_RegisterMACBLL(int id)
            : base(DALClassName)
        {
            _dal = (User_RegisterMACDAL)_DAL;
            FillModel(id);
        }

        public User_RegisterMACBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (User_RegisterMACDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<User_RegisterMAC> GetModelList(string condition)
        {
            return new User_RegisterMACBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 根据MAC地址判断，是否可以可以登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="MACAddr">传入：客户端所有MAC地址，传出：客户端当前已在系统中注册的地址</param>
        /// <param name="Flag">0:只有注册在自己名下的MAC才可登录，1:只要在系统中注册的MAC都可以登录</param>
        /// <returns></returns>
        public static bool CanLogin(string UserName, ref string MACAddr, int Flag)
        {
            User_RegisterMACDAL dal = (User_RegisterMACDAL)DataAccess.CreateObject(DALClassName);
            return dal.CanLogin(UserName, ref MACAddr, Flag) == 1;
        }
    }
}