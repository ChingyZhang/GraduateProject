
// ===================================================================
// 文件： PM_SalaryDAL.cs
// 项目名称：
// 创建时间：2011-11-27
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Promotor;
using MCSFramework.SQLDAL.Promotor;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.CM;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using System.Text;
using MCSFramework.BLL.FNA;
namespace MCSFramework.BLL.Promotor
{
    /// <summary>
    ///PM_SalaryBLL业务逻辑BLL类
    /// </summary>
    public class PM_SalaryBLL : BaseComplexBLL<PM_Salary, PM_SalaryDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_SalaryDAL";
        private PM_SalaryDAL _dal;

        #region 构造函数
        ///<summary>
        ///PM_SalaryBLL
        ///</summary>
        public PM_SalaryBLL()
            : base(DALClassName)
        {
            _dal = (PM_SalaryDAL)_DAL;
            _m = new PM_Salary();
        }

        public PM_SalaryBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_SalaryDAL)_DAL;
            FillModel(id);
        }

        public PM_SalaryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_SalaryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PM_Salary> GetModelList(string condition)
        {
            return new PM_SalaryBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 提交费用申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Submit(int staff, int taskid, int feetype)
        {
            return _dal.Submit(_m.ID, staff, taskid, feetype);
        }

        /// <summary>
        /// 获取促销员工资申请单的总金额
        /// </summary>
        /// <returns></returns>
        public decimal GetSumSalary()
        {
            return _dal.GetSumSalary(_m.ID);
        }

        /// <summary>
        /// 获取促销员工资申请单的总金额
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static decimal GetSumSalary(int ID)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumSalary(ID);
        }

        /// <summary>
        /// 生成指定管理片区指定月份的促销员工资
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public static int GenerateSalary(int OrganizeCity, int DIClient, int AccountMonth, int Staff)
        {
            int Result = 0, flag = 0;
            #region 获取该月份内在职的促销员名单
            DataTable promotors = PM_PromotorBLL.GetByDIClient(OrganizeCity, DIClient, AccountMonth);
            if (promotors.Rows.Count == 0) return -2;//无促销员

            //获取促销员类别，根据类别生成工资        
            Dictionary<string, Dictionary_Data> SalsryClassify = DictionaryBLL.GetDicCollections("PM_SalaryClassify", true);
            foreach (string key in SalsryClassify.Keys) //非专职、非流导，认为是兼职导购
            {
                //判断是否生成，根据导购逐个判断
                //if (GetModelList("OrganizeCity=" + OrganizeCity.ToString() + " AND State IN (1,2,3) AND AccountMonth=" + AccountMonth.ToString() + " AND MCS_SYS.dbo.UF_Spilt2('MCS_Promotor.dbo.PM_Salary',ExtPropertys,'Client')=" + DIClient.ToString() + " AND MCS_SYS.dbo.UF_Spilt2('MCS_Promotor.dbo.PM_Salary',ExtPropertys,'PMClassfiy')=" + key).Count > 0)
                //{
                //    如果已生成过工资，则不再生成
                //    continue;
                //}
                Result = DoGenerateSalary(OrganizeCity, DIClient, AccountMonth, Staff, key, promotors);
                if (Result != 0)
                    flag = Result;
                if (Result < 0) return Result;
            }
            #endregion
            return flag;
        }

        private static int DoGenerateSalary(int OrganizeCity, int DIClient, int AccountMonth, int Staff, string SalaryClassify, DataTable promotors)
        {
            if (promotors.Select("SalaryClassify=0").Length > 0)
            {
                return -4;
            }
            string PMClassifys = SalaryClassify == "1" ? "1,4,5" : "3";//专职工资1,兼职工资2 (根据底薪模式进行筛选)


            #region 过滤促销员类别
            DataRow[] row_promotr = promotors.Select("SalaryClassify IN (" + PMClassifys + ")");
            if (row_promotr.Length == 0) return 1;//该类别无促销员
            #endregion

            AC_AccountMonth month = new AC_AccountMonthBLL(AccountMonth).Model;
            int monthDays = month.EndDate.Subtract(month.BeginDate).Days + 1;
            #region 生成工资单头信息
            PM_SalaryBLL bll = new PM_SalaryBLL();
            bll.Model.OrganizeCity = OrganizeCity;
            bll.Model.AccountMonth = AccountMonth;
            bll.Model.InputStaff = Staff;
            bll.Model.State = 1;
            bll.Model["Client"] = DIClient.ToString();
            bll.Model["PMClassfiy"] = SalaryClassify;
            #endregion

            #region 依次为每个促销员计算工资单
            decimal target = 0; //目标销量         
            foreach (DataRow row in row_promotr)
            {
                PM_Promotor p = new PM_PromotorBLL(int.Parse(row["Promotor"].ToString())).Model;

                if (row["ErrType"].ToString() == "1" || PM_Salary_GetStateByPromotor(p.ID, AccountMonth) > 0)
                {
                    continue;
                }

                PM_SalaryDetail detail = new PM_SalaryDetail();
                #region 获取促销员工资相关信息
                IList<PM_SalaryDataObject> PM_DataList = PM_SalaryDataObjectBLL.GetModelList("ApproveFlag=1 AND Promotor=" + p.ID.ToString() + " and AccountMonth=" + AccountMonth.ToString());
                if (PM_DataList.Count == 0) return -3;//无出勤天数及调整信息

                IList<PM_PromotorSalary> PM_Salary = PM_PromotorSalaryBLL.GetModelList("Promotor=" + p.ID.ToString() + " AND State=3 AND ApproveFlag=1");
                if (PM_Salary.Count == 0) return -4; //无薪酬定义
                int[] InsurancTyps = { 1 };//如果工资生成月是导购入职月且保险类型为【给予补贴】，则导购的社保方式为工伤
                string InsuranceMode = p.BeginWorkDate >= month.BeginDate && Array.IndexOf(InsurancTyps, PM_Salary[0].InsuranceMode) != -1 ? "2" : PM_Salary[0].InsuranceMode.ToString();


                #region 取终端门店导购费用协议管理内的管理费分摊比例及提成分摊比例

                #region 实时保存导购信息
                detail["OrganizeCity"] = p.OrganizeCity.ToString();
                detail["Dimission"] = p.Dimission.ToString();
                IList<PM_PromotorInRetailer> rtlist = PM_PromotorInRetailerBLL.GetModelList("Promotor=" + p.ID.ToString());
                string rts = "";
                foreach (PM_PromotorInRetailer m in rtlist)
                {
                    rts += m.Client.ToString() + ",";
                }
                detail["RetailerS"] = rts != "" ? rts.Substring(0, rts.Length - 1) : "";
                detail["BankName"] = p["BankName"];
                detail["AccountCode"] = p["AccountCode"];
                detail["Classfiy"] = p["Classfiy"];
                detail["MobileNumber"] = p.MobileNumber;
                detail["EndWorkDate"] = p.EndWorkDate.ToString();
                detail["IDCode"] = p["IDCode"];
                detail["RTManager"] = rtlist.Count > 0 ? new CM_ClientBLL(rtlist[0].Client).Model.ClientManager.ToString() : "0";
                detail["BasePayMode"] = PM_Salary[0].ID.ToString();
                detail["MonthDays"] = monthDays.ToString();
                detail["InsuranceMode"] = InsuranceMode;
                detail["InsuranceSubsidy"] = InsuranceMode == "1" ? PM_Salary[0].InsuranceSubsidy.ToString() : "0";
                detail["BeginWorkDate"] = p.BeginWorkDate.ToString();
                #endregion
                decimal PromotorCostRate = 0;//促管费分摊比例
                decimal PromotorAwardRate = 100;//促销员提成分摊比例
                IList<CM_Contract> CM_ContractList = CM_ContractBLL.GetModelList("Client=" + row["Client"].ToString() + " AND Classify=3 AND ApproveFlag=1 AND State IN (3,9) AND CM_Contract.EndDate >= '" + month.BeginDate.ToString("yyyy-MM-dd") + "'AND CM_Contract.BeginDate<='" + month.EndDate.ToString("yyyy-MM-dd") + "'");
                if (CM_ContractList.Count > 0)
                {
                    decimal.TryParse(CM_ContractList[0]["PromotorCostRate"], out PromotorCostRate);
                    decimal.TryParse(CM_ContractList[0]["PromotorAwardRate"], out PromotorAwardRate);
                }
                #endregion

                //保险
                IList<PM_StdInsuranceCost> InsuranceCostList = PM_StdInsuranceCostBLL.GetModelList("InsuranceMode=" + InsuranceMode);
                #endregion

                detail.Promotor = p.ID;

                #region 计算促销员提成
                //计算促销员当月实际销量数量或金额 
                //DataTable dt = PM_Salary_GetActSalesVolume(AccountMonth, p.ID);

                target = PM_DataList[0].SalesTarget;
                detail.ActWorkDays = PM_DataList[0].ActWorkDays;
                detail.ActSalesVolume = PM_DataList[0].Data12;//实际销售额             
                detail["MixesSales"] = PM_DataList[0].Data13.ToString();//冲调部销量
                detail["MilkPowderSales"] = PM_DataList[0].Data14.ToString();//奶粉部销量
                detail.Bonus = PM_DataList[0].ActWorkDays > 0 && detail.ActSalesVolume > 0 ? Math.Round(PM_DataList[0].Data11 + PM_DataList[0].Data18, 1) : 0;//按销售数量提成，每一分对应一元  Data11总提成
                #endregion

                detail.Pay9 = 0;
                //底薪补贴
                decimal BasePaySubsidy = PM_Salary[0].BasePaySubsidyBeginDate <= month.EndDate && (PM_Salary[0].BasePaySubsidyEndDate == new DateTime(1900, 1, 1) || PM_Salary[0].BasePaySubsidyEndDate.AddDays(1) > month.BeginDate) ? PM_Salary[0].BasePaySubsidy : 0;
                detail.Pay2 = Math.Round(BasePaySubsidy / monthDays * detail.ActWorkDays, 0, MidpointRounding.AwayFromZero);

                #region 获取薪酬信息
                #region 计算底薪
                int basepaymode = PM_Salary[0].BasePayMode;
                if (basepaymode >= 4 && PM_DataList[0]["ISFloating"] != "1")//如果暂不执行浮动底薪，则执行固定底薪
                {
                    basepaymode = 1;
                }

                switch (basepaymode)
                {
                    case 1:
                        //固定底薪
                        detail.Pay1 = detail.ActWorkDays > 0 ? Math.Round(PM_Salary[0].BasePay / monthDays * detail.ActWorkDays, 0, MidpointRounding.AwayFromZero) : 0;
                        break;
                    case 2:
                        //坎级底薪
                        break;
                    case 3:
                        //兼职底薪
                        /*
                        1.	底薪计算原则:总销量*8%(我司承担)；
                        2.	提成计算原则：总销量*6%（根据以分摊的批复为准，默认为我司不承担。）；
                        */
                        detail.Pay1 = PM_DataList[0].ActWorkDays > 0 ? Math.Round(PM_DataList[0].Data15 * 0.08m, 0, MidpointRounding.AwayFromZero) : 0;
                        if (detail.Pay1 > 900) detail.Pay1 = 900;//2013-05-08 加入900上限
                        detail.Bonus = PM_DataList[0].ActWorkDays > 0 && PM_DataList[0].Data15 > 0 ? Math.Round(PM_DataList[0].Data11 + PM_DataList[0].Data18, 1, MidpointRounding.AwayFromZero) : 0;
                        break;
                    case 4://非华南浮动底薪
                        detail.Pay1 = detail.ActWorkDays > 0 ? Math.Round(PM_Salary[0].BasePay / monthDays * detail.ActWorkDays, 0, MidpointRounding.AwayFromZero) : 0;
                        if (PM_Salary[0]["ISArriveTarget"] != "1" && decimal.Parse(detail["MilkPowderSales"]) >= decimal.Parse(PM_Salary[0]["FloatingTarget"]))
                        {
                            PM_Salary[0]["ISArriveTarget"] = "1";
                            PM_PromotorSalaryBLL _bll = new PM_PromotorSalaryBLL(PM_Salary[0].ID);
                            _bll.Model = PM_Salary[0];
                            _bll.Update();
                        }
                        #region 浮动底薪上限
                        //浮动底薪上限=ROUND(IF(底薪费率*浮动底薪上限任务量<(底薪标准+底薪补贴),(底薪标准+底薪补贴),IF(底薪费率*浮动底薪上限任务量>(底薪标准+底薪补贴)*1.5,(底薪标准+底薪补贴)*1.5,底薪费率*浮动底薪上限任务量)),0)

                        decimal FloatingBasePay = 0m;//浮动底薪上限
                        if (decimal.Parse(PM_Salary[0]["BaseFeeRate"]) / 100m * decimal.Parse(PM_Salary[0]["FloatingTarget"]) < PM_Salary[0].BasePay + BasePaySubsidy)
                        {
                            FloatingBasePay = PM_Salary[0].BasePay + BasePaySubsidy;
                        }
                        else if (decimal.Parse(PM_Salary[0]["BaseFeeRate"]) / 100m * decimal.Parse(PM_Salary[0]["FloatingTarget"]) > (PM_Salary[0].BasePay + BasePaySubsidy) * 1.5m)
                        {
                            FloatingBasePay = (PM_Salary[0].BasePay + BasePaySubsidy) * 1.5m;
                        }
                        else
                        {
                            FloatingBasePay = decimal.Parse(PM_Salary[0]["BaseFeeRate"]) / 100m * decimal.Parse(PM_Salary[0]["FloatingTarget"]);
                        }
                        #endregion
                        if (PM_Salary[0]["ISArriveTarget"] == "1" && detail.ActWorkDays > 0 && decimal.Parse(PM_Salary[0]["FloatingTarget"]) > 0)
                        {
                            // 浮动底薪补贴= “当月奶粉部实销”÷（“浮动底薪上限任务量”÷会计月天数×实际出勤天数）×浮动底薪上限÷会计月天数×实际出勤天数－(底薪标准+底薪补贴)÷会计月天数×实际出勤天数
                            //a)	当“当月奶粉部实销”÷（“浮动底薪上限任务量”÷会计月天数×实际出勤天数）×浮动底薪上限>“浮动底薪上限”，按“浮动底薪上限”的1.0倍计算；
                            //b)	当“当月奶粉部实销”÷（“浮动底薪上限任务量”÷会计月天数×实际出勤天数）×浮动底薪上限<(“底薪标准”+ “底薪补贴”)的1.0倍，按(“底薪标准”+ “底薪补贴”)的1.0倍计算；                         

                            decimal compare = decimal.Parse(detail["MilkPowderSales"]) / (decimal.Parse(PM_Salary[0]["FloatingTarget"]) / monthDays * detail.ActWorkDays) * FloatingBasePay;
                            if (compare > FloatingBasePay) compare = FloatingBasePay;
                            if (compare < PM_Salary[0].BasePay + BasePaySubsidy) compare = PM_Salary[0].BasePay + BasePaySubsidy;

                            detail.Pay9 = (compare > PM_Salary[0].BasePay + BasePaySubsidy) ? ((compare - PM_Salary[0].BasePay - BasePaySubsidy) / monthDays * detail.ActWorkDays) : 0;
                        }
                        else if (detail.ActWorkDays > 0)
                        {
                            //浮动底薪补贴=当月奶粉部实销×底薪费率÷会计月天数×实际出勤天数－(底薪标准+底薪补贴)÷会计月天数×实际出勤天数
                            //a)	当“当月奶粉部实销×底薪费率”>(“底薪标准”+ “底薪补贴”)的1.5倍，按(“底薪标准”+ “底薪补贴”)的1.5倍计算；
                            //b)	当“当月奶粉部实销×底薪费率”<(底薪标准+底薪补贴)的1.0倍，按(“底薪标准”+ “底薪补贴”)的1.0倍计算；                          
                            decimal compare = decimal.Parse(detail["MilkPowderSales"]) * decimal.Parse(PM_Salary[0]["BaseFeeRate"]) / 100m;
                            if (compare > 1.5m * (PM_Salary[0].BasePay + BasePaySubsidy)) compare = 1.5m * (PM_Salary[0].BasePay + BasePaySubsidy);
                            if (compare < PM_Salary[0].BasePay + BasePaySubsidy) compare = PM_Salary[0].BasePay + BasePaySubsidy;
                            detail.Pay9 = (compare > (PM_Salary[0].BasePay + BasePaySubsidy)) ? ((compare - PM_Salary[0].BasePay - BasePaySubsidy) / monthDays * detail.ActWorkDays) : 0;
                        }
                        //浮动底薪上限： “底薪标准”+“底薪补贴”+ “浮动底薪补贴”的最高限额为“底薪标准”+“底薪补贴”的1.5倍
                        if (PM_Salary[0].BasePay + BasePaySubsidy + detail.Pay9 > FloatingBasePay)
                        {
                            detail.Pay9 = FloatingBasePay - PM_Salary[0].BasePay - BasePaySubsidy;
                        }
                        detail.Pay9 = Math.Round(detail.Pay9, 0, MidpointRounding.AwayFromZero);
                        break;
                    case 5://华南浮动底薪
                        detail.Pay1 = detail.ActWorkDays > 0 ? Math.Round(PM_Salary[0].BasePay / monthDays * detail.ActWorkDays, 0, MidpointRounding.AwayFromZero) : 0;
                        decimal sales = 0;
                        if (PM_Salary[0]["SalesType"] == "1")
                        {
                            sales = decimal.Parse(detail["MilkPowderSales"]);
                        }
                        else
                        {
                            sales = detail.ActSalesVolume;
                        }
                        //A.	（底薪标准+浮动底薪补贴100元）÷当月实销<=底薪费率
                        //B.	（底薪标准+浮动底薪补贴200元）÷当月实销<=底薪费率
                        //C.	（底薪标准+浮动底薪补贴300元）÷当月实销<=底薪费率
                        decimal FloatingSubsidy = decimal.Parse(PM_Salary[0]["BaseFeeRate"]) / 100m * sales - PM_Salary[0].BasePay;
                        if (FloatingSubsidy < 100)
                        {
                            FloatingSubsidy = 0;
                        }
                        else
                        {
                            FloatingSubsidy = FloatingSubsidy >= 300 ? 300 : (int)Math.Floor(FloatingSubsidy) / 100 * 100;
                        }
                        detail.Pay9 = FloatingSubsidy;
                        break;
                }
                detail.Pay9 = Math.Round(detail.Pay9, 0, MidpointRounding.AwayFromZero);
                #endregion

                #region 计算工龄工资
                //----------------------------工龄工资---------------------------------------------
                //1.陕西营业部-25、云南营业部-29、贵州营业部-31、冀南营业部-34、新疆营业部-30、甘肃营业部-49、冀北营业部-4240，北京营业部-37 销量>=0  50	100	150
                //2.浙江营业部-15、湘北营业部-17、东三省营业部-40、鲁东营业部-48、[黑龙江营业部-41]、[吉林营业部-50]、湘南营业部-4239	，销量>=4000元	 50	100	150
                //3.苏南营业部-12、苏北营业部-13、 湖北营业部-14、四川营业部-23、川南营业部-26、重庆营业部-27，销量>=5000元	                     50	100	150
                //4.皖北营业部-11、江西营业部-18、福建营业部-19、广州营业部-20、粤西营业部-21、粤东营业部-22、广西营业部-24、皖南营业部-4238
                //山西营业部-35、鲁西营业部-36、内蒙营业部-38、豫南营业部-46、豫北营业部-47,豫中营业部-4237，销量>=6000	 50	100	150
                //系统根据导购员的入职时间及福利标准得到工龄补贴标准，再用工龄补贴标准/30*天数(有销量限制的，当不足月的，按销量÷天数×30) 自动计算
                int PMOrganizeCity = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", p.OrganizeCity, 3);
                if (PM_Salary[0].SeniorityPayMode == 1 && detail.ActWorkDays > 0)
                {
                    int[] City1 = new int[] { 25, 29, 31, 34, 30, 49, 4240, 37 };
                    int[] City2 = new int[] { 15, 17, 40, 48, 41, 50, 4239 };
                    int[] City3 = new int[] { 12, 13, 14, 23, 26, 27 };
                    int[] City4 = new int[] { 11, 18, 19, 20, 21, 22, 24, 35, 36, 38, 46, 47, 4237, 4238 };
                    DateTime Enddate = p.Dimission == 1 ? month.EndDate : p.EndWorkDate;
                    //计算工龄
                    int SeniorityYear = Enddate.Year - p.BeginWorkDate.Year - 1;

                    if (Enddate >= p.BeginWorkDate.AddYears(Enddate.Year - p.BeginWorkDate.Year))
                    {
                        SeniorityYear++;
                    }

                    SeniorityYear = SeniorityYear > 0 ? SeniorityYear : 0;
                    decimal Seniority = SeniorityYear * 50 > 150 ? 150 : SeniorityYear * 50;

                    if (Array.IndexOf(City1, PMOrganizeCity) != -1 || Array.IndexOf(City2, PMOrganizeCity) != -1 && detail.ActSalesVolume >= 4000 ||
                        (Array.IndexOf(City3, PMOrganizeCity) != -1 && detail.ActSalesVolume >= 5000) || (Array.IndexOf(City4, PMOrganizeCity) != -1 && detail.ActSalesVolume >= 6000))
                    {
                        detail.Pay3 = Math.Round(Seniority / monthDays * detail.ActWorkDays, 0, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        detail.Pay3 = 0;
                    }
                }
                else
                {
                    detail.Pay3 = 0;
                }
                #endregion

                //--------------------------我司薪酬明细-----------------------------------------------
                if (InsuranceMode == "1" && PM_Salary[0].InsuranceSubsidy > 0)
                {
                    detail.Pay5 = Math.Round(Convert.ToDecimal(PM_Salary[0].InsuranceSubsidy) / monthDays * detail.ActWorkDays, 0, MidpointRounding.AwayFromZero);
                }
                detail.Pay6 = Math.Round(detail.Bonus * (100 - PromotorAwardRate) / 100, 0, MidpointRounding.AwayFromZero);
                detail.Pay7 = Math.Round(PM_DataList[0].Data10, 1, MidpointRounding.AwayFromZero);    //奖惩项
                detail.Pay8 = InsuranceCostList.Count > 0 ? Math.Round(InsuranceCostList[0].StaffCost, 1, MidpointRounding.AwayFromZero) : 0;
                detail.Pay17 = Math.Round(PM_Salary[0].DIFeeSubsidy / monthDays * detail.ActWorkDays, 1, MidpointRounding.AwayFromZero);//我司通讯补贴2014-05-12修改
                for (int i = 1; i < 11; i++)
                {
                    detail.Sum1 += Math.Round(decimal.Parse(detail["Pay" + i.ToString()]), 1);
                }
                detail.Sum1 += detail.Pay17;//我司通讯补贴算导购实得
                //--------------------------我司薪酬明细-----------------------------------------------
                //  社保费用额                    
                detail.Pay12 = InsuranceCostList.Count > 0 ? Math.Round(InsuranceCostList[0].StaffCost + InsuranceCostList[0].CompanyCost, 1, MidpointRounding.AwayFromZero) : 0;

                if (InsuranceMode == "8") detail.Pay13 = Math.Round(PM_DataList[0].Data17, 1, MidpointRounding.AwayFromZero);          //自购保险
                detail.Pay14 = InsuranceCostList.Count > 0 ? Math.Round(InsuranceCostList[0].ServiceCost, 0, MidpointRounding.AwayFromZero) : 0;  //派遣服务费

                //--------------------------经销商结构-----------------------------------------------
                detail.Pay16 = Math.Round(PM_Salary[0].DIBasePaySubsidy / monthDays * detail.ActWorkDays, 1, MidpointRounding.AwayFromZero);

                detail.Pay18 = Math.Round(detail.Bonus * PromotorAwardRate / 100, 1, MidpointRounding.AwayFromZero);
                detail.Pay19 = Math.Round(PM_DataList[0].Data19, 1, MidpointRounding.AwayFromZero);

                #region 当总销量为0时，底薪，浮动底薪补贴，我司底薪补贴，工龄工资，保底补贴，社保补贴，我司承担提成,经销商底薪补贴,经销商结构→费用补贴,经销商承担提成 都将数据刷为0
                if (detail.ActSalesVolume == 0)
                {
                    detail.Pay1 = 0;
                    detail.Pay2 = 0;
                    detail.Pay7 = 0;
                    detail.Pay9 = 0;
                    detail.Pay3 = 0;
                    detail.Pay4 = 0;
                    detail.Pay5 = 0;
                    detail.Pay6 = 0;
                    detail.Pay16 = 0;
                    detail.Pay17 = 0;
                    detail.Pay18 = 0;
                }
                #endregion
                //经销商承担薪资合计
                detail.Sum3 = Math.Round(detail.Pay16 + detail.Pay18 + detail.Pay19, 0, MidpointRounding.AwayFromZero);


                #region 计算保底补贴
                //导购实得薪资小计<保底工资       保底补贴=保底标准/30*出勤天数-(底薪+底薪补贴+工龄工资+我司承担提成+浮动底薪补贴+经销薪资合计)
                detail.Sum4 = Math.Round(detail.Pay1 + detail.Pay2 + detail.Pay3 + detail.Pay6 + detail.Pay9 + detail.Sum3, 1, MidpointRounding.AwayFromZero);
                if (detail.ActSalesVolume > 0 && detail.ActWorkDays > 0 && PM_Salary[0].MinimumWageBeginDate <= month.EndDate && detail.Sum4 < PM_Salary[0].MinimumWage &&
                    (PM_Salary[0].MinimumWageEndDate == new DateTime(1900, 1, 1) || PM_Salary[0].MinimumWageEndDate.AddDays(1) > month.BeginDate))
                {
                    detail.Pay4 = Math.Round((PM_Salary[0].MinimumWage / monthDays * detail.ActWorkDays - detail.Sum4), 1, MidpointRounding.AwayFromZero);
                    if (detail.Pay4 < 0) detail.Pay4 = 0;
                }

                //重新计算我司实发额
                detail.Sum1 = 0;
                for (int i = 1; i < 11; i++)
                {
                    if (i == 8) continue;
                    detail.Sum1 += Math.Round(decimal.Parse(detail["Pay" + i.ToString()]), 1, MidpointRounding.AwayFromZero);
                }
                detail.Sum1 += detail.Pay17;//我司通讯补贴算导购实得
                #endregion
                //含税工资额                 
                detail.Pay11 = Math.Round(detail.Sum1 - detail.Pay8, 1, MidpointRounding.AwayFromZero);//减社保代扣额
                detail.Tax = Math.Round(ComputeIncomeTax(detail.Pay11), 0, MidpointRounding.AwayFromZero);
                detail.Sum1 = Math.Round(detail.Pay11 - detail.Tax, 1, MidpointRounding.AwayFromZero);
                detail["PayAdjust_Approve"] = "0";

                if (detail.Sum1 < 0)
                {
                    detail["PayAdjust_Approve"] = (-detail.Sum1).ToString();
                    detail["Remark"] = "实得薪资小计不能为负，进行冲抵：" + detail["PayAdjust_Approve"];
                    detail.Sum1 = 0;
                    detail.Pay11 = detail.Sum1;

                }
                if (detail.Sum3 < 0)
                {
                    detail.Sum3 = 0;
                }
                //重新计算导购实得薪资小计
                detail.Sum4 = Math.Round(detail.Sum1 + detail.Sum3, 1, MidpointRounding.AwayFromZero);

                #endregion

                //我司导购费用小计
                detail.Sum2 = Math.Round(detail.Pay11 + detail.Pay12 + detail.Pay14 + detail.Pay13, 1, MidpointRounding.AwayFromZero);

                //导购薪资合计
                detail.TotalSalary = Math.Round(detail.Sum2 + detail.Sum3, 1, MidpointRounding.AwayFromZero);
                //----------------------------------管理费明细-----------------------------------------
                #region 判断是否要生成管理费申请单
                DataRow[] rows = promotors.Select("Promotor=" + p.ID);
                if (decimal.Parse(rows[0]["PMFee"].ToString()) > 0)
                {
                    detail["PMFee"] = detail.ActWorkDays == 0 ? "0" : (Math.Round(decimal.Parse(rows[0]["PMFee"].ToString()) + PM_DataList[0].Data20, 1, MidpointRounding.AwayFromZero)).ToString();
                    detail["IsGeneratePMFee"] = "2";
                }
                else
                {
                    detail["PMFee"] = detail.ActWorkDays == 0 ? "0" : (PM_Salary[0].RTManageCost + PM_DataList[0].Data20).ToString("0.0");
                }
                #endregion

                detail["PMFee1"] = PM_DataList[0].Data30.ToString("0.0");
                detail["PMFee2"] = (decimal.Parse(detail["PMFee"]) * (100 - PromotorCostRate) / 100).ToString("0.0");
                detail.CoPMFee = decimal.Parse(detail["PMFee1"]) + decimal.Parse(detail["PMFee2"]);
                detail.DIPMFee = decimal.Parse(detail["PMFee"]) * PromotorCostRate / 100;
                detail.PMFeeTotal = detail.CoPMFee + detail.DIPMFee;
                detail["FlagCancel"] = "2";

                bll.Items.Add(detail);
            }
            #endregion

            if (bll.Items.Count > 0)
                return bll.Add();
            else
                return 0;
        }

        /// <summary>
        /// 获取实际销量数量或金额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Promotor"></param>
        /// <returns></returns>
        public static DataTable PM_Salary_GetActSalesVolume(int AccountMonth, int Promotor)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.PM_Salary_GetActSalesVolume(AccountMonth, Promotor));
        }


        /// <summary>
        /// 根据工资计算方式计算奖金 1:最终系数值计算 2:分段累加计算
        /// </summary>
        /// <param name="SalesTarget"></param>
        /// <param name="ActSalesVolume"></param>
        /// <param name="ComputMethd"></param>
        /// <returns></returns>
        public static decimal PM_Salary_ComputeBonus(int Promotor, int AccountMonth, decimal SalesActual, decimal ActComplete)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.PM_Salary_ComputeBonus(Promotor, AccountMonth, SalesActual, ActComplete);
        }

        public static void UpdateAdjustRecord(int ID, int Staff, string OldAdjustCost, string AdjustCost, string promotorName)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            dal.UpdateAdjustRecord(ID, Staff, OldAdjustCost, AdjustCost, promotorName);
        }

        public static int PM_Salary_GetStateByPromotor(int promotor, int accountmonth)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.PM_Salary_GetStateByPromotor(promotor, accountmonth);
        }

        public static int Merge(int AccountMonth, string SalaryIDs, int Staff)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.Merge(AccountMonth, SalaryIDs, Staff);
        }

        public static decimal ComputeIncomeTax(decimal Income)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.ComputeIncomeTax(Income);
        }
        public static DataTable GetSummaryTotal(int AccountMonth, int OrganizeCity, int Level, int State, int Staff, int SalaryClassify)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummaryTotal(AccountMonth, OrganizeCity, Level, State, Staff, SalaryClassify));
        }

        public static DataTable GetSummary(int AccountMonth, int OrganizeCity, int Level, int State, int Staff, int SalaryClassify)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary(AccountMonth, OrganizeCity, Level, State, Staff, SalaryClassify));
        }

        public static DataTable GetDetailByState(int AccountMonth, int OrganizeCity, int Level, int State, int Staff, int SalaryClassify, int RTChannel)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetDetailByState(AccountMonth, OrganizeCity, Level, State, Staff, SalaryClassify, RTChannel));
        }

        /// <summary>
        /// 取消已生成的导购人员导购工资
        /// 是（值为1）：取消工资；否（值为2）：未取消工资
        /// </summary>
        /// <param name="SalaryDetailID">工资明细ID</param>
        /// <param name="DeleteStaff">取消人ID</param>
        /// <returns></returns>
        public static int CancelSalaryDetail(int SalaryDetailID, int DeleteStaff)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            PM_SalaryDetail detail = dal.GetDetailModel(SalaryDetailID);
            detail["FlagCancel"] = "1";
            Org_Staff staff = new Org_StaffBLL(DeleteStaff).Model;
            detail["DeleteStaff"] = staff.RealName;
            detail["DeleteTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            detail["DeletePosition"] = new Org_PositionBLL(staff.Position).Model.Name;
            dal.HeadID = detail.SalaryID;
            string a = detail.SalaryID + "|" + detail["FlagCancel"] + "|" + detail["DeleteTime"] + "|" + detail["DeleteStaff"] + "|" + detail["DeletePosition"];
            return dal.UpdateDetail(detail);

        }

        /// <summary>
        /// 导出工行和建行工资表所需数据
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="BankCode">银行代码:1：农行；2：建行</param>
        /// <param name="Remark">到处单中的备注</param>
        /// <returns></returns>
        public static DataTable Salary_Table_Export(int AccountMonth, int OrganizeCity, int BankCode, string Remark)
        {
            PM_SalaryDAL dal = (PM_SalaryDAL)DataAccess.CreateObject(DALClassName);
            try
            {
                SqlDataReader rd = dal.PM_Salary_Export(AccountMonth, OrganizeCity, BankCode);
                DataTable table = Tools.ConvertDataReaderToDataTable(rd);
                table.Columns.Add("Index", typeof(System.Int32));
                table.Columns["Index"].SetOrdinal(0);
                table.Columns.Add("Remark", typeof(System.String));
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    row["Index"] = i + 1;
                    row["Remark"] = Remark;
                }
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
