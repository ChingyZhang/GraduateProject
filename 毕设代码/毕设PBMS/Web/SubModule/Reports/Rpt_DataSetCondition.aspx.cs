using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model.RPT;
using MCSFramework.BLL.RPT;
using System.Data;
using MCSFramework.Model;

public partial class SubModule_Reports_Rpt_DataSetCondition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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

                    LoadCondition();
                }
                #endregion
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
        ddl_TableName_SelectedIndexChanged(null, null);

        ddl_Param.DataSource = new Rpt_DataSetBLL((Guid)ViewState["ID"]).GetParams();
        ddl_Param.DataBind();
        if (ddl_Param.Items.Count == 0)
        {
            rbl_ValueFrom.SelectedValue = "M";
            rbl_ValueFrom.Enabled = false;
        }
    }
    #endregion

    #region 载入高级查询条件
    /// <summary>
    /// 载入高级查询条件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LoadCondition()
    {
        lbx_search.Items.Clear();

        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            Rpt_DataSet dataset = new Rpt_DataSetBLL((Guid)ViewState["ID"]).Model;

            if (dataset != null && !string.IsNullOrEmpty(dataset.ConditionText))
            {
                string[] charSeparators = new string[] { "||" };
                string[] _texts = dataset.ConditionText.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                string[] _values = dataset.ConditionValue.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < _texts.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_texts[i]))
                    {
                        lbx_search.Items.Add(new ListItem(_texts[i], _values[i]));
                    }
                }
            }
        }
    }
    #endregion

    #region 保存高级查询条件
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            Rpt_DataSetBLL datasetbll = new Rpt_DataSetBLL((Guid)ViewState["ID"]);

            string _text = "";
            string _value = "";

            foreach (ListItem lt in lbx_search.Items)
            {
                if (lt.Value != "")
                {
                    _text += lt.Text + "||";
                    _value += lt.Value + "||";
                }
            }
            datasetbll.Model.ConditionText = _text;
            datasetbll.Model.ConditionValue = _value;
            datasetbll.Model.ConditionSQL = GenarateSQL();
            datasetbll.Update();

            new Rpt_DataSetBLL((Guid)ViewState["ID"]).ClearCache();
            MessageBox.Show(this.Page, "高级条件保存成功！");
        }

    }
    #endregion

    #region 创建高级查询条件操作
    protected void ddl_TableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Field.DataSource = UD_ModelFieldsBLL.GetModelList("TableID='" + ddl_TableName.SelectedValue + "'");
        ddl_Field.DataBind();

        ddl_Field.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));
    }
    protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_op.Items.Clear();
        ddl_op.Visible = false;
        ddl_TreeLevel.Visible = false;

        #region 操作符选择
        UD_ModelFields modelfield = new UD_ModelFieldsBLL(new Guid(ddl_Field.SelectedValue), true).Model;
        if (modelfield == null)
        {
            btn_addsearch.Enabled = false;
            return;
        }
        btn_addsearch.Enabled = true;

        switch (modelfield.RelationType)
        {
            case 1:
                //字典关联
                break;
            case 2:
                //实体表关联
                #region 如果是管理片区或行政城市，可以选择指定的树形级别
                if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                {
                    ddl_TreeLevel.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel");
                    ddl_TreeLevel.DataBind();
                    ddl_TreeLevel.Items.Insert(0, new ListItem("当前级别", "0"));
                    ddl_TreeLevel.Items.Insert(1, new ListItem("所属于", "100"));
                    ddl_TreeLevel.Visible = true;
                }
                else if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
                {
                    ddl_TreeLevel.DataSource = DictionaryBLL.GetDicCollections("Addr_OfficialCityLevel");
                    ddl_TreeLevel.DataBind();
                    ddl_TreeLevel.Items.Insert(0, new ListItem("当前级别", "0"));
                    ddl_TreeLevel.Items.Insert(1, new ListItem("所属于", "100"));
                    ddl_TreeLevel.Visible = true;
                }
                #endregion
                break;
            default:
                //非关联字段
                #region 设置操作符
                switch (modelfield.DataType)
                {
                    case 1:     //整型(int)
                    case 2:     //小数(decimal)
                        ddl_op.Items.Add(new ListItem("等于", "="));
                        ddl_op.Items.Add(new ListItem("大于", ">"));
                        ddl_op.Items.Add(new ListItem("大于等于", ">="));
                        ddl_op.Items.Add(new ListItem("小于", "<"));
                        ddl_op.Items.Add(new ListItem("小于等于", "<="));
                        ddl_op.Items.Add(new ListItem("不等于", "<>"));
                        break;
                    case 3:     //字符串(varchar)
                    case 6:     //字符串(nvarchar)
                    case 8:     //ntext
                        ddl_op.Items.Add(new ListItem("等于", "="));
                        ddl_op.Items.Add(new ListItem("相似", "like"));
                        break;
                    case 4:     //日期(datetime)
                        ddl_op.Items.Add(new ListItem("等于", "="));
                        ddl_op.Items.Add(new ListItem("大于", ">"));
                        ddl_op.Items.Add(new ListItem("大于等于", ">="));
                        ddl_op.Items.Add(new ListItem("小于", "<"));
                        ddl_op.Items.Add(new ListItem("小于等于", "<="));
                        ddl_op.Visible = true;

                        tbx_searchvalue.Attributes["onfocus"] = "WdatePicker()";
                        break;
                    case 5:     //GUID(uniqueidentifier)
                    case 7:     //bit
                        ddl_op.Items.Add(new ListItem("等于", "="));
                        ddl_op.Items.Add(new ListItem("不等于", "<>"));
                        break;
                }
                ddl_op.SelectedIndex = 0;
                ddl_op.Visible = true;
                #endregion
                break;
        }
        #endregion

        rbl_ValueFrom_SelectedIndexChanged(null, null);
    }

    protected void rbl_ValueFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_searchvalue.Text = "";
        tbx_searchvalue.Attributes["onfocus"] = "";
        cbl_SearchValue.Items.Clear();

        tbx_searchvalue.Visible = false;
        cbl_SearchValue.Visible = false;
        MCSSelectControl1.Visible = false;
        MCSTreeControl1.Visible = false;
        ddl_Param.Visible = false;

        if (rbl_ValueFrom.SelectedValue == "M")
        {
            #region 条件选择
            UD_ModelFields modelfield = new UD_ModelFieldsBLL(new Guid(ddl_Field.SelectedValue), true).Model;
            if (modelfield == null) return;

            switch (modelfield.RelationType)
            {
                case 1:
                    //字典关联
                    cbl_SearchValue.DataTextField = "Value";
                    cbl_SearchValue.DataValueField = "Key";
                    cbl_SearchValue.DataSource = DictionaryBLL.GetDicCollections(modelfield.RelationTableName);
                    cbl_SearchValue.DataBind();
                    cbl_SearchValue.Visible = true;
                    break;
                case 2:
                    //实体表关联
                    if (!string.IsNullOrEmpty(modelfield.SearchPageURL))
                    {
                        //通过查询控件查询
                        MCSSelectControl1.PageUrl = modelfield.SearchPageURL;
                        MCSSelectControl1.Visible = true;
                    }
                    else if (new UD_TableListBLL(modelfield.RelationTableName).Model.TreeFlag == "Y")
                    {
                        MCSTreeControl1.DataSource = null;
                        if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                        {
                            #region 如果为管理片区字段，则取员工所能管辖的片区
                            Org_StaffBLL staff = new Org_StaffBLL((int)System.Web.HttpContext.Current.Session["UserID"]);
                            MCSTreeControl1.DataSource = staff.GetStaffOrganizeCity();

                            if (MCSTreeControl1.DataSource.Select("ID = 1").Length > 0 || staff.Model.OrganizeCity == 0)
                            {
                                MCSTreeControl1.RootValue = "0";
                                MCSTreeControl1.SelectValue = "0";
                            }
                            else
                            {
                                MCSTreeControl1.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
                                MCSTreeControl1.SelectValue = staff.Model.OrganizeCity.ToString();
                            }

                            #endregion
                        }
                        else
                        {
                            //通过树形结构查询
                            MCSTreeControl1.TableName = modelfield.RelationTableName;
                            MCSTreeControl1.RootValue = "0";
                            MCSTreeControl1.SelectValue = "0";
                        }
                        MCSTreeControl1.DataBind();
                        MCSTreeControl1.Visible = true;
                    }
                    else
                    {
                        //其他关联方式的实体表
                        cbl_SearchValue.DataTextField = "Value";
                        cbl_SearchValue.DataValueField = "Key";
                        cbl_SearchValue.DataSource = TreeTableBLL.GetRelationTableSourceData(modelfield.RelationTableName, modelfield.RelationValueField, modelfield.RelationTextField);
                        cbl_SearchValue.DataBind();
                        cbl_SearchValue.Visible = true;
                    }
                    break;
                default:
                    //非关联字段
                    tbx_searchvalue.Visible = true;
                    break;
            }

            #endregion
        }
        else
        {
            ddl_Param.Visible = true;
        }
    }

    protected void btn_addsearch_Click(object sender, EventArgs e)
    {
        if (ddl_Field.SelectedValue == "0") return;
        //添加条件
        ListItem lt = new ListItem();

        #region 条件选择
        UD_TableList table = new UD_TableListBLL(new Guid(ddl_TableName.SelectedValue), true).Model;
        if (table == null) return;

        UD_ModelFields modelfield = new UD_ModelFieldsBLL(new Guid(ddl_Field.SelectedValue), true).Model;
        if (modelfield == null) return;

        //字段全称
        string fielddisplayname = table.DisplayName + "." + modelfield.DisplayName;
        string fieldfullname = "";

        if (modelfield.Flag == "Y")
            fieldfullname = table.Name + "." + modelfield.FieldName;        //实体字段
        else
            fieldfullname = "MCS_SYS.dbo.UF_Spilt(" + table.Name + ".ExtPropertys,'|'," + modelfield.Position.ToString() + ")";

        if (rbl_ValueFrom.SelectedValue == "M")
        {
            #region 从手工设定中取值
            switch (modelfield.RelationType)
            {
                case 1:
                    #region 字典关联
                    if (cbl_SearchValue.SelectedIndex != -1)
                    {
                        lt.Text = fielddisplayname + " 包含 ( ";
                        lt.Value = fieldfullname + " IN ( ";
                        foreach (ListItem item in cbl_SearchValue.Items)
                        {
                            if (item.Selected)
                            {
                                lt.Text += item.Text + ",";
                                lt.Value += "'" + item.Value + "',";
                            }
                        }
                        lt.Text = lt.Text.Substring(0, lt.Text.Length - 1);
                        lt.Value = lt.Value.Substring(0, lt.Value.Length - 1);
                        lt.Text += ")";
                        lt.Value += ")";
                    }
                    break;
                    #endregion
                case 2:
                    #region 实体表关联
                    lt.Text = fielddisplayname + " 为  ";
                    lt.Value = fieldfullname + " =  ";
                    if (!string.IsNullOrEmpty(modelfield.SearchPageURL))
                    {
                        //通过查询控件查询
                        lt.Text += MCSSelectControl1.SelectText;
                        lt.Value += "'" + MCSSelectControl1.SelectValue + "'";
                    }
                    else if (new UD_TableListBLL(modelfield.RelationTableName).Model.TreeFlag == "Y")
                    {
                        #region 通过树形结构查询
                        if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                        {
                            if (ddl_TreeLevel.SelectedValue == "0")
                            {
                                lt.Text += MCSTreeControl1.SelectText;
                                lt.Value = fieldfullname + " = " + MCSTreeControl1.SelectValue;
                            }
                            else if (ddl_TreeLevel.SelectedValue == "100")
                            {
                                //所属于
                                lt.Text = fielddisplayname + " " + ddl_TreeLevel.SelectedItem.Text + " " + MCSTreeControl1.SelectText;
                                lt.Value = "MCS_SYS.dbo.UF_IsChildOrganizeCity(" + MCSTreeControl1.SelectValue + "," + fieldfullname + ") = 0";
                            }
                            else
                            {
                                //指定级别上级
                                lt.Text = fielddisplayname + " 的 " + ddl_TreeLevel.SelectedItem.Text + " 为 " + MCSTreeControl1.SelectText;
                                lt.Value = "MCS_SYS.dbo.UF_GetSuperOrganizeCityByLevel02(" + fieldfullname + "," + ddl_TreeLevel.SelectedValue + ") = " + MCSTreeControl1.SelectValue;
                            }
                        }
                        else if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
                        {
                            if (ddl_TreeLevel.SelectedValue == "0")
                            {
                                lt.Text += MCSTreeControl1.SelectText;
                                lt.Value = fieldfullname + " = " + MCSTreeControl1.SelectValue;
                            }
                            else if (ddl_TreeLevel.SelectedValue == "100")
                            {
                                //所属于
                                lt.Text = fielddisplayname + " " + ddl_TreeLevel.SelectedItem.Text + " " + MCSTreeControl1.SelectText;
                                lt.Value = "MCS_SYS.dbo.UF_IsChildOfficialCityCity(" + MCSTreeControl1.SelectValue + "," + fieldfullname + ") = 0";
                            }
                            else
                            {
                                //指定级别上级
                                lt.Text = fielddisplayname + " 的 " + ddl_TreeLevel.SelectedItem.Text + " 为 " + MCSTreeControl1.SelectText;
                                lt.Value = "MCS_SYS.dbo.UF_GetSuperOfficialCityByLevel02(" + fieldfullname + "," + ddl_TreeLevel.SelectedValue + ") = " + MCSTreeControl1.SelectValue;
                            }
                        }
                        else
                        {
                            lt.Text += MCSTreeControl1.SelectText;
                            lt.Value = fieldfullname + " IN  (";
                            DataTable dt = TreeTableBLL.GetAllChildByNodes(modelfield.RelationTableName, "ID", "SuperID", MCSTreeControl1.SelectValue);
                            foreach (DataRow dr in dt.Rows)
                            {
                                lt.Value += "'" + dr[0].ToString() + "',";
                            }
                            lt.Value += "'" + MCSTreeControl1.SelectValue + "')";
                        }
                        #endregion
                    }
                    else
                    {
                        #region 其他关联方式的实体表
                        if (cbl_SearchValue.SelectedIndex != -1)
                        {
                            lt.Text = fielddisplayname + " 包含 ( ";
                            lt.Value = fieldfullname + " IN ( ";
                            foreach (ListItem item in cbl_SearchValue.Items)
                            {
                                if (item.Selected)
                                {
                                    lt.Text += item.Text + ",";
                                    lt.Value += "'" + item.Value + "',";
                                }
                            }
                            lt.Text = lt.Text.Substring(0, lt.Text.Length - 1);
                            lt.Value = lt.Value.Substring(0, lt.Value.Length - 1);
                            lt.Text += ")";
                            lt.Value += ")";
                        }
                        #endregion
                    }
                    break;
                    #endregion
                default:
                    #region 非关联字段
                    lt.Text = fielddisplayname + " " + ddl_op.SelectedItem.Text + " (" + tbx_searchvalue.Text + ")";
                    lt.Value = fieldfullname + " =  ";
                    if (ddl_op.SelectedValue != "like")
                    {
                        if (tbx_searchvalue.Text.StartsWith("@"))
                            lt.Value = fieldfullname + " " + ddl_op.SelectedItem.Value + tbx_searchvalue.Text;
                        else
                            lt.Value = fieldfullname + " " + ddl_op.SelectedItem.Value + " '" + tbx_searchvalue.Text + "'";
                    }
                    else
                        lt.Value = fieldfullname + " " + ddl_op.SelectedItem.Value + " '%" + tbx_searchvalue.Text + "%'";
                    break;
                    #endregion
            }
            #endregion
        }
        else
        {
            #region 从参数中取值
            switch (modelfield.RelationType)
            {
                case 1:
                    #region 字典关联
                    lt.Text = fielddisplayname + " = " + ddl_Param.SelectedValue;
                    lt.Value = fieldfullname + " = " + ddl_Param.SelectedValue;
                    #endregion
                    break;
                case 2:
                    #region 实体表关联
                    lt.Text = fielddisplayname + " 为  ";
                    lt.Value = fieldfullname + " =  ";
                    if (ddl_TreeLevel.Visible)
                    {
                        #region 管理片区及行政城市选项 
                        if (ddl_TreeLevel.SelectedValue == "0")
                        {
                            //当前级
                            lt.Text += ddl_Param.SelectedValue;
                            lt.Value = fieldfullname + " = " + ddl_Param.SelectedValue;
                        }
                        else if (ddl_TreeLevel.SelectedValue == "100")
                        {
                            //所属于
                            lt.Text = fielddisplayname + " " + ddl_TreeLevel.SelectedItem.Text + " " + ddl_Param.SelectedValue;

                            if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                            {
                                lt.Value = "MCS_SYS.dbo.UF_IsChildOrganizeCity(" + ddl_Param.SelectedValue + "," + fieldfullname + ") = 0";
                            }
                            else if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
                            {
                                lt.Value = "MCS_SYS.dbo.UF_IsChildOfficialCityCity(" + ddl_Param.SelectedValue + "," + fieldfullname + ") = 0";
                            }
                        }
                        else
                        {
                            lt.Text = fielddisplayname + " 的 " + ddl_TreeLevel.SelectedItem.Text + " 为 " + ddl_Param.SelectedValue;

                            //指定级别上级
                            if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                            {
                                lt.Value = "MCS_SYS.dbo.UF_GetSuperOrganizeCityByLevel02(" + fieldfullname + "," + ddl_TreeLevel.SelectedValue + ") = " + ddl_Param.SelectedValue;
                            }
                            else if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
                            {
                                lt.Value = "MCS_SYS.dbo.UF_GetSuperOfficialCityByLevel02(" + fieldfullname + "," + ddl_TreeLevel.SelectedValue + ") = " + ddl_Param.SelectedValue;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        lt.Text += ddl_Param.SelectedValue;
                        lt.Value = fieldfullname + " = " + ddl_Param.SelectedValue;
                    }
                    break;
                    #endregion
                default:
                    #region 非关联字段
                    lt.Text = fielddisplayname + " " + ddl_op.SelectedItem.Text + " (" + ddl_Param.SelectedValue + ")";
                    lt.Value = fieldfullname + " =  ";
                    if (ddl_op.SelectedValue != "like")
                        lt.Value = fieldfullname + " " + ddl_op.SelectedItem.Value + ddl_Param.SelectedValue;
                    else
                        lt.Value = fieldfullname + " LIKE '%'+" + ddl_Param.SelectedValue + "+'%'";
                    break;
                    #endregion
            }
            #endregion

        }
        #endregion

        if (!string.IsNullOrEmpty(lt.Text)) lbx_search.Items.Add(lt);
    }
    protected void btn_Del_Click(object sender, EventArgs e)
    {
        //删除条件
        foreach (ListItem lt in lbx_search.Items)
        {
            if (lt.Selected)
            {
                try
                {
                    lbx_search.Items.Remove(lt);
                }
                catch { }
                break;
            }
        }
    }
    private string GenarateSQL()
    {
        string searchstring = "";
        if (lbx_search.Items.Count > 0)
        {
            foreach (ListItem lt in lbx_search.Items)
            {
                if (lt.Value != "")
                {
                    searchstring += lt.Value + " AND ";
                }
            }

            if (searchstring.EndsWith("AND "))
            {
                searchstring = searchstring.Substring(0, searchstring.Length - 4);
            }
        }
        return searchstring;
    }
    #endregion

    protected void lbx_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lbx_search.SelectedItem != null)
        {
            tbx_CustomConditionName.Text = lbx_search.SelectedItem.Text;
            tbx_CustomConditionSQL.Text = lbx_search.SelectedItem.Value;
        }
    }
    protected void bt_Replace_Click(object sender, EventArgs e)
    {
        if (lbx_search.SelectedItem != null)
        {
            lbx_search.SelectedItem.Text = tbx_CustomConditionName.Text;
            lbx_search.SelectedItem.Value = tbx_CustomConditionSQL.Text;
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



}
