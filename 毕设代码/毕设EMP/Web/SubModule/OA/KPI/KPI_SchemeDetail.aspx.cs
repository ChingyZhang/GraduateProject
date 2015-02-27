using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.OA;
using MCSFramework.BLL.OA;
using MCSFramework.Common;

public partial class SubModule_OA_KPI_KPI_SchemeDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
            else
            {
                bt_Add.Visible = false;
                bt_Approve.Visible = false;
                bt_del.Visible = false;
                bt_CancelApprove.Visible = false;
            }
        }
    }
    private void BindData()
    {
        KPI_Scheme model = new KPI_SchemeBLL((int)ViewState["ID"]).Model;
        DV_KPIScheme.BindData(model);
        gv_List.BindGrid(new KPI_SchemeBLL((int)ViewState["ID"]).Items);
        if (model.ApproveFlag == 1)
        {
            bt_Add.Visible = false;
            bt_Approve.Visible = false;
            bt_del.Visible = false;
            bt_Save.Visible = false;
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
        }
        else
        {
            bt_CancelApprove.Visible = false;
        }
    }


    protected void bt_del_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            new KPI_SchemeBLL((int)ViewState["ID"]).Delete();
            Response.Redirect("KPI_SchemeList.aspx");
        }
    }
    #region 审核与反审核考核方案
    private void doApprove(int ApproveFlag, string message)
    {
        if ((int)ViewState["ID"] != 0)
        {
            KPI_SchemeBLL bll = new KPI_SchemeBLL((int)ViewState["ID"]);
            bll.Model.ApproveFlag = ApproveFlag;
            bll.Update();
            MessageBox.ShowAndRedirect(this, message, "KPI_SchemeDetail.aspx?ID=" + ViewState["ID"].ToString());
        }
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        doApprove(1, "审核成功!");
    }
    protected void bt_CancelApprove_Click(object sender, EventArgs e)
    {
        doApprove(2, "取消审核成功！");
    }
    #endregion
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int detailID = int.Parse(gv_List.DataKeys[e.NewSelectedIndex]["ID"].ToString());
        ViewState["DetailID"] = detailID;
        KPI_SchemeDetail model = new KPI_SchemeBLL((int)ViewState["ID"]).GetDetailModel(detailID);
        DV_KPISchemeDetail.BindData(model);
        DV_KPISchemeDetail.Visible = true;
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int detailID = int.Parse(gv_List.DataKeys[e.RowIndex]["ID"].ToString());
        new KPI_SchemeBLL((int)ViewState["ID"]).DeleteDetail(detailID);
        BindData();
        MessageBox.Show(this, "删除成功！");
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        DV_KPISchemeDetail.Visible = true;
        DV_KPISchemeDetail.BindData(new KPI_SchemeDetail());
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        KPI_SchemeBLL bll = (int)ViewState["ID"] > 0 ? new KPI_SchemeBLL((int)ViewState["ID"]) : new KPI_SchemeBLL();
        DV_KPIScheme.GetData(bll.Model);
        if ((int)ViewState["ID"] > 0)
        {
            KPI_SchemeDetail detailModel = ViewState["DetailID"] == null ? new KPI_SchemeDetail() : bll.GetDetailModel((int)ViewState["DetailID"]);
            DV_KPISchemeDetail.GetData(detailModel);
            bll.Update();
            if (detailModel.ID > 0)
            {

                bll.UpdateDetail(detailModel);
            }
            else if (DV_KPISchemeDetail.Visible)
            {
                detailModel.Scheme = (int)ViewState["ID"];
                bll.AddDetail(detailModel);
            }
        }
        else
        {             
            ViewState["ID"] = bll.Add();
        }
        MessageBox.ShowAndRedirect(this, "保存成功！", "KPI_SchemeDetail.aspx?ID=" + ViewState["ID"].ToString());
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        gv_List.BindGrid(new KPI_SchemeBLL((int)ViewState["ID"]).Items);
        gv_List.SelectedIndex = -1;
        DV_KPISchemeDetail.Visible = false;
        gv_List.BindGrid();
    }
}
