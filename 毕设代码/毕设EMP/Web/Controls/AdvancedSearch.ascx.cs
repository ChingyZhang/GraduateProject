using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.SQLDAL;
using System.Data;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.Common;

public partial class Controls_AdvancedSearch : System.Web.UI.UserControl
{
    private string _panelcode = "";
    private AdvancedSearch_OnSelectedChangingEventHandler _onselected;
    private AdvancedSearch_OnTableNameControlSelectedIndexChanged _ontableselectedindexchanged;
    /// <summary>
    /// 要实现高级查询对应的已维护的Panel
    /// </summary>
    public string PanelCode
    {
        get { return _panelcode; }
        set
        {
            _panelcode = value;
            gv_List.PanelCode = value;
        }
    }

    /// <summary>
    /// 高级查询扩展条件
    /// </summary>
    public string ExtCondition
    {
        get
        {
            if (ViewState["ExtCondition"] == null)
                return "";
            else
                return (string)ViewState["ExtCondition"];
        }
        set
        {
            ViewState["ExtCondition"] = value;
        }
    }

    public string[] DataKeyNames
    {
        get { return gv_List.DataKeyNames; }
        set { gv_List.DataKeyNames = value; }
    }
    public event AdvancedSearch_OnSelectedChangingEventHandler SelectedChanging
    {
        add { _onselected += value; }
        remove { _onselected -= value; }
    }
    public event AdvancedSearch_OnTableNameControlSelectedIndexChanged TableNameControlSelectedIndexChanged
    {
        add { _ontableselectedindexchanged += value; }
        remove { _ontableselectedindexchanged -= value; }
    }

    protected void Page_OnInit(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            gv_List.PanelCode = PanelCode;
        }

        #region 注册脚本
        //注册修改客户状态按钮script
        string script = "function OpenNewAdvancedFind(panelid){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("~/SubModule/AdvanceFind/NewAdvancedFind.aspx")
            + "?PanelID='+panelid+'&tempid='+tempid, window, 'dialogWidth:400px;DialogHeight=300px;status:no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenNewAdvancedFind", script, true);

        #endregion
    }

    private void BindDropDown()
    {
        UD_PanelBLL panelbll = new UD_PanelBLL(PanelCode, true);
        if (panelbll.Model != null)
        {
            ViewState["Panel"] = panelbll.Model.ID;
            //获取当前Panel已保存的查询条件
            ddl_FindCondition.DataSource = ADFind_FindConditionBLL.GetMyADFind(PanelCode, (int)Session["UserID"]);
            ddl_FindCondition.DataBind();
            ddl_FindCondition.Items.Insert(0, new ListItem("请选择...", "0"));

            //获取当前Pannel中包括的数据表
            ddl_TableName.DataSource = UD_Panel_TableBLL.GetTableListByPanelID(panelbll.Model.ID);
            ddl_TableName.DataBind();
            ddl_TableName_SelectedIndexChanged(null, null);

            bt_SaveCondition.OnClientClick = "javascript:OpenNewAdvancedFind('" + panelbll.Model.ID.ToString() + "')";
        }
        else
        {
            MessageBox.Show(this.Page, "对不起,指定的PanelCode未在UD_Panel表中找到对应项!");
        }
    }

    #region 查询条件保存操作
    protected void bt_CreateNewCondition_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/AdvanceFind/FindCondition_Detail.aspx?Panel=" + ViewState["Panel"]);
    }
    protected void bt_ConditionList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/AdvanceFind/FindCondition_List.aspx?Panel=" + ViewState["Panel"]);
    }


    protected void bt_SaveCondition_Click(object sender, EventArgs e)
    {
        ADFind_FindConditionBLL _f = null;

        if (ddl_FindCondition.SelectedValue != "0")
        {
            _f = new ADFind_FindConditionBLL(Int32.Parse(ddl_FindCondition.SelectedValue));
        }
        else if (Session["AdvancedFindNewID"] != null)
        {
            _f = new ADFind_FindConditionBLL((int)Session["AdvancedFindNewID"]);
            Session["AdvancedFindNewID"] = null;

            ddl_FindCondition.DataSource = ADFind_FindConditionBLL.GetMyADFind(PanelCode, (int)Session["UserID"]);
            ddl_FindCondition.DataBind();
            ddl_FindCondition.Items.Insert(0, new ListItem("请选择...", "0"));
            ddl_FindCondition.SelectedValue = _f.Model.ID.ToString();
            bt_SaveCondition.OnClientClick = "";
        }

        if (_f != null)
        {
            string _text = "";
            string _value = "";

            foreach (ListItem lt in lbx_search.Items)
            {
                if (lt.Value != "")
                {
                    _text += lt.Text + "|";
                    _value += lt.Value + "|";
                }
            }
            _f.Model.ConditionText = _text;
            _f.Model.ConditionValue = _value;
            _f.Model.ConditionSQL = GenarateSQL();
            _f.Model.OpStaff = Int32.Parse(Session["UserID"].ToString());
            _f.Update();

            MessageBox.Show(this.Page, "高级条件保存成功！");
        }

    }

    /// <summary>
    /// 载入高级查询条件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_FindCondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbx_search.Items.Clear();
        if (ddl_FindCondition.SelectedValue != "0")
        {
            ADFind_FindCondition _findcondition = new ADFind_FindConditionBLL(Int32.Parse(ddl_FindCondition.SelectedValue)).Model;
            if (!string.IsNullOrEmpty(_findcondition.ConditionText))
            {
                char[] charSeparators = new char[] { '|' };
                string[] _texts = _findcondition.ConditionText.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                string[] _values = _findcondition.ConditionValue.Replace("'|'", "'#'").Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < _texts.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_texts[i]))
                    {
                        lbx_search.Items.Add(new ListItem(_texts[i], _values[i].Replace("'#'", "'|'")));
                    }
                }
            }

            bt_SaveCondition.Enabled = true;
            bt_SaveCondition.OnClientClick = "";
        }
        else
        {
            bt_SaveCondition.OnClientClick = "javascript:OpenNewAdvancedFind('" + ViewState["Panel"].ToString() + "')";
            //bt_SaveCondition.Enabled = false;
        }
    }
    #endregion


    #region 创建高级查询条件操作
    protected void ddl_TableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Field.DataSource = UD_ModelFieldsBLL.GetModelList("TableID='" + ddl_TableName.SelectedValue + "'");
        ddl_Field.DataBind();

        ddl_Field.Items.Insert(0, new ListItem("请选择...", "0"));
        if (_ontableselectedindexchanged != null) _ontableselectedindexchanged.Invoke(sender, e);
    }

    protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_searchvalue.Text = "";
        tbx_searchvalue.Attributes["onfocus"] = "";
        cbl_SearchValue.Items.Clear();
        ddl_op.Items.Clear();
        ddl_TreeLevel.Visible = false;

        tbx_searchvalue.Visible = false;
        cbl_SearchValue.Visible = false;
        MCSSelectControl1.Visible = false;
        MCSTreeControl1.Visible = false;

        #region 条件选择
        UD_ModelFields modelfield = null;
        try
        {
            modelfield = new UD_ModelFieldsBLL(new Guid(ddl_Field.SelectedValue), true).Model;
        }
        catch { }

        if (modelfield == null)
        {
            #region 外部自定义条件
            ddl_op.Items.Add(new ListItem("等于", "="));
            ddl_op.Items.Add(new ListItem("大于", ">"));
            ddl_op.Items.Add(new ListItem("大于等于", ">="));
            ddl_op.Items.Add(new ListItem("小于", "<"));
            ddl_op.Items.Add(new ListItem("小于等于", "<="));
            ddl_op.Items.Add(new ListItem("不等于", "<>"));
            tbx_searchvalue.Visible = true;
            #endregion
        }
        else
        {
            switch (modelfield.RelationType)
            {
                case 1:
                    #region 字典关联
                    cbl_SearchValue.DataTextField = "Value";
                    cbl_SearchValue.DataValueField = "Key";
                    cbl_SearchValue.DataSource = DictionaryBLL.GetDicCollections(modelfield.RelationTableName);
                    cbl_SearchValue.DataBind();
                    cbl_SearchValue.Visible = true;
                    ddl_op.Items.Insert(0, new ListItem("值", "SELECTITEM"));
                    break;
                    #endregion
                case 2:
                    #region 实体表关联
                    if (!string.IsNullOrEmpty(modelfield.SearchPageURL))
                    {
                        //通过查询控件查询
                        MCSSelectControl1.PageUrl = modelfield.SearchPageURL;
                        MCSSelectControl1.Visible = true;
                    }
                    else if (new UD_TableListBLL(modelfield.RelationTableName).Model.TreeFlag == "Y")
                    {
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
                            MCSTreeControl1.DataSource = null;
                            MCSTreeControl1.TableName = modelfield.RelationTableName;
                            MCSTreeControl1.RootValue = "0";
                            MCSTreeControl1.SelectValue = "0";
                        }
                        MCSTreeControl1.DataBind();
                        MCSTreeControl1.Visible = true;

                        #region 如果是管理片区或行政城市，可以选择指定的树形级别
                        if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                        {
                            ddl_TreeLevel.Items.Clear();
                            ddl_TreeLevel.Items.Insert(0, new ListItem("所属于", "100"));
                            ddl_TreeLevel.Items.Insert(1, new ListItem("当前值", "0"));
                            ddl_TreeLevel.Visible = true;
                        }
                        else if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
                        {
                            ddl_TreeLevel.Items.Clear();
                            ddl_TreeLevel.Items.Insert(0, new ListItem("所属于", "100"));
                            ddl_TreeLevel.Items.Insert(1, new ListItem("当前值", "0"));
                            ddl_TreeLevel.Visible = true;
                        }
                        #endregion
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
                    ddl_op.Items.Insert(0, new ListItem("值", "SELECTITEM"));
                    break;
                    #endregion
                default:
                    #region 非关联字段
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
                            ddl_op.Items.Add(new ListItem("起始于", "StartWith"));
                            ddl_op.Items.Add(new ListItem("不起始于", "NotStartWith"));
                            break;
                        case 4:     //日期(datetime)
                            ddl_op.Items.Add(new ListItem("等于", "="));
                            ddl_op.Items.Add(new ListItem("大于等于", ">="));
                            ddl_op.Items.Add(new ListItem("小于等于", "<="));
                            ddl_op.Visible = true;

                            tbx_searchvalue.Attributes["onfocus"] = "setday(this)";
                            break;
                        case 5:     //GUID(uniqueidentifier)
                        case 7:     //bit
                            ddl_op.Items.Add(new ListItem("等于", "="));
                            ddl_op.Items.Add(new ListItem("不等于", "<>"));
                            break;
                    }
                    tbx_searchvalue.Visible = true;
                    break;
                    #endregion
            }

            ddl_op.Items.Add(new ListItem("不为空", "NOTNULL"));
            ddl_op.Items.Add(new ListItem("为空", "ISNULL"));
        }
        #endregion
        ddl_op.SelectedIndex = 0;
    }
    protected void ddl_op_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_op.SelectedValue == "NOTNULL" || ddl_op.SelectedValue == "ISNULL")
        {
            tbx_searchvalue.Visible = false;
            cbl_SearchValue.Visible = false;
            ddl_TreeLevel.Visible = false;
            MCSSelectControl1.Visible = false;
            MCSTreeControl1.Visible = false;
        }
        else
        {
            #region 条件选择
            UD_ModelFields modelfield = null;
            try
            {
                modelfield = new UD_ModelFieldsBLL(new Guid(ddl_Field.SelectedValue), true).Model;
            }
            catch { }

            if (modelfield == null)
            {
                tbx_searchvalue.Visible = true;
                return;
            }
            else
            {
                switch (modelfield.RelationType)
                {
                    case 1:
                        //字典关联
                        cbl_SearchValue.Visible = true;
                        break;
                    case 2:
                        //实体表关联
                        if (!string.IsNullOrEmpty(modelfield.SearchPageURL))
                        {
                            //通过查询控件查询
                            MCSSelectControl1.Visible = true;
                        }
                        else if (new UD_TableListBLL(modelfield.RelationTableName).Model.TreeFlag == "Y")
                        {
                            MCSTreeControl1.Visible = true;
                            if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity" ||
                                modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
                            { ddl_TreeLevel.Visible = true; }
                        }
                        else
                        {
                            //其他关联方式的实体表
                            cbl_SearchValue.Visible = true;
                        }
                        break;
                    default:
                        //非关联字段
                        tbx_searchvalue.Visible = true;
                        break;
                }
            }
            #endregion
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

        UD_ModelFields modelfield = null;
        try
        {
            modelfield = new UD_ModelFieldsBLL(new Guid(ddl_Field.SelectedValue), true).Model;
        }
        catch { }

        if (modelfield == null)
        {
            //自定义字段
            lt.Text = ddl_Field.SelectedItem.Text + " " + ddl_op.SelectedItem.Text + " ('" + tbx_searchvalue.Text + "')";
            lt.Value = ddl_Field.SelectedValue + " " + ddl_op.SelectedItem.Value + " '" + tbx_searchvalue.Text + "'";
        }
        else
        {
            string fielddisplayname = table.DisplayName + "." + modelfield.DisplayName;
            string fieldfullname = "";

            if (modelfield.Flag == "Y")
                fieldfullname = table.Name + "." + modelfield.FieldName;        //实体字段
            else
                fieldfullname = "MCS_SYS.dbo.UF_Spilt(" + table.Name + ".ExtPropertys,'|'," + modelfield.Position.ToString() + ")";


            if (ddl_op.SelectedValue == "NOTNULL")
            {
                lt.Text = fielddisplayname + " 不为空";
                lt.Value = " ISNULL(" + fieldfullname + ",'')<>'' ";
                lbx_search.Items.Add(lt);
                return;
            }
            if (ddl_op.SelectedValue == "ISNULL")
            {
                lt.Text = fielddisplayname + " 为空";
                lt.Value = " ISNULL(" + fieldfullname + ",'')='' ";
                lbx_search.Items.Add(lt);
                return;
            }

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
                        if (modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity" || modelfield.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
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
                        
                                lt.Value = fieldfullname + " IN  (";
                                DataTable dt = TreeTableBLL.GetAllChildByNodes(modelfield.RelationTableName, "ID", "SuperID", MCSTreeControl1.SelectValue);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    lt.Value += "'" + dr[0].ToString() + "',";
                                }
                                lt.Value += "'" + MCSTreeControl1.SelectValue + "')";                               
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
                    lt.Text = fielddisplayname + " " + ddl_op.SelectedItem.Text + " ('" + tbx_searchvalue.Text + "')";
                    lt.Value = fieldfullname + " =  ";
                    if (ddl_op.SelectedValue == "StartWith")
                    {
                        lt.Value = fieldfullname + " " + "Like" + " '" + tbx_searchvalue.Text + "%'";
                    }
                    else if (ddl_op.SelectedValue == "NotStartWith")
                    {
                        lt.Value = fieldfullname + " " + "Not Like" + " '" + tbx_searchvalue.Text + "%'";
                    }
                    else if (ddl_op.SelectedValue != "like")
                    {
                        if (modelfield.DataType == 4)        //日期(datetime)
                        {
                            lt.Value = "CONVERT(DATETIME,CONVERT(VARCHAR,CONVERT(DATETIME," + fieldfullname + "),111))" + " " + ddl_op.SelectedItem.Value + " '" + tbx_searchvalue.Text + "'";
                        }
                        else
                        {
                            lt.Value = fieldfullname + " " + ddl_op.SelectedItem.Value + " '" + tbx_searchvalue.Text + "'";
                        }
                    }
                    else
                        lt.Value = fieldfullname + " " + ddl_op.SelectedItem.Value + " '%" + tbx_searchvalue.Text + "%'";
                    break;
                    #endregion
            }
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

    protected void btn_OK_Click(object sender, EventArgs e)
    {
        //查询
        ViewState["PageIndex"] = 0;
        cbl_SearchValue.Visible = false;
        tbx_searchvalue.Visible = false;
        MCSTreeControl1.Visible = false;
        MCSSelectControl1.Visible = false;

        ddl_Field.SelectedIndex = 0;

        BindGrid();
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


        if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 201, "ViewSelfClientByInfoCollectManID"))
        {
           searchstring += " And CU_Client.InfoCollectManID=" + Session["UserID"].ToString();
        }
        if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 201, "ViewSelefClientByINfoSource20"))  //仅查看来源于医务渠道的顾客
        {
           searchstring += " And CU_Client.InfoSource = 20";
        }
        #endregion
        return searchstring;
    }
 

    private void BindGrid()
    {
        gv_List.ConditionString = GenarateSQL();
        if (gv_List.ConditionString == "")
        {
            MessageBox.Show(this.Page, "请设定要查询的条件!");
            return;
        }

        if (ExtCondition != "")
        {
            if (gv_List.ConditionString != "") gv_List.ConditionString += " AND ";
            gv_List.ConditionString += ExtCondition;
        }
        gv_List.BindGrid();


    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        if (gv_List.ConditionString == "")
        {
            MessageBox.Show(this.Page, "请设定要查询的条件!");
            return;
        }

        gv_List.AllowPaging = false;
        gv_List.BindGrid();
        ToExcel(gv_List, "ExtportFile.xls");

        gv_List.AllowPaging = true;
        gv_List.BindGrid();
    }

    private void ToExcel(Control ctl, string FileName)
    {
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "" + FileName);
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (_onselected != null)
        {
            _onselected.Invoke(gv_List, new AdvancedSearch_OnSelectedChangingEventArgs(gv_List.DataKeys[e.NewSelectedIndex]));
        }
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        lbl_TotalRowsNumer.Text = "";
        int num = gv_List.TotalRecordCount;
        lbl_TotalRowsNumer.Text = "共 " + "<Font Color=Red>" + num + "</Font>" + "条";




    }
    protected void bt_TotalCount_Click(object sender, EventArgs e)
    {
        lbl_TotalRowsNumer.Text = "";
        gv_List.ConditionString = GenarateSQL();

        if (ExtCondition != "")
        {
            if (gv_List.ConditionString != "") gv_List.ConditionString += " AND ";
            gv_List.ConditionString += ExtCondition;
        }
        gv_List.GetTotalRecordCount();
        int num = gv_List.TotalRecordCount;
        lbl_TotalRowsNumer.Text = "共 " + "<Font Color=Red>" + num + "</Font>" + "条";
    }

}

public class AdvancedSearch_OnSelectedChangingEventArgs : System.EventArgs
{
    private DataKey _selectedRowDataKey;
    public DataKey SelectedRowDataKey
    {
        get { return _selectedRowDataKey; }
    }
    public AdvancedSearch_OnSelectedChangingEventArgs(DataKey key)
    {
        _selectedRowDataKey = key;
    }
}

public delegate void AdvancedSearch_OnSelectedChangingEventHandler(object Sender, AdvancedSearch_OnSelectedChangingEventArgs e);
public delegate void AdvancedSearch_OnTableNameControlSelectedIndexChanged(object Sender, EventArgs e);
