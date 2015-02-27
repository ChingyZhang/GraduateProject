using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Collections;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL;
using MCSFramework.UD_Control;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Promotor;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Pub;
using System.Data;

public partial class SubModule_FNA_FeeWriteoff_FeeWriteOffDetail_Print : System.Web.UI.Page
{
    private int PRINTPAGESIZE = 5;
    private Hashtable ht_pageitem = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            BindDropDown();

            if ((int)ViewState["ID"] == 0) Response.Redirect("FeeWriteOffList.aspx");

            BindData();

        }
    }

    private void BindDropDown()
    {
    }

    #region 绑定报销单详细信息
    private void BindData()
    {
        int id = (int)ViewState["ID"];

        FNA_FeeWriteOffBLL bll = new FNA_FeeWriteOffBLL(id);
        ViewState["Details"] = bll.Items.OrderBy(p => p.Client).ToList();
        ViewState["FLPurchase"] = FNA_FeeWriteOffBLL.GetPurchaseVolume((int)ViewState["ID"]);
        FNA_FeeWriteOff writeoff = bll.Model;
        if (writeoff == null) Response.Redirect("FeeWriteOffList.aspx");

        pn_FeeWriteOff.BindData(writeoff);

        ViewState["AccountMonth"] = writeoff.AccountMonth;
        ViewState["SheetCode"] = writeoff.SheetCode;
        ViewState["State"] = writeoff.State;
        ViewState["HasFeeApply"] = writeoff["HasFeeApply"];
        ViewState["IsEvectionWriteOff"] = writeoff["IsEvectionWriteOff"];
        ViewState["InsteadPayClient"] = bll.Model.InsteadPayClient;
        writeoff["Remark"] = writeoff["Remark"].Replace("\r", "<br/>");
        detailPrint.Visible = true;
        if (bll.Model.InsteadPayClient > 0)
        {
            BindGridPrint();
            string type = "";
            if (writeoff["InvoiceClassAB"] != "0" && writeoff["InvoiceClassAB"] != "")
                try
                {
                    type = DictionaryBLL.Dictionary_Data_GetAlllList("TableName='FNA_InvoiceClassAB' AND Code='" + writeoff["InvoiceClassAB"] + "'")[0].Name;
                }
                catch
                {
                }

            lbl_message.Text = "<font color='red'>" + new CM_ClientBLL((int)ViewState["InsteadPayClient"]).Model.FullName + "</font>代垫费用" + "<font color='red'>" + type + "</font>" + "明细表";
            div_client.InnerText = new CM_ClientBLL((int)ViewState["InsteadPayClient"]).Model.FullName;
            div_type.InnerText = div_type2.InnerText = div_type3.InnerText = div_type0.InnerText = type;
            div_SheetCode.InnerText = writeoff.SheetCode;
        }
        else if (bll.Model["InsteadPayStaff"] != "0" && bll.Model["InsteadPayStaff"] != "")
        {
            BindGridPrint();
            string type = "";
            if (writeoff["InvoiceClassAB"] != "0" && writeoff["InvoiceClassAB"] != "")
                try
                {
                    type = DictionaryBLL.Dictionary_Data_GetAlllList("TableName='FNA_InvoiceClassAB' AND Code='" + writeoff["InvoiceClassAB"] + "'")[0].Name;
                }
                catch
                {
                }
            lbl_message.Text = "<font color='red'>" + new Org_StaffBLL(int.Parse(writeoff["InsteadPayStaff"])).Model.RealName + "</font>代垫费用" + "<font color='red'>" + type + "</font>" + "明细表";
            div_client.InnerText = new Org_StaffBLL(int.Parse(writeoff["InsteadPayStaff"])).Model.RealName;
            div_type.InnerText = div_type2.InnerText = div_type3.InnerText = div_type0.InnerText = type;
            div_SheetCode.InnerText = writeoff.SheetCode;
            div_insType.InnerText = "员工：";
            label_insName.InnerText = "该员工";
            label_insName2.InnerText = label_insName3.InnerText = "代垫员工";
        }
        writeoff.InsteadPayClient = 0;
        pn_Remark.BindData(writeoff);

        //求费用核消金额合计
        decimal _totalcost = 0;
        foreach (FNA_FeeWriteOffDetail _detail in bll.Items)
        {
            _totalcost += _detail.WriteOffCost + _detail.AdjustCost;
        }
        _totalcost = Math.Round(_totalcost, 1);
        lb_TotalCostCN.Text = MCSFramework.Common.Rmb.CmycurD(_totalcost.ToString());
        lb_TotalCost.Text = _totalcost.ToString("#,##0.00");
        lab_TotalCostCN.Text = lb_TotalCostCN.Text;
        lab_TotalCost.Text = lb_TotalCost.Text;
        BindGrid();

        #region 绑定差旅行程
        if (writeoff["IsEvectionWriteOff"] == "Y")
        {
            tb_EvectionRouteList.Visible = true;
            gv_EvectionRouteList.ConditionString = "FNA_EvectionRoute.WriteOffID = " + ViewState["ID"].ToString();
            gv_EvectionRouteList.BindGrid();
        }
        #endregion
    }
    #endregion

    #region 绑定费用核消明细打印列表
    private void BindGridPrint()
    {

        IList<FNA_FeeWriteOffDetail> list = (IList<FNA_FeeWriteOffDetail>)ViewState["Details"];
        gv_ListDetail.BindGrid<FNA_FeeWriteOffDetail>(list.OrderBy(p => p.Client).ThenBy(p => p.AccountTitle).ThenBy(p => p.BeginMonth).ToList());

    }
    #endregion

    #region 绑定费用核消明细列表
    private void BindGrid()
    {
        IList<FNA_FeeWriteOffDetail> list = ((IList<FNA_FeeWriteOffDetail>)ViewState["Details"]).OrderBy(p => p.Client).ThenBy(p => p.AccountTitle).ThenBy(p => p.BeginMonth).ToList();

        IList<AC_AccountTitle> titleList = AC_AccountTitleBLL.GetModelList(ConfigHelper.GetConfigString("PagingType"));

        int pagecount = 0, cycle = 1;

        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].Client == list[i - 1].Client || (titleList.Where(p => p.ID == list[i].AccountTitle).Count() == 0 && titleList.Where(p => p.ID == list[i-1].AccountTitle).Count() == 0))
            {
                if (++cycle == PRINTPAGESIZE)
                {
                    ht_pageitem[pagecount.ToString()] = cycle;
                    pagecount++;
                    cycle = 0;
                }
            }
            else if (cycle > 0)
            {
                ht_pageitem[pagecount.ToString()] = cycle;
                pagecount++;
                cycle = 1;
            }
            else if (cycle == 0)
            {
                cycle = 1;
            }

        }
        if (cycle > 0)
        {
            ht_pageitem[pagecount.ToString()] = cycle;
            pagecount++;
        }
        int[] Pages = new int[pagecount];


        //Pages = new int[list.Count / PRINTPAGESIZE + (list.Count % PRINTPAGESIZE == 0 ? 0 : 1)];

        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i] = i;
        }

        ViewState["TotalPage"] = Pages.Length;
        Repeater1.DataSource = Pages;
        Repeater1.DataBind();
    }
    #endregion


    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int applydetailid = (int)((UC_GridView)sender).DataKeys[e.Row.RowIndex]["ApplyDetailID"];
            int writeoffdetailid = (int)((UC_GridView)sender).DataKeys[e.Row.RowIndex]["ID"];
            if (applydetailid > 0)
            {
                Label lb_ApplySheetCode = (Label)e.Row.FindControl("lb_ApplySheetCode");
                if (lb_ApplySheetCode != null)
                    lb_ApplySheetCode.Text = FNA_FeeApplyBLL.GetSheetCodeByDetailID(applydetailid);
                FNA_FeeApplyDetail _detail = new FNA_FeeApplyBLL().GetDetailModel(applydetailid);
                Label lb_AllCost = (Label)e.Row.FindControl("lb_AllCost");
                if (lb_AllCost != null)
                    lb_AllCost.Text = Math.Round(decimal.Parse(_detail["DICost"] == "" ? "0" : _detail["DICost"]) + _detail.ApplyCost, 1).ToString("0.##");
                Label lb_Remark = (Label)e.Row.FindControl("lb_Remark");
                Label lb_ContractCode = (Label)e.Row.FindControl("lb_ContractCode");
                FNA_FeeWriteOffDetail _writeoffdetail = new FNA_FeeWriteOffBLL().GetDetailModel(writeoffdetailid);//(int)gv_ListDetail.DataKeys[e.Row.RowIndex][0]
                if (_detail.RelateContractDetail != 0)
                {
                    CM_ContractDetail detail = new CM_ContractBLL().GetDetailModel(_detail.RelateContractDetail);
                    if (detail != null)
                    {
                        CM_Contract _mContract = new CM_ContractBLL(detail.ContractID).Model;
                        lb_ContractCode.Text = "合同编码:" + _mContract.ContractCode;
                    }
                    else lb_ContractCode.Text = "合同编码:【未找到指定合同】";
                }
                if (lb_Remark != null)
                {
                    try
                    {
                        if (_detail.AccountTitle == 80) //导购工资
                        {
                            lb_Remark.Text = _detail.Remark.Replace("－", "<br/>");
                        }
                        else if (_detail.AccountTitle == 82)//无导返利
                        {
                            lb_Remark.Text = _detail["FLRemark"];
                            if (ViewState["FLPurchase"] != null)
                                lb_Remark.Text = "进货额:" + ((DataTable)ViewState["FLPurchase"]).Compute("Sum(PurchaseVolume)", "ID=" + _writeoffdetail.ID).ToString() + ";" + _detail["FLRemark"];
    
                        }
                        else if (_writeoffdetail.Remark.IndexOf("是否CA") > 0)
                        {
                            lb_Remark.Text = _writeoffdetail.Remark;
                        }
                        else
                        {
                            lb_Remark.Text = _detail.Remark;
                        }
                    }
                    catch (Exception)
                    {
                        lb_Remark.Text = _detail.Remark;
                    }
                }
                Label lb_RelateBrand = (Label)e.Row.FindControl("lb_RelateBrand");
                if (lb_RelateBrand != null)
                {
                    string[] brands = new FNA_FeeApplyBLL().GetDetailModel(applydetailid).RelateBrands.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lb_RelateBrand.Text = "";

                    foreach (string b in brands)
                    {
                        lb_RelateBrand.Text += new PDT_BrandBLL(int.Parse(b)).Model.Name + ",";
                    }
                }
            }

            int client = (int)((UC_GridView)sender).DataKeys[e.Row.RowIndex]["Client"];

            if (client > 0)
            {
                Label lb_RTChannel = (Label)e.Row.FindControl("lb_RTChannel");
                CM_Client c = new CM_ClientBLL(client).Model;
                if (c != null && c.ClientType == 3 && c["RTChannel"] != "" && lb_RTChannel != null)
                {
                    if (DictionaryBLL.GetDicCollections("CM_RT_Channel").Keys.Contains(c["RTChannel"]))
                        lb_RTChannel.Text = DictionaryBLL.GetDicCollections("CM_RT_Channel")[c["RTChannel"]].ToString();
                }
            }
            else
            {
                Label lb_RTChannel = (Label)e.Row.FindControl("lb_CanWriteOffCost");
                lb_RTChannel.Visible = false;
            }

        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lb_SheetCode = (Label)e.Item.FindControl("lb_SheetCode");
        lb_SheetCode.Text = string.Format("核销单号:{0}---费用明细 第{1}页,共{2}页", ViewState["SheetCode"], e.Item.ItemIndex + 1, ViewState["TotalPage"]);

        Literal _c = (Literal)e.Item.FindControl("lb_RepeaterNextPage");
        if (_c != null && e.Item.ItemIndex % 2 == 0)
        {
            _c.Text = "<br/><div class='PageNext'></div><br/>";
        }

        UC_GridView gv_List = (UC_GridView)e.Item.FindControl("gv_List");
        if (gv_List != null)
        {
            IList<FNA_FeeWriteOffDetail> list = ((IList<FNA_FeeWriteOffDetail>)ViewState["Details"]).OrderBy(p => p.Client).ThenBy(p => p.AccountTitle).ThenBy(p => p.BeginMonth).ToList();

            IList<FNA_FeeWriteOffDetail> l = new List<FNA_FeeWriteOffDetail>();
            decimal subtotal = 0;
            if (ht_pageitem.Contains(e.Item.ItemIndex.ToString()) && (int)ht_pageitem[e.Item.ItemIndex.ToString()] > 0)
            {
                for (int i = 0; i < (int)ht_pageitem[e.Item.ItemIndex.ToString()]; i++)
                {

                    if (list.Count > 0)
                    {
                        FNA_FeeWriteOffDetail m = list[0];
                        list.Remove(m);
                        l.Add(m);
                        subtotal += m.WriteOffCost + m.AdjustCost;
                    }
                }
                for (int j = 0; j < PRINTPAGESIZE - (int)ht_pageitem[e.Item.ItemIndex.ToString()]; j++)
                {
                    l.Add(new FNA_FeeWriteOffDetail());
                }
            }


            gv_List.BindGrid(l);
            ViewState["Details"] = list;
            if ((int)ViewState["State"] == 3)
            {
                //核销完成,可见调整金额及原因
                gv_List.Columns[gv_List.Columns.Count - 1].Visible = true; //扣减原因
                gv_List.Columns[gv_List.Columns.Count - 2].Visible = true; //扣减方式
                gv_List.Columns[gv_List.Columns.Count - 3].Visible = true; //扣减金额
                gv_List.Columns[gv_List.Columns.Count - 4].Visible = true; //批复金额

                gv_List.Columns[gv_List.Columns.Count - 5].Visible = true; //扣减原因
                gv_List.Columns[gv_List.Columns.Count - 6].Visible = true; //扣减方式
                gv_List.Columns[gv_List.Columns.Count - 7].Visible = true; //扣减金额
            }

            if (ViewState["HasFeeApply"].ToString() == "N")
            {
                //当费用为无申请费用时，下列字段隐藏
                gv_List.Columns[0].Visible = false;        //发生客户
                gv_List.Columns[1].Visible = false;        //客户渠道
                gv_List.Columns[6].Visible = false;        //申请单备案号
            }

            if (ViewState["IsEvectionWriteOff"].ToString() == "Y")
            {
                //当核销单关联至差旅行程报销时，下列字段隐藏
                gv_List.Columns[5].Visible = false;        //备注
            }

            Label lb_SubTotalCostCN = (Label)e.Item.FindControl("lb_SubTotalCostCN");

            if (lb_SubTotalCostCN != null)
                lb_SubTotalCostCN.Text = MCSFramework.Common.Rmb.CmycurD(subtotal);

            Label lb_SubTotalCost = (Label)e.Item.FindControl("lb_SubTotalCost");
            if (lb_SubTotalCost != null)
                lb_SubTotalCost.Text = subtotal.ToString("0.##");
        }

    }
    protected void gv_ListDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int applydetailid = (int)gv_ListDetail.DataKeys[e.Row.RowIndex]["ApplyDetailID"];
            if (applydetailid > 0)
            {
                Label lb_ApplySheetCode = (Label)e.Row.FindControl("lb_ApplySheetCode");
                if (lb_ApplySheetCode != null)
                    lb_ApplySheetCode.Text = FNA_FeeApplyBLL.GetSheetCodeByDetailID(applydetailid);
                FNA_FeeApplyDetail _detail = new FNA_FeeApplyBLL().GetDetailModel(applydetailid);
                FNA_FeeWriteOffDetail _writeoffdetail = new FNA_FeeWriteOffBLL().GetDetailModel((int)gv_ListDetail.DataKeys[e.Row.RowIndex]["ID"]);
                Label lb_AllCost = (Label)e.Row.FindControl("lb_AllCost");
                if (lb_AllCost != null)
                    lb_AllCost.Text = Math.Round(decimal.Parse(_detail["DICost"] == "" ? "0" : _detail["DICost"]) + _detail.ApplyCost, 1).ToString("0.##");
                Label lb_Remark = (Label)e.Row.FindControl("lb_Remark");

                if (lb_Remark != null)
                {
                    try
                    {
                        if (_detail.AccountTitle == 80) //导购工资
                        {
                            lb_Remark.Text = _detail.Remark.Replace("－", "<br/>");
                        }
                        else if (_detail.AccountTitle == 82)//无导返利
                        {
                            lb_Remark.Text = _detail["FLRemark"];
                            if (ViewState["FLPurchase"] != null)
                                lb_Remark.Text = "进货额:" + ((DataTable)ViewState["FLPurchase"]).Compute("Sum(PurchaseVolume)", "ID=" + _writeoffdetail.ID).ToString() + ";" + _detail["FLRemark"];
                        }
                        else
                        {
                            lb_Remark.Text = _writeoffdetail.Remark;
                        }
                    }
                    catch (Exception)
                    {
                        lb_Remark.Text = _writeoffdetail.Remark;
                    }
                }
                Label lb_RelateBrand = (Label)e.Row.FindControl("lb_RelateBrand");
                if (lb_RelateBrand != null)
                {
                    string[] brands = new FNA_FeeApplyBLL().GetDetailModel(applydetailid).RelateBrands.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lb_RelateBrand.Text = "";

                    foreach (string b in brands)
                    {
                        lb_RelateBrand.Text += new PDT_BrandBLL(int.Parse(b)).Model.Name + ",";
                    }
                }
                int client = (int)((UC_GridView)sender).DataKeys[e.Row.RowIndex]["Client"];

                if (client > 0)
                {
                    Label lb_RTChannel = (Label)e.Row.FindControl("lb_RTChannel");
                    CM_Client c = new CM_ClientBLL(client).Model;
                    if (c != null && c.ClientType == 3 && c["RTChannel"] != "" && lb_RTChannel != null)
                    {
                        if (DictionaryBLL.GetDicCollections("CM_RT_Channel").Keys.Contains(c["RTChannel"]))
                            lb_RTChannel.Text = DictionaryBLL.GetDicCollections("CM_RT_Channel")[c["RTChannel"]].ToString();
                    }
                }

            }
        }
    }
}
