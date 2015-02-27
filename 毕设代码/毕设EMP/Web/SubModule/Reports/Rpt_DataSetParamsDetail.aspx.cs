// ===================================================================
// 文件路径:SubModule/Reports/Rpt_DataSetParamsDetail.aspx.cs 
// 生成日期:2010/9/30 13:22:00 
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
using MCSFramework.BLL.RPT;
using MCSFramework.Model.RPT;
using MCSFramework.BLL;
public partial class SubModule_Reports_Rpt_DataSetParamsDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["DataSet"] = Request.QueryString["DataSet"] != null ? new Guid(Request.QueryString["DataSet"]) : Guid.Empty;
            ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
            #endregion

            BindDropDown();

            if ((Guid)ViewState["ID"] != Guid.Empty)
            {
                //修改
                BindData();
                td_TemParam.Visible = false;
            }
            else
            {
                //新增
                if ((Guid)ViewState["DataSet"] == Guid.Empty) Response.Redirect("Rpt_DataSetList.aspx");
                bt_Delete.Visible = false;

                TextBox tbx_ParamSortID = (TextBox)pl_detail.FindControl("Rpt_DataSetParams_ParamSortID");
                if (tbx_ParamSortID != null)
                {
                    int maxsortid = 0;

                    IList<Rpt_DataSetParams> paramlist = new Rpt_DataSetBLL((Guid)ViewState["DataSet"]).GetParams();
                    if (paramlist.Count > 0) maxsortid = paramlist.Max(p => p.ParamSortID);

                    tbx_ParamSortID.Text = (++maxsortid).ToString();
                    ViewState["MaxSortID"] = maxsortid;
                }
            }
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    protected void rbl_RelationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (rbl_RelationType.SelectedValue)
        {
            case "1":
                tr_1.Visible = true;
                tr_2.Visible = false;
                ddl_RelationTableName.DataTextField = "Name";
                ddl_RelationTableName.DataValueField = "TableName";
                ddl_RelationTableName.DataSource = DictionaryBLL.Dictionary_Type_GetAllList();
                ddl_RelationTableName.DataBind();
                break;
            case "2":
                tr_1.Visible = true;
                tr_2.Visible = true;
                ddl_RelationTableName.DataTextField = "DisplayName";
                ddl_RelationTableName.DataValueField = "Name";
                ddl_RelationTableName.DataSource = new UD_TableListBLL()._GetModelList("");
                ddl_RelationTableName.DataBind();
                ddl_RelationTableName_SelectedIndexChanged(null, null);
                break;
            case "3":
                tr_1.Visible = false;
                tr_2.Visible = false;
                break;
        }
    }

    protected void ddl_RelationTableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_RelationTableName.Visible && rbl_RelationType.SelectedValue == "2")
        {

            ddl_RelationTextField.DataSource = UD_ModelFieldsBLL.GetModelList("TableID='" +
                new UD_TableListBLL(ddl_RelationTableName.SelectedValue).Model.ID.ToString() + "'");
            ddl_RelationTextField.DataBind();

            ddl_RelationValueField.DataSource = ddl_RelationTextField.DataSource;
            ddl_RelationValueField.DataBind();
        }
    }
    #endregion

    private void BindData()
    {
        Rpt_DataSetParams m = new Rpt_DataSetParamsBLL((Guid)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);
            ViewState["DataSet"] = m.DataSet;

            if (m.RelationType > 0)
            {
                rbl_RelationType.SelectedValue = m.RelationType.ToString();
                rbl_RelationType_SelectedIndexChanged(null, null);
            }

            if (ddl_RelationTableName != null || ddl_RelationTableName.Items.Count != 0)
            {
                ddl_RelationTableName.SelectedValue = m.RelationTableName;
                ddl_RelationTableName_SelectedIndexChanged(null, null);
            }
            if (ddl_RelationTextField != null || ddl_RelationTextField.Items.Count != 0)
            {
                ddl_RelationTextField.SelectedValue = m.RelationTextField;
            }
            if (ddl_RelationValueField != null || ddl_RelationValueField.Items.Count != 0)
            {
                ddl_RelationValueField.SelectedValue = m.RelationValueField;
            }

        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        new Rpt_DataSetBLL((Guid)ViewState["DataSet"]).ClearCache();

        Rpt_DataSetParamsBLL _bll;
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            _bll = new Rpt_DataSetParamsBLL((Guid)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new Rpt_DataSetParamsBLL();
            _bll.Model.DataSet = (Guid)ViewState["DataSet"];
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项

        #endregion

        #region 参数数据源关联类型
        _bll.Model.RelationType = int.Parse(rbl_RelationType.SelectedValue);
        if (ddl_RelationTableName.Visible)
        {
            _bll.Model.RelationTableName = ddl_RelationTableName.SelectedValue;
        }
        else
        {
            _bll.Model.RelationTableName = "";
        }
        if (ddl_RelationTextField.Visible)
        {
            _bll.Model.RelationTextField = ddl_RelationTextField.SelectedValue;
        }
        else
        {
            _bll.Model.RelationTextField = "";
        }
        if (ddl_RelationValueField.Visible)
        {
            _bll.Model.RelationValueField = ddl_RelationValueField.SelectedValue;
        }
        else
        {
            _bll.Model.RelationValueField = "";
        }
        #endregion


        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "Rpt_DataSetParamsList.aspx?ID=" + _bll.Model.DataSet.ToString());
            }
        }
        else
        {
            //新增
            if (_bll.Add() == 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "Rpt_DataSetParamsList.aspx?ID=" + _bll.Model.DataSet.ToString());
            }
        }

    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        new Rpt_DataSetParamsBLL((Guid)ViewState["ID"]).Delete();
        Response.Redirect("Rpt_DataSetParamsList.aspx?ID=" + ViewState["DataSet"].ToString());
    }
    protected void bt_AddTemParam_Click(object sender, EventArgs e)
    {
        Rpt_DataSetParamsBLL _bll = new Rpt_DataSetParamsBLL();
        _bll.Model.DataSet = (Guid)ViewState["DataSet"];
        _bll.Model.ParamSortID = ViewState["MaxSortID"] != null ? (int)ViewState["MaxSortID"] : 0;
        _bll.Model["IsRequireField"] = "Y";
        _bll.Model["Enable"] = "Y";
        _bll.Model.Visible = "Y";

        switch (ddl_TemParam.SelectedValue)
        {
            case "":
                MessageBox.Show(this, "请选择要增加的预定参数!");
                return;
            case "OrganizeCity":
                _bll.Model.ParamName = "@OrganizeCity";
                _bll.Model.DisplayName = "管理片区";
                _bll.Model.DataType = 1;
                _bll.Model.DefaultValue = "$StaffOrganizeCity$";
                _bll.Model.ControlType = 7;
                _bll.Model["ControlWidth"] = "220";

                _bll.Model.RelationType = 2;
                _bll.Model.RelationTableName = "MCS_SYS.dbo.Addr_OrganizeCity";
                _bll.Model.RelationTextField = "Name";
                _bll.Model.RelationValueField = "ID";
                break;
            case "AccountMonth":
                _bll.Model.ParamName = "@AccountMonth";
                _bll.Model.DisplayName = "会计月";
                _bll.Model.DataType = 1;
                _bll.Model.DefaultValue = "$CurrentAccountMonth$";
                _bll.Model.ControlType = 3;

                _bll.Model.RelationType = 2;
                _bll.Model.RelationTableName = "MCS_Pub.dbo.AC_AccountMonth";
                _bll.Model.RelationTextField = "Name";
                _bll.Model.RelationValueField = "ID";
                break;
            case "Staff":
                _bll.Model.ParamName = "@Staff";
                _bll.Model.DisplayName = "员工";
                _bll.Model.DataType = 1;
                _bll.Model.DefaultValue = "$StaffID$";
                _bll.Model.ControlType = 6;
                _bll.Model.SearchPageURL = "~/SubModule/StaffManage/Pop_Search_Staff.aspx";
                _bll.Model["ControlWidth"] = "260";

                _bll.Model.RelationType = 2;
                _bll.Model.RelationTableName = "MCS_SYS.dbo.Org_Staff";
                _bll.Model.RelationTextField = "RealName";
                _bll.Model.RelationValueField = "ID";
                break;
            case "Retailer":
                _bll.Model.ParamName = "@ClientID";
                _bll.Model.DisplayName = "零售商";
                _bll.Model.DataType = 1;
                _bll.Model.ControlType = 6;
                _bll.Model.SearchPageURL = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=3";
                _bll.Model["ControlWidth"] = "260";

                _bll.Model.RelationType = 2;
                _bll.Model.RelationTableName = "MCS_CM.dbo.CM_Client";
                _bll.Model.RelationTextField = "FullName";
                _bll.Model.RelationValueField = "ID";
                break;
            case "Distributor":
                _bll.Model.ParamName = "@ClientID";
                _bll.Model.DisplayName = "经销商";
                _bll.Model.DataType = 1;
                _bll.Model.ControlType = 6;
                _bll.Model.SearchPageURL = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2";
                _bll.Model["ControlWidth"] = "260";

                _bll.Model.RelationType = 2;
                _bll.Model.RelationTableName = "MCS_CM.dbo.CM_Client";
                _bll.Model.RelationTextField = "FullName";
                _bll.Model.RelationValueField = "ID";
                break;
            case "BeginDate":
                _bll.Model.ParamName = "@BeginDate";
                _bll.Model.DisplayName = "开始日期";
                _bll.Model.DataType = 4;
                _bll.Model.ControlType = 2;                
                _bll.Model["ControlWidth"] = "70";
                _bll.Model.RelationType = 1;
                _bll.Model.DefaultValue = "$ThisMonthFirstDay$";
                break;
            case "EndDate":
                _bll.Model.ParamName = "@EndDate";
                _bll.Model.DisplayName = "截止日期";
                _bll.Model.DataType = 4;
                _bll.Model.ControlType = 2;
                _bll.Model["ControlWidth"] = "70";
                _bll.Model.RelationType = 1;
                _bll.Model.DefaultValue = "$Today$";
                break;
            default:
                break;
        }

        if (_bll.Add() == 0)
        {
            MessageBox.ShowAndRedirect(this, "新增成功!", "Rpt_DataSetParamsDetail.aspx?ID=" + _bll.Model.ID.ToString());
        }
    }
}