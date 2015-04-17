<%@ Page Language="C#" AutoEventWireup="true" CodeFile="readmail.aspx.cs" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    Inherits="SubModule_OA_Mail_readmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" name="hdnMailID" value="<%=MailID%>">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td align="right" width="20">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td align="left" width="150">
                            <h2>
                                阅读邮件</h2>
                        </td>
                        <td align="right">
                            <input onclick="javascript:self.location='Compose.aspx?Action=1&amp;MailID=<%=MailID%>'"
                                type="button" value="回 复" class="button">
                            <input onclick="javascript:self.location='Compose.aspx?Action=2&amp;MailID=<%=MailID%>'"
                                type="button" value="转 发" class="button">
                            <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="删 除" OnClick="btnDelete_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="3" cellpadding="3" width="100%" border="0">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" class="tabDetailView">
                                <tr>
                                    <td colspan="2" class="tabDetailViewDL2">
                                        <asp:Label ID="lblSubject" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" width="100" class="tabDetailViewDL2">
                                        发送人:
                                    </td>
                                    <td class="tabDetailViewDF2">
                                        <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" width="100" class="tabDetailViewDL2">
                                        收件人:
                                    </td>
                                    <td class="tabDetailViewDF2">
                                        <asp:Label ID="lblReceiver" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" width="100" class="tabDetailViewDL2">
                                        抄送人:
                                    </td>
                                    <td class="tabDetailViewDF2">
                                        <asp:Label ID="lblCcToAddr" runat="server" Width="538px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" width="100" class="tabDetailViewDL2">
                                        发送时间:
                                    </td>
                                    <td class="tabDetailViewDF2">
                                        <asp:Label ID="lblSendTime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="left" width="100" class="tabDetailViewDL2">
                                        附件:
                                    </td>
                                    <td class="tabDetailViewDF2">
                                        &nbsp;
                                        <asp:Label ID="lblAttachFile" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="5" cellpadding="5" width="100%" class="tabDetailView" height="400px">
                                <tr>
                                    <td valign="top" >
                                        <asp:Literal ID="lblContent" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
