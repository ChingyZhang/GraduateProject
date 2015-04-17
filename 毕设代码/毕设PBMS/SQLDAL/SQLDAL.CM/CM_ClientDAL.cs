
// ===================================================================
// 文件： CM_ClientDAL.cs
// 项目名称：
// 创建时间：2009/2/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Model;
using MCSFramework.Common;
namespace MCSFramework.SQLDAL.CM
{
    /// <summary>
    ///CM_Client数据访问DAL类
    /// </summary>
    public class CM_ClientDAL : BaseSimpleDAL<CM_Client>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CM_ClientDAL()
        {
            _ProcePrefix = "MCS_CM.dbo.sp_CM_Client";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_Client m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@FullName", SqlDbType.VarChar, 500, m.FullName),
				SQLDatabase.MakeInParam("@ShortName", SqlDbType.VarChar, 500, m.ShortName),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@PostCode", SqlDbType.VarChar, 10, m.PostCode),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, m.Address),
				SQLDatabase.MakeInParam("@DeliveryAddress", SqlDbType.VarChar, 500, m.DeliveryAddress),
				SQLDatabase.MakeInParam("@LinkManName", SqlDbType.VarChar, 50, m.LinkManName),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum),
				SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, m.Mobile),
				SQLDatabase.MakeInParam("@Fax", SqlDbType.VarChar, 50, m.Fax),
				SQLDatabase.MakeInParam("@ChiefLinkMan", SqlDbType.Int, 4, m.ChiefLinkMan),
				SQLDatabase.MakeInParam("@OpenTime", SqlDbType.DateTime, 8, m.OpenTime),
				SQLDatabase.MakeInParam("@CloseTime", SqlDbType.DateTime, 8, m.CloseTime),
				SQLDatabase.MakeInParam("@BusinessLicenseCode", SqlDbType.VarChar, 100, m.BusinessLicenseCode),
				SQLDatabase.MakeInParam("@OrganizationCode", SqlDbType.VarChar, 100, m.OrganizationCode),
				SQLDatabase.MakeInParam("@TaxesCode", SqlDbType.VarChar, 100, m.TaxesCode),
				SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, m.ClientType),
				SQLDatabase.MakeInParam("@OwnerType", SqlDbType.Int, 4, m.OwnerType),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(CM_Client m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@FullName", SqlDbType.VarChar, 500, m.FullName),
				SQLDatabase.MakeInParam("@ShortName", SqlDbType.VarChar, 500, m.ShortName),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@PostCode", SqlDbType.VarChar, 10, m.PostCode),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, m.Address),
				SQLDatabase.MakeInParam("@DeliveryAddress", SqlDbType.VarChar, 500, m.DeliveryAddress),
				SQLDatabase.MakeInParam("@LinkManName", SqlDbType.VarChar, 50, m.LinkManName),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum),
				SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, m.Mobile),
				SQLDatabase.MakeInParam("@Fax", SqlDbType.VarChar, 50, m.Fax),
				SQLDatabase.MakeInParam("@ChiefLinkMan", SqlDbType.Int, 4, m.ChiefLinkMan),
				SQLDatabase.MakeInParam("@OpenTime", SqlDbType.DateTime, 8, m.OpenTime),
				SQLDatabase.MakeInParam("@CloseTime", SqlDbType.DateTime, 8, m.CloseTime),
				SQLDatabase.MakeInParam("@BusinessLicenseCode", SqlDbType.VarChar, 100, m.BusinessLicenseCode),
				SQLDatabase.MakeInParam("@OrganizationCode", SqlDbType.VarChar, 100, m.OrganizationCode),
				SQLDatabase.MakeInParam("@TaxesCode", SqlDbType.VarChar, 100, m.TaxesCode),
				SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, m.ClientType),
				SQLDatabase.MakeInParam("@OwnerType", SqlDbType.Int, 4, m.OwnerType),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CM_Client FillModel(IDataReader dr)
        {
            CM_Client m = new CM_Client();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Code"].ToString())) m.Code = (string)dr["Code"];
            if (!string.IsNullOrEmpty(dr["FullName"].ToString())) m.FullName = (string)dr["FullName"];
            if (!string.IsNullOrEmpty(dr["ShortName"].ToString())) m.ShortName = (string)dr["ShortName"];
            if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString())) m.OfficialCity = (int)dr["OfficialCity"];
            if (!string.IsNullOrEmpty(dr["PostCode"].ToString())) m.PostCode = (string)dr["PostCode"];
            if (!string.IsNullOrEmpty(dr["Address"].ToString())) m.Address = (string)dr["Address"];
            if (!string.IsNullOrEmpty(dr["DeliveryAddress"].ToString())) m.DeliveryAddress = (string)dr["DeliveryAddress"];
            if (!string.IsNullOrEmpty(dr["LinkManName"].ToString())) m.LinkManName = (string)dr["LinkManName"];
            if (!string.IsNullOrEmpty(dr["TeleNum"].ToString())) m.TeleNum = (string)dr["TeleNum"];
            if (!string.IsNullOrEmpty(dr["Mobile"].ToString())) m.Mobile = (string)dr["Mobile"];
            if (!string.IsNullOrEmpty(dr["Fax"].ToString())) m.Fax = (string)dr["Fax"];
            if (!string.IsNullOrEmpty(dr["ChiefLinkMan"].ToString())) m.ChiefLinkMan = (int)dr["ChiefLinkMan"];
            if (!string.IsNullOrEmpty(dr["OpenTime"].ToString())) m.OpenTime = (DateTime)dr["OpenTime"];
            if (!string.IsNullOrEmpty(dr["CloseTime"].ToString())) m.CloseTime = (DateTime)dr["CloseTime"];
            if (!string.IsNullOrEmpty(dr["BusinessLicenseCode"].ToString())) m.BusinessLicenseCode = (string)dr["BusinessLicenseCode"];
            if (!string.IsNullOrEmpty(dr["OrganizationCode"].ToString())) m.OrganizationCode = (string)dr["OrganizationCode"];
            if (!string.IsNullOrEmpty(dr["TaxesCode"].ToString())) m.TaxesCode = (string)dr["TaxesCode"];
            if (!string.IsNullOrEmpty(dr["ClientType"].ToString())) m.ClientType = (int)dr["ClientType"];
            if (!string.IsNullOrEmpty(dr["OwnerType"].ToString())) m.OwnerType = (int)dr["OwnerType"];
            if (!string.IsNullOrEmpty(dr["OwnerClient"].ToString())) m.OwnerClient = (int)dr["OwnerClient"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        /// <summary>
        /// 客户资料查重判断
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="OrgClientID"></param>
        /// <param name="Mobile"></param>
        /// <param name="TeleNum"></param>
        /// <param name="FullName"></param>
        /// <param name="Address"></param>
        /// <returns>大于0，与该客户ID号重复</returns>
        public int CheckRepeat(int Supplier, int OrgClientID, string Mobile, string TeleNum, string FullName, string Address)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@OrgClientID", SqlDbType.Int, 4, OrgClientID),
                SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, Mobile),
                SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, TeleNum),
                SQLDatabase.MakeInParam("@FullName", SqlDbType.VarChar, 500, FullName),
                SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, Address)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_CheckRepeat", prams);
        }

        /// <summary>
        /// 根据员工获取管辖的门店列表
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public IList<CM_Client> GetRetailerListBySalesMan(int Supplier, int Salesman)
        {
            SqlDataReader dr = null;

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@Salesman", SqlDbType.Int, 4, Salesman)
                                   };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetRetailerListBySalesMan", prams, out dr);

            return FillModelList(dr);
        }
        /// <summary>
        /// 审核商业客户
        /// </summary>
        /// <param name="ClientID"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public int Approve(int ClientID, int State)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, ClientID),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }

        /// <summary>
        /// 批量替换客户的客户经理
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="hospitalstring"></param>
        /// <returns></returns>
        public int ReplaceClientManager(string ClientIDs, int NewClientManager, int ClientType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ClientIDs", SqlDbType.VarChar, 4000, ClientIDs),
                SQLDatabase.MakeInParam("@NewClientManager", SqlDbType.Int, 4, NewClientManager),
		        SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, ClientType)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_ReplaceClientManagerByClientIDs", prams);
        }
        /// <summary>
        /// 批量替换客户的供货商
        /// </summary>
        /// <param name="OrgSupplier"></param>
        /// <param name="NewSupplier"></param>
        /// <returns></returns>
        public int ReplaceSupplier(int OrgSupplier, int NewSupplier)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrgSupplier", SqlDbType.Int, 4, OrgSupplier),
				SQLDatabase.MakeInParam("@NewSupplier", SqlDbType.Int, 4, NewSupplier)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_ReplaceSupplier", prams);
        }

        /// <summary>
        /// 批量替换客户的客户经理
        /// </summary>
        /// <param name="OrgClientManager"></param>
        /// <param name="NewClientManager"></param>
        /// <returns></returns>
        public int ReplaceClientManager(int OrgClientManager, int NewClientManager, int ClientType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrgClientManager", SqlDbType.Int, 4, OrgClientManager),
				SQLDatabase.MakeInParam("@NewClientManager", SqlDbType.Int, 4, NewClientManager),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, ClientType),
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_ReplaceClientManager", prams);
        }

        /// <summary>
        /// 子户头切换为主户头
        /// </summary>
        /// <param name="SubACClientID"></param>
        /// <returns></returns>
        public int DISubACUpgrade(int SubACClientID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, SubACClientID)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_DISubACUpgrade", prams);
        }

        #region 物业常住人员
        public int StaffInProperty_Add(int Client, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4 ,Client),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4 ,Staff),
            };
            #endregion
            return SQLDatabase.RunProc("MCS_CM.dbo.sp_CM_StaffInProperty_Add", prams);
        }

        public int StaffInProperty_Delete(int Client, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4 ,Client),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4 ,Staff),
            };
            #endregion
            return SQLDatabase.RunProc("MCS_CM.dbo.sp_CM_StaffInProperty_Delete", prams);
        }
        #endregion

        #region 获取客户销量信息
        /// <summary>
        /// 获取指定客户指定月份的预计销量
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public decimal GetSalesForcast(int Client, int AccountMonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                new SqlParameter("@TotalVolume", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"TotalVolume", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSalesForcast", prams);

            if (prams[2].Value != DBNull.Value)
                return (decimal)prams[2].Value;
            else
                return 0;
        }

        /// <summary>
        /// 获取指定客户指定月份的实际销量
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public decimal GetSalesVolume(int Client, int AccountMonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                new SqlParameter("@TotalVolume", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"TotalVolume", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSalesVolume", prams);

            if (prams[2].Value != DBNull.Value)
                return (decimal)prams[2].Value;
            else
                return 0;
        }

        /// <summary>
        /// 获取指定客户历史平均实际销量
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="MonthCount"></param>
        /// <returns></returns>
        public decimal GetSalesVolumeAvg(int Client, int EndMonth, int MonthCount)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth),
                SQLDatabase.MakeInParam("@MonthCount", SqlDbType.Int, 4, MonthCount),
                new SqlParameter("@AvgVolume", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"AvgVolume", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSalesVolumeAvg", prams);

            if (prams[3].Value != DBNull.Value)
                return (decimal)prams[3].Value;
            else
                return 0;
        }

        /// <summary>
        /// 获取门店指定品牌的销量占比
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="Brand"></param>
        /// <returns></returns>
        public decimal GetBrandSalesVolumeRate(int Client, int AccountMonth, int Brand)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, Brand),
                new SqlParameter("@Rate", SqlDbType.Decimal,9, ParameterDirection.Output,false,10,3,"Rate", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetBrandSalesVolumeRate", prams);

            if (prams[3].Value != DBNull.Value)
                return (decimal)prams[3].Value;
            else
                return 0;
        }
        #endregion

        #region 经销商覆盖片区
        public int ClientInOrganizeCity_Add(int Client, int Organizecity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4 ,Client),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,Organizecity),
            };
            #endregion
            return SQLDatabase.RunProc("MCS_CM.dbo.sp_CM_ClientInOrganizeCity_Add", prams);
        }

        public int ClientInOrganizeCity_Delete(int Client, int Organizecity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4 ,Client),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,Organizecity),
            };
            #endregion
            return SQLDatabase.RunProc("MCS_CM.dbo.sp_CM_ClientInOrganizeCity_Delete", prams);
        }

        public IList<Addr_OrganizeCity> ClientInOrganizeCity_GetOrganizeCitys(int Client)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4 ,Client)
              
            };
            #endregion
            SQLDatabase.RunProc("MCS_CM.dbo.sp_CM_ClientInOrganizeCity_GetByClient", prams, out dr);
            return new Addr_OrganizeCityDAL().FillModelList(dr);
        }
        #endregion

        #region 统计各类活跃状态门店数量
        public DataTable GetClientCountByActiveFlag(int OrganizeCity, int Supplier, int ClientType)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,OrganizeCity),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4 ,Supplier),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4 ,ClientType)
            };
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetClientCountByActiveFlag", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion
    }
}

