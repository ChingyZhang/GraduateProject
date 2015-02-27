// ===================================================================
// 文件路径:SubModule/Reports/Rpt_DataSetFieldsList.aspx.cs 
// 生成日期:2010/9/30 13:21:14 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model.RPT;
using MCSFramework.BLL.RPT;
using MCSFramework.Model;

public partial class SubModule_Reports_Rpt_DataSetFieldsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
            #endregion
            BindDropDown();

            if ((Guid)ViewState["ID"] != Guid.Empty)
            {
                #region 绑定页面数据集信息
                Rpt_DataSet m = new Rpt_DataSetBLL((Guid)ViewState["ID"]).Model;
                if (m != null)
                {
                    lb_DataSetName.Text = m.Name;

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

                    if (m.CommandType == 3)
                        bt_Refresh.Visible = false;
                    else
                    {
                        td_TableFields.Visible = false;
                        gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
                    }
                }
                #endregion

                BindGrid();
            }
            else
                Response.Redirect("Rpt_DataSetList.aspx");
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_TableName.DataSource = UD_TableListBLL.GetModelList
            ("ID IN(SELECT TableID FROM MCS_Reports.dbo.Rpt_DataSetTables WHERE DataSet='" + ViewState["ID"].ToString() + "')");
        ddl_TableName.DataBind();
        if (ddl_TableName.Items.Count > 0)
            ddl_TableName_SelectedIndexChanged(null, null);
    }

    protected void ddl_TableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        IList<UD_ModelFields> fields = UD_ModelFieldsBLL.GetModelList("TableID='" + ddl_TableName.SelectedValue + "'");

        foreach (Rpt_DataSetFields f in new Rpt_DataSetBLL((Guid)ViewState["ID"]).GetFields())
        {
            UD_ModelFields field = fields.FirstOrDefault(p => p.ID == f.FieldID);
            if (field != null)
            {
                UD_TableList table = new UD_TableListBLL(field.RelationTableName).Model;
                if (field.RelationType == 2 && table != null && table.TreeFlag == "Y") continue;

                fields.Remove(field);
            }
        }

        cbxl_Fields.DataSource = fields;
        cbxl_Fields.DataBind();
    }

    protected void cb_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbxl_Fields.Items)
        {
            item.Selected = cb_CheckAll.Checked;
        }
    }
    #endregion

    protected void bt_AddToDataSet_Click(object sender, EventArgs e)
    {
        IList<Rpt_DataSetFields> datasetfields = new Rpt_DataSetBLL((Guid)ViewState["ID"]).GetFields();
        int maxsortid = 0;
        if (datasetfields.Count > 0) maxsortid = datasetfields.Max(p => p.ColumnSortID);

        foreach (ListItem item in cbxl_Fields.Items)
        {
            if (item.Selected)
            {
                Guid fieldid = new Guid(item.Value);

                UD_ModelFields f = new UD_ModelFieldsBLL(fieldid).Model;
                if (f != null)
                {
                    string fieldname = new UD_TableListBLL(f.TableID).Model.ModelClassName + "_" + f.FieldName;
                    if (datasetfields.FirstOrDefault(p => p.FieldName == fieldname) != null) continue;

                    maxsortid++;

                    Rpt_DataSetFieldsBLL fieldbll = new Rpt_DataSetFieldsBLL();
                    fieldbll.Model.DataSet = (Guid)ViewState["ID"];
                    fieldbll.Model.FieldID = f.ID;
                    fieldbll.Model.FieldName = fieldname;
                    fieldbll.Model.DisplayName = f.DisplayName;
                    fieldbll.Model.DataType = f.DataType;
                    fieldbll.Model.IsComputeField = "N";
                    fieldbll.Model.ColumnSortID = maxsortid;

                    if (f.RelationType == 1 || f.RelationType == 2)
                        fieldbll.Model.DisplayMode = 2;
                    else
                        fieldbll.Model.DisplayMode = 1;

                    fieldbll.Model.Description = f.Description;
                    fieldbll.Add();
                }
            }
        }

        BindGrid();
        new Rpt_DataSetBLL((Guid)ViewState["ID"]).ClearCache();
    }

    private void BindGrid()
    {
        string ConditionStr = " DataSet ='" + ViewState["ID"].ToString() + "' ";

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }


    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rpt_DataSetFieldsDetail.aspx?DataSet=" + ViewState["ID"].ToString());
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

    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
        Rpt_DataSetBLL bll = new Rpt_DataSetBLL((Guid)ViewState["ID"]);

        bll.ClearCache();

        #region 初始化参数
        Dictionary<string, object> prams = new Dictionary<string, object>();
        foreach (Rpt_DataSetParams p in bll.GetParams())
        {
            object value;
            switch (p.DataType)
            {
                case 1:         //整型(int)
                case 2:         //小数(decimal)
                case 7:         //位(bit)
                    value = 0;
                    break;
                case 3:         //字符串(varchar)
                case 6:         //字符串(nvarchar)
                case 8:         //ntext
                    value = "";
                    break;
                case 4:         //日期(datetime)
                    value = "1900-1-1";
                    break;
                case 5:         //GUID(uniqueidentifier)
                    value = Guid.Empty.ToString();
                    break;
                default:
                    value = "";
                    break;
            }


            prams.Add(p.ParamName, value);
        }
        #endregion

        DateTime t;
        DataTable dt = bll.GetData(prams, false, out t);

        IList<Rpt_DataSetFields> fields = bll.GetFields();

        #region 加入数据集中不包括的列
        foreach (DataColumn column in dt.Columns)
        {
            if (fields.FirstOrDefault(p => p.FieldName == column.ColumnName) == null)
            {
                Rpt_DataSetFieldsBLL field = new Rpt_DataSetFieldsBLL();

                field.Model.DataSet = (Guid)ViewState["ID"];
                field.Model.FieldName = column.ColumnName;
                field.Model.DisplayName = column.ColumnName;
                field.Model.ColumnSortID = column.Ordinal + 1;
                field.Model.DisplayMode = 1;
                field.Model.TreeLevel = 0;
                field.Model.IsComputeField = "N";

                #region 数据类型
                switch (column.DataType.FullName)
                {
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                        field.Model.DataType = 1;  //整形
                        break;
                    case "System.String":
                        field.Model.DataType = 3;  //字符串
                        break;
                    case "System.Decimal":
                        field.Model.DataType = 2;  //小数
                        break;
                    case "System.DateTime":
                        field.Model.DataType = 4;  //日期
                        break;
                    case "System.Guid":
                        field.Model.DataType = 5;  //GUID
                        break;
                    default:
                        field.Model.DataType = 3;  //字符串
                        break;
                }
                #endregion

                field.Add();
            }
        }
        #endregion


        #region 删除数据表中不存在的列
        foreach (Rpt_DataSetFields f in fields)
        {
            if (f.IsComputeField == "Y") continue;
            if (!dt.Columns.Contains(f.FieldName))
            {
                new Rpt_DataSetFieldsBLL(f.ID).Delete();
            }
        }
        #endregion

        gv_List.PageIndex = 0;
        BindGrid();
        MessageBox.Show(this, "数据集字段刷新成功!");
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = (Guid)gv_List.DataKeys[e.RowIndex][0];
        new Rpt_DataSetFieldsBLL(id).Delete();
        BindGrid();
        new Rpt_DataSetBLL((Guid)ViewState["ID"]).ClearCache();

        ddl_TableName_SelectedIndexChanged(null, null);
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            IList<Rpt_DataSetFields> fields = new Rpt_DataSetBLL((Guid)ViewState["ID"]).GetFields();
            if (fields.Count > 0)
            {
                Guid id = (Guid)gv_List.DataKeys[e.Row.RowIndex][0];

                if (id == fields[0].ID)
                    e.Row.FindControl("bt_Decrease").Visible = false;

                if (id == fields[fields.Count - 1].ID)
                    e.Row.FindControl("bt_Increase").Visible = false;
            }
        }
    }
    protected void bt_Increase_Click(object sender, EventArgs e)
    {
        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;

        Guid id = (Guid)gv_List.DataKeys[rowindex][0];

        Rpt_DataSetFieldsBLL bll = new Rpt_DataSetFieldsBLL(id);
        bll.Model.ColumnSortID++;
        bll.Update();

        IList<Rpt_DataSetFields> fields = new Rpt_DataSetBLL((Guid)ViewState["ID"]).GetFields();
        Rpt_DataSetFields next = fields.FirstOrDefault(p => p.ColumnSortID == bll.Model.ColumnSortID && p.ID != id);
        if (next != null)
        {
            bll = new Rpt_DataSetFieldsBLL(next.ID);
            if (bll.Model.ColumnSortID > 0) bll.Model.ColumnSortID--;
            bll.Update();
        }

        BindGrid();
    }
    protected void bt_Decrease_Click(object sender, EventArgs e)
    {
        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;

        Guid id = (Guid)gv_List.DataKeys[rowindex][0];

        Rpt_DataSetFieldsBLL bll = new Rpt_DataSetFieldsBLL(id);
        if (bll.Model.ColumnSortID > 0) bll.Model.ColumnSortID--;
        bll.Update();

        IList<Rpt_DataSetFields> fields = new Rpt_DataSetBLL((Guid)ViewState["ID"]).GetFields();
        Rpt_DataSetFields pre = fields.FirstOrDefault(p => p.ColumnSortID == bll.Model.ColumnSortID && p.ID != id);
        if (pre != null)
        {
            bll = new Rpt_DataSetFieldsBLL(pre.ID);
            if (bll.Model.ColumnSortID > 0) bll.Model.ColumnSortID++;
            bll.Update();
        }

        BindGrid();
    }
}