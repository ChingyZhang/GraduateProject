<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_Recipient, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                工作流审批</h2>
                        </td>
                        <td align="right">
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
                                    <td valign="middle" align="left" height="22" class="dataLabel">
                                        发起人
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_Applyer" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        所属流程
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_AppName" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        主题
                                    </td>
                                    <td height="18" class="dataField" colspan="3">
                                        <asp:Label ID="lbl_Title" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" height="22" class="dataLabel">
                                        当前工作项
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_CurrentJobName" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        流程状态
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_Status" runat="server" ForeColor="Red"></asp:Label>
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
                                <tr>
                                    <td valign="middle" align="left" height="18" class="dataLabel">
                                        备注
                                    </td>
                                    <td height="18" class="dataField" colspan="11">
                                        <asp:Literal ID="lt_Remark" runat="server"></asp:Literal>
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
                            <iframe height="400px" width="100%" scrolling="auto" frameborder="no" runat="server"
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
                    <tr id="tr_UploadAtt" runat="server">
                        <td height="28px" class="tabForm">
                            附件：<asp:FileUpload ID="FileUpload1" runat="server" />
                            名称：<asp:TextBox ID="tbx_AttachmentName" runat="server" Width="150px"></asp:TextBox>
                            描述：<asp:TextBox ID="tbx_AttachmentDescription" runat="server" Width="350px"></asp:TextBox>
                            <asp:Button ID="btn_Up" runat="server" Text="上传" Width="60px" OnClick="btn_Up_Click"
                                ValidationGroup="2" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <mcs:UC_GridView ID="gv_List_Attachment" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="ID" Width="100%" OnRowDeleting="gv_List_Attachment_RowDeleting">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="GUID" DataNavigateUrlFormatString="DownloadAttachment.aspx?GUID={0}"
                                        DataTextField="Name" HeaderText="名称" >
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField HeaderText="上传人" DataField="UploadStaff" />
                                    <asp:BoundField HeaderText="上传日期" DataField="UploadTime" />
                                    <asp:BoundField HeaderText="描述" DataField="Description" />
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
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            审批历史环节</h2>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <mcs:UC_GridView ID="gv_List_DecisionHistory" runat="server" AutoGenerateColumns="False"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="环节名称" DataField="ProcessName" />
                                    <asp:BoundField HeaderText="审批人" DataField="RecipientStaffName" />
                                    <asp:BoundField HeaderText="审批结果" DataField="RecipientResult" />
                                    <asp:BoundField HeaderText="审批时间" DataField="RecipientTime" />
                                    <asp:BoundField HeaderText="审批意见" DataField="DecisionComment" />
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
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            我的审批信息</h2>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                <tr>
                                    <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                        审批人
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_RecipientStaff" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td height="18" width="100" class="dataLabel">
                                        审批时间
                                    </td>
                                    <td height="18" class="dataField" style="width: 226px">
                                        <asp:Label ID="lbl_RecipientTime" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbx_NotifyInitiator" runat="server" Text="短讯通知发起人" />
                                    </td>
                                    <td height="18" align="right">
                                        <asp:Button ID="btn_Pass" runat="server" Text="审批通过" Width="80px" OnClick="btn_Pass_Click"
                                            CausesValidation="False" ForeColor="Red" OnClientClick="return confirm(&quot;确认将该申请审批通过么？&quot;)"
                                            ValidationGroup="Decision" />
                                        <asp:Button ID="btn_NotPass" runat="server" Text="审批不通过" Width="80px" OnClick="btn_NotPass_Click"
                                            CausesValidation="False" ForeColor="Blue" OnClientClick="return confirm(&quot;确认将该申请审批不通过么？&quot;)"
                                            ValidationGroup="Decision" />
                                        <asp:Button ID="bt_SaveDecisionComment" runat="server" ForeColor="#990000" OnClick="bt_SaveDecisionComment_Click"
                                            Text="暂挂" Width="80px" ValidationGroup="Decision" />
                                        <asp:Button ID="btn_WaitProcess" runat="server" Text="待处理" Width="80px" OnClick="btn_WaitProcess_Click"
                                            CausesValidation="False" OnClientClick="return confirm(&quot;确认将该申请设为待处理么？&quot;)"
                                            ValidationGroup="Decision" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                        审批意见
                                    </td>
                                    <td height="18" class="dataField" colspan="5">
                                        <asp:Literal ID="lb_DecisionComment" runat="server"></asp:Literal><br />
                                        <asp:TextBox ID="tbx_DecisionComment" runat="server" Height="50px" TextMode="MultiLine" Text="已阅"
                                            Width="85%" onblur="if(this.innerText=='') this.innerText='已阅';" onFocus="if(this.innerText=='已阅') this.innerText='';"></asp:TextBox>
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
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                <tr>
                                    <td height="28px">
                                        <h2>
                                            发起协助邀审</h2>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                        <tr>
                                            <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                                协助员工
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_RecipientStaff" runat="server" Width="320" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx?MultiSelected=Y" />
                                            </td>
                                            <td class="dataLabel">
                                                协助说明
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_InvitedMessageSubject" runat="server" Width="180px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddInviteConsult" runat="server" Width="80px" ForeColor="Red"
                                                    Text="发起协助邀审" OnClick="bt_AddInviteConsult_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <mcs:UC_GridView ID="gv_InviteConsult" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="协助人" DataField="RecipientStaff" />
                                                        <asp:BoundField HeaderText="协助说明" DataField="MessageSubject" />
                                                        <asp:BoundField HeaderText="回复参考意见" DataField="ConsultComment" HtmlEncode="false" />
                                                        <asp:BoundField HeaderText="回复时间" DataField="ConsultTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                                        <asp:BoundField HeaderText="发起人" DataField="InvitedStaff" />
                                                        <asp:BoundField HeaderText="发起时间" DataField="InvitedTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                                        <asp:BoundField HeaderText="读取标志" DataField="ReadFlag" />
                                                        <asp:BoundField HeaderText="读取时间" DataField="ReadTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        无数据
                                                    </EmptyDataTemplate>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row">
                                        <tr>
                                            <td>
                                                <h2>
                                                    当前申请人相关其他流程列表信息</h2>
                                            </td>
                                            <td align="left" class="dataLabel">
                                                按流程查询:<asp:DropDownList ID="ddl_App" runat="server" DataTextField="Name" DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                日期:<asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对"
                                                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox
                                                        ID="tbx_end" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="日期格式不对"
                                                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                流程状态:<asp:DropDownList ID="ddl_Status" runat="server" DataTextField="Value" DataValueField="Key">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_Search" runat="server" Text="查 询" Width="60px" OnClick="btn_Search_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tabForm">
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                                    Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" AllowPaging="True"
                                                    OnPageIndexChanging="gv_List_PageIndexChanging">
                                                    <Columns>
                                                        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="TaskDetail.aspx?TaskID={0}"
                                                            DataTextField="ID" HeaderText="流程标识" ControlStyle-CssClass="listViewTdLinkS1" />
                                                        <asp:BoundField DataField="App" HeaderText="流程名称" />
                                                        <asp:TemplateField HeaderText="主题">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Status" HeaderText="当前状态" />
                                                        <asp:HyperLinkField DataTextField="RelateURL" HeaderText="关联URL" DataNavigateUrlFields="RelateURL"
                                                            DataNavigateUrlFormatString="{0}" ControlStyle-CssClass="listViewTdLinkS1" 
                                                            Visible="false">
                                                            <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                                        </asp:HyperLinkField>
                                                        <asp:BoundField DataField="StartTime" HeaderText="发起时间" />
                                                        <asp:BoundField DataField="EndTime" HeaderText="结束时间" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# "Recipient.aspx?CurrentJobID="+StaffCanApproveTask(DataBinder.Eval(Container,"DataItem.ID").ToString())+"&TaskID="+DataBinder.Eval(Container,"DataItem.ID").ToString()%>'
                                                                     Text='审批' Visible='<%# StaffCanApproveTask(DataBinder.Eval(Container,"DataItem.ID").ToString())!="0" %>'
                                                                    CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
