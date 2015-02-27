using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.EWF;

public partial class SubModule_EWF_TaskDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["TaskID"] = Request.QueryString["TaskID"] == "" ? 0 : int.Parse(Request.QueryString["TaskID"]);
            if ((int)ViewState["TaskID"] == 0)
            {
                MessageBox.ShowAndRedirect(this, "缺少必要参数！", "../Login/index.aspx");
                return;
            }

            BindTaskInfo();

            BindAttachmentList();
        }
    }

    //绑定环节列表
    private void BindProcessList()
    {
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);
    }

    //选择某个Job，查看详细信息
    protected void gv_List_JobList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int JobID = (int)this.gv_List_JobList.DataKeys[e.NewSelectedIndex]["ID"];
        EWF_Task_JobBLL job = new EWF_Task_JobBLL(JobID);
        EWF_Flow_ProcessBLL process = new EWF_Flow_ProcessBLL(job.Model.CurrentProcess);

        this.tr_RecipientProcess.Visible = false;
        this.tr_ConditionProcess.Visible = false;
        this.tr_DataBaseProcess.Visible = false;
        this.tr_SendMailProcess.Visible = false;
        this.tr_CCProcess.Visible = false;

        switch (process.Model.Type)
        {
            case 1://开始环节
                break;
            case 2://结束环节
                break;
            case 3://人员审批环节
            case 10:   //人员会审环节
                this.tr_RecipientProcess.Visible = true;
                BindRecipientProcessInfo(JobID);
                break;
            case 4://条件判断环节
                this.tr_ConditionProcess.Visible = true;
                BindConditionProcessInfo(JobID);
                break;
            case 5://执行数据库环节
                this.tr_DataBaseProcess.Visible = true;
                BindDataBaseProcessInfo(JobID);
                break;
            case 6://发送邮件环节
                this.tr_SendMailProcess.Visible = true;
                BindSendMailProcessInfo(JobID);
                break;
            case 9: //抄送环节
                tr_CCProcess.Visible = true;
                BindCCProcessInfo(JobID);
                break;
        }
    }

    //绑定开始环节详细信息
    private void BindTaskInfo()
    {
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);

        #region 如果任务在待审批状态，且正好是待当前人审批，则直接切换到审批页面
        if (task.Model.Status == 3)
        {
            int job = EWF_TaskBLL.StaffCanApproveTask(task.Model.ID, (int)Session["UserID"]);
            if (job > 0) Response.Redirect("Recipient.aspx?CurrentJobID=" + job.ToString() + "&TaskID=" + task.Model.ID.ToString());
        }
        #endregion

        EWF_Flow_App app = new EWF_Flow_AppBLL(task.Model.App).Model;

        #region 绑定流程信息
        lbl_ID.Text = task.Model.ID.ToString();
        lbl_Applyer.Text = new Org_StaffBLL(task.Model.Initiator).Model.RealName;
        lbl_AppName.Text = app.Name;

        lbl_Applyer_Position.Text = new Org_PositionBLL(new Org_StaffBLL(task.Model.Initiator).Model.Position).Model.Name;

        lbl_Title.Text = task.Model.Title;
        lb_Status.Text = DictionaryBLL.GetDicCollections("EWF_Task_TaskStatus")[task.Model.Status.ToString()].Name;

        lb_StartTime.Text = task.Model.StartTime.ToString();
        if (task.Model.EndTime != new DateTime(1900, 1, 1))
            lb_EndTime.Text = task.Model.EndTime.ToString();
        else
            lb_EndTime.Text = "未结束";

        lt_Remark.Text = task.Model.Remark;

        #region 绑定流程当前环节信息
        if (task.Model.Status < 5)
        {
            IList<EWF_Task_Job> jobs = EWF_Task_JobBLL.GetModelList("Task=" + task.Model.ID.ToString() + " ORDER BY StartTime DESC");
            if (jobs.Count > 0)
            {
                EWF_Flow_Process process = new EWF_Flow_ProcessBLL(jobs[0].CurrentProcess).Model;
                if (process != null)
                {
                    lb_CurrentJobInfo.Text = process.Name;
                    if (process.Type == 3)
                    {
                        lb_CurrentJobInfo.Text += " 等待审批人:";
                        foreach (EWF_Task_JobDecision decision in EWF_Task_JobDecisionBLL.GetModelList("Job=" + jobs[0].ID.ToString()))
                        {
                            MCSFramework.Model.Org_Staff staff = new Org_StaffBLL(decision.RecipientStaff).Model;
                            if (staff != null) lb_CurrentJobInfo.Text += staff.RealName + "   ";
                        }
                    }
                }
            }
        }
        else
            tr_CurrentProcessInfo.Visible = false;
        #endregion

        #endregion

        #region 控制流程状态
        if (task.Model.Initiator == (int)Session["UserID"])
        {
            //只有流程发起的本人，才可控制流程状态

            if (task.Model.Status == 6)
            {
                MessageBox.Show(this, "您的流程因故停止流转，请尽快联系系统管理员排查原因，或点击“继续执行”按钮以重新启动流程的流转！");
                bt_Restart.Visible = true;
            }

            if (task.Model.Status == 4)
            {
                MessageBox.Show(this, "您的流程被最后一审批人设为了待处理状态，请补充相关信息附件后，或点击“继续执行”按钮以重新启动流程的流转！");
                bt_Restart.Visible = true;

            }

            if (task.Model.Status == 1)
            {
                MessageBox.Show(this, "您的流程还没正式发起申请，请尽快点击“确定发起”按钮以发起流程！");
                btn_Start.Visible = true;
            }

            if (task.Model.Status != 5)
            {
                //未完成
                if (app.RelateBusiness == "N")
                    bt_Cancel.Visible = true;
                tr_UploadAtt.Visible = true;
            }
        }
        #endregion

        #region 显示申请详细信息IFrame
        //显示申请详细信息IFrame
        if (!string.IsNullOrEmpty(task.Model.RelateURL))
        {
            hyl_RelateURL.NavigateUrl = task.Model.RelateURL;
            tr_RelateUrl.Visible = true;
            string url = this.ResolveClientUrl(task.Model.RelateURL);
            if (url.IndexOf('?') >= 0)
                url += "&ViewFramework=False";
            else
                url += "?ViewFramework=False";
            frame_relateurl.Attributes.Add("src", url);
        }
        else
        {
            tr_RelateUrl.Visible = false;
            hyl_RelateURL.Visible = false;
        }
        #endregion


        //Bind the dataobject info
        pl_dataobjectinfo.BindData(task.Model.DataObjectValues);
        if (task.Model.Status > 1)
        {
            pl_dataobjectinfo.SetPanelEnable(false);

            TextBox tbx_ReMark = pl_dataobjectinfo.FindControl("C_Remark") != null ? (TextBox)pl_dataobjectinfo.FindControl("C_Remark") : null;
            if (tbx_ReMark != null)
            {
                tbx_ReMark.Enabled = true;
                tbx_ReMark.ReadOnly = true;
            }

        }

        //绑定环节列表
        gv_List_JobList.BindGrid<EWF_Task_Job>(task.GetJobList());
    }

    private void BindAttachmentList()
    {
        //绑定附件
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);
        gv_List_Attachment.BindGrid<EWF_Task_Attachment>(task.GetAttachmentsList());
    }

    //绑定审批环节详细信息
    private void BindRecipientProcessInfo(int JobID)
    {
        EWF_Task_JobBLL bll = new EWF_Task_JobBLL(JobID);
        gv_JobDecision.BindGrid<EWF_Task_JobDecision>(bll.GetDecisionList());
    }

    private void BindCCProcessInfo(int JobID)
    {
        gv_JobCC.BindGrid<EWF_Task_JobCC>(EWF_Task_JobCCBLL.GetModelList("Job=" + JobID.ToString()));
    }
    //绑定条件判断环节详细信息
    private void BindConditionProcessInfo(int JobID)
    {
        EWF_Task_JobBLL job = new EWF_Task_JobBLL(JobID);
        EWF_TaskBLL task = new EWF_TaskBLL(job.Model.Task);
        EWF_Flow_ProcessConditionBLL process = new EWF_Flow_ProcessConditionBLL(job.Model.CurrentProcess);
        EWF_Flow_DataObjectBLL dataobject = new EWF_Flow_DataObjectBLL(process.Model.DataObject);

        this.lbl_DataObjectName.Text = dataobject.Model.Name;
        this.lbl_DataObjectDisPlayName.Text = dataobject.Model.DisplayName;

        this.lbl_DataObjectValue.Text = task.GetDataObjectValue()[dataobject.Model.Name]; ;
        this.lbl_OperatorTypeName.Text = DictionaryBLL.GetDicCollections("EWF_Flow_OperatorType")[process.Model.OperatorType.ToString()].Name;

        this.lbl_Value1.Text = process.Model.Value1;
        this.lbl_Value2.Text = process.Model.Value2;

    }

    //绑定执行数据库环节详细信息
    private void BindDataBaseProcessInfo(int JobID)
    {
        EWF_Task_JobBLL job = new EWF_Task_JobBLL(JobID);
        EWF_Flow_ProcessDataBaseBLL process = new EWF_Flow_ProcessDataBaseBLL(job.Model.CurrentProcess);

        this.lbl_DSN.Text = process.Model.DSN;
        this.lbl_StoreProcName.Text = process.Model.StoreProcName;
        this.gv_List_ParamList.BindGrid<EWF_Flow_DataBaseParam>(process.GetParamsList());
    }

    //绑定邮件发送环节详细信息
    private void BindSendMailProcessInfo(int JobID)
    {
        EWF_Task_JobBLL job = new EWF_Task_JobBLL(JobID);
        EWF_Flow_ProcessEmailBLL process = new EWF_Flow_ProcessEmailBLL(job.Model.CurrentProcess);

        this.lbl_ReciverRoleName.Text = new EWF_RoleBLL(process.Model.RecipientRole).Model.Name;
        this.lbl_MailSubject.Text = process.Model.Subject;
        this.lbl_MailContent.Text = process.Model.Content;
    }

    //查看附件
    protected void gv_List_Attachment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.Write("<script>window.open('" + this.gv_List_Attachment.DataKeys[e.NewSelectedIndex]["FilePath"].ToString() + "');</script>");
    }

    //Upload Attachment
    protected void btn_Up_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                string fullfilename = FileUpload1.FileName;

                if (fullfilename.LastIndexOf(".") < 0)
                {
                    MessageBox.Show(this, "无法识别的附件格式!");
                    return;
                }

                int FileSize = (FileUpload1.PostedFile.ContentLength / 1024);
                if (FileSize > ConfigHelper.GetConfigInt("MaxAttachmentSize"))
                {
                    MessageBox.Show(this.Page, "上传的文件不能大于" + ConfigHelper.GetConfigInt("MaxAttachmentSize") +
                        "KB!当前上传文件大小为:" + FileSize.ToString() + "KB");
                    return;
                }
                string extendname = fullfilename.Substring(fullfilename.LastIndexOf(".") + 1).ToLowerInvariant();
                string filename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));

                byte[] filedata;
                Stream filestream = FileUpload1.PostedFile.InputStream;

                #region 自动压缩上传的图片
                if (ATMT_AttachmentBLL.IsImage(extendname))
                {
                    try
                    {
                        System.Drawing.Image originalImage = System.Drawing.Image.FromStream(filestream);
                        filestream.Position = 0;

                        int width = originalImage.Width;

                        if (width > 1024 || extendname == "bmp")
                        {
                            if (width > 1024) width = 1024;

                            System.Drawing.Image thumbnailimage = ImageProcess.MakeThumbnail(originalImage, width, 0, "W");

                            MemoryStream thumbnailstream = new MemoryStream();
                            thumbnailimage.Save(thumbnailstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            thumbnailstream.Position = 0;
                            FileSize = (int)(thumbnailstream.Length / 1024);
                            extendname = "jpg";

                            filestream = thumbnailstream;
                        }
                    }
                    catch { }
                }
                #endregion

                filedata = new byte[filestream.Length];
                filestream.Read(filedata, 0, (int)filestream.Length);
                filestream.Close();

                EWF_Task_AttachmentBLL ta = new EWF_Task_AttachmentBLL();
                ta.Model.Task = (int)ViewState["TaskID"];
                ta.Model.Name = this.tbx_AttachmentName.Text.Trim();
                if (ta.Model.Name == "") ta.Model.Name = filename;
                ta.Model.Description = this.tbx_AttachmentDescription.Text.Trim();
                //ta.Model.FilePath = saveasfilename;
                ta.Model.UploadStaff = int.Parse(Session["UserID"].ToString());
                ta.Model.FileSize = FileUpload1.PostedFile.ContentLength / 1024;
                ta.Model.FileType = extendname.ToUpper();

                if (ta.Add(filedata) > 0)
                {
                    BindAttachmentList();
                }
                else
                {
                    MessageBox.Show(this, "附件上传失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }
        else
        {
            MessageBox.Show(this, "未选择要上传的附件！");
            return;
        }
    }

    public string GetDataObjectValue(Guid DataObjectID)
    {
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);

        if (DataObjectID != Guid.Empty)
            return task.GetDataObjectValue()[new EWF_Flow_DataObjectBLL(DataObjectID).Model.Name];
        else
            return "";
    }

    //确定发起
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);
        task.Model.DataObjectValues = pl_dataobjectinfo.GetData();
        task.Update();
        if (task.Start() >= 0)
        {
            MessageBox.ShowAndRedirect(this, "流程已成功发起！！", "TaskDetail.aspx?TaskID=" + task.Model.ID.ToString());
        }
        else
        {
            MessageBox.Show(this, "对不起，流程发起失败，请确认该申请流程有开始环节！");
        }
    }

    //继续执行
    protected void bt_Restart_Click(object sender, EventArgs e)
    {
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);

        task.ReStart();
        MessageBox.ShowAndRedirect(this, "流程已继续执行！！", "../desktop.aspx");
    }

    //取消执行
    protected void bt_Cancel_Click(object sender, EventArgs e)
    {
        EWF_TaskBLL task = new EWF_TaskBLL((int)ViewState["TaskID"]);

        task.Cancel();
        MessageBox.ShowAndRedirect(this, "流程已被取消执行！！", "../desktop.aspx");
    }
    protected void gv_List_Attachment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gv_List_Attachment.DataKeys[e.RowIndex]["ID"];
        EWF_Task_AttachmentBLL att = new EWF_Task_AttachmentBLL(id);

        if (att.Model.UploadStaff == (int)Session["UserID"])
        {
            att.Delete();
            BindAttachmentList();
        }
        else
        {
            MessageBox.Show(this, "对不起，您只能删除您自己上传的附件!");
        }
    }
}
