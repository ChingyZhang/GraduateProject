<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_RoleStaticList, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

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
                                静态人员角色列表</h2>
                        </td>
                        <td>
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="Table4" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td align="left" colspan="3" valign="middle">
                                    <h4>
                                        数据相关角色信息</h4>
                                </td>
                            </tr>
                            <tr>
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
        <tr class="tabForm">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gv_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="RecipientStaff" PageSize="15" Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging"
                                        OnSorting="gv_List_Sorting" OnRowDeleting="gv_List_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="RecipientStaffCode" HeaderText="审批人工号" SortExpression="RecipientLoginName" />
                                            <asp:BoundField DataField="RecipientStaffName" HeaderText="人员名称" SortExpression="RecipientStaffName" />
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
