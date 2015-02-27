using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.Logistics;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using System.Data;
using MCSFramework.Common;
using MCSFramework.BLL.SVM;
using System.Text;

public partial class SubModule_Logistics_Order_OrderAlibraryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
            int _relateclient = 0;
            if (staff.Model["RelateClient"] != "" && int.TryParse(staff.Model["RelateClient"], out _relateclient))
            {
                ViewState["ClientID"] = _relateclient;
                select_Client.Enabled = false;
            }
            else
            {
                if (Request.QueryString["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                    Session["ClientID"] = ViewState["ClientID"];
                }
                else if (Session["ClientID"] != null && Request.QueryString["State"] == null && Request.QueryString["ApproveFlag"] == null)
                {
                    ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
                }
            }
            #endregion

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;

            }
        }
    }

    private void BindGrid()
    {
        if (select_Client.SelectValue != "" && this.txtsDate.Text != "" && this.txteDate.Text != "")
        {
            ORD_OrderDeliveryBLL ordBll = new ORD_OrderDeliveryBLL();
            DataTable dt =ordBll.InitOrderDeliveryList(select_Client.SelectValue,txtsDate.Text, txteDate.Text);

            string strSqldel = " DELETE MCS_Logistics.dbo.ORD_OrderAlibrary";
            SQLDatabase.RunSQL(strSqldel);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string strSql = " INSERT INTO MCS_Logistics.dbo.ORD_OrderAlibrary VALUES (" + int.Parse(dt.Rows[j]["OrderId"].ToString()) + ",'" + dt.Rows[j]["OrderNo"].ToString() + "','" + dt.Rows[j]["JxsId"].ToString() + "','" + dt.Rows[j]["经销商名称"].ToString() + "','" + dt.Rows[j]["StoreId"].ToString() + "','" + dt.Rows[j]["下级客户全称"].ToString() + "','" + dt.Rows[j]["barcode"].ToString() + "','" + dt.Rows[j]["Milkid"].ToString() + "','" + dt.Rows[j]["milkname"].ToString() + "','" + dt.Rows[j]["spec"].ToString() + "','" + dt.Rows[j]["Unit"].ToString() + "','" + DateTime.Parse(dt.Rows[j]["Adate"].ToString()) + "')";
                SQLDatabase.RunSQL(strSql);
            }

            DataTable dtAlibrary = ordBll.InitOrderAlibrayList();

            gv_OrderList.DataSource = dtAlibrary;
            gv_OrderList.DataBind();
        }


    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_OrderList.PageIndex = 0;
        BindGrid();
    }

    protected void gv_OrderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_OrderList.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}

