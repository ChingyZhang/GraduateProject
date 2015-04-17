using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Model.PBM;

public partial class SubModule_PBM_Order_DeliveryListHasAssign : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {

            tbx_begin.Text = DateTime.Today.ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

            BindDropDown();

            if (!string.IsNullOrEmpty(Request.QueryString["BeginDate"]))
                tbx_begin.Text = Request.QueryString["BeginDate"];
            if (!string.IsNullOrEmpty(Request.QueryString["EndDate"]))
                tbx_end.Text = Request.QueryString["EndDate"];


            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Salesman.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.ID IN (SELECT SalesMan FROM MCS_PBM.dbo.PBM_Delivery WHERE Supplier=" + Session["OwnerClient"].ToString() +
            " AND InsertTime>DATEADD(MONTH,-6,GETDATE()) ) AND Org_Staff.Dimission = 1");
        ddl_Salesman.DataBind();
        ddl_Salesman.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_DeliveryMan.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Dimission=1");
        ddl_DeliveryMan.DataBind();
        ddl_DeliveryMan.Items.Insert(0, new ListItem("请选择", "0"));

        ddl_SupplierWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]);
        ddl_SupplierWareHouse.DataBind();

    }
    #endregion

    private void BindGrid()
    {
        gv_Summary.Visible = false;
        gv_List.Visible = false;
        bt_SummaryPrint.Visible = false;
        bt_DetailPrint.Visible = false;
        bt_BatConfirm.Enabled = false;

        if (MCSTabControl1.SelectedIndex == 0)
        {
            DataTable dt = PBM_DeliveryBLL.GetNeedDeliverySummary(DateTime.Parse(tbx_begin.Text), DateTime.Parse(tbx_end.Text),
            (int)Session["OwnerClient"], int.Parse(ddl_SupplierWareHouse.SelectedValue),
            int.Parse(ddl_Salesman.SelectedValue), int.Parse(ddl_DeliveryMan.SelectedValue));


            #region 求合计行
            int q_t = 0, q_p = 0;
            decimal w = 0, a = 0;
            foreach (DataRow row in dt.Rows)
            {
                q_t += (int)row["Quantity_T"];
                q_p += (int)row["Quantity_P"];
                a += (decimal)row["Amount"];
                w += (decimal)row["Weight"];
            }

            DataRow dr = dt.NewRow();
            dr["ProductName"] = "合计";
            dr["Quantity_T"] = q_t;
            dr["Packagint_T"] = "件";
            dr["Quantity_P"] = q_p;
            dr["Packagint_P"] = "散";
            dr["Amount"] = a;
            dr["Weight"] = w;

            dt.Rows.Add(dr);
            #endregion

            gv_Summary.DataSource = dt;
            gv_Summary.DataBind();

            gv_Summary.Visible = true;
            bt_SummaryPrint.Visible = true;
            bt_SummaryPrint.OnClientClick = "javascript:window.open('" + string.Format("DeliveryListHasAssign_Print.aspx?PreArrivalDate_BeginDate={0}&PreArrivalDate_EndDate={1}&SupplierWareHouse={2}&SalesMan={3}&DeliveryMan={4}",
    tbx_begin.Text, tbx_end.Text, ddl_SupplierWareHouse.SelectedValue, ddl_Salesman.SelectedValue, ddl_DeliveryMan.SelectedValue) + "');";
        }
        else
        {
            string condition = GetFindCondition();
            gv_List.ConditionString = condition;
            gv_List.BindGrid();
            gv_List.Visible = true;

            IList<PBM_Delivery> lists = PBM_DeliveryBLL.GetModelList(condition);
            string ids = "";
            foreach (PBM_Delivery item in lists)
            {
                ids += item.ID.ToString() + ",";
            }
            if (ids.EndsWith(",")) ids = ids.Substring(0, ids.Length - 1);

            bt_BatConfirm.Enabled = true;
            bt_DetailPrint.Visible = true;
            bt_DetailPrint.OnClientClick = "javascript:window.open('../Delivery/SaleOut/SaleOutDetail_Print.aspx?ID=" + ids + "');";
        }

    }

    private string GetFindCondition()
    {
        //已派单的销售单、退货单、赠送单
        string ConditionStr = " PBM_Delivery.Classify IN (1,2,4) AND PBM_Delivery.State IN (2,3) AND PBM_Delivery.PrepareMode = 3";

        ConditionStr += " AND PBM_Delivery.PreArrivalDate BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59'";

        if ((int)Session["OwnerType"] == 3)
        {
            ConditionStr += " AND PBM_Delivery.Supplier = " + Session["OwnerClient"].ToString();
        }

        if (ddl_SupplierWareHouse.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.SupplierWareHouse = " + ddl_SupplierWareHouse.SelectedValue;
        }

        if (ddl_Salesman.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.Salesman = " + ddl_Salesman.SelectedValue;
        }

        if (ddl_DeliveryMan.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.DeliveryMan=" + ddl_DeliveryMan.SelectedValue;
        }

        return ConditionStr;
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Visible)
            {
                cbx.Checked = ((CheckBox)sender).Checked;
            }
        }
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindGrid();
    }
    protected void gv_Summary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Summary.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void bt_BatConfirm_Click(object sender, EventArgs e)
    {
        int success = 0;
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["PBM_Delivery_ID"];
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            {
                if (cbx != null && cbx.Checked)
                {
                    PBM_DeliveryBLL _bll = new PBM_DeliveryBLL(id);
                    if (_bll.Model == null) continue;
                    if (_bll.Model.State >= 4) continue;

                    IList<PBM_DeliveryPayInfo> payinfos = _bll.GetPayInfoList();
                    if (payinfos.Count == 0 && _bll.Model.ActAmount != 0)
                    {
                        #region 默认现金收款
                        PBM_DeliveryPayInfoBLL paybll = new PBM_DeliveryPayInfoBLL();
                        paybll.Model.DeliveryID = _bll.Model.ID;
                        paybll.Model.PayMode = 1;
                        paybll.Model.Amount = _bll.Model.ActAmount;
                        paybll.Model.ApproveFlag = 2;
                        paybll.Model.InsertStaff = (int)Session["UserID"];
                        paybll.Add();
                        #endregion
                    }
                    else if (payinfos.Sum(p => p.Amount) != _bll.Model.ActAmount)
                    {
                        continue;
                    }

                    int confirmstaff = (int)Session["UserID"];
                    if (_bll.Model.PrepareMode > 1 && _bll.Model.DeliveryMan > 0)
                    {
                        //车销与预售模式，在确认时，如果有送货人，则以送货人为确认人。以便将收款记录关联在该员工头上
                        confirmstaff = _bll.Model.DeliveryMan;
                    }

                    if (_bll.Confirm(confirmstaff) == 0) success++;
                }
            }
        }

        MessageBox.Show(this, "成功确认" + success.ToString() + "条发货单!");
        gv_Summary.PageIndex = 0;
        gv_List.PageIndex = 0;
        BindGrid();
    }
}