<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Login_UserDetail, App_Web_w-mwiuzz" enableEventValidation="false" stylesheettheme="basic" %>

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
                                用户登录帐号设置</h2>
                        </td>
                        <td align="right">
                            &nbsp;
                            
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
                                                <h3>
                                                    帐号信息</h3>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" class="tabForm">
                                        <tr>
                                            <td class="dataLabel" style="width: 120px; height: 30px;">
                                                登录名
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_Username" runat="server"></asp:Label>
                                            </td>
                                            <td class="dataLabel" style="width: 120px; height: 30px;">
                                                是否锁定
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_IsLockedOut" runat="server"></asp:Label>
                                            </td>
                                            <td class="dataLabel" style="width: 120px; height: 30px;">
                                                是否启用
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_IsApproved" runat="server"></asp:Label>
                                            </td>
                                             
                                        </tr>
                                        <tr align="center">
                                        <td colspan="6">
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
                                                <h3>
                                                    密码重置</h3>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%" class="tabForm">
                                        <tr>
                                            <td class="dataLabel">
                                                密码
                                            </td>
                                            <td class="dataField">
                                                &nbsp;
                                                <asp:TextBox ID="tbx_Pwd" runat="server" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel">
                                                确认密码
                                            </td>
                                            <td class="dataField">
                                                &nbsp;
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
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true"><ContentTemplate>
                                   <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="29px">
                                        <tr>
                                            <td>
                                                <h3>
                                                    帐号关联信息</h3>
                                            </td>
                                            <td align="right">
                                                <mcs:MCSSelectControl ID="select_Client" runat="server" Width="220" PageUrl='~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2' />
                                                <asp:Button ID="btn_Add" runat="server" Text="添加关联" Width="85" 
                                                    onclick="btn_Add_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <mcs:UC_GridView ID="gv_DI" runat="server" AllowPaging="true" PageSize="10" 
                                            Width="100%" PanelCode="DI_UserList" DataKeyNames="CM_DIUsers_ID" 
                                            AutoGenerateDeleteButton="false" AutoGenerateColumns="False" 
                                            onrowdeleting="gv_DI_RowDeleting">
                                        <Columns>
                                            <asp:CommandField HeaderText="" DeleteText="删除" ButtonType="Link" ShowDeleteButton="true" />
                                        </Columns>
                                    </mcs:UC_GridView>
                                    </ContentTemplate></asp:UpdatePanel>
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
