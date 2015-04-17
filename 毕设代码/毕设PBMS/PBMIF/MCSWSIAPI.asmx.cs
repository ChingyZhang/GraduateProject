using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Common.Encrypter;
using System.Collections;
using System.Web.Script.Services;
using MCSFramework.WSI.Model;
using System.Collections.Specialized;

namespace MCSFramework.WSI
{
    /// <summary>
    /// MCSWSIAPI 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://mmp.meichis.com/DataInterface/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MCSWSIAPI : System.Web.Services.WebService
    {
        #region 可以匿名访问的方法列表
        /// <summary>
        /// 可以匿名访问的方法列表
        /// </summary>
        private static string[] AnonymousMethods = new string[] { 
            "AppRunExceptionLog",
            "GetAttachmentDownloadURL", 
            "OfficialCityService.GetCurrentOfficialCityByGPS",
            "OfficialCityService.GetSubCitysBySuper",
            "OfficialCityService.GetAllOfficialCitys",
            "OfficialCityService.GetSuperOfficialCity",
            "OfficialCityService.GetCurrentOfficialCityByGPS",
            "OfficialCityService.DistanceByLatLong",
            "NoticeService.GetNoticeByNoticeID",
            "PBMService.GetTDPList",
            "PBMService.GetRelateVehicleList",
            "PBMService.GetTDP_Info",
            "PBMService.GetTDP_RTChannel",
            "PBMService.GetTDP_RTSalesArea",
            "PBMService.GetTDP_StandardPrice",
            "PBMService.GetTDP_ProductList",
            "PBMService.GetTDP_WareHouseList",
            "PBMService.GetRetailerList",
            "PBMService.GetNearRetailerList",
            "PBMService.GetRetailerListByVisitRoute",
            "PBMService.GetRetailerDeailInfo",
            "PBMService.SetClientGPS",
            "PBMService.UploadRetailerPicture",
            "PBMService.RetailerAdd",
            "PBMService.RetailerUpdate",
            "PBMService.GetVisitRouteList",
            "PBMService.GetVisitTemplateList",
        };
        #endregion

        #region 不需要AuthKey访问的方式列表
        /// <summary>
        /// 不需要AuthKey访问的方式列表
        /// </summary>
        private static string[] NoAuthKeyMethods = new string[] { 
            "AppRunExceptionLog",
            "UserLogin.Login", 
            "UserLogin.Login_T", 
            "UserLogin.ApplyAESEncryptKey", 
            "GetAttachmentDownloadURL",
            "OfficialCityService.DistanceByLatLong",
            "OfficialCityService.WGS2GCJ",
            "AppUpdateService.GetLastVersionInfo"
        };
        #endregion

        private static AppUpdateService _APKAutoUpdate = new AppUpdateService();
        private static UserLogin _UserLogin = new UserLogin();
        private static OfficialCityService _OfficialCityService = new OfficialCityService();
        private static NoticeService _NoticeService = new NoticeService();



        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequestPack"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public string Call(string RequestPack)
        {
            int _Sequence = -1;

            try
            {
                LogWriter.FILE_PATH = "C:\\MCSLog_PBMIF";
                LogWriter.WriteLog("MCSWSIAPI.Call:RequestPack=" + (RequestPack.Length > 10240 ? RequestPack.Substring(0, 10240) : RequestPack));
                WSIRequestPack reqPack = JsonConvert.DeserializeObject<WSIRequestPack>(RequestPack);
                UserInfo User = null;
                string DecryptParamsText = "";

                _Sequence = reqPack.Sequence;

                if (!NoAuthKeyMethods.Contains(reqPack.Method))
                {
                    #region 判断用户AuthKey
                    if (AnonymousMethods.Contains(reqPack.Method))
                        User = CheckAuthKeyIncludeAnonymous(reqPack.AuthKey);
                    else
                        User = CheckAuthKey(reqPack.AuthKey);

                    if (User == null)
                    {
                        WSIResultPack resultPack = new WSIResultPack(reqPack.Sequence, -110, "AuthKey Invalid!");
                        return resultPack.ToJsonString();
                    }
                    #endregion

                    #region 解密参数文本
                    if (!string.IsNullOrEmpty(reqPack.Params))
                    {
                        int ret = CryptHelper.AESDecryptText(reqPack.AuthKey, reqPack.Params, out DecryptParamsText);

                        if (ret < 0 && !ConfigHelper.GetConfigBool("DebugMode"))
                        {
                            //0:成功 -1:未找到缓存的密钥 -2:解密失败 -100:用户未登录
                            WSIResultPack resultPack = new WSIResultPack(reqPack.Sequence, -101, "Decrypt Failed! Ret=" + ret.ToString());
                            return resultPack.ToJsonString();
                        }

                        if (ConfigHelper.GetConfigBool("DebugMode"))
                        {
                            LogWriter.WriteLog("MCSWSIAPI.Call:AuthKey=" + reqPack.AuthKey + ", Method=" + reqPack.Method + ", Decrypt Params=" + (DecryptParamsText.Length > 1024 ? DecryptParamsText.Substring(0, 1024) : DecryptParamsText));
                        }
                    }
                    #endregion
                }
                else
                {
                    DecryptParamsText = reqPack.Params;
                }

                Dictionary<string, string> _params = string.IsNullOrEmpty(DecryptParamsText) ? new Dictionary<string, string>() :
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(DecryptParamsText);

                switch (reqPack.Method)
                {
                    #region 用户登录类接口

                    #region 客户端申请AES密码
                    case "UserLogin.ApplyAESEncryptKey":
                        {
                            string CryptAESKey = "", CryptAESIV = "";
                            int ret = UserLogin.ApplyAESEncryptKey((string)_params["DeviceCode"], (string)_params["Modulus"], (string)_params["Exponent"], out  CryptAESKey, out  CryptAESIV);
                            string info = "";
                            switch (ret)
                            {
                                case -100:
                                    info = "设备号未在可登录的列表中登记!";
                                    break;
                                default:
                                    break;
                            }
                            Hashtable hs_result = new Hashtable();
                            hs_result.Add("CryptAESKey", CryptAESKey);
                            hs_result.Add("CryptAESIV", CryptAESIV);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, info, JsonConvert.SerializeObject(hs_result));
                            return resultpack.ToJsonString();
                        }
                    #endregion

                    #region 用户登录
                    case "UserLogin.Login":
                        {
                            string _authkey = "";
                            int ret = UserLogin.Login((string)_params["UserName"], (string)_params["Password"], (string)_params["DeviceCode"], (string)_params["ExtParams"], out _authkey);
                            string info = "";
                            switch (ret)
                            {
                                case -1001: info = "用户名或密码错误，登录失败"; break;
                                case -1002: info = "未能获取到对称加密密钥 "; break;
                                case -1003: info = "设备号未在可登录的列表中登记"; break;
                                case -1004: info = "当前用户不允许从该设备号登录"; break;
                                case -1005: info = "登录失败"; break;
                                default: break;
                            }
                            Hashtable hs_result = new Hashtable();
                            hs_result.Add("AuthKey", _authkey);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, info, JsonConvert.SerializeObject(hs_result));
                            return resultpack.ToJsonString();   //不加密返回
                        }
                    case "UserLogin.Login_T":
                        {
                            string authkey = UserLogin.Login_T((string)_params["UserName"], (string)_params["Password"], (string)_params["DeviceCode"]);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, authkey);
                            return resultpack.ToJsonString();
                        }
                    #endregion

                    #region 获取当前用户信息
                    case "UserLogin.GetCurrentUser":
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "", JsonConvert.SerializeObject(User));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取当前用户的新消息数
                    case "UserLogin.GetNewMsg":
                        {
                            int msgcount = 0;
                            User = CheckAuthKey(reqPack.AuthKey, out msgcount);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, msgcount);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 修改密码
                    case "UserLogin.ChangePassword":
                        {
                            int ret = UserLogin.ChangePassword(User, (string)_params["OldPassword"], (string)_params["NewPassword"]);
                            string info = "";
                            switch (ret)
                            {
                                case -1010: info = "密码修改失败"; break;
                            }
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, info);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 用户注销
                    case "UserLogin.Logout":
                        {
                            int ret = UserLogin.Logout(User, reqPack.AuthKey);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret);
                            return resultpack.ToJsonString();
                        }
                    #endregion

                    #endregion

                    #region 行政城市信息接口

                    #region 获取指定城市的下级城市
                    case "OfficialCityService.GetSubCitysBySuper":
                        {
                            int cityid = 0;
                            int.TryParse(_params["CityID"].ToString(), out cityid);
                            string jsonstr = JsonConvert.SerializeObject(OfficialCityService.GetSubCitysBySuper(User, cityid));
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "", jsonstr);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取所有行政城市
                    case "OfficialCityService.GetAllOfficialCitys":
                        {
                            string jsonstr = JsonConvert.SerializeObject(OfficialCityService.GetAllOfficialCitys(User));
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "", jsonstr);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取指定城市的全名
                    case "OfficialCityService.GetCityFullName":
                        {
                            int cityid = 0;
                            int.TryParse(_params["CityID"].ToString(), out cityid);
                            string retinfo = OfficialCityService.GetCityFullName(User, cityid);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, retinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取行政城市对应的省市县
                    case "OfficialCityService.GetSuperOfficialCity":
                        {
                            int cityid = 0;
                            string prov = "", city = "", area = "";
                            int.TryParse(_params["OfficialCity"].ToString(), out cityid);
                            int ret = OfficialCityService.GetSuperOfficialCityJson(User, cityid, out prov, out city, out area);

                            Hashtable hs_result = new Hashtable();
                            hs_result.Add("Prov", prov);
                            hs_result.Add("City", city);
                            hs_result.Add("Area", area);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, "", JsonConvert.SerializeObject(hs_result));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 根据当前经纬度获取所在的城市信息
                    case "OfficialCityService.GetCurrentOfficialCityByGPS":
                        {
                            float latitude = 0, longitude = 0;
                            float.TryParse(_params["Latitude"].ToString(), out latitude);
                            float.TryParse(_params["Longitude"].ToString(), out longitude);
                            OfficialCity city = OfficialCityService.GetCurrentOfficialCityByGPS(User, latitude, longitude);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "", JsonConvert.SerializeObject(city));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region WGS坐标转火星坐标
                    case "OfficialCityService.WGS2GCJ":
                        {
                            float mgLat = 0, mgLon = 0;
                            float Latitude = 0, Longitude = 0;
                            float.TryParse(_params["Latitude"].ToString(), out Latitude);
                            float.TryParse(_params["Longitude"].ToString(), out Longitude);
                            OfficialCityService.WGS2GCJ(Latitude, Longitude, out mgLat, out mgLon);

                            Hashtable hsResult = new Hashtable();
                            hsResult.Add("mgLat", mgLat);
                            hsResult.Add("mgLon", mgLon);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "", JsonConvert.SerializeObject(hsResult));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 计算经纬度的距离（米）
                    case "OfficialCityService.DistanceByLatLong":
                        {
                            double latitude1 = 0, longitude1 = 0, latitude2 = 0, longitude2 = 0;
                            double.TryParse(_params["Latitude1"].ToString(), out latitude1);
                            double.TryParse(_params["Longitude1"].ToString(), out longitude1);
                            double.TryParse(_params["Latitude2"].ToString(), out latitude2);
                            double.TryParse(_params["Longitude2"].ToString(), out longitude2);
                            int distance = OfficialCityService.DistanceByLatLong(latitude1, longitude1, latitude2, longitude2);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, distance);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #endregion

                    #region 通知公告接口

                    #region 获取通知列表
                    case "NoticeService.GetMyNoticeList":
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(NoticeService.GetMyNoticeList(User)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 根据ID号查询通知内容
                    case "NoticeService.GetNoticeByNoticeID":
                        {
                            int noticeid = 0;
                            int.TryParse(_params["NoticeID"].ToString(), out noticeid);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(NoticeService.GetNoticeByNoticeID(User, noticeid)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 设置通知已读标志
                    case "NoticeService.SetHasRead":
                        {
                            int noticeid = 0;
                            int.TryParse(_params["NoticeID"].ToString(), out noticeid);
                            int ret = NoticeService.SetHasRead(User, noticeid);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 追加公告评论
                    case "NoticeService.AddComment":
                        {
                            int noticeid = 0;
                            int.TryParse(_params["NoticeID"].ToString(), out noticeid);
                            int ret = NoticeService.AddComment(User, noticeid, (string)_params["CommentContent"]);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取未读公告数量
                    case "NoticeService.GetMyNewNoticeCount":
                        {
                            int ret = NoticeService.GetMyNewNoticeCount(User);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取指定目录的通知内容
                    case "NoticeService.GetNoticeListByCatalog":
                        {
                            int Catalog = 0;
                            int.TryParse(_params["Catalog"].ToString(), out Catalog);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(NoticeService.GetNoticeListByCatalog(User, Catalog)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取指定目录的未读通知数量
                    case "NoticeService.GetNewNoticeListByCatalog":
                        {
                            int Catalog = 0;
                            int.TryParse(_params["Catalog"].ToString(), out Catalog);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, NoticeService.GetNewNoticeListByCatalog(User, Catalog));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 查询我的消息
                    case "MessageService.GetMyMessageList":
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(NoticeService.GetMyMessageList(User)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 设置消息已读
                    case "MessageService.SetMessageRead":
                        {
                            int MsgID = 0;
                            int.TryParse(_params["MsgID"].ToString(), out MsgID);
                            int ret = NoticeService.SetMessageRead(User, MsgID);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取未读消息的数量
                    case "MessageService.GetNewMessageCount":
                        {
                            int ret = NoticeService.GetNewMessageCount(User);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #endregion

                    #region 公共接口
                    case "AppRunExceptionLog":
                        {
                            int ret = AppRunExceptionLog(reqPack.AuthKey, (string)_params["DeviceCode"], (string)_params["ExceptionInfo"]);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret);
                            return resultpack.ToJsonString();
                        }
                    case "GetAttachmentDownloadURL":
                        {
                            string url = GetAttachmentDownloadURL((string)_params["Guid"]);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, url);
                            return resultpack.ToJsonString();
                        }

                    #region 获取当前版本
                    case "AppUpdateService.GetLastVersionInfo":
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(AppUpdateService.GetLastVersionInfo((string)_params["AppCode"])));
                            return resultpack.ToJsonString();
                        }
                    #endregion

                    #endregion

                    #region PBMS接口

                    #region TDP经销商信息管理
                    #region 获取当前员工可管辖的经销商
                    case "PBMService.GetTDPList":
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetTDPList(User)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }

                    #endregion

                    #region 获取当前员工关联的送货车辆
                    case "PBMService.GetRelateVehicleList":
                        {
                            int tdp = 0;
                            if (_params.ContainsKey("TDP")) int.TryParse(_params["TDP"], out tdp);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetRelateVehicleList(User, tdp)));
                            return resultpack.ToJsonString(reqPack.AuthKey);

                        }
                    #endregion

                    #region 获取经销商详细资料
                    case "PBMService.GetTDP_Info":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetTDP_Info(User, TDP)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取经销商自营销售渠道
                    case "PBMService.GetTDP_RTChannel":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetTDP_RTChannel(User, TDP)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取经销商自营销售区域
                    case "PBMService.GetTDP_RTSalesArea":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetTDP_RTSalesArea(User, TDP)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取经销商销售价表
                    case "PBMService.GetTDP_StandardPrice":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetTDP_StandardPrice(User, TDP)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取经销商经营产品目录
                    case "PBMService.GetTDP_ProductList":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetTDP_ProductList(User, TDP)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取TDP所属仓库目录
                    case "PBMService.GetTDP_WareHouseList":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetTDP_WareHouseList(User, TDP)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion
                    #endregion

                    #region 门店信息管理
                    #region 获取所管辖区域的门店列表
                    case "PBMService.GetRetailerList":
                        {


                            int TDP = 0, PageSize = 0, PageIndex = 0, Counts = 0;
                            string FindKey = "";

                            FindKey = (string)_params["FindKey"];
                            int.TryParse(_params["TDP"].ToString(), out TDP);

                            int.TryParse(_params["PageIndex"].ToString(), out PageIndex);
                            int.TryParse(_params["PageSize"].ToString(), out PageSize);
                            List<ClientInfo> ret = PBMService.GetRetailerList(User, TDP, FindKey, PageSize, PageIndex, out Counts);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, Counts, "", JsonConvert.SerializeObject(ret));
                            return resultpack.ToJsonString(reqPack.AuthKey);


                        }
                    #endregion

                    #region  获取附近门店列表
                    case "PBMService.GetNearRetailerList":
                        {
                            int TDP = 0, MaxDistance = 0, PageSize = 0, PageIndex = 0, Counts = 0;
                            string FindKey = "";
                            decimal Latitude = 0, Longitude = 0;

                            FindKey = (string)_params["FindKey"];

                            decimal.TryParse(_params["Latitude"].ToString(), out Latitude);
                            decimal.TryParse(_params["Longitude"].ToString(), out Longitude);

                            int.TryParse(_params["MaxDistance"].ToString(), out MaxDistance);
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["PageIndex"].ToString(), out PageIndex);
                            int.TryParse(_params["PageSize"].ToString(), out PageSize);
                            List<ClientInfo> ret = PBMService.GetNearRetailerList(User, TDP, FindKey, Latitude, Longitude, MaxDistance, PageSize, PageIndex, out Counts);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, Counts, "", JsonConvert.SerializeObject(ret));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取制定拜访路线上的门店
                    case "PBMService.GetRetailerListByVisitRoute":
                        {
                            int TDP = 0, PageSize = 0, PageIndex = 0, Counts = 0, VisitRoute = 0;
                            string FindKey = "";
                            FindKey = (string)_params["FindKey"];
                            int.TryParse(_params["VisitRoute"].ToString(), out VisitRoute);
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["PageIndex"].ToString(), out PageIndex);
                            int.TryParse(_params["PageSize"].ToString(), out PageSize);

                            List<ClientInfo> ret = PBMService.GetRetailerListByVisitRoute(User, TDP, FindKey, VisitRoute, PageSize, PageIndex, out Counts);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, Counts, "", JsonConvert.SerializeObject(ret));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region  根据门店ID获取门店详细信息
                    case "PBMService.GetRetailerDeailInfo":
                        {
                            int TDP = 0, ClientID = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["ClientID"].ToString(), out ClientID);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetRetailerDeailInfo(User, TDP, ClientID)));
                            return resultpack.ToJsonString(reqPack.AuthKey);

                        }
                    #endregion

                    #region 定位门店GPS经纬度
                    case "PBMService.SetClientGPS":
                        {
                            int ClientID = 0;
                            float Latitude = 0, Longitude;
                            int.TryParse(_params["ClientID"].ToString(), out ClientID);
                            float.TryParse(_params["Latitude"].ToString(), out Latitude);
                            float.TryParse(_params["Longitude"].ToString(), out Longitude);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, PBMService.SetClientGPS(User, ClientID, Latitude, Longitude));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 上传门店照片
                    case "PBMService.UploadRetailerPicture":
                        {
                            int ClientID = 0;
                            string PicName = "", Description = "", PicData = "";
                            int.TryParse(_params["ClientID"].ToString(), out ClientID);
                            PicName = _params["PicName"].ToString();
                            Description = _params["Description"].ToString();
                            PicData = _params["PicData"].ToString();

                            Guid PicGUID = Guid.Empty;
                            int ret = PBMService.UploadRetailerPicture(User, ClientID, PicName, Description, PicData, out PicGUID);
                            Hashtable hs_result = new Hashtable();
                            hs_result.Add("PicGUID", PicGUID);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, "", JsonConvert.SerializeObject(hs_result));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 新增门店资料
                    case "PBMService.RetailerAdd":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"], out TDP);
                            ClientInfo Client = JsonConvert.DeserializeObject<ClientInfo>(_params["ClientJson"]);
                            string ErrorInfo = "";
                            int ret = PBMService.RetailerAdd(User, TDP, Client, out ErrorInfo);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, ErrorInfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);

                        }
                    #endregion

                    #region 更新门店资料
                    case "PBMService.RetailerUpdate":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            ClientInfo Client = JsonConvert.DeserializeObject<ClientInfo>(_params["ClientJson"]);
                            string ErrorInfo = "";
                            int ret = PBMService.RetailerUpdate(User, TDP, Client, out ErrorInfo);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, ErrorInfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);

                        }
                    #endregion

                    #region 上传TDP向门店供货产品品项列表
                    case "PBMService.UpdateRetailerProductList":
                        {
                            int TDP = 0, Retailer = 0;
                            int.TryParse(_params["TDP"], out TDP);
                            int.TryParse(_params["Retailer"], out Retailer);
                            List<ClientInfo.ProductList> ProductList = JsonConvert.DeserializeObject<List<ClientInfo.ProductList>>(_params["ProductList"]);
                            int ret = PBMService.UpdateRetailerProductList(User, TDP, Retailer, ref ProductList);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, "", JsonConvert.SerializeObject(ProductList));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #endregion

                    #region 库存管理
                    #region 获取指定车辆的仓库库存
                    case "PBMService.GetInventoryByVehicle":
                        {
                            int VehicleID = 0, DisplayMode = 0;
                            int.TryParse(_params["VehicleID"], out VehicleID);
                            int.TryParse(_params["DisplayMode"], out DisplayMode);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetInventoryByVehicle(User, VehicleID, DisplayMode)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取指定仓库库存
                    case "PBMService.GetInventoryByWareHouse":
                        {
                            int WareHouse = 0, DisplayMode = 0;
                            int.TryParse(_params["WareHouse"], out WareHouse);
                            int.TryParse(_params["DisplayMode"], out DisplayMode);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetInventoryByWareHouse(User, WareHouse, DisplayMode)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion
                    #endregion

                    #region 拜访管理
                    #region 获取当前员工的拜访路线
                    case "PBMService.GetVisitRouteList":
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetVisitRouteList(User)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取拜访模板信息
                    case "PBMService.GetVisitTemplateList":
                        {
                            int TDP = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetVisitTemplateList(User, TDP)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取指定步骤编码的环节详细信息
                    case "PBMService.GetVisitProcessDefineInfo":
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetVisitProcessDefineInfo(User, _params["ProcessCodes"])));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取员工拜访计划
                    case "PBMService.GetVisitPlanListByStaff":
                        {
                            int staff = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["Staff"], out staff);
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetVisitPlanListByStaff(User, staff, begindate, enddate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 新增门店拜访记录
                    case "PBMService.VisitWorkAdd":
                        {
                            VisitWork Work = JsonConvert.DeserializeObject<VisitWork>(_params["Work"]);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, PBMService.VisitWorkAdd(User, Work));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 上传拜访照片
                    case "PBMService.UploadVisitWorkPicture":
                        {
                            int VisitWorkID = 0;
                            int.TryParse(_params["VisitWorkID"], out VisitWorkID);
                            Guid PicGUID = Guid.Empty;

                            int ret = PBMService.UploadVisitWorkPicture(User, VisitWorkID, _params["PicName"], _params["Description"], _params["PicData"], out PicGUID);
                            Hashtable hs_result = new Hashtable();
                            hs_result.Add("PicGUID", PicGUID);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, "", JsonConvert.SerializeObject(hs_result));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 定时上传拜访位置
                    case "PBMService.VisitReportLocation":
                        {
                            int Type = 0;
                            float Latitude = 0, Longitude = 0;
                            string Remark = "";

                            int.TryParse(_params["Type"], out Type);
                            float.TryParse(_params["Latitude"], out Latitude);
                            float.TryParse(_params["Longitude"], out Longitude);
                            Remark = _params["Remark"];

                            int ret = PBMService.VisitReportLocation(User, Type, Latitude, Longitude, Remark);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion
                    #endregion

                    #region 车销管理

                    #region 查询指定门店的销售单
                    case "PBMService.GetSaleOutListByRetailer":
                        {
                            int retailer = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["Retailer"], out retailer);
                            string stateflag = _params["StateFlag"];        //状态标志 ALL:所有 COMPLETE:已完成的销售单 UNCOMPLETE:未完成的销售单
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetSaleOutListByRetailer(User, retailer, stateflag, begindate, enddate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 新增销售单
                    case "PBMService.SaleOut_Add":
                        {
                            Delivery d = JsonConvert.DeserializeObject<Delivery>(_params["DeliveryInfo"]);
                            string errorinfo = "";
                            int ret = PBMService.SaleOut_Add(User, d, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 更新销售单
                    case "PBMService.SaleOut_Update":
                        {
                            Delivery d = JsonConvert.DeserializeObject<Delivery>(_params["DeliveryInfo"]);
                            string errorinfo = "";
                            int ret = PBMService.SaleOut_Update(User, d, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 提交销售单
                    case "PBMService.SaleOut_Submit":
                        {
                            int id = 0;
                            Delivery d = null;
                            string errorinfo = "";
                            int.TryParse(_params["DeliveryID"], out id);

                            int ret = PBMService.SaleOut_Submit(User, id, out d, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo,
                                JsonConvert.SerializeObject(d));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 确认销售及收款
                    case "PBMService.SaleOut_Confirm":
                        {
                            int id = 0;
                            decimal WipeAmount = 0;
                            int.TryParse(_params["DeliveryID"], out id);
                            decimal.TryParse(_params["WipeAmount"], out WipeAmount);

                            List<Delivery.DeliveryPayInfo> PayInfoList =
                                JsonConvert.DeserializeObject<List<Delivery.DeliveryPayInfo>>(_params["PayInfoList"]);
                            string errorinfo = "";

                            int ret = PBMService.SaleOut_Confirm(User, id, WipeAmount, PayInfoList, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 移库调拨申请
                    case "PBMService.TransWithVehicle":
                        {
                            Delivery d = JsonConvert.DeserializeObject<Delivery>(_params["DeliveryInfo"]);
                            string errorinfo = "";
                            int ret = PBMService.TransWithVehicle(User, d, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 查询车销调拨单
                    case "PBMService.GetTransDeliveryByVehicle":
                        {
                            int Vehicle = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["Vehicle"], out Vehicle);
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetTransDeliveryByVehicle(User, Vehicle, begindate, enddate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取赠送方式字典项
                    case "PBMService.GetDictionarySalseMode":
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetDictionarySalseMode(User)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 销售单汇总
                    //查询指定送货人销售单汇总（按产品或按门店）
                    case "PBMService.GetDeliverySummary":
                        {
                            int tdp = 0, vehicle = 0, displaymode = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["TDP"], out tdp);
                            int.TryParse(_params["Vehicle"], out vehicle);
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);
                            int.TryParse(_params["DisplayMode"], out displaymode);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetDeliverySummary(User, tdp, vehicle, begindate, enddate, displaymode)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    //按送货人查询销售收款汇总
                    case "PBMService.GetDeliveryPayInfoSummary":
                        {
                            int tdp = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["TDP"], out tdp);
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetDeliveryPayInfoSummary(User, tdp, begindate, enddate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }

                    //按送货人查询销售收款明细
                    case "PBMService.GetDeliveryPayInfoDetail":
                        {
                            int tdp = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["TDP"], out tdp);
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetDeliveryPayInfoDetail(User, tdp, begindate, enddate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #endregion

                    #region 送货接口

                    #region 获取已派发给当前员工的待送货的销售单
                    case "PBMService.GetNeedDeliveryList":
                        {
                            int tdp = 0, vehicle = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["TDP"], out tdp);
                            int.TryParse(_params["Vehicle"], out vehicle);
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetNeedDeliveryList(User, tdp, vehicle, begindate, enddate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 汇总预售待送货列表
                    case "PBMService.GetNeedDeliverySummary":
                        {
                            int tdp = 0, vehicle = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["TDP"], out tdp);
                            int.TryParse(_params["Vehicle"], out vehicle);
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetNeedDeliverySummary(User, tdp, vehicle, begindate, enddate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #endregion

                    #region 发货单管理通用接口

                    #region 查询指定ID的发货单
                    case "PBMService.GetDeliveryByDeliveryID":
                        {
                            int id = 0;
                            int.TryParse(_params["DeliveryID"], out id);

                            Delivery d = PBMService.GetDeliveryByDeliveryID(User, id);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "", JsonConvert.SerializeObject(d));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 取消销售单
                    case "PBMService.Delivery_Cancel":
                        {
                            int id = 0;
                            int.TryParse(_params["DeliveryID"], out id);
                            string cancelreason = _params["CancelReason"];
                            string errorinfo = "";

                            int ret = PBMService.Delivery_Cancel(User, id, cancelreason, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion
                    #endregion

                    #region 预售管理

                    #region 查询指定门店的预售订单
                    case "PBMService.GetOrderListByRetailer":
                        {
                            int retailer = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["Retailer"], out retailer);
                            string stateflag = _params["StateFlag"];        //状态标志 ALL:所有 COMPLETE:已完成的销售单 UNCOMPLETE:未完成的销售单
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetOrderListByRetailer(User, retailer, stateflag, begindate, enddate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 新增预售订单
                    case "PBMService.Order_Add":
                        {
                            Order d = JsonConvert.DeserializeObject<Order>(_params["OrderInfo"]);
                            string errorinfo = "";
                            int ret = PBMService.Order_Add(User, d, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 更新预售订单
                    case "PBMService.Order_Update":
                        {
                            Order d = JsonConvert.DeserializeObject<Order>(_params["OrderInfo"]);
                            string errorinfo = "";
                            int ret = PBMService.Order_Update(User, d, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 提交预售订单
                    case "PBMService.Order_Submit":
                        {
                            int id = 0;
                            decimal WipeAmount = 0;
                            Order d = null;
                            string errorinfo = "";
                            List<Order.OrderPayInfo> PayInfoList =
                                JsonConvert.DeserializeObject<List<Order.OrderPayInfo>>(_params["PayInfoList"]);
                            int.TryParse(_params["OrderID"], out id);
                            decimal.TryParse(_params["WipeAmount"], out WipeAmount);

                            int ret = PBMService.Order_Submit(User, id, WipeAmount, PayInfoList, out d, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo,
                                JsonConvert.SerializeObject(d));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 取消预售订单
                    case "PBMService.Order_Cancel":
                        {
                            int id = 0;
                            int.TryParse(_params["OrderID"], out id);
                            string cancelreason = _params["CancelReason"];
                            string errorinfo = "";

                            int ret = PBMService.Order_Cancel(User, id, cancelreason, out  errorinfo);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 查询指定ID的预售单
                    case "PBMService.GetOrderByOrderID":
                        {
                            int id = 0;
                            int.TryParse(_params["DeliveryID"], out id);

                            Order d = PBMService.GetOrderByOrderID(User, id);
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "", JsonConvert.SerializeObject(d));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 预售订单汇总
                    case "PBMService.GetOrderSummary":
                        {
                            int tdp = 0, displaymode = 0;
                            DateTime begindate, enddate;
                            int.TryParse(_params["TDP"], out tdp);
                            DateTime.TryParse(_params["BeginDate"], out begindate);
                            DateTime.TryParse(_params["EndDate"], out enddate);
                            int.TryParse(_params["DisplayMode"], out displaymode);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                               JsonConvert.SerializeObject(PBMService.GetOrderSummary(User, tdp, begindate, enddate, displaymode)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #endregion

                    #region 财务收款管理

                    #region 查询门店预收款可用余额
                    case "PBMService.GetRetailerPreReceivedBalance":
                        {
                            int TDP = 0, Retailer = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["Retailer"].ToString(), out Retailer);

                            Hashtable hs_result = new Hashtable();
                            hs_result.Add("PreReceivedBalance", PBMService.GetRetailerPreReceivedBalance(User, TDP, Retailer));

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "", JsonConvert.SerializeObject(hs_result));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 获取零售店预收款及应收款额
                    case "PBMService.GetRetailerCurrentAccount":
                        {
                            int TDP = 0, Retailer = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["Retailer"].ToString(), out Retailer);

                            decimal PreReceivedAmount = 0, AR = 0, PreReceivedBalance = 0;
                            int ret = PBMService.GetRetailerCurrentAccount(User, TDP, Retailer, out PreReceivedAmount,
                                out AR, out PreReceivedBalance);

                            Hashtable hs_result = new Hashtable();
                            hs_result.Add("PreReceivedAmount", PreReceivedAmount);
                            hs_result.Add("AR", AR);
                            hs_result.Add("PreReceivedBalance", PreReceivedBalance);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, "", JsonConvert.SerializeObject(hs_result));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 收预收款
                    case "PBMService.Receipt_PreReceived":
                        {
                            int TDP = 0, Retailer = 0, PayMode = 0, WorkList = 0;
                            decimal Amount = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["Retailer"].ToString(), out Retailer);
                            decimal.TryParse(_params["Amount"].ToString(), out Amount);
                            int.TryParse(_params["PayMode"].ToString(), out PayMode);
                            int.TryParse(_params["WorkList"].ToString(), out WorkList);
                            string Remark = _params["Remark"];

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence,
                                PBMService.Receipt_PreReceived(User, TDP, Retailer, PayMode, Amount, WorkList, Remark));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 查询应收款记录
                    case "PBMService.GetRetailerARList":
                        {
                            int TDP = 0, Retailer = 0, BalanceFlag = 0;
                            DateTime BeginDate = new DateTime(1900, 1, 1), EndDate = new DateTime(1900, 1, 1);
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["Retailer"].ToString(), out Retailer);
                            DateTime.TryParse(_params["BeginDate"].ToString(), out BeginDate);
                            DateTime.TryParse(_params["EndDate"].ToString(), out EndDate);
                            int.TryParse(_params["BalanceFlag"].ToString(), out BalanceFlag);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetRetailerARList(User, TDP, Retailer, BeginDate, EndDate, BalanceFlag)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 结应收款
                    case "PBMService.BalanceRetailerAR_Receipt":        //结应收款_现金结
                        {
                            int TDP = 0, Retailer = 0, PayMode = 0, WorkList = 0;
                            decimal Amount = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["Retailer"].ToString(), out Retailer);
                            int.TryParse(_params["PayMode"].ToString(), out PayMode);
                            decimal.TryParse(_params["Amount"].ToString(), out Amount);
                            int.TryParse(_params["WorkList"].ToString(), out WorkList);

                            string errorinfo = "";
                            int ret = PBMService.BalanceRetailerAR_Receipt(User, TDP, Retailer, PayMode, Amount, _params["Remark"],
                                WorkList, _params["ARIDs"], out errorinfo);


                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    case "PBMService.BalanceRetailerAR_UsageBalance":        //结应收款_余额结
                        {
                            int TDP = 0, Retailer = 0;
                            decimal Amount = 0;
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["Retailer"].ToString(), out Retailer);
                            decimal.TryParse(_params["Amount"].ToString(), out Amount);

                            string errorinfo = "";
                            int ret = PBMService.BalanceRetailerAR_UsageBalance(User, TDP, Retailer, Amount, _params["Remark"],
                                 _params["ARIDs"], out errorinfo);


                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, ret, errorinfo);
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 查询预收款变动记录
                    case "PBMService.GetRetailerBalanceUsageList":
                        {
                            int TDP = 0, Retailer = 0;
                            DateTime BeginDate = new DateTime(1900, 1, 1), EndDate = new DateTime(1900, 1, 1);
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["Retailer"].ToString(), out Retailer);
                            DateTime.TryParse(_params["BeginDate"].ToString(), out BeginDate);
                            DateTime.TryParse(_params["EndDate"].ToString(), out EndDate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetRetailerBalanceUsageList(User, TDP, Retailer, BeginDate, EndDate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #region 查询业务员收款记录
                    case "PBMService.GetCashReceiptList":
                        {
                            int TDP = 0, PayMode = 0;
                            DateTime BeginDate = new DateTime(1900, 1, 1), EndDate = new DateTime(1900, 1, 1);
                            int.TryParse(_params["TDP"].ToString(), out TDP);
                            int.TryParse(_params["PayMode"].ToString(), out PayMode);
                            DateTime.TryParse(_params["BeginDate"].ToString(), out BeginDate);
                            DateTime.TryParse(_params["EndDate"].ToString(), out EndDate);

                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, 0, "",
                                JsonConvert.SerializeObject(PBMService.GetCashReceiptList(User, TDP, PayMode, BeginDate, EndDate)));
                            return resultpack.ToJsonString(reqPack.AuthKey);
                        }
                    #endregion

                    #endregion
                    #endregion

                    default:
                        {
                            WSIResultPack resultpack = new WSIResultPack(reqPack.Sequence, -10000, "Invalid Method Name!");
                            return resultpack.ToJsonString();
                        }
                }
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("MCSWSIAPI.Request Exception!RequestPack=" + RequestPack, err);

                WSIResultPack resultpack = new WSIResultPack(_Sequence, -100, "Request Exception!" + err.Message + err.Source);
                return resultpack.ToJsonString();

            }
        }

        [WebMethod]
        public string Test(string Method, string AuthKey, string Params)
        {
            WSIRequestPack reqPack = new WSIRequestPack();
            reqPack.Sequence = 1;
            reqPack.Method = Method;
            reqPack.AuthKey = AuthKey;
            reqPack.Params = Params;

            return Call(JsonConvert.SerializeObject(reqPack));
        }

        [WebMethod]
        public string LoginTest(string UserName, string Password)
        {
            return Test("UserLogin.Login_T", "", "{\"UserName\":\"" + UserName + "\",\"Password\":\"" + Password + "\",\"DeviceCode\":\"\"}");
        }

        #region 检查用户授权码
        /// <summary>
        /// 检查用户授权码
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="NewMsgCount"></param>
        /// <returns></returns>
        public UserInfo CheckAuthKey(string AuthKey, out int NewMsgCount)
        {
            string UserName = "";
            NewMsgCount = 0;
            if (string.IsNullOrEmpty(AuthKey)) return null;

            string cachekey = "EBMIF_OnlineUser-" + AuthKey;
            UserInfo User = (UserInfo)DataCache.GetCache(cachekey);
            if (User == null)
            {
                #region 如果缓存中丢失，从数据库获取用户登录信息，并再次放入缓存中
                string strUserInfo = "", strCryptKey = "", ExtPropertys = "";
                int ret = UserBLL.CheckAuthKey(AuthKey, 0, out UserName, out NewMsgCount, out strUserInfo, out strCryptKey, out ExtPropertys);
                if (ret < 0 || string.IsNullOrEmpty(strUserInfo)) return null;

                try
                {
                    User = JsonConvert.DeserializeObject<UserInfo>(strUserInfo);
                    if (User == null) return null;

                    DataCache.SetCache(cachekey, User, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));

                    return User;
                }
                catch { return null; }
                #endregion
            }
            else
            {
                int ret = UserBLL.CheckAuthKey(AuthKey, 0, out UserName, out NewMsgCount);
                if (ret < 0)
                {
                    DataCache.RemoveCache(cachekey);
                    return null;
                }

                //放入1分钟活跃名单中
                string cachekey2 = "EBMIF_OnlineUser-ActiveOneMinute-" + AuthKey;
                DataCache.SetCache(cachekey2, User, DateTime.Now.AddMinutes(1), System.Web.Caching.Cache.NoSlidingExpiration);

                return User;
            }
        }

        /// <summary>
        /// 检查用户授权码
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public UserInfo CheckAuthKey(string AuthKey)
        {
            //1分钟之内Check过的AuthKey，不再Check
            string cachekey = "EBMIF_OnlineUser-ActiveOneMinute-" + AuthKey;
            UserInfo User = (UserInfo)DataCache.GetCache(cachekey);
            if (User != null) return User;

            int NewMsgCount = 0;
            return CheckAuthKey(AuthKey, out NewMsgCount);
        }

        /// <summary>
        /// 检查授权码(含匿名用户)
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public UserInfo CheckAuthKeyIncludeAnonymous(string AuthKey)
        {
            int NewMsgCount = 0;
            UserInfo User = CheckAuthKey(AuthKey, out NewMsgCount);

            return User;
        }
        #endregion

        #region 公共接口方法
        /// <summary>
        /// 写入APP错误日志
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="ExceptionInfo"></param>
        /// <returns></returns>
        public static int AppRunExceptionLog(string AuthKey, string DeviceCode, string ExceptionInfo)
        {
            try
            {
                LogWriter.FILE_PATH = "C:\\MCSLog_MCSWSIAPI_Exception";
                LogWriter.WriteLog("UserLogin.AppRunExceptionLog:AuthKey=" + AuthKey + ",DeviceCode=" + DeviceCode + ",ExceptionInfo=" + ExceptionInfo);

                UserInfo User = UserLogin.CheckAuthKeyIncludeAnonymous(AuthKey);
                if (User != null)
                {
                    LogWriter.WriteLog("UserLogin.AppRunExceptionLog:AuthKey=" + AuthKey + ",UserInfo=" + JsonConvert.SerializeObject(User));
                }
            }
            catch { }

            return 0;
        }

        /// <summary>
        /// 获取指定GUID附件下载的URL地址
        /// </summary>
        /// <param name="Guid"></param>
        /// <returns></returns>
        public static string GetAttachmentDownloadURL(string Guid)
        {
            return ConfigHelper.GetConfigString("ATMTDownloadURL") + Guid;
        }
        #endregion
    }
}
