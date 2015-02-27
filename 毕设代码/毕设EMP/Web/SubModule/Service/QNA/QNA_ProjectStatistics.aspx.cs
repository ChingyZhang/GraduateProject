using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.QNA;
using MCSFramework.Model.QNA;
using MCSFramework.Common;
using System.Data;

public partial class SubModule_Service_QNA_QNA_ProjectStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Project"] = Request.QueryString["Project"] != null ? int.Parse(Request.QueryString["Project"]) : 0;
            #endregion

            #region 判断参数是否正确
            if ((int)ViewState["Project"] == 0)
            {
                MessageBox.ShowAndClose(this, "对不起，参数中必须有指定的调研问卷结果Project！");
                return;
            }
            else
            {
                BindData();
            }
            #endregion
        }
    }

    private void BindData()
    {
        QNA_ProjectBLL p = new QNA_ProjectBLL((int)ViewState["Project"]);
        lb_ProjectName.Text = p.Model.Name;
        ViewState["ResultStatistics"] = p.GetResultStatistics();
        lb_TotalCount.Text = p.GetResultCount().ToString();

        if (MCSTabControl1.SelectedIndex == 0)
        {
            rpt_Question.DataSource = QNA_QuestionBLL.GetModelList("Project=" + p.Model.ID.ToString() + " ORDER BY SortCode");
            rpt_Question.DataBind();

            rpt_Question.Visible = true;
            gv_List.Visible = false;
        }
        else
        {
            gv_List.DataSource = (DataTable)ViewState["ResultStatistics"];
            gv_List.DataBind();

            rpt_Question.Visible = false;
            gv_List.Visible = true;
        }
    }

    protected void rpt_Question_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int id = int.Parse(((Label)e.Item.FindControl("lb_ID")).Text);
            QNA_QuestionBLL q = new QNA_QuestionBLL(id);

            Repeater rpt_Option = (Repeater)e.Item.FindControl("rpt_Option");
            rpt_Option.DataSource = q.Items;
            rpt_Option.DataBind();

            foreach (RepeaterItem _ri in rpt_Option.Items)
            {
                #region 绑定选项统计结果及柱形条长度
                DataTable dt_result = (DataTable)ViewState["ResultStatistics"];
                int option = int.Parse(((Label)_ri.FindControl("lb_OptionID")).Text);
                Label lb_Bar = (Label)_ri.FindControl("lb_Bar");
                Label lb_Counts = (Label)_ri.FindControl("lb_Counts");

                int _maxcounts = 0, _counts = 0;
                DataRow[] _rows = dt_result.Select("QuestionID = " + id.ToString());
                foreach (DataRow _r in _rows)
                {
                    if ((int)_r["OptionID"] == option) _counts = (int)_r["Counts"];
                    if ((int)_r["Counts"] > _maxcounts) _maxcounts = (int)_r["Counts"];
                }

                lb_Counts.Text = _counts.ToString();
                if (_counts > 0)
                {
                    lb_Bar.Width = new Unit(400 * _counts / _maxcounts);
                    lb_Bar.Text = ".";
                }
                #endregion

            }
        }
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindData();

    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        gv_List.DataSource = (DataTable)ViewState["ResultStatistics"];
        gv_List.DataBind();
    }
}
