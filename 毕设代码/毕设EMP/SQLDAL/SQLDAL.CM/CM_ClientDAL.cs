
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
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@OfficalCity", SqlDbType.Int, 4, m.OfficalCity),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, m.Address),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 100, m.TeleNum),
				SQLDatabase.MakeInParam("@Fax", SqlDbType.VarChar, 50, m.Fax),
				SQLDatabase.MakeInParam("@Email", SqlDbType.VarChar, 50, m.Email),
				SQLDatabase.MakeInParam("@URL", SqlDbType.VarChar, 100, m.URL),
				SQLDatabase.MakeInParam("@PostCode", SqlDbType.VarChar, 10, m.PostCode),
				SQLDatabase.MakeInParam("@ChiefLinkMan", SqlDbType.Int, 4, m.ChiefLinkMan),
				SQLDatabase.MakeInParam("@ActiveFlag", SqlDbType.Int, 4, m.ActiveFlag),
				SQLDatabase.MakeInParam("@OpenTime", SqlDbType.DateTime, 8, m.OpenTime),
				SQLDatabase.MakeInParam("@CloseTime", SqlDbType.DateTime, 8, m.CloseTime),
				SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, m.ClientType),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, m.ClientManager),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@OfficalCity", SqlDbType.Int, 4, m.OfficalCity),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, m.Address),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 100, m.TeleNum),
				SQLDatabase.MakeInParam("@Fax", SqlDbType.VarChar, 50, m.Fax),
				SQLDatabase.MakeInParam("@Email", SqlDbType.VarChar, 50, m.Email),
				SQLDatabase.MakeInParam("@URL", SqlDbType.VarChar, 100, m.URL),
				SQLDatabase.MakeInParam("@PostCode", SqlDbType.VarChar, 10, m.PostCode),
				SQLDatabase.MakeInParam("@ChiefLinkMan", SqlDbType.Int, 4, m.ChiefLinkMan),
				SQLDatabase.MakeInParam("@ActiveFlag", SqlDbType.Int, 4, m.ActiveFlag),
				SQLDatabase.MakeInParam("@OpenTime", SqlDbType.DateTime, 8, m.OpenTime),
				SQLDatabase.MakeInParam("@CloseTime", SqlDbType.DateTime, 8, m.CloseTime),
				SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, m.ClientType),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, m.ClientManager),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["OfficalCity"].ToString())) m.OfficalCity = (int)dr["OfficalCity"];
            if (!string.IsNullOrEmpty(dr["Address"].ToString())) m.Address = (string)dr["Address"];
            if (!string.IsNullOrEmpty(dr["TeleNum"].ToString())) m.TeleNum = (string)dr["TeleNum"];
            if (!string.IsNullOrEmpty(dr["Fax"].ToString())) m.Fax = (string)dr["Fax"];
            if (!string.IsNullOrEmpty(dr["Email"].ToString())) m.Email = (string)dr["Email"];
            if (!string.IsNullOrEmpty(dr["URL"].ToString())) m.URL = (string)dr["URL"];
            if (!string.IsNullOrEmpty(dr["PostCode"].ToString())) m.PostCode = (string)dr["PostCode"];
            if (!string.IsNullOrEmpty(dr["ChiefLinkMan"].ToString())) m.ChiefLinkMan = (int)dr["ChiefLinkMan"];
            if (!string.IsNullOrEmpty(dr["ActiveFlag"].ToString())) m.ActiveFlag = (int)dr["ActiveFlag"];
            if (!string.IsNullOrEmpty(dr["OpenTime"].ToString())) m.OpenTime = (DateTime)dr["OpenTime"];
            if (!string.IsNullOrEmpty(dr["CloseTime"].ToString())) m.CloseTime = (DateTime)dr["CloseTime"];
            if (!string.IsNullOrEmpty(dr["ClientType"].ToString())) m.ClientType = (int)dr["ClientType"];
            if (!string.IsNullOrEmpty(dr["Supplier"].ToString())) m.Supplier = (int)dr["Supplier"];
            if (!string.IsNullOrEmpty(dr["ClientManager"].ToString())) m.ClientManager = (int)dr["ClientManager"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
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
        public int ReplaceSupplier(int OrgSupplier, int NewSupplier, int NewSupplier2)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrgSupplier", SqlDbType.Int, 4, OrgSupplier),
				SQLDatabase.MakeInParam("@NewSupplier", SqlDbType.Int, 4, NewSupplier),
                SQLDatabase.MakeInParam("@NewSupplier2", SqlDbType.Int, 4, NewSupplier2)
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

        #region 获取经销商的收货地址信息
        public IList<CM_DIAddressID> GetAddressByClient(int Client)
        {

            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4 ,Client)
              
            };
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetAddressByClient", prams, out dr);
            return FillAddressModelList(dr);
        }

        private IList<CM_DIAddressID> FillAddressModelList(IDataReader dr)
        {
            IList<CM_DIAddressID> list = new List<CM_DIAddressID>();
            while (dr.Read())
            {
                CM_DIAddressID m = new CM_DIAddressID();
                if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
                if (!string.IsNullOrEmpty(dr["AddressID"].ToString())) m.AddressID = (int)dr["AddressID"];
                if (!string.IsNullOrEmpty(dr["Address"].ToString())) m.Address = (string)dr["Address"];
                list.Add(m);
            }
            dr.Close();

            return list;
        }
        #endregion

        public string CheckRealClassifyShowMessage(int ChangeType, int Client)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ChangeType", SqlDbType.Int, 4, ChangeType),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeOutParam("@ReturnMessage",SqlDbType.VarChar,5000)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_CheckRealClassifyShowMessage", prams);
            return prams[2].Value.ToString();

        }

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

        /// <summary>
        /// 与金蝶系统数据对接，获取经销商对账单，让利明细，转账明细
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="clientCode">客户代码</param>
        /// <returns></returns>
        public SqlDataReader GetStatement(string procName, int year, int month, string clientCode)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@year", SqlDbType.Int, 4, year),
                SQLDatabase.MakeInParam("@month", SqlDbType.Int, 4, month),
                SQLDatabase.MakeInParam("@customernumber", SqlDbType.VarChar,50, clientCode),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(procName, prams, out dr);
            return dr;
        }
    }
}

