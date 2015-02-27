<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_TaskDetail_CC, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

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
                                抄送我的工作流</h2>
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
                    <tr>
                        <td class="tabForm">
                            <mcs:UC_GridView ID="gv_List_Attachment" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="ID" Width="100%">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="GUID" DataNavigateUrlFormatString="DownloadAttachment.aspx?GUID={0}"
                                        DataTextField="Name" HeaderText="名称" >
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField HeaderText="上传人" DataField="UploadStaff" />
                                    <asp:BoundField HeaderText="上传日期" DataField="UploadTime" />
                                    <asp:BoundField HeaderText="描述" DataField="Description" />
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
                                            我的批注信息</h2>
                                    </td>
                                    <td height="18" align="right">
                                        <asp:CheckBox ID="cbx_NotifyInitiator" runat="server" Text="短讯通知发起人" Checked="true" />
                                        <asp:Button ID="bt_SaveComment" runat="server" ForeColor="#990000" OnClick="bt_SaveComment_Click"
                                            Text="回复批注意见" ValidationGroup="Decision" />
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
                                        消息标题
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_MessageSubject" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                        批注意见
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Literal ID="lb_Comment" runat="server"></asp:Literal><br />
                                        <asp:TextBox ID="tbx_Comment" runat="server" Height="50px" TextMode="MultiLine" Width="85%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                            ControlToValidate="tbx_Comment" ValidationGroup="Decision" Display="Dynamic"></asp:RequiredFieldValidator>
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
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
