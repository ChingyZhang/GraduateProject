<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FlowDataObjectList.aspx.cs" Inherits="SubModule_EWF_FlowDataObjectList" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
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
                                �����������ֶ��б�</h2>
                        </td>
                        <td align="right">
                            ��������:
                            <asp:HyperLink ID="lb_AppName" runat="server" CssClass="listViewTdLinkS1">[HyperLink4]</asp:HyperLink>
                        </td>
                        <td align="right">
                            ����:
                            <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" Text="��ѯ" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="�� ��" Width="60px"
                                ForeColor="Red" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" SelectedIndex="2" OnOnTabClicked="MCSTabControl1_OnTabClicked"
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            DataKeyNames="Name" PageSize="15" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                            OnRowDeleting="gv_FeeDetialList_RowDeleting" Binded="False" ConditionString=""
                            PanelCode="" TotalRecordCount="0">
                            <Columns>
                                <asp:ButtonField CommandName="Select" Text="ѡ��" ControlStyle-CssClass="listViewTdLinkS1"
                                    ItemStyle-Width="30px">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="Name" HeaderText="��������" SortExpression="Name" />
                                <asp:BoundField DataField="DisplayName" HeaderText="��ʾ����" SortExpression="DisplayName" />
                                <asp:BoundField DataField="DataType" HeaderText="��������" SortExpression="DataType" />
                                <asp:TemplateField HeaderText="��ʾ˳��" SortExpression="SortID">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SortID" runat="server" Text='<%# Bind("SortID") %>' Width="20px"></asp:Label>
                                        <asp:Button ID="bt_Increase" runat="server" OnClick="bt_Increase_Click" Text="+" />
                                        <asp:Button ID="bt_Decrease" runat="server" OnClick="bt_Decrease_Click" Text="-" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ReadOnly" HeaderText="ֻ��" SortExpression="ReadOnly" />
                                <asp:BoundField DataField="Visible" HeaderText="�ɼ�" SortExpression="Visible" />
                                <asp:BoundField DataField="RelationType" HeaderText="��������" SortExpression="RelationType" />
                                <asp:BoundField DataField="RelationTableName" HeaderText="��������" SortExpression="RelationTableName" />
                                <asp:ButtonField CommandName="Delete" Text="ɾ��" ControlStyle-CssClass="listViewTdLinkS1"
                                    ItemStyle-Width="30px">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:ButtonField>
                            </Columns>
                            <EmptyDataTemplate>
                                ������
                            </EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Add" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row" height="28">
                    <tr>
                        <td>
                            <h3>
                                �����������ֶ���ϸ����</h3>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="�� ��" ValidationGroup="2"
                                        Width="60px" /></ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gv_List" EventName="SelectedIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    ��������
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_DataType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    ��������
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_Name" runat="server" Width="120px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server" ControlToValidate="tbx_Name" ErrorMessage="����Ϊ��" Display="Dynamic"
                                        ValidationGroup="2"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbx_Name"
                                        ErrorMessage="������Ӣ����ĸ" ValidationExpression='([a-z]|[A-Z]|_)([a-z]|[A-Z]|[0-9]|_)*'
                                        Display="Dynamic" ValidationGroup="2"></asp:RegularExpressionValidator>
                                </td>
                                <td class="dataLabel">
                                    ��ʾ����
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_DisplayName" Width="120px" runat="server"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server" ControlToValidate="tbx_DisplayName" ErrorMessage="����Ϊ��" Display="Dynamic"
                                        ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    ֻ������
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_ReadOnly" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y">ֻ��</asp:ListItem>
                                        <asp:ListItem Value="N" Selected="True">��ֻ��</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel">
                                    �༭����
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_Enable" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y" Selected="True">�ɱ༭</asp:ListItem>
                                        <asp:ListItem Value="N">���ɱ༭</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel">
                                    �ɼ�����
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_Visible" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="Y" Selected="True">�ɼ�</asp:ListItem>
                                        <asp:ListItem Value="N">���ɼ�</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    �ؼ�����
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ControlType" DataTextField="Value" DataValueField="Key"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    �ؼ���
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_ControlWidth" runat="server" Width="60px">100</asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_ControlWidth"
                                        Display="Dynamic" ErrorMessage="ֻ��Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                        ValueToCompare="2"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel">
                                    �ؼ���
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_ControlHeight" runat="server" Width="60px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_ControlHeight"
                                        Display="Dynamic" ErrorMessage="ֻ��Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                        ValueToCompare="2"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    �ؼ���ʽ
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_ControlStyle" runat="server"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    ռ�п���
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_ColSpan" runat="server" Width="60px">1</asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_ColSpan"
                                        Display="Dynamic" ErrorMessage="ֻ��Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                        ValueToCompare="2"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel">
                                    ��ʾ˳��
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_SortID" runat="server" Width="60px"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server" ControlToValidate="tbx_SortID" Display="Dynamic" ErrorMessage="����Ϊ��"
                                        ValidationGroup="2"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_SortID"
                                        Display="Dynamic" ErrorMessage="ֻ��Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                        ValueToCompare="2"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    �Ƿ����
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_IsRequireField" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y">����</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="N">�Ǳ���</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel">
                                    ������ʽ
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_RegularExpression" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    ��ʽ�ַ���
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_FormatString" runat="server" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    �ֶ�˵��
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_Description" runat="server" Width="150px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    ������ѯҳ
                                </td>
                                <td>
                                    <asp:TextBox ID="tbx_SearchPageURL" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    ��������
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_RelationType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_RelationType_SelectedIndexChanged"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="1">�ֵ�</asp:ListItem>
                                        <asp:ListItem Value="2">��</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="3">������</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="tr_1" runat="server" visible="false">
                                <td class="dataLabel">
                                    ��������
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_RelationTableName" runat="server" DataTextField="TableName"
                                        DataValueField="TableName" OnSelectedIndexChanged="ddl_RelationTableName_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="tr_2" runat="server" visible="false">
                                <td class="dataLabel">
                                    ����ֵ�ֶ�
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_RelationValueField" runat="server" DataTextField="DisplayName"
                                        DataValueField="FieldName">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    &nbsp;�����ı��ֶ�
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_RelationTextField" runat="server" DataTextField="DisplayName"
                                        DataValueField="FieldName">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
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
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row" height="28">
                    <tr>
                        <td>
                            <h3>
                                ����Ԥ���������ֶ�</h3>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                    <tr>
                        <td class="dataLabel" width="120px">
                            ��ѡ��Ԥ���������ֶ�
                        </td>
                        <td class="dataField">
                            <asp:CheckBoxList ID="cbx_PreDefineDO" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="Position">������ְλ</asp:ListItem>
                                <asp:ListItem Value="OrganizeCity">��ǰ����Ƭ��</asp:ListItem>
                                <asp:ListItem Value="OfficeCity">�������´�(ʡ��)</asp:ListItem>
                                <asp:ListItem Value="Remark">��ע</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_AddDefineDataObject" runat="server" Text="�� ��" Width="60px" 
                                onclick="bt_AddDefineDataObject_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
