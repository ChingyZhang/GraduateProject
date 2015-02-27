using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL;
using MCSControls.MCSTabControl;
using MCSControls.MCSWebControls;

public partial class SubModule_Logistics_Publish_PublishDetail : System.Web.UI.Page
{
    protected bool CanEnabled = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["Type"] = Request.QueryString["Type"] == null ? 1 : int.Parse(Request.QueryString["Type"]); //类型，1.发布成品目录 2：发布促销品目录

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                BindData();
            }
            else
            {
                #region 初始化界面字段值
                AC_AccountMonth m = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddMonths(1))).Model;
                ((DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_AccountMonth")).SelectedValue = m.ID.ToString();
                ((TextBox)pl_ApplyPublish.FindControl("ORD_ApplyPublish_BeginTime")).Text = m.BeginDate.ToString("yyyy-MM-dd");
                ((TextBox)pl_ApplyPublish.FindControl("ORD_ApplyPublish_EndTime")).Text = m.EndDate.ToString("yyyy-MM-dd");
                ((DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_Type")).SelectedValue = ViewState["Type"].ToString();
                ((MCSTreeControl)pl_ApplyPublish.FindControl("ORD_ApplyPublish_ToOrganizeCity")).SelectValue = new Org_StaffBLL((int)Session["UserID"]).Model.OrganizeCity.ToString();

                if ((int)ViewState["Type"] == 1)
                {
                    DropDownList ddl_FeeType = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_FeeType");
                    if (ddl_FeeType != null) ddl_FeeType.Enabled = false;

                    DropDownList ddl_ProductBrand = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_ProductBrand");
                    if (ddl_ProductBrand != null) ddl_ProductBrand.Enabled = false;

                    DropDownList ddl_GiftClassify = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_GiftClassify");
                    if (ddl_GiftClassify != null) ddl_GiftClassify.Enabled = false;

                    gv_List.Columns[gv_List.Columns.Count - 3].Visible = false;     //赠送坎级不显示
                }
                #endregion

                gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
                bt_Cancel.Visible = false;
                bt_Close.Visible = false;
                btn_Delete.Visible = false;
                bt_Modify.Visible = false;
                bt_Publish.Visible = false;
                bt_ViewApplyList.Visible = false;
            }



            Header.Attributes["WebPageSubCode"] = "Type=" + ViewState["Type"].ToString();
            #region 创建空列表
            ListTable<ORD_ApplyPublishDetail> _details = new ListTable<ORD_ApplyPublishDetail>(new ORD_ApplyPublishBLL((int)ViewState["ID"]).Items, "Product");
            ViewState["Details"] = _details;
            BindGrid();
            #endregion
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if ((int)ViewState["ID"] != 0)
        {
            ORD_ApplyPublish m = new ORD_ApplyPublishBLL((int)ViewState["ID"]).Model;
            ViewState["Type"] = m.Type;
        }

        if ((int)ViewState["Type"] == 1)
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent IN (1,9)");
        else
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent='9'");

        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand.SelectedValue = "0";
        rbl_Brand_SelectedIndexChanged(null, null);

        DropDownList ddl_ProductBrand = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_ProductBrand");
        if (ddl_ProductBrand != null)
        {
            ddl_ProductBrand.DataTextField = "Name";
            ddl_ProductBrand.DataValueField = "ID";
            ddl_ProductBrand.DataSource = PDT_BrandBLL.GetModelList("(IsOpponent IN (1) OR ID IN (4,5,99))");
            ddl_ProductBrand.DataBind();
            ddl_ProductBrand.Items.Insert(0, new ListItem("请选择", "0"));
            ddl_ProductBrand.SelectedValue = "0";
        }
    }

    protected void rbl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Classify.DataSource = PDT_ClassifyBLL.GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.SelectedValue = "0";
    }
    #endregion

    private void BindData()
    {
        ORD_ApplyPublish m = new ORD_ApplyPublishBLL((int)ViewState["ID"]).Model;
        ViewState["Type"] = m.Type;
        pl_ApplyPublish.BindData(m);

        if ((int)ViewState["Type"] == 1)
        {
            gv_List.Columns[5].Visible = false;     //成品请购单，单价依据各客户价表结算
            gv_List.Columns[6].Visible = false;     //成品请购单，单价依据各客户价表结算
            gv_List.Columns[gv_List.Columns.Count - 3].Visible = false;     //赠送坎级不显示

            DropDownList ddl_FeeType = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_FeeType");
            if (ddl_FeeType != null) ddl_FeeType.Enabled = false;

            DropDownList ddl_ProductBrand = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_ProductBrand");
            if (ddl_ProductBrand != null) ddl_ProductBrand.Enabled = false;

            DropDownList ddl_GiftClassify = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_GiftClassify");
            if (ddl_GiftClassify != null) ddl_GiftClassify.Enabled = false;
        }

        if (m.State != 1)
        {
            pl_ApplyPublish.SetControlsEnable(false);
            btn_Save.Visible = false;
            btn_Delete.Visible = false;
            tbl_publish.Visible = false;
            CanEnabled = false;

        }
        switch (m.State)
        {
            case 1:     //未发布
                bt_Close.Visible = false;
                bt_Cancel.Visible = false;
                bt_Modify.Visible = false;
                bt_ViewApplyList.Visible = false;
                gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
                break;
            case 2:     //已发布
                bt_Publish.Visible = false;
                break;
            case 3:     //关闭
            case 4:     //撤销
                bt_Publish.Visible = false;
                bt_Close.Visible = false;
                bt_Cancel.Visible = false;
                bt_Modify.Visible = false;
                break;
        }
    }

    private void BindGrid()
    {
        if (MCSTabControl1.SelectedIndex == 0)
        {
            ListTable<ORD_ApplyPublishDetail> _details = ViewState["Details"] as ListTable<ORD_ApplyPublishDetail>;

            gv_List.BindGrid<ORD_ApplyPublishDetail>(_details.GetListItem().OrderByDescending(p => p.Price).ThenBy(p => p["GiveLevel"]).ToArray());
        }
        else
        {
            //获取非价表产品列表
            string condition = "ID NOT IN (SELECT Product FROM MCS_Logistics.dbo.ORD_ApplyPublishDetail WHERE PublishID=" + ViewState["ID"].ToString() + ")";

            if (ddl_Brand.SelectedValue != "0")
            {
                if (ddl_Classify.SelectedValue == "0")
                    condition += " AND Brand =" + ddl_Brand.SelectedValue;
                else
                    condition += " AND Classify =" + ddl_Classify.SelectedValue;
            }
            condition += " AND State = 1 AND ApproveFlag = 1 AND Brand IN (SELECT ID FROM MCS_PUB.dbo.PDT_Brand WHERE IsOpponent=9)";

            if (tbx_ProductText.Text != "")
                condition += " AND (FullName like '%" + tbx_ProductText.Text + "%' OR ShortName like '%" + tbx_ProductText.Text + "%' OR Code like '%" + tbx_ProductText.Text + "%')";
            IList<PDT_Product> products = PDT_ProductBLL.GetModelList(condition);            
            gv_NotInList.BindGrid<PDT_Product>(products);
        }

        cb_SelectAll.Checked = false;
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSTabClickedEventArgs e)
    {
        if (MCSTabControl1.SelectedIndex == 0)
        {
            bt_In.Visible = false;
            bt_Out.Visible = true;
            btn_Save.Visible = true;

            tr_NotInList.Visible = false;
            tr_List.Visible = true;
            tbl_Find.Visible = false;
        }
        else
        {
            bt_In.Visible = true;
            bt_Out.Visible = false;
            btn_Save.Visible = false;
            btn_Delete.Visible = false;
            bt_Publish.Visible = false;

            tr_NotInList.Visible = true;
            tr_List.Visible = false;
            tbl_Find.Visible = true;
        }
        gv_List.PageIndex = 0;
        gv_NotInList.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_NotInList.PageIndex = 0;
        BindGrid();
    }

    protected void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (tr_List.Visible)
        {
            foreach (GridViewRow row in gv_List.Rows)
            {
                CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
                cb_check.Checked = cb_SelectAll.Checked;
            }
        }
        else
        {
            foreach (GridViewRow row in gv_NotInList.Rows)
            {
                CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
                cb_check.Checked = cb_SelectAll.Checked;
            }
        }
    }

    #region 移入经营产品
    protected void bt_In_Click(object sender, EventArgs e)
    {
        Save();
        foreach (GridViewRow row in gv_NotInList.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                ORD_ApplyPublishBLL _bll;
                if ((int)ViewState["ID"] != 0)
                    _bll = new ORD_ApplyPublishBLL((int)ViewState["ID"]);
                else
                    return;

                ORD_ApplyPublishDetail pd = new ORD_ApplyPublishDetail();
                pd.ID = (int)ViewState["ID"];
                pd.Product = int.Parse(gv_NotInList.DataKeys[row.RowIndex]["ID"].ToString());
                pd.Price = new PDT_ProductBLL(pd.Product).Model.FactoryPrice;
                _bll.AddDetail(pd);
            }
        }
        Response.Redirect("PublishDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Out_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                ORD_ApplyPublishBLL _bll;
                if ((int)ViewState["ID"] != 0)
                    _bll = new ORD_ApplyPublishBLL((int)ViewState["ID"]);
                else
                    return;
                _bll.DeleteDetail(int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString()));
            }
        }
        Response.Redirect("PublishDetail.aspx?ID=" + ViewState["ID"].ToString());
    }
    #endregion

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (Save()) Response.Redirect("PublishDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    private bool Save()
    {
        ORD_ApplyPublishBLL _bll;
        if ((int)ViewState["ID"] != 0)
            _bll = new ORD_ApplyPublishBLL((int)ViewState["ID"]);
        else
            _bll = new ORD_ApplyPublishBLL();

        pl_ApplyPublish.GetData(_bll.Model);

        #region 判断有效性
        if (_bll.Model.Type == 2)
        {
            if (_bll.Model["ProductBrand"] == "0")
            {
                MessageBox.Show(this, "请选择【品牌】！");
                return false;
            }

            if (_bll.Model["GiftClassify"] == "0")
            {
                MessageBox.Show(this, "请选择【赠品费用类别】！");
                return false;
            }
        }
        #endregion

        #region 组织主题
        if (_bll.Model.Topic == "")
        {
            DropDownList ddl_Type = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_Type");
            if (ddl_Type != null) _bll.Model.Topic += ddl_Type.SelectedItem.Text + " ";

            DropDownList ddl_AccountMonth = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_AccountMonth");
            if (ddl_AccountMonth != null) _bll.Model.Topic += ddl_AccountMonth.SelectedItem.Text.Replace('-', '年') + "月 ";

            DropDownList ddl_ProductBrand = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_ProductBrand");
            if (ddl_ProductBrand != null) _bll.Model.Topic += ddl_ProductBrand.SelectedItem.Text + " ";

            DropDownList ddl_GiftClassify = (DropDownList)pl_ApplyPublish.FindControl("ORD_ApplyPublish_GiftClassify");
            if (ddl_GiftClassify != null) _bll.Model.Topic += ddl_GiftClassify.SelectedItem.Text + " ";
        }
        #endregion

        if ((int)ViewState["ID"] != 0)
        {
            if (_bll.Model.Type == 2 && _bll.Model.FeeType == 0)
            {
                //赠品所属费用类型
                _bll.Model.FeeType = ConfigHelper.GetConfigInt("GiftFeeType");
            }
            _bll.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            _bll.Update();
        }
        else
        {
            #region 判断相同品牌的申请当月是否已存在
            string condition = string.Format(@"AccountMonth={0} AND Type={1} AND 
                ToOrganizeCity={2} AND
                MCS_SYS.dbo.UF_Spilt2('MCS_Logistics.dbo.ORD_ApplyPublish',ExtPropertys,'ProductBrand')='{3}' AND
                MCS_SYS.dbo.UF_Spilt2('MCS_Logistics.dbo.ORD_ApplyPublish',ExtPropertys,'GiftClassify')='{4}' ",
                _bll.Model.AccountMonth, _bll.Model.Type, _bll.Model.ToOrganizeCity, _bll.Model["ProductBrand"], _bll.Model["GiftClassify"]);

            if (ORD_ApplyPublishBLL.GetModelList(condition).Count > 0)
            {
                MessageBox.Show(this, "对不起，相同品牌相同类别的申请目录已发布了，请勿重复发布!");
                return false;
            }
            #endregion

            _bll.Model.State = 1;
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Add();
            ViewState["ID"] = _bll.Model.ID;
        }

        #region 修改明细
        foreach (GridViewRow row in gv_List.Rows)
        {
            ORD_ApplyPublishDetail item = new ORD_ApplyPublishDetail();
            item.PublishID = _bll.Model.ID;
            item.ID = int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString());
            item.Product = int.Parse(gv_List.DataKeys[row.RowIndex]["Product"].ToString());

            PDT_Product p = new PDT_ProductBLL(item.Product).Model;
            item.Price = p.FactoryPrice;

            if (row.FindControl("tbx_AvailableQuantity_T") != null)
                item.AvailableQuantity = int.Parse(((TextBox)row.FindControl("tbx_AvailableQuantity_T")).Text) * p.ConvertFactor;

            if (row.FindControl("tbx_MinQuantity_T") != null)
                item.MinQuantity = int.Parse(((TextBox)row.FindControl("tbx_MinQuantity_T")).Text) * p.ConvertFactor;

            if (row.FindControl("tbx_MaxQuantity_T") != null)
                item.MaxQuantity = int.Parse(((TextBox)row.FindControl("tbx_MaxQuantity_T")).Text) * p.ConvertFactor;

            if (row.FindControl("tbx_Remark") != null)
                item.Remark = ((TextBox)row.FindControl("tbx_Remark")).Text;

            if (row.FindControl("tbx_GiveLevel") != null)
                item["GiveLevel"] = ((TextBox)row.FindControl("tbx_GiveLevel")).Text;

            if (item.MaxQuantity > 0 && item.MinQuantity > item.MaxQuantity)
            {
                MessageBox.Show(this, "产品：" + p.FullName + "的【单次最小请购数量】不能大于【单次最大请购数量】！");
                return false;
            }

            if (item.MaxQuantity > 0 && item.AvailableQuantity > 0 && item.MaxQuantity > item.AvailableQuantity)
            {
                MessageBox.Show(this, "产品：" + p.FullName + "的【单次最大请购数量】不能大于【可供请购数量】！");
                return false;
            }
            _bll.UpdateDetail(item);
        }

        #endregion

        return true;
    }

    protected void gv_NotInList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_NotInList.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            new ORD_ApplyPublishBLL((int)ViewState["ID"]).Delete();
            Response.Redirect("PublishList.aspx");
        }
    }

    #region 转换产品数量为界面需要的格式
    protected string GetProductInfo(int ProductID, string FieldName)
    {
        return new PDT_ProductBLL(ProductID, true).Model[FieldName];
    }

    protected string GetQuantityString(int product, int quantity)
    {
        if (quantity == 0) return "0";

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        string packing1 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
        string packing2 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();

        string ret = "";
        if (quantity / p.ConvertFactor != 0) ret += (quantity / p.ConvertFactor).ToString() + packing1 + " ";
        if (quantity % p.ConvertFactor != 0) ret += (quantity % p.ConvertFactor).ToString() + packing2 + " ";
        return ret;
    }

    protected int GetTrafficeQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return quantity / p.ConvertFactor;
    }

    protected int GetPackagingQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return quantity % p.ConvertFactor;
    }

    protected string GetTrafficeName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
    }

    protected string GetPackagingName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();
    }
    protected int GetSubmitQuantity(int product)
    {
        if ((int)ViewState["ID"] > 0)
            return ORD_OrderApplyBLL.GetSubmitQuantity((int)ViewState["ID"], product);
        else
            return 0;
    }
    #endregion

    protected void bt_Publish_Click(object sender, EventArgs e)
    {
        Save();
        if ((int)ViewState["ID"] > 0)
        {
            ORD_ApplyPublishBLL _bll = new ORD_ApplyPublishBLL((int)ViewState["ID"]);

            #region 赠品发布有效性判断
            if (_bll.Model.Type == 2)
            {
                if (_bll.Model.FeeType == 0)
                {
                    MessageBox.Show(this, "请指定正确的费用类型!");
                    return;
                }

                if (pl_ApplyPublish.FindControl("ORD_ApplyPublish_ProductBrand") != null && _bll.Model["ProductBrand"] != null)
                {
                    if (_bll.Model["ProductBrand"] == "" || _bll.Model["ProductBrand"] == "0")
                    {
                        MessageBox.Show(this, "请指定赠品请购的所属品牌!");
                        return;
                    }
                }

                if (pl_ApplyPublish.FindControl("ORD_ApplyPublish_GiftClassify") != null && _bll.Model["GiftClassify"] != null)
                {
                    if (_bll.Model["GiftClassify"] == "" || _bll.Model["GiftClassify"] == "0")
                    {
                        MessageBox.Show(this, "请指定赠品请购的赠品费用类别!");
                        return;
                    }
                }
            }
            #endregion


            _bll.Publish(2, (int)Session["UserID"]);

            Response.Redirect("PublishList.aspx");
        }
    }
    protected void bt_Modify_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            new ORD_ApplyPublishBLL((int)ViewState["ID"]).Publish(1, (int)Session["UserID"]);
            Response.Redirect("PublishList.aspx");
        }
    }
    protected void bt_Close_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            new ORD_ApplyPublishBLL((int)ViewState["ID"]).Publish(3, (int)Session["UserID"]);
            Response.Redirect("PublishList.aspx");
        }
    }
    protected void bt_Cancel_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            new ORD_ApplyPublishBLL((int)ViewState["ID"]).Publish(4, (int)Session["UserID"]);
            Response.Redirect("PublishList.aspx");
        }
    }

    protected void bt_ViewApplyList_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            Response.Redirect("../Order/OrderApplyList.aspx?Publish=" + ViewState["ID"].ToString());
        }
    }
    protected void bt_Copy_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            int id = new ORD_ApplyPublishBLL((int)ViewState["ID"]).Copy((int)Session["UserID"]);
            Response.Redirect("PublishDetail.aspx?ID=" + id.ToString());
        }
    }   
  
}
