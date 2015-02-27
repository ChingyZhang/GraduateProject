using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MCSFramework.BLL;

/// <summary>
/// onlineperson 的摘要说明。
/// </summary>
public partial class OnlinePerson : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["SortField"] = "Position_Name";
            ViewState["SortDirect"] = "ASC";
            BindGrid();
        }

      

    }

    private void BindGrid()
    {
        DataTable dt = UserBLL.GetOnlineUserList();
        if (ViewState["Sort"] != null)
        {
            dt.DefaultView.Sort = ViewState["Sort"].ToString() + " " + ViewState["SortDirect"];
        }

        gv_List.DataSource = dt.DefaultView;
        gv_List.DataBind();
    }

    protected void btSendAllSM_Click(object sender, EventArgs e)
    {
        DataTable dt = UserBLL.GetOnlineUserList();

        if (dt.Rows.Count > 0)
        {
            string _sendto = "";
            string _sendtorealname = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _sendto += dt.Rows[i]["Username"].ToString() + ",";
                _sendtorealname += dt.Rows[i]["RealName"].ToString() + ",";
            }
            _sendto = _sendto.Substring(0, _sendto.Length - 1);
            _sendtorealname = _sendtorealname.Substring(0, _sendtorealname.Length - 1);

            Page.ClientScript.RegisterStartupScript(GetType(), "SendMsg", "<script language=javascript>SendMsg('" + _sendto + "','" + _sendtorealname + "');</script>");
        }
    }

    protected void gv_List_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["SortDirect"] != null)
        {
            if (ViewState["SortDirect"].ToString() == "DESC")
                ViewState["SortDirect"] = "ASC";
            else
                ViewState["SortDirect"] = "DESC";
        }
        else
            ViewState["SortDirect"] = "ASC";

        ViewState["SortField"] = (string)e.SortExpression;

        ViewState["Sort"] = e.SortExpression;
        BindGrid();
    }
}
