<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="WebPageDetail.aspx.cs" Inherits="SubModule_UDM_WebPageDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" align="left">
                                    <h2>
                                        系统内WEB页面列表
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Save" runat="server" Text="保存" Width="60px" OnClick="bt_Save_Click" />
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
                                <mcs:MCSTabItem Text="WEB页面信息" Value="0" />
                                <mcs:MCSTabItem Text="页面包含控件" Value="1" />
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="DV_UD_WebPageDetail">
                        </mcs:UC_DetailView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
