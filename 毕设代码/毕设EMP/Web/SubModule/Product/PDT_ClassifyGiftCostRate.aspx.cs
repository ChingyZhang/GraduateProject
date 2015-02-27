using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using System.Data;

public partial class SubModule_Product_PDT_ClassifyGiftCostRate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["Client"] = Request.QueryString["Client"] == null ? 0 : int.Parse(Request.QueryString["Client"]);
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            

            BindDropDown();

            if ((int)ViewState["Client"] != 0)
            {
                select_Client.SelectValue = ViewState["Client"].ToString();
                select_Client.SelectText = new CM_ClientBLL((int)ViewState["Client"]).Model.FullName;
            }
            else if ((int)ViewState["OrganizeCity"] != 0)
            {
                tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
            }
           
            if (select_Client.SelectValue == "" || select_Client.SelectValue == "0")
            {
                int level = int.Parse(ddl_Level.SelectedValue);
                string citys = "";
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                DataTable dt = TreeTableBLL.GetAllChildNodeByNodes("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", tr_OrganizeCity.SelectValue);
                if (level > 0)
                {
                    foreach (DataRow row in dt.Select("Level=" + level))
                    {
                        citys += row["ID"] + ",";
                    }
                    citys = citys.Substring(0, citys.Length - 1);
                }
                else citys = tr_OrganizeCity.SelectValue;
                ListTable<PDT_ClassifyGiftCostRate> _details = new ListTable<PDT_ClassifyGiftCostRate>
                       (PDT_ClassifyGiftCostRateBLL.GetModelList("OrganizeCity in (" + citys + ") "), "ID");
                ViewState["Details"] = _details;
            }
            else
            {
                ListTable<PDT_ClassifyGiftCostRate> _details = new ListTable<PDT_ClassifyGiftCostRate>
                       (PDT_ClassifyGiftCostRateBLL.GetModelList("Client=" + select_Client.SelectValue ), "ID");
                ViewState["Details"] = _details;
            }
            ViewState["MAXID"] = ((ListTable<PDT_ClassifyGiftCostRate>)ViewState["Details"]).GetListItem().Count > 0 ? ((ListTable<PDT_ClassifyGiftCostRate>)ViewState["Details"]).GetListItem().Max(p => p.ID) : 0;
            BindGrid();
        }
    }

    private void BindDropDown()
    {
        #region 绑定用户可管辖的片区
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
        //if (tr_OrganizeCity.SelectValue != "1")
        //{
        //    select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
        //}
        //else
        //{
        //    select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2";
        //}
        Addr_OrganizeCityBLL city = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
        ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel").Where(p => int.Parse(p.Key) > city.Model.Level && int.Parse(p.Key) <= 4).ToList().OrderBy(p => p.Key);
        ddl_Level.DataBind();
        ddl_Level.Items.Insert(0, new ListItem("本级", "0"));

        ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("(IsOpponent IN (1) OR ID IN (4,5))");
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("请选择", "0"));

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("Year>="+(DateTime.Now.Year-2).ToString());
        ddl_BeginMonth.DataBind();
       

        ddl_GiftCostClassify.DataSource = DictionaryBLL.GetDicCollections("ORD_GiftClassify");
        ddl_GiftCostClassify.DataBind();
        ddl_GiftCostClassify.Items.Insert(0, new ListItem("请选择", "0"));
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        Response.Redirect("PDT_ClassifyGiftCostRate.aspx?OrganizeCity=" + e.CurSelectIndex.ToString());   
    }
    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        Response.Redirect("PDT_ClassifyGiftCostRate.aspx?Client=" + e.SelectValue.ToString());
    }

 
    private void BindGrid()
    {
        ListTable<PDT_ClassifyGiftCostRate> _details = (ListTable<PDT_ClassifyGiftCostRate>)ViewState["Details"];
        gv_List.BindGrid<PDT_ClassifyGiftCostRate>(_details.GetListItem());
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue == "" || select_Client.SelectValue == "0")
        {
            int level = int.Parse(ddl_Level.SelectedValue);
            string citys = "";
            DataTable dt = TreeTableBLL.GetAllChildNodeByNodes("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", tr_OrganizeCity.SelectValue);
            if (level > 0)
            {
                foreach (DataRow row in dt.Select("Level=" + level))
                {
                    citys += row["ID"] + ",";
                }
                citys = citys.Substring(0, citys.Length - 1);
            }
            else 
                citys = tr_OrganizeCity.SelectValue;
            ListTable<PDT_ClassifyGiftCostRate> _details = new ListTable<PDT_ClassifyGiftCostRate>
                   (PDT_ClassifyGiftCostRateBLL.GetModelList("OrganizeCity in (" + citys + ") "), "ID");
            ViewState["Details"] = _details;
        }
        else
        {
            ListTable<PDT_ClassifyGiftCostRate> _details = new ListTable<PDT_ClassifyGiftCostRate>
                   (PDT_ClassifyGiftCostRateBLL.GetModelList("Client=" + select_Client.SelectValue), "ID");
            ViewState["Details"] = _details;
        }
        BindGrid();  
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        ListTable<PDT_ClassifyGiftCostRate> _details = (ListTable<PDT_ClassifyGiftCostRate>)ViewState["Details"];
     
        PDT_ClassifyGiftCostRateBLL _bll = new PDT_ClassifyGiftCostRateBLL();

       
        foreach (PDT_ClassifyGiftCostRate m in _details.GetListItem(ItemState.Added))
        {
            _bll.Model = m;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.ApproveFlag = 1;                      
            _bll.Add();          
        }

        foreach (PDT_ClassifyGiftCostRate m in _details.GetListItem(ItemState.Modified))
        {
            _bll.Model = m;
            _bll.Model.UpdateStaff = (int)Session["UserID"];
           _bll.Update();
        }

        foreach (PDT_ClassifyGiftCostRate m in _details.GetListItem(ItemState.Deleted))
        {
            _bll.Model = m;
            _bll.Delete(m.ID);
        }
        if ((int)ViewState["Client"] != 0)
            Response.Redirect("PDT_ClassifyGiftCostRate.aspx?Client=" + select_Client.SelectValue);
        else 
            Response.Redirect("PDT_ClassifyGiftCostRate.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue);
    }


    #region 添加一条明细
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        #region 验证必填项
        decimal costPrice=0;
        if (ddl_Brand.SelectedValue == "0" || ddl_Brand.SelectedValue == "")
        {
            MessageBox.Show( this, "产品品牌必选！");
            return;
        }
        else if(ddl_GiftCostClassify.SelectedValue=="0" || ddl_GiftCostClassify.SelectedValue=="")
        {
            MessageBox.Show(this, "赠品费用类别必选！");
            return;
        }
        else if (!decimal.TryParse(tbx_CostPrice.Text, out costPrice))
        {
            MessageBox.Show(this, "赠品费率必须是小数或整数!");
            return;
        }
        #endregion

        ListTable<PDT_ClassifyGiftCostRate> _details = (ListTable<PDT_ClassifyGiftCostRate>)ViewState["Details"];

        PDT_ClassifyGiftCostRate item;
        #region 产品存在与否判断
        if (ViewState["Selected"] == null)
        {
            item = new PDT_ClassifyGiftCostRate();
            if (select_Client.SelectValue == "" || select_Client.SelectValue == "0")
                item.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
            else
            {
                item.Client = int.Parse(select_Client.SelectValue.ToString());
                item.OrganizeCity = new CM_ClientBLL(item.Client).Model.OrganizeCity;
            }
            item["InsertStaff"] = Session["UserID"].ToString();
            ViewState["MAXID"] = ((int)ViewState["MAXID"]) + 1;
            item.ID = (int)ViewState["MAXID"];           
        }
        else
        {
            //修改科目
            if (!_details.Contains(ViewState["Selected"].ToString()))
            {
                MessageBox.Show(this,"要修改的产品不存在！");
                return;
            }
            item = _details[ViewState["Selected"].ToString()];
            item["UpdateStaff"] = Session["UserID"].ToString();
            gv_List.SelectedIndex = -1;
        }
        #endregion

        item["PDTBrand"] = ddl_Brand.SelectedValue;
        
        item["GiftCostClassify"] = ddl_GiftCostClassify.SelectedValue;
        item["Enabled"] = ddl_Enabled.SelectedValue;
      
        item["GiftCostRate"] = tbx_CostPrice.Text;
        item["UpdateStaff"] = Session["UserID"].ToString();
        item.BeginMonth = int.Parse(ddl_BeginMonth.SelectedValue);
        int cycle = 0;
        if (int.TryParse(txt_Cycle.Text.Trim(),out cycle))
        {
            item.Cycle = cycle;
        }
        item.Remark = tbx_Remark.Text.Trim();
       
        if (ViewState["Selected"] == null)
        {
            foreach (PDT_ClassifyGiftCostRate p in _details.GetListItem())
            {
                if (p.PDTBrand == item.PDTBrand && p.GiftCostClassify == item.GiftCostClassify)
                {                   
                        MessageBox.Show(this, "已存在该分类的数据！");
                        return;                   
                }
            }

            _details.Add(item);
          
        }
        else
        {
           
            _details.Update(item);          //更新产品
        }
        
      
        BindGrid();
        ddl_Brand.SelectedValue = "0";
        ddl_GiftCostClassify.SelectedValue = "0";       
        ddl_Brand.Enabled = true;      
        ddl_GiftCostClassify.Enabled = true;
        tbx_CostPrice.Text = "";
        tbx_Remark.Text = "";
        ViewState["Selected"] = null;
        bt_Add.Text = "增加";
    }
    #endregion

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    { 
        int _id = int.Parse(this.gv_List.DataKeys[e.NewSelectedIndex]["ID"].ToString());
        ListTable<PDT_ClassifyGiftCostRate> _details = (ListTable<PDT_ClassifyGiftCostRate>)ViewState["Details"];        
        ddl_Brand.SelectedValue = _details[_id.ToString()].PDTBrand.ToString() ;

        if (_details[_id.ToString()].BeginMonth > 0) ddl_BeginMonth.SelectedValue = _details[_id.ToString()].BeginMonth.ToString();        
        ddl_GiftCostClassify.SelectedValue = _details[_id.ToString()].GiftCostClassify.ToString();
        ddl_Brand.Enabled = false;
       
        ddl_GiftCostClassify.Enabled = false;
        tbx_CostPrice.Text = _details[_id.ToString()].GiftCostRate.ToString("0.###");
        tbx_Remark.Text = _details[_id.ToString()].Remark.ToString();
        ddl_Enabled.SelectedValue = _details[_id.ToString()].Enabled.ToString();
        if (_details[_id.ToString()].Client > 0)
        {
            select_Client.SelectValue = _details[_id.ToString()].Client.ToString();
            select_Client.SelectText = new CM_ClientBLL(_details[_id.ToString()].Client).Model.FullName;
        }
        ViewState["Selected"] = _id.ToString();
        bt_Add.Text = "修 改";
    }

    #region 删除一条产品明细
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ListTable<PDT_ClassifyGiftCostRate> _details = (ListTable<PDT_ClassifyGiftCostRate>)ViewState["Details"];
        int _id = int.Parse(this.gv_List.DataKeys[e.RowIndex]["ID"].ToString());      
        _details.Remove(_id.ToString());
        BindGrid();
    }
    #endregion

}
