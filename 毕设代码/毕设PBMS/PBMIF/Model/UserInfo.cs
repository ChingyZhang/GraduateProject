using System;
using System.Web.Security;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model;
using System.Collections.Generic;
using MCSFramework.Model.CM;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class UserInfo
    {
        public Guid aspnetUserId = Guid.Empty;
        public string UserName = "";

        /// <summary>
        /// 账户类型 1:员工,2:商业客户,3:导购
        /// </summary>
        public int AccountType = 1;
        public string[] UserInRoles = new string[] { };
        public int StaffID = 0;
        public string StaffName = "";
        public string StaffMobile = "";
        public int StaffPosition = 0;

        public int ClientID = 0;
        public string ClientName = "";

        /// <summary>
        /// 归属类型 1:平台级 2:厂商级 3:经销商级
        /// </summary>
        public int OwnerType = 0;

        /// <summary>
        /// 管理区域
        /// </summary>
        public int OrganizeCity = 0;

        /// <summary>
        /// 客户类型 1:厂商 2:经销商 3：门店
        /// </summary>
        public int ClientType = 0;

        /// <summary>
        /// 登录设备识别号
        /// </summary>
        public string DeviceCode = "";

        public override string ToString()
        {
            if (AccountType == 1)
                return StaffName;
            else if (AccountType == 2)
                return ClientName;
            else
                return "";
        }

        public UserInfo() { }

        public UserInfo(string username)
        {
            GetUserInfo(username);
        }

        public UserInfo(Guid aspuserid)
        {
            if (aspuserid == null) return;

            MembershipUser user = Membership.GetUser(aspuserid);
            if (user != null) GetUserInfo(user.UserName);
        }

        private void GetUserInfo(string username)
        {
            if (string.IsNullOrEmpty(username)) return;

            UserName = username;
            aspnetUserId = new Guid(Membership.GetUser(UserName).ProviderUserKey.ToString());
            UserInRoles = Roles.GetRolesForUser(username);

            if (username.Contains("Anonymous")) { AccountType = 0; return; }

            Org_Staff staff = UserBLL.GetStaffByUsername(username);
            if (staff != null)
            {
                StaffID = staff.ID;
                StaffName = staff.RealName;
                StaffPosition = staff.Position;
                StaffMobile = staff.Mobile;

                OwnerType = staff.OwnerType;
                OrganizeCity = staff.OrganizeCity;
                ClientID = staff.OwnerClient;
                if (ClientID > 0)
                {
                    CM_Client _c = new CM_ClientBLL(ClientID).Model;
                    if (_c != null)
                    {
                        ClientName = _c.FullName == "" ? _c.ShortName : _c.FullName;
                        ClientType = _c.ClientType;
                    }
                }
            }
        }
    }
}
