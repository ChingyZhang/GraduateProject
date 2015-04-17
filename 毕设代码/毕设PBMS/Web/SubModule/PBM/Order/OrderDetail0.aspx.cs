using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Order_OrderDetail0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Classify"] = Request.QueryString["Classify"] != null ? int.Parse(Request.QueryString["Classify"]) : 0;
            #endregion

            BindDropDown();

            if (DateTime.Now.Hour < 12)
                tbx_ArriveTime.Text = DateTime.Today.ToString("yyyy-MM-dd");
            else
                tbx_ArriveTime.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

            if ((int)ViewState["Classify"] == 2)
            {
                bt_OK.Text = "新增退货订单";
            }
        }
    }

    private void BindDropDown()
    {
        ddl_Salesman.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString());
        ddl_Salesman.DataBind();
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int client = 0, salesman = 0;
        int.TryParse(select_Client.SelectValue, out client);
        int.TryParse(ddl_Salesman.SelectedValue, out salesman);

        if (client == 0)
        {
            MessageBox.Show(this, "请正确选择零售店!");
            return;
        }

        PBM_OrderBLL bll = new PBM_OrderBLL();
        bll.Model.Supplier = (int)Session["OwnerClient"];
        bll.Model.Client = client;
        bll.Model.SalesMan = salesman;
        bll.Model.Classify = (int)ViewState["Classify"] == 0 ? 1 : (int)ViewState["Classify"];
        bll.Model.ArriveTime = DateTime.Parse(tbx_ArriveTime.Text);
        bll.Model.InsertStaff = (int)Session["UserID"];
        bll.Model.State = 1;
        bll.Model.ApproveFlag = 2;

        int id = bll.Add();
        Response.Redirect("OrderDetail.aspx?ID=" + id.ToString());

        //Response.Redirect("SaleOutDeail.aspx?Supplier=" + ddl_Supplier.SelectedValue +
        //    "&WareHouse=" + ddl_ClientWareHouse.SelectedValue + "&Salesman=" + ddl_Salesman.SelectedValue);
    }
}