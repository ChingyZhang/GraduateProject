<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowProcessDecisionDetail.aspx.cs" Inherits="SubModule_EWF_FlowProcessDecisionDetail" %>

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
                                    &nbsp;<asp:Label ID="lb_AppName" runat="server"></asp:Label>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ����
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" Width="250px" runat="server"></asp:TextBox>
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
                <tr runat="server" id="tr_ProcessDecision" visible="true" class="tabForm">
                    <td class="tabForm" width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td align="left" colspan="4" height="22" valign="middle">
                                    <h4>
                                        ��Ա�����Ϣ</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    �����˽�ɫ
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RecipientRole_Decision" DataTextField="Name" DataValueField="ID"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_RecipientRole_Decision_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ��Ϣ����
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_MessageSubject" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_MessageSubject"
                                        Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    �������ݶ���
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DataObject_Decision" DataTextField="DisplayName" DataValueField="ID"
                                        runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ����ȷ����ѡ��
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AllowNotSure" runat="server">
                                        <asp:ListItem Value="Y">����</asp:ListItem>
                                        <asp:ListItem Value="N">������</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ���ͨ����һ����
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_PositiveNextProcess" DataTextField="Name" DataValueField="ID"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ��˲�ͨ����һ����
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_NegativeNextProcess" DataTextField="Name" DataValueField="ID"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    ����ʱ��
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_TimeoutHours" Width="60px" runat="server">0</asp:TextBox>
                                    Сʱ<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_TimeoutHours"
                                        Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_TimeoutHours"
                                        Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                    (���ں����̽��Զ���Ĭ����һ����)
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    �Ƿ��������&nbsp;
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_CanSkip" runat="server">
                                        <asp:ListItem Text="��" Value="Y" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="��" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                    �����Ϊ�ǣ������������֮ǰ�����Ѳ����������������ͨ����
                                    ���ڸû�������Ҫͬһ����������ʱ��ϵͳ�Զ�������
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    �Ƿ�������������</td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_CanBatchApprove" runat="server">
                                        <asp:ListItem Selected="True" Text="����" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="������" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    &nbsp;</td>
                                <td class="dataField">
                                    &nbsp;</td>
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
