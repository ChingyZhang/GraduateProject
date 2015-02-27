<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="TaskDetail.aspx.cs" Inherits="SubModule_EWF_TaskDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <%--页面标题--%>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                流程任务详细信息</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Start" runat="server" Text="确定发起" Width="80px" OnClick="btn_OK_Click"
                                Visible="false" />
                            <asp:Button ID="bt_Restart" runat="server" Text="继续执行" Visible="False" Width="80px"
                                OnClick="bt_Restart_Click" CausesValidation="false" />
                            <asp:Button ID="bt_Cancel" runat="server" OnClick="bt_Cancel_Click" Text="取消申请" Width="80px"
                                Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            基本信息</h2>
                                    </td>
                                    <td height="18" colspan="11" align="right">
                                        <asp:HyperLink ID="hyl_RelateURL" runat="server" CssClass="listViewTdLinkS1" >查看详细申请信息</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                <tr>
                                    <td align="left" height="18" class="dataLabel">
                                        发起人
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_Applyer" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="left" height="18" class="dataLabel">
                                        发起人职位
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_Applyer_Position" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        所属流程
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_AppName" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        流程状态
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_Status" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" height="18" class="dataLabel">
                                        流程编号
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_ID" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="left" height="18" class="dataLabel">
                                        主题
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_Title" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        发起日期
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_StartTime" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        截止日期
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_EndTime" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" id="tr_CurrentProcessInfo">
                                    <td valign="middle" align="left" height="18" class="dataLabel">
                                        当前环节
                                    </td>
                                    <td height="18" class="dataField" colspan="7">
                                        <asp:Label ID="lb_CurrentJobInfo" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" height="18" class="dataLabel">
                                        审批意见
                                    </td>
                                    <td height="18" class="dataField" colspan="7">
                                        <font color="blue">
                                            <asp:Literal ID="lt_Remark" runat="server"></asp:Literal></font>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_EWFPanel ID="pl_dataobjectinfo" runat="server">
                </mcs:UC_EWFPanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr runat="server" id="tr_RelateUrl">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            申请详细信息</h2>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <iframe width="100%" height="400px" scrolling="auto" frameborder="no" runat="server"
                                id="frame_relateurl"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            相关附件信息</h2>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="tr_UploadAtt" visible="false" runat="server">
                        <td height="28px">
                            附件：<asp:FileUpload ID="FileUpload1" runat="server" />
                            名称：<asp:TextBox ID="tbx_AttachmentName" runat="server" Width="150px"></asp:TextBox>
                            描述：<asp:TextBox ID="tbx_AttachmentDescription" runat="server" Width="350px"></asp:TextBox>
                            <asp:Button ID="btn_Up" runat="server" Text="上传" Width="60px" OnClick="btn_Up_Click"
                                ValidationGroup="2" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List_Attachment" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="ID,FilePath" Width="100%" OnSelectedIndexChanging="gv_List_Attachment_SelectedIndexChanging"
                                Binded="False" ConditionString="" PanelCode="" OnRowDeleting="gv_List_Attachment_RowDeleting">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="GUID" DataNavigateUrlFormatString="DownloadAttachment.aspx?GUID={0}"
                                        Text="下载"  ControlStyle-CssClass="listViewTdLinkS1" />
                                    <asp:BoundField DataField="Name" HeaderText="名称" />
                                    <asp:BoundField DataField="FileType" HeaderText="附件类型" />
                                    <asp:BoundField DataField="Description" HeaderText="描述" />
                                    <asp:BoundField DataField="FileSize" HeaderText="附件大小(KB)" />
                                    <asp:BoundField DataField="UploadStaff" HeaderText="上传人" />
                                    <asp:BoundField DataField="UploadTime" HeaderText="上传日期" />
                                    <asp:ButtonField CommandName="Delete" Text="删除" ControlStyle-CssClass="listViewTdLinkS1"
                                        ItemStyle-Width="30px" />
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据
                                </EmptyDataTemplate>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                        <tr>
                                            <td height="28px">
                                                <h2>
                                                    流程环节列表</h2>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List_JobList" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="ID" Width="100%" OnSelectedIndexChanging="gv_List_JobList_SelectedIndexChanging">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" SelectText="查看详细">
                                                <ItemStyle Width="100px" />
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="CurrentProcess" HeaderText="环节名称" />
                                            <asp:BoundField DataField="Status" HeaderText="当前状态" />
                                            <asp:BoundField DataField="StartTime" HeaderText="开始时间" />
                                            <asp:BoundField DataField="EndTime" HeaderText="结束时间" />
                                            <asp:BoundField DataField="ErrorMessage" HeaderText="错误信息" />
                                            <asp:BoundField DataField="Remark" HeaderText="备注" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_RecipientProcess" visible="false">
                                <%--人员审批环节详细信息--%>
                                <td>
                                    <table id="Table1" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_JobDecision" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    Binded="False" ConditionString="" OrderFields="" PanelCode="" TotalRecordCount="0">
                                                    <Columns>
                                                        <asp:BoundField DataField="RecipientStaff" HeaderText="收件人" />
                                                        <asp:BoundField DataField="DecisionStaff" HeaderText="审批人" />
                                                        <asp:BoundField DataField="DecisionResult" HeaderText="审批结果" />
                                                        <asp:BoundField DataField="DecisionTime" HeaderText="审批时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                                        <asp:TemplateField HeaderText="审批意见">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="Label1" runat="server" Text='<%# Bind("DecisionComment") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ReadFlag" HeaderText="读取标志" />
                                                        <asp:BoundField DataField="ReadTime" HeaderText="读取时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        无数据
                                                    </EmptyDataTemplate>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_CCProcess" visible="false">
                                <%--抄送环节详细信息--%>
                                <td>
                                    <table id="Table3" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_JobCC" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    Binded="False" ConditionString="" OrderFields="" PanelCode="" TotalRecordCount="0">
                                                    <Columns>
                                                        <asp:BoundField DataField="RecipientStaff" HeaderText="收件人" />
                                                        <asp:TemplateField HeaderText="批注意见">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="Label1" runat="server" Text='<%# Bind("Comment") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ReadFlag" HeaderText="读取标志" />
                                                        <asp:BoundField DataField="ReadTime" HeaderText="读取时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        无数据
                                                    </EmptyDataTemplate>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_ConditionProcess" visible="false">
                                <%--条件判断环节详细信息--%>
                                <td>
                                    <table id="Table5" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td class="tabForm">
                                                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                                    <tr>
                                                        <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                                            数据对象名
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_DataObjectName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" width="100" class="dataLabel">
                                                            数据对象值
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_DataObjectValue" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" class="dataLabel" width="100">
                                                            比较类型
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            &nbsp;<asp:Label ID="lbl_OperatorTypeName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                                            显示名称
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_DataObjectDisPlayName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" width="100" class="dataLabel">
                                                            比较值1
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_Value1" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" class="dataLabel" width="100">
                                                            比较值2
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_Value2" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_DataBaseProcess" visible="false">
                                <%--执行存储过程环节详细信息--%>
                                <td>
                                    <table id="Table6" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td class="tabForm">
                                                <table id="Table7" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                    <tr>
                                                        <td align="left" colspan="4" height="22" valign="middle">
                                                            <h4>
                                                                执行数据库信息</h4>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataLabel" style="width: 120px; height: 30px;">
                                                            数据库连接
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:Label ID="lbl_DSN" Width="120px" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="dataLabel" style="width: 120px; height: 30px;">
                                                            存储过程名称
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:Label ID="lbl_StoreProcName" Width="120px" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table id="Table8" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                    <tr>
                                                        <td class="dataLabel" align="left">
                                                            <h4>
                                                                存储过程参数</h4>
                                                        </td>
                                                        <td class="dataField">
                                                            &nbsp;
                                                        </td>
                                                        <td style="height: 30px;" align="right">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataLabel" style="height: 30px;" colspan="3">
                                                            <mcs:UC_GridView ID="gv_List_ParamList" runat="server" AutoGenerateColumns="False"
                                                                Width="100%">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ParamName" HeaderText="参数名称" />
                                                                    <asp:BoundField DataField="IsOutput" HeaderText="是否输出参数" />
                                                                    <asp:BoundField DataField="IsDataObject" HeaderText="是否数据对象" />
                                                                    <asp:BoundField DataField="DataObject" HeaderText="数据对象名称" />
                                                                    <asp:TemplateField HeaderText="数据对象值">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# GetDataObjectValue((Guid)DataBinder.Eval(Container,"DataItem.DataObject")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ConstStrValue" HeaderText="常量字符串" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    无数据
                                                                </EmptyDataTemplate>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_SendMailProcess" visible="false">
                                <%--发送邮件环节详细信息--%>
                                <td>
                                    <table id="Table4" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                                        <tr>
                                            <td class="tabForm">
                                                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                                    <tr>
                                                        <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                                            收件人角色
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_ReciverRoleName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" width="100" class="dataLabel">
                                                            邮件主题
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_MailSubject" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td height="18" class="dataLabel" style="width: 125px" width="100">
                                                            邮件内容
                                                        </td>
                                                        <td height="18" class="dataField">
                                                            <asp:Label ID="lbl_MailContent" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
