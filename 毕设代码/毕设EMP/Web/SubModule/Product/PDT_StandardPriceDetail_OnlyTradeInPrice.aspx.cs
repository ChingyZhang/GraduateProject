using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MCSControls.MCSTabControl;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSControls.MCSWebControls;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;

public partial class SubModule_Product_PDT_StandardPriceDetail_OnlyTradeInPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["PriceID"] = Request.QueryString["PriceID"] == null ? 0 : int.Parse(Request.QueryString["PriceID"]);
            #endregion

            BindDropDown();
            if ((int)ViewState["PriceID"] != 0)//修改客户价表
            {
                BindData();
            }
            else
            {
                Response.Redirect("~/SubModule/desktop.aspx");
            }
        }
    }

    private void BindData()
    {
        PDT_StandardPriceBLL _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
        panel1.BindData(_bll.Model);
        panel1.SetControlsEnable(false);

        BindGrid();

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion


    private void BindGrid()
    {
        string condition = " 1 = 1 ";

        condition += " ORDER BY PDT_Product.Code";

        PDT_StandardPriceBLL bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
        gv_List.BindGrid(bll.GetDetail(condition));
    }

    
    public string GetERPCode(string ProductID)
    {
        return new PDT_ProductBLL(int.Parse(ProductID)).Model.Code;
    }
    
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        string filename = HttpUtility.UrlEncode("价盘导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "").Replace("<br />", "");

        Response.Write(outhtml.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
}
