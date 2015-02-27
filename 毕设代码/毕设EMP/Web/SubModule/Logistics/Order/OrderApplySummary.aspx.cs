using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.Model;
using System.IO;
using System.Text;
using System.Data;
using MCSFramework.BLL.Logistics;
public partial class SubModule_Logistics_Order_OrderApplySummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            BindDropDown();            
        }
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
        tr_OrganizeCity_Selected(null, null);
        #endregion

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

     
        ddl_State.DataSource = DictionaryBLL.GetDicCollections("ORD_OrderApplyState");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("全部", "0"));

        IList<PDT_Brand> _brandList = PDT_BrandBLL.GetModelList("IsOpponent=1");
        ddl_Brand.DataTextField = "Name";
        ddl_Brand.DataValueField = "ID";
        ddl_Brand.DataSource = _brandList;
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.Items.Insert(0, new ListItem("所有", "0"));

        ddl_ProductType.DataSource = DictionaryBLL.GetDicCollections("ORD_ProductOrderType");
        ddl_ProductType.DataBind();
        ddl_ProductType.Items.Insert(0, new ListItem("全部", "0"));
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCity city = new Addr_OrganizeCityBLL((int.Parse(tr_OrganizeCity.SelectValue))).Model;
            ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel").Where(p => int.Parse(p.Key) > city.Level).ToList().OrderBy(p => p.Key);
            ddl_Level.DataBind();
            if (ddl_Level.Items.Count == 0)
            {
                ddl_Level.Items.Add(new ListItem("本级", city.Level.ToString()));
            }

            ddl_Level.Items.Add(new ListItem("经销商", "10"));
            ddl_Level.Items.Add(new ListItem("经销商汇总", "30"));
            ddl_Level.Items.Add(new ListItem("品牌汇总", "20"));
            
        }
    }
    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region 绑定品系
        ddl_Classify.DataSource = PDT_ClassifyBLL.GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("所有", "0"));
        #endregion
    }
    #endregion
    protected string GetPackagingName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product).Model;
        if (p == null) return "";
        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();
    }

    private void BindGrid()
    {
        gv_List.Columns[2].Visible = true;
        gv_List.Columns[3].Visible = true;
        gv_List.Columns[4].Visible = true;
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int productType = int.Parse(ddl_ProductType.SelectedValue);
        int brand=int.Parse(ddl_Brand.SelectedValue);
        int classify=int.Parse(ddl_Classify.SelectedValue);
        int product=select_Product.SelectValue==""?0:int.Parse(select_Product.SelectValue);
        int state = int.Parse(ddl_State.SelectedValue);
        if (level > 10)
        {
            gv_List.Columns[3].Visible = false;
            gv_List.Columns[4].Visible = false;
        }
        if (level == 30)
        {
            gv_List.Columns[2].Visible = false;
        }
        if (MCSTabControl1.SelectedIndex == 0)
        {
            #region 显示汇总单数据源

            DataTable dtSummary = ORD_OrderApplyBLL.GetSummaryTotal(month, organizecity, level,productType, brand, classify, product, state);
            if (dtSummary.Rows.Count == 0)
            {
                gv_List.DataBind();
                return;
            }

            DataRow dtrow = dtSummary.NewRow();
            dtrow["Level"] = "合计";         
            dtrow["ProductID"] = 0;
            dtrow["Weight"] = dtSummary.Compute("Sum(Weight)", "true");
            dtrow["Price"] = dtSummary.Compute("Sum(Price)", "true");
            dtrow["Quantity"] = dtSummary.Compute("Sum(Quantity)", "true");
            dtSummary.Rows.Add(dtrow);
            gv_List.DataSource = dtSummary;
            gv_List.DataBind();

           
            #endregion

           
        }
        else
        {
            string condition = " ORD_OrderApply.Type =1 AND ORD_OrderApply.AccountMonth = " + ddl_Month.SelectedValue;
            #region 组织查询条件
           
                if (tr_OrganizeCity.SelectValue != "1")
                {
                    //管理片区及所有下属管理片区
                    Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                    string orgcitys = orgcity.GetAllChildNodeIDs();
                    if (orgcitys != "") orgcitys += ",";
                    orgcitys += tr_OrganizeCity.SelectValue;

                    condition += " AND ORD_OrderApply.OrganizeCity IN (" + orgcitys + ")";
                }     
      
               if (ddl_ProductType.SelectedValue != "0")
                {
                    condition += " AND MCS_SYS.dbo.UF_Spilt(ORD_OrderApply.ExtPropertys,'|',4)=" + ddl_ProductType.SelectedValue;
                }

                if (ddl_Brand.SelectedValue != "0")
                {
                    condition += " AND MCS_SYS.dbo.UF_Spilt(ORD_OrderApply.ExtPropertys,'|',5)=" + ddl_Brand.SelectedValue;
                }
              
                //审批状态
                if (ddl_State.SelectedValue != "0")
                {
                    condition += " AND ORD_OrderApply.State = " + ddl_State.SelectedValue;
                }
            
            #endregion
            gv_ListDetail.ConditionString = condition;
            gv_ListDetail.BindGrid();
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.Visible = MCSTabControl1.SelectedIndex == 0;
        gv_ListDetail.Visible = !gv_List.Visible;
        BindGrid();
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        BindGrid();

        string filename = HttpUtility.UrlEncode("费用申请汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "");

        Response.Write(outhtml.ToString());
        Response.End();

        gv_List.AllowPaging = true;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
