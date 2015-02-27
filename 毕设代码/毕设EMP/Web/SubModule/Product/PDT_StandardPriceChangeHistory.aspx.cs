using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;

public partial class SubModule_Product_PDT_StandardPriceChangeHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] == 0)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }

            BindData();
        }
    }

    private void BindData()
    {
        gv_List.ConditionString = " StandardPrice=" + ViewState["ID"];
        gv_List.BindGrid();
    }

    protected string showChangeType(int type)
    {
        switch(type)
        {
            case 1:
                return "新增";
            case 2: 
                return "修改";
            case 3: 
                return "删除";
            default:
                return "";
        }
    }
  
}
