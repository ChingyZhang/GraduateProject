// ===================================================================
// 文件路径:SubModule/Reports/Rpt_DataSetFieldsDetail.aspx.cs 
// 生成日期:2010/9/30 13:21:14 
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
using MCSFramework.Model;
public partial class SubModule_Reports_Rpt_DataSetFieldsDetail : System.Web.UI.Page
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

            #region 获取字段对应的数据集ID
            if ((Guid)ViewState["ID"] != Guid.Empty)
            {
                Rpt_DataSetFieldsBLL _bll = new Rpt_DataSetFieldsBLL((Guid)ViewState["ID"]);
                ViewState["DataSet"] = _bll.Model.DataSet;
            }

            if ((Guid)ViewState["DataSet"] == Guid.Empty)
            {
                Response.Redirect("Rpt_DataSetList.aspx");
            }

            Rpt_DataSet dataset = new Rpt_DataSetBLL((Guid)ViewState["DataSet"]).Model;
            ViewState["DataSet_CommandType"] = dataset.CommandType;
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

                ddl_IsComputeField.SelectedValue = "Y";
                ddl_IsComputeField.Enabled = false;
                ddl_IsComputeField_SelectedIndexChanged(null, null);

                int maxsortid = 0;
                IList<Rpt_DataSetFields> fields = new Rpt_DataSetBLL((Guid)ViewState["DataSet"]).GetFields();
                if (fields.Count > 0) maxsortid = fields.Max(p => p.ColumnSortID);
                tbx_SortID.Text = (++maxsortid).ToString();

                tbx_FieldName.Visible = true;
                ddl_DisplayMode.Enabled = false;
                tbx_TreeLevel.Enabled = false;
                bt_Delete.Visible = false;
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_DataType.DataSource = DictionaryBLL.GetDicCollections("UD_DataType");
        ddl_DataType.DataBind();

        ddl_DisplayMode.DataSource = DictionaryBLL.GetDicCollections("UD_DisplayMode");
        ddl_DisplayMode.DataBind();
    }

    protected void ddl_IsComputeField_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool _iscompute = ddl_IsComputeField.SelectedValue == "Y";

        hy_ExpressionHelp.Visible = _iscompute;
        tbx_Expression.Enabled = _iscompute;
        tbx_FieldName.Enabled = _iscompute;
        if ((int)ViewState["DataSet_CommandType"] == 3)
        {
            hy_ExpressionHelp.Text = "该表达式将在数据库中执行，请按SQL语法编写";
            hy_ExpressionHelp.NavigateUrl = "";
        }
        else
        {
            hy_ExpressionHelp.Text = "该表达式将在DataTable中计算，点击查看帮助!";
            hy_ExpressionHelp.NavigateUrl = "DataTableExpressionHelp.htm";
        }
        ddl_DataType.Enabled = _iscompute;
        ddl_DisplayMode.Enabled = !_iscompute;
        tbx_TreeLevel.Enabled = !_iscompute;
    }

    #endregion

    private void BindData()
    {
        Rpt_DataSetFieldsBLL _bll = new Rpt_DataSetFieldsBLL((Guid)ViewState["ID"]);

        ViewState["DataSet"] = _bll.Model.DataSet;

        ddl_IsComputeField.SelectedValue = _bll.Model.IsComputeField;
        ddl_IsComputeField_SelectedIndexChanged(null, null);
        tbx_Expression.Text = _bll.Model.Expression;

        if (_bll.Model.FieldID != Guid.Empty)
        {
            #region 设置关联字段界面控件属性
            UD_ModelFields field = new UD_ModelFieldsBLL(_bll.Model.FieldID).Model;
            if (field == null) return;

            if (field.RelationType == 1 || field.RelationType == 2)
            {
                ddl_DisplayMode.Enabled = true;

                //如果关联表是树形结构，则允许设定树表层次
                if (field.RelationType == 2 && new UD_TableListBLL(field.RelationTableName).Model.TreeFlag == "Y")
                {
                    tbx_TreeLevel.Enabled = true;
                    if (_bll.Model.TreeLevel == 0 && (field.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity" ||
                        field.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity"))
                    {
                        bt_Expand.Visible = true;  //如果字段关联于管理片区或行政城市，显示"展开层级"按钮
                    }
                }
                else
                {
                    tbx_TreeLevel.Text = "";
                    tbx_TreeLevel.Enabled = false;
                }
            }
            else
            {
                ddl_DisplayMode.Enabled = false;
                ddl_DisplayMode.SelectedValue = "1";
                tbx_TreeLevel.Text = "";
                tbx_TreeLevel.Enabled = false;
            }
            #endregion
        }

        tbx_FieldName.Text = _bll.Model.FieldName;
        tbx_DisplayName.Text = _bll.Model.DisplayName;
        ddl_DataType.SelectedValue = _bll.Model.DataType.ToString();
        tbx_Description.Text = _bll.Model.Description;

        tbx_SortID.Text = _bll.Model.ColumnSortID.ToString();
        tbx_TreeLevel.Text = _bll.Model.TreeLevel.ToString();
        ddl_DisplayMode.SelectedValue = _bll.Model.DisplayMode.ToString();

        tbx_FieldName.Enabled = false;
        ddl_IsComputeField.Enabled = false;


        bt_OK.Text = "修 改";
        bt_OK.ForeColor = System.Drawing.Color.Red;

        if ((int)ViewState["DataSet_CommandType"] == 1 || (int)ViewState["DataSet_CommandType"] == 2)
        {
            tbx_FieldName.Visible = true;

            ddl_DisplayMode.Enabled = false;
            tbx_TreeLevel.Enabled = false;

            if (_bll.Model.IsComputeField != "Y") bt_Delete.Visible = false;
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Rpt_DataSetFieldsBLL _bll;

        if ((Guid)ViewState["ID"] != Guid.Empty)
            _bll = new Rpt_DataSetFieldsBLL((Guid)ViewState["ID"]);
        else
            _bll = new Rpt_DataSetFieldsBLL();

        _bll.Model.DataSet = (Guid)ViewState["DataSet"];

        _bll.Model.FieldName = tbx_FieldName.Text;
        _bll.Model.DisplayName = tbx_DisplayName.Text;
        _bll.Model.DataType = int.Parse(ddl_DataType.SelectedValue);
        _bll.Model.Description = tbx_Description.Text;

        if (ddl_IsComputeField.SelectedValue == "Y")
        {
            _bll.Model.IsComputeField = "Y";
            _bll.Model.Expression = tbx_Expression.Text.Trim();
            if (_bll.Model.Expression == "")
            {
                tbx_Expression.Focus();
                MessageBox.Show(this, "请正确录入计算列的表达式!");
                return;
            }
        }
        else
        {
            _bll.Model.IsComputeField = "N";
            _bll.Model.Expression = "";
        }

        if (tbx_SortID.Text != "") _bll.Model.ColumnSortID = int.Parse(tbx_SortID.Text);
        _bll.Model.DisplayMode = int.Parse(ddl_DisplayMode.SelectedValue);

        #region 树形字段有指定层次时，重设字段名
        //要与查询条件生成的字段要一致，以便支持多同一树形字段多列显示
        if (tbx_TreeLevel.Enabled)
        {
            UD_ModelFields f = new UD_ModelFieldsBLL(_bll.Model.FieldID).Model;

            if (new UD_TableListBLL(f.RelationTableName).Model.TreeFlag == "Y")
            {

                int level = 0;
                int.TryParse(tbx_TreeLevel.Text, out level);
                _bll.Model.TreeLevel = level;

                UD_TableList t = new UD_TableListBLL(f.TableID).Model;
                if (level > 0)
                    _bll.Model.FieldName = t.ModelClassName + "_" + f.FieldName + level.ToString();
                else
                    _bll.Model.FieldName = t.ModelClassName + "_" + f.FieldName;
            }
        }
        #endregion

        if (Rpt_DataSetFieldsBLL.GetModelList("DataSet='" + _bll.Model.DataSet.ToString() + "' AND FieldName='" +
               _bll.Model.FieldName + "' AND ID <>'" + _bll.Model.ID.ToString() + "'").Count > 0)
        {
            MessageBox.Show(this, "对不起，字段名已重复，请更改字段名称!");
            return;
        }

        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            _bll.Update();
        }
        else
        {
            _bll.Add();
        }
        new Rpt_DataSetBLL((Guid)ViewState["DataSet"]).ClearCache();

        Response.Redirect("Rpt_DataSetFieldsList.aspx?ID=" + ViewState["DataSet"].ToString());
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            new Rpt_DataSetFieldsBLL((Guid)ViewState["ID"]).Delete();
            Response.Redirect("Rpt_DataSetFieldsList.aspx?ID=" + ViewState["DataSet"].ToString());
        }
    }
    protected void bt_Expand_Click(object sender, EventArgs e)
    {
        Rpt_DataSetFields m = new Rpt_DataSetFieldsBLL((Guid)ViewState["ID"]).Model;
        if (m == null || m.TreeLevel != 0) return;

        UD_ModelFields field = new UD_ModelFieldsBLL(m.FieldID).Model;

        if (field == null) return;

        //如果关联表是树形结构，则允许设定树表层次
        if (field.RelationType == 2 && new UD_TableListBLL(field.RelationTableName).Model.TreeFlag == "Y")
        {
            int maxsortid = 0;
            IList<Rpt_DataSetFields> datasetfields = new Rpt_DataSetBLL((Guid)ViewState["DataSet"]).GetFields();
            if (datasetfields.Count > 0) maxsortid = datasetfields.Max(p => p.ColumnSortID);

            Dictionary<string, Dictionary_Data> levels;
            if (field.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                levels = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel");//关联至管理片区
            else if (field.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
                levels = DictionaryBLL.GetDicCollections("Addr_OfficialCityLevel");  //关联至行政城市
            else
                return;

            foreach (string level in levels.Keys)
            {
                string fieldname = m.FieldName + level;
                if (datasetfields.FirstOrDefault(p => p.FieldName == fieldname) == null)
                {
                    maxsortid++;

                    Rpt_DataSetFieldsBLL fieldbll = new Rpt_DataSetFieldsBLL();
                    fieldbll.Model.DataSet = (Guid)ViewState["DataSet"];
                    fieldbll.Model.FieldID = field.ID;
                    fieldbll.Model.FieldName = fieldname;
                    fieldbll.Model.DisplayName = levels[level].Name;
                    fieldbll.Model.DataType = 3;                                  //固定为字符串型
                    fieldbll.Model.IsComputeField = "N";
                    fieldbll.Model.ColumnSortID = maxsortid;
                    fieldbll.Model.DisplayMode = 2;
                    fieldbll.Model.TreeLevel = int.Parse(level);
                    fieldbll.Model.Description = field.Description;
                    fieldbll.Add();
                }
            }
            MessageBox.ShowAndRedirect(this, "展开级别成功!", "Rpt_DataSetFieldsList.aspx?ID=" + ViewState["DataSet"].ToString());
        }
    }
}