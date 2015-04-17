<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="StaffDetail.aspx.cs" Inherits="SubModule_PBM_Partner_Staff_StaffDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16"></td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="员工详细信息"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_CreateUser" runat="server" OnClick="bt_CreateUser_Click" Text="创建登录账户" Width="80px" />
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_TDP_Staff_Detail_01">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pl_RoleList" runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                <table class="h3Row" height="28" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td>
                                            <h3>员工登录账户及角色设定</h3>
                                        </td>
                                        <td>登录用户名：<asp:Label ID="lb_Username" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td align="right">
                                            <asp:Button ID="bt_SetUserInRole" runat="server" Text="设置用户角色" Width="80px" OnClick="bt_SetUserInRole_Click" />
                                            <asp:Button ID="bt_Unlock" runat="server" Text="帐户解锁" UseSubmitBehavior="False" Width="80px"
                                                OnClick="bt_Unlock_Click" />
                                            <asp:Button ID="bt_OnApprove" runat="server" Text="帐户启用" UseSubmitBehavior="False"
                                                Width="80px" OnClick="bt_OnApprove_Click" />
                                            <asp:Button ID="bt_UnApprove" runat="server" Text="帐户停用" Width="80px" OnClick="bt_UnApprove_Click" />
                                            <asp:Button ID="bt_DeleteUser" runat="server" Text="删除帐户" Width="80px" OnClick="bt_DeleteUser_Click"
                                                OnClientClick="return confirm('是否确认删除该帐户?')" />
                                            <asp:Button ID="bt_ResetPwd" runat="server" Text="密码重置" Width="80px" OnClick="bt_ResetPwd_Click" />
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="cbx_Roles" runat="server" RepeatDirection="Horizontal">
                                </asp:CheckBoxList></td>
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
                    </table>
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

