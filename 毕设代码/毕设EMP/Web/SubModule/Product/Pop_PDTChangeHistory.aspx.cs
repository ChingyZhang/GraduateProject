using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.CSO;
using MCSFramework.BLL.Pub;

public partial class SubModule_CM_Pop_PDTChangeHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            //ViewState["TableName"] = Request.QueryString["TableName"] == null ? "0" : Request.QueryString["TableName"].ToString();
            if ((int)ViewState["ID"] != 0)
            {
                ich_Product.InfoID = (int)ViewState["ID"];
                ich_Product.Message = "【" + new PDT_ProductBLL(ich_Product.InfoID).Model.FullName.ToString() + "】修改记录";
            }
        }
    }
}
