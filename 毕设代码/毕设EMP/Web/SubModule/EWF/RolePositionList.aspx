﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="RolePositionList.aspx.cs" Inherits="SubModule_EWF_RolePositionList" %>

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
                        <td nowrap="noWrap" style="width: 355px">
                            <h2>
                                职位相关角色列表</h2>
                        </td>
                        <td align="right">
                            角色ID:<asp:Label ID="lb_ID" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" OnClick="bt_Save_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left" colspan="4" height="22" valign="middle">
                            <h4>
                                基本信息</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            角色名称
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_Name" Width="120px" runat="server"></asp:TextBox>
                            <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbx_Name" ErrorMessage="不能为空"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class="dataLabel" style="width: 120px;">
                            角色类型
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Type" DataTextField="Value" DataValueField="Key" Width="120px"
                                runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            描述
                        </td>
                        <td class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_Description" Width="400px" runat="server"></asp:TextBox>
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
            <td class="tabForm">
                <table id="Table4" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                    <tr>
                        <td align="left" colspan="3" valign="middle">
                            <h4>
                                新增关联的职位</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="height: 30px;">
                            职位
                        </td>
                        <td class="dataField" style="height: 30px;">
                            <mcs:MCSTreeControl ID="tr_SuperPosition" runat="server" IDColumnName="ID" 
                                NameColumnName="Name" ParentColumnName="SuperID" RootValue="0" Width="300px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                Visible="True" ValidationGroup="2" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" valign="middle">
                            <h4>
                                已关联的职位</h4>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gv_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="Position" Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging"
                                        OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnSorting="gv_List_Sorting"
                                        OnRowDeleting="gv_List_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="RoleName" HeaderText="角色名称" SortExpression="RoleName" />
                                            <asp:BoundField DataField="PositionName" HeaderText="职务名称" SortExpression="PositionName" />
                                            <asp:ButtonField CommandName="Delete" Text="删除" ControlStyle-CssClass="listViewTdLinkS1"
                                                ItemStyle-Width="30px" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Add" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
