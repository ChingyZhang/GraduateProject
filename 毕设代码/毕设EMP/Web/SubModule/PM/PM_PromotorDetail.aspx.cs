// ===================================================================
// 文件路径:PM/PM_PromotorDetail.aspx.cs 
// 生成日期:2008-12-30 10:07:41 
// 作者:	  yangwei
// ===================================================================
using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Promotor;
using MCSFramework.Common;
using MCSFramework.Model.Promotor;
using MCSFramework.Model;
using System.Collections.Generic;
using MCSFramework.Model.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class PM_PM_PromotorDetail : System.Web.UI.Page
{

    MCSTreeControl tr_OrganizeCity, tr_OfficialCity;
    TextBox txt_IDCode, txt_Name, txt_AccountName, txt_BankName;
    DropDownList ddl_BankType, ddl_Classify;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        #region 获取界面控件
        tr_OfficialCity = UC_DetailView1.FindControl("PM_Promotor_OfficialCity") == null ? null : (MCSTreeControl)UC_DetailView1.FindControl("PM_Promotor_OfficialCity");
        tr_OrganizeCity = UC_DetailView1.FindControl("PM_Promotor_OrganizeCity") == null ? null : (MCSTreeControl)UC_DetailView1.FindControl("PM_Promotor_OrganizeCity");
        txt_IDCode = UC_DetailView1.FindControl("PM_Promotor_IDCode") == null ? null : (TextBox)UC_DetailView1.FindControl("PM_Promotor_IDCode");
        txt_Name = UC_DetailView1.FindControl("PM_Promotor_Name") == null ? null : (TextBox)UC_DetailView1.FindControl("PM_Promotor_Name");
        txt_AccountName = UC_DetailView1.FindControl("PM_Promotor_AccountName") == null ? null : (TextBox)UC_DetailView1.FindControl("PM_Promotor_AccountName");
        ddl_BankType = UC_DetailView1.FindControl("PM_Promotor_BankType") == null ? null : (DropDownList)UC_DetailView1.FindControl("PM_Promotor_BankType");
        txt_BankName = UC_DetailView1.FindControl("PM_Promotor_BankName") == null ? null : (TextBox)UC_DetailView1.FindControl("PM_Promotor_BankName");
        ddl_Classify = UC_DetailView1.FindControl("PM_Promotor_Classfiy") == null ? null : (DropDownList)UC_DetailView1.FindControl("PM_Promotor_Classfiy");
        if (ddl_BankType != null)
        {
            ddl_BankType.AutoPostBack = true;
            ddl_BankType.SelectedIndexChanged += new EventHandler(ddl_BankType_SelectedIndexChanged);
        }
        if (ddl_Classify != null)
        {
            ddl_Classify.AutoPostBack = true;
            ddl_Classify.SelectedIndexChanged += new EventHandler(ddl_Classify_SelectedIndexChanged);
        }
        if (tr_OrganizeCity != null)
        {
            tr_OrganizeCity.AutoPostBack = true;
            tr_OrganizeCity.Selected += new SelectedEventHandler(tr_OrganizeCity_Selected);
        }
        if (txt_IDCode != null)
        {
            txt_IDCode.AutoPostBack = true;
            txt_IDCode.TextChanged += new EventHandler(txt_IDCode_TextChanged);
        }
        if (txt_Name != null)
        {
            txt_Name.AutoPostBack = true;
            txt_Name.TextChanged += new EventHandler(txt_Name_TextChanged);
        }
        #endregion

        if (!Page.IsPostBack)
        {
            ViewState["PromotorID"] = Request.QueryString["PromotorID"] == null ? 0 : int.Parse(Request.QueryString["PromotorID"]);

            if ((int)ViewState["PromotorID"] > 0)
            {
                BindDropDown();
                BindData();

                UploadFile1.RelateID = (int)ViewState["PromotorID"];
                UploadFile1.BindGrid();
            }
            else
            {
                Org_Staff s = new Org_StaffBLL((int)Session["UserID"]).Model;
                if (s != null)
                {
                    if (tr_OfficialCity != null)
                        tr_OfficialCity.SelectValue = s.OfficialCity.ToString();
                    if (tr_OrganizeCity != null)
                        tr_OrganizeCity.SelectValue = s.OrganizeCity.ToString();
                }

                DropDownList ddl_SalaryFlag = (DropDownList)UC_DetailView1.FindControl("PM_Promotor_SalaryFlag");
                if (ddl_SalaryFlag != null) ddl_SalaryFlag.SelectedValue = "1";

                DropDownList ddl_Dimission = (DropDownList)UC_DetailView1.FindControl("PM_Promotor_Dimission");
                if (ddl_Dimission != null)
                {
                    ddl_Dimission.Enabled = false;
                    ddl_Dimission.SelectedValue = "1";
                }
                TextBox tbx_EndWorkDate = (TextBox)UC_DetailView1.FindControl("PM_Promotor_EndWorkDate");
                if (tbx_EndWorkDate != null) tbx_EndWorkDate.Enabled = false;

                tbl_Promotor.Visible = false;
                UploadFile1.Visible = false;
                gv_list.Visible = false;
                bt_AddApply.Visible = false;
                bt_Approve.Visible = false;
                bt_RevocationApply.Visible = false;
            }

            //根据管理片区，获取可用的薪酬级别
            tr_OrganizeCity_Selected(null, null);
        }

        #region 给账号/开户行文本框添加事件
        //TextBox tbx_BankName = (TextBox)UC_DetailView1.FindControl("PM_Promotor_BankName");
        //tbx_BankName.AutoPostBack = true;
        //tbx_BankName.TextChanged += new EventHandler(tbx_Bank_TextChanged);
        TextBox tbx_BankAccountNo = (TextBox)UC_DetailView1.FindControl("PM_Promotor_AccountCode");
        tbx_BankAccountNo.AutoPostBack = true;
        tbx_BankAccountNo.TextChanged += new EventHandler(tbx_Bank_TextChanged);
        #endregion

        if ((int)ViewState["PromotorID"] == 0)
        {
            MCSTabControl1.Items[1].Visible = false;
        }
        string script = "function PopPMClassify_Approve(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/PM/PMClassify_Approve.aspx?PromotorID=' + id ") +
            ", window, 'dialogWidth:930px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopPMClassify_Approve", script, true);
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if ((int)ViewState["PromotorID"] > 0)
        {
            PM_Promotor m = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;
            if (m.Dimission == 2)
            {
                TextBox txt_BeginWorkDate = (TextBox)UC_DetailView1.FindControl("PM_Promotor_BeginWorkDate");
                txt_BeginWorkDate.Enabled = true;
                //ddl_Classify.Enabled = true;
            }
        }
    }
    private void BindData()
    {
        PM_Promotor m = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;
        UC_DetailView1.BindData(m);
        gv_list.ConditionString = " Promotor= " + (int)ViewState["PromotorID"];
        gv_list.BindGrid();

        //验证身份证号码
        txt_BankName.Enabled = m["BankType"] == "3";

        if (m.ApproveFlag == 1)
        {
            bt_Approve.Visible = false;
            if (m.Dimission == 1)
            {
                TextBox txt_EndWorkDate = (TextBox)UC_DetailView1.FindControl("PM_Promotor_EndWorkDate");
                if (txt_EndWorkDate != null) txt_EndWorkDate.Enabled = false;

                bt_AddApply.Visible = false;
                #region 导购兼职/专职转换
                int classfiy = 0;
                int.TryParse(m["Classfiy"], out classfiy);
                if (classfiy == 0)
                    bt_ChangeClassify.Visible = false;
                else if (classfiy == 1 || classfiy == 2)
                {
                    bt_ChangeClassify.Text = "转为兼职";
                    bt_ChangeClassify.Visible = true;
                }
                else
                {
                    bt_ChangeClassify.Text = "转为专职/流动";
                    bt_ChangeClassify.Visible = true;
                }
                bt_ChangeClassify.OnClientClick = "PopPMClassify_Approve(" + ViewState["PromotorID"].ToString() + ")";
                #endregion
            }
            Header.Attributes["WebPageSubCode"] = "Modify";
            //TextBox txt_BeginWorkDate = (TextBox)UC_DetailView1.FindControl("PM_Promotor_BeginWorkDate");

            //临时开放修改功能
            //if (txt_BeginWorkDate != null && m.Dimission==1) txt_BeginWorkDate.Enabled = false;

            //有些离职日期会填写错误,更新为权限修改 12.11.21
            //if (m.Dimission == 2)
            //{
            //    TextBox txt_EndWorkDate = (TextBox)UC_DetailView1.FindControl("PM_Promotor_EndWorkDate");
            //    if (txt_EndWorkDate != null) txt_EndWorkDate.Enabled = false;
            //}
            string[] allowdays = Addr_OrganizeCityParamBLL.GetValueByType(1, 9).Replace(" ", "").Split(new char[] { ',', '，', ';', '；' });
            if (allowdays.Contains(DateTime.Now.Day.ToString()))
            {
                bt_ChangeClassify.Enabled = false;
                bt_ChangeClassify.ToolTip = "导购工资生成期间不能互转";
            }

        }

        if (m["State"] == "2")
        {
            //审批中
            bt_AddApply.Visible = false;
            bt_OK.Visible = false;
            bt_TaskDetail.Visible = true;
            bt_ChangeClassify.Enabled = false;
            bt_ChangeClassify.ToolTip = "该导购有未完成的流程,请待流程完成后，再做调整！";
        }
        bt_OK.Text = "保 存";
        bt_OK.ForeColor = System.Drawing.Color.Red;


        if (m["Classfiy"] != "")
        {
            int budget = PM_PromotorNumberLimitBLL.CheckOverBudget(m.OrganizeCity, int.Parse(m["Classfiy"]));
            if (budget == 0)
                lb_OverBudgetInfo.Text = "导购员数量已等于预定的预算人数，请注意！";
            else if (budget < 0)
                lb_OverBudgetInfo.Text = "导购员数量已超过预定的预算人数 " + (0 - budget).ToString() + "人，请注意！";
        }


        if (m["IDCode"].Length == 18 && !Tools.DoVerifyIDCode(m["IDCode"]))
        {
            lb_OverBudgetInfo.Text += "         注意：该导购员身份证号码错误！";
        }

        ddl_Classify_SelectedIndexChanged(null, null);
    }

    void tbx_Bank_TextChanged(object sender, EventArgs e)
    {
        int bankid = int.Parse(ddl_BankType.SelectedValue);
        TextBox tbx_BankAccountNo = (TextBox)UC_DetailView1.FindControl("PM_Promotor_AccountCode");
        if (bankid == 0)
            return;
        else
        {
            if (bankid == 2 && !tbx_BankAccountNo.Text.Trim().StartsWith("6228") && !tbx_BankAccountNo.Text.Trim().StartsWith("9559"))
            {
                MessageBox.Show(this, "所填账号非农行卡号，请检查!");
                tbx_BankAccountNo.Text = "";
            }
            else if (bankid == 1 && (tbx_BankAccountNo.Text.Trim().StartsWith("6228") || tbx_BankAccountNo.Text.Trim().StartsWith("9559")))
            {
                MessageBox.Show(this, "所填账号非建行卡号，请检查！");
                tbx_BankAccountNo.Text = "";
            }
            else if (bankid == 3 && (txt_BankName.Text.Contains("农业银行") || txt_BankName.Text.Contains("建设银行") || txt_BankName.Text.Contains("农行") || txt_BankName.Text.Contains("建行")))
            {
                MessageBox.Show(this, "如果是建行或农行，请直接选择对应银行！");
                txt_BankName.Text = ""; ddl_BankType.SelectedIndex = 0;
            }
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        PM_PromotorBLL _promotor = null;
        if ((int)ViewState["PromotorID"] == 0)
        {
            _promotor = new PM_PromotorBLL();
        }
        else
        {
            _promotor = new PM_PromotorBLL((int)ViewState["PromotorID"]);
        }

        int classify = 0, oldClassify = 0;
        int.TryParse(_promotor.Model["Classfiy"], out oldClassify);
        int oldOrganizeCity = _promotor.Model.OrganizeCity;

        UC_DetailView1.GetData(_promotor.Model);

        #region 判断数据有效性
        if (!(_promotor.Model.OrganizeCity > 1))
        {
            MessageBox.Show(this, "请选择导购员所在的管理片区！");
            return;
        }
        int.TryParse(_promotor.Model["Classfiy"], out classify);
        if (classify == 0)
        {
            MessageBox.Show(this, "请正确选择导购员类别！");
            return;
        }
        if (_promotor.Model["InfoSource"] == "" || _promotor.Model["InfoSource"] == "0")
        {
            MessageBox.Show(this, "请正确选择导购来源！");
            return;
        }
        if (_promotor.Model["OldClassify"] == "" || _promotor.Model["OldClassify"] == "0")
        {
            MessageBox.Show(this, "请正确选择原属品牌！");
            return;
        }
        if (_promotor.Model["BankType"] == "" || _promotor.Model["BankType"] == "0")
        {
            MessageBox.Show(this, "请正确选择银行信息");
            return;
        }
        int bankid = int.Parse(ddl_BankType.SelectedValue);
        if (_promotor.Model["BankName"] == "" || bankid <= 2 && _promotor.Model["BankName"] != ddl_BankType.SelectedItem.Text)
        {
            MessageBox.Show(this, "银行备注信息不正确");
            return;
        }
        //Org_Staff _staffM = new Org_StaffBLL((int)Session["UserID"]).Model;
        //if (_staffM.OrganizeCity == _promotor.Model.OrganizeCity)
        //{
        //    MessageBox.Show(this, "对不起，你不能把导购员放在与你同级的管理片区");
        //    return;
        //}

        #endregion


        #region 判断在职及离职状态
        if (_promotor.Model.Dimission == 1)
            _promotor.Model.EndWorkDate = new DateTime(1900, 1, 1);
        else if (_promotor.Model.EndWorkDate == new DateTime(1900, 1, 1))
        {
            _promotor.Model.EndWorkDate = DateTime.Today;
        }
        #endregion
        #region 如果有工资生成判断离职时间
        if ((int)ViewState["PromotorID"] > 0)
        {
            string[] allowday = Addr_OrganizeCityParamBLL.GetValueByType(1, 3).Split(new char[] { ',', '，', ';', '；' });
            AC_AccountMonth lastmonth = GetMaxSalaryDate((int)ViewState["PromotorID"], AC_AccountMonthBLL.GetCurrentMonth() - 1);
            if (lastmonth != null && allowday.Contains(DateTime.Now.Day.ToString()) && _promotor.Model.BeginWorkDate > lastmonth.EndDate)
            {
                MessageBox.Show(this, "该导购在" + lastmonth.Name + "生成过工资，入职日期不能大于" + lastmonth.EndDate.ToString("yyyy-MM-dd"));
                return;
            }
            if (_promotor.Model.Dimission == 2 && _promotor.Model.EndWorkDate < _promotor.Model.BeginWorkDate && _promotor.Model.EndWorkDate.AddDays(40) > DateTime.Now)
            {
                MessageBox.Show(this, "导购离职日期不能小于入职日期！");
                return;
            }
            AC_AccountMonth month = GetMaxSalaryDate((int)ViewState["PromotorID"], 0);
            if (month != null && _promotor.Model.Dimission == 2 && _promotor.Model.EndWorkDate < month.BeginDate)
            {
                MessageBox.Show(this, "该导购在" + month.Name + "生成过工资，离职日期不能小于" + month.BeginDate.ToString("yyyy-MM-dd"));
                return;
            }
        }
        #endregion
        if (_promotor.Model["IDCode"] != string.Empty && _promotor._GetModelList("ID!=" + ViewState["PromotorID"].ToString() + " AND MCS_SYS.dbo.UF_Spilt(PM_Promotor.ExtPropertys,'|',1)='" + _promotor.Model["IDCode"] + "'").Count > 0)
        {
            MessageBox.Show(this, "对不起，该身份证号的导购员已在系统中，请核实后再新增!");
            return;
        }

        if ((int)ViewState["PromotorID"] == 0)
        {

            DateTime birthday;
            if (DateTime.TryParse(_promotor.Model["Birthday"], out birthday))
            {
                if (DateTime.Now < birthday.AddYears(16) || DateTime.Now > birthday.AddYears(50))
                {
                    int year = DateTime.Now.Year - birthday.Year;
                    if (birthday.AddYears(year) > DateTime.Now)
                        year++;
                    MessageBox.Show(this, "对不起，该导购年龄不符合规则（16~49岁），该人员年龄：" + year);
                    return;
                }

            }

            if (PM_PromotorNumberLimitBLL.CheckAllowAdd(_promotor.Model.OrganizeCity, classify) <= 0)
            {
                MessageBox.Show(this, "对不起当前城市导购员人数满额，要想继续新增请与人事经理联系");
                return;
            }

            _promotor.Model.InputStaff = (int)Session["UserID"];
            _promotor.Model.ApproveFlag = 2;
            _promotor.Model.Dimission = 1;
            _promotor.Model.EndWorkDate = new DateTime(1900, 1, 1);
            ViewState["PromotorID"] = _promotor.Add();
        }
        else
        {
            if (!PM_PromotorNumberLimitBLL.IsSameLimit(oldOrganizeCity, _promotor.Model.OrganizeCity, oldClassify, classify) &&
                PM_PromotorNumberLimitBLL.CheckAllowAdd(_promotor.Model.OrganizeCity, classify) <= 0)
            {
                MessageBox.Show(this, "对不起当前城市导购员人数满额，要想继续新增请与人事经理联系");
                return;
            }

            _promotor.Model.UpdateStaff = (int)Session["UserID"];
            _promotor.Update();
        }
        if (sender != null)
            Response.Redirect("PM_PromotorDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString());
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        new PM_PromotorBLL(int.Parse(ViewState["PromotorID"].ToString())).Delete();
        Response.Redirect("PM_PromotorList.aspx");
    }

    protected void txt_IDCode_TextChanged(object sender, EventArgs e)
    {
        DateTime BirthDay; int Sex;
        TextBox txt_birthday = UC_DetailView1.FindControl("PM_Promotor_Birthday") != null ? (TextBox)UC_DetailView1.FindControl("PM_Promotor_Birthday") : null;
        DropDownList ddl_sex = UC_DetailView1.FindControl("PM_Promotor_Sex") != null ? (DropDownList)UC_DetailView1.FindControl("PM_Promotor_Sex") : null;
        if (Tools.DoVerifyIDCode(((TextBox)sender).Text, out BirthDay, out Sex) && ((TextBox)sender).Text.Trim().Length==18)
        {
            if (txt_birthday != null) txt_birthday.Text = BirthDay.ToString("yyyy-MM-dd");
            if (ddl_sex != null) ddl_sex.SelectedValue = Sex.ToString();


            if (DateTime.Now < BirthDay.AddYears(16) || DateTime.Now > BirthDay.AddYears(50))
            {
                int year = DateTime.Now.Year - BirthDay.Year;
                if (BirthDay.AddYears(year) > DateTime.Now)
                    year++;
                MessageBox.Show(this, "对不起，该导购年龄不符合规则（16~49岁），该人员年龄：" + year);
                return;
            }

        }
        else
        {
            MessageBox.Show(this, "身份证号码错误！");
            ((TextBox)sender).Text = "";
            if (txt_birthday != null) txt_birthday.Text = "";
            if (ddl_sex != null) ddl_sex.SelectedValue = "0";
            return;
        }

    }

    protected void txt_Name_TextChanged(object sender, EventArgs e)
    {
        if (txt_AccountName.Text != null) txt_AccountName.Text = txt_Name.Text;
    }

    protected void tr_OrganizeCity_Selected(object sender, SelectedEventArgs e)
    {
        #region 限定选择提成计算方法的范围只能是当前片区
        DropDownList ddl_SalaryGrade = (DropDownList)UC_DetailView1.FindControl("PM_Promotor_SalaryGrade");
        if (ddl_SalaryGrade != null)
        {
            try
            {
                ddl_SalaryGrade.DataTextField = "Name";
                ddl_SalaryGrade.DataValueField = "ID";

                string orgcitys = "";
                DataTable dt = TreeTableBLL.GetFullPath("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", int.Parse(tr_OrganizeCity.SelectValue));
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ID"].ToString() != "")
                        orgcitys += "," + dr["ID"].ToString();
                }
                if (orgcitys.Length > 1) orgcitys = orgcitys.Substring(1, orgcitys.Length - 1);

                ddl_SalaryGrade.DataSource = PM_SalaryLevelBLL.GetModelList("OrganizeCity in (" + orgcitys + ")");
                ddl_SalaryGrade.DataBind();
            }
            catch { }
            ddl_SalaryGrade.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion
    }

    public void BindDropDown()
    {
        PM_Promotor p = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;

        ddl_CM.DataTextField = "FullName";
        ddl_CM.DataValueField = "ID";
        ddl_CM.DataSource = CM_ClientBLL.GetModelList(" OrganizeCity in (" + p.OrganizeCity + ")" + @" AND ClientType=3 AND ActiveFlag=1 AND ApproveFlag=1 AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',5)='3'");
        ddl_CM.DataBind();
        ddl_CM.Items.Insert(0, new ListItem("请选择", "0"));
    }

    protected void gv_list_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        IList<PM_PromotorInRetailer> _list = PM_PromotorInRetailerBLL.GetModelList("Promotor=" + ViewState["PromotorID"].ToString());
        if (_list.Count > 1)
        {
            int id = int.Parse(gv_list.DataKeys[e.RowIndex]["PM_PromotorInRetailer_ID"].ToString());
            PM_PromotorInRetailerBLL bll = new PM_PromotorInRetailerBLL(id);
            bll.Delete();
            BindData();
        }
        else
        {
            MessageBox.Show(this, "请确保将导购关联新的门店再删除！");
            return;
        }
    }

    protected void bt_AddCM_Click(object sender, EventArgs e)
    {
        PM_PromotorInRetailerBLL bll = new PM_PromotorInRetailerBLL();

        bll.Model.Promotor = int.Parse(ViewState["PromotorID"].ToString());
        bll.Model.Client = int.Parse(ddl_CM.SelectedValue.ToString());
        if (bll.Model.Client <= 0)
        {
            MessageBox.Show(this, "门店未选择，请选择需要添加的门店！");
            return;
        }
        if (PM_PromotorInRetailerBLL.GetModelList(" Promotor=" + bll.Model.Promotor + " and  Client=" + bll.Model.Client).Count > 0)
        {
            MessageBox.Show(this, "该门店已经存在！");
            return;
        }
        else
        {
            //IList<CM_Contract> contracts = CM_ContractBLL.GetModelList("Client= " + bll.Model.Client.ToString() +
            //    " AND GETDATE() BETWEEN BeginDate AND DATEADD(day,1,ISNULL(EndDate,GETDATE())) AND State=3");

            //CM_Contract _c = contracts.FirstOrDefault(p => p.Classify == 3);

            //int _AllowPromotorCount = 0;
            //if (_c != null) int.TryParse(_c["PromotorCount"], out _AllowPromotorCount);

            //if (PM_PromotorInRetailerBLL.GetModelList("Client=" + bll.Model.Client).Count >= _AllowPromotorCount)
            //{
            //    MessageBox.Show(this, "对不起，当前门店最多只允许" + _AllowPromotorCount.ToString() + "个导购入场，请修改门店导购入场协议数！");
            //    return;
            //}

            CM_ClientBLL _cm = new CM_ClientBLL(bll.Model.Client);
            //导购店添加返利协议
            if (_cm.Model["RTClassify"] == "2")
            {
                MessageBox.Show(this, _cm.CheckRealClassifyShowMessage(2));
                return;
            }
            bll.Add();
        }
        ddl_CM.SelectedValue = "0";
        BindData();
    }
    private AC_AccountMonth GetMaxSalaryDate(int Prmomtor, int accountmonth)
    {
        string conditon = "ID IN (SELECT MAX(SalaryID) FROM MCS_Promotor.dbo.PM_SalaryDetail WHERE Promotor=" + Prmomtor.ToString() + ")";
        if (accountmonth != 0)
        {
            conditon += " AND AccountMonth=" + accountmonth.ToString();
        }
        IList<PM_Salary> _listsalary = PM_SalaryBLL.GetModelList(conditon);
        AC_AccountMonthBLL _mbll;
        if (_listsalary.Count > 0)
        {
            _mbll = new AC_AccountMonthBLL(_listsalary[0].AccountMonth);
            return _mbll.Model;
        }
        else
        {
            return null;
        }
    }
    protected void bt_AddApply_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PromotorID"] == 0)
        {
            MessageBox.Show(this, "对不起，请您先保存后在发起申请");
            return;
        }
        bt_OK_Click(null, null);

        PM_PromotorBLL bll = new PM_PromotorBLL((int)ViewState["PromotorID"]);
        DateTime birthday;
        if (DateTime.TryParse(bll.Model["Birthday"], out birthday))
        {
            if (DateTime.Now < birthday.AddYears(16) || DateTime.Now > birthday.AddYears(50))
            {
                int year = DateTime.Now.Year - birthday.Year;
                if (birthday.AddYears(year) > DateTime.Now)
                    year++;
                MessageBox.Show(this, "对不起，该导购年龄不符合规则（16~49岁），该人员年龄：" + year);
                return;
            }

        }
        if (PM_PromotorNumberLimitBLL.CheckAllowAdd(bll.Model.OrganizeCity, int.Parse(bll.Model["Classfiy"])) < 0)
        {
            MessageBox.Show(this, "对不起当前城市导购员人数满额，要想继续新增请与人事经理联系");
            return;
        }
        if (bll.Model.BeginWorkDate.AddDays(40) < DateTime.Now)
        {
            MessageBox.Show(this, "对不起，入职时间必须在发起日期前40天之内！");
            return;
        }
        if (bll.Model["IDCode"] == "")
        {
            MessageBox.Show(this, "请录入身份证号！");
            return;
        }
        if (bll.Model["InfoSource"] == "" || bll.Model["InfoSource"] == "0")
        {
            MessageBox.Show(this, "请正确选择导购来源！");
            return;
        }
        if (bll.Model["OldClassify"] == "" || bll.Model["OldClassify"] == "0")
        {
            MessageBox.Show(this, "请正确选择原属品牌！");
            return;
        }
        if (bll.Model["BankType"] == "" || bll.Model["BankType"] == "0")
        {
            MessageBox.Show(this, "请正确选择银行信息");
            return;
        }
        string[] allowday = Addr_OrganizeCityParamBLL.GetValueByType(1, 3).Split(new char[] { ',', '，', ';', '；' });
        AC_AccountMonth lastmonth = GetMaxSalaryDate((int)ViewState["PromotorID"], AC_AccountMonthBLL.GetCurrentMonth() - 1);
        if (lastmonth != null && allowday.Contains(DateTime.Now.Day.ToString()) && bll.Model.BeginWorkDate > lastmonth.EndDate)
        {
            MessageBox.Show(this, "该导购在" + lastmonth.Name + "生成过工资，入职日期不能大于" + lastmonth.EndDate.ToString("yyyy-MM-dd"));
            return;
        }

        #region 判断是否KA店导购
        bool IsKAChannel = false;  //是否KA店导购

        if (bll.Model["Classify"] != "2")       //非流导
        {
            IList<PM_PromotorInRetailer> retailers = PM_PromotorInRetailerBLL.GetModelList(" Promotor=" + ViewState["PromotorID"].ToString());
            if (retailers.Count == 0)
            {
                MessageBox.Show(this, "对不起，请关联该导购所在的工作门店!");
                return;
            }

            //判断导购是否在KA店工作
            foreach (PM_PromotorInRetailer item in retailers)
            {
                CM_Client client = new CM_ClientBLL(item.Client).Model;
                if (client["RTChannel"] == "1" || client["RTChannel"] == "2") IsKAChannel = true;
            }
        }
        #endregion

        #region 判断是否超薪酬超准
        bool bSalaryFlag = false;   //false : 薪酬标准内 true:超标准
        IList<PM_PromotorSalary> salarylists = PM_PromotorSalaryBLL.GetModelList("Promotor=" + bll.Model.ID.ToString() + " AND State IN(1,3) Order BY State");

        if (salarylists.Count == 0 || salarylists.Count > 0 && (bll.Model["Classfiy"] == "6" && salarylists[0].BasePayMode != 3 || bll.Model["Classfiy"] != "6" && salarylists[0].BasePayMode == 3))
        {
            MessageBox.Show(this, "对不起，尚未为该导购设定薪酬信息，请设定完薪酬信息后，再提交入职申请！");
            return;
        }
        else
        {
            PM_PromotorSalary salary = salarylists[0];
            if (salary.BasePaySubsidy > 0) bSalaryFlag = true;   //有底薪补贴
            if (salary.MinimumWageMode == 2) bSalaryFlag = true; //特殊保底
            if (salary.InsuranceMode == 1 && salary.InsuranceSubsidy > 100) bSalaryFlag = true;     //社保补贴大于100元的
        }
        #endregion

        int budget = PM_PromotorNumberLimitBLL.CheckOverBudget(bll.Model.OrganizeCity, int.Parse(bll.Model["Classfiy"]));

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", ViewState["PromotorID"].ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("SalaryFlag", !bSalaryFlag ? "1" : "2");      //薪酬标志 1：薪酬标准内 2：薪酬标准外
        dataobjects.Add("StaffName", bll.Model.Name.ToString());
        dataobjects.Add("IsKAChannel", IsKAChannel ? "1" : "2");      //是否KA卖场的导购
        dataobjects.Add("IsOverBudget", budget < 0 ? "1" : "2");      //是否超人数预算 1:超 2:未超

       
        int TaskID = EWF_TaskBLL.NewTask("Add_Promotor", (int)Session["UserID"], "新增导购员流程,姓名:" + bll.Model.Name+ "【"+ddl_Classify.SelectedItem.Text.ToString()+"】", "~/SubModule/PM/PM_PromotorDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString() , dataobjects);
        if (TaskID > 0)
        {
            bll.Submit(TaskID, (int)Session["UserID"]);
        }

        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }

    protected void bt_RevocationApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/EWF/FlowAppInitList.aspx");
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
        {
            Response.Redirect("PM_PromotorSalaryDetail.aspx?PromotorID=" + ViewState["PromotorID"].ToString() +
                 (Request.QueryString["ViewFramework"] == null ? "" : "&ViewFramework=" + Request.QueryString["ViewFramework"]));
        }
    }
    /// <summary>
    /// 审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PromotorID"] != 0)
        {
            PM_PromotorBLL _promotor = new PM_PromotorBLL((int)ViewState["PromotorID"]);

            if (PM_PromotorNumberLimitBLL.CheckAllowAdd(_promotor.Model.OrganizeCity, int.Parse(_promotor.Model["Classfiy"])) < 0)
            {
                MessageBox.Show(this, "对不起当前城市导购员人数满额，要想继续新增请与人事经理联系");
                return;
            }
            _promotor.Model.ApproveFlag = 1;
            _promotor.Model.UpdateStaff = (int)Session["UserID"];
            _promotor.Update();
            MessageBox.Show(this, "审核成功！");
            BindData();
        }
    }
    protected void bt_TaskDetail_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PromotorID"] == 0)
        {
            MessageBox.Show(this, "对不起，当前还没有审批记录");
            return;
        }

        PM_PromotorBLL _promotor = new PM_PromotorBLL((int)ViewState["PromotorID"]);
        Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + _promotor.Model["TaskID"].ToString());
    }

    protected void ddl_BankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_BankName != null)
        {
            int val = int.Parse(ddl_BankType.SelectedValue);
            txt_BankName.Enabled = val > 2;
            txt_BankName.Text = val > 2 ? "" : ddl_BankType.SelectedItem.Text;
            TextBox tbx_BankAccountNo = (TextBox)UC_DetailView1.FindControl("PM_Promotor_AccountCode");
            if (val == 1 && (tbx_BankAccountNo.Text.Trim().StartsWith("6228") || tbx_BankAccountNo.Text.Trim().StartsWith("9559")))
            {
                MessageBox.Show(this, "所填账号非建行卡号，请重新选择！");
                txt_BankName.Text = ""; ddl_BankType.SelectedIndex = 0;
            }
            else if (val == 2 && !tbx_BankAccountNo.Text.Trim().StartsWith("6228") && !tbx_BankAccountNo.Text.Trim().StartsWith("9559"))
            {
                MessageBox.Show(this, "所填账号非农行卡号，请重新选择！");
                txt_BankName.Text = ""; ddl_BankType.SelectedIndex = 0;
            }

        }
    }
    protected void ddl_Classify_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_BankType != null)
            ddl_BankType.Items[3].Enabled = ddl_Classify.SelectedValue == "6";

    }
}
