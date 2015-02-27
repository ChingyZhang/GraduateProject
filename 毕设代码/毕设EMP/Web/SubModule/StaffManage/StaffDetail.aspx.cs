using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.Model;
using System.Web.Security;

public partial class SubModule_StaffManage_StaffDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            #region 如果非总部职位，其只能选择自己职位及以下职位
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
            Org_Position p = new Org_PositionBLL(staff.Model.Position).Model;
            MCSTreeControl tr_Position = (MCSTreeControl)panel1.FindControl("Org_Staff_Position");
            if (p != null && p.IsHeadOffice != "Y" && p.Remark != "OfficeHR")  //备注为"OfficeHR":人事经理,可以选择全部职位
            {
                tr_Position.RootValue = p.SuperID.ToString();
                tr_Position.SelectValue = staff.Model.Position.ToString();
            }
            else if (tr_Position!=null)
            {
                tr_Position.RootValue = "1";
                tr_Position.SelectValue = "1";
            }
            #endregion

            DropDownList ddl_Dimission = (DropDownList)panel1.FindControl("Org_Staff_Dimission");
            if (ddl_Dimission != null) ddl_Dimission.Enabled = false;
            TextBox tbx_EndWorkTime = (TextBox)panel1.FindControl("Org_Staff_EndWorkTime");
            if (tbx_EndWorkTime != null) tbx_EndWorkTime.Enabled = false;

            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
            else
            {
                tr_LoginUser.Visible = false;
                tr_StaffInOrganizeCity.Visible = false;
                UploadFile1.Visible = false;

                bt_AddApply.Visible = false;
                bt_RevocationApply.Visible = false;
                bt_Approve.Visible = false;
                bt_Print.Visible = false;

                if (ddl_Dimission != null) ddl_Dimission.SelectedValue = "1";

                RadioButtonList rbl_ApproveFlag = (RadioButtonList)panel1.FindControl("Org_Staff_ApproveFlag");
                if (rbl_ApproveFlag != null) rbl_ApproveFlag.SelectedValue = "2";

                DropDownList ddl_SalaryFlag = (DropDownList)panel1.FindControl("Org_Staff_SalaryFlag");
                if (ddl_SalaryFlag != null) ddl_SalaryFlag.SelectedValue = "1";

            }
        }
    }

    private void BindData()
    {
        Org_StaffBLL bll = new Org_StaffBLL((int)ViewState["ID"]);
        panel1.BindData(bll.Model);
        Header.Attributes["WebPageSubCode"] = "Modify";
        gv_List.DataSource = bll.GetUserList();
        gv_List.DataBind();

        UploadFile1.RelateID = (int)ViewState["ID"];
        UploadFile1.BindGrid();

        Org_Staff m = bll.Model;
        if (m.ApproveFlag == 1)
        {
            if (m.Dimission == 1)
            {
                bt_AddApply.Visible = false;
            }
            bt_Approve.Visible = false;            
            TextBox tbx_BeginWorkTime = (TextBox)panel1.FindControl("Org_Staff_BeginWorkTime");
            // if (tbx_BeginWorkTime != null) tbx_BeginWorkTime.Enabled = false;//03-17暂停使用,便于维护资料
            if (m.Dimission == 2)
            {
                TextBox tbx_EndWorkTime = (TextBox)panel1.FindControl("Org_Staff_EndWorkTime");
                if (tbx_EndWorkTime != null) tbx_EndWorkTime.Enabled = false;
            }
        }

        if (m["State"] == "2")
        {
            //审批中
            bt_AddApply.Visible = false;
            bt_OK.Visible = false;
            bt_CreateUser.Visible = false;
            bt_TaskDetail.Visible = true;
            bt_RevocationApply.Visible = false;
        }

        if (bll.Model.OrganizeCity > 1)
        {
            #region 绑定兼管片区
            IList<Addr_OrganizeCity> staffincity = bll.StaffInOrganizeCity_GetOrganizeCitys();

            Addr_OrganizeCity currentcity = new Addr_OrganizeCityBLL(bll.Model.OrganizeCity).Model;
            if (currentcity != null)
            {
                Addr_OrganizeCity parent = new Addr_OrganizeCityBLL(currentcity.SuperID).Model;
                if (parent != null)
                {
                    IList<Addr_OrganizeCity> lists = Addr_OrganizeCityBLL.GetModelList("SuperID = " + parent.ID.ToString() + " AND ID <> " + bll.Model.OrganizeCity.ToString());
                    if (lists.Count > 0)
                    {
                        foreach (Addr_OrganizeCity city in lists)
                        {
                            if (staffincity.FirstOrDefault(p => p.ID == city.ID) == null)
                                ddl_StaffInOrganizeCity.Items.Add(new ListItem(parent.Name + "->" + city.Name + "(" + city.Code + ")", city.ID.ToString()));
                        }
                    }
                    Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
                    Org_Position position = new Org_PositionBLL(staff.Model.Position).Model;
                    if (currentcity.Level > 1 && position != null &&( position.IsHeadOffice == "Y" || position.Remark == "OfficeHR"))
                    {
                        int grandpacityid = new Addr_OrganizeCityBLL(currentcity.SuperID).Model.SuperID;
                        string condition = "SuperID = " + grandpacityid.ToString() + " AND ID <> " + currentcity.SuperID.ToString();
                        if (currentcity.Level < ConfigHelper.GetConfigInt("OrganizeCity-CityLevel"))
                        {
                            condition = "SuperID != " +parent.ID.ToString() + " AND Level=" + (currentcity.Level-1).ToString() + " AND ID <> " + currentcity.SuperID.ToString();
                        }
                        IList<Addr_OrganizeCity> bobos = Addr_OrganizeCityBLL.GetModelList(condition);
                        foreach (Addr_OrganizeCity bobo in bobos)
                        {
                            IList<Addr_OrganizeCity> tangxongs = Addr_OrganizeCityBLL.GetModelList("SuperID = " + bobo.ID.ToString());
                            foreach (Addr_OrganizeCity city in tangxongs)
                            {
                                if (staffincity.FirstOrDefault(p => p.ID == city.ID) == null)
                                    ddl_StaffInOrganizeCity.Items.Add(new ListItem(bobo.Name + "->" + city.Name + "(" + city.Code + ")", city.ID.ToString()));
                            }
                        }
                    }
                }

                ddl_StaffInOrganizeCity.Items.Insert(0, new ListItem("请选择...", "0"));
            }
            if (ddl_StaffInOrganizeCity.Items.Count == 0) bt_Add_StaffInOrganizeCity.Enabled = false;
            #endregion

            gv_StaffInOrganizeCity.BindGrid<Addr_OrganizeCity>(bll.StaffInOrganizeCity_GetOrganizeCitys());
        }
        else
        {
            tr_StaffInOrganizeCity.Visible = false;
        }

        bt_OK.Text = "保 存";
        bt_OK.ForeColor = System.Drawing.Color.Red;

        //if (new Org_StaffBLL((int)Session["UserID"]).Model.ID != 1)
        //{
        //    ((MCSTreeControl)panel1.FindControl("Org_Staff_Position")).Enabled = false;
        //}

        int budget = Org_StaffNumberLimitBLL.CheckOverBudget(m.OrganizeCity, m.Position);
        if (budget == 0)
            lb_OverBudgetInfo.Text = "当前城市该职位人员数量已等于预定的预算人数，请注意！";
        else if (budget < 0)
            lb_OverBudgetInfo.Text = "当前城市该职位人员数量已超过预定的预算人数 " + (0 - budget).ToString() + "人，请注意！";
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Org_StaffBLL _staffbll = null;
        if ((int)ViewState["ID"] == 0)
        {
            _staffbll = new Org_StaffBLL();
        }
        else
        {
            _staffbll = new Org_StaffBLL((int)ViewState["ID"]);
        }

        int oldPosition = _staffbll.Model.Position;
        int oldOrganizeCity = _staffbll.Model.OrganizeCity;

        panel1.GetData(_staffbll.Model);

        if ((int)ViewState["ID"] != 0 && (oldPosition != _staffbll.Model.Position||oldOrganizeCity!=_staffbll.Model.OrganizeCity)&&
            EWF_Task_JobDecisionBLL.GetModelList("DecisionResult=1 AND RecipientStaff=" + ViewState["ID"].ToString()).Count > 0)
        {
             MessageBox.Show(this, "对不起，该人员还有未审批流程不能调整【职务】或【管理片区】信息!");
             return;
        }
        Addr_OfficialCityBLL citybll = new Addr_OfficialCityBLL(_staffbll.Model.OfficialCity);
        if (citybll != null && citybll.Model.Level < 2)
        {
            MessageBox.Show(this, "驻点所在城市必须选到市级!");
            return;
        }

        Org_Staff _staffM = new Org_StaffBLL((int)Session["UserID"]).Model;
        if (_staffM.Position == _staffbll.Model.Position && new Org_PositionBLL(_staffM.Position).Model.IsHeadOffice != "Y")
        {
            MessageBox.Show(this, "对不起，您没有权限新增职位与您相同的员工");
            return;
        }

        if (_staffbll.Model["OperateProperty"]=="0")
        {
            MessageBox.Show(this, "归属办事处类型必填!");
            return;
        }

        if ((int)ViewState["ID"] == 0)
        {
            #region 根据身份证号判断是否重复
            if (_staffbll.Model["IDCode"] != string.Empty &&
                Org_StaffBLL.GetStaffList("MCS_SYS.dbo.UF_Spilt(Org_Staff.ExtPropertys,'|',1)='" + _staffbll.Model["IDCode"] + "' AND Dimission=1").Count > 0)
            {
                MessageBox.Show(this, "对不起，该身份证号的员工已在系统中入职，请核实后再新增员工!");
                return;
            }
            #endregion

            if (Org_StaffNumberLimitBLL.CheckAllowAdd(_staffbll.Model.OrganizeCity, _staffbll.Model.Position) <= 0)
            {
                MessageBox.Show(this, "对不起当前城市该职位员工人数满额，要想继续新增请与人事经理联系");
                return;
            }
            _staffbll.Model.InsertStaff = (int)Session["UserID"];
            _staffbll.Model.InsertTime = DateTime.Now;
            _staffbll.Model.ApproveFlag = 1;            //未审批
            _staffbll.Model.Dimission = 1;              //在职
            ViewState["ID"] = _staffbll.Add();
        }
        else
        {
            if (!Org_StaffNumberLimitBLL.IsSameLimit(oldOrganizeCity, _staffbll.Model.OrganizeCity, oldPosition, _staffbll.Model.Position) &&
                Org_StaffNumberLimitBLL.CheckAllowAdd(_staffbll.Model.OrganizeCity, _staffbll.Model.Position) <= 0)
            {
                MessageBox.Show(this, "对不起当前城市该职位员工人数满额，要想继续新增请与人事经理联系");
                return;
            }
            _staffbll.Model.UpdateStaff = (int)Session["UserID"];
            _staffbll.Update();
        }
        MessageBox.ShowAndRedirect(this, "保存成功", "StaffDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Add_StaffInOrganizeCity_Click(object sender, EventArgs e)
    {
        if (ddl_StaffInOrganizeCity.SelectedValue != "0" && (int)ViewState["ID"] != 0)
        {
            Org_StaffBLL _staffbll = new Org_StaffBLL((int)ViewState["ID"]);
            _staffbll.StaffInOrganizeCity_Add(int.Parse(ddl_StaffInOrganizeCity.SelectedValue));

            gv_StaffInOrganizeCity.BindGrid<Addr_OrganizeCity>(_staffbll.StaffInOrganizeCity_GetOrganizeCitys());
        }
    }

    protected void gv_StaffInOrganizeCity_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            int id = (int)gv_StaffInOrganizeCity.DataKeys[e.RowIndex][0];
            Org_StaffBLL _staffbll = new Org_StaffBLL((int)ViewState["ID"]);
            _staffbll.StaffInOrganizeCity_Delete(id);

            gv_StaffInOrganizeCity.BindGrid<Addr_OrganizeCity>(_staffbll.StaffInOrganizeCity_GetOrganizeCitys());
        }

    }

    protected void bt_CreateUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/Login/CreateUser.aspx?StaffID=" + ViewState["ID"].ToString());
    }

    protected void bt_AddApply_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 0)
        {
            MessageBox.Show(this, "对不起，请您先保存后在发起申请");
            return;
        }

        Org_StaffBLL bll = new Org_StaffBLL((int)ViewState["ID"]);
        if (Org_StaffNumberLimitBLL.CheckAllowAdd(bll.Model.OrganizeCity, bll.Model.Position) < 0)
        {
            MessageBox.Show(this, "对不起当前城市该职位员工人数满额，要想继续新增请与人事经理联系");
            return;
        }
        int budget = Org_StaffNumberLimitBLL.CheckOverBudget(bll.Model.OrganizeCity, bll.Model.Position);

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", ViewState["ID"].ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("Position", bll.Model.Position.ToString());
        dataobjects.Add("SalaryFlag", bll.Model["SalaryFlag"].ToString());
        dataobjects.Add("StaffName", bll.Model.RealName.ToString());
        dataobjects.Add("IsOverBudget", budget < 0 ? "1" : "2");



        int TaskID = EWF_TaskBLL.NewTask("Add_Staff", (int)Session["UserID"], "人员入职流程,姓名:" + bll.Model.RealName, "~/SubModule/StaffManage/StaffDetail.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model["TaskID"] = TaskID.ToString();
            bll.Model["State"] = "2";
            bll.Update();
        }

        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }

    protected void bt_TaskDetail_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 0)
        {
            MessageBox.Show(this, "对不起，当前还没有审批记录");
            return;
        }

        Org_StaffBLL bll = new Org_StaffBLL((int)ViewState["ID"]);

        Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + bll.Model["TaskID"].ToString());
    }

    protected string GetRolesByUserName(string username)
    {
        string ret = "";
        foreach (string s in Roles.GetRolesForUser(username))
        {
            ret += "<a href='../Login/RightAssign.aspx?RoleName=" + Server.UrlPathEncode(s) + "' target='_blank' style='color:Blue'>" + s + "</a> ";
        }

        return ret;
    }
    /// <summary>
    /// 审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            Org_StaffBLL _staff = new Org_StaffBLL((int)ViewState["ID"]);

            if (Org_StaffNumberLimitBLL.CheckAllowAdd(_staff.Model.OrganizeCity, _staff.Model.Position) < 0)
            {
                MessageBox.Show(this, "对不起当前城市该职位员工人数满额，要想继续新增请与人事经理联系");
                return;
            }

            _staff.Model.ApproveFlag = 1;
            _staff.Model.UpdateStaff = (int)Session["UserID"];
            _staff.Update();
            MessageBox.Show(this, "审核成功！");
            BindData();
        }
    }

    protected void bt_Print_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/StaffManage/StaffDetail_Print.aspx?ID=" + ViewState["ID"].ToString());
    }
    protected void bt_RevocationApply_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 0)
        {
            MessageBox.Show(this, "对不起，请您先保存后在发起申请");
            return;
        }
        //判断是否还有未审批流程
        if (EWF_Task_JobDecisionBLL.GetModelList("DecisionResult=1 AND RecipientStaff=" + ViewState["ID"].ToString()).Count > 0)
        {
            MessageBox.Show(this, "对不起，该人员还有未审批流程，暂不能发起离职流程！");
            return;
        }


        Org_StaffBLL bll = new Org_StaffBLL((int)ViewState["ID"]);
        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", ViewState["ID"].ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("Position", bll.Model.Position.ToString());
        dataobjects.Add("StaffName", bll.Model.RealName.ToString());
        int TaskID = EWF_TaskBLL.NewTask("Revocation_Staff",
                                        (int)Session["UserID"],
                                        TreeTableBLL.GetSuperNameByLevel("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "Name", "SuperID", bll.Model.OrganizeCity, ConfigHelper.GetConfigInt("OrganizePartCity-CityLevel")) +
                                        "  " + TreeTableBLL.GetSuperNameByLevel("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "Name", "SuperID", bll.Model.OrganizeCity, ConfigHelper.GetConfigInt("OrganizeCity-CityLevel")) + " " +
                                        new Org_PositionBLL(bll.Model.Position).Model.Name + " " +
                                        bll.Model.RealName + " 离职申请", "~/SubModule/StaffManage/StaffDetail.aspx?ID="
                                        + ViewState["ID"].ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model["TaskID"] = TaskID.ToString();
            bll.Model["State"] = "2";
            bll.Update();
        }
        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }
}
