using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.QNA;

public partial class SubModule_Service_QNA_QNA_ProjectListReferenceFaceTo : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //*******************
            //1.获取面向对象类别
            ViewState["FaceTo"] = Request.QueryString["FaceTo"] == null ? "0" : Request.QueryString["FaceTo"].ToString();
            //2.根据登陆人，面向对象类别和面向对象管理片区显示和其相关的问卷

            //****************
            BindDropDown();

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }
    #endregion

    private void BindGrid()
    {
        DateTime dtBegin = DateTime.Parse(this.tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(this.tbx_end.Text).AddDays(1);
        string ConditionStr = " 1 = 1 ";

        ConditionStr += " AND MCS_QNA.dbo.QNA_Project.InsertTime between ' " + dtBegin.ToShortDateString() + " 'and '" + dtEnd.ToShortDateString() + "' ";

        if (ViewState["FaceTo"] != null)
        {
            ConditionStr += " AND MCS_QNA.dbo.QNA_Project.FaceTo =" + ViewState["FaceTo"].ToString();

            switch (ViewState["FaceTo"].ToString())
            {
                case "1":
                    ConditionStr += " AND((ToAllStaff ='N'AND ((MCS_QNA.dbo.QNA_Project.InsertStaff ='" + Session["UserID"].ToString() + "')  OR EXISTS(SELECT 1 FROM MCS_QNA.dbo.QNA_ToPosition WHERE ProjectID = MCS_QNA.dbo.QNA_Project.ID AND Position =(SELECT  Position FROM MCS_SYS.dbo.Org_Staff  WHERE ID ='" + Session["UserID"].ToString() + "'))))"
                                   +" OR (ToAllStaff ='Y'))"
                                   + "AND ((ToAllOrganizeCity ='N' AND ((MCS_QNA.dbo.QNA_Project.InsertStaff ='" + Session["UserID"].ToString() + "') OR  EXISTS(SELECT 1 FROM MCS_QNA.dbo.QNA_ToOrganizeCity  WHERE ProjectID = MCS_QNA.dbo.QNA_Project.ID AND OrganizeCity =(SELECT OrganizeCity  FROM MCS_SYS.dbo.Org_Staff WHERE ID = '" + Session["UserID"].ToString() + "'))))"
                                   +"OR (ToAllOrganizeCity ='Y'))";
                    break;
                default:
                    ConditionStr += "AND ((ToAllStaff ='N'AND ( EXISTS(SELECT 1 FROM MCS_QNA.dbo.QNA_ToPosition WHERE ProjectID = MCS_QNA.dbo.QNA_Project.ID AND Position =(SELECT  Position FROM MCS_SYS.dbo.Org_Staff  WHERE ID ='" + Session["UserID"].ToString() + "'))))"
                                 + " OR (ToAllStaff ='Y'))"
                                 + "AND ((ToAllOrganizeCity ='N' AND ( EXISTS(SELECT 1 FROM MCS_QNA.dbo.QNA_ToOrganizeCity  WHERE ProjectID = MCS_QNA.dbo.QNA_Project.ID AND OrganizeCity =(SELECT OrganizeCity  FROM MCS_SYS.dbo.Org_Staff WHERE ID = '" + Session["UserID"].ToString() + "'))))"
                                 + "OR (ToAllOrganizeCity ='Y'))";
                    break;
            }
        }

        if (MCSTabControl2.SelectedTabItem.Value == "0")
        {
            ConditionStr += "AND  CloseTime >='" + DateTime.Now.ToShortDateString() + "'AND MCS_QNA.dbo.QNA_Project.Enabled ='Y'";
        }
        if (MCSTabControl2.SelectedTabItem.Value == "1")
        {
            ConditionStr += "AND CloseTime <= '" + DateTime.Now.ToShortDateString() + "' AND MCS_QNA.dbo.QNA_Project.Enabled ='Y'";
        }
        if (MCSTabControl2.SelectedTabItem.Value == "2")
        {
            ConditionStr += "AND MCS_QNA.dbo.QNA_Project.Enabled ='N'";
        }

        if (tbx_Search.Text.Trim() != "")
        {
            ConditionStr += " AND  MCS_QNA.dbo.QNA_Project.Name  like '%"+tbx_Search.Text.Trim()+"%'";
        }

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }

   
    protected string GetResultCount(int project)
    {
        return new QNA_ProjectBLL(project).GetResultCount().ToString();
    }


    protected void MCSTabControl2_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.item.Value == "1") //已经完成的问卷，
        {
            bt_FillQuestionnair.Visible = false;
            bt_ScanQuestionnair.Visible = true;
        }

        if (e.item.Value == "0") //进行中的问卷，
        {
            bt_FillQuestionnair.Visible = true;
            bt_ScanQuestionnair.Visible = false;
        }

        if (e.item.Value == "3") //已取消的问卷，
        {
            bt_FillQuestionnair.Visible = false;
            bt_ScanQuestionnair.Visible = false;
        }

        gv_List.PageIndex = 0;
        BindGrid();
    }


    #region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Session["ProjectID"] = _id;
    }
    #endregion

    /// <summary>
    /// 填写问卷
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bt_FillQuestionnair_Click(object sender, EventArgs e)
    {
        if (int.Parse(Session["ProjectID"].ToString()) > 0)
        {
            Response.Redirect("QNA_FillQuestionnairA.aspx?Project=" + Session["ProjectID"].ToString());
        }
        else
        {
            MessageBox.Show(this, "必须选择某个问卷！");
            return;
        }
    }

    /// <summary>
    /// 查看问卷结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bt_ScanQuestionnair_Click(object sender, EventArgs e)
    {
        if (int.Parse(Session["ProjectID"].ToString()) > 0)
        {
            Response.Redirect("QNA_ProjectStatistics.aspx?Project=" + Session["ProjectID"].ToString());
        }
        else
        {
            MessageBox.Show(this, "必须选择某个问卷！");
            return;
        }

    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
}
