using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Logistics;
using MCSFramework.Common;

public partial class SubModule_Logistics_Order_OrderGiftApplyAmount : System.Web.UI.Page
{
    protected bool bEdit = false;
    protected bool bAdujst = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            BindGrid();
            ViewState["bEdit"] = false;
            ViewState["bAdujst"] = true;
            bEdit = (bool)ViewState["bEdit"];
            bAdujst = (bool)ViewState["bAdujst"];
        }
        #region 注册弹出窗口脚本
        string script = "function PopAdjust(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_AdjustGiftApplyAmount.aspx") +
            "?ID=' + id + '&tempid='+tempid, window, 'dialogWidth:800px;DialogHeight=600px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopAdjust", script, true);
        string script2 = "function PopAdjustHistory(id){\r\n";
        script2 += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_OrderApplyDetailAdjustHistory.aspx") +
            "?ID=' + id + '&tempid='+tempid, window, 'dialogWidth:700px;DialogHeight=500px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopAdjustHistory", script2, true);
        #endregion
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        #endregion

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();



        IList<PDT_Brand> _brandList = PDT_BrandBLL.GetModelList("(IsOpponent IN (1) OR ID IN (4,5))");
        ddl_Brand.DataTextField = "Name";
        ddl_Brand.DataValueField = "ID";
        ddl_Brand.DataSource = _brandList;
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("全部", "0"));

        ddl_GiftClassify.DataSource = DictionaryBLL.GetDicCollections("ORD_GiftClassify");
        ddl_GiftClassify.DataBind();
        ddl_GiftClassify.Items.Insert(0, new ListItem("全部", "0"));

    }
    #endregion
    private void BindGrid()
    {
        string condition = "AccountMonth=" + ddl_Month.SelectedValue;
        if (tr_OrganizeCity.SelectValue != "1")
        {
            //管理片区及所有下属管理片区
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND CM_Client.OrganizeCity IN (" + orgcitys + ")";
        }
        if (select_Client.SelectValue != "")
        {
            condition += " AND Client=" + select_Client.SelectValue;
        }
        if (ddl_Brand.SelectedValue != "0")
        {
            condition += " AND Brand=" + ddl_Brand.SelectedValue;
        }
        if (ddl_GiftClassify.SelectedValue != "0")
        {
            condition += " AND ORD_GiftApplyAmount.Classify=" + ddl_GiftClassify.SelectedValue;
        }
        gv_List.ConditionString = condition + " ORDER BY CM_Client.FullName,ORD_GiftApplyAmount.Brand,ORD_GiftApplyAmount.Classify";
        gv_List.BindGrid();
        if (ViewState["PageIndex"] != null)
        {
            gv_List.PageIndex = (int)ViewState["PageIndex"];
        }
    }
    
    protected void bt_Adjust_Click(object sender, EventArgs e)
    {
        if (Session["SuccessFlag"] != null && (bool)Session["SuccessFlag"])
        {
            BindGrid();
        }
        ViewState["PageIndex"] = gv_List.PageIndex;
    }



    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            Button bt_Adjust = e.Row.FindControl("bt_Adjust") != null ? (Button)e.Row.FindControl("bt_Adjust") : null;
            Button bt_AdjustHistory = e.Row.FindControl("bt_AdjustHistory") != null ? (Button)e.Row.FindControl("bt_AdjustHistory") : null;
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ORD_GiftApplyAmount_ID"];
            if (bt_Adjust != null && id > 0)
            {
                bt_Adjust.OnClientClick = "PopAdjust(" + id.ToString() + ")";
            }
            if (bt_AdjustHistory != null && id > 0)
                bt_AdjustHistory.OnClientClick = "PopAdjustHistory(" + id.ToString() + ")";
        }
    }

    protected void bt_ImportTools_Click(object sender, EventArgs e)
    {      
        string url = ConfigHelper.GetConfigString("GiftAmount_ImportToolsURL");
        if (!string.IsNullOrEmpty(url)) Response.Redirect(url);
    }
    protected void bt_downtemple_Click(object sender, EventArgs e)
    {
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        string filename = ConfigHelper.GetConfigString("GiftFileName");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += filename;
        try
        {
            Response.Clear();
            Response.BufferOutput = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.WriteFile(path);
            Response.Flush();
            Response.End();
        }
        catch (System.Exception err)
        {
            MessageBox.Show(this, "系统错误-3!" + err.Message);
        }
    }
}
