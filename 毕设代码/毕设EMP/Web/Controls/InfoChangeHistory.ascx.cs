using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model;
using MCSFramework.BLL;
using System.Data;
using MCSFramework.Common;

public partial class Controls_InfoChangeHistory : System.Web.UI.UserControl
{
    
    #region Property
    public string TableName
    {
        get
        {
            if (ViewState["TableName"] == null)
                return "";
            return ViewState["TableName"].ToString();
        }
        set
        {
            ViewState["TableName"] = value;
        }
    }

    public int InfoID
    {
        get
        {
            if (ViewState["InfoID"] == null)
                return 0;
            return (int)ViewState["InfoID"];
        }
        set
        {
            ViewState["InfoID"] = value;

        }
    }

    public string Message
    {
        get
        {
            if (ViewState["Message"] == null)
                return "";
            return ViewState["Message"].ToString();
        }
        set
        {
            ViewState["Message"] = value;
        }
    }

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ViewState["TableName"] == null)
            {
                return;
            }
            BindDropDown();
            if (ViewState["Message"] != null)
            {
                lbl_Message.Text = ViewState["Message"].ToString();
            }
            BindGrid();
        }
    }

    private void BindDropDown()
    {
        if (ViewState["TableName"]!=null)
        {
            IList<UD_TableList> tablelist = UD_TableListBLL.GetModelList("Name='" + ViewState["TableName"].ToString() + "'");
           
            if (tablelist.Count > 0)
            {
                ViewState["TableID"] = tablelist[0].ID;
                IList<UD_ModelFields> fieldlist = UD_ModelFieldsBLL.GetModelList("Tableid='" + tablelist[0].ID + "'");
                ddl_FieldName.DataSource = fieldlist;
                ddl_FieldName.DataTextField ="DisplayName";
                ddl_FieldName.DataValueField ="ID";
                ddl_FieldName.DataBind();
                ddl_FieldName.Items.Insert(0, new ListItem("所有", "0"));
            }
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        if (ViewState["TableID"] == null) return;
        string condition = "Info_ChangeHistory.TableID='" + ViewState["TableID"].ToString() + "' AND InfoID=" + InfoID.ToString();
        if (ddl_FieldName.SelectedValue != "0")
        {
            condition += " AND Info_ChangeHistory.FieldID='" + ddl_FieldName.SelectedValue + "'";
        }
        gv_List.ConditionString = condition + " Order By FieldID,ChangeTime";
        gv_List.BindGrid();
       
    }
}

 