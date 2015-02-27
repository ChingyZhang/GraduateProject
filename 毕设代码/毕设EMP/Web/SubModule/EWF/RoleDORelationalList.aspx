<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="RoleDORelationalList.aspx.cs" Inherits="SubModule_EWF_RoleDORelationalList" %>

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
                                数据相关的角色列表</h2>
                        </td>
                        <td align="right">
                            角色ID:<asp:Label ID="lb_ID" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                        <td align="right">
                            名称:
                            <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                            <asp:Button ID="bt_Find" runat="server" Text="查询" Width="60px" OnClick="bt_Find_Click" />
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="Table4" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td align="left" colspan="5" valign="middle">
                                    <h4>
                                        数据相关角色信息</h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="height: 30px;">
                                    数据对象值
                                </td>
                                <td class="dataLabel" style="height: 30px;">
                                    <asp:TextBox ID="tbx_ScrValue" Width="120px" runat="server"></asp:TextBox>
                                    <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_ScrValue"
                                        ErrorMessage="不能为空" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>
                                </td>
                                <td class="dataLabel" style="height: 30px;">
                                    审批人名
                                </td>
                                <td class="dataLabel" style="height: 30px;">
                                    <mcs:MCSSelectControl ID="select_Recipient" runat="server" 
                                        PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx" Width="200px">
                                    </mcs:MCSSelectControl>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                        Visible="True" ValidationGroup="2" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gv_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="ScrValue,RecipientStaff" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                        OnSorting="gv_List_Sorting" OnRowDeleting="gv_List_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="ScrValue" HeaderText="数据对象值" SortExpression="ScrValue" />
                                            <asp:BoundField DataField="RecipientStaffCode" HeaderText="审批人工号" SortExpression="RecipientStaffCode" />
                                            <asp:BoundField DataField="RecipientStaffName" HeaderText="审批人姓名" SortExpression="RecipientStaffName" />
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
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
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
