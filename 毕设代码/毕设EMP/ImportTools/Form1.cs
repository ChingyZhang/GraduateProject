using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using System.Threading;
using System.Web.Security;

namespace ImportTools
{
    public partial class Form1 : Form
    {
        DataTable dt_SalesStaff, dt_Distributor, dt_Retailer, dt_Promotor;
        public Form1()
        {
            InitializeComponent();
            bt_StartImport.Enabled = false;
            bt_StopImport.Enabled = false;

            CheckForIllegalCrossThreadCalls = false;

        }

        private void bt_RetrieveData_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            OleDbConnection oleConn = new OleDbConnection();
            dt_SalesStaff = new DataTable();
            dt_Distributor = new DataTable();
            dt_Retailer = new DataTable();
            dt_Promotor = new DataTable();

            try
            {
                oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=EXCEL 8.0";
                oleConn.Open();

                OleDbCommand oledbcmd = new OleDbCommand("SELECT * FROM [业务人员资料表$] where (导入标志='' or 导入标志 is null) and 序号 is not null ", oleConn);
                OleDbDataAdapter oleAdapter = new OleDbDataAdapter(oledbcmd);
                oleAdapter.Fill(dt_SalesStaff);

                oledbcmd = new OleDbCommand("SELECT * FROM [经销商基本资料表$] where (导入标志='' or 导入标志 is null) and 序号 is not null ", oleConn);
                oleAdapter = new OleDbDataAdapter(oledbcmd);
                oleAdapter.Fill(dt_Distributor);

                oledbcmd = new OleDbCommand("SELECT * FROM [终端门店资料表$] where (导入标志='' or 导入标志 is null) and 序号 is not null ", oleConn);
                oleAdapter = new OleDbDataAdapter(oledbcmd);
                oleAdapter.Fill(dt_Retailer);

                oledbcmd = new OleDbCommand("SELECT * FROM [促销员资料表$] where (导入标志='' or 导入标志 is null) and 序号 is not null ", oleConn);
                oleAdapter = new OleDbDataAdapter(oledbcmd);
                oleAdapter.Fill(dt_Promotor);

                oleConn.Close();

                DataColumn[] columns_staff = { dt_SalesStaff.Columns["序号"] };
                dt_SalesStaff.PrimaryKey = columns_staff;

                DataColumn[] columns_Distributor = { dt_Distributor.Columns["序号"] };
                dt_Distributor.PrimaryKey = columns_Distributor;

                DataColumn[] columns_Retailer = { dt_Retailer.Columns["序号"] };
                dt_Retailer.PrimaryKey = columns_Retailer;

                DataColumn[] columns_Promotor = { dt_Promotor.Columns["序号"] };
                dt_Promotor.PrimaryKey = columns_Promotor;

                gv_SalesStaff.DataSource = dt_SalesStaff;
                gv_Distributor.DataSource = dt_Distributor;
                gv_Retailer.DataSource = dt_Retailer;
                gv_Promotor.DataSource = dt_Promotor;

                //lbCount.Text = dt_SalesStaff.Rows.Count.ToString();
                progressBar1.Value = 0;

                //bt_RetrieveData.Enabled = false;
                bt_StartImport.Enabled = true;
                bt_StopImport.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                oleConn.Close();
            }
        }

        #region 开始导入
        private void bt_StartImport_Click(object sender, EventArgs e)
        {
            bt_RetrieveData.Enabled = false;
            bt_StartImport.Enabled = false;
            bt_StopImport.Enabled = true;

            backgroundWorker1.RunWorkerAsync();
        }
        #endregion

        #region 停止导入
        private void bt_StopImport_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
        #endregion

        #region 后台进程
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            threadStartImport(worker, e);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lb_Current.Text = ((int)e.UserState + 1).ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bt_RetrieveData.Enabled = true;
            bt_StartImport.Enabled = false;
            bt_StopImport.Enabled = false;
        }
        #endregion

        #region 更新导入标志信息
        /// <summary>
        /// 更新导入标志信息
        /// </summary>
        /// <param name="Flag"></param>
        /// <param name="Row"></param>
        /// <param name="Result"></param>
        /// <returns></returns>
        private int UpdateImportFlag(int Flag, int Row, string Result)
        {
            DataTable dt;
            string cmd;

            switch (Flag)
            {
                case 1:
                    dt = dt_SalesStaff;
                    cmd = "Update [业务人员资料表$] set 导入标志='" + Result + "' where 序号=";
                    break;
                case 2:
                    dt = dt_Distributor;
                    cmd = "Update [经销商基本资料表$] set 导入标志='" + Result + "' where 序号=";
                    break;
                case 3:
                    dt = dt_Retailer;
                    cmd = "Update [终端门店资料表$] set 导入标志='" + Result + "' where 序号=";
                    break;
                case 4:
                    dt = dt_Promotor;
                    cmd = "Update [促销员资料表$] set 导入标志='" + Result + "' where 序号=";
                    break;
                default:
                    return -1;
            }

            dt.Rows[Row]["导入标志"] = Result;                  //更新DataTable中的导入标志
            string xh = dt.Rows[Row]["序号"].ToString();
            if (char.IsDigit(xh, 0))
                cmd += xh;            //更新Excel中对应记录的失败信息
            else
                cmd += "'" + xh + "'";            //更新Excel中对应记录的失败信息

            OleDbConnection oleConn = new OleDbConnection();
            oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=EXCEL 8.0";
            OleDbCommand oledbcmd = new OleDbCommand(cmd, oleConn);
            try
            {
                oleConn.Open();
                oledbcmd.ExecuteNonQuery();
                oleConn.Close();
            }
            catch (Exception ex)
            {
                oleConn.Close();
                MessageBox.Show(ex.Message + cmd);
                return -2;
            }

            return 1;
        }

        #endregion


        private void threadStartImport(BackgroundWorker worker, DoWorkEventArgs e)
        {
            DataTable dt_OfficialCity = Addr_OfficialCityBLL.GetAllOfficialCity();
            DataTable dt_OrganizeCity = Addr_OrganizeCityBLL.GetAllOrganizeCity();
            DataTable dt_Position = Org_PositionBLL.GetAllPostion();
            bool bFind = false;

            StringBuilder _xhs_success = new StringBuilder("");//成功导入的记录序号（用于定期批量更新导入标志）
            string XH = "";

            #region 导入销售人员资料
            try
            {
                if (cbx_Promotor.Checked)
                {
                    lb_Count.Text = dt_SalesStaff.Rows.Count.ToString();
                    tabControl1.SelectedIndex = 0;
                    XH = "";
                    for (int i = 0; i < dt_SalesStaff.Rows.Count; i++)
                    {
                        worker.ReportProgress((i + 1) * 100 / this.dt_SalesStaff.Rows.Count, i);
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                        XH = dt_SalesStaff.Rows[i]["序号"].ToString();
                        Org_StaffBLL staff = new Org_StaffBLL();

                        //序号
                        if (dt_SalesStaff.Rows[i]["员工工号"].ToString() != "")
                            staff.Model["StaffCode"] = dt_SalesStaff.Rows[i]["员工工号"].ToString();
                        else
                            staff.Model["StaffCode"] = FormatClientCode('S', dt_SalesStaff.Rows[i]["序号"].ToString().Trim(), dt_SalesStaff.Rows[i]["管理片区代码"].ToString().Trim());

                        #region 判断数据是否重复
                        if (Org_StaffBLL.GetStaffList("StaffCode = '" + staff.Model["StaffCode"] + "'").Count > 0)
                        {
                            UpdateImportFlag(1, i, "导入失败，数据重复！");
                            continue;
                        }
                        #endregion

                        //姓名
                        staff.Model["RealName"] = dt_SalesStaff.Rows[i]["姓名"].ToString().Trim();

                        //职务
                        DataRow[] rows_Position = dt_Position.Select("Name = '" + dt_SalesStaff.Rows[i]["职务"].ToString().Trim() + "'");
                        if (rows_Position.Length == 0)
                        {
                            UpdateImportFlag(1, i, "导入失败，职务在系统字典中不存在！");
                            continue;
                        }
                        else
                            staff.Model["Position"] = rows_Position[0]["ID"].ToString().Trim();

                        //管理片区代码
                        DataRow[] rows_OrganizeCity = dt_OrganizeCity.Select("Code='" + dt_SalesStaff.Rows[i]["管理片区代码"].ToString().Trim() + "'");
                        if (rows_OrganizeCity.Length == 0)
                        {
                            UpdateImportFlag(1, i, "导入失败，管理片区代码在系统字典中不存在！");
                            continue;
                        }
                        else
                            staff.Model["OrganizeCity"] = rows_OrganizeCity[0]["ID"].ToString().Trim();

                        //性别
                        staff.Model["Sex"] = dt_SalesStaff.Rows[i]["性别"].ToString();
                        //bFind = false;
                        //foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("Pub_Sex").Values)
                        //{
                        //    if (item.Name == dt_SalesStaff.Rows[i]["性别"].ToString().Trim())
                        //    {
                        //        staff.Model["Sex"] = item.Code;
                        //        bFind = true;
                        //        break;
                        //    }
                        //}
                        //if (!bFind)
                        //{
                        //    UpdateImportFlag(1, i, "导入失败，性别在系统字典中不存在！");
                        //    continue;
                        //}

                        //联系电话
                        staff.Model["TeleNum"] = dt_SalesStaff.Rows[i]["联系电话"].ToString().Trim();

                        //手机
                        staff.Model["Mobile"] = dt_SalesStaff.Rows[i]["手机"].ToString().Trim();

                        //城市
                        string cityname = dt_SalesStaff.Rows[i]["城市"].ToString().Trim();
                        if (cityname.Length > 3) cityname = cityname.Substring(0, 3);
                        DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + cityname + "%'");
                        if (rows_OfficialCity.Length == 0)
                        {
                            UpdateImportFlag(1, i, "导入失败，城市在系统字典中不存在！");
                            continue;
                        }
                        else
                            staff.Model["OfficialCity"] = rows_OfficialCity[0]["ID"].ToString().Trim();

                        //家庭地址
                        staff.Model["Address"] = dt_SalesStaff.Rows[i]["家庭地址"].ToString().Trim();

                        //身份证号
                        staff.Model["IDCode"] = dt_SalesStaff.Rows[i]["身份证号"].ToString().Trim();

                        //上岗时间
                        if (dt_SalesStaff.Rows[i]["上岗时间"].ToString().Trim() != "")
                            staff.Model["BeginWorkTime"] = dt_SalesStaff.Rows[i]["上岗时间"].ToString().Trim();

                        //离岗时间
                        if (dt_SalesStaff.Rows[i]["离岗时间"].ToString().Trim() != "")
                            staff.Model["EndWorkTime"] = dt_SalesStaff.Rows[i]["离岗时间"].ToString().Trim();

                        //银行类别
                        staff.Model["BankName"] = dt_SalesStaff.Rows[i]["银行类别"].ToString().Trim();

                        //工资卡号
                        staff.Model["BankAccount"] = dt_SalesStaff.Rows[i]["工资卡号"].ToString().Trim();

                        //在职标志
                        staff.Model["Dimission"] = "1";

                        int iret = staff.Add();
                        if (iret > 0)
                        {
                            _xhs_success.Append(XH + ",");
                            //UpdateImportFlag(1, i, "导入成功，ID＝" + iret.ToString());

                            #region 创建用户登录帐户
                            string username = staff.Model.RealName;
                            string pwd = "000000";
                            MembershipUserCollection haveusers = Membership.FindUsersByName(username);
                            if (haveusers.Count > 0)
                            {
                                username += "_" + haveusers.Count.ToString();
                            }
                            if (staff.Model["IDCode"].Length >= 6)
                                pwd = staff.Model["IDCode"].Substring(staff.Model["IDCode"].Length - 6);

                            MembershipUser user = Membership.CreateUser(username, pwd, "null@null.com");

                            if (user != null)
                            {
                                UserBLL.Membership_SetStaffID(user.UserName, iret);

                                Roles.AddUserToRole(user.UserName, " 全体员工");
                            }
                            #endregion
                        }
                        else
                            UpdateImportFlag(1, i, "导入失败，写入数据库失败！");
                    }
                    UpdateSuccess(_xhs_success.ToString(), 1);
                }
            }
            catch (System.Exception err)
            {
                UpdateSuccess(_xhs_success.ToString(), 1);
                MessageBox.Show(err.Source + "~r~n" + err.StackTrace, err.Message);
            }
            #endregion

            Thread.Sleep(1000);

            #region 导入经销商
            try
            {
                if (checkBox2.Checked)
                {
                    lb_Count.Text = dt_Distributor.Rows.Count.ToString();
                    tabControl1.SelectedIndex = 1;
                    XH = "";
                    for (int i = 0; i < dt_Distributor.Rows.Count; i++)
                    {
                        worker.ReportProgress((i + 1) * 100 / this.dt_Distributor.Rows.Count, i);

                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                        XH = dt_Distributor.Rows[i]["序号"].ToString();
                        CM_ClientBLL client = new CM_ClientBLL();

                        client.Model.ClientType = 2;    //经销商

                        #region 编号
                        if (dt_Distributor.Rows[i]["经销商编号"].ToString().Trim() != "")
                            client.Model["Code"] = dt_Distributor.Rows[i]["经销商编号"].ToString().Trim();
                        else
                            client.Model["Code"] = FormatClientCode('D', dt_Distributor.Rows[i]["序号"].ToString().Trim(), dt_Distributor.Rows[i]["管理片区代码"].ToString().Trim());

                        #region 判断数据是否重复
                        if (CM_ClientBLL.GetModelList("Code = '" + client.Model["Code"] + "'").Count > 0)
                        {
                            UpdateImportFlag(2, i, "导入失败，数据重复！");
                            continue;
                        }
                        #endregion
                        #endregion

                        //全称
                        client.Model["FullName"] = dt_Distributor.Rows[i]["全称"].ToString().Trim();
                        client.Model["ShortName"] = dt_Distributor.Rows[i]["简称"].ToString().Trim();

                        #region 管理片区代码
                        DataRow[] rows_OrganizeCity = dt_OrganizeCity.Select("Code='" + dt_Distributor.Rows[i]["管理片区代码"].ToString().Trim() + "'");
                        if (rows_OrganizeCity.Length == 0)
                        {
                            UpdateImportFlag(2, i, "导入失败，管理片区代码在系统字典中不存在！");
                            continue;
                        }
                        else
                            client.Model["OrganizeCity"] = rows_OrganizeCity[0]["ID"].ToString().Trim();
                        #endregion

                        #region 联系方式
                        client.Model["TeleNum"] = dt_Distributor.Rows[i]["固定电话"].ToString().Trim();
                        client.Model["Fax"] = dt_Distributor.Rows[i]["传真"].ToString().Trim();
                        client.Model["PostCode"] = dt_Distributor.Rows[i]["邮编"].ToString().Trim();
                        client.Model["Email"] = dt_Distributor.Rows[i]["Email"].ToString().Trim();
                        client.Model["Address"] = dt_Distributor.Rows[i]["地址"].ToString().Trim();
                        #endregion

                        #region 所在城市
                        string cityname = dt_Distributor.Rows[i]["城市"].ToString().Trim();
                        string areaname = dt_Distributor.Rows[i]["县城"].ToString().Trim();
                        if (areaname != "")
                        {
                            DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + areaname + "%'");
                            if (rows_OfficialCity.Length == 1)
                                client.Model.OfficalCity = int.Parse(rows_OfficialCity[0]["ID"].ToString().Trim());
                        }
                        if (cityname != "" && client.Model.OfficalCity == 0)
                        {
                            DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + cityname + "%'");
                            if (rows_OfficialCity.Length == 1)
                                client.Model.OfficalCity = int.Parse(rows_OfficialCity[0]["ID"].ToString().Trim());
                        }

                        if (client.Model["TeleNum"] != "" && client.Model.OfficalCity == 0)
                        {
                            Addr_OfficialCity city = null;
                            city = Addr_OfficialCityBLL.GetCityByTeleNumOrPostCode(client.Model["TeleNum"]);
                            if (city != null) client.Model.OfficalCity = city.ID;
                        }

                        if (client.Model["PostCode"] != "" && client.Model.OfficalCity == 0)
                        {
                            Addr_OfficialCity city = null;
                            city = Addr_OfficialCityBLL.GetCityByTeleNumOrPostCode(client.Model["PostCode"]);
                            if (city != null) client.Model.OfficalCity = city.ID;
                        }


                        if (dt_Distributor.Rows[i]["手机"].ToString().Trim() != "" && client.Model.OfficalCity == 0)
                        {
                            Addr_OfficialCity city = null;
                            city = Addr_OfficialCityBLL.GetCityByTeleNumOrPostCode(dt_Distributor.Rows[i]["手机"].ToString().Trim());
                            if (city != null) client.Model.OfficalCity = city.ID;
                        }

                        if (client.Model.OfficalCity == 0)
                        {
                            UpdateImportFlag(2, i, "导入失败，城市在系统字典中不存在！");
                            continue;
                        }
                        #endregion

                        #region 经销商类别
                        bFind = false;
                        foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_DI_Classify").Values)
                        {
                            if (item.Name == dt_Distributor.Rows[i]["类别"].ToString().Trim())
                            {
                                client.Model["DIClassify"] = item.Code;
                                bFind = true;
                                break;
                            }
                        }
                        if (!bFind)
                        {
                            UpdateImportFlag(2, i, "导入失败，类别在系统字典中不存在！");
                            continue;
                        }

                        #endregion

                        #region 活跃状态
                        bFind = false;
                        foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_ActiveFlag").Values)
                        {
                            if (item.Name == dt_Distributor.Rows[i]["活跃状态"].ToString().Trim())
                            {
                                client.Model["ActiveFlag"] = item.Code;
                                bFind = true;
                                break;
                            }
                        }
                        if (!bFind)
                        {
                            UpdateImportFlag(2, i, "导入失败，活跃状态在系统字典中不存在！");
                            continue;
                        }
                        #endregion

                        #region 纳税类型
                        if (dt_Distributor.Rows[i]["纳税类型"].ToString().Trim() != "")
                        {
                            bFind = false;
                            foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_TaxesClassify").Values)
                            {
                                if (item.Name == dt_Distributor.Rows[i]["纳税类型"].ToString().Trim())
                                {
                                    client.Model["TaxesClassify"] = item.Code;
                                    bFind = true;
                                    break;
                                }
                            }
                            if (!bFind)
                            {
                                UpdateImportFlag(2, i, "导入失败，纳税类型在系统字典中不存在！");
                                continue;
                            }
                        }
                        #endregion

                        #region 市场类型
                        if (dt_Distributor.Rows[i]["市场类型"].ToString().Trim() != "")
                        {
                            bFind = false;
                            foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_MarketType").Values)
                            {
                                if (item.Name == dt_Distributor.Rows[i]["市场类型"].ToString().Trim())
                                {
                                    client.Model["MarketType"] = item.Code;
                                    bFind = true;
                                    break;
                                }
                            }
                            if (!bFind)
                            {
                                UpdateImportFlag(2, i, "导入失败，市场类型在系统字典中不存在！");
                                continue;
                            }
                        }
                        #endregion

                        #region 主营渠道
                        if (dt_Distributor.Rows[i]["主营渠道"].ToString().Trim() != "")
                        {
                            bFind = false;
                            foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_RT_Channel").Values)
                            {
                                if (item.Name == dt_Distributor.Rows[i]["主营渠道"].ToString().Trim())
                                {
                                    client.Model["RTChannel"] = item.Code;
                                    bFind = true;
                                    break;
                                }
                            }
                            if (!bFind)
                            {
                                UpdateImportFlag(2, i, "导入失败，主营渠道在系统字典中不存在！");
                                continue;
                            }
                        }
                        #endregion

                        //开始合作时间
                        if (dt_Distributor.Rows[i]["开始合作时间"].ToString().Trim() != "")
                            client.Model["OpenTime"] = dt_Distributor.Rows[i]["开始合作时间"].ToString().Trim();

                        //覆盖区域
                        client.Model["CoverageArea"] = dt_Distributor.Rows[i]["覆盖区域"].ToString().Trim();

                        //主要销售奶粉品牌
                        client.Model["MostlyBrand"] = dt_Distributor.Rows[i]["主要销售奶粉品牌"].ToString().Trim();

                        //开户行类别
                        client.Model["BankName"] = dt_Distributor.Rows[i]["开户行类别"].ToString().Trim();
                        //开户行账号
                        client.Model["BankAccountNo"] = dt_Distributor.Rows[i]["开户行账号"].ToString().Trim();

                        #region 隶属一级商编号
                        if (client.Model["DIClassify"] != "1" && dt_Distributor.Rows[i]["隶属一级商编号"].ToString().Trim() != "")
                        {
                            IList<CM_Client> lists = CM_ClientBLL.GetModelList("Code='" + dt_Distributor.Rows[i]["隶属一级商编号"].ToString().Trim() + "'");
                            if (lists.Count > 0)
                            {
                                client.Model["Supplier"] = lists[0]["ID"];
                            }
                            else
                            {
                                //UpdateImportFlag(2, i, "导入失败，隶属一级商编号在客户中不存在！");
                                //continue;
                            }
                        }
                        else if (client.Model["DIClassify"] == "1")
                        {
                            client.Model.Supplier = 1223; //总部仓库
                        }
                        #endregion

                        #region 责任业代
                        bool fail1 = false;
                        if (dt_Distributor.Rows[i]["责任业代"].ToString().Trim() != "")
                        {
                            string cityids = new Addr_OrganizeCityBLL(client.Model.OrganizeCity).GetAllChildNodeIDs();
                            if (cityids != "") cityids += ",";
                            cityids += client.Model.OrganizeCity.ToString();

                            IList<Org_Staff> staffs = Org_StaffBLL.GetStaffList("OrganizeCity IN(" + cityids + ") AND RealName='" + dt_Distributor.Rows[i]["责任业代"].ToString().Trim() + "' AND Dimission=1");
                            if (staffs.Count == 1)
                                client.Model.ClientManager = staffs[0].ID;
                            else
                            {
                                fail1 = true;
                                //UpdateImportFlag(2, i, "导入失败，责任业代在员工表中不存在！" + staffs.Count.ToString());
                                //continue;
                            }
                        }
                        #endregion

                        client.Model.ApproveFlag = 1;
                        int iret = client.Add();
                        if (iret > 0)
                        {
                            //_xhs_success.Append(XH + ",");
                            if (!fail1)
                                UpdateImportFlag(2, i, "导入成功，ID＝" + iret.ToString());
                            else
                                UpdateImportFlag(2, i, "部分导入成功，责任业代在员工表中不存在！");
                        }
                        else
                        {
                            UpdateImportFlag(2, i, "导入失败，写入数据库失败！");
                            continue;
                        }

                        #region 加入联系人
                        CM_LinkManBLL linkman = new CM_LinkManBLL();
                        linkman.Model.ClientID = iret;

                        #region 负责人
                        if (dt_Distributor.Rows[i]["负责人"].ToString().Trim() != "")
                        {
                            linkman.Model["Name"] = dt_Distributor.Rows[i]["负责人"].ToString().Trim();

                            #region 负责人职务
                            if (dt_Distributor.Rows[i]["负责人职务"].ToString().Trim() != "")
                            {
                                bFind = false;
                                foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_LinkManJob").Values)
                                {
                                    if (item.Name == dt_Distributor.Rows[i]["负责人职务"].ToString().Trim())
                                    {
                                        linkman.Model["Job"] = item.Code;
                                        bFind = true;
                                        break;
                                    }
                                }
                            }
                            #endregion

                            //固定电话
                            linkman.Model["OfficeTeleNum"] = dt_Distributor.Rows[i]["固定电话"].ToString().Trim();

                            //手机
                            linkman.Model["Mobile"] = dt_Distributor.Rows[i]["手机"].ToString().Trim();
                            iret = linkman.Add();

                            if (iret > 0)
                            {
                                client.Model["ChiefLinkMan"] = iret.ToString();
                                client.Update();
                            }
                        }
                        #endregion

                        #region 法人
                        if (dt_Distributor.Rows[i]["负责人"].ToString().Trim() != "")
                        {
                            linkman.Model["Name"] = dt_Distributor.Rows[i]["法人代表"].ToString().Trim();
                            linkman.Model["Job"] = "2";       //法人代表
                            linkman.Model["OfficeTeleNum"] = dt_Distributor.Rows[i]["法人联系电话"].ToString().Trim();

                            if (linkman.Model["OfficeTeleNum"].StartsWith("1"))
                                linkman.Model["Mobile"] = linkman.Model["OfficeTeleNum"];

                            linkman.Add();
                        }
                        #endregion

                        #endregion
                    }
                    //UpdateSuccess(_xhs_success.ToString(), 2);
                }
            }
            catch (System.Exception err)
            {
                UpdateSuccess(_xhs_success.ToString(), 1);
                MessageBox.Show(err.Source + "~r~n" + err.StackTrace, err.Message);
            }
            #endregion

            Thread.Sleep(1000);

            #region 导入终端门店资料表
            StringBuilder _xhs_rt_fail1 = new StringBuilder("");
            StringBuilder _xhs_rt_fail2 = new StringBuilder("");
            try
            {
                if (checkBox3.Checked)
                {
                    lb_Count.Text = dt_Retailer.Rows.Count.ToString();
                    tabControl1.SelectedIndex = 2;
                    XH = "";
                    for (int i = 0; i < dt_Retailer.Rows.Count; i++)
                    {
                        worker.ReportProgress((i + 1) * 100 / this.dt_Retailer.Rows.Count, i);
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }

                        bool bsuccess = true;
                        XH = dt_Retailer.Rows[i]["序号"].ToString();
                        CM_ClientBLL client = new CM_ClientBLL();

                        client.Model.ClientType = 3;    //终端门店

                        #region 序号
                        client.Model["Code"] = dt_Retailer.Rows[i]["门店编码"].ToString().Trim();

                        #region 判断数据是否重复
                        if (CM_ClientBLL.GetModelList("Code = '" + client.Model["Code"] + "'").Count > 0)
                        {
                            UpdateImportFlag(3, i, "导入失败，数据重复！");
                            continue;
                        }
                        #endregion
                        #endregion

                        //连锁店名称
                        client.Model["ChainStoreName"] = dt_Retailer.Rows[i]["连锁店名称"].ToString().Trim();

                        //门店全称
                        client.Model["FullName"] = dt_Retailer.Rows[i]["门店全称"].ToString().Trim();

                        //门店简称
                        client.Model["ShortName"] = dt_Retailer.Rows[i]["门店简称"].ToString().Trim();
                        if (client.Model.ShortName == "") client.Model.ShortName = client.Model.FullName;

                        #region 管理片区代码
                        DataRow[] rows_OrganizeCity = dt_OrganizeCity.Select("Code='" + dt_Retailer.Rows[i]["管理片区代码"].ToString().Trim() + "'");
                        if (rows_OrganizeCity.Length == 0)
                        {
                            UpdateImportFlag(3, i, "导入失败，管理片区代码在系统字典中不存在！");
                            continue;
                        }
                        else
                            client.Model["OrganizeCity"] = rows_OrganizeCity[0]["ID"].ToString().Trim();
                        #endregion

                        //固定电话
                        client.Model["TeleNum"] = dt_Retailer.Rows[i]["固定电话"].ToString().Trim();
                        if (client.Model["TeleNum"] == "") client.Model["TeleNum"] = dt_Retailer.Rows[i]["联系人手机"].ToString().Trim();

                        //门店地址
                        client.Model["Address"] = dt_Retailer.Rows[i]["门店地址"].ToString().Trim();

                        #region 所在城市
                        string cityname = dt_Retailer.Rows[i]["城市"].ToString().Trim();
                        string areaname = dt_Retailer.Rows[i]["县城"].ToString().Trim();
                        string townname = dt_Retailer.Rows[i]["乡镇"].ToString().Trim();
                        if (townname != "")
                        {
                            DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + townname + "%'");
                            if (rows_OfficialCity.Length == 1)
                                client.Model.OfficalCity = int.Parse(rows_OfficialCity[0]["ID"].ToString().Trim());
                        }
                        if (areaname != "" && client.Model.OfficalCity == 0)
                        {
                            DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + areaname + "%'");
                            if (rows_OfficialCity.Length == 1)
                                client.Model.OfficalCity = int.Parse(rows_OfficialCity[0]["ID"].ToString().Trim());
                        }
                        if (cityname != "" && client.Model.OfficalCity == 0)
                        {
                            DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + cityname + "%'");
                            if (rows_OfficialCity.Length > 0)
                                client.Model.OfficalCity = int.Parse(rows_OfficialCity[0]["ID"].ToString().Trim());
                        }

                        if (client.Model["TeleNum"] != "" && client.Model.OfficalCity == 0)
                        {
                            Addr_OfficialCity city = null;
                            city = Addr_OfficialCityBLL.GetCityByTeleNumOrPostCode(client.Model["TeleNum"]);
                            if (city != null) client.Model.OfficalCity = city.ID;
                        }


                        if (client.Model.OfficalCity == 0)
                        {
                            UpdateImportFlag(3, i, "导入失败，城市在系统字典中不存在！");
                            continue;
                        }
                        #endregion

                        #region 门店渠道
                        if (dt_Retailer.Rows[i]["门店渠道"].ToString().Trim() != "")
                        {
                            bFind = false;
                            foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_RT_Channel").Values)
                            {
                                if (item.Name == dt_Retailer.Rows[i]["门店渠道"].ToString().Trim().ToUpper())
                                {
                                    client.Model["RTChannel"] = item.Code;
                                    bFind = true;
                                    break;
                                }
                            }
                            if (!bFind)
                            {
                                UpdateImportFlag(3, i, "导入失败，门店渠道在系统字典中不存在！");
                                continue;
                            }
                        }
                        #endregion

                        #region 门店类型
                        if (dt_Retailer.Rows[i]["门店类型"].ToString().Trim() != "")
                        {
                            bFind = false;
                            foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_RT_Classify").Values)
                            {
                                if (item.Name == dt_Retailer.Rows[i]["门店类型"].ToString().Trim())
                                {
                                    client.Model["RTClassify"] = item.Code;
                                    bFind = true;
                                    break;
                                }
                            }
                            if (!bFind)
                            {
                                UpdateImportFlag(3, i, "导入失败，门店类型在系统字典中不存在！");
                                continue;
                            }
                        }
                        #endregion

                        #region 销售代表
                        if (dt_Retailer.Rows[i]["销售代表工号"].ToString().Trim() != "")
                        {
                            IList<Org_Staff> staffs = Org_StaffBLL.GetStaffList("StaffCode='" + dt_Retailer.Rows[i]["销售代表工号"].ToString().Trim() + "' AND Dimission=1");
                            if (staffs.Count > 1) staffs = Org_StaffBLL.GetStaffList("StaffCode='" + dt_Retailer.Rows[i]["销售代表工号"].ToString().Trim() + "' AND Dimission=1 AND RealName='" + dt_Retailer.Rows[i]["销售代表"].ToString().Trim() + "'");

                            if (staffs.Count == 0)
                            {
                                _xhs_rt_fail2.Append(XH + ",");
                                bsuccess = false;
                                //UpdateImportFlag(3, i, "导入失败，销售代表在员工中不存在！");
                                //continue;
                            }
                            else if (staffs.Count > 1)
                            {
                                _xhs_rt_fail2.Append(XH + ",");
                                bsuccess = false;
                                //UpdateImportFlag(3, i, "导入失败，销售代表在员工中有多个相同姓名的员工！");
                                //continue;
                            }
                            else
                            {
                                client.Model["ClientManager"] = staffs[0]["ID"];
                            }
                        }
                        #endregion

                        #region 送货经销商编号
                        IList<CM_Client> clients = CM_ClientBLL.GetModelList("Code='" + dt_Retailer.Rows[i]["送货经销商编号"].ToString().Trim() + "'");
                        if (clients.Count == 0)
                        {
                            _xhs_rt_fail1.Append(XH + ",");
                            bsuccess = false;
                            //UpdateImportFlag(3, i, "导入失败，送货经销商编号在客户中不存在！");
                            //continue;
                        }
                        else if (clients.Count > 1)
                        {
                            _xhs_rt_fail1.Append(XH + ",");
                            bsuccess = false;
                            //UpdateImportFlag(3, i, "导入失败，送货经销商编号在客户中有多个相同编号的客户！");
                            //continue;
                        }
                        else
                        {
                            client.Model["Supplier"] = clients[0]["ID"];
                        }
                        #endregion

                        client.Model["Salesroom"] = dt_Retailer.Rows[i]["奶粉月容量"].ToString().Trim();
                        client.Model["BusinessArea"] = dt_Retailer.Rows[i]["卖场面积"].ToString().Trim();

                        client.Model["BankName"] = dt_Retailer.Rows[i]["开户行全称"].ToString().Trim();
                        client.Model["AccountName"] = dt_Retailer.Rows[i]["开户名"].ToString().Trim();
                        client.Model["BankAccountNo"] = dt_Retailer.Rows[i]["卡账号"].ToString().Trim();

                        //活跃状态
                        client.Model["ActiveFlag"] = "1";
                        client.Model.ApproveFlag = 1;

                        int iret = client.Add();
                        if (iret > 0)
                        {
                            if (bsuccess) _xhs_success.Append(XH + ",");
                            if (_xhs_success.Length >= 10000)
                            {
                                UpdateSuccess(_xhs_success.ToString(), 3);
                                UpdateFail1(_xhs_rt_fail1.ToString());
                                UpdateFail2(_xhs_rt_fail2.ToString());

                                _xhs_success = new StringBuilder("");
                                _xhs_rt_fail1 = new StringBuilder("");
                                _xhs_rt_fail2 = new StringBuilder("");
                            }
                            //UpdateImportFlag(3, i, "导入成功，ID＝" + iret.ToString());
                        }
                        else
                        {
                            UpdateImportFlag(3, i, "导入失败，写入数据库失败！");
                            continue;
                        }

                        #region 加入联系人
                        CM_LinkManBLL linkman = new CM_LinkManBLL();
                        linkman.Model.ClientID = iret;

                        //门店联系人
                        linkman.Model["Name"] = dt_Retailer.Rows[i]["门店联系人"].ToString().Trim();
                        if (linkman.Model["Name"] == "") linkman.Model.Name = "无";

                        //联系人职务
                        #region 联系人职务
                        if (dt_Retailer.Rows[i]["联系人职务"].ToString().Trim() != "")
                        {
                            bFind = false;
                            foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("CM_LinkManJob").Values)
                            {
                                if (item.Name == dt_Retailer.Rows[i]["联系人职务"].ToString().Trim())
                                {
                                    linkman.Model["Job"] = item.Code;
                                    bFind = true;
                                    break;
                                }
                            }
                        }
                        #endregion

                        //联系人固定电话
                        linkman.Model["OfficeTeleNum"] = dt_Retailer.Rows[i]["联系人固定电话"].ToString().Trim();

                        //联系人手机
                        linkman.Model["Mobile"] = dt_Retailer.Rows[i]["联系人手机"].ToString().Trim();

                        if (linkman.Model.Name == "无" && linkman.Model["OfficeTeleNum"] == "" && linkman.Model["Mobile"] == "")
                        {
                        }
                        else
                        {
                            iret = linkman.Add();

                            if (iret > 0)
                            {
                                client.Model["ChiefLinkMan"] = iret.ToString();
                                client.Update();
                            }
                        }
                        #endregion
                    }
                    UpdateSuccess(_xhs_success.ToString(), 3);
                    UpdateFail1(_xhs_rt_fail1.ToString());
                    UpdateFail2(_xhs_rt_fail2.ToString());
                }
            }
            catch (System.Exception err)
            {
                UpdateSuccess(_xhs_success.ToString(), 3);
                UpdateFail1(_xhs_rt_fail1.ToString());
                UpdateFail2(_xhs_rt_fail2.ToString());

                MessageBox.Show(err.Source + "~r~n" + err.StackTrace, err.Message);
            }
            #endregion

            Thread.Sleep(1000);

            #region 导入促销员
            try
            {
                if (checkBox4.Checked)
                {
                    lb_Count.Text = dt_Promotor.Rows.Count.ToString();
                    tabControl1.SelectedIndex = 3;
                    XH = "";

                    for (int i = 0; i < dt_Promotor.Rows.Count; i++)
                    {
                        worker.ReportProgress((i + 1) * 100 / this.dt_Promotor.Rows.Count, i);
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                        XH = dt_Promotor.Rows[i]["序号"].ToString();

                        PM_PromotorBLL promotor = new PM_PromotorBLL();

                        //序号
                        promotor.Model["Code"] = dt_Promotor.Rows[i]["导购编号"].ToString().Trim();

                        //判断数据是否重复
                        int promotorid = 0;
                        if (PM_PromotorBLL.GetModelList("Code = '" + promotor.Model["Code"] + "'").Count == 0)
                        {
                            #region 导入促销员资料
                            //姓名
                            promotor.Model["Name"] = dt_Promotor.Rows[i]["姓名"].ToString().Trim();

                            //管理片区代码
                            DataRow[] rows_OrganizeCity = dt_OrganizeCity.Select("Code='" + dt_Promotor.Rows[i]["管理片区代码"].ToString().Trim() + "'");
                            if (rows_OrganizeCity.Length == 0)
                            {
                                UpdateImportFlag(4, i, "导入失败，管理片区代码在系统字典中不存在！");
                                continue;
                            }
                            else
                                promotor.Model["OrganizeCity"] = rows_OrganizeCity[0]["ID"].ToString().Trim();

                            //联系电话
                            promotor.Model["TeleNum"] = dt_Promotor.Rows[i]["联系电话"].ToString().Trim();
                            if (promotor.Model["TeleNum"] == "")
                            {
                                promotor.Model["TeleNum"] = dt_Promotor.Rows[i]["手机"].ToString().Trim();
                            }

                            //手机
                            promotor.Model["Mobile"] = dt_Promotor.Rows[i]["手机"].ToString().Trim();
                            if (promotor.Model["Mobile"] == "" && promotor.Model["TeleNum"].StartsWith("1")) promotor.Model["Mobile"] = promotor.Model["TeleNum"];

                            #region 所在城市
                            string cityname = dt_Promotor.Rows[i]["城市"].ToString().Trim();
                            string areaname = dt_Promotor.Rows[i]["县城"].ToString().Trim();
                            string townname = dt_Promotor.Rows[i]["乡镇"].ToString().Trim();
                            if (townname != "")
                            {
                                DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + townname + "%'");
                                if (rows_OfficialCity.Length == 1)
                                    promotor.Model.OfficialCity = int.Parse(rows_OfficialCity[0]["ID"].ToString().Trim());
                            }
                            if (areaname != "" && promotor.Model.OfficialCity == 0)
                            {
                                DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + areaname + "%'");
                                if (rows_OfficialCity.Length == 1)
                                    promotor.Model.OfficialCity = int.Parse(rows_OfficialCity[0]["ID"].ToString().Trim());
                            }
                            if (cityname != "" && promotor.Model.OfficialCity == 0)
                            {
                                DataRow[] rows_OfficialCity = dt_OfficialCity.Select("Name like '" + cityname + "%'");
                                if (rows_OfficialCity.Length > 0)
                                    promotor.Model.OfficialCity = int.Parse(rows_OfficialCity[0]["ID"].ToString().Trim());
                            }

                            if (promotor.Model["TeleNum"] != "" && promotor.Model.OfficialCity == 0)
                            {
                                Addr_OfficialCity city = null;
                                city = Addr_OfficialCityBLL.GetCityByTeleNumOrPostCode(promotor.Model["TeleNum"]);
                                if (city != null) promotor.Model.OfficialCity = city.ID;
                            }


                            if (promotor.Model.OfficialCity == 0)
                            {
                                UpdateImportFlag(4, i, "导入失败，城市在系统字典中不存在！");
                                continue;
                            }
                            #endregion


                            //性别
                            if (dt_Promotor.Rows[i]["性别"].ToString().Trim() != "")
                            {
                                bFind = false;
                                foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("PUB_Sex").Values)
                                {
                                    if (item.Name == dt_Promotor.Rows[i]["性别"].ToString().Trim())
                                    {
                                        promotor.Model["Sex"] = item.Code;
                                        bFind = true;
                                        break;
                                    }
                                }
                                if (!bFind)
                                {
                                    UpdateImportFlag(4, i, "导入失败，性别在系统字典中不存在！");
                                    continue;
                                }
                            }

                            //分类
                            if (dt_Promotor.Rows[i]["分类"].ToString().Trim() != "")
                            {
                                bFind = false;
                                foreach (Dictionary_Data item in DictionaryBLL.GetDicCollections("PM_PromotorClassify").Values)
                                {
                                    if (item.Name == dt_Promotor.Rows[i]["分类"].ToString().Trim())
                                    {
                                        promotor.Model["Classfiy"] = item.Code;
                                        bFind = true;
                                        break;
                                    }
                                }
                                if (!bFind)
                                {
                                    UpdateImportFlag(4, i, "导入失败，分类在系统字典中不存在！");
                                    continue;
                                }
                            }


                            //家庭地址
                            promotor.Model["Address"] = dt_Promotor.Rows[i]["家庭地址"].ToString().Trim();

                            //身份证号
                            promotor.Model["IDCode"] = dt_Promotor.Rows[i]["身份证号"].ToString().Trim();

                            //上岗时间
                            if (dt_Promotor.Rows[i]["上岗时间"].ToString().Trim() != "")
                                promotor.Model["BeginWorkDate"] = dt_Promotor.Rows[i]["上岗时间"].ToString().Trim();

                            //离岗时间
                            if (dt_Promotor.Rows[i]["离岗时间"].ToString().Trim() != "")
                                promotor.Model["EndWorkDate"] = dt_Promotor.Rows[i]["离岗时间"].ToString().Trim();

                            //银行类别
                            promotor.Model["BankName"] = dt_Promotor.Rows[i]["银行类别"].ToString().Trim();

                            //工资卡号
                            promotor.Model["BankAccount"] = dt_Promotor.Rows[i]["工资卡号"].ToString().Trim();

                            //在职标志
                            promotor.Model["Dimission"] = "1";
                            promotor.Model.ApproveFlag = 1;
                            promotorid = promotor.Add();
                            if (promotorid > 0)
                            {
                                _xhs_success.Append(XH + ",");
                                //UpdateImportFlag(4, i, "导入成功，ID＝" + iret.ToString());
                            }
                            else
                            {
                                UpdateImportFlag(4, i, "导入失败，写入数据库失败！");
                                continue;
                            }
                            #endregion
                        }
                        else
                        {
                            promotorid = PM_PromotorBLL.GetModelList("Code = '" + promotor.Model["Code"] + "'")[0].ID;
                        }

                        #region 促销员所在门店
                        //门店编号
                        string rtcodes = dt_Promotor.Rows[i]["门店编号"].ToString().Trim();
                        if (rtcodes != "")
                        {
                            char[] sep = { ',' };
                            string[] codes = rtcodes.Split(sep);
                            foreach (string c in codes)
                            {
                                if (string.IsNullOrEmpty(c)) continue;
                                //if (c.IndexOf('-') > 0)
                                //{
                                //    char[] sep2 = { '-' };
                                //    string[] codes2 = c.Split(sep2);

                                //    if (codes2.Length == 2)
                                //    {
                                //        int begincode, endcode;
                                //        if (int.TryParse(codes2[0], out begincode) && int.TryParse(codes2[1], out endcode))
                                //        {
                                //            for (int codexh = begincode; codexh <= endcode; codexh++)
                                //            {
                                //                string code = FormatClientCode('R', codexh.ToString(), dt_Promotor.Rows[i]["管理片区代码"].ToString().Trim());

                                //                IList<CM_Client> clients = CM_ClientBLL.GetModelList("Code='" + code + "' AND ClientType = 3");
                                //                if (clients.Count == 1)
                                //                {
                                //                    PM_PromotorInRetailerBLL pinr = new PM_PromotorInRetailerBLL();
                                //                    pinr.Model.Promotor = promotorid;
                                //                    pinr.Model.Client = clients[0].ID;
                                //                    pinr.Add();
                                //                }
                                //                else
                                //                    break;
                                //            }
                                //        }

                                //    }
                                //    else
                                //    {
                                //        string xhs = _xhs_success.ToString();
                                //        if (xhs.EndsWith(","))
                                //        {
                                //            xhs = xhs.Substring(0, xhs.Length - 1);
                                //            xhs = xhs.Substring(0, xhs.LastIndexOf(',') + 1);
                                //            _xhs_success.Remove(0, _xhs_success.Length);
                                //            _xhs_success.Append(xhs);
                                //        }
                                //        UpdateImportFlag(4, i, "导入成功，ID＝" + promotorid.ToString() + ",关联所在门店失败!");
                                //        break;
                                //    }
                                //}
                                //else
                                {
                                    string code = c;

                                    IList<CM_Client> clients = CM_ClientBLL.GetModelList("Code='" + code + "' AND ClientType = 3");
                                    if (clients.Count == 1)
                                    {
                                        PM_PromotorInRetailerBLL pinr = new PM_PromotorInRetailerBLL();
                                        pinr.Model.Promotor = promotorid;
                                        pinr.Model.Client = clients[0].ID;
                                        pinr.Add();
                                    }
                                    else
                                    {
                                        string xhs = _xhs_success.ToString();
                                        if (xhs.EndsWith(","))
                                        {
                                            xhs = xhs.Substring(0, xhs.Length - 1);
                                            xhs = xhs.Substring(0, xhs.LastIndexOf(',') + 1);
                                            _xhs_success.Remove(0, _xhs_success.Length);
                                            _xhs_success.Append(xhs);
                                        }
                                        UpdateImportFlag(4, i, "导入成功，ID＝" + promotorid.ToString() + ",关联所在门店失败!");
                                        break;
                                    }
                                }
                            }
                        }

                        #endregion
                    }

                    UpdateSuccess(_xhs_success.ToString(), 4);

                }
            }
            catch (System.Exception err)
            {
                UpdateSuccess(_xhs_success.ToString(), 1);
                MessageBox.Show(err.Source + "~r~n" + err.StackTrace, err.Message);
            }
            #endregion

            return;
        }

        private int UpdateSuccess(string xhs, int flag)
        {
            if (xhs.EndsWith(",")) xhs = xhs.Substring(0, xhs.Length - 1);
            if (xhs.Length > 0)
            {
                string strcmd = "";
                if (flag == 1) strcmd = "Update [业务人员资料表$] set 导入标志='导入成功' where 序号 in (" + xhs + ")";
                else if (flag == 2) strcmd = "Update [经销商基本资料表$] set 导入标志='导入成功' where 序号 in (" + xhs + ")";
                else if (flag == 3) strcmd = "Update [终端门店资料表$] set 导入标志='导入成功' where 序号 in (" + xhs + ")";
                else if (flag == 4) strcmd = "Update [促销员资料表$] set 导入标志='导入成功' where 序号 in (" + xhs + ")";
                OleDbConnection oleConn = new OleDbConnection();
                oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=EXCEL 8.0";
                OleDbCommand oledbcmd = new OleDbCommand(strcmd, oleConn);
                try
                {
                    oleConn.Open();
                    oledbcmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + strcmd);
                }
                oleConn.Close();
            }
            return 1;
        }

        private int UpdateFail1(string xhs)
        {
            if (xhs.EndsWith(",")) xhs = xhs.Substring(0, xhs.Length - 1);
            if (xhs.Length > 0)
            {
                string strcmd = "Update [终端门店资料表$] set 导入标志='导入成功,但【送货经销商编号】在经销商目录中不存在!'  where 序号 in (" + xhs + ")";

                OleDbConnection oleConn = new OleDbConnection();
                oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=EXCEL 8.0";
                OleDbCommand oledbcmd = new OleDbCommand(strcmd, oleConn);
                try
                {
                    oleConn.Open();
                    oledbcmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + strcmd);
                }
                oleConn.Close();
            }
            return 1;
        }

        private int UpdateFail2(string xhs)
        {
            if (xhs.EndsWith(",")) xhs = xhs.Substring(0, xhs.Length - 1);
            if (xhs.Length > 0)
            {
                string strcmd = "Update [终端门店资料表$] set 导入标志='导入成功,但【销售代表】在员工目录中不存在!'  where 序号 in (" + xhs + ")";

                OleDbConnection oleConn = new OleDbConnection();
                oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=EXCEL 8.0";
                OleDbCommand oledbcmd = new OleDbCommand(strcmd, oleConn);
                try
                {
                    oleConn.Open();
                    oledbcmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + strcmd);
                }
                oleConn.Close();
            }
            return 1;
        }
        /// <summary>
        /// 格式化客户编号
        /// </summary>
        /// <param name="xh"></param>
        /// <param name="OrganCode"></param>
        /// <returns></returns>
        public string FormatClientCode(char pre, string xh, string OrganCode)
        {
            if (char.IsDigit(xh, 0))
            {
                xh = "0000" + xh.Trim();
                xh = xh.Substring(xh.Length - 4, 4);
                return pre + OrganCode.Trim().Substring(0, 1) + xh;
            }
            return xh;
        }

    }
}
