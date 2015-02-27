using System;
using System.Linq;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Promotor;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model;
using System.Collections.Generic;
using MCSFramework.UD_Control;


public partial class SubModule_CM_PD_PropertyDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Request.QueryString["Mode"] != "New" && Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }
            #endregion

            if (ViewState["ClientID"] != null)
            {
                BindData();
                BindDropDown();
            }
            else if (Request.QueryString["Mode"] == "New")
            {
                #region 新增物业时的初始值
                Org_Staff staff = new Org_StaffBLL((int)Session["UserID"]).Model;
                if (staff == null) Response.Redirect("~/SubModule/Desktop.aspx");

                #region 新增客户时，详细资料界面控件初始化
                DropDownList ddl_ActiveFlag = (DropDownList)pl_detail.FindControl("CM_Client_ActiveFlag");
                if (ddl_ActiveFlag != null) ddl_ActiveFlag.SelectedValue = "4";

                MCSTreeControl tr_OrganizeCity = (MCSTreeControl)pl_detail.FindControl("CM_Client_OrganizeCity");
                if (tr_OrganizeCity != null) tr_OrganizeCity.SelectValue = staff.OrganizeCity.ToString();

                MCSTreeControl tr_OfficalCity = (MCSTreeControl)pl_detail.FindControl("CM_Client_OfficalCity");
                if (tr_OfficalCity != null) tr_OfficalCity.SelectValue = staff.OfficialCity.ToString();

                #endregion


                bt_Approve.Visible = false;
                tr_Contract.Visible = false;
                tr_Staff.Visible = false;
                tr_PropertyInOrganizeCity.Visible = false;
                #endregion
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "请先在物业列表’中选择要查看的物业！", "PropertyList.aspx?URL=" + Request.Url.PathAndQuery);
            }
        }
    }

    #region 绑定下拉列表框
    public void BindDropDown()
    {

    }
    #endregion

    private void BindData()
    {
        CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);
        pl_detail.BindData(bll.Model);

        if (bll.Model.ApproveFlag == 1)
        {
            Header.Attributes["WebPageSubCode"] = "Modify";
            bt_Approve.Visible = false;
        }

        BindGrid();
    }

    private void BindGrid()
    {
        if (ViewState["ClientID"] != null)
        {
            CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);

            #region 绑定住宿人员
            gv_Staff.ConditionString = "MCS_CM.dbo.CM_StaffInProperty.Client = " + ViewState["ClientID"].ToString() + " AND Org_Staff.Dimission=1";
            gv_Staff.BindGrid();
            #endregion

            #region 绑定兼管片区
            if (bll.Model.OrganizeCity > 1)
            {
                ddl_PropertyInOrganizeCity.Items.Clear();

                int superid = new Addr_OrganizeCityBLL(bll.Model.OrganizeCity).Model.SuperID;
                IList<Addr_OrganizeCity> lists = Addr_OrganizeCityBLL.GetModelList("SuperID = " + superid.ToString() +
                    " AND ID <> " + bll.Model.OrganizeCity.ToString() +
                    " AND ID NOT IN (SELECT OrganizeCity FROM MCS_CM.dbo.CM_PropertyInOrganizeCity WHERE Client=" + bll.Model.ID.ToString() + ")");
                if (lists.Count > 0)
                {
                    foreach (Addr_OrganizeCity city in lists)
                    {
                        ddl_PropertyInOrganizeCity.Items.Add(new ListItem("(" + city.Code + ")" + city.Name, city.ID.ToString()));
                    }
                }

                ddl_PropertyInOrganizeCity.Items.Insert(0, new ListItem("请选择...", "0"));

                gv_PropertyInOrganizeCity.BindGrid<Addr_OrganizeCity>(bll.ClientInOrganizeCity_GetOrganizeCitys());
            }
            else
            {
                tr_PropertyInOrganizeCity.Visible = false;
            }
            #endregion

            #region 绑定电话列表
            gv_Telephone.ConditionString = "Client=" + bll.Model.ID.ToString();
            gv_Telephone.BindGrid();
            #endregion

            #region 绑定合同列表
            gv_List_Contract.BindGrid(CM_ContractBLL.GetModelList("Client=" + bll.Model.ID.ToString()));
            #endregion


        }
    }

    protected void gv_List_Contract_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List_Contract.DataKeys[e.Row.RowIndex][0];
            UC_GridView gv_Detail = (UC_GridView)e.Row.FindControl("gv_Detail");
            if (gv_Detail != null)
                gv_Detail.BindGrid(new CM_ContractBLL(id).Items);
        }
    }

    protected void gv_PropertyInOrganizeCity_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            int organizecity = (int)gv_PropertyInOrganizeCity.DataKeys[e.RowIndex]["ID"];
            CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            bll.ClientInOrganizeCity_Delete(organizecity);
            BindGrid();
        }
    }
    protected void gv_Staff_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            int organizecity = (int)gv_Staff.DataKeys[e.RowIndex]["Org_Staff_ID"];
            CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            bll.StaffInProperty_Delete((int)ViewState["ClientID"], organizecity);
            BindGrid();
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ClientBLL _bll = null;
        if (ViewState["ClientID"] == null)
        {
            _bll = new CM_ClientBLL();

        }
        else
        {
            _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
        }

        pl_detail.GetData(_bll.Model);



        if (ViewState["ClientID"] == null)
        {
            _bll.Model.ClientType = 6;
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ClientID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
        }

        MessageBox.ShowAndRedirect(this, "保存物业资料成功！", "PropertyDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());


    }
    protected void gv_List_Contract_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List_Contract.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("PropertyContractDetail.aspx?ContractID=" + id);
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ClientID"] != 0)
        {
            CM_ClientBLL _bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            _bll.Model.ApproveFlag = 1;
            _bll.Model.ActiveFlag = 1;
            if (_bll.Model.OpenTime.Year == 1900) _bll.Model.OpenTime = DateTime.Today;
            _bll.Model.CloseTime = new DateTime(1900, 1, 1);
            _bll.Update();
            MessageBox.ShowAndRedirect(this, "审核物业资料成功！", "PropertyDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
    }

    protected void bt_Add_PropertyInOrganizeCity_Click(object sender, EventArgs e)
    {
        if (ddl_PropertyInOrganizeCity.SelectedValue != "0" && (int)ViewState["ClientID"] != 0)
        {
            CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);
            if (bll.ClientInOrganizeCity_GetOrganizeCitys().FirstOrDefault(p => p.ID == int.Parse(ddl_PropertyInOrganizeCity.SelectedValue)) == null)
            {
                bll.ClientInOrganizeCity_Add(int.Parse(ddl_PropertyInOrganizeCity.SelectedValue));
                BindGrid();
            }
            else
            {
                MessageBox.Show(this, "请勿重复添加该区域!");
                return;
            }
        }
    }
    protected void bt_AddStaff_Click(object sender, EventArgs e)
    {
        if (select_Staff.SelectValue != "" && (int)ViewState["ClientID"] != 0)
        {
            string failed = "";
            foreach (string s in select_Staff.SelectValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int staff = 0;
                if (int.TryParse(s, out staff) && staff > 0)
                {
                    CM_ClientBLL bll = new CM_ClientBLL((int)ViewState["ClientID"]);
                    if (bll.StaffInProperty_Add((int)ViewState["ClientID"], staff) == 0)
                    {
                        failed += new Org_StaffBLL(staff).Model.RealName + ",";
                    }
                    BindGrid();
                }
            }

            if (failed != "")
                MessageBox.Show(this, "对不起，" + failed + "员工已住宿在公司住房中！");
        }
    }
    protected void bt_AddContract_Click(object sender, EventArgs e)
    {
        Response.Redirect("PropertyContractDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }

    protected void bt_AddTele_Click(object sender, EventArgs e)
    {
        Response.Redirect("PropertyInTelephoneDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }
}
