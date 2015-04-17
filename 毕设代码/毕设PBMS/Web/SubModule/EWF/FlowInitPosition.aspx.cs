using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.EWF;
using System.Data;

public partial class SubModule_EWF_FlowInitPosition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["AppID"] = Request.QueryString["AppID"] != null ? new Guid(Request.QueryString["AppID"]) : Guid.Empty;
            #endregion
            BindDropDown();

            if ((Guid)ViewState["AppID"] != Guid.Empty)
            {
                BindGrid();
            }
            else
            {
                Response.Redirect("FlowAppList.aspx");
            }
        }
    }

    private void BindDropDown()
    {
        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
        tr_Position.DataBind();
    }

    private void BindGrid()
    {
        IList<EWF_Flow_InitPosition> list = EWF_Flow_InitPositionBLL.GetModelList("App='" + ViewState["AppID"].ToString() + "'");
        gv_List.BindGrid<EWF_Flow_InitPosition>(list);
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if (tr_Position.SelectValue != "0")
        {
            int position = int.Parse(tr_Position.SelectValue);
            int beginday = int.Parse(tbx_BeginDay.Text);
            int endday = int.Parse(tbx_EndDay.Text);

            EWF_Flow_InitPositionBLL bll = new EWF_Flow_InitPositionBLL();
            bll.Model.App = (Guid)ViewState["AppID"];
            bll.Model.Position = position;
            bll.Model.BeginDay = beginday;
            bll.Model.EndDay = endday;
            bll.Add();

            if (cb_IncludeChild.Checked)
            {
                DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Org_Position", "ID", "SuperID", tr_Position.SelectValue);

                foreach (DataRow dr in dt.Rows)
                {
                    bll.Model.ID = Guid.NewGuid();
                    bll.Model.Position = (int)dr["ID"];
                    bll.Add();
                }
            }
        }
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
            Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
        else if (e.Index == 2)
            Response.Redirect("FlowDataObjectList.aspx?AppID=" + ViewState["AppID"].ToString());
        else if (e.Index == 0)
            Response.Redirect("FlowAppDetail.aspx?AppID=" + ViewState["AppID"].ToString());
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = (Guid)gv_List.DataKeys[e.RowIndex][0];

        new EWF_Flow_InitPositionBLL(id).Delete();

        BindGrid();
    }
}
