<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_EWF_Apply, App_Web_8sm6e0fs" enableEventValidation="false" stylesheettheme="basic" %>

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
                                审批流程发起申请</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_OK" runat="server" Text="确定发起" Width="80px" OnClick="btn_OK_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr>
                        <td align="left">
                            <h4>
                                基本信息</h4>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" height="26" class="dataLabel" width="60">
                            发起人
                        </td>
                        <td height="26" class="dataField">
                            <asp:Label ID="lbl_Applyer" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td height="26" class="dataLabel" width="60">
                            所属流程
                        </td>
                        <td height="26" class="dataField">
                            <asp:Label ID="lbl_AppName" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                        <td height="26" class="dataLabel" width="60">
                            流程主题
                        </td>
                        <td height="26" class="dataField">
                            <asp:TextBox ID="tbx_Topic" runat="server" Width="200px"></asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填" 
                                ControlToValidate="tbx_Topic" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td height="26" class="dataLabel" width="60">
                            发起日期
                        </td>
                        <td height="26" class="dataField">
                            <asp:Label ID="lb_StartTime" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td height="26" class="dataLabel" width="60">
                            截止日期
                        </td>
                        <td height="26" class="dataField">
                            <asp:Label ID="lb_EndTime" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td height="26" class="dataField" align="right">
                            <asp:HyperLink ID="hyl_RelateURL" runat="server" CssClass="listViewTdLinkS1" >查看详细申请信息</asp:HyperLink>
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
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <mcs:UC_EWFPanel ID="pl_dataobjectinfo" runat="server">
                        </mcs:UC_EWFPanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
