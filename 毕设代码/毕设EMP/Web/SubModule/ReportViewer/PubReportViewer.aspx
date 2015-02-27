<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="PubReportViewer.aspx.cs" Inherits="SubModule_ReportViewer_PubReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table2" class="moduleTitle"
                    height="28">
                    <tr>
                        <td align="right" width="20">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td align="left">
                            <h2>
                                查看统计报表</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Refresh" runat="server" OnClick="bt_Refresh_Click" Text=" 查 看 "
                                Visible="False" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="600px" valign="top">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowCredentialPrompts="False"
                    Width="100%" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" Height="550px">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
