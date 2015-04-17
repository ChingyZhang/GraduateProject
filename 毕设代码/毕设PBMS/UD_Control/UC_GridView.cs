using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.IFStrategy;
using System.Collections;
using MCSControls.MCSWebControls;
using MCSFramework.Common;

namespace MCSFramework.UD_Control
{
    [ToolboxData("<{0}:UC_GridView runat=server></{0}:UC_GridView>")]
    public class UC_GridView : GridView
    {
        #region Property

        [Browsable(true),
        Description("The code of the panel contains the content of the gridview"),
        Category("Extends")]
        public string PanelCode
        {
            get
            {
                if (ViewState["PanelCode"] == null)
                    return "";
                return ViewState["PanelCode"].ToString();
            }
            set
            {
                ViewState["PanelCode"] = value;
            }
        }

        [Browsable(true),
        Description("The conditionstring of the sqlcommand"),
        Category("Extends")]
        public string ConditionString
        {
            get
            {
                if (ViewState["ConditionString"] == null)
                    return "";
                return ViewState["ConditionString"].ToString();
            }
            set
            {
                ViewState["ConditionString"] = value;
            }
        }

        [Browsable(true),
        Description("需要进行排序字段"),
        Category("Extends")]
        public string OrderFields
        {
            get
            {
                if (ViewState["OrderFields"] == null) return "";
                return ViewState["OrderFields"].ToString();
            }
            set
            {
                ViewState["OrderFields"] = value;
            }
        }

        [Description("The totalrecordcount of the datasource"),
        Category("Extends")]
        public int TotalRecordCount
        {
            get
            {
                if (ViewState["TotalRecordCount"] == null)
                    return 0;
                return (int)ViewState["TotalRecordCount"];
            }
            set
            {
                ViewState["TotalRecordCount"] = value;
            }
        }

        public override int PageCount
        {
            get
            {
                if (string.IsNullOrEmpty(PanelCode)) return base.PageCount;

                if (ViewState["TotalRecordCount"] == null || !AllowPaging || PageSize == 0)
                    return 0;

                if ((int)ViewState["TotalRecordCount"] == 0)
                    return 0;
                else
                    return ((int)ViewState["TotalRecordCount"] - 1) / PageSize + 1;
            }
        }

        public override int PageIndex
        {
            get
            {
                if (string.IsNullOrEmpty(PanelCode)) return base.PageIndex;

                if (ViewState["PageIndex"] == null)
                    return 0;
                return (int)ViewState["PageIndex"];
            }
            set
            {
                if (string.IsNullOrEmpty(PanelCode))
                    base.PageIndex = value;
                else
                    ViewState["PageIndex"] = value;
            }
        }

        [Description("if the gridview is binded"),
        Category("Extends")]
        public bool Binded
        {
            get
            {
                if (ViewState["Binded"] == null)
                    return false;
                return (bool)ViewState["Binded"];
            }
            set
            {
                ViewState["Binded"] = value;
            }
        }

        private string _pagertextFormat = "每页<font color=red>{0}</font>条/共<font color=red>{1}</font>条&nbsp;第<font color=red>{2}</font>页/共<font color=red>{3}</font>页";
        /// <summary>
        /// 自定义分页的文本显示样式（{0}-每页显示记录数；{1}-总记录数；{2}-当前页数；{3}-总页数）
        /// </summary>
        [
        Description("自定义分页的文本显示样式"),
        Category("扩展"),
        DefaultValue(typeof(string), "每页<font color=red>{0}</font>条/共<font color=red>{1}</font>条&nbsp;第<font color=red>{2}</font>页/共<font color=red>{3}</font>页"),
        NotifyParentProperty(true),
        ]
        public virtual string PagerTextFormat
        {
            get { return _pagertextFormat; }
            set { _pagertextFormat = value; }
        }
        #endregion

        #region Init the components of the control
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                if (ViewState["PanelCode"] != null && ViewState["PanelCode"].ToString() != string.Empty)
                    CreateSqlString();
            }
            catch { }

        }
        #endregion

        #region create the string of sqlcommand
        private void CreateSqlString()
        {
            string _sqlstring = "";
            string _selectstr = "SELECT ";
            string _fromstr = "";
            string _tmpfrom = "";           //字段如果是关联时，临时生成的from sql

            #region create the select string
            UD_PanelBLL _panelbll = new UD_PanelBLL(PanelCode, true);
            if (_panelbll.Model == null) throw new Exception("Panel:" + PanelCode + ",未能找到该Code对应的Panel,Code无效!");

            Dictionary<string, UD_Panel_ModelFields> dicTreeColumnList = new Dictionary<string, UD_Panel_ModelFields>();
            IList<UD_Panel_ModelFields> _panel_modelfieldsmodels = _panelbll.GetModelFields();
            OrderFields = _panelbll.Model.DefaultSortFields;

            if (_panel_modelfieldsmodels.Count == 0)
            {
                //未定义Panel字段
                ViewState["SqlString"] = "";
                return;
            }

            if (!string.IsNullOrEmpty(_panelbll.Model.Description))
            {
                string[] _array = _panelbll.Model.Description.Split(new char[] { '|' }, StringSplitOptions.None);
                if (_array.Length >= 2) ViewState["DBConnectString"] = _array[1];
            }

            foreach (UD_Panel_ModelFields _panel_modelfields in _panel_modelfieldsmodels)
            {

                UD_ModelFields _modelfield = new UD_ModelFieldsBLL(_panel_modelfields.FieldID, true).Model;
                UD_TableList _tablemodel = new UD_TableListBLL(_modelfield.TableID, true).Model;


                string _fieldfullname = _tablemodel.ModelClassName + "_" + _modelfield.FieldName;

                #region 判断字段关联类型，决定如果创建SQL
                switch (_modelfield.RelationType)
                {
                    case 1://Relation to dic
                        if (_panel_modelfields.DisplayMode == 1)//Bound the id value of the field
                        {
                            if (_modelfield.Flag == "Y")
                            {
                                _selectstr += _tablemodel.Name + "." + _modelfield.FieldName + " AS " + _fieldfullname + ",";
                            }
                            else//扩展字段
                            {
                                _selectstr += "[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," +
                                    _modelfield.Position.ToString() + ") AS " + _fieldfullname + ",";
                            }
                        }
                        else//Bound the text value of the field
                        {
                            //计算出关联字典表的别名"_vname"
                            string _vname = "V_Dictionary_Data" + "_" + _modelfield.FieldName;
                            if (_modelfield.Flag == "Y")
                            {
                                _selectstr += _vname + ".Name" + " AS " + _fieldfullname + ",";
                                _tmpfrom += " left join V_Dictionary_Data AS " + _vname + " ON " + _tablemodel.Name + "." + _modelfield.FieldName + "=" + _vname + ".Code AND " + _vname + ".TableName='" + _modelfield.RelationTableName + "' ";
                            }
                            else//扩展字段
                            {
                                _selectstr += _vname + ".Name" + " AS " + _fieldfullname + ",";
                                _tmpfrom += " left join V_Dictionary_Data AS " + _vname + " ON [dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") =" + _vname + ".Code AND " + _vname + ".TableName='" + _modelfield.RelationTableName + "' ";
                            }
                        }
                        break;
                    case 2://Relation to model table
                        if (_panel_modelfields.TreeLevel > 0 && new UD_TableListBLL(_modelfield.RelationTableName).Model.TreeFlag == "Y")
                        {
                            #region 字段关联到树形结构表，且要显示上层父结点信息
                            _fieldfullname += _panel_modelfields.TreeLevel.ToString();
                            if (_modelfield.Flag == "Y")
                                _selectstr += _tablemodel.Name + "." + _modelfield.FieldName + " AS " + _fieldfullname + ",";
                            else//扩展字段
                                _selectstr += "[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") AS " + _fieldfullname + ",";

                            dicTreeColumnList.Add(_fieldfullname, _panel_modelfields);
                            #endregion

                            #region 注释
                            //if (param.TreeLevel == 100 && param.DisplayMode == 2)
                            //{
                            //    //显示字段路径全称
                            //    if (_modelfield.Flag == "Y")
                            //        _selectstr += "[dbo].[Exuf_TreeTable_GetFullName]('" + _modelfield.RelationTableName + "'," + _tablemodel.Name + "." + _modelfield.FieldName + ",1) AS " + _fieldfullname + ",";
                            //    else//扩展字段
                            //        _selectstr += "[dbo].[Exuf_TreeTable_GetFullName]('" + _modelfield.RelationTableName + "'," + "[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + "),0) AS " + _fieldfullname + ",";
                            //}
                            //else
                            //{
                            //    _fieldfullname += param.TreeLevel.ToString();
                            //    if (param.DisplayMode == 1)//Bound the id value of the field
                            //    {
                            //        if (_modelfield.Flag == "Y")
                            //            _selectstr += "[dbo].[Exuf_TreeTable_GetSuperIDByLevel]('" + _modelfield.RelationTableName + "'," + _tablemodel.Name + "." + _modelfield.FieldName + "," + param.TreeLevel.ToString() + ") AS " + _fieldfullname + ",";
                            //        else//扩展字段
                            //        {
                            //            _selectstr += "[dbo].[Exuf_TreeTable_GetSuperIDByLevel]('" + _modelfield.RelationTableName + "'," + "[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ")," + param.TreeLevel.ToString() + ") AS " + _fieldfullname + ",";
                            //        }
                            //    }
                            //    else//Bound the text value of the field
                            //    {
                            //        if (_modelfield.Flag == "Y")
                            //            _selectstr += "[dbo].[Exuf_TreeTable_GetSuperNameByLevel]('" + _modelfield.RelationTableName + "'," + _tablemodel.Name + "." + _modelfield.FieldName + "," + param.TreeLevel.ToString() + ") AS " + _fieldfullname + ",";
                            //        else//扩展字段
                            //            _selectstr += "[dbo].[Exuf_TreeTable_GetSuperNameByLevel]('" + _modelfield.RelationTableName + "'," + "[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ")," + param.TreeLevel.ToString() + ") AS " + _fieldfullname + ",";
                            //    }
                            //}
                            #endregion
                        }
                        else
                        {
                            #region 不需关联到树形结构表
                            if (_panel_modelfields.DisplayMode == 1)//Bound the id value of the field
                            {
                                if (_modelfield.Flag == "Y")
                                    _selectstr += _tablemodel.Name + "." + _modelfield.FieldName + " AS " + _fieldfullname + ",";
                                else//扩展字段
                                {
                                    _selectstr += "[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") AS " + _fieldfullname + ",";
                                }
                            }
                            else//Bound the text value of the field
                            {
                                #region 计算出关联表的别名"_vname"
                                string _vname = "";
                                int pos = _modelfield.RelationTableName.LastIndexOf('.');
                                if (pos >= 0)
                                    _vname = _modelfield.RelationTableName.Substring(pos + 1) + "_" + _modelfield.FieldName;
                                else
                                    _vname = _modelfield.RelationTableName + "_" + _modelfield.FieldName;
                                #endregion

                                if (_modelfield.Flag == "Y")
                                {
                                    _selectstr += _vname + "." + _modelfield.RelationTextField + " AS " + _fieldfullname + ",";
                                    _tmpfrom += " left join " + _modelfield.RelationTableName + " AS " + _vname + " ON " + _tablemodel.Name + "." + _modelfield.FieldName + "=" + _vname + "." + _modelfield.RelationValueField;
                                }
                                else//扩展字段
                                {
                                    _selectstr += _vname + "." + _modelfield.RelationTextField + " AS " + _fieldfullname + ",";
                                    _tmpfrom += " left join " + _modelfield.RelationTableName + " AS " + _vname + " ON [dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") =" + _vname + "." + _modelfield.RelationValueField;
                                }
                            }
                            #endregion
                        }

                        break;
                    default://No relation
                        if (_modelfield.Flag == "Y")
                            _selectstr += _tablemodel.Name + "." + _modelfield.FieldName + " AS " + _fieldfullname + ",";
                        else
                        {
                            _selectstr += "[dbo].[UF_Spilt](" + _tablemodel.Name + ".ExtPropertys,'|'," + _modelfield.Position.ToString() + ") AS " + _fieldfullname + ",";
                        }
                        break;
                }
                #endregion

                //Add a bound field to the gridview according to the selected field
                BoundField _boundfield = new BoundField();
                _boundfield.HeaderText = string.IsNullOrEmpty(_panel_modelfields.LabelText) ? _modelfield.DisplayName : _panel_modelfields.LabelText;
                _boundfield.DataField = _fieldfullname;
                _boundfield.SortExpression = _fieldfullname;

                if (!_panel_modelfields.FormatString.StartsWith("{0:"))
                    _boundfield.DataFormatString = "{0:" + _panel_modelfields.FormatString + "}";
                else
                    _boundfield.DataFormatString = _panel_modelfields.FormatString;
                //_boundfield.Visible = _panel_modelfieldsmodel.Visible == "Y";

                if (_panel_modelfields.Visible == "Y")
                {
                    if (_panel_modelfields.SortID - 1 > Columns.Count)
                        Columns.Add(_boundfield);
                    else
                        Columns.Insert(_panel_modelfields.SortID - 1, _boundfield);
                }
            }
            _selectstr = _selectstr.Substring(0, _selectstr.Length - 1);
            #endregion

            #region create the from string of sql
            IList<UD_Panel_TableRelation> _panel_tablerelations = _panelbll.GetTableRelations();
            if (_panel_tablerelations.Count == 0)
            {
                IList<UD_Panel_Table> _panel_tables = _panelbll.GetPanelTables();

                if (_panel_tables.Count == 0) throw new Exception("Panel:" + PanelCode + "在定义中未包括包含的表信息!");

                _fromstr = " FROM " + new UD_TableListBLL(_panel_tables[0].TableID, true).Model.Name;
            }
            else
            {
                foreach (UD_Panel_TableRelation _panel_tablerelation in _panel_tablerelations)
                {
                    string _parenttablename = new UD_TableListBLL(_panel_tablerelation.ParentTableID, true).Model.Name;
                    string _childtablename = new UD_TableListBLL(_panel_tablerelation.ChildTableID, true).Model.Name;
                    UD_ModelFields _parentfield = new UD_ModelFieldsBLL(_panel_tablerelation.ParentFieldID, true).Model;
                    UD_ModelFields _childfield = new UD_ModelFieldsBLL(_panel_tablerelation.ChildFieldID, true).Model;
                    if (_panel_tablerelation.JoinMode == "") _panel_tablerelation.JoinMode = "LEFT JOIN";

                    if (_fromstr == "")
                        _fromstr = " FROM " + _parenttablename + " ";

                    if (_parentfield.Flag == "Y" && _childfield.Flag == "Y")
                    {
                        _fromstr += " " + _panel_tablerelation.JoinMode + " " + _childtablename + " ON " + _parenttablename + "." + _parentfield.FieldName + "=" + _childtablename + "." + _childfield.FieldName + " ";
                    }
                    else
                    {
                        _fromstr += " " + _panel_tablerelation.JoinMode + " " + _childtablename + " ON ";

                        if (_parentfield.Flag == "Y")
                            _fromstr += "CONVERT(VarChar," + _parenttablename + "." + _parentfield.FieldName + ")";
                        else
                            _fromstr += "[dbo].[UF_Spilt](" + _parenttablename + ".ExtPropertys,'|'," +
                                    new UD_ModelFieldsBLL(_panel_tablerelation.ParentFieldID, true).Model.Position + ")";

                        _fromstr += " = ";
                        
                        if (_childfield.Flag == "Y")
                            _fromstr += "CONVERT(VarChar," + _childtablename + "." + _childfield.FieldName + ")";
                        else
                            _fromstr += "[dbo].[UF_Spilt](" + _childtablename + ".ExtPropertys,'|'," +
                                    new UD_ModelFieldsBLL(_panel_tablerelation.ChildFieldID, true).Model.Position + ")";

                    }
                    if (_panel_tablerelation.RelationCondition != "")
                    {
                        _fromstr += " AND " + _panel_tablerelation.RelationCondition + " ";
                    }
                }
            }

            if (_tmpfrom != "")
                _fromstr += _tmpfrom;

            _sqlstring = _selectstr + _fromstr;
            #endregion

            ViewState["SqlString"] = _sqlstring;
            ViewState["TreeColumn"] = dicTreeColumnList;
        }
        #endregion

        #region Bind the GridView with the special datasource
        public void BindGrid()
        {
            FillDataSource();
            DataBind();
            Binded = true;

            if (PanelCode != "" && ViewState["TreeColumn"] != null)
            {
                Dictionary<string, UD_Panel_ModelFields> dicTreeColumnList = (Dictionary<string, UD_Panel_ModelFields>)ViewState["TreeColumn"];
                for (int i = 0; i < Columns.Count; i++)
                {
                    DataControlField column = Columns[i];

                    if (column.GetType().Name == "BoundField")
                    {
                        BoundField field = (BoundField)column;
                        if (!column.Visible) continue;

                        foreach (GridViewRow row in Rows)
                        {
                            if (row.Cells[i].Text.StartsWith("1900-01-01")) row.Cells[i].Text = "";
                        }

                        if (!dicTreeColumnList.ContainsKey(field.DataField)) continue;

                        UD_Panel_ModelFields _panel_modelfields = dicTreeColumnList[field.DataField];
                        Dictionary<int, string> cachevalue = new Dictionary<int, string>();

                        foreach (GridViewRow row in Rows)
                        {
                            int value = 0;
                            if (int.TryParse(row.Cells[i].Text, out value))
                            {
                                if (cachevalue.ContainsKey(value))
                                {
                                    row.Cells[i].Text = cachevalue[value];
                                }
                                else
                                {
                                    if (_panel_modelfields.TreeLevel == 100)
                                    {
                                        //显示全路径
                                        row.Cells[i].Text = TreeTableBLL.GetFullPathName(new UD_ModelFieldsBLL(_panel_modelfields.FieldID, true).Model.RelationTableName, value);
                                    }
                                    else
                                    {
                                        //显示父级
                                        if (_panel_modelfields.DisplayMode == 1)
                                            row.Cells[i].Text = TreeTableBLL.GetSuperIDByLevel(new UD_ModelFieldsBLL(_panel_modelfields.FieldID, true).Model.RelationTableName, value, _panel_modelfields.TreeLevel).ToString();
                                        else
                                            row.Cells[i].Text = TreeTableBLL.GetSuperNameByLevel(new UD_ModelFieldsBLL(_panel_modelfields.FieldID, true).Model.RelationTableName, value, _panel_modelfields.TreeLevel);
                                    }
                                    cachevalue.Add(value, row.Cells[i].Text);
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion

        #region Fill the datasource of the GridView
        private void FillDataSource()
        {
            try
            {
                if (ViewState["SqlString"] != null && ViewState["SqlString"].ToString() != "")
                {
                    int TotalRecordCount = 0;
                    string _sqlstring = ViewState["SqlString"].ToString();
                    if (ConditionString != "") _sqlstring += " WHERE " + ConditionString;

                    string DBConnectString = null;
                    if (ViewState["DBConnectString"] != null) DBConnectString = (string)ViewState["DBConnectString"];
                    if (AllowPaging)
                        DataSource = TreeTableBLL.ExecSqlString(DBConnectString, _sqlstring, PageSize, PageIndex, OrderFields, out TotalRecordCount);
                    else
                        DataSource = TreeTableBLL.ExecSqlString(DBConnectString, _sqlstring, 999999, 0, OrderFields, out TotalRecordCount);
                    ViewState["TotalRecordCount"] = TotalRecordCount;

                }
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("UC_GridView.FillDataSource: TreeTableBLL.ExecSqlString Error!", err);
                LogWriter.WriteLog(ViewState["SqlString"].ToString());
            }
        }
        #endregion


        public void GetTotalRecordCount()
        {
            try
            {
                if (ViewState["SqlString"] != null && ViewState["SqlString"].ToString() != "")
                {
                    int TotalRecordCount = 0;
                    string _sqlstring = ViewState["SqlString"].ToString();
                    if (ConditionString != "") _sqlstring += " WHERE " + ConditionString;

                    string DBConnectString = null;
                    if (ViewState["DBConnectString"] != null) DBConnectString = (string)ViewState["DBConnectString"];
                    TreeTableBLL.ExecSqlStringByTotalCount(DBConnectString, _sqlstring, out TotalRecordCount);
                    ViewState["TotalRecordCount"] = TotalRecordCount;
                }
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("UC_GridView.FillDataSource: TreeTableBLL.ExecSqlString Error!", err);
                LogWriter.WriteLog(ViewState["SqlString"].ToString());
            }

        }

        protected override void InitializePager(System.Web.UI.WebControls.GridViewRow row, int columnSpan, System.Web.UI.WebControls.PagedDataSource pagedDataSource)
        {
            base.InitializePager(row, columnSpan, pagedDataSource);
            int recordCount = pagedDataSource.DataSourceCount;

            LinkButton First = new LinkButton();
            LinkButton Prev = new LinkButton();
            LinkButton Next = new LinkButton();
            LinkButton Last = new LinkButton();

            TableCell tc = new TableCell();

            row.Controls.Clear();

            tc.Controls.Add(new LiteralControl("&nbsp"));

            #region 显示总记录数 每页记录数 当前页数/总页数
            string textFormat = String.Format(_pagertextFormat,
                PageSize,
                TotalRecordCount == 0 ? recordCount : TotalRecordCount,
                PageIndex + 1,
                PageCount);
            tc.Controls.Add(new LiteralControl(textFormat));
            tc.Controls.Add(new LiteralControl("&nbsp"));
            #endregion

            #region 设置“首页 上一页 下一页 末页”按钮
            First.Text = "首页";
            First.CommandName = "Page";
            First.CommandArgument = "First";
            First.CssClass = "listViewTdLinkS1";

            Prev.Text = "上一页";
            Prev.CommandName = "Page";
            Prev.CommandArgument = "Prev";
            Prev.CssClass = "listViewTdLinkS1";

            Next.Text = "下一页";
            Next.CommandName = "Page";
            Next.CommandArgument = "Next";
            Next.CssClass = "listViewTdLinkS1";

            Last.Text = "末页";
            Last.CommandName = "Page";
            Last.CommandArgument = "Last";
            Last.CssClass = "listViewTdLinkS1";

            if (PageIndex <= 0)
                First.Enabled = Prev.Enabled = false;
            else
                First.Enabled = Prev.Enabled = true;

            if (PageIndex >= PageCount - 1)
                Next.Enabled = Last.Enabled = false;
            else
                Next.Enabled = Last.Enabled = true;

            tc.Controls.Add(First);
            tc.Controls.Add(new LiteralControl("&nbsp;"));
            tc.Controls.Add(Prev);
            tc.Controls.Add(new LiteralControl("&nbsp;"));
            tc.Controls.Add(Next);
            tc.Controls.Add(new LiteralControl("&nbsp"));
            tc.Controls.Add(Last);
            tc.Controls.Add(new LiteralControl("&nbsp;"));


            #endregion

            #region 设置转至指定页
            tc.Controls.Add(new LiteralControl("&nbsp跳转至第"));
            TextBox tbx_go = new TextBox();
            tbx_go.ID = "_tbx_page_go";
            tbx_go.Text = (PageIndex + 1).ToString();
            tbx_go.Width = Unit.Pixel(30);
            tc.Controls.Add(tbx_go);
            tc.Controls.Add(new LiteralControl("页"));

            RangeValidator rg = new RangeValidator();
            rg.ControlToValidate = "_tbx_page_go";
            rg.MinimumValue = "1";
            if (PageCount != 0)
                rg.MaximumValue = PageCount.ToString();
            else
                rg.MaximumValue = rg.MinimumValue;
            rg.ErrorMessage = "页码范围无效";
            rg.ValidationGroup = "_checkpage";
            rg.Display = ValidatorDisplay.Dynamic;
            rg.Type = ValidationDataType.Integer;
            tc.Controls.Add(rg);

            Button bt_go = new Button();
            bt_go.Text = "GO";
            bt_go.Click += new EventHandler(bt_go_Click);
            bt_go.ValidationGroup = "_checkpage";
            tc.Controls.Add(bt_go);
            #endregion

            if (this.AutoGenerateColumns)
            {
                if (this.DataSource != null && this.DataSource.GetType().FullName == "System.Data.DataTable")
                {
                    tc.ColumnSpan = ((DataTable)this.DataSource).Columns.Count + this.Columns.Count;
                }
                else
                    tc.ColumnSpan = 5;
            }
            else
                tc.ColumnSpan = this.Columns.Count;

            row.Controls.Add(tc);
        }

        void bt_go_Click(object sender, EventArgs e)
        {
            TextBox tbx_go = (TextBox)this.BottomPagerRow.Cells[0].FindControl("_tbx_page_go");
            int pageindex = 0;

            if (int.TryParse(tbx_go.Text, out pageindex))
            {
                OnPageIndexChanging(new GridViewPageEventArgs(pageindex - 1));
            }
        }

        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            if (string.IsNullOrEmpty(PanelCode))
                base.OnPageIndexChanging(e);
            else
            {
                PageIndex = e.NewPageIndex;
                BindGrid();
            }
        }

        protected override void OnSorting(GridViewSortEventArgs e)
        {
            if (string.IsNullOrEmpty(PanelCode))
                base.OnSorting(e);
            else
            {
                if (OrderFields.StartsWith(e.SortExpression))
                {
                    if (OrderFields.EndsWith("ASC"))
                        OrderFields = e.SortExpression + " DESC";
                    else
                        OrderFields = e.SortExpression + " ASC";
                }
                else
                    OrderFields = e.SortExpression + " ASC";

                BindGrid();
            }
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // 为每一个数据行添加两个属性，实现当鼠标经过时高亮的效果
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //鼠标移到该行时所在行变颜色，移出时恢复原来颜色
                    e.Row.Attributes.Add("OnMouseOver", "this.style.cursor='hand';this.originalcolor=this.style.backgroundColor;this.style.backgroundColor='#FFCC66';");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=this.originalcolor;");
                }
            }
        }
        /// <summary>
        /// 用IList(IModel)数据源来绑定GridView
        /// </summary>
        /// <param name="m"></param>
        public void BindGrid<T>(IList<T> source)
        {
            this.DataSource = source;
            this.DataBind();

            if (source.Count == 0) return;

            IList<UD_TableList> _tables = new UD_TableListBLL()._GetModelList("ModelName='" + ((IModel)source[0]).ModelName + "'");
            if (_tables.Count == 0) return;
            UD_TableList table = _tables[0];

            IList<UD_ModelFields> fields = new UD_TableListBLL(table.ID).GetModelFields();

            for (int i = 0; i < Columns.Count; i++)
            {
                DataControlField column = Columns[i];

                if (column.GetType().Name == "BoundField")
                {
                    BoundField field = (BoundField)column;
                    if (!column.Visible) continue;

                    IList<UD_ModelFields> _models = UD_ModelFieldsBLL.GetModelList("TableID='" + table.ID.ToString() + "' AND FieldName='" + field.DataField + "'");
                    if (_models.Count == 0) continue;
                    UD_ModelFields model = _models[0];

                    switch (model.RelationType)
                    {
                        case 1:     //关联字典表
                            foreach (GridViewRow row in Rows)
                            {
                                Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections(model.RelationTableName, false);
                                row.Cells[i].Text = dic.ContainsKey(row.Cells[i].Text) ? dic[row.Cells[i].Text].Name : "";
                            }
                            break;
                        case 2:     //关联实体表
                            foreach (GridViewRow row in Rows)
                            {
                                row.Cells[i].Text = TreeTableBLL.GetRelationTableDataValue(model.RelationTableName, model.RelationValueField, model.RelationTextField, row.Cells[i].Text);
                            }
                            break;
                    }

                    if (model.DataType == 4)  //日期型
                    {
                        foreach (GridViewRow row in Rows)
                        {
                            if (row.Cells[i].Text.StartsWith("1900-01-01")) row.Cells[i].Text = "";
                        }
                    }
                }
            }
        }


        #region 设置指定控件中的输入控件的Enable
        /// <summary>
        /// 设置TR中的输入控件的Enable
        /// </summary>
        /// <param name="TRName"></param>
        /// <param name="Enable"></param>
        public void SetControlsEnable(bool enable)
        {
            SetControlsEnable(this, enable);
        }

        private void SetControlsEnable(Control control, bool Enable)
        {
            foreach (Control _c in control.Controls)
            {
                string Type = _c.GetType().ToString();
                if (Type == "System.Web.UI.WebControls.TextBox")
                    ((TextBox)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.DropDownList")
                    ((DropDownList)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.RadioButtonList")
                    ((RadioButtonList)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.CheckBoxList")
                    ((CheckBoxList)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.CheckBox")
                    ((CheckBox)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.Button")
                    ((Button)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.DataControlButton")
                    ((Button)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.HyperLink")
                    ((HyperLink)_c).Enabled = Enable;
                if (Type == "MCSControls.MCSWebControls.MCSSelectControl")
                    ((MCSSelectControl)_c).Enabled = Enable;
                if (Type == "MCSControls.MCSWebControls.MCSTreeControl")
                    ((MCSTreeControl)_c).Enabled = Enable;

                if (_c.HasControls())
                {
                    SetControlsEnable(_c, Enable);
                }
            }
        }
        #endregion
    }
}
