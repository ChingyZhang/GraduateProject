using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSFramework.BLL;

public partial class SubModule_LeftTreeMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenarateMainMenu();
        }
    }

    #region 生成主菜单
    protected void GenarateMainMenu()
    {
        #region 生成主菜单datatable
        DataTable dt = Right_Module_BLL.GetBrowseMoudleByUser(Session["UserName"].ToString());
        //DataTable dt_main = Right_Module_BLL.GetMainMenu(Session["UserName"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            //wc.DataSource = dt_main;
            tr_List.Nodes.Clear();
            BindModuleTree(tr_List.Nodes, "1", dt);
        }
        else
            Response.Write("对不起,当前用户没有分配任何浏览权限!");
        #endregion
    }
    private void BindModuleTree(TreeNodeCollection TNC, string SuperID, DataTable dtMenu)
    {
        if (dtMenu != null && !string.IsNullOrEmpty(SuperID))
        {
            DataTable dt_menusuper = null;
            if (Session["ActiveModule"] != null)
            {
                dt_menusuper = TreeTableBLL.GetAllSuperNodeIDs("MCS_SYS.dbo.Right_Module", "SuperID", "ID", Session["ActiveModule"].ToString());
            }
            foreach (DataRow _row in dtMenu.Select("SuperID=" + SuperID))
            {
                TreeNode tn = new TreeNode();
                tn.Text = "<span style=font-size:14px;>" + _row["Name"].ToString() + "</span>";
                tn.Value = _row["ID"].ToString();
                tn.ToolTip = _row["Name"].ToString();
                if (SuperID == "1")
                {
                    tn.Text = "<div style='font-size:14px; background-color:#CCCCCC; width:140px;height:24px;line-height:24px;'>" + _row["Name"].ToString() + "</div>";                   
                }
                if (dt_menusuper != null && dt_menusuper.Select("SuperID=" + tn.Value).Length > 0)
                {
                    tn.ExpandAll();
                }
                if (dtMenu.Select("SuperID=" + tn.Value).Length == 0)
                {
                    tn.NavigateUrl = "~/SubModule/switch.aspx?Action=1&Module=" + tn.Value;
                    tn.Target = "_top";
                }
                else
                {
                    tn.NavigateUrl = "#";
                    //tn.Target = "_top";
                }
                TNC.Add(tn);
                BindModuleTree(tn.ChildNodes, _row["ID"].ToString(), dtMenu);
            }
        }

    }
    #endregion
}
