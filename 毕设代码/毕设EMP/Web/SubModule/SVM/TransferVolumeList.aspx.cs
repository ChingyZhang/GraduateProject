using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.SVM;
using MCSFramework.Common;
using System.Data;

public partial class SubModule_SVM_TransferVolumeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            #endregion

            BindDropDown();

            //查找过滤添加：0：所有 1：审核 2：未审核
            if (Request.QueryString["ApproveFlag"] != null)
            {
                Session["ClientID"] = null;
                ViewState["ClientID"] = null;
                rbl_ApproveFlag.SelectedValue = Request.QueryString["ApproveFlag"];
            }

            tbx_begin.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2";
            select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2";

            #region 调出客户的管理片区为当前人的上一级，以便向管辖外的区域调货
            try
            {
                int organizecity = new Org_StaffBLL((int)Session["UserID"]).Model.OrganizeCity;
                if (organizecity > 1)
                {
                    int superid = new Addr_OrganizeCityBLL(organizecity).Model.SuperID;

                    select_Client.PageUrl += "&OrganizeCity=" + superid.ToString();
                }
            }
            catch { }
            #endregion
            if (Request.QueryString["TransferOutClientID"] != null || Request.QueryString["TransferInClientID"] != null)
            {
                if (Request.QueryString["TransferOutClientID"] != null)
                {
                    CM_ClientBLL _s = new CM_ClientBLL(int.Parse(Request.QueryString["TransferOutClientID"]));
                    select_Supplier.SelectText = _s.Model.FullName;
                    select_Supplier.SelectValue = _s.Model.ID.ToString();
                }
                if (Request.QueryString["TransferInClientID"] != null)
                {
                    CM_ClientBLL _r = new CM_ClientBLL(int.Parse(Request.QueryString["TransferInClientID"]));
                    select_Client.SelectText = _r.Model.FullName;
                    select_Client.SelectValue = _r.Model.ID.ToString();
                }
                BindGrid();
            }
            else if (ViewState["ClientID"] != null)
            {
                Response.Redirect("TransferVolumeList.aspx?TransferOutClientID=" + ViewState["ClientID"].ToString());
            }
            else if (Request.QueryString["ApproveFlag"] != null)
            {
                BindGrid();
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        #region 绑定用户可管辖的片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        tr_OrganizeCity_Selected(null, null);
        #endregion

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";

    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2" + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_Supplier.SelectText = "";
        select_Supplier.SelectValue = "";

        select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2" + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_Client.SelectText = "";
        select_Client.SelectValue = "";
    }
    #endregion

    private void BindGrid()
    {
        DateTime dtBegin = DateTime.Parse(this.tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(this.tbx_end.Text).AddDays(1);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);

        if (tr_detail.Visible)
        {
            string condition = " SVM_SalesVolume.SalesDate between '" + dtBegin.ToString() + "' AND '" + dtEnd.ToString() +
                "' AND SVM_SalesVolume.Type = 6";

            if (rbl_ApproveFlag.SelectedValue != "0")
            {
                condition += " And SVM_SalesVolume.ApproveFlag =" + rbl_ApproveFlag.SelectedValue;
            }

            //管理片区及所有下属管理片区
            //销量查询可以不根据某个指定的客户，根据一个片区范围来查找
            if (select_Supplier.SelectValue != "")
            {
                condition += " AND SVM_SalesVolume.Supplier = " + select_Supplier.SelectValue;
            }

            if (select_Client.SelectValue != "")
            {
                condition += " AND SVM_SalesVolume.Client =" + select_Client.SelectValue;
            }

            if (organizecity > 1 && select_Supplier.SelectValue == "" && select_Client.SelectValue == "")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                condition += " AND SVM_SalesVolume.OrganizeCity IN (" + orgcitys + ")";
            }

            gv_List.ConditionString = condition;
            gv_List.BindGrid();
        }
        else
        {
            int supplier = select_Supplier.SelectValue == "" ? 0 : int.Parse(select_Supplier.SelectValue);
            int client = select_Client.SelectValue == "" ? 0 : int.Parse(select_Client.SelectValue);
            if (organizecity == 0) organizecity = 1;

            DataTable dt = SVM_SalesVolumeBLL.GetSummary(organizecity, supplier, client, dtBegin, dtEnd, 6, 0);
            int _quantity = 0;
            decimal _totalvalue = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _quantity += (int)dt.Rows[i]["SumQuantity"];
                _totalvalue += (decimal)dt.Rows[i]["SumFactoryMoney"];
            }
            DataRow dr = dt.NewRow();
            dr["ProductCode"] = "合计";
            dr["SumQuantity"] = _quantity;
            dr["SumMoney"] = _totalvalue;
            dt.Rows.Add(dr);
            gv_Summary.DataSource = dt;
            gv_Summary.DataBind();
        }


    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            tr_detail.Visible = true;
            tr_summary.Visible = false;
            rbl_ApproveFlag.Visible = true;
            MCSTabControl1.SelectedIndex = 0;
            gv_List.PageIndex = 0;
        }
        else
        {
            tr_detail.Visible = false;
            tr_summary.Visible = true;
            rbl_ApproveFlag.Visible = false;
            MCSTabControl1.SelectedIndex = 1;
        }
        BindGrid();
    }

    protected void rbl_ApproveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    public string GetTotalValue(string SalesVolumeID)
    {
        return new SVM_SalesVolumeBLL(int.Parse(SalesVolumeID)).GetTotalValue().ToString("f2");
    }

    public string GetTotalFactoryPriceValue(string SalesVolumeID)
    {
        return new SVM_SalesVolumeBLL(int.Parse(SalesVolumeID)).GetTotalFactoryPriceValue().ToString("f2");
    }

    protected void bt_BatchInput_Click(object sender, EventArgs e)
    {
        int transferinclient = 0, transferoutclient = 0;
        int.TryParse(select_Client.SelectValue, out transferinclient);
        int.TryParse(select_Supplier.SelectValue, out transferoutclient);
        if (transferinclient > 0 && transferoutclient > 0)
        {
            Response.Redirect("TransferVolumeDetail.aspx?TransferOutClientID=" + transferoutclient.ToString() + "&TransferInClientID=" + transferinclient.ToString());
        }
        else if (transferoutclient > 0)
        {
            Response.Redirect("TransferVolumeDetail.aspx?TransferOutClientID=" + transferoutclient.ToString());
        }
        else
        {
            MessageBox.Show(this, "对不起，必须选择要调出货的经销商!");
            return;
        }
    }

    protected void bt_BatchInput2_Click(object sender, EventArgs e)
    {
        int transferinclient = 0, transferoutclient = 0;
        int.TryParse(select_Client.SelectValue, out transferinclient);
        int.TryParse(select_Supplier.SelectValue, out transferoutclient);
        if (transferinclient > 0 && transferoutclient > 0)
        {
            Response.Redirect("TransferVolumeDetail.aspx?TransferOutClientID=" + transferoutclient.ToString() + "&TransferInClientID=" + transferinclient.ToString() + "&IsCXP=1");
        }
        else if (transferoutclient > 0)
        {
            Response.Redirect("TransferVolumeDetail.aspx?TransferOutClientID=" + transferoutclient.ToString() + "&IsCXP=1");
        }
        else
        {
            MessageBox.Show(this, "对不起，必须选择要调出货的经销商!");
            return;
        }
    }

    protected void bt_BathApprove_Click(object sender, EventArgs e)
    {
        string ids = "";
        foreach (GridViewRow gr in gv_List.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                ids += gv_List.DataKeys[gr.RowIndex]["SVM_SalesVolume_ID"].ToString() + ",";
            }
        }

        SVM_SalesVolumeBLL.BatApprove(ids, (int)Session["UserID"]);
        BindGrid();       
    }
}
