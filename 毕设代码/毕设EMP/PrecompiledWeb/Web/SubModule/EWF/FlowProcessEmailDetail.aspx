<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_FlowProcessDetail, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 129px; height: 24px;">
                            <h2>
                                ���̻�����ϸ��Ϣ</h2>
                        </td>
                        <td align="left">
                            ID��:<asp:Label ID="lb_ID" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                        <td align="left" style="height: 24px">
                            ��������<asp:DropDownList ID="ddl_Type" DataTextField="Value" DataValueField="Key" runat="server"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_Save" runat="server" Width="60px" Text="�� ��" OnClick="bt_Save_Click" />
                            <asp:Button ID="bt_Del" runat="server" Text="ɾ ��" Width="60px" OnClick="bt_Del_Click"
                                OnClientClick="return confirm(&quot;�Ƿ�ȷ��ɾ��?&quot;)" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table2" cellspacing="5" cellpadding="5" width="100%" align="center" border="0">
                <tr>
                    <td class="tabForm" width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left" colspan="4" height="22" valign="middle">
                                    <h4>
                                        ������Ϣ</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ��������
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lb_AppName" runat="server"></asp:Label>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ����
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" Width="250px" runat="server">�����ʼ�֪ͨ</asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="tbx_Name" Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px;">
                                    ����
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_Description" Width="500px" runat="server" TextMode="MultiLine"
                                        Height="61px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    Ĭ����һ����
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DefaultNextProcess" DataTextField="Name" DataValueField="ID"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    �����
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Sort" runat="server" Width="50px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server" ControlToValidate="tbx_Sort" Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Sort"
                                        Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr_ProcessEmail" visible="true" class="tabForm">
                    <td class="tabForm" width="100%">
                        <table id="Table3" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td align="left" colspan="4" height="22" valign="middle">
                                    <h4>
                                        �����ʼ���Ϣ</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    �ռ��˽�ɫ
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RecipientRole_Mail" DataTextField="Name" DataValueField="ID"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_RecipientRole_Mail_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ���ݶ���
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DataObject_Mail" DataTextField="DisplayName" DataValueField="ID"
                                        runat="server" Enable="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    �ʼ�����
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_Subject" Width="440px" runat="server">$AppName$ ����δ�ܱ�����ͨ��</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    �ʼ�����
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_Content" Width="440px" runat="server" Height="144px" TextMode="MultiLine">$InitiatorName$���ã�����$AppName$����δ�ܱ�����ͨ�������һλ������Ϊ$LastApproveStaffName$��

��л����֧�֣� </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="0" border="0" class="h3Row" width="100%">
                                        <tr>
                                            <td>
                                                <h3>
                                                    ��������</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    $Initiator$
                                </td>
                                <td class="dataField">
                                    ��ǰ���̷��̷�����Ա��ID��
                                </td>
                                <td class="dataLabel">
                                    $InitiatorName$
                                </td>
                                <td class="dataField">
                                    ��ǰ���̷��̷�����Ա������
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    $Today$
                                </td>
                                <td class="dataField">
                                    ��ǰϵͳ�����ڣ���ʽΪyyyy-mm-dd
                                </td>
                                <td class="dataLabel">
                                    $Now$
                                </td>
                                <td class="dataField">
                                    ��ǰϵͳ��ʱ�䣬��ʽΪhh:mm:ss
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    $AppName$
                                </td>
                                <td class="dataField">
                                    ��ǰ�������̵�����
                                </td>
                                <td class="dataLabel">
                                    $LastApproveStaffName$
                                </td>
                                <td class="dataField">
                                    ��ǰ�������һ�������˵�����
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    $TaskID$
                                </td>
                                <td class="dataField">
                                    ��ǰ����Ĺ������̵�ID
                                </td>
                                 <td class="dataLabel">
                                    $InitiateDateTime$"
                                </td>
                                <td class="dataField">
                                    ���̷���ʱ�� yyyy-MM-dd HH:mm:ss
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
