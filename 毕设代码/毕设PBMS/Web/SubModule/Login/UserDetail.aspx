<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="UserDetail.aspx.cs" Inherits="SubModule_Login_UserDetail" %>

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
                            <h2>用户登录帐号设置</h2>
                        </td>
                        <td align="right">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="29px">
                                        <tr>
                                            <td>
                                                <h3>帐号信息</h3>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" class="tabForm">
                                        <tr>
                                            <td class="dataLabel" style="width: 120px; height: 30px;">登录名
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_Username" runat="server"></asp:Label>
                                            </td>
                                            <td class="dataLabel">Email
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_Email" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbx_Email"
                                                    Display="Dynamic" ErrorMessage="Email无效" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataLabel" style="width: 120px; height: 30px;">关联公司员工
                                            </td>
                                            <td class="dataField" colspan="3">
                                                <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                                    Width="400px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px;" align="center" colspan="4">
                                                <asp:Button ID="bt_OK" runat="server" Text="保存" Width="80px" UseSubmitBehavior="False"
                                                    OnClick="bt_OK_Click" />
                                                <asp:Button ID="bt_Unlock" runat="server" Text="帐户解锁" UseSubmitBehavior="False" Width="80px"
                                                    OnClick="bt_Unlock_Click" />
                                                <asp:Button ID="bt_Approve" runat="server" Text="帐户启用" UseSubmitBehavior="False"
                                                    Width="80px" OnClick="bt_Approve_Click" />
                                                <asp:Button ID="bt_UnApprove" runat="server" Text="帐户停用" Width="80px" OnClick="bt_UnApprove_Click" />
                                                <asp:Button ID="bt_DeleteUser" runat="server" Text="删除帐户" Width="80px" OnClick="bt_DeleteUser_Click"
                                                    OnClientClick="return confirm('是否确认删除该帐户?')" />
                                                <asp:Button ID="bt_ResetPwd" runat="server" Text="密码重置" Width="80px" OnClick="bt_ResetPwd_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" visible="false" id="tr_resetpwd">
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row">
                                        <tr>
                                            <td>
                                                <h3>密码重置</h3>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%" class="tabForm">
                                        <tr>
                                            <td class="dataLabel">密码
                                            </td>
                                            <td class="dataField">&nbsp;
                                                <asp:TextBox ID="tbx_Pwd" runat="server" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel">确认密码
                                            </td>
                                            <td class="dataField">&nbsp;
                                                <asp:TextBox ID="tbx_ConfirmPwd" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbx_Pwd"
                                                    ControlToValidate="tbx_ConfirmPwd" Display="Dynamic" ErrorMessage="密码不一致" ValidationGroup="1"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_ConfirmPwd"
                                                    Display="Dynamic" ErrorMessage="必填" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:Button ID="bt_SavePwd" runat="server" Text="保存" Width="80px" UseSubmitBehavior="False"
                                                    OnClick="bt_SaveResetPwd_Click" ValidationGroup="1" />
                                                <asp:Label ID="lb_Info" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="29px">
                                                                <tr>
                                                                    <td>
                                                                        <h3>员工归属角色</h3>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:DropDownList ID="ddl_Role" runat="server">
                                                                        </asp:DropDownList>
                                                                        <asp:Button ID="bt_AddRole" runat="server" Text="加入角色" OnClick="bt_AddRole_Click" />
                                                                        <asp:Button ID="bt_DeleteRole" runat="server" Text="移出角色" OnClick="bt_DeleteRole_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBoxList ID="cbx_UserInRoles" runat="server">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="10px">&nbsp;
                                            </td>
                                            <td valign="top">
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="29px">
                                                                <tr>
                                                                    <td>
                                                                        <h3>可登录MAC地址</h3>
                                                                    </td>
                                                                    <td align="right">MAC地址：<asp:TextBox ID="tbx_MACAddr" runat="server" Width="200px"></asp:TextBox>&nbsp;<asp:Button
                                                                        ID="bt_AddMAC" runat="server" Text="新增MAC地址" OnClick="bt_AddMAC_Click" />
                                                                        <asp:Button ID="bt_FindMacAddr" runat="server" OnClick="bt_FindMacAddr_Click" Text="查找" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <mcs:UC_GridView ID="gv_MACAddrList" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                DataKeyNames="ID" OnRowDeleting="gv_MACAddrList_RowDeleting" PageSize="10" AllowPaging="true" OnPageIndexChanging="gv_MACAddrList_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:BoundField DataField="MacAddr" HeaderText="MAC地址" SortExpression="MacAddr" />
                                                                    <asp:BoundField DataField="Enabled" HeaderText="启用标志" SortExpression="Enabled" />
                                                                    <asp:BoundField DataField="InsertTime" HeaderText="录入日期" SortExpression="InsertTime" />
                                                                    <asp:BoundField DataField="InsertStaff" HeaderText="录入人" SortExpression="InsertStaff" />
                                                                    <asp:TemplateField ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="bt_Delete" runat="server" CausesValidation="False" OnClientClick="return confirm('是否确认删除该记录?')"
                                                                                CommandName="Delete" Text="删除" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </mcs:UC_GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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
