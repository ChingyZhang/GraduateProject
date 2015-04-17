using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.IPT;
using MCSFramework.BLL.PBM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.VST;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.VST;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ImportExcelTDP
{
    /// <summary>
    /// 该类方法用于PBMS第一次导入经销商数据时读取五张Sheet表数据  By ChingyZhang
    /// </summary>
    public class ClientInit
    {
        /**
         *返回状态 3：成功 4：失败 5：部分成功 
         */

        #region 1.导入员工资料，废弃不用
        /// <summary>
        /// 导入员工资料
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <param name="Staff"></param>
        /// <param name="StaffSheet"></param>
        /// <param name="State">3：成功 4：失败 5：部分成功</param>
        /// <returns></returns>
        //public string DoImportStaff(int TemplateID, int Client, ISheet StaffSheet, out int State)
        public string DoImportStaff(int TemplateID, int Client, ISheet Sheet, out int State)
        {
            string ImportInfo = "【员工资料】Excel表：";
            State = 0;
            IPT_UploadTemplateBLL _template = new IPT_UploadTemplateBLL(TemplateID);

            List<string> listPDT = new List<string>() { "序号", "姓名", "职务", "手机号码", "关联车号", "身份证号" };

            DataTable dt = null;
            bool flag = VertifySheet(Sheet, listPDT, out dt, ref ImportInfo);
            if (!flag) { State = 4; return ImportInfo; }

            foreach (DataRow dr in dt.Rows)//循环导入数据
            {
                try
                {
                    string _staffName = dr["姓名"].ToString();

                    IList<Org_Staff> _listStaff = Org_StaffBLL.GetStaffList("  Dimission=1 AND OwnerClient=" + _template.Model.ClientID.ToString() + " AND RealName='" + _staffName + "'");
                    if (_listStaff != null && _listStaff.Count > 0)
                    {
                        ImportInfo += string.Format("导入序列号为{0}，名字为{1}的数据行时与现有员工名字重复，跳过此行\r\n", dr["序号"].ToString(), _staffName);
                        continue;
                    }

                    int _staffPosition = 0; string _strStaffPosition = dr["职务"].ToString();
                    string _staffPhone = dr["手机号码"].ToString();
                    string _staffIDCode = dr["身份证号"].ToString();
                    if (!string.IsNullOrEmpty(_strStaffPosition))//无职位默认为业代
                    {
                        IList<Org_Position> _listPosition = Org_PositionBLL.GetModelList(" Name='" + _strStaffPosition + "'");
                        if (_listPosition != null && _listPosition.Count > 0) _staffPosition = _listPosition[0].ID;
                    }
                    else { _staffPosition = 1030; }

                    Org_StaffBLL _bllStaff = new Org_StaffBLL();
                    CM_ClientBLL c = new CM_ClientBLL(_template.Model.ClientID);
                    CM_ClientManufactInfo manufactinfo = c.GetManufactInfo();
                    if (c != null && manufactinfo != null)
                    {
                        _bllStaff.Model.OrganizeCity = manufactinfo.OrganizeCity;
                        _bllStaff.Model.OfficialCity = c.Model.OfficialCity;
                    }
                    _bllStaff.Model.RealName = _staffName;
                    _bllStaff.Model.Position = _staffPosition;
                    _bllStaff.Model.Mobile = _staffPhone;
                    _bllStaff.Model.TeleNum = _staffPhone;
                    _bllStaff.Model.IDCode = _staffIDCode;
                    _bllStaff.Model.InsertStaff = _template.Model.InsertStaff;
                    _bllStaff.Model.OwnerClient = _template.Model.ClientID;
                    _bllStaff.Model.OwnerType = 3;
                    _bllStaff.Model.Dimission = 1;
                    _bllStaff.Model.ApproveFlag = 1;
                    int _ClientID = _bllStaff.Add();//当前员工ID
                    //创建默认员工线路
                    if (_ClientID > 0)
                    {
                        if (_bllStaff.Model.Position == 1030)//业待创建默认路线
                        {
                            VST_RouteBLL routebll = new VST_RouteBLL();
                            routebll.Model.Code = "R" + _ClientID.ToString();
                            routebll.Model.Name = "线路-" + _bllStaff.Model.RealName;
                            routebll.Model.RelateStaff = _ClientID;
                            routebll.Model.OrganizeCity = _bllStaff.Model.OrganizeCity;
                            routebll.Model.OwnerClient = _template.Model.ClientID;
                            routebll.Model.OwnerType = 3;
                            routebll.Model.ApproveFlag = 1;
                            routebll.Model.EnableFlag = "Y";
                            routebll.Model.InsertStaff = _template.Model.InsertStaff;
                            routebll.Add();
                        }
                        if (_bllStaff.Model.Position == 1030 || _bllStaff.Model.Position == 1050)//只有业待和司机能关联车号
                        {
                            #region 获取车号ID
                            int _staffCar = 0; string _strStaffCar = dr["关联车号"].ToString();
                            if (!string.IsNullOrEmpty(_strStaffCar))
                            {
                                IList<CM_Vehicle> _listVehicles = CM_VehicleBLL.GetModelList("Client=" + _template.Model.ClientID + " AND VehicleNo='" + _strStaffPosition + "' ");
                                if (_listVehicles != null && _listVehicles.Count > 0) _staffCar = _listVehicles[0].ID;
                                else
                                {
                                    CM_VehicleBLL vehicle = new CM_VehicleBLL();
                                    vehicle.Model.Client = _template.Model.ClientID;
                                    vehicle.Model.VehicleNo = _strStaffCar;
                                    vehicle.Model.State = 1;
                                    vehicle.Model.Remark = "新导入未确认";
                                    vehicle.Model.VehicleClassify = 2;
                                    _staffCar = vehicle.Add();
                                }
                            }
                            #endregion
                            if (_staffCar > 0)//关联车号
                            {
                                IList<CM_VehicleInStaff> _listVehicleInStaff = CM_VehicleInStaffBLL.GetModelList("Vehicle=" + _staffCar.ToString() + " AND Staff=" + _ClientID.ToString());
                                if (_listVehicleInStaff == null || _listVehicleInStaff.Count == 0)
                                {
                                    CM_VehicleInStaffBLL vehicle_staff = new CM_VehicleInStaffBLL();
                                    vehicle_staff.Model.Staff = _ClientID;
                                    vehicle_staff.Model.Vehicle = _staffCar;
                                    vehicle_staff.Model.InsertStaff = _template.Model.InsertStaff;
                                    vehicle_staff.Add();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ImportInfo += "导入序列号为" + dr["序号"].ToString() + "的数据行时出现错误，错误说明：" + ex.Message + "\r\n";
                    State = 5;
                    continue;
                }
            }
            if (State == 0) State = 3;
            ImportInfo += (State == 3 ? "导入完成！\r\n" : "");
            IPT_UploadTemplateMessageBLL _bllUploadTemplateMessage = new IPT_UploadTemplateMessageBLL();
            _bllUploadTemplateMessage.Model.TemplateID = TemplateID;
            _bllUploadTemplateMessage.Model.MessageType = State;
            _bllUploadTemplateMessage.Model.Content = ImportInfo;
            _bllUploadTemplateMessage.Add();
            return ImportInfo;
        }
        #endregion

        #region 2.导入商品资料
        public string DoImportProduct(int TemplateID, int Client, ISheet Sheet, out int State)
        {
            string ImportInfo = "【商品资料】Excel表：";
            State = 0;
            IPT_UploadTemplateBLL _template = new IPT_UploadTemplateBLL(TemplateID);

            List<string> listPDT = new List<string>() { "序号", "大类", "小类", "商品编码", "产品名称", "规格型号", "大单位", "小单位", "整零换算系数", "整件重量", "销售价", "采购价", "保质期", "整件条码", "零售条码" };

            DataTable dt = null;
            bool flag = VertifySheet(Sheet, listPDT, out dt, ref ImportInfo);
            if (!flag) { State = 4; return ImportInfo; }

            foreach (DataRow dr in dt.Rows)//循环导入数据
            {
                try
                {
                    decimal _pdtBuyPrice = (!string.IsNullOrEmpty(dr["采购价"].ToString())) ? Convert.ToDecimal(dr["采购价"].ToString()) : 0;
                    decimal _pdtSalesPrice = (!string.IsNullOrEmpty(dr["销售价"].ToString())) ? Convert.ToDecimal(dr["销售价"].ToString()) : 0;
                    if (_pdtSalesPrice == 0) continue;//销售价格围标表示门店不经营

                    int _pdtID = 0;
                    string _pdtCode = dr["商品编码"].ToString();

                    IList<PDT_ProductExtInfo> _listPDTExtInfo = PDT_ProductExtInfoBLL.GetProductExtInfoList_BySupplier(_template.Model.ClientID).Where(m => m.Code == _pdtCode).ToList();
                    if (_listPDTExtInfo != null && _listPDTExtInfo.Count > 0)
                    {
                        ImportInfo += string.Format("序号为{0},产品编码为{1}的产品与现有产品编码重复，跳过此行信息\r\n", dr["序号"], _pdtCode);
                        continue;
                    }

                    int _pdtCategory = 0;//类别
                    IList<PDT_Product> listPdt = PDT_ProductBLL.GetModelList(string.Format(" OwnerType IN (1,2) AND FactoryCode='{0}' ", _pdtCode));
                    if (listPdt == null || listPdt.Count == 0)//添加新产品
                    {
                        #region 获取产品类别
                        IList<PDT_Category> _listCategory = PDT_CategoryBLL.GetModelList(" SuperID=1 AND ApproveFlag=1 AND(OwnerType IN(1,2) OR (OwnerType=3 AND OwnerClient=" + _template.Model.ClientID.ToString() + " )) AND Name='" + dr["大类"].ToString() + "'");//获取大类(大类可能属于平台级和厂商级，也有可能属于经销商自己)
                        if (_listCategory == null || _listCategory.Count == 0)
                        {
                            PDT_CategoryBLL _bllCategory = new PDT_CategoryBLL();
                            _bllCategory.Model.Name = dr["大类"].ToString();
                            _bllCategory.Model.SuperID = 1;
                            _bllCategory.Model.EnabledFlag = "Y";
                            _bllCategory.Model.OwnerType = 3;
                            _bllCategory.Model.OwnerClient = _template.Model.ClientID;
                            _bllCategory.Model.Remark = "Excel批量导入";
                            _bllCategory.Model.ApproveFlag = 1;
                            _bllCategory.Model.InsertTime = DateTime.Now;
                            _bllCategory.Model.InsertStaff = _template.Model.InsertStaff;
                            _pdtCategory = _bllCategory.Add();
                        }
                        else { _pdtCategory = _listCategory[0].ID; }
                        IList<PDT_Category> _listCategory2 = PDT_CategoryBLL.GetModelList(" ApproveFlag=1 AND SuperID=" + _pdtCategory.ToString() + " AND (OwnerType IN(1,2) OR (OwnerType=3 AND OwnerClient=" + _template.Model.ClientID.ToString() + ")) AND Name='" + dr["小类"].ToString() + "'");//获取小类
                        if (_listCategory2 == null || _listCategory2.Count == 0)
                        {
                            PDT_CategoryBLL _bllCategory = new PDT_CategoryBLL();
                            _bllCategory.Model.Name = dr["小类"].ToString();
                            _bllCategory.Model.SuperID = _pdtCategory;
                            _bllCategory.Model.EnabledFlag = "Y";
                            _bllCategory.Model.OwnerType = 3;
                            _bllCategory.Model.OwnerClient = _template.Model.ClientID;
                            _bllCategory.Model.Remark = "Excel批量导入";
                            _bllCategory.Model.ApproveFlag = 1;
                            _bllCategory.Model.InsertTime = DateTime.Now;
                            _bllCategory.Model.InsertStaff = _template.Model.InsertStaff;
                            _pdtCategory = _bllCategory.Add();
                        }
                        else { _pdtCategory = _listCategory2[0].ID; }
                        #endregion

                        string _pdtName = dr["产品名称"].ToString();

                        if (string.IsNullOrEmpty(_pdtCode) && string.IsNullOrEmpty(_pdtName))
                        {
                            ImportInfo += string.Format("序号为{0}的行产品编码和名称均为空，跳过此行信息\r\n", dr["序号"]);
                            continue;
                        }
                        string _pdtSpec = dr["规格型号"].ToString();
                        IList<Dictionary_Data> _listData = DictionaryBLL.Dictionary_Data_GetAlllList(" TableName='PDT_Packaging' ");
                        string _strPdtTrafficPackaging = _listData.First(m => m.Name == dr["大单位"].ToString()).Code;
                        int _pdtTrafficPackaging = 0; int.TryParse(_strPdtTrafficPackaging, out _pdtTrafficPackaging);//整件单位
                        string _strPdtPackaging = _listData.First(m => m.Name == dr["小单位"].ToString()).Code;
                        int _pdtPackaging = 0; int.TryParse(_strPdtPackaging, out _pdtPackaging);//零售包装

                        int _pdtConvertFactor = (!string.IsNullOrEmpty(dr["整零换算系数"].ToString())) ? Convert.ToInt32(dr["整零换算系数"].ToString()) : 0;//重要信息强制显示，出错时直接跳过此行
                        decimal _pdtWeight = 0; decimal.TryParse(dr["整件重量"].ToString(), out _pdtWeight);
                        int _pdtExpiry = 0; int.TryParse(dr["保质期"].ToString(), out _pdtExpiry);
                        string _pdtBoxBarCode = dr["整件条码"].ToString();
                        string _pdtBarCode = dr["零售条码"].ToString();

                        PDT_ProductBLL _bllProduct = new PDT_ProductBLL();
                        _bllProduct.Model.FullName = _pdtName;
                        _bllProduct.Model.ShortName = _pdtName;
                        _bllProduct.Model.Spec = _pdtSpec;
                        _bllProduct.Model.TrafficPackaging = _pdtTrafficPackaging;
                        _bllProduct.Model.Packaging = _pdtPackaging;
                        _bllProduct.Model.ConvertFactor = _pdtConvertFactor;
                        _bllProduct.Model.BoxBarCode = _pdtBoxBarCode;
                        _bllProduct.Model.BarCode = _pdtBarCode;
                        _bllProduct.Model.Weight = _pdtWeight;
                        _bllProduct.Model.Expiry = _pdtExpiry;
                        _bllProduct.Model.Category = _pdtCategory;
                        _bllProduct.Model.State = 1;//在用产品
                        _bllProduct.Model.OwnerClient = _template.Model.ClientID;//所属经销商
                        _bllProduct.Model.OwnerType = 3;//经销商级
                        _bllProduct.Model.ApproveFlag = 1;
                        _bllProduct.Model.InsertTime = DateTime.Now;
                        _bllProduct.Model.InsertStaff = _template.Model.InsertStaff;
                        _bllProduct.Model.Remark = "Excel批量导入";

                        _pdtID = _bllProduct.Add();
                    }
                    else _pdtID = listPdt[0].ID;//已存在的厂商或平台级产品

                    IList<PDT_ProductExtInfo> _listPdtExtInfo = PDT_ProductExtInfoBLL.GetModelList(" Product=" + _pdtID.ToString() + " AND Supplier=" + _template.Model.ClientID.ToString() + " AND ApproveFlag=1 ");
                    if (_listPdtExtInfo == null || _listPdtExtInfo.Count == 0)
                    {
                        PDT_ProductExtInfoBLL _pdtExtInfoBLL = new PDT_ProductExtInfoBLL();
                        _pdtExtInfoBLL.Model.Supplier = _template.Model.ClientID;
                        _pdtExtInfoBLL.Model.Product = _pdtID;
                        _pdtExtInfoBLL.Model.Code = _pdtCode;
                        _pdtExtInfoBLL.Model.BuyPrice = _pdtBuyPrice;
                        _pdtExtInfoBLL.Model.SalesPrice = _pdtSalesPrice;
                        _pdtExtInfoBLL.Model.SalesState = 1;
                        _pdtExtInfoBLL.Model.Category = _pdtCategory;
                        _pdtExtInfoBLL.Model.Remark = "Excel批量导入";
                        _pdtExtInfoBLL.Model.ApproveFlag = 1;
                        _pdtExtInfoBLL.Model.InsertStaff = _template.Model.InsertStaff;
                        _pdtExtInfoBLL.Model.InsertTime = DateTime.Now;
                        _pdtExtInfoBLL.Add();
                    }
                    else
                    {
                        PDT_ProductExtInfoBLL _pdtExtInfoBLL = new PDT_ProductExtInfoBLL(_listPdtExtInfo[0].ID);
                        _pdtExtInfoBLL.Model.BuyPrice = _pdtBuyPrice;
                        _pdtExtInfoBLL.Model.SalesPrice = _pdtSalesPrice;
                        _pdtExtInfoBLL.Model.Remark = "Excel批量导入时修改";
                        _pdtExtInfoBLL.Model.UpdateStaff = _template.Model.InsertStaff;
                        _pdtExtInfoBLL.Model.UpdateTime = DateTime.Now;
                        _pdtExtInfoBLL.Update();
                    }
                }
                catch (Exception ex)
                {
                    ImportInfo += "导入序列号为" + dr["序号"].ToString() + "的数据行时出现错误，错误说明：" + ex.Message + "\r\n";
                    State = 5;
                    continue;
                }
            }
            if (State == 0) State = 3;
            ImportInfo += (State == 3 ? "导入完成！\r\n" : "");
            IPT_UploadTemplateMessageBLL _bllUploadTemplateMessage = new IPT_UploadTemplateMessageBLL();
            _bllUploadTemplateMessage.Model.TemplateID = TemplateID;
            _bllUploadTemplateMessage.Model.MessageType = State;
            _bllUploadTemplateMessage.Model.Content = ImportInfo;
            _bllUploadTemplateMessage.Add();
            return ImportInfo;
        }
        #endregion

        #region 3.导入客户资料
        public string DoImportClient(int TemplateID, int Client, ISheet Sheet, out int State)
        {
            string ImportInfo = "【客户资料】Excel表：";
            State = 0;
            IPT_UploadTemplateBLL _template = new IPT_UploadTemplateBLL(TemplateID);

            List<string> listClient = new List<string>() { "序号", "区域", "渠道", "客户编号", "客户名称", "联系人", "地址", "电话", "手机", "销售线路", "备注" };

            DataTable dt = null;
            bool flag = VertifySheet(Sheet, listClient, out dt, ref ImportInfo);
            if (!flag) { State = 4; return ImportInfo; }

            foreach (DataRow dr in dt.Rows)//循环导入数据
            {
                try
                {
                    string _ClientCode = dr["客户编号"].ToString();//客户自编码
                    string _ClientName = dr["客户名称"].ToString();

                    if (string.IsNullOrEmpty(_ClientCode))// && string.IsNullOrEmpty(_ClientName)
                    {
                        ImportInfo += "导入序列号为" + dr["序号"].ToString() + "的数据行时找不到门店编码，跳过此行记录\r\n";//门店名称和
                        continue;
                    }
                    IList<CM_Client> _listClient = CM_ClientBLL.GetModelList(" ApproveFlag=1 AND ClientType=3 AND OwnerType=3 AND OwnerClient=" + _template.Model.ClientID + "AND EXISTS(SELECT 1 FROM MCS_CM.dbo.CM_ClientSupplierInfo WHERE Supplier=" + _template.Model.ClientID + " AND Client= CM_Client.id  AND Code=" + _ClientCode + " ) ");
                    if (_listClient != null && _listClient.Count > 0)
                    {
                        ImportInfo += "导入序列号为" + dr["序号"].ToString() + "的数据行时找到相同编码的门店，跳过此行记录\r\n";
                        continue;
                    }

                    string _ClientLinkMan = dr["联系人"].ToString();
                    string _ClientAddress = dr["地址"].ToString();
                    string _ClientTel = dr["电话"].ToString();
                    string _ClientMobile = dr["手机"].ToString();
                    #region 获取线路
                    string _strClientVisitRoute = dr["销售线路"].ToString();//业务拜访模板Code
                    VST_Route _route = null;
                    if (!string.IsNullOrEmpty(_strClientVisitRoute))//优先匹配厂商级的拜访记录   
                    {
                        _route = VST_RouteBLL.GetModelList(" OwnerType IN(1,2) AND Code='" + _strClientVisitRoute + "'").FirstOrDefault();//IList<VST_Route>_listRoute
                        if (_route == null)//找不到厂商级的线路找经销商级别的
                        {
                            _route = VST_RouteBLL.GetModelList(" OwnerType=3 AND OwnerClient=" + _template.Model.ClientID + " AND Code='" + _strClientVisitRoute + "'").FirstOrDefault();
                        }
                    }
                    /*模板中存在负责业务时获取门店业代和拜访记录代码
                     int _ClientSalesMan = 0;//负责业务
                     int _ClientVisitRoute = 0;//业务拜访模板ID
                    string _strClientSalesMan = dr["负责业务"].ToString();
                    if (!string.IsNullOrEmpty(_strClientSalesMan))
                    {
                        IList<Org_Staff> _listStaff = Org_StaffBLL.GetStaffList(" OwnerClient=" + _template.Model.ClientID + " AND OwnerType=3 AND RealName='" + _strClientSalesMan + "'");
                        if (_listStaff != null && _listStaff.Count > 0)
                        {
                            _ClientSalesMan = _listStaff[0].ID;
                            IList<VST_Route> _listRoute = VST_RouteBLL.GetModelList(" OwnerClient=" + _template.Model.ClientID + " AND RelateStaff=" + _ClientSalesMan);
                            if (_listRoute != null && _listRoute.Count > 0) _ClientVisitRoute = _listRoute[0].ID;
                        }
                        else
                        {
                            Org_StaffBLL _bllStaff = new Org_StaffBLL();
                            CM_ClientBLL c = new CM_ClientBLL(_template.Model.ClientID);
                            CM_ClientManufactInfo manufactinfo = c.GetManufactInfo();
                            if (c != null && manufactinfo != null)
                            {
                                _bllStaff.Model.OrganizeCity = manufactinfo.OrganizeCity;
                                _bllStaff.Model.OfficialCity = c.Model.OfficialCity;
                            }
                            _bllStaff.Model.RealName = _strClientSalesMan;
                            _bllStaff.Model.Position = 1030;//默认为业务员
                            _bllStaff.Model.InsertStaff = _template.Model.InsertStaff;
                            _bllStaff.Model.OwnerClient = _template.Model.ClientID;
                            _bllStaff.Model.OwnerType = 3;
                            _bllStaff.Model.Dimission = 1;
                            _bllStaff.Model.ApproveFlag = 1;
                            _ClientSalesMan = _bllStaff.Add();
                            //创建默认员工线路
                            if (_ClientSalesMan > 0)
                            {
                                if (_bllStaff.Model.Position == 1030)
                                {
                                    VST_RouteBLL routebll = new VST_RouteBLL();
                                    routebll.Model.Code = "R" + _ClientSalesMan.ToString();
                                    routebll.Model.Name = "线路-" + _bllStaff.Model.RealName;
                                    routebll.Model.RelateStaff = _ClientSalesMan;
                                    routebll.Model.OrganizeCity = _bllStaff.Model.OrganizeCity;
                                    routebll.Model.OwnerClient = _template.Model.ClientID;
                                    routebll.Model.OwnerType = 3;
                                    routebll.Model.ApproveFlag = 1;
                                    routebll.Model.EnableFlag = "Y";
                                    routebll.Model.InsertStaff = _template.Model.InsertStaff;
                                    _ClientVisitRoute = routebll.Add();
                                }
                            }
                        }
                    }*/
                    #endregion
                    string _ClientRemark = dr["备注"].ToString();

                    int _OwnerClient = _template.Model.ClientID;

                    #region 获取所在区域
                    int _SalesArea = 0;//区域
                    string _strSalesArea = dr["区域"].ToString();
                    if (!string.IsNullOrEmpty(_strSalesArea))
                    {
                        IList<CM_RTSalesArea_TDP> _listSalesArea = CM_RTSalesArea_TDPBLL.GetModelList(" OwnerClient= " + _OwnerClient.ToString() + " AND Name='" + _strSalesArea + "' ");
                        if (_listSalesArea != null && _listSalesArea.Count > 0) _SalesArea = _listSalesArea[0].ID;
                        else
                        {
                            CM_RTSalesArea_TDPBLL _bllRTSalesArea_TDPBLL = new CM_RTSalesArea_TDPBLL();
                            _bllRTSalesArea_TDPBLL.Model.Name = _strSalesArea;
                            _bllRTSalesArea_TDPBLL.Model.OwnerClient = _OwnerClient;
                            _bllRTSalesArea_TDPBLL.Model.Remark = "Excel批量导入";
                            _bllRTSalesArea_TDPBLL.Model.InsertStaff = _template.Model.InsertStaff;
                            _bllRTSalesArea_TDPBLL.Model.InsertTime = DateTime.Now;
                            _SalesArea = _bllRTSalesArea_TDPBLL.Add();
                        }
                    }
                    #endregion
                    #region 获取所在渠道
                    int _RTChannel = 0;//渠道
                    string _strRTChannel = dr["渠道"].ToString();
                    if (!string.IsNullOrEmpty(_strRTChannel))
                    {
                        IList<CM_RTChannel_TDP> _listRTChannel = CM_RTChannel_TDPBLL.GetModelList(" OwnerClient= " + _OwnerClient.ToString() + " AND Name='" + _strRTChannel + "' ");
                        if (_listRTChannel != null && _listRTChannel.Count > 0) _RTChannel = _listRTChannel[0].ID;
                        else
                        {
                            CM_RTChannel_TDPBLL _bllRTSalesArea_TDPBLL = new CM_RTChannel_TDPBLL();
                            _bllRTSalesArea_TDPBLL.Model.Name = _strRTChannel;
                            _bllRTSalesArea_TDPBLL.Model.OwnerClient = _OwnerClient;
                            _bllRTSalesArea_TDPBLL.Model.Remark = "Excel批量导入";
                            _bllRTSalesArea_TDPBLL.Model.InsertStaff = _template.Model.InsertStaff;
                            _bllRTSalesArea_TDPBLL.Model.InsertTime = DateTime.Now;
                            _RTChannel = _bllRTSalesArea_TDPBLL.Add();
                        }

                    }
                    #endregion

                    CM_ClientBLL _bll = new CM_ClientBLL();//Client表Code字段暂不启用，以FactoryCode作为厂商编码。经销商对门店的编码存于CM_ClientSupplierInfo表的Code字段
                    _bll.Model.FullName = _ClientName;
                    _bll.Model.ShortName = _ClientName;
                    _bll.Model.LinkManName = _ClientLinkMan;
                    _bll.Model.Address = _ClientAddress;
                    _bll.Model.TeleNum = _ClientTel;
                    _bll.Model.Mobile = _ClientMobile;
                    _bll.Model.Remark = string.IsNullOrEmpty(_ClientRemark) ? "Excel批量导入" : _ClientRemark;
                    _bll.Model.ClientType = 3;
                    _bll.Model.ApproveFlag = 1;
                    _bll.Model.InsertStaff = _template.Model.InsertStaff;
                    _bll.Model.OwnerType = 3;           //所属经销商
                    _bll.Model.OwnerClient = _OwnerClient;
                    int _ClientID = _bll.Add();
                    if (_ClientID > 0)
                    {
                        CM_ClientSupplierInfo _Supplierinfo = _bll.GetSupplierInfo(_OwnerClient);
                        if (_Supplierinfo == null)
                        {
                            _Supplierinfo = new CM_ClientSupplierInfo();
                            _Supplierinfo.Supplier = _OwnerClient;
                        }
                        _Supplierinfo.TDPChannel = _RTChannel;
                        _Supplierinfo.TDPSalesArea = _SalesArea;
                        _Supplierinfo.Code = _ClientCode;
                        _Supplierinfo.Remark = _ClientRemark;

                        CM_ClientBLL s = new CM_ClientBLL(_OwnerClient);
                        CM_ClientManufactInfo _manufactinfo = _bll.GetManufactInfo(s.Model.OwnerClient);
                        if (_manufactinfo == null)
                        {
                            _manufactinfo = new CM_ClientManufactInfo();
                            //门店所属区域为经销商对应区域
                            _manufactinfo.Manufacturer = s.Model.OwnerClient;
                            _manufactinfo.OrganizeCity = s.GetManufactInfo().OrganizeCity;
                        }

                        if (_route != null && _route.OwnerType == 3)//线路为经销商级别的存放在经销商表中
                            _Supplierinfo.VisitRoute = _route.ID;
                        else if (_route != null && (_route.OwnerType == 1 || _route.OwnerType == 2))//线路为厂商级别的存放在经销商表中
                            _manufactinfo.VisitRoute = _route.ID;

                        _bll.SetSupplierInfo(_Supplierinfo);
                        _bll.SetManufactInfo(_manufactinfo);
                    }
                }
                catch (Exception ex)
                {
                    ImportInfo += "导入序列号为" + dr["序号"].ToString() + "的数据行时出现错误，错误说明：" + ex.Message + "\r\n";
                    State = 5;
                    continue;
                }
            }
            if (State == 0) State = 3;
            ImportInfo += (State == 3 ? "导入完成！\r\n" : "");
            IPT_UploadTemplateMessageBLL _bllUploadTemplateMessage = new IPT_UploadTemplateMessageBLL();
            _bllUploadTemplateMessage.Model.TemplateID = TemplateID;
            _bllUploadTemplateMessage.Model.MessageType = State;
            _bllUploadTemplateMessage.Model.Content = ImportInfo;
            _bllUploadTemplateMessage.Add();
            return ImportInfo;

        }
        #endregion

        #region 4.导入供货单位信息
        public string DoImportSupplier(int TemplateID, int Client, ISheet Sheet, out int State)
        {
            string ImportInfo = "【供货单位信息】Excel表：";
            State = 0;
            IPT_UploadTemplateBLL _template = new IPT_UploadTemplateBLL(TemplateID);

            List<string> listSupplier = new List<string>() { "序号", "客户名称", "地址", "联系人", "电话" };

            DataTable dt = null;
            bool flag = VertifySheet(Sheet, listSupplier, out dt, ref ImportInfo);
            if (!flag) { State = 4; return ImportInfo; }

            foreach (DataRow dr in dt.Rows)//循环导入数据
            {
                try
                {
                    string _SupplierName = dr["客户名称"].ToString();
                    string _SupplierAddress = dr["地址"].ToString();
                    string _SupplierTeleNum = dr["电话"].ToString();
                    string _SupplierLinkMan = dr["联系人"].ToString();

                    if (string.IsNullOrEmpty(_SupplierName))
                    {
                        ImportInfo += string.Format("序号为{0}的行客户名称均为空，跳过此行信息\r\n", dr["序号"]);
                        continue;
                    }
                    IList<CM_Client> _listClient = CM_ClientBLL.GetModelList(" ClientType=1 AND OwnerType=3 AND OwnerClient=" + _template.Model.ClientID.ToString() + " AND FullName='" + _SupplierName + "'");
                    if (_listClient != null && _listClient.Count > 0)
                    {
                        ImportInfo += string.Format("序号为{0},名称为{1}的客户已存在，跳过此行信息\r\n", dr["序号"], _SupplierName);
                        continue;
                    }
                    CM_ClientBLL _bllClient = new CM_ClientBLL();
                    _bllClient.Model.FullName = _SupplierName;
                    _bllClient.Model.ShortName = _SupplierName;
                    _bllClient.Model.Address = _SupplierAddress;
                    _bllClient.Model.TeleNum = _SupplierTeleNum;
                    _bllClient.Model.Remark = "Excel批量导入";
                    _bllClient.Model.ClientType = 1;
                    _bllClient.Model.OwnerType = 3;
                    _bllClient.Model.OwnerClient = _template.Model.ClientID;
                    _bllClient.Add();
                }
                catch (Exception ex)
                {
                    ImportInfo += "导入序列号为" + dr["序号"].ToString() + "的数据行时出现错误，错误说明：" + ex.Message + "\r\n";
                    State = 5;
                    continue;
                }
            }
            if (State == 0) State = 3;
            ImportInfo += (State == 3 ? "导入完成！\r\n" : "");
            IPT_UploadTemplateMessageBLL _bllUploadTemplateMessage = new IPT_UploadTemplateMessageBLL();
            _bllUploadTemplateMessage.Model.TemplateID = TemplateID;
            _bllUploadTemplateMessage.Model.MessageType = State;
            _bllUploadTemplateMessage.Model.Content = ImportInfo;
            _bllUploadTemplateMessage.Add();
            return ImportInfo;
        }
        #endregion

        #region 5.导入期初库存
        public string DoImportInventory(int TemplateID, int Client, ISheet Sheet, out int State)
        {
            string ImportInfo = "【期初库存】Excel表：";
            State = 0;
            IPT_UploadTemplateBLL _template = new IPT_UploadTemplateBLL(TemplateID);

            List<string> listSupplier = new List<string>() { "序号", "商品编码", "商品名称", "批号", "生产日期", "库存成本价", "整件数量", "零散数量" };

            DataTable dt = null;
            bool flag = VertifySheet(Sheet, listSupplier, out dt, ref ImportInfo);
            if (!flag) { State = 4; return ImportInfo; }

            foreach (DataRow dr in dt.Rows)//循环导入数据
            {
                try
                {
                    int _pdtTrafficPackaging = (!string.IsNullOrEmpty(dr["整件数量"].ToString())) ? Convert.ToInt32(dr["整件数量"].ToString()) : 0;
                    int _pdtPackaging = (!string.IsNullOrEmpty(dr["零散数量"].ToString())) ? Convert.ToInt32(dr["零散数量"].ToString()) : 0;
                    if (_pdtPackaging == 0 && _pdtTrafficPackaging == 0)
                    {
                        ImportInfo += string.Format("序号为{0}的行导入库存数量为0，跳过此行信息\r\n", dr["序号"]);
                        continue;
                    }
                    string _pdtCode = dr["商品编码"].ToString();
                    IList<PDT_ProductExtInfo> _listPdtExtInfo = PDT_ProductExtInfoBLL.GetProductExtInfoList_BySupplier(_template.Model.ClientID);
                    PDT_ProductExtInfo _ProductExtInfo = _listPdtExtInfo.FirstOrDefault(m => m.Code == _pdtCode);
                    if (_ProductExtInfo == null || _ProductExtInfo.ID == 0)
                    {
                        ImportInfo += string.Format("序号为{0}的行找不到对应编码的产品，跳过此行信息\r\n", dr["序号"]);
                        continue;
                    }
                    int _pdtID = _ProductExtInfo.Product;

                    //string _pdtName = dr["商品名称"].ToString();
                    string _pdtLotNumber = dr["批号"].ToString();
                    DateTime _pdtProductDate; DateTime.TryParse(dr["生产日期"].ToString(), out _pdtProductDate);
                    decimal _pdtPrice = (!string.IsNullOrEmpty(dr["库存成本价"].ToString())) ? Convert.ToDecimal(dr["库存成本价"].ToString()) : 0;
                    int _pdtQuantity = new PDT_ProductBLL(_pdtID).Model.ConvertFactor * _pdtTrafficPackaging + _pdtPackaging;


                    #region 获取经销商主仓库
                    int _WareHouse = 0;
                    IList<CM_WareHouse> _listWareHouse = CM_WareHouseBLL.GetModelList("  Classify=1 AND ApproveFlag=1 AND Client=" + _template.Model.ClientID.ToString());
                    if (_listWareHouse != null || _listWareHouse.Count > 0) _WareHouse = _listWareHouse[0].ID;
                    else
                    {
                        CM_WareHouseBLL _bllWareHouse = new CM_WareHouseBLL();
                        _bllWareHouse.Model.Name = "默认主仓库";
                        _bllWareHouse.Model.Client = _template.Model.ClientID;
                        _bllWareHouse.Model.InsertStaff = _template.Model.InsertStaff;
                        _bllWareHouse.Model.ApproveFlag = 1;
                        _WareHouse = _bllWareHouse.Add();
                    }
                    #endregion

                    int _result = INV_InventoryBLL.IncreaseQuantity(_WareHouse, _pdtID, _pdtLotNumber, _pdtPrice, _pdtQuantity);
                    if (_result != 0)
                    {
                        ImportInfo += "导入序列号为" + dr["序号"].ToString() + "的销量时出现错误";
                        State = 5;
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    ImportInfo += "导入序列号为" + dr["序号"].ToString() + "的数据行时出现错误，错误说明：" + ex.Message + "\r\n";
                    State = 5;
                    continue;
                }
            }
            if (State == 0) State = 3;
            ImportInfo += (State == 3 ? "导入完成！\r\n" : "");
            IPT_UploadTemplateMessageBLL _bllUploadTemplateMessage = new IPT_UploadTemplateMessageBLL();
            _bllUploadTemplateMessage.Model.TemplateID = TemplateID;
            _bllUploadTemplateMessage.Model.MessageType = State;
            _bllUploadTemplateMessage.Model.Content = ImportInfo;
            _bllUploadTemplateMessage.Add();
            return ImportInfo;
        }
        #endregion


        /// <summary>
        /// 验证传入的Sheet表是否与预设的列一致
        /// </summary>
        /// <param name="Sheet">需要验证的Sheet表</param>
        /// <param name="ListCol">需要验证的列名</param>
        /// <param name="dt">从Sheet表中提取出的数据</param>
        /// <param name="ImportMsg">导入信息信息</param>
        /// <returns></returns>
        private bool VertifySheet(ISheet Sheet, List<string> ListCol, out DataTable dt, ref string ImportMsg)
        {
            string errorInfo = string.Empty;
            dt = DataTableFromExcel.ImportDt(Sheet, 0, true, out errorInfo);

            if (!string.IsNullOrEmpty(errorInfo)) { ImportMsg += errorInfo; ; return false; }
            if (dt == null || dt.Rows.Count == 0) { ImportMsg += "当前Sheet表无数据"; ; return false; }
            if (dt.Columns.Count != ListCol.Count) { ImportMsg += "当前Sheet表列数量错误"; ; return false; }
            //判断列名是否正确
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string _name = string.IsNullOrEmpty(dt.Columns[i].ColumnName) ? "" : dt.Columns[i].ColumnName;
                if (_name != ListCol[i]) { ImportMsg += string.Format("无法找到产品表第{0}列、列名为{1}的列", (i + 1).ToString(), ListCol[i]); ; return false; }
            }
            return true;
        }

    }
}