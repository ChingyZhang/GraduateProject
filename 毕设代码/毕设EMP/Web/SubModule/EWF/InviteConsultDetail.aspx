<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="InviteConsultDetail.aspx.cs" Inherits="SubModule_EWF_InviteConsultDetail" %>

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
                                ������Э������</h2>
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
                                            ������Ϣ</h2>
                                    </td>
                                    <td height="18" colspan="11" align="right">
                                        <asp:HyperLink ID="hyl_RelateURL" runat="server" CssClass="listViewTdLinkS1" >�鿴��ϸ������Ϣ</asp:HyperLink>
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
                                        ������
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_Applyer" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ��������
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_AppName" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ����
                                    </td>
                                    <td height="18" class="dataField" colspan="3">
                                        <asp:Label ID="lbl_Title" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" height="22" class="dataLabel">
                                        ��ǰ������
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_CurrentJobName" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ����״̬
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_Status" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ��������
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_StartTime" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td height="18" class="dataLabel">
                                        ��ֹ����
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lb_EndTime" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" height="18" class="dataLabel">
                                        ��ע
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
                                            ������ϸ��Ϣ</h2>
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
                                            ��ظ�����Ϣ</h2>
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
                                        DataTextField="Name" HeaderText="����" >
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField HeaderText="�ϴ���" DataField="UploadStaff" />
                                    <asp:BoundField HeaderText="�ϴ�����" DataField="UploadTime" />
                                    <asp:BoundField HeaderText="����" DataField="Description" />
                                </Columns>
                                <EmptyDataTemplate>
                                    ������
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
                                            ������ʷ����</h2>
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
                                    <asp:BoundField HeaderText="��������" DataField="ProcessName" />
                                    <asp:BoundField HeaderText="������" DataField="RecipientStaffName" />
                                    <asp:BoundField HeaderText="�������" DataField="RecipientResult" />
                                    <asp:BoundField HeaderText="����ʱ��" DataField="RecipientTime" />
                                    <asp:BoundField HeaderText="�������" DataField="DecisionComment" />
                                </Columns>
                                <EmptyDataTemplate>
                                    ������
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
                                            �ҵ�������Ϣ</h2>
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
                                        Э��������
                                    </td>
                                    <td height="18" class="dataField">
                                        <asp:Label ID="lbl_InvitedStaff" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td height="18" width="100" class="dataLabel">
                                        Э������ʱ��
                                    </td>
                                    <td height="18" class="dataField" style="width: 226px">
                                        <asp:Label ID="lbl_InvitedTime" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbx_NotifyInitiator" runat="server" Text="��Ѷ֪ͨ������" Checked="true" />
                                    </td>
                                    <td height="18" align="right">
                                        <asp:Button ID="bt_SaveDecisionComment" runat="server" ForeColor="#990000" OnClick="bt_SaveDecisionComment_Click"
                                            Text="�ظ��������" ValidationGroup="Decision" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" width="100" height="18" class="dataLabel">
                                        �������
                                    </td>
                                    <td height="18" class="dataField" colspan="5">
                                        <asp:Literal ID="lb_DecisionComment" runat="server"></asp:Literal><br />
                                        <asp:TextBox ID="tbx_DecisionComment" runat="server" Height="50px" TextMode="MultiLine"
                                            Width="85%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="����"
                                            ControlToValidate="tbx_DecisionComment" ValidationGroup="Decision" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="*"></asp:Label>
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
