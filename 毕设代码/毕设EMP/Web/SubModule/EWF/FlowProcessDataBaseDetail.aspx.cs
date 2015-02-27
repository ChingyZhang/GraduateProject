// ===================================================================
// 文件路径:SubModule/RM/RetailerDetail.aspx.cs 
// 生成日期:2007-12-29 14:26:36 
// 作者:	  
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL;
using MCSFramework.Model.EWF;
using MCSFramework.Common;

public partial class SubModule_EWF_FlowProcessDataBaseDetail : System.Web.UI.Page
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
                BindGrid();
                this.bt_Save.Text = "修改";
                this.bt_Save.ForeColor = System.Drawing.Color.Red;
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


        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_ProcessType");
        ddl_Type.DataBind();
        ddl_Type.SelectedValue = "5";

        ddl_DataObject_DataBase.DataSource = app.GetDataObjectList();
        ddl_DataObject_DataBase.DataBind();
        ddl_DataObject_DataBase.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));
    }
    #endregion

    private void BindData()
    {
        EWF_Flow_ProcessDataBaseBLL bll = new EWF_Flow_ProcessDataBaseBLL((Guid)ViewState["ProcessID"]);

        this.lb_ID.Text = ViewState["ProcessID"].ToString();

        #region 绑定基本信息
        tbx_Name.Text = bll.Model.Name;
        tbx_Description.Text = bll.Model.Description;
        ddl_DefaultNextProcess.SelectedValue = bll.Model.DefaultNextProcess.ToString();
        tbx_Sort.Text = bll.Model.Sort.ToString();

        tbx_DSN.Text = bll.Model.DSN;
        tbx_StoreProcName.Text = bll.Model.StoreProcName;
        #endregion
    }

    private void BindGrid()
    {
        EWF_Flow_ProcessDataBaseBLL bll = new EWF_Flow_ProcessDataBaseBLL((Guid)ViewState["ProcessID"]);
        gv_List.BindGrid<EWF_Flow_DataBaseParam>(bll.GetParamsList());
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ProcessID"] == Guid.Empty)
        {
            Add();
        }
        else
        {
            Update();
        }
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());

    }

    protected void btn_Save_Param_Click(object sender, EventArgs e)
    {
        if ((Guid)ViewState["ProcessID"] == Guid.Empty)
        {
            Add();
        }
        EWF_Flow_DataBaseParamBLL bll;
        if (ViewState["SelectedID"] != null)
            bll = new EWF_Flow_DataBaseParamBLL((Guid)ViewState["SelectedID"]);    //修改
        else
            bll = new EWF_Flow_DataBaseParamBLL();

        bll.Model.Process = (Guid)ViewState["ProcessID"];

        if (!string.IsNullOrEmpty(ddl_DataObject_DataBase.SelectedValue) && ddl_DataObject_DataBase.SelectedValue != Guid.Empty.ToString())
        {
            bll.Model.IsDataObject = "Y";
            bll.Model.DataObject = new Guid(ddl_DataObject_DataBase.SelectedValue);
            bll.Model.ConstStrValue = "";
        }
        else
        {
            bll.Model.IsDataObject = "N";
            bll.Model.DataObject = Guid.Empty;
            bll.Model.ConstStrValue = tbx_ConstStrValue.Text;
        }

        bll.Model.IsOutput = ddl_IsOutput.SelectedValue;
        bll.Model.ParamName = tbx_ParamName.Text;

        if (ViewState["SelectedID"] == null)
            bll.Add();
        else
        {
            bll.Update();
            ViewState["SelectedID"] = null;
            btn_Save_Param.Text = "添 加";
        }
        BindGrid();

        tbx_ParamName.Text = "";
        tbx_ConstStrValue.Text = "";

    }


    private void Add()
    {
        EWF_Flow_ProcessDataBaseBLL bll = new EWF_Flow_ProcessDataBaseBLL();

        bll.Model.App = (Guid)ViewState["AppID"];
        if (!string.IsNullOrEmpty(ddl_DefaultNextProcess.SelectedValue)) bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
        bll.Model.Description = tbx_Description.Text;
        bll.Model.Sort = int.Parse(tbx_Sort.Text);
        bll.Model.Type = 5;

        bll.Model.DSN = tbx_DSN.Text;
        bll.Model.Name = tbx_Name.Text;
        bll.Model.StoreProcName = tbx_StoreProcName.Text;
        if (!string.IsNullOrEmpty(ddl_Type.SelectedValue)) bll.Model.Type = int.Parse(ddl_Type.SelectedValue);
        ViewState["ProcessID"] = bll.Add();
    }

    private void Update()
    {
        EWF_Flow_ProcessDataBaseBLL bll = new EWF_Flow_ProcessDataBaseBLL((Guid)ViewState["ProcessID"]);

        if (!string.IsNullOrEmpty(ddl_DefaultNextProcess.SelectedValue)) bll.Model.DefaultNextProcess = new Guid(ddl_DefaultNextProcess.SelectedValue);
        bll.Model.Description = tbx_Description.Text;
        bll.Model.Sort = int.Parse(tbx_Sort.Text);

        bll.Model.DSN = tbx_DSN.Text;
        bll.Model.Name = tbx_Name.Text;
        bll.Model.StoreProcName = tbx_StoreProcName.Text;
        bll.Update();
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        EWF_Flow_DataBaseParamBLL bll = new EWF_Flow_DataBaseParamBLL((Guid)this.gv_List.DataKeys[e.NewSelectedIndex]["ID"]);
        this.tbx_ParamName.Text = bll.Model.ParamName;
        this.ddl_IsOutput.SelectedValue = bll.Model.IsOutput;
        this.ddl_DataObject_DataBase.SelectedValue = bll.Model.DataObject.ToString();
        this.tbx_ConstStrValue.Text = bll.Model.ConstStrValue;
        ddl_DataObject_DataBase_SelectedIndexChanged(null, null);
        ViewState["SelectedID"] = bll.Model.ID;
        btn_Save_Param.Text = "修 改";

    }
    protected void ddl_DataObject_DataBase_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (new Guid(ddl_DataObject_DataBase.SelectedValue) == Guid.Empty)
        {
            tbx_ConstStrValue.Enabled = true;
        }
        else
        {
            tbx_ConstStrValue.Enabled = false;
        }
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid ID = (Guid)this.gv_List.DataKeys[e.RowIndex]["ID"];
        new EWF_Flow_DataBaseParamBLL(ID).Delete();
        BindGrid();
    }
    protected void bt_Del_Click(object sender, EventArgs e)
    {
        new EWF_Flow_ProcessDataBaseBLL((Guid)ViewState["ProcessID"]).Delete();
        Response.Redirect("FlowProcessList.aspx?AppID=" + ViewState["AppID"].ToString());
    }
}
