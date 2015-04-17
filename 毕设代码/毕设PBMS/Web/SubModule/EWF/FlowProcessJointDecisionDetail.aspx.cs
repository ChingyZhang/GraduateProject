using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.Model.EWF;
using System.Collections.Generic;
using System.Linq;

public partial class SubModule_EWF_FlowProcessJointDecisionDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["AppID"] = Request.QueryString["AppID"] != null ? new Guid(Request.QueryString["AppID"]) : Guid.Empty;
            ViewState["ProcessID"] = Request.QueryString["ProcessID"] != null ? new Guid(Request.QueryString["ProcessID"]) : Guid.Empty;
            #endregion

            if ((Guid)ViewState["AppID"] == Guid.Empty)
            {
                MessageBox.ShowAndRedirect(this, "缺少必要参数！", "../Login/Index.aspx");
                return;
            }

            BindDropDown();

            if ((Guid)ViewState["ProcessID"] != Guid.Empty)
            {
                BindData();
                this.bt_Save.Text = "修改";
                this.bt_Save.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ViewState["Details"] = new ListTable<EWF_Flow_ProcessJointDecision_Recipients>(new List<EWF_Flow_ProcessJointDecision_Recipients>(), "RecipientRole");
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        EWF_Flow_AppBLL app = new EWF_Flow_AppBLL((Guid)ViewState["AppID"]);
        lb_AppName.Text = app.Model.Name;

        // 默认下一环节
        ddl_DefaultNextProcess.DataSource = app.GetProcessList();
        ddl_DefaultNextProcess.DataBind();
        ddl_DefaultNextProcess.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        //环节类型
        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_ProcessType");
        ddl_Type.DataBind();
        this.ddl_Type.SelectedValue = "10";

        ddl_PositiveNextProcess.DataSource = app.GetProcessList();
        ddl_PositiveNextProcess.DataBind();
        ddl_PositiveNextProcess.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        ddl_NegativeNextProcess.DataSource = app.GetProcessList();
        ddl_NegativeNextProcess.DataBind();
        ddl_NegativeNextProcess.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        ddl_RecipientRole_Decision.DataSource = EWF_RoleBLL.GetModelList("");
        ddl_RecipientRole_Decision.DataBind();
        ddl_RecipientRole_Decision.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

        ddl_DataObject_Decision.DataSource = app.GetDataObjectList();
        ddl_DataObject_Decision.DataBind();
        ddl_DataObject_Decision.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

    }
    #endregion

    private void BindData()
    {
        EWF_Flow_ProcessJointDecisionBLL bll = new EWF_Flow_ProcessJointDecisionBLL((Guid)ViewState["ProcessID"]);
        ViewState["Details"] = new ListTable<EWF_Flow_ProcessJointDecision_Recipients>(bll.Items, "RecipientRole");

        #region 绑定基本信息
        this.lb_ID.Text = ViewState["ProcessID"].ToString();
        tbx_Name.Text = bll.Model.Name;
        tbx_Description.Text = bll.Model.Description;
        ddl_DefaultNextProcess.SelectedValue = bll.Model.DefaultNextProcess.ToString();
        tbx_Sort.Text = bll.Model.Sort.ToString();
        #endregion

        tbx_TimeoutHours.Text = bll.Model.TimeoutHours.ToString();
        tbx_MessageSubject.Text = bll.Model.MessageSubject;
        
        ddl_NeedAllPositive.SelectedValue = bll.Model.NeedAllPositive;
        tbx_AtLeastPositiveNum.Text = bll.Model.AtLeastPositiveNum.ToString();
        ddl_NeedAllPositive_SelectedIndexChanged(null, null);
        
        ddl_PositiveNextProcess.SelectedValue = bll.Model.PositiveNextProcess.ToString();
        ddl_NegativeNextProcess.SelectedValue = bll.Model.NegativeNextProcess.ToString();

        BindGrid();
    }
    private void BindGrid()
    {
        ListTable<EWF_Flow_ProcessJointDecision_Recipients> items = (ListTable<EWF_Flow_ProcessJointDecision_Recipients>)ViewState["Details"];
        gv_List.BindGrid<EWF_Flow_ProcessJointDecision_Recipients>(items.GetListItem());
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ProcessID"] != Guid.Empty)//修改
        {
            EWF_Flow_ProcessJointDecisionBLL bll = new EWF_Flow_ProcessJointDecisionBLL((Guid)ViewState["ProcessID"]);

            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            bll.Model.Sort = int.Parse(tbx_Sort.Text);

            bll.Model.MessageSubject = tbx_MessageSubject.Text;
            bll.Model.Name = tbx_Name.Text;
            if (!string.IsNullOrEmpty(tbx_TimeoutHours.Text)) bll.Model.TimeoutHours = int.Parse(tbx_TimeoutHours.Text);

            bll.Model.NeedAllPositive = ddl_NeedAllPositive.SelectedValue;
            bll.Model.AtLeastPositiveNum = int.Parse(tbx_AtLeastPositiveNum.Text);
            bll.Model.PositiveNextProcess = new Guid(ddl_PositiveNextProcess.SelectedValue);
            bll.Model.NegativeNextProcess = new Guid(ddl_NegativeNextProcess.SelectedValue);

            bll.Update();

            #region 更新明细
            ListTable<EWF_Flow_ProcessJointDecision_Recipients> items = (ListTable<EWF_Flow_ProcessJointDecision_Recipients>)ViewState["Details"];
            foreach (EWF_Flow_ProcessJointDecision_Recipients item in items.GetListItem(ItemState.Added))
            {
                bll.AddDetail(item);
            }
            foreach (EWF_Flow_ProcessJointDecision_Recipients item in items.GetListItem(ItemState.Modified))
            {
                bll.UpdateDetail(item);
            }
            foreach (EWF_Flow_ProcessJointDecision_Recipients item in items.GetListItem(ItemState.Deleted))
            {
                bll.DeleteDetail(item.ID);
            }
            #endregion           
        }
        else//新增
        {
            EWF_Flow_ProcessJointDecisionBLL bll = new EWF_Flow_ProcessJointDecisionBLL();
            bll.Model.App = (Guid)ViewState["AppID"];
            bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
            bll.Model.Description = tbx_Description.Text;
            bll.Model.Sort = int.Parse(tbx_Sort.Text);
            bll.Model.Type = 10;

            bll.Model.MessageSubject = tbx_MessageSubject.Text;
            bll.Model.Name = tbx_Name.Text;
            if (!string.IsNullOrEmpty(tbx_TimeoutHours.Text)) bll.Model.TimeoutHours = int.Parse(tbx_TimeoutHours.Text);

            bll.Model.NeedAllPositive = ddl_NeedAllPositive.SelectedValue;
            bll.Model.AtLeastPositiveNum = int.Parse(tbx_AtLeastPositiveNum.Text);
            bll.Model.PositiveNextProcess = new Guid(ddl_PositiveNextProcess.SelectedValue);
            bll.Model.NegativeNextProcess = new Guid(ddl_NegativeNextProcess.SelectedValue);

            bll.Items = ((ListTable<EWF_Flow_ProcessJointDecision_Recipients>)ViewState["Details"]).GetListItem();
            bll.Add();
        }
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());

    }

    protected void ddl_RecipientRole_Decision_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_DataObject_Decision.SelectedValue = Guid.Empty.ToString();
        if (ddl_RecipientRole_Decision.SelectedValue != Guid.Empty.ToString())
        {
            int roletype = new EWF_RoleBLL(new Guid(ddl_RecipientRole_Decision.SelectedValue)).Model.Type;
            if (roletype == 3 || roletype == 6)
            {
                this.ddl_DataObject_Decision.Enabled = true;
            }
            else
            {
                this.ddl_DataObject_Decision.Enabled = false;
            }
        }
        else
            this.ddl_DataObject_Decision.Enabled = false;
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        new EWF_Flow_ProcessJointDecisionBLL((Guid)ViewState["ProcessID"]).Delete();
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
    }
    protected void ddl_NeedAllPositive_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_NeedAllPositive.SelectedValue == "Y")
        {
            tbx_AtLeastPositiveNum.Text = "0";
            tbx_AtLeastPositiveNum.Enabled = false;
        }
        else
        {
            tbx_AtLeastPositiveNum.Enabled = true;
        }
    }
    protected void btn_Save_Recipients_Click(object sender, EventArgs e)
    {
        ListTable<EWF_Flow_ProcessJointDecision_Recipients> items = (ListTable<EWF_Flow_ProcessJointDecision_Recipients>)ViewState["Details"];
        EWF_Flow_ProcessJointDecision_Recipients m;

        if (ViewState["SelectedRecipientRole"] == null)
            m = new EWF_Flow_ProcessJointDecision_Recipients();
        else
            m = items[ViewState["SelectedRecipientRole"].ToString()];

        m.Process = (Guid)ViewState["ProcessID"];
        m.RecipientRole = new Guid(ddl_RecipientRole_Decision.SelectedValue);
        m.DataObject = new Guid(ddl_DataObject_Decision.SelectedValue);
        m.Remark = tbx_Recipients_Remark.Text;

        if (m.RecipientRole == Guid.Empty)
        {
            MessageBox.Show(this, "请正确选择参与会审人员角色!");
            return;
        }

        if (ddl_DataObject_Decision.Enabled && m.DataObject == Guid.Empty)
        {
            MessageBox.Show(this, "请正确选择参与会审人员角色关联的数据对象!");
            return;
        }

        if (ViewState["SelectedRecipientRole"] == null)
            items.Add(m);
        else
        {
            items.Update(m);
            ViewState["SelectedRecipientRole"] = null;
            btn_Save_Recipients.Text = "添加会审角色";
        }
        BindGrid();
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid RecipientRole = (Guid)gv_List.DataKeys[e.RowIndex]["RecipientRole"];
        ListTable<EWF_Flow_ProcessJointDecision_Recipients> items = (ListTable<EWF_Flow_ProcessJointDecision_Recipients>)ViewState["Details"];

        items.Remove(RecipientRole.ToString());
        BindGrid();

        if (ViewState["SelectedRecipientRole"] != null)
        {
            ViewState["SelectedRecipientRole"] = null;
            btn_Save_Recipients.Text = "添加会审角色";
        }
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Guid RecipientRole = (Guid)gv_List.DataKeys[e.NewSelectedIndex]["RecipientRole"];
        ListTable<EWF_Flow_ProcessJointDecision_Recipients> items = (ListTable<EWF_Flow_ProcessJointDecision_Recipients>)ViewState["Details"];

        EWF_Flow_ProcessJointDecision_Recipients m = items[RecipientRole.ToString()];

        if (m != null)
        {
            ViewState["SelectedRecipientRole"] = RecipientRole;
            ddl_RecipientRole_Decision.SelectedValue = m.RecipientRole.ToString();
            ddl_RecipientRole_Decision_SelectedIndexChanged(null, null);
            ddl_DataObject_Decision.SelectedValue = m.DataObject.ToString();
            tbx_Recipients_Remark.Text = m.Remark;

            btn_Save_Recipients.Text = "修改会审角色";
        }
    }
}
