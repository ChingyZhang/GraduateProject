<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_FlowInitPosition, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                �����������ְ���б�</h2>
                        </td>
                        <td align="left" style="width: 76px; height: 24px">
                            &nbsp;</td>
                        <td align="right" style="height: 24px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" SelectedIndex="3" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    Width="100%">
                    <Items>
                        <mcs:MCSTabItem Text="���̻�����Ϣ" Value="0" />
                        <mcs:MCSTabItem Text="���̻����б�" Value="1" />
                        <mcs:MCSTabItem Text="���������ֶ�" Value="2" />
                        <mcs:MCSTabItem Text="������ְλ" Value="3" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" align="center">
                                        <tr>
                                            <td class="dataLabel">
                                                ְλ
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSTreeControl ID="tr_Position" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    RootValue="0" Width="200px" ParentColumnName="SuperID" />
                                            </td>
                                            <td class="dataLabel">
                                                <asp:CheckBox ID="cb_IncludeChild" Text="���������¼�ְ��" runat="server" />
                                            </td>
                                            <td class="dataLabel">
                                                ������ʼ��
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_BeginDay" runat="server" Width="60px">1</asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    runat="server" ControlToValidate="tbx_BeginDay" Display="Dynamic" ErrorMessage="����"
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_BeginDay"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tbx_BeginDay"
                                                    Display="Dynamic" ErrorMessage="�������1С��31" MaximumValue="31" MinimumValue="1"
                                                    Type="Integer" ValidationGroup="1"></asp:RangeValidator>
                                            </td>
                                            <td class="dataLabel">
                                                �������ֹ��
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_EndDay" runat="server" Width="60px">31</asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                    runat="server" ControlToValidate="tbx_EndDay" Display="Dynamic" ErrorMessage="����"
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_EndDay"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="tbx_EndDay"
                                                    Display="Dynamic" ErrorMessage="�������1С��31" MaximumValue="31" MinimumValue="1"
                                                    Type="Integer" ValidationGroup="1"></asp:RangeValidator>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_Add" runat="server" Text="����" OnClick="bt_Add_Click" ValidationGroup="1"
                                                    Width="60px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                        Binded="False" ConditionString="" PanelCode="" TotalRecordCount="0" DataKeyNames="ID"
                                        OnRowDeleting="gv_List_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="App" HeaderText="��������" SortExpression="App" />
                                            <asp:BoundField DataField="Position" HeaderText="ְ������" SortExpression="Position" />
                                            <asp:BoundField DataField="BeginDay" HeaderText="������ʼ��" SortExpression="BeginDay" />
                                            <asp:BoundField DataField="EndDay" HeaderText="�������ֹ��" SortExpression="EndDay" />
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:CommandField>
                                        </Columns>
                                    </mcs:UC_GridView>
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
