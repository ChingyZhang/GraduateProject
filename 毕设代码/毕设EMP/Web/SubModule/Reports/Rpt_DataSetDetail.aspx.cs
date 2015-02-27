// ===================================================================
// 文件路径:SubModule/Reports/Rpt_DataSetDetail.aspx.cs 
// 生成日期:2010/9/30 12:37:31 
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

public partial class SubModule_Reports_Rpt_DataSetDetail : System.Web.UI.Page
{
    DropDownList ddl_CommandType;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        ddl_CommandType = (DropDownList)pl_detail.FindControl("Rpt_DataSet_CommandType");
        if (ddl_CommandType != null)
        {
            ddl_CommandType.AutoPostBack = true;
            ddl_CommandType.SelectedIndexChanged += new EventHandler(ddl_CommandType_SelectedIndexChanged);
        }


        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
            #endregion

            BindDropDown();

            if ((Guid)ViewState["ID"] != Guid.Empty)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                pl_detail.SetPanelVisible("Panel_Rpt_DataSet_Detail_SQL", false);

                DropDownList ddl_Enabled = (DropDownList)pl_detail.FindControl("Rpt_DataSet_Enabled");
                if (ddl_Enabled != null) ddl_Enabled.SelectedValue = "Y";

                DropDownList ddl_IsParamDataSet = (DropDownList)pl_detail.FindControl("Rpt_DataSet_IsParamDataSet");
                if (ddl_IsParamDataSet != null) ddl_IsParamDataSet.SelectedValue = "N";

                MCSTabControl1.Items[1].Visible = false;
                MCSTabControl1.Items[2].Visible = false;
                MCSTabControl1.Items[3].Visible = false;
                MCSTabControl1.Items[4].Visible = false;
                MCSTabControl1.Items[5].Visible = false;
                bt_Delete.Visible = false;
            }
        }
    }


    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    void ddl_CommandType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pl_detail.SetPanelVisible("Panel_Rpt_DataSet_Detail_SQL", ddl_CommandType.SelectedValue != "3");
    }
    #endregion

    private void BindData()
    {
        Rpt_DataSet m = new Rpt_DataSetBLL((Guid)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);

            pl_detail.SetPanelVisible("Panel_Rpt_DataSet_Detail_SQL", m.CommandType != 3);

            #region 根据数据集类型控制Tab可见
            switch (m.CommandType)
            {
                case 1:
                case 2:
                    MCSTabControl1.Items[2].Visible = false;
                    MCSTabControl1.Items[3].Visible = false;
                    MCSTabControl1.Items[5].Visible = false;
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            #endregion

        }

    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Rpt_DataSetBLL _bll;
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            _bll = new Rpt_DataSetBLL((Guid)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new Rpt_DataSetBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.CommandType == 0)
        {
            MessageBox.Show(this, "语句类型必填!");
            return;
        }

        if (_bll.Model.DataSource == Guid.Empty)
        {
            MessageBox.Show(this, "数据源必填!");
            return;
        }
        #endregion

        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                _bll.ClearCache();
                MessageBox.ShowAndRedirect(this, "修改成功!", "Rpt_DataSetDetail.aspx?ID=" + _bll.Model.ID.ToString());
            }
        }
        else
        {
            //新增
            _bll.Model.InsertStaff = (int)Session["UserID"];

            if (_bll.Add() == 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "Rpt_DataSetDetail.aspx?ID=" + _bll.Model.ID.ToString());
            }
        }

    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0":
                Response.Redirect("Rpt_DataSetDetail.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "1":
                Response.Redirect("Rpt_DataSetParamsList.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "2":
                Response.Redirect("Rpt_DataSetTablesList.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "3":
                Response.Redirect("Rpt_DataSetTableRelationsList.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "4":
                Response.Redirect("Rpt_DataSetFieldsList.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "5":
                Response.Redirect("Rpt_DataSetCondition.aspx?ID=" + ViewState["ID"].ToString());
                break;
            default:
                break;
        }
    }
    protected void bt_ViewSQL_Click(object sender, EventArgs e)
    {
        Rpt_DataSetBLL m = new Rpt_DataSetBLL((Guid)ViewState["ID"]);

        if (m.Model.CommandType == 1 || m.Model.CommandType == 2)
        {
            lb_ViewSQL.Text = m.Model.CommandText;
        }
        else
        {
            if (m.GetFields().Count > 0)
            {
                lb_ViewSQL.Text = m.GetDataSetSQL();
            }
        }
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        new Rpt_DataSetBLL((Guid)ViewState["ID"]).Delete();
        MessageBox.ShowAndRedirect(this, "删除成功!", "Rpt_DataSetList.aspx");
    }
}