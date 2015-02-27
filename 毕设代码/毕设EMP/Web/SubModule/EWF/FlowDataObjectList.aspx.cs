// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.Model.EWF;
using System.Collections.Generic;

public partial class SubModule_EWF_FlowDataObjectList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["AppID"] = Request.QueryString["AppID"] != null ? new Guid(Request.QueryString["AppID"]) : Guid.Empty;

            if ((Guid)ViewState["AppID"] == Guid.Empty)
            {
                MessageBox.ShowAndRedirect(this, "缺少必要参数！", "../Login/Index.aspx");
                return;
            }

            ViewState["PageIndex"] = 0;
            //ViewState["Sort"] = "DisplayOrder";
            //ViewState["SortDirect"] = "ASC";

            BindDropDown();

            EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]);
            lb_AppName.Text = app.Model.Name;
            lb_AppName.NavigateUrl = "FlowAppDetail.aspx?AppID=" + ViewState["AppID"].ToString();

            ListTable<EWF_Flow_DataObject> lt = new ListTable<EWF_Flow_DataObject>(app.GetDataObjectList(), "Name");
            ViewState["ListTable"] = lt;

            BindGrid();

            #region 权限判断
            //Right right = new Right();
            //string strUserName = Session["UserName"].ToString();
            //if (!right.GetAccessPermission(strUserName, 110, 0)) Response.Redirect("../noaccessright.aspx");        //有无查看的权限
            //if (!right.GetAccessPermission(strUserName, 0, 1001)) bt_Add.Visible = false;                              //有无新增的权限
            #endregion
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_DataType.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_DataObjectType");
        ddl_DataType.DataBind();

        ddl_ControlType.DataSource = DictionaryBLL.GetDicCollections("UD_ControlType");
        ddl_ControlType.DataBind();

    }
    #endregion

    private void BindGrid()
    {
        ListTable<EWF_Flow_DataObject> lt = (ListTable<EWF_Flow_DataObject>)ViewState["ListTable"];

        //排序后显示
        IList<EWF_Flow_DataObject> orderlist = new List<EWF_Flow_DataObject>();
        foreach (EWF_Flow_DataObject item in lt.GetListItem().OrderBy<EWF_Flow_DataObject, int>(p => p.SortID))
        {
            orderlist.Add(item);
        }
        gv_List.BindGrid<EWF_Flow_DataObject>(orderlist);
    }

    #region 分页、排序、选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string _name = (string)gv_List.DataKeys[e.NewSelectedIndex]["Name"];

        ListTable<EWF_Flow_DataObject> lt = (ListTable<EWF_Flow_DataObject>)ViewState["ListTable"];

        if (lt.Contains(_name))
        {
            EWF_Flow_DataObject item = lt[_name];

            ViewState["SelectedName"] = item["Name"];
            tbx_Name.Text = item["Name"];
            tbx_DisplayName.Text = item["DisplayName"];
            ddl_DataType.SelectedValue = item["DataType"];
            rbl_ReadOnly.SelectedValue = item["ReadOnly"];
            rbl_Enable.SelectedValue = item["Enable"];
            rbl_Visible.SelectedValue = item["Visible"];
            ddl_ControlType.SelectedValue = item["ControlType"];
            tbx_ControlWidth.Text = item["ControlWidth"] == "0" ? "" : item["ControlWidth"];
            tbx_ControlHeight.Text = item["ControlHeight"] == "0" ? "" : item["ControlHeight"];
            tbx_ControlStyle.Text = item["ControlStyle"];
            tbx_ColSpan.Text = item["ColSpan"];
            tbx_SortID.Text = item["SortID"];
            rbl_IsRequireField.SelectedValue = item["IsRequireField"];
            tbx_RegularExpression.Text = item["RegularExpression"];
            tbx_FormatString.Text = item["FormatString"];
            tbx_Description.Text = item["Description"];

            if (int.Parse(item["RelationType"]) > 0)
                rbl_RelationType.SelectedValue = item["RelationType"];
            rbl_RelationType_SelectedIndexChanged(null, null);


            if (rbl_RelationType.SelectedValue != "3")
            {
                if (ddl_RelationTableName != null || ddl_RelationTableName.Items.Count != 0)
                {
                    ddl_RelationTableName.SelectedValue = item["RelationTableName"];
                    {
                        ddl_RelationTableName_SelectedIndexChanged(null, null);
                    }
                }
                if (ddl_RelationTextField != null || ddl_RelationTextField.Items.Count != 0)
                {
                    ddl_RelationTextField.SelectedValue = item["RelationTextField"];
                }
                if (ddl_RelationValueField != null || ddl_RelationValueField.Items.Count != 0)
                {
                    ddl_RelationValueField.SelectedValue = item["RelationValueField"];
                }
            }
            tbx_SearchPageURL.Text = item["SearchPageURL"];


            bt_Add.Text = "修 改";
        }
    }
    #endregion

    //查询
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    //新增
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        ListTable<EWF_Flow_DataObject> lt = (ListTable<EWF_Flow_DataObject>)ViewState["ListTable"];

        if (ViewState["SelectedName"] == null)
        {
            if (!lt.Contains(tbx_Name.Text))
            {
                #region 新增数据字段
                EWF_Flow_DataObject item = new EWF_Flow_DataObject();
                item["App"] = ViewState["AppID"].ToString();
                item["Name"] = tbx_Name.Text;
                item["DisplayName"] = tbx_DisplayName.Text;
                item["DataType"] = ddl_DataType.SelectedValue;
                item["ReadOnly"] = rbl_ReadOnly.SelectedValue;
                item["Enable"] = rbl_Enable.SelectedValue;
                item["Visible"] = rbl_Visible.SelectedValue;
                item["ControlType"] = ddl_ControlType.SelectedValue;
                item["ControlWidth"] = tbx_ControlWidth.Text;
                item["ControlHeight"] = tbx_ControlHeight.Text;
                item["ControlStyle"] = tbx_ControlStyle.Text;
                item["ColSpan"] = tbx_ColSpan.Text;
                item["SortID"] = tbx_SortID.Text;
                item["IsRequireField"] = rbl_IsRequireField.SelectedValue;
                item["RegularExpression"] = tbx_RegularExpression.Text;
                item["FormatString"] = tbx_FormatString.Text;
                item["Description"] = tbx_Description.Text;
                item["RelationType"] = rbl_RelationType.SelectedValue;

                if (item["RelationType"] != "3")
                {
                    item["RelationTableName"] = ddl_RelationTableName.SelectedValue;
                    if (item["RelationType"] == "2")
                    {
                        item["RelationValueField"] = ddl_RelationValueField.SelectedValue;
                        item["RelationTextField"] = ddl_RelationTextField.SelectedValue;
                    }
                }
                item["SearchPageURL"] = tbx_SearchPageURL.Text;
                lt.Add(item);
                #endregion
            }
        }
        else
        {
            if (lt.Contains((string)ViewState["SelectedName"]))
            {
                #region 修改数据字段
                EWF_Flow_DataObject item = lt[(string)ViewState["SelectedName"]];

                item["App"] = ViewState["AppID"].ToString();
                item["Name"] = tbx_Name.Text;
                item["DisplayName"] = tbx_DisplayName.Text;
                item["DataType"] = ddl_DataType.SelectedValue;
                item["ReadOnly"] = rbl_ReadOnly.SelectedValue;
                item["Enable"] = rbl_Enable.SelectedValue;
                item["Visible"] = rbl_Visible.SelectedValue;
                item["ControlType"] = ddl_ControlType.SelectedValue;
                item["ControlWidth"] = tbx_ControlWidth.Text;
                item["ControlHeight"] = tbx_ControlHeight.Text;
                item["ControlStyle"] = tbx_ControlStyle.Text;
                item["ColSpan"] = tbx_ColSpan.Text;
                item["SortID"] = tbx_SortID.Text;
                item["IsRequireField"] = rbl_IsRequireField.SelectedValue;
                item["RegularExpression"] = tbx_RegularExpression.Text;
                item["FormatString"] = tbx_FormatString.Text;
                item["Description"] = tbx_Description.Text;
                item["RelationType"] = rbl_RelationType.SelectedValue;
                if (item["RelationType"] != "3")
                {
                    item["RelationTableName"] = ddl_RelationTableName.SelectedValue;
                    if (item["RelationType"] == "2")
                    {
                        item["RelationValueField"] = ddl_RelationValueField.SelectedValue;
                        item["RelationTextField"] = ddl_RelationTextField.SelectedValue;
                    }
                }
                item["SearchPageURL"] = tbx_SearchPageURL.Text;
                lt.Update(item);
                #endregion

            }

            ViewState["SelectedName"] = null;
        }

        bt_Add.Text = "新 增";
        gv_List.SelectedIndex = -1;

        tbx_DisplayName.Text = "";
        tbx_Name.Text = "";
        tbx_SortID.Text = (int.Parse(tbx_SortID.Text) + 1).ToString();
        BindGrid();
    }

    protected void bt_AddDefineDataObject_Click(object sender, EventArgs e)
    {
        ListTable<EWF_Flow_DataObject> lt = (ListTable<EWF_Flow_DataObject>)ViewState["ListTable"];

        #region 寻找最大排序ID号
        int maxsortid = 0;
        foreach (EWF_Flow_DataObject item in lt.GetListItem())
        {
            if (maxsortid < item.SortID) maxsortid = item.SortID;
        }
        #endregion


        foreach (ListItem listitem in cbx_PreDefineDO.Items)
        {
            if (!listitem.Selected) continue;
            maxsortid++;
            EWF_Flow_DataObject item;
            switch (listitem.Value)
            {
                case "Position":
                    #region 职位
                    item = new EWF_Flow_DataObject();
                    item["App"] = ViewState["AppID"].ToString();
                    item["Name"] = "Position";
                    item["DisplayName"] = "发起人职位";
                    item["DataType"] = "1";     //整形数值
                    item["ReadOnly"] = "N";
                    item["Enable"] = "Y";
                    item["Visible"] = "Y";
                    item["ControlType"] = "1";
                    item["ColSpan"] = "2";
                    item["SortID"] = maxsortid.ToString();
                    item["IsRequireField"] = "N";
                    item["RelationType"] = "2";
                    item["RelationTableName"] = "MCS_SYS.dbo.Org_Position";
                    item["RelationValueField"] = "ID";
                    item["RelationTextField"] = "Name";

                    lt.Add(item);
                    break;
                    #endregion
                case "OrganizeCity":
                    #region 当前管理片区
                    item = new EWF_Flow_DataObject();
                    item["App"] = ViewState["AppID"].ToString();
                    item["Name"] = "OrganizeCityID";
                    item["DisplayName"] = "当前管理片区";
                    item["DataType"] = "1";     //整形数值
                    item["ReadOnly"] = "N";
                    item["Enable"] = "N";
                    item["Visible"] = "Y";
                    item["ControlType"] = "1";
                    item["ColSpan"] = "2";
                    item["SortID"] = maxsortid.ToString();
                    item["IsRequireField"] = "N";
                    item["RelationType"] = "2";
                    item["RelationTableName"] = "MCS_SYS.dbo.Addr_OrganizeCity";
                    item["RelationValueField"] = "ID";
                    item["RelationTextField"] = "Name";

                    lt.Add(item);

                    item = new EWF_Flow_DataObject();
                    item["App"] = ViewState["AppID"].ToString();
                    item["Name"] = "OrganizeCityName";
                    item["DisplayName"] = "管理片区名称";
                    item["DataType"] = "1";     //整形数值
                    item["ReadOnly"] = "N";
                    item["Enable"] = "N";
                    item["Visible"] = "N";
                    item["ControlType"] = "1";
                    item["ColSpan"] = "2";
                    item["SortID"] = maxsortid.ToString();
                    item["IsRequireField"] = "N";
                    item["RelationType"] = "3";

                    lt.Add(item);
                    break;
                    #endregion
                case "OfficeCity":
                    #region 所属办事处
                    item = new EWF_Flow_DataObject();
                    item["App"] = ViewState["AppID"].ToString();
                    item["Name"] = "OfficeCityID";
                    item["DisplayName"] = "所属办事处ID";
                    item["DataType"] = "1";     //整形数值
                    item["ReadOnly"] = "N";
                    item["Enable"] = "N";
                    item["Visible"] = "N";
                    item["ControlType"] = "1";
                    item["ColSpan"] = "2";
                    item["SortID"] = maxsortid.ToString();
                    item["IsRequireField"] = "N";
                    item["RelationType"] = "3";
                    lt.Add(item);

                    item = new EWF_Flow_DataObject();
                    item["App"] = ViewState["AppID"].ToString();
                    item["Name"] = "OfficeCityName";
                    item["DisplayName"] = "所属办事处名称";
                    item["DataType"] = "1";     //整形数值
                    item["ReadOnly"] = "N";
                    item["Enable"] = "N";
                    item["Visible"] = "Y";
                    item["ControlType"] = "1";
                    item["ColSpan"] = "2";
                    item["SortID"] = maxsortid.ToString();
                    item["IsRequireField"] = "N";
                    item["RelationType"] = "3";

                    lt.Add(item);
                    break;
                    #endregion
                case "Remark":
                    #region 备注
                    item = new EWF_Flow_DataObject();
                    item["App"] = ViewState["AppID"].ToString();
                    item["Name"] = "Remark";
                    item["DisplayName"] = "备注";
                    item["DataType"] = "3";     //整形数值
                    item["ReadOnly"] = "N";
                    item["Enable"] = "N";
                    item["Visible"] = "Y";
                    item["ControlType"] = "5";
                    item["ColSpan"] = "3";
                    item["SortID"] = maxsortid.ToString();
                    item["IsRequireField"] = "N";
                    item["RelationType"] = "3";
                    item["ControlWidth"] = "500";
                    item["ControlHeight"] = "60";
                    lt.Add(item);
                    break;
                    #endregion
            }
        }
        tbx_SortID.Text = maxsortid.ToString();
        BindGrid();

    }

    protected void gv_FeeDetialList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string _name = (string)gv_List.DataKeys[e.RowIndex]["Name"];

        ListTable<EWF_Flow_DataObject> lt = (ListTable<EWF_Flow_DataObject>)ViewState["ListTable"];

        if (lt.Contains(_name))
        {
            lt.Remove(_name);
        }
        BindGrid();
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        ListTable<EWF_Flow_DataObject> lt = (ListTable<EWF_Flow_DataObject>)ViewState["ListTable"];

        EWF_Flow_DataObjectBLL bll = new EWF_Flow_DataObjectBLL();
        foreach (EWF_Flow_DataObject item in lt.GetListItem(ItemState.Added))
        {
            bll.Model = item;
            bll.Add();
        }

        foreach (EWF_Flow_DataObject item in lt.GetListItem(ItemState.Modified))
        {
            bll.Model = item;
            bll.Update();
        }

        foreach (EWF_Flow_DataObject item in lt.GetListItem(ItemState.Deleted))
        {
            bll.Delete(item.ID);
        }

        Response.Redirect("FlowDataObjectList.aspx?AppID=" + ViewState["AppID"].ToString());
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
                ddl_RelationTableName.DataSource = UD_TableListBLL.GetModelList("");
                ddl_RelationTableName.DataBind();
                ddl_RelationTableName_SelectedIndexChanged(null, null);
                break;
            case "3":
                tr_1.Visible = false;
                tr_2.Visible = false;
                break;
        }
    }

    protected void bt_Increase_Click(object sender, EventArgs e)
    {
        string name = (string)gv_List.DataKeys[((GridViewRow)((Button)sender).Parent.Parent).RowIndex][0];
        ListTable<EWF_Flow_DataObject> lt = (ListTable<EWF_Flow_DataObject>)ViewState["ListTable"];
        lt[name].SortID++;
        lt.Update(lt[name]);
        //UD_Panel_ModelFieldsBLL bll = new UD_Panel_ModelFieldsBLL(id);
        //bll.Model.SortID++;
        //bll.Update();

        BindGrid();
    }
    protected void bt_Decrease_Click(object sender, EventArgs e)
    {
        string name = (string)gv_List.DataKeys[((GridViewRow)((Button)sender).Parent.Parent).RowIndex][0];
        ListTable<EWF_Flow_DataObject> lt = (ListTable<EWF_Flow_DataObject>)ViewState["ListTable"];
        lt[name].SortID--;
        lt.Update(lt[name]);
        //UD_Panel_ModelFieldsBLL bll = new UD_Panel_ModelFieldsBLL(id);
        //if (bll.Model.SortID > 0) bll.Model.SortID--;
        //bll.Update();

        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
            Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
        else if (e.Index == 0)
            Response.Redirect("FlowAppDetail.aspx?AppID=" + ViewState["AppID"].ToString());
        else if (e.Index == 3)
            Response.Redirect("FlowInitPosition.aspx?AppID=" + ViewState["AppID"].ToString());
    }

}