using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Logistics;
using System.Data;

public partial class SubModule_Logistics_Order_OrderQuery_OrderApplyDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SheetCode"] = Request.QueryString["SheetCode"] == null ? "" : Request.QueryString["SheetCode"];
            BindData();
        }
    }

    private void BindData()
    {
        if (ViewState["SheetCode"].ToString() != "")
        {
            DataTable dt = ORD_OrderApplyBLL.GetRPTOrderDetail(ViewState["SheetCode"].ToString());
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lb_Fdate.Text = DateTime.Parse(row["fdate"].ToString()).ToShortDateString();
                lb_DICode.Text = row["fnumber"].ToString();
                lb_Addr.Text = row["ffetchadd"].ToString();
                lb_SheetCode.Text = ViewState["SheetCode"].ToString();
                lb_DIName.Text = row["经销商名称"].ToString();
                lb_CarryDI.Text = row["货运商"].ToString();
                lb_Remark.Text = row["fheadselfB0167"].ToString();
            }
            gv_ListDetail.DataSource = dt;
            gv_ListDetail.BindGrid();
        }
    }
}
