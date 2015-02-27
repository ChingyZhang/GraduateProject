<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt_ReportList.aspx.cs" Inherits="SubModule_Reports_Rpt_ReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td valign="top" width="180px">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td align="right" width="20">
                                                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td align="left" width="150">
                                                <h2>
                                                    报表目录</h2>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="180px" valign="top">
                                    <asp:TreeView ID="tr_List" runat="server" Width="100%" ImageSet="Msdn" ExpandDepth="1"
                                        OnSelectedNodeChanged="tr_List_SelectedNodeChanged">
                                        <NodeStyle CssClass="listViewTdLinkS1" />
                                        <SelectedNodeStyle BackColor="#E0E0E0" ForeColor="Red" />
                                    </asp:TreeView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td width="24">
                                                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td nowrap="noWrap" style="width: 180px">
                                                <h2>
                                                    <asp:Label ID="lb_PageTitle" runat="server" Text="报表列表"></asp:Label></h2>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                                                <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                           
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            DataKeyNames="Rpt_Report_ID" PageSize="15" Width="100%" PanelCode="Panel_Rpt_Report_List">
                                                            <Columns>
                                                                <asp:HyperLinkField DataNavigateUrlFields="Rpt_Report_ID" DataNavigateUrlFormatString="ReportViewer.aspx?Report={0}"
                                                                    Text="查看报表" ControlStyle-CssClass="listViewTdLinkS1" >
                                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                                </asp:HyperLinkField>
                                                                <asp:HyperLinkField DataNavigateUrlFields="Rpt_Report_ID" DataNavigateUrlFormatString="Rpt_ReportDetail.aspx?ID={0}"
                                                                    Text="设计报表" ControlStyle-CssClass="listViewTdLinkS1" >
                                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                                </asp:HyperLinkField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                无数据
                                                            </EmptyDataTemplate>
                                                        </mcs:UC_GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
