
// ===================================================================
// 文件： ORD_PublishDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.EBM;
using MCSFramework.SQLDAL.EBM;

namespace MCSFramework.BLL.EBM
{
    /// <summary>
    ///ORD_PublishBLL业务逻辑BLL类
    /// </summary>
    public class ORD_PublishBLL : BaseComplexBLL<ORD_Publish, ORD_PublishDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.EBM.ORD_PublishDAL";
        private ORD_PublishDAL _dal;
        private IList<ORD_PublishFaceToClient> _faceToClients;

        /// <summary>
        /// 面向客户
        /// </summary>
        public IList<ORD_PublishFaceToClient> FaceToClients
        {
            get
            {
                if (_faceToClients == null)
                    _faceToClients = GetFaceToClients();

                return _faceToClients;
            }
        }

        #region 构造函数
        ///<summary>
        ///ORD_PublishBLL
        ///</summary>
        public ORD_PublishBLL()
            : base(DALClassName)
        {
            _dal = (ORD_PublishDAL)_DAL;
            _m = new ORD_Publish();
        }

        public ORD_PublishBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_PublishDAL)_DAL;
            FillModel(id);
        }

        public ORD_PublishBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_PublishDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_Publish> GetModelList(string condition)
        {
            return new ORD_PublishBLL()._GetModelList(condition);
        }
        #endregion

        #region 面向客户相关操作
        /// <summary>
        /// 获取发布面向客户
        /// </summary>
        /// <returns></returns>
        private IList<ORD_PublishFaceToClient> GetFaceToClients()
        {
            ORD_PublishFaceToClientDAL dal = (ORD_PublishFaceToClientDAL)DataAccess.CreateObject("MCSFramework.SQLDAL.EBM.ORD_PublishFaceToClientDAL");

            return dal.GetModelList("PublishID=" + _m.ID.ToString());
        }

        public int AddFaceToClient(ORD_PublishFaceToClient m)
        {
            ORD_PublishFaceToClientDAL dal = (ORD_PublishFaceToClientDAL)DataAccess.CreateObject("MCSFramework.SQLDAL.EBM.ORD_PublishFaceToClientDAL");

            return dal.Add(m);
        }

        public int UpdateFaceToClient(ORD_PublishFaceToClient m)
        {
            ORD_PublishFaceToClientDAL dal = (ORD_PublishFaceToClientDAL)DataAccess.CreateObject("MCSFramework.SQLDAL.EBM.ORD_PublishFaceToClientDAL");

            return dal.Update(m);
        }

        public int DeleteFaceToClient(int id)
        {
            ORD_PublishFaceToClientDAL dal = (ORD_PublishFaceToClientDAL)DataAccess.CreateObject("MCSFramework.SQLDAL.EBM.ORD_PublishFaceToClientDAL");

            return dal.Delete(id);
        }
        #endregion

        #region 设置上架发布状态
        public int ConfirmPublish(Guid User)
        {
            return _dal.SetState(_m.ID, 2, User);
        }

        public int UnConfirmPublish(Guid User)
        {
            return _dal.SetState(_m.ID, 1, User);
        }

        public int ClosePublish(Guid User)
        {
            return _dal.SetState(_m.ID, 3, User);
        }

        public int CancelPublish(Guid User)
        {
            return _dal.SetState(_m.ID, 8, User);
        }
        #endregion

        #region 获取发布目录
        public static IList<ORD_Publish> GetPublishListByClient(int Client, int Supplier, int Type)
        {
            ORD_PublishDAL dal = (ORD_PublishDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPublishListByClient(Client, Supplier, Type);
        }

        /// <summary>
        /// 获取面向指定订货商的上架发布目录ID
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static int GetPublishIDByClient(int Client, int Type)
        {
            ORD_PublishDAL dal = (ORD_PublishDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPublishIDByClient(Client, 0, Type);
        }
        /// <summary>
        /// 获取指定供货商的上架发布目录ID
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static int GetPublishIDBySupplier(int Supplier, int Type)
        {
            ORD_PublishDAL dal = (ORD_PublishDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPublishIDByClient(0, Supplier, Type);
        }
        #endregion

        /// <summary>
        /// 获取某客户的指定产品的请购价格
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static decimal GetPublishPriceByClientAndProduct(int Client, int Product)
        {
            ORD_PublishDAL dal = (ORD_PublishDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPublishPriceByClientAndProduct(Client, Product);
        }

        #region 设置可使用的账户类型
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int AddAccountType(int AccountType)
        {
            return _dal.AddAccountType(_m.ID, AccountType);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int RemoveAccountType(int AccountType)
        {
            return _dal.RemoveAccountType(_m.ID, AccountType);
        }

        /// <summary>
        /// 查询发布目录的可用账户类型
        /// </summary>
        /// <param name="PublishID"></param>
        /// <returns></returns>
        public IList<ORD_PublishWithAccountType> GetAccountType()
        {
            return _dal.GetAccountType(_m.ID);
        }
        #endregion

        #region 获取指定发布目录、指定产品已订货数量
        public static int GetSubmitQuantity(int PublishID, int Product)
        {
            ORD_PublishDAL dal = (ORD_PublishDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSubmitQuantity(PublishID, Product);
        }
        #endregion
    }
}