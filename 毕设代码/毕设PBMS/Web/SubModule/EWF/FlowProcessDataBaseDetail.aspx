<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowProcessDataBaseDetail.aspx.cs" Inherits="SubModule_EWF_FlowProcessDataBaseDetail" %>

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
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                                <td class="dataLabel" width="100">
                                    ��������
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lb_AppName" runat="server"></asp:Label>
                                </td>
                                <td class="dataLabel" width="100">
                                    ����
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" Width="250px" runat="server">ִ�����ݿ�</asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="tbx_Name" Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    ����
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_Description" Width="500px" runat="server" TextMode="MultiLine"
                                        Height="61px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
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
                <tr runat="server" id="tr_ProcessDataBase" visible="true" class="tabForm">
                    <td class="tabForm" style="width: 100%">
                        <table id="Table4" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td align="left" colspan="2" height="22" valign="middle">
                                    <h4>
                                        ִ�����ݿ���Ϣ</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" width="100">
                                    ���ݿ�����
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_DSN" Width="400px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    �洢��������
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_StoreProcName" Width="400px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr1" visible="true" class="tabForm">
                    <td class="tabForm" style="width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                    <tr>
                                        <td class="dataLabel" style="height: 30px;" colspan="2">
                                            <h4>
                                                �洢���̲���ά��</h4>
                                        </td>
                                        <td style="height: 30px;" align="right" colspan="2">
                                            <asp:Button ID="btn_Save_Param" runat="server" Text="�� ��" Width="60" OnClick="btn_Save_Param_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" width="100">
                                            ��������
                                        </td>
                                        <td class="dataField">
                                            <asp:TextBox ID="tbx_ParamName" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="dataLabel">
                                            �Ƿ��������
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_IsOutput" runat="server">
                                                <asp:ListItem Value="Y">��</asp:ListItem>
                                                <asp:ListItem Value="N" Selected="True">��</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel">
                                            �������ݶ���
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_DataObject_DataBase" DataTextField="DisplayName" DataValueField="ID"
                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_DataObject_DataBase_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            ��������ֵ
                                        </td>
                                        <td class="dataField">
                                            <asp:TextBox ID="tbx_ConstStrValue" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dataLabel" style="height: 30px;" colspan="4">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                                        Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnRowDeleting="gv_List_RowDeleting">
                                                        <Columns>
                                                            <asp:BoundField DataField="ParamName" HeaderText="��������" />
                                                            <asp:BoundField DataField="IsOutput" HeaderText="�Ƿ��������" />
                                                            <asp:BoundField DataField="IsDataObject" HeaderText="�Ƿ����ݶ���" />
                                                            <asp:BoundField DataField="DataObject" HeaderText="�������ݶ���" />
                                                            <asp:BoundField DataField="ConstStrValue" HeaderText="�����ַ���" />
                                                            <asp:ButtonField CommandName="Select" Text="ѡ��" ControlStyle-CssClass="listViewTdLinkS1" />
                                                            <asp:ButtonField CommandName="Delete" Text="ɾ��" ControlStyle-CssClass="listViewTdLinkS1" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            ������
                                                        </EmptyDataTemplate>
                                                    </mcs:UC_GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Param" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gv_List" EventName="SelectedIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                                    ��ǰϵͳ�����ڣ���ʽΪyyyy-MM-dd
                                </td>
                                <td class="dataLabel">
                                    $Now$
                                </td>
                                <td class="dataField">
                                    ��ǰϵͳ��ʱ�䣬��ʽΪHH:mm:ss
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
