using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.Logistics;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using System.Data;

public partial class SubModule_Logistics_Order_OrderStorageDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["CarrySheetCode"] = Request.QueryString["CarrySheetCode"];

            BindTableData();

        }
    }
    private void BindTableData()
    {
        string strID = ViewState["CarrySheetCode"].ToString();
        DataTable dt = InitOrderDeliveryList(strID);
        string strSqldel = " DELETE MCS_Logistics.dbo.ORD_OrderStorage";
        SQLDatabase.RunSQL(strSqldel);
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            string strSql = " INSERT INTO MCS_Logistics.dbo.ORD_OrderStorage VALUES ('" + dt.Rows[j]["AID"].ToString() + "','" + dt.Rows[j]["BillNo"].ToString() + "','" + dt.Rows[j]["ProdID"].ToString() + "','" + dt.Rows[j]["milkname"].ToString() + "','" + dt.Rows[j]["Spec"].ToString() + "','" + dt.Rows[j]["BarCode"].ToString() + "','" + dt.Rows[j]["Unit"].ToString() + "','" + DateTime.Parse(dt.Rows[j]["Scantime"].ToString()) + "','" + dt.Rows[j]["Source"].ToString() + "')";
            SQLDatabase.RunSQL(strSql);
        }

        ORD_OrderDeliveryBLL ordBll = new ORD_OrderDeliveryBLL();
        DataTable dtStorage = ordBll.GetOrderStorageDetailList(strID);

        gv_OrderList.DataSource = dtStorage;
        gv_OrderList.DataBind();
    }

    public DataTable InitOrderDeliveryList(string OutputNO)
    {
        SqlDataReader dr = null;
        #region	设置参数集
        SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OutputNO", SqlDbType.VarChar, 50, OutputNO),
				
			};
        #endregion

        SQLDatabase.RunProc("ERP26.YSL_DMS.dbo.emp_Barcode_Upload_Info", prams, out dr);

        return Tools.ConvertDataReaderToDataTable(dr);
    }
}
