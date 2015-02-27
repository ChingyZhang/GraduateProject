<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Login_ChangePassword, App_Web_w-mwiuzz" enableEventValidation="false" stylesheettheme="basic" %>

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
                                修改用户密码</h2>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td height="30"> </td></tr>
        <tr>
            <td align="center">
                <table width="230" >
                    <tr>
                        <td width="55" align="right">原密码:</td>
                        <td>
                            <asp:TextBox ID="tbx_OrgPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" Text="*" ErrorMessage="必填!" ControlToValidate="tbx_OrgPassword"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td width="55" align="right">新密码:</td>
                        <td><asp:TextBox ID="tbx_NewPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" Text="*" ErrorMessage="必填!" ControlToValidate="tbx_NewPassword"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td width="55" align="right">确认密码:</td>
                        <td><asp:TextBox ID="tbx_RePassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" Text="*" ErrorMessage="必填!" ControlToValidate="tbx_RePassword"></asp:RequiredFieldValidator><asp:CompareValidator
                                ID="CompareValidator1" runat="server" Text="*"  ErrorMessage="与原密码不同！"  ControlToCompare="tbx_NewPassword" ControlToValidate="tbx_RePassword"></asp:CompareValidator>
                                </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="color:Red">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal> </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="226">
                                <tr >
                                    <td align="right"><asp:Button ID="bt_Back" height="20" runat="server" Text="返回" CausesValidation="false" /></td>
                                    <td><asp:Button ID="bt_OK" height="20" runat="server" Text="确认修改" onclick="bt_OK_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td height="50"> </td></tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
