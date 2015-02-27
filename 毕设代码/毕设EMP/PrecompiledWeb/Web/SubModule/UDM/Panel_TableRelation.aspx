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
                                        Panel与表关系定义
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Text="新 增" OnClick="bt_OK_Click" Width="60px" />
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
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="详细信息维护" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="包含数据表维护" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="数据表关系维护" Description=""
                                    Value="2" Enable="True" Visible="true"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="模块字段维护" Description=""
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
                                    Panel名
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lbl_PanelName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    主表
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ParentTable" runat="server" DataTextField="DisplayName"
                                        DataValueField="TableID" OnSelectedIndexChanged="ddl_ParentTable_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    主表字段
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ParentField" runat="server" DataTextField="DisplayName"
                                        DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    子表
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ChildTable" runat="server" DataTextField="DisplayName"
                                        DataValueField="TableID" OnSelectedIndexChanged="ddl_ChildTable_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    子表字段
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ChildField" runat="server" DataTextField="DisplayName"
                                        DataValueField="ID" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    关联方式
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RelateionMode" runat="server" DataTextField="Value" DataValueField="Key">
                                        <asp:ListItem Value="INNER JOIN" Selected="True">内联</asp:ListItem>
                                        <asp:ListItem Value="LEFT OUTER JOIN">左关联</asp:ListItem>
                                        <asp:ListItem Value="RIGHT OUTER JOIN">右关联</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    &nbsp;排序
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_SortID" runat="server" Width="50px">1</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_SortID"
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_SortID"
                                        Display="Dynamic" ErrorMessage="必需为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    关联条件
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
                                            <asp:BoundField DataField="PanelName" HeaderText="Panel名称" SortExpression="PanelName"
                                                Visible="false" />
                                            <asp:BoundField DataField="ParentTableID" HeaderText="主表" SortExpression="ParentTableID" />
                                            <asp:BoundField DataField="ParentFieldID" HeaderText="主表字段" SortExpression="ParentFieldID" />
                                            <asp:BoundField DataField="ChildTableID" HeaderText="子表" SortExpression="ChildTableID" />
                                            <asp:BoundField DataField="ChildFieldID" HeaderText="子表字段" SortExpression="ChildFieldID" />
                                            <asp:BoundField DataField="JoinMode" HeaderText="关联方式" SortExpression="JoinMode" />
                                            <asp:TemplateField HeaderText="顺序编号">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SortID" runat="server" Text='<%# Bind("SortID") %>' Width="20px"></asp:Label>
                                                    <asp:Button ID="bt_Increase" runat="server" OnClick="bt_Increase_Click" Text="+" />
                                                    <asp:Button ID="bt_Decrease" runat="server" OnClick="bt_Decrease_Click" Text="-" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RelationCondition" HeaderText="关联条件" SortExpression="RelationCondition" />
                                            <asp:CommandField SelectText="选择" ShowSelectButton="True" ControlStyle-CssClass="listViewTdLinkS1" />
                                            <asp:CommandField DeleteText="删除" ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1" />
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
