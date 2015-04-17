// ===================================================================
// 文件路径:SubModule/PBM/Product/StandardPriceDetail.aspx.cs 
// 生成日期:2015-02-16 18:51:01 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.CM;
using MCSControls.MCSTabControl;
using MCSFramework.BLL;

public partial class SubModule_PBM_Product_StandardPriceDetail : System.Web.UI.Page
{
    private DropDownList ddl_FitSalesArea, ddl_FitRTChannel;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        ddl_FitSalesArea = (DropDownList)pl_detail.FindControl("PDT_StandardPrice_FitSalesArea");
        ddl_FitRTChannel = (DropDownList)pl_detail.FindControl("PDT_StandardPrice_FitRTChannel");

        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                PDT_StandardPrice s = new PDT_StandardPrice();
                s.IsDefault = "N";
                s.IsEnabled = "Y";
                pl_detail.BindData(s);

                UpdatePanel2.Visible = false;
                tr_List.Visible = false;
                tr_NotInList.Visible = false;
            }

        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if ((int)Session["OwnerType"] == 3)
        {
            if (ddl_FitSalesArea != null)
            {
                ddl_FitSalesArea.DataTextField = "Name";
                ddl_FitSalesArea.DataValueField = "ID";
                ddl_FitSalesArea.DataSource = CM_RTSalesArea_TDPBLL.GetRTSalesArea_ByTDP((int)Session["OwnerClient"]);
                ddl_FitSalesArea.DataBind();
                ddl_FitSalesArea.Items.Insert(0, new ListItem("不限区域", "0"));
            }

            if (ddl_FitRTChannel != null)
            {
                ddl_FitRTChannel.DataTextField = "Name";
                ddl_FitRTChannel.DataValueField = "ID";
                ddl_FitRTChannel.DataSource = CM_RTChannel_TDPBLL.GetRTChannel_ByTDP((int)Session["OwnerClient"]);
                ddl_FitRTChannel.DataBind();
                ddl_FitRTChannel.Items.Insert(0, new ListItem("不限渠道", "0"));
            }
        }

        tr_Category.DataSource = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"]);
        tr_Category.DataBind();
        tr_Category.SelectValue = "1";
    }
    #endregion

    private void BindData()
    {
        PDT_StandardPrice m = new PDT_StandardPriceBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);
            BindGrid();
            if (m.IsDefault == "Y")
            {
                //默认价表，不允许修改属性
                pl_detail.SetControlsEnable(false);
                gv_List.SetControlsEnable(false);
                bt_Out.Enabled = false;
                bt_In.Enabled = false;
            }
        }

    }

    private void BindGrid()
    {
        int category = 0;
        int.TryParse(tr_Category.SelectValue, out category);

        if (MCSTabControl1.SelectedIndex == 0)
        {

            string condition = " 1 = 1 ";

            #region 获取产品分类
            if (category > 1)
            {
                condition += " AND PDT_StandardPrice_Detail.Product IN (SELECT Product FROM MCS_PUB.dbo.PDT_ProductExtInfo WHERE Supplier=" + Session["OwnerClient"].ToString();
                string _categoryids = "";
                DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_Pub.dbo.PDT_Category", "ID", "SuperID", tr_Category.SelectValue);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _categoryids += dt.Rows[i]["ID"].ToString() + ",";
                }
                _categoryids += tr_Category.SelectValue;

                condition += " AND PDT_ProductExtInfo.Category IN (" + _categoryids + ")";

                condition += ")";
            }
            #endregion

            if (tbx_ProductText.Text != "")
            {
                condition += "AND PDT_StandardPrice_Detail.Product IN (SELECT ID FROM MCS_PUB.dbo.PDT_Product WHERE ";
                condition += " (FullName like '%" + tbx_ProductText.Text + "%' OR ShortName like '%" + tbx_ProductText.Text + "%' OR FactoryCode like '%" + tbx_ProductText.Text +
                    "%' OR PDT_Product.ID IN (SELECT Product FROM MCS_PUB.dbo.PDT_ProductExtInfo WHERE Supplier=" + Session["OwnerClient"].ToString() + " AND PDT_ProductExtInfo.Code LIKE '%" + tbx_ProductText.Text + "%'))";
                condition += ")";
            }

            IList<PDT_StandardPrice_Detail> list = new PDT_StandardPriceBLL((int)ViewState["ID"]).GetDetail(condition);
            gv_List.BindGrid<PDT_StandardPrice_Detail>(list);
        }
        else
        {
            //获取非价表产品列表
            string condition = "PDT_Product.ID NOT IN (SELECT Product FROM MCS_PUB.dbo.PDT_StandardPrice_Detail WHERE PriceID=" + ViewState["ID"].ToString() + ")";
            condition += " AND State = 1 AND ApproveFlag = 1";
            condition += " AND PDT_Product.ID IN (SELECT Product FROM MCS_PUB.dbo.PDT_ProductExtInfo WHERE Supplier=" + Session["OwnerClient"].ToString();

            #region 获取产品分类
            if (category > 1)
            {
                string _categoryids = "";
                DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_Pub.dbo.PDT_Category", "ID", "SuperID", tr_Category.SelectValue);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _categoryids += dt.Rows[i]["ID"].ToString() + ",";
                }
                _categoryids += tr_Category.SelectValue;

                condition += " AND PDT_ProductExtInfo.Category IN (" + _categoryids + ")";
            }
            #endregion

            condition += ")";

            if (tbx_ProductText.Text != "")
                condition += " AND (FullName like '%" + tbx_ProductText.Text + "%' OR ShortName like '%" + tbx_ProductText.Text + "%' OR Code like '%" + tbx_ProductText.Text +
                    "%' OR PDT_Product.ID IN (SELECT Product FROM MCS_PUB.dbo.PDT_ProductExtInfo WHERE Supplier=" + Session["OwnerClient"].ToString() + " AND PDT_ProductExtInfo.Code LIKE '%" + tbx_ProductText.Text + "%'))";

            IList<PDT_Product> products = PDT_ProductBLL.GetModelList(condition);

            gv_NotInList.BindGrid<PDT_Product>(products);
        }

        cb_SelectAll.Checked = false;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            Response.Redirect("StandardPriceDetail.aspx?ID=" + ViewState["ID"].ToString());
        }
    }

    private bool Save()
    {
        PDT_StandardPriceBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new PDT_StandardPriceBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new PDT_StandardPriceBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项

        #endregion

        #region 判断是否与默认价表重复
        if (_bll.Model.IsDefault.ToUpper() == "Y")
        {
            int _priceid = PDT_StandardPriceBLL.GetDefaultPrice((int)Session["OwnerClient"]);
            if (_priceid != _bll.Model.ID)
            {
                MessageBox.Show(this, "对不起，不能重复新增默认价表，新增的价表必须限制区域或渠道!");
                return false;
            }
        }

        #endregion

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
        }
        else
        {
            //新增
            _bll.Model.Supplier = (int)Session["OwnerClient"];
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ID"] = _bll.Add();
        }

        #region 保存修改明细
        foreach (GridViewRow row in gv_List.Rows)
        {
            PDT_StandardPrice_Detail item = new PDT_StandardPrice_Detail();
            item.PriceID = _bll.Model.ID;
            item.ID = int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString());
            item.Product = int.Parse(gv_List.DataKeys[row.RowIndex]["Product"].ToString());

            PDT_Product p = new PDT_ProductBLL(item.Product).Model;

            if (row.FindControl("tbx_Price") != null)
            {
                decimal d = 0;
                if (decimal.TryParse(((TextBox)row.FindControl("tbx_Price")).Text, out d)) item.Price = d;

                PDT_ProductBLL productbll = new PDT_ProductBLL(item.Product);
                item.Price = item.Price / productbll.Model.ConvertFactor;

                PDT_ProductExtInfo extinfo = productbll.GetProductExtInfo((int)Session["OwnerClient"]);
                if (extinfo != null)
                {
                    if (extinfo.MinSalesPrice > 0 && item.Price < extinfo.MinSalesPrice)
                    {
                        MessageBox.Show(this, "对不起，您发布的价格不能小于建议零售价!");
                        return false;
                    }
                    else if (extinfo.MaxSalesPrice > 0 && item.Price > extinfo.MaxSalesPrice)
                    {
                        MessageBox.Show(this, "对不起，您发布的价格不能过高于建议零售价!");
                        return false;
                    }
                }
            }


            if (row.FindControl("tbx_Remark") != null)
                item.Remark = ((TextBox)row.FindControl("tbx_Remark")).Text;

            _bll.UpdateDetail(item);
        }

        #endregion

        return true;
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSTabClickedEventArgs e)
    {
        if (MCSTabControl1.SelectedIndex == 0)
        {
            bt_In.Visible = false;
            bt_Out.Visible = true;
            bt_OK.Visible = true;

            tr_NotInList.Visible = false;
            tr_List.Visible = true;
        }
        else
        {
            bt_In.Visible = true;
            bt_Out.Visible = false;
            bt_OK.Visible = false;

            tr_NotInList.Visible = true;
            tr_List.Visible = false;
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
                PDT_StandardPriceBLL _bll;
                if ((int)ViewState["ID"] != 0)
                    _bll = new PDT_StandardPriceBLL((int)ViewState["ID"]);
                else
                    return;

                PDT_StandardPrice_Detail pd = new PDT_StandardPrice_Detail();
                pd.Product = int.Parse(gv_NotInList.DataKeys[row.RowIndex]["ID"].ToString());

                PDT_ProductBLL productbll = new PDT_ProductBLL(pd.Product);
                PDT_ProductExtInfo extinfo = productbll.GetProductExtInfo((int)Session["OwnerClient"]);
                if (extinfo != null)
                    pd.Price = extinfo.SalesPrice;
                else
                    pd.Price = productbll.Model.StdPrice;
                _bll.AddDetail(pd);
            }
        }
        Response.Redirect("StandardPriceDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Out_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                PDT_StandardPriceBLL _bll;
                if ((int)ViewState["ID"] != 0)
                    _bll = new PDT_StandardPriceBLL((int)ViewState["ID"]);
                else
                    return;
                _bll.DeleteDetail(int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString()));
            }
        }
        Response.Redirect("StandardPriceDetail.aspx?ID=" + ViewState["ID"].ToString());
    }
    #endregion

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
            new PDT_StandardPriceBLL((int)ViewState["ID"]).Delete();
            Response.Redirect("PublishList.aspx?ClientType=" + ViewState["ClientType"].ToString());
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
        string a = "个";
        PDT_Product p = new PDT_ProductBLL(product, true).Model;
        if (p.Packaging < 1)
            return a; //零售包装为空时，默认“个”
        else
            return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();

    }
    protected int GetSubmitQuantity(int product)
    {
        //if ((int)ViewState["ID"] > 0)
        //    return ORD_OrderBLL.GetSubmitQuantity((int)ViewState["ID"], product);
        //else
        return 0;
    }
    #endregion

}