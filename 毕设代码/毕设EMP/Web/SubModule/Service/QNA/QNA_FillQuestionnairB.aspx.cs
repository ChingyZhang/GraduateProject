using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.QNA;
using MCSFramework.Model.QNA;
using MCSFramework.Common;

public partial class SubModule_Service_QNA_QNA_FillQuestionnairB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Project"] = Request.QueryString["Project"] != null ? int.Parse(Request.QueryString["Project"]) : 0;
            ViewState["RelateClient"] = Request.QueryString["RelateClient"] != null ? int.Parse(Request.QueryString["RelateClient"]) : 0;
            ViewState["RelateTask"] = Request.QueryString["RelateTask"] != null ? int.Parse(Request.QueryString["RelateTask"]) : 0;
            #endregion

            #region 验证问卷启用标及及出入口
            if ((int)ViewState["Project"] == 0)
            {
                MessageBox.ShowAndClose(this, "对不起，参数中必须有指定的调研问卷Project！");
                return;
            }
            else
            {
                QNA_Project p = new QNA_ProjectBLL((int)ViewState["Project"]).Model;
                if (p == null || p.Enabled == "N")
                {
                    MessageBox.ShowAndClose(this, "对不起，该调研问卷未启用！");
                    return;
                }
                else
                {
                    lb_ProjectName.Text = p.Name;

                    if (p.DisplayType == 1)     //跳转模式问卷
                    {
                        Response.Redirect("QNA_FillQuestionnairA.aspx" + Request.Url.Query);
                        return;
                    }
                }
            }
            #endregion

            BindRepeater();
        }
    }

    private void BindRepeater()
    {
        if ((int)ViewState["Project"] != 0)
        {
            Repeater1.DataSource = QNA_QuestionBLL.GetModelList("Project=" + ViewState["Project"].ToString() + " ORDER BY SortCode");
            Repeater1.DataBind();
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
            RegularExpressionValidator rev_Result = (RegularExpressionValidator)e.Item.FindControl("rev_Result");
            rev_Result.ValidationExpression = q.Model.TextRegularExp;

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
                        if (rev_Result.ValidationExpression != "") rev_Result.Enabled = true;
                        break;
                }
            }
        }
    }

    #region 单选及多选控件被选择事件,判断是否出现其他文本框
    protected void cbl_Result_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lb_ID = (Label)((CheckBoxList)sender).Parent.FindControl("lb_ID");
        RadioButtonList rbl_Result = (RadioButtonList)((CheckBoxList)sender).Parent.FindControl("rbl_Result");
        CheckBoxList cbl_Result = (CheckBoxList)((CheckBoxList)sender).Parent.FindControl("cbl_Result");
        TextBox tbx_Result = (TextBox)((CheckBoxList)sender).Parent.FindControl("tbx_Result");
        RegularExpressionValidator rev_Result = (RegularExpressionValidator)((CheckBoxList)sender).Parent.FindControl("rev_Result");

        tbx_Result.Visible = false;
        rev_Result.Enabled = false;
        foreach (ListItem item in cbl_Result.Items)
        {
            if (item.Selected)
            {
                QNA_QuestionOption option = new QNA_QuestionBLL(int.Parse(lb_ID.Text)).GetDetailModel(int.Parse(item.Value));
                if (option.CanInputText == "Y")
                {
                    tbx_Result.Visible = true;
                    if (rev_Result.ValidationExpression != "") rev_Result.Enabled = true;
                    break;
                }
            }
        }
    }
    protected void rbl_Result_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lb_ID = (Label)((RadioButtonList)sender).Parent.FindControl("lb_ID");
        RadioButtonList rbl_Result = (RadioButtonList)((RadioButtonList)sender).Parent.FindControl("rbl_Result");
        CheckBoxList cbl_Result = (CheckBoxList)((RadioButtonList)sender).Parent.FindControl("cbl_Result");
        TextBox tbx_Result = (TextBox)((RadioButtonList)sender).Parent.FindControl("tbx_Result");
        RegularExpressionValidator rev_Result = (RegularExpressionValidator)((RadioButtonList)sender).Parent.FindControl("rev_Result");

        if (rbl_Result.SelectedValue != "")
        {
            QNA_QuestionOption option = new QNA_QuestionBLL(int.Parse(lb_ID.Text)).GetDetailModel(int.Parse(rbl_Result.SelectedValue));
            if (option.CanInputText == "Y")
            {
                tbx_Result.Visible = true;
                if (rev_Result.ValidationExpression != "") rev_Result.Enabled = true;
            }
            else
            {
                tbx_Result.Visible = false;
                rev_Result.Enabled = false;
            }
        }
    }
    #endregion

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["Project"] != 0)
        {
            QNA_ResultBLL r = new QNA_ResultBLL();
            r.Model.Project = (int)ViewState["Project"];
            r.Model.RelateClient = (int)ViewState["RelateClient"];
            r.Model.RelateTask = (int)ViewState["RelateTask"];
            r.Model.InsertStaff = (int)Session["UserID"];

            foreach (RepeaterItem _ri in Repeater1.Items)
            {
                int question = int.Parse(((Label)_ri.FindControl("lb_ID")).Text);
                RadioButtonList rbl_Result = (RadioButtonList)_ri.FindControl("rbl_Result");
                CheckBoxList cbl_Result = (CheckBoxList)_ri.FindControl("cbl_Result");
                TextBox tbx_Result = (TextBox)_ri.FindControl("tbx_Result");

                if (cbl_Result.Visible && cbl_Result.SelectedValue != "")
                {
                    foreach (ListItem item in cbl_Result.Items)
                    {
                        if (item.Selected)
                        {
                            QNA_Result_Detail _resultdetail = new QNA_Result_Detail();
                            _resultdetail.Question = question;
                            _resultdetail.Option = int.Parse(item.Value);
                            r.Items.Add(_resultdetail);
                        }
                    }
                }

                if (rbl_Result.Visible)
                {
                    if (rbl_Result.SelectedValue == "")
                    {
                        MessageBox.Show(this, "单选列表中，您必须选择一项!");
                        rbl_Result.Focus();
                        return;
                    }
                    else
                    {
                        QNA_Result_Detail _resultdetail = new QNA_Result_Detail();
                        _resultdetail.Question = question;
                        _resultdetail.Option = int.Parse(rbl_Result.SelectedValue);
                        r.Items.Add(_resultdetail);
                    }
                }

                if (tbx_Result.Visible)
                {
                    if (tbx_Result.Text == "")
                    {
                        MessageBox.Show(this, "文本框中，您必须填写内容!");
                        tbx_Result.Focus();
                        return;
                    }
                    else
                    {
                        QNA_Result_Detail _resultdetail = new QNA_Result_Detail();
                        _resultdetail.Question = question;
                        _resultdetail.OptionText = tbx_Result.Text;
                        r.Items.Add(_resultdetail);
                    }
                }
            }

            Session["QuestionnaireResult"] = r.Add();

            MessageBox.ShowAndClose(this, "问卷保存成功!");
        }
    }
}
