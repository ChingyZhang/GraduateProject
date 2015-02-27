<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="Web_Panel_TableRelation, App_Web_bl88rr1i" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" align="left">
                                    <h2>
                                        Panel����ϵ����
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Text="�� ��" OnClick="bt_OK_Click" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="2" Width="100%">
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="��ϸ��Ϣά��" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="�������ݱ�ά��" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="���ݱ��ϵά��" Description=""
                                    Value="2" Enable="True" Visible="true"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="ģ���ֶ�ά��" Description=""
                                    Value="3" Enable="True" Visible="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="dataLabel">
                                    ID
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lbl_ID" runat="server"></asp:Label>
                                </td>
                                <td class="dataLabel">
                                    Panel��
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lbl_PanelName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    ����
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ParentTable" runat="server" DataTextField="DisplayName"
                                        DataValueField="TableID" OnSelectedIndexChanged="ddl_ParentTable_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    �����ֶ�
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ParentField" runat="server" DataTextField="DisplayName"
                                        DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    �ӱ�
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ChildTable" runat="server" DataTextField="DisplayName"
                                        DataValueField="TableID" OnSelectedIndexChanged="ddl_ChildTable_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    �ӱ��ֶ�
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ChildField" runat="server" DataTextField="DisplayName"
                                        DataValueField="ID" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    ������ʽ
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RelateionMode" runat="server" DataTextField="Value" DataValueField="Key">
                                        <asp:ListItem Value="INNER JOIN" Selected="True">����</asp:ListItem>
                                        <asp:ListItem Value="LEFT OUTER JOIN">�����</asp:ListItem>
                                        <asp:ListItem Value="RIGHT OUTER JOIN">�ҹ���</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    &nbsp;����
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_SortID" runat="server" Width="50px">1</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_SortID"
                                        Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_SortID"
                                        Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    ��������
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_RelationCondition" runat="server" Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="100%">
                                    <mcs:UC_GridView ID="gv_Relation" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="ID" OnRowDeleting="gv_Relation_RowDeleting" OnSelectedIndexChanging="gv_Relation_SelectedIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false" />
                                            <asp:BoundField DataField="PanelName" HeaderText="Panel����" SortExpression="PanelName"
                                                Visible="false" />
                                            <asp:BoundField DataField="ParentTableID" HeaderText="����" SortExpression="ParentTableID" />
                                            <asp:BoundField DataField="ParentFieldID" HeaderText="�����ֶ�" SortExpression="ParentFieldID" />
                                            <asp:BoundField DataField="ChildTableID" HeaderText="�ӱ�" SortExpression="ChildTableID" />
                                            <asp:BoundField DataField="ChildFieldID" HeaderText="�ӱ��ֶ�" SortExpression="ChildFieldID" />
                                            <asp:BoundField DataField="JoinMode" HeaderText="������ʽ" SortExpression="JoinMode" />
                                            <asp:TemplateField HeaderText="˳����">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SortID" runat="server" Text='<%# Bind("SortID") %>' Width="20px"></asp:Label>
                                                    <asp:Button ID="bt_Increase" runat="server" OnClick="bt_Increase_Click" Text="+" />
                                                    <asp:Button ID="bt_Decrease" runat="server" OnClick="bt_Decrease_Click" Text="-" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RelationCondition" HeaderText="��������" SortExpression="RelationCondition" />
                                            <asp:CommandField SelectText="ѡ��" ShowSelectButton="True" ControlStyle-CssClass="listViewTdLinkS1" />
                                            <asp:CommandField DeleteText="ɾ��" ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1" />
                                        </Columns>
                                    </mcs:UC_GridView>
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
