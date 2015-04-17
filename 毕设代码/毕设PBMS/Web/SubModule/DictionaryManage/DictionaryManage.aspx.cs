using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using MCSFramework.Common;
using MCSFramework.BLL;

public partial class SubModule_DictionaryManage_DictionaryManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BindDicType();

            ViewState["Page"] = 0;
            BindGrid();

        }
    }

    #region BindDropDownList

    #region 绑定字典类型
    private void BindDicType()
    {
        this.ddl_DicType.DataSource = DictionaryBLL.Dictionary_Type_GetAllList();
        this.ddl_DicType.DataBind();
    }
    #endregion

    #endregion

    #region BindGrid
    private void BindGrid()
    {
        //try
        {
            Dictionary<string, MCSFramework.Model.Dictionary_Data> li = DictionaryBLL.GetDicCollections(int.Parse(ddl_DicType.SelectedValue), false);

            if (ViewState["Page"] != null)
                gv_List.PageIndex = (int)ViewState["Page"];

            gv_List.DataSource = li.Values;
            gv_List.DataBind();

            lb_rowcount.Text = li.Count.ToString();
            tbx_PageGo.Text = (gv_List.PageIndex + 1).ToString();

            tbx_name.Text = "";
            tbx_SelfNo.Text = "";
            bt_Save.Text = "添 加";
            bt_Save.ForeColor = System.Drawing.Color.Black;
            ViewState["SelectedID"] = Guid.Empty;
        }
        //catch { }
    }
    #endregion


    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    #region 添加、修改
    protected void bt_Save_Click(object sender, System.EventArgs e)
    {
        DictionaryBLL dbl;
        Guid selectedid = (Guid)ViewState["SelectedID"];

        if (selectedid == Guid.Empty)
            dbl = new DictionaryBLL();
        else
            dbl = new DictionaryBLL(selectedid);


        dbl.Model.Type = int.Parse(this.ddl_DicType.SelectedValue);
        dbl.Model.Name = tbx_name.Text;
        dbl.Model.Code = this.tbx_SelfNo.Text;
        dbl.Model.Enabled = cb_Enabled.Checked ? "Y" : "N";
        dbl.Model.Description = tbx_Description.Text;

        if (selectedid == Guid.Empty)//添加
        {
            try
            {
                dbl.Model.InsertUser = Session["UserName"].ToString();
                if (dbl.Add() < 0)
                    MessageBox.Show(this, "您使用的自定义编号已被使用，请更改！");
            }
            catch
            {
                MessageBox.Show(this, "数据库执行出错！");
                return;
            }
        }
        else//修改
        {
            try
            {
                dbl.Model.UpdateUser = Session["UserName"].ToString();
                if (dbl.Update() < 0)
                    MessageBox.Show(this, "您使用的自定义编号已被使用或该信息正在业务中使用！");
            }
            catch
            {
                MessageBox.Show(this, "数据库执行出错！");
                return;
            }
        }

        DataCache.RemoveCache("Cache-Dictionary_Data-Enabled-" + true.ToString());
        DataCache.RemoveCache("Cache-Dictionary_Data-Enabled-" + false.ToString());
        BindGrid();


    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = (Guid)gv_List.DataKeys[e.RowIndex]["ID"];

        new DictionaryBLL(id).Delete();

        DataCache.RemoveCache("Cache-Dictionary_Data-Enabled-" + true.ToString());
        DataCache.RemoveCache("Cache-Dictionary_Data-Enabled-" + false.ToString());
        BindGrid();
    }
    #endregion


    protected void ddl_DicType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ViewState["Page"] = 0;
        BindGrid();
    }

    #region 翻页事件
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["Page"] = e.NewPageIndex;
        BindGrid();
    }

    protected void bt_PageOk_Click(object sender, EventArgs e)
    {
        int _page = Int32.Parse(tbx_PageGo.Text) - 1;
        if (_page >= 0 && _page <= gv_List.PageCount - 1)
        {
            ViewState["Page"] = _page;
            BindGrid();
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PageError", "<script language='javascript'>alert('页码范围无效!');</script>");
            tbx_PageGo.Text = ((int)ViewState["Page"] + 1).ToString();
        }
    }
    #endregion

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Guid id = (Guid)gv_List.DataKeys[e.NewSelectedIndex]["ID"];

        DictionaryBLL bll = new DictionaryBLL(id);

        if (bll.Model.ID != Guid.Empty)
        {
            bt_Save.Text = "修改";
            bt_Save.ForeColor = System.Drawing.Color.Red;

            tbx_SelfNo.Text = bll.Model.Code;
            tbx_name.Text = bll.Model.Name;
            cb_Enabled.Checked = (bll.Model.Enabled == "Y");
            tbx_Description.Text = bll.Model.Description;

            ViewState["SelectedID"] = id;
        }
    }

   
}

