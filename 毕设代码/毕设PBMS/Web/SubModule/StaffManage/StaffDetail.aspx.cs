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
            if (tr_Position != null)
            {
                if (p != null && p.IsHeadOffice != "Y" && p.Remark != "OfficeHR")  //备注为"OfficeHR":人事经理,可以选择全部职位
                {
                    tr_Position.RootValue = p.SuperID.ToString();
                    tr_Position.SelectValue = staff.Model.Position.ToString();
                }
                else
                {
                    tr_Position.RootValue = "1";
                    tr_Position.SelectValue = "1";
                }
            }
            #endregion

            DropDownList ddl_Dimission = (DropDownList)panel1.FindControl("Org_Staff_Dimission");
            if (ddl_Dimission != null) ddl_Dimission.Enabled = false;
            TextBox tbx_EndWorkTime = (TextBox)panel1.FindControl("Org_Staff_EndWorkTime");
            if (tbx_EndWorkTime != null) tbx_EndWorkTime.Enabled = false;

            #region 非平台用户只能新增本层级用户
            if ((int)Session["OwnerType"] == 2 || (int)Session["OwnerType"] == 3)
            {
                DropDownList ddl_OwnerType = (DropDownList)panel1.FindControl("Org_Staff_OwnerType");
                MCSSelectControl select_OwnerClient = (MCSSelectControl)panel1.FindControl("Org_Staff_OwnerClient");
                if (ddl_OwnerType != null && select_OwnerClient != null)
                {
                    ddl_OwnerType.SelectedValue = Session["OwnerType"].ToString();
                    ddl_OwnerType.Enabled = false;

                    select_OwnerClient.SelectValue = Session["OwnerClient"].ToString();
                    select_OwnerClient.SelectText = Session["OwnerClientName"].ToString();
                    select_OwnerClient.Enabled = false;
                }
            }
            #endregion
            

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
        if (bll.Model == null) return;

        panel1.BindData(bll.Model);

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
            Header.Attributes["WebPageSubCode"] = "Modify";
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

                    if (currentcity.Level > 1)
                    {
                        int grandpacityid = new Addr_OrganizeCityBLL(currentcity.SuperID).Model.SuperID;
                        IList<Addr_OrganizeCity> bobos = Addr_OrganizeCityBLL.GetModelList("SuperID = " + grandpacityid.ToString() + " AND ID <> " + currentcity.SuperID.ToString());
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

        Org_Staff _staffM = new Org_StaffBLL((int)Session["UserID"]).Model;
        if ((int)ViewState["ID"] == 0)
        {
            #region 根据身份证号判断是否重复
            if (_staffbll.Model["IDCode"] != string.Empty &&
                Org_StaffBLL.GetStaffList("IDCode='" + _staffbll.Model["IDCode"] + "' AND Dimission=1").Count > 0)
            {
                MessageBox.Show(this, "对不起，该身份证号的员工已在系统中入职，请核实后再新增员工!");
                return;
            }
            #endregion

            _staffbll.Model.InsertStaff = (int)Session["UserID"];
            _staffbll.Model.InsertTime = DateTime.Now;
            _staffbll.Model.ApproveFlag = 2;            //未审批
            _staffbll.Model.Dimission = 1;              //在职
            ViewState["ID"] = _staffbll.Add();
        }
        else
        {
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

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", ViewState["ID"].ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("Position", bll.Model.Position.ToString());
        dataobjects.Add("SalaryFlag", bll.Model["SalaryFlag"].ToString());
        dataobjects.Add("StaffName", bll.Model.RealName.ToString());

        int TaskID = EWF_TaskBLL.NewTask("Add_Staff", (int)Session["UserID"], "人员入职流程,姓名:" + bll.Model.RealName, "~/SubModule/StaffManage/StaffDetail.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model["TaskID"] = TaskID.ToString();
            bll.Model["State"] = "2";
            bll.Update();
        }

        Response.Redirect("~/SubModule/EWF/Apply.aspx?TaskID=" + TaskID.ToString());
    }

    protected void bt_RevocationApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/EWF/FlowAppInitList.aspx");
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
            if (_staff.Model != null && _staff.Model.ApproveFlag == 2)
            {
                if (_staff.Approve() == 0)
                {
                    MessageBox.Show(this, "审核成功！");
                    BindData();
                }
            }
        }
    }

    protected void bt_Print_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/StaffManage/StaffDetail_Print.aspx?ID=" + ViewState["ID"].ToString());
    }
}
