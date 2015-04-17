<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="RetailerDetail.aspx.cs" Inherits="SubModule_RM_RetailerDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap" style="height: 24px;">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="零售商详细信息" /></h2>
                        </td>

                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="提交审核" Width="60px" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审核通过" Width="70px" />
                            <asp:Button ID="bt_UnApprove" runat="server" OnClick="bt_UnApprove_Click" Text="审核不通过" Width="70px" />
                            <asp:Button ID="bt_Map" runat="server" Text="地图位置" Width="60px" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_RT_RetailDetail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
