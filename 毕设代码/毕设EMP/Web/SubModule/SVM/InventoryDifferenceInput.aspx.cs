using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.SVM;
using MCSFramework.BLL;
using MCSControls.MCSWebControls;
using System.Collections.Generic;
using MCSFramework.Model.Pub;
using System.Linq;
using MCSFramework.Model;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.OPI;

public partial class SubModule_SVM_InventoryDifferencesDifferenceInput : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["InventoryID"] = Request.QueryString["InventoryID"] == null ? 0 : int.Parse(Request.QueryString["InventoryID"]);
            ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);
            ViewState["IsCXP"] = Request.QueryString["IsCXP"] == null ? false : int.Parse(Request.QueryString["IsCXP"]) != 0;    //是否是赠品销量录入 0:成品 1:赠品
            #endregion
            ViewState["EditEnable"] = true;          
            if ((int)ViewState["InventoryID"] != 0)
            {
                if (Request.QueryString["Flag"] == null) cb_OnlyDisplayUnZero.Checked = true;
                BindData();
                btn_Inventory.Visible = false;
                TextBox tbx_InventoryDate = UC_DetailView1.FindControl("SVM_InventoryDifferences_InventoryDate") != null ? (TextBox)UC_DetailView1.FindControl("SVM_InventoryDifferences_InventoryDate") : null;

            }
            else
            {
                if ((int)ViewState["ClientID"] == 0) Response.Redirect("InventoryDifferencesList.aspx");

                #region 新增库存盘点盈亏
                CM_Client c = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_InventoryDifferences_Client");
                if (select_Client != null)
                {
                    select_Client.SelectValue = ViewState["ClientID"].ToString();
                    select_Client.SelectText = c.FullName;
                    select_Client.Enabled = false;
                }

                MCSTreeControl tr_OrganizeCity = (MCSTreeControl)UC_DetailView1.FindControl("SVM_InventoryDifferences_OrganizeCity");
                if (tr_OrganizeCity != null)
                {
                    tr_OrganizeCity.SelectValue = c.OrganizeCity.ToString();
                    tr_OrganizeCity.Enabled = false;
                }

                DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_InventoryDifferences_AccountMonth");
                if (ddl_Month != null)
                {
                    double JXCDelayDays = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["JXCDelayDays"]);
                    ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays)).ToString();
                    ddl_Month.Enabled = false;
                }

                TextBox tbx_InventoryDate = (TextBox)UC_DetailView1.FindControl("SVM_InventoryDifferences_InventoryDate");
                if (tbx_InventoryDate != null)
                {
                    tbx_InventoryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

                bt_Del.Visible = false;
                bt_Save.Visible = false;
                bt_Approve.Visible = false;
                bt_Submit.Visible = false;
                #endregion
            }
            BindDropDown();

            #region 确定页面权限
            if ((int)ViewState["ClientID"] != 0)
            {
                CM_Client _r = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                ViewState["ClientType"] = _r.ClientType;
                if (_r.ClientType == 3)
                {
                    Header.Attributes["WebPageSubCode"] += "ClientType=3";
                    ViewState["EditEnable"] = false;                 
                    lbl_Notice.Text = "请以最小单位数量填报";
                }
                else if (_r.ClientType == 2)             
                    Header.Attributes["WebPageSubCode"] += "ClientType=2&DIClassify=" + _r["DIClassify"];
                
            }
            #endregion
        }
      
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if (ViewState["IsCXP"] == null)
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('1','9')");
        else
        {
            if ((bool)ViewState["IsCXP"])
            {
                ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('9')");
                select_Product.PageUrl += "?IsOpponent=9";
                select_Product1.PageUrl += "?IsOpponent=9";
            }
            else
            {
                ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('1')");
                select_Product.PageUrl += "?IsOpponent=1";
                select_Product1.PageUrl += "?IsOpponent=1";
            }
        }
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand_SelectedIndexChanged(null, null);
    }

    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Classify.DataSource = new PDT_ClassifyBLL()._GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.SelectedValue = "0";
    }
    #endregion


    private void BindData()
    {
        SVM_InventoryDifferencesBLL bll = new SVM_InventoryDifferencesBLL((int)ViewState["InventoryID"]);

        ViewState["ClientID"] = bll.Model.Client;
        ViewState["IsCXP"] = bll.Model["IsCXP"] == "1";

        UC_DetailView1.BindData(bll.Model);
        UC_DetailView1.SetControlsEnable(false);
       
        #region 根据状态设定界面控件可见属性
        if (bll.Model.ApproveFlag == 1 || bll.Model["SubmitFlag"] == "1")
        {
            gv_List.SetControlsEnable(false);
            cb_OnlyDisplayUnZero.Checked = true;
            bt_Save.Visible = false;
            bt_Submit.Visible = false;
            bt_Del.Visible = false;
            tb_AddProduct.Visible = false;
            bt_Submit.Visible = false;

            if (bll.Model.ApproveFlag == 1) bt_Approve.Visible = false;     //未提交
        }
        else
        {
            bt_Del.Visible = (int)Session["UserID"] == bll.Model.InsertStaff;
            bt_Approve.Visible = false;
        }
        #endregion
        ComputDiffInfo(bll);

        BindGrid();        

    }

    private void ComputDiffInfo(SVM_InventoryDifferencesBLL bll)
    {
        #region 计算差异率
        decimal computeinventory = 0, totaldiffvalue = 0, totalabsdiffvalue = 0, absdiffrate = 0,lastdiffvalue=0;

        DataTable dt = SVM_JXCSummaryBLL.GetSummaryListByClient(bll.Model.AccountMonth, bll.Model.AccountMonth, 1, bll.Model.Client, bll.Model["IsCXP"] == "2" ? 1 : 9);
        computeinventory = dt.Rows.Count > 0 ? Convert.ToDecimal(dt.Rows[0]["ComputInventory"]) : 0;
        totaldiffvalue = bll.Items.Sum(p => p.FactoryPrice * p.Quantity);

        totalabsdiffvalue = bll.Items.Sum(p => p.FactoryPrice * Math.Abs(p.Quantity));
        absdiffrate = computeinventory == 0 ? 0 : (totalabsdiffvalue / computeinventory) * 100;

        lastdiffvalue=bll.GetOPIInventory();

        ViewState["ComputeInventory"] = computeinventory;
        ViewState["TotalDiffValue"] = totaldiffvalue;
        ViewState["TotalAbsDiffValue"] = totalabsdiffvalue;  //绝对差异额
        ViewState["AbsDiffRate"] = absdiffrate;              //绝对差异率
        ViewState["LastDiffValue"] = lastdiffvalue;

        lb_DifferenceInfo.Text = string.Format("实时库存额:{0:0.00},上期（截止20日）库存额:{2:0.00},本次盈亏额:{1:0.00}", computeinventory, totaldiffvalue, lastdiffvalue);
        #endregion
    }

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["InventoryID"] == 0)
        {
            MessageBox.Show(this, "请先点击“填报盈亏”按钮，再填写具体的产品！");
            return;
        }
        if (bt_Save.Visible) Save();
        BindGrid();
    }

    protected void cb_OnlyDisplayUnZero_CheckedChanged(object sender, EventArgs e)
    {
        if (bt_Save.Visible) Save();
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void Save()
    {
        SVM_InventoryDifferencesBLL bll = new SVM_InventoryDifferencesBLL((int)ViewState["InventoryID"]);
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_InventoryDifferences_AccountMonth");
        bll.Model.InventoryDate = DateTime.Now.Date;
        bll.Model.AccountMonth = int.Parse(ddl_Month.SelectedValue);
        bll.Model["SubmitFlag"] = bll.Model["SubmitFlag"] == "" ? "2" : bll.Model["SubmitFlag"];
        bll.Update();
        foreach (GridViewRow gr in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[gr.RowIndex][0];
            SVM_InventoryDifferences_Detail item = bll.GetDetailModel(id);

            item.Quantity = int.Parse(((TextBox)gr.FindControl("tbx_Quantity1")).Text) * new PDT_ProductBLL(item.Product).Model.ConvertFactor +
                int.Parse(((TextBox)gr.FindControl("tbx_Quantity2")).Text);
            item.Remark = ((TextBox)gr.FindControl("tbx_Remark")).Text;
            item.LotNumber = ((DropDownList)gr.FindControl("ddl_DifferencesReason")).SelectedValue;
            bll.UpdateDetail(item);
        }

    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        string condition = "SVM_InventoryDifferences_Detail.InventoryID=" + ViewState["InventoryID"].ToString();
        if (ddl_Classify.SelectedValue.CompareTo("0") > 0)
        {
            condition += " AND PDT_Product.Classify=" + ddl_Classify.SelectedValue;
        }
        else if (ddl_Brand.SelectedValue.CompareTo("0") > 0)
        {
            condition += "AND PDT_Product.Brand=" + ddl_Brand.SelectedValue;
        }

        if (select_Product1.SelectValue != "")
        {
            condition += " AND SVM_InventoryDifferences_Detail.Product=" + select_Product1.SelectValue;
        }

        if (cb_OnlyDisplayUnZero.Checked)
        {
            condition += " AND SVM_InventoryDifferences_Detail.Quantity <> 0";
        }
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_InventoryDifferences_AccountMonth");
        AC_AccountMonthBLL prbll = new AC_AccountMonthBLL(int.Parse(ddl_Month.SelectedValue)-1);
        //gv_List.Columns[4].HeaderText = ddl_Month.SelectedItem.Text.Substring( ddl_Month.SelectedItem.Text.LastIndexOf("-")+1)+ "月实时库存（实物）";
        //gv_List.Columns[5].HeaderText = prbll.Model.Month + "月" + "20日期末实物库存";
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
      
        if (!bt_Save.Visible)
        {
            gv_List.SetControlsEnable(false);
        }
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl_DifferencesReason = (DropDownList)e.Row.FindControl("ddl_DifferencesReason");
            if (ddl_DifferencesReason!=null)
            {
                string LotNumber = gv_List.DataKeys[e.Row.RowIndex]["SVM_InventoryDifferences_Detail_LotNumber"].ToString();

                ddl_DifferencesReason.DataSource = DictionaryBLL.GetDicCollections("SVM_DifferencesReason");
                ddl_DifferencesReason.DataBind();
                if (ddl_DifferencesReason.Items.FindByValue(LotNumber) != null)
                {
                    ddl_DifferencesReason.SelectedValue = LotNumber;
                }
            }
        }
    }
    #endregion

    #region 保存按钮事件
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        Save();

        if (sender != null)
            MessageBox.ShowAndRedirect(this, "数据暂存成功,请在数据全部录入完成后，及时点击提交按钮!",
                "InventoryDifferenceInput.aspx?InventoryID=" + ViewState["InventoryID"].ToString() + "&Flag=1&IsCXP=" + ((bool)ViewState["IsCXP"] ? "1" : "0"));
    }
    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        Save();

        if ((int)ViewState["InventoryID"] > 0)
        {
            SVM_InventoryDifferencesBLL bll = new SVM_InventoryDifferencesBLL((int)ViewState["InventoryID"]);
            if (bll.Model["TaskID"] != "" && bll.Model["SubmitFlag"] == "1") return;
            CM_Client client = new CM_ClientBLL(bll.Model.Client).Model;

            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", bll.Model.ID.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            
            dataobjects.Add("ClientID", bll.Model.Client.ToString());
            dataobjects.Add("ClientFullName", client.FullName);
            dataobjects.Add("ClientType", client.ClientType.ToString());
            dataobjects.Add("DIClassify", client["DIClassify"]);
            dataobjects.Add("RTClassify", client["RTClassify"]);
            dataobjects.Add("RTChannel", client["RTChannel"]);

            dataobjects.Add("ComputeInventory", ((decimal)ViewState["ComputeInventory"]).ToString("0.0"));
            dataobjects.Add("TotalDiffValue", ((decimal)ViewState["TotalDiffValue"]).ToString("0.0"));
            dataobjects.Add("TotalAbsDiffValue", ((decimal)ViewState["TotalAbsDiffValue"]).ToString("0.0"));
            dataobjects.Add("AbsDiffRate", ((decimal)ViewState["AbsDiffRate"]).ToString("0.00")+"%");
            dataobjects.Add("ISCXP", bll.Model["IsCXP"]);

            int TaskID = EWF_TaskBLL.NewTask("SVM_InventoryDifferences_Flow", (int)Session["UserID"], "客户名称：" + client.FullName, "~/SubModule/SVM/InventoryDifferenceInput.aspx?InventoryID=" + ViewState["InventoryID"].ToString(), dataobjects);
            if (TaskID > 0)
            {
                bt_Submit.Visible = false;
                bll.Model["TaskID"] = TaskID.ToString();
                bll.Model["SubmitFlag"] = "1";
                bll.Update();
                new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            }

            Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
        }
    }
    #endregion

    protected void btn_Inventory_Click(object sender, EventArgs e)
    {
        #region 创建空的销量列表
        MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_InventoryDifferences_Client");
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_InventoryDifferences_AccountMonth");

        if (select_Client.SelectValue != "")
        {
            TextBox tbx_InventoryDate = (TextBox)UC_DetailView1.FindControl("SVM_InventoryDifferences_InventoryDate");
            if (tbx_InventoryDate != null)
            {
                int id = SVM_InventoryDifferencesBLL.InitProductList(int.Parse(ddl_Month.SelectedValue), int.Parse(select_Client.SelectValue), DateTime.Parse(tbx_InventoryDate.Text), (int)Session["UserID"], (bool)ViewState["IsCXP"]); //空的

                Response.Redirect("InventoryDifferenceInput.aspx?InventoryID=" + id.ToString() + "&Flag=1&IsCXP=" + ((bool)ViewState["IsCXP"] ? "1" : "0"));
            }
        }
        #endregion
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["InventoryID"] != 0)
        {
            new SVM_InventoryDifferencesBLL((int)ViewState["InventoryID"]).Approve((int)Session["UserID"]);

            Response.Redirect("InventoryDifferencesList.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["InventoryID"] != 0)
        {
            new SVM_InventoryDifferencesBLL((int)ViewState["InventoryID"]).Delete();
            Response.Redirect("InventoryDifferencesList.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    protected void bt_AddProduct_Click(object sender, EventArgs e)
    {
        SVM_InventoryDifferencesBLL bll = new SVM_InventoryDifferencesBLL((int)ViewState["InventoryID"]);
        int product = 0;
        if (int.TryParse(select_Product.SelectValue, out product) && product > 0)
        {
            PDT_Product pdt = new PDT_ProductBLL(product).Model;
            if (pdt == null) return;
            int quantity = int.Parse(tbx_Q1.Text) * pdt.ConvertFactor + int.Parse(tbx_Q2.Text);
            if (quantity == 0) return;
            SVM_InventoryDifferences_Detail _detail = bll.Items.FirstOrDefault(m => m.Product == product);
            if (_detail == null)
            {
                _detail = new SVM_InventoryDifferences_Detail();
                decimal factoryprice = 0, price = 0;
                PDT_ProductPriceBLL.GetPriceByClientAndType((int)ViewState["ClientID"], product, 2, out factoryprice, out price);
                _detail.FactoryPrice = factoryprice;
                _detail.Product = pdt.ID;
                _detail.Quantity = quantity;
                _detail.InventoryID = (int)ViewState["InventoryID"];
                bll.AddDetail(_detail);
            }
            else
            {
                _detail.Quantity = quantity;
                bll.UpdateDetail(_detail);
            }

            BindGrid();
        }
    }

    protected string GetDifferenceRate(string difference, string ComputInventory)
    {
        int differenceQ = 0, ComputInventoryQ = 0;
        int.TryParse(difference, out differenceQ);
        int.TryParse(ComputInventory, out ComputInventoryQ);
        return ComputInventoryQ == 0 ? "0" : (differenceQ * 1.00 / ComputInventoryQ).ToString("0.##%");
    }

}
