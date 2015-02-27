<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PubReportViewer05.aspx.cs" Inherits="SubModule_ReportViewer_PubReportViewer05" %>

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
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="dataLabel">
                                        管理片区
                                    </td>
                                    <td class="dataField">
                                        <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                            ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                                    </td>
                                    <td class="dataLabel">
                                        统计日期
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对"
                                            Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox
                                                ID="tbx_end" runat="server" onfocus="setday(this)" Width="80px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对"
                                            Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                    </td>
                            </table>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Refresh" runat="server" OnClick="bt_Refresh_Click" 
                                Text=" 查 看 " />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="600px" valign="top">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowCredentialPrompts="False"
                    Width="100%" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" 
                    Height="550px" ShowParameterPrompts="False">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
