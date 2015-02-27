using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.Model.OA;
using MCSFramework.BLL.OA;

public partial class SubModule_FNA_FeeApply_Pop_AddFeeApplyDetailItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("meizzDate", Page.ResolveClientUrl("~/App_Themes/basic/meizzDate.js"));
        if (!IsPostBack)
        {
            #region 获取参数
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 1 : int.Parse(Request.QueryString["FeeType"]);
            ViewState["AccountTitle2"] = Request.QueryString["AccountTitle2"] == null ? 0 : int.Parse(Request.QueryString["AccountTitle2"]);
            ViewState["Client"] = Request.QueryString["Client"] == null ? 0 : int.Parse(Request.QueryString["Client"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            ViewState["Brand"] = Request.QueryString["Brand"] == null ? 0 : int.Parse(Request.QueryString["Brand"]);
            ViewState["RelateCar"] = Request.QueryString["RelateCar"] == null ? 0 : int.Parse(Request.QueryString["RelateCar"]);
            ViewState["FromGeneralFlow"] = Request.QueryString["FromGeneralFlow"] == null ? "N" : Request.QueryString["FromGeneralFlow"];

            if (Request.QueryString["OrganizeCity"] != null)
            {
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?OrganizeCity=" + Request.QueryString["OrganizeCity"];
            }
            #endregion

            BindDropDown();

            if (ViewState["Client"] != null && (int)ViewState["Client"] > 0)
            {
                MCSFramework.Model.CM.CM_Client c = new CM_ClientBLL((int)ViewState["Client"]).Model;
                if (c != null)
                {
                    select_Client.SelectText = c.FullName;
                    select_Client.SelectValue = c.ID.ToString();
                    select_Client.Enabled = false;
                }
            }

            #region 是否是车辆参数
            if ((int)ViewState["RelateCar"] > 0)
            {
                Car_CarList car = new Car_CarListBLL((int)ViewState["RelateCar"]).Model;
                if (car != null)
                {
                    lb_RelateCar.Text = car.CarNo;
                    tb_Client.Visible = false;
                }
                else
                {
                    ViewState["RelateCar"] = 0;
                    tb_Car.Visible = false;
                }
            }
            else
            {
                tb_Car.Visible = false;
            }
            #endregion

            #region 传递或创建空的费用明细列表
            ListTable<FNA_FeeApplyDetail> _details;

            if (Session["FeeApplyDetail"] != null)
            {
                _details = (ListTable<FNA_FeeApplyDetail>)Session["FeeApplyDetail"];
                int max = 0;
                foreach (FNA_FeeApplyDetail item in _details.GetListItem())
                {
                    if (item.ID > max) max = item.ID;
                }
                ViewState["MaxID"] = max;
                ViewState["Details"] = _details;
            }
            else
            {
                _details = new ListTable<FNA_FeeApplyDetail>(new List<FNA_FeeApplyDetail>(), "ID");
                ViewState["MaxID"] = 0;
                ViewState["Details"] = _details;
                AddEmptyDetail();
            }


            #endregion

            BindGrid();
        }
    }

    private void BindDropDown()
    {
        #region 绑定产品列表
        cbl_Brand.DataTextField = "Name";
        cbl_Brand.DataValueField = "ID";
        cbl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=1");
        cbl_Brand.DataBind();
        if ((int)ViewState["Brand"] == 0)
        {
            cbx_CheckAll.Checked = true;
            cbx_CheckAll_CheckedChanged(null, null);
        }
        else
        {
            cbl_Brand.Items.FindByValue(ViewState["Brand"].ToString()).Selected = true;
            cbx_CheckAll.Enabled = false;
            cbl_Brand.Enabled = false;
        }
        #endregion

        #region 最迟允许核销月份
        int lastmonth = ConfigHelper.GetConfigInt("LastWriteOffMonth");
        if (lastmonth == 0) lastmonth = 3;
        ddl_LastWriteOffMonth.DataSource = AC_AccountMonthBLL.GetModelList("ID>" +
            ViewState["AccountMonth"].ToString() + "AND ID<=" +
            ((int)ViewState["AccountMonth"] + lastmonth).ToString());
        ddl_LastWriteOffMonth.DataBind();
        #endregion

    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbl_Brand.Items)
        {
            item.Selected = cbx_CheckAll.Checked;
        }
    }

    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        ddl_LinkMan.Items.Clear();

        int client = 0;
        if (int.TryParse(select_Client.SelectValue, out client) && client > 0)
        {
            IList<CM_LinkMan> lists = CM_LinkManBLL.GetModelList("ClientID = " + client.ToString());
            foreach (CM_LinkMan item in lists)
            {
                ddl_LinkMan.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }

            CM_ClientBLL clientbll = new CM_ClientBLL(client);
            decimal forcast = clientbll.GetSalesForcast((int)ViewState["AccountMonth"]);
            tbx_SalesForcast.Text = forcast.ToString("0.##");
            tbx_SalesForcast.ReadOnly = forcast > 0;

            lb_PreSalesVolume.Text = clientbll.GetSalesVolume(AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString("0.##元");
            lb_AvgSalesVolume.Text = clientbll.GetSalesVolumeAvg().ToString("0.##元");

        }
        ddl_LinkMan.Items.Insert(0, new ListItem("请选择", "0"));
    }

    #region 返回指定类型的会计科目
    protected IList<AC_AccountTitle> GetAccountTitleList()
    {
        IList<AC_AccountTitle> lists;
        if ((int)ViewState["AccountTitle2"] == 0)
            lists = AC_AccountTitleBLL.GetListByFeeType((int)ViewState["FeeType"]);
        else
            lists = AC_AccountTitleBLL.GetModelList("(ID = 1 OR SuperID=" + ViewState["AccountTitle2"].ToString() + ") AND MCS_SYS.dbo.UF_Spilt2('MCS_Pub.dbo.AC_AccountTitle',ExtPropertys,'IsDisable')<>'Y'");
        
        if (ViewState["FromGeneralFlow"].ToString() == "Y")
        {
            lists = lists.Where(p => p["CanApplyInGeneralFlow"] != "N").ToList();
        }
        return lists;
    }
    #endregion

    protected void AddEmptyDetail()
    {
        SaveGrid();

        ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
        for (int i = 0; i < 5; i++)
        {
            ViewState["MaxID"] = (int)ViewState["MaxID"] + 1;

            FNA_FeeApplyDetail item;

            item = new FNA_FeeApplyDetail();
            item.AccountTitle = 1;
            item.ApplyCost = 0;
            item.ID = (int)ViewState["MaxID"];

            #region 获取当前会计月的开始及截止日期
            int month = (int)ViewState["AccountMonth"];
            if (month == 0) month = AC_AccountMonthBLL.GetCurrentMonth();
            AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
            item.BeginDate = m.BeginDate;
            item.EndDate = m.EndDate;
            item.BeginMonth = month;
            item.EndMonth = month;
            #endregion

            item.Flag = 1;                  //未报销
            item.Remark = "";
            _details.Add(item);             //新增科目 
        }
        BindGrid();
    }

    #region 绑定费用申请明细列表
    private void BindGrid()
    {
        ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
        gv_List.BindGrid<FNA_FeeApplyDetail>(_details.GetListItem());

        //求销售额合计
        decimal _totalcost = 0;
        foreach (FNA_FeeApplyDetail _detail in _details.GetListItem())
        {
            _totalcost += _detail.ApplyCost + _detail.AdjustCost;
        }
        lb_TotalCost.Text = _totalcost.ToString("0.###");
    }
    #endregion

    #region 保存GridView内里数据至内存
    private bool SaveGrid()
    {
        #region 获取关联品牌明细
        string relatebrands = "";
        foreach (ListItem item in cbl_Brand.Items)
        {
            if (item.Selected) relatebrands += item.Value + ",";
        }
        if (relatebrands.EndsWith(",")) relatebrands = relatebrands.Substring(0, relatebrands.Length - 1);
        #endregion

        ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
        foreach (GridViewRow gr in gv_List.Rows)
        {
            FNA_FeeApplyDetail item = new FNA_FeeApplyDetail();
            item.ID = (int)gv_List.DataKeys[gr.RowIndex]["ID"];
            if (select_Client.SelectValue != "")
            {
                item.Client = int.Parse(select_Client.SelectValue);
                item["RelateLinkMan"] = ddl_LinkMan.SelectedValue;
                item.SalesForcast = decimal.Parse(tbx_SalesForcast.Text);
            }
            item.AccountTitle = int.Parse(((DropDownList)gr.FindControl("ddl_AccountTitle")).SelectedValue);
            if (((TextBox)gr.FindControl("tbx_ApplyCost")).Text.Trim() != "")
                item.ApplyCost = decimal.Parse(((TextBox)gr.FindControl("tbx_ApplyCost")).Text.Trim());
            if (((TextBox)gr.FindControl("tbx_DICost")).Text.Trim() != "")
                item.DICost = decimal.Parse(((TextBox)gr.FindControl("tbx_DICost")).Text.Trim());

            if (item.AccountTitle > 1 && item.ApplyCost != 0)
            {
                #region 获取费用的开始及截止日期
                TextBox tbx_BeginDate = (TextBox)gr.FindControl("tbx_BeginDate");
                TextBox tbx_EndDate = (TextBox)gr.FindControl("tbx_EndDate");

                if (tbx_BeginDate != null && !string.IsNullOrEmpty(tbx_BeginDate.Text))
                {
                    item.BeginDate = DateTime.Parse(tbx_BeginDate.Text);
                    item.BeginMonth = AC_AccountMonthBLL.GetMonthByDate(item.BeginDate);
                }
                else
                {
                    item.BeginDate = _details[item.ID.ToString()].BeginDate;
                    item.BeginMonth = _details[item.ID.ToString()].BeginMonth;
                }

                if (tbx_EndDate != null && !string.IsNullOrEmpty(tbx_EndDate.Text))
                {
                    item.EndDate = DateTime.Parse(tbx_EndDate.Text);
                    item.EndMonth = AC_AccountMonthBLL.GetMonthByDate(item.EndDate);
                }
                else
                {
                    item.EndDate = _details[item.ID.ToString()].EndDate;
                    item.EndMonth = _details[item.ID.ToString()].EndMonth;
                }

                if (item.BeginDate > item.EndDate)
                {
                    MessageBox.Show(this, "费用发生范围的开始时间不能大于截止时间");
                    return false;
                }
                #endregion
                item.LastWriteOffMonth = int.Parse(ddl_LastWriteOffMonth.SelectedValue);
                item.Remark = ((TextBox)gr.FindControl("tbx_Remark")).Text;

                if (item.Remark == "")
                {
                    MessageBox.Show(this, "费用说明必填");
                    return false;
                }

                if (TreeTableBLL.GetChild("MCS_PUB.dbo.AC_AccountTitle", "ID", "SuperID", item.AccountTitle).Rows.Count > 0)
                {
                    MessageBox.Show(this, "费用科目必须选择最底级会计科目!" + ((DropDownList)gr.FindControl("ddl_AccountTitle")).SelectedItem.Text);
                    return false;
                }
                item.Flag = 1;      //未报销

                item.RelateBrands = relatebrands;
                _details.Update(item);
            }
            else
            {
                _details.Remove(item);
            }
        }

        return true;
    }
    #endregion

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SaveGrid();
        int id = (int)gv_List.DataKeys[e.RowIndex]["ID"];

        ListTable<FNA_FeeApplyDetail> _details = ViewState["Details"] as ListTable<FNA_FeeApplyDetail>;
        _details.Remove(id.ToString());

        AddEmptyDetail();

        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void bt_AddDetail_Click(object sender, EventArgs e)
    {
        AddEmptyDetail();
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue == "" && (int)ViewState["FeeType"] != 9)
        {
            string RelateClientFeeType = ConfigHelper.GetConfigString("RelateClientFeeType");
            if (!string.IsNullOrEmpty(RelateClientFeeType))
            {
                string[] s = RelateClientFeeType.Split(',');
                if (s.Where(p => p == ViewState["FeeType"].ToString()).ToArray().Length > 0)
                {
                    MessageBox.Show(this, "该费用类型必须选择客户!");
                    return;
                }
            }
        }

        if (SaveGrid())
        {
            Session["FeeApplyDetail"] = ViewState["Details"];
            Session["SuccessFlag"] = true;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
        }
    }


}
