<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt_ReportDetail.aspx.cs" Inherits="SubModule_Reports_Rpt_ReportDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="报表详细信息"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="80px" Text="保 存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_ViewReport" runat="server" Width="80px" Text="查看报表" 
                                onclick="bt_ViewReport_Click" />
                            <asp:Button ID="bt_Delete" runat="server" Text="删 除" Width="80px"  
                                OnClientClick="return confirm('该操作请谨慎操作，是否确认删除？')" onclick="bt_Delete_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0">
                    <Items>
                        <mcs:MCSTabItem Text="基础信息" Value="0" Enable="false" />
                        <mcs:MCSTabItem Text="列表字段" Value="1" />
                        <mcs:MCSTabItem Text="透视表字段" Value="2" />
                        <mcs:MCSTabItem Text="图表定义" Value="3" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_Rpt_Report_Detail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
