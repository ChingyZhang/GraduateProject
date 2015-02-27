using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.QNA;
using MCSFramework.Model.QNA;
using MCSFramework.Common;

public partial class SubModule_Service_QNA_QNA_DisplayResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Result"] = Request.QueryString["Result"] != null ? int.Parse(Request.QueryString["Result"]) : 0;
            #endregion

            #region 判断参数是否正确
            if ((int)ViewState["Result"] == 0)
            {
                MessageBox.ShowAndClose(this, "对不起，参数中必须有指定的调研问卷结果Result！");
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
        QNA_ResultBLL r = new QNA_ResultBLL((int)ViewState["Result"]);
        if (r.Model != null)
        {
            QNA_Project p = new QNA_ProjectBLL(r.Model.Project).Model;
            lb_ProjectName.Text = p.Name;

            Repeater1.DataSource = QNA_QuestionBLL.GetModelList("Project=" + r.Model.Project.ToString() + " ORDER BY ID");
            Repeater1.DataBind();

            foreach (RepeaterItem _ri in Repeater1.Items)
            {
                int question = int.Parse(((Label)_ri.FindControl("lb_ID")).Text);
                RadioButtonList rbl_Result = (RadioButtonList)_ri.FindControl("rbl_Result");
                CheckBoxList cbl_Result = (CheckBoxList)_ri.FindControl("cbl_Result");
                TextBox tbx_Result = (TextBox)_ri.FindControl("tbx_Result");

                foreach (QNA_Result_Detail _d in r.Items.Where<QNA_Result_Detail>(d => d.Question == question))
                {
                    if (_d.Option != 0)
                    {
                        if (rbl_Result.Visible)
                            rbl_Result.SelectedValue = _d.Option.ToString();
                        else if (cbl_Result.Visible)
                        {
                            foreach (ListItem item in cbl_Result.Items)
                            {
                                if (item.Value == _d.Option.ToString()) item.Selected = true;
                            }
                        }
                    }

                    if (_d.OptionText != "")
                    {
                        tbx_Result.Visible = true;
                        tbx_Result.Text = _d.OptionText;
                    }
                }
                
            }
        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int id = int.Parse(((Label)e.Item.FindControl("lb_ID")).Text);
            QNA_QuestionBLL q = new QNA_QuestionBLL(id);

            RadioButtonList rbl_Result = (RadioButtonList)e.Item.FindControl("rbl_Result");
            CheckBoxList cbl_Result = (CheckBoxList)e.Item.FindControl("cbl_Result");
            TextBox tbx_Result = (TextBox)e.Item.FindControl("tbx_Result");

            if (q.Model != null)
            {
                switch (q.Model.OptionMode)
                {
                    case 1:                 //单选
                        rbl_Result.DataSource = q.Items;
                        rbl_Result.DataBind();
                        rbl_Result.Visible = true;
                        break;
                    case 2:                 //多选
                        cbl_Result.DataSource = q.Items;
                        cbl_Result.DataBind();
                        cbl_Result.Visible = true;
                        break;
                    case 3:                 //输入文本
                        tbx_Result.Visible = true;
                        break;
                }
            }
        }
    }

    #region 单选及多选控件被选择事件,不允许变更选项
    protected void cbl_Result_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void rbl_Result_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    #endregion

}
