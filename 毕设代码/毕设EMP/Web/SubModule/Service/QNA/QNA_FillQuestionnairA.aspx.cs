using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.QNA;
using MCSFramework.Model.QNA;
using System.Collections;

public partial class SubModule_Service_QNA_QNA_FillQuestionnairA : System.Web.UI.Page
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

            ViewState["PreviousQuestion"] = new Stack<int>();       //上一题堆栈
            ViewState["CurrentQuestion"] = 0;
            ViewState["NextQuestion"] = 0;
            ViewState["LastQuestion"] = 0;
            ViewState["ResultDetails"] = new List<QNA_Result_Detail>();

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

                    if (p.DisplayType == 2)     //顺序模式问卷
                    {
                        Response.Redirect("QNA_FillQuestionnairB.aspx" + Request.Url.Query);
                        return;
                    }

                    IList<QNA_Question> firstq = QNA_QuestionBLL.GetModelList("Project=" + p.ID.ToString() + " AND IsFirstQ='Y'");
                    if (firstq.Count != 1)
                    {
                        MessageBox.ShowAndClose(this, "对不起，该调研问卷没有正确的入口！");
                        return;
                    }
                    else
                    {
                        BindQuestion(firstq[0].ID);
                    }

                    IList<QNA_Question> lastq = QNA_QuestionBLL.GetModelList("Project=" + p.ID.ToString() + " AND IsLastQ='Y'");
                    if (lastq.Count != 1)
                    {
                        MessageBox.ShowAndClose(this, "对不起，该调研问卷没有正确的出口！");
                        return;
                    }
                    else
                    {
                        ViewState["LastQuestion"] = lastq[0].ID;
                    }

                }
            }
            #endregion

        }
    }

    #region 绑定当前要显示的问卷标题
    private void BindQuestion(int question)
    {
        if (question != 0)
        {
            ViewState["CurrentQuestion"] = question;
            QNA_QuestionBLL q = new QNA_QuestionBLL(question);
            if (q != null)
            {
                lb_QuestionTitle.Text = q.Model.Title;
                lb_QuestionDescription.Text = q.Model.Description;
                ViewState["TextRegularExp"] = q.Model.TextRegularExp;

                cbl_Result.Items.Clear();
                rbl_Result.Items.Clear();
                tbx_Result.Text = "";

                cbl_Result.Visible = false;
                rbl_Result.Visible = false;
                tbx_Result.Visible = false;

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
                    case 3:
                        tbx_Result.Visible = true;
                        break;
                }

                if (q.Model.DefaultNextQ != 0)
                {
                    ViewState["NextQuestion"] = q.Model.DefaultNextQ;
                    lb_NextQuestion.Text = new QNA_QuestionBLL(q.Model.DefaultNextQ).Model.Title;
                }

                rev_Result.ValidationExpression = q.Model.TextRegularExp;

                if (((Stack<int>)ViewState["PreviousQuestion"]).Count == 0)
                    bt_Previous.Visible = false;
                else
                    bt_Previous.Visible = true;

                #region 问卷出口时的界面控制
                if (q.Model.IsLastQ == "Y")
                {
                    bt_Next.Visible = false;
                    bt_ToEnd.Visible = false;

                    bt_Save.Visible = true;
                    bt_Cancel.Visible = true;
                    ViewState["NextQuestion"] = 0;
                }
                else
                {
                    bt_Next.Visible = true;
                    bt_ToEnd.Visible = true;

                    bt_Save.Visible = false;
                    bt_Cancel.Visible = false;
                }
                #endregion

            }
        }
    }
    #endregion

    #region 单选及多选控件被选择事件
    protected void cbl_Result_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbx_Result.Visible = false;
        rev_Result.Enabled = false;
        foreach (ListItem item in cbl_Result.Items)
        {
            if (item.Selected)
            {
                QNA_QuestionOption option = new QNA_QuestionBLL((int)ViewState["CurrentQuestion"]).GetDetailModel(int.Parse(item.Value));
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
        if (rbl_Result.SelectedValue != "")
        {
            QNA_QuestionOption option = new QNA_QuestionBLL((int)ViewState["CurrentQuestion"]).GetDetailModel(int.Parse(rbl_Result.SelectedValue));
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

            if (option.NextQuestion != 0 && (int)ViewState["CurrentQuestion"] != (int)ViewState["LastQuestion"])
                ViewState["NextQuestion"] = option.NextQuestion;
        }
    }
    #endregion

    #region 下一题
    protected void bt_Next_Click(object sender, EventArgs e)
    {
        List<QNA_Result_Detail> _details = new List<QNA_Result_Detail>();
        if (cbl_Result.Visible && cbl_Result.SelectedValue != "")
        {
            foreach (ListItem item in cbl_Result.Items)
            {
                if (item.Selected)
                {
                    QNA_Result_Detail _resultdetail = new QNA_Result_Detail();
                    _resultdetail.Question = (int)ViewState["CurrentQuestion"];
                    _resultdetail.Option = int.Parse(item.Value);
                    _details.Add(_resultdetail);
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
                _resultdetail.Question = (int)ViewState["CurrentQuestion"];
                _resultdetail.Option = int.Parse(rbl_Result.SelectedValue);
                _details.Add(_resultdetail);
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
                _resultdetail.Question = (int)ViewState["CurrentQuestion"];
                _resultdetail.OptionText = tbx_Result.Text;
                _details.Add(_resultdetail);
            }
        }

        foreach (QNA_Result_Detail item in _details)
        {
            ((List<QNA_Result_Detail>)ViewState["ResultDetails"]).Add(item);
        }

        ((Stack<int>)ViewState["PreviousQuestion"]).Push((int)ViewState["CurrentQuestion"]);
        BindQuestion((int)ViewState["NextQuestion"]);
    }
    #endregion

    #region 上一题
    protected void bt_Previous_Click(object sender, EventArgs e)
    {
        if (((Stack<int>)ViewState["PreviousQuestion"]).Count != 0)
        {
            int _PreviousQuestion = ((Stack<int>)ViewState["PreviousQuestion"]).Pop();

            BindQuestion(_PreviousQuestion);

            #region 绑定显示并清除原有的选项
            List<QNA_Result_Detail> ResultDetails = (List<QNA_Result_Detail>)ViewState["ResultDetails"];
            for (int i = ResultDetails.Count - 1; i >= 0; i--)
            {
                if (ResultDetails[i].Question == _PreviousQuestion)
                {
                    if (ResultDetails[i].Option != 0)
                    {
                        if (cbl_Result.Visible)
                        {
                            foreach (ListItem item in cbl_Result.Items)
                            {
                                if (item.Value == ResultDetails[i].Option.ToString()) item.Selected = true;
                            }
                        }
                        if (rbl_Result.Visible) rbl_Result.SelectedValue = ResultDetails[i].Option.ToString();
                    }
                    else if (ResultDetails[i].OptionText != "")
                    {
                        tbx_Result.Text = ResultDetails[i].OptionText;
                        tbx_Result.Visible = true;
                    }
                    ResultDetails.RemoveAt(i);
                }
            }
            #endregion
        }
    }
    #endregion

    protected void bt_ToEnd_Click(object sender, EventArgs e)
    {
        BindQuestion((int)ViewState["LastQuestion"]);
    }
    protected void bt_Cancel_Click(object sender, EventArgs e)
    {
        MessageBox.ShowAndClose(this, "问卷取消成功!");
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["Project"] != 0)
        {
            QNA_ResultBLL r = new QNA_ResultBLL();
            r.Model.Project = (int)ViewState["Project"];
            r.Model.RelateClient = (int)ViewState["RelateClient"];
            r.Model.RelateTask = (int)ViewState["RelateTask"];
            r.Model.InsertStaff = (int)Session["UserID"];

            bt_Next_Click(null, null);
            r.Items = (List<QNA_Result_Detail>)ViewState["ResultDetails"];

            Session["QuestionnaireResult"] = r.Add();

            MessageBox.ShowAndClose(this, "问卷保存成功!");
        }
    }
}
