<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="AdvanceFind.aspx.cs" Inherits="SubModule_CM_RT_AdvanceFind" %>

<%@ Register Src="../../../Controls/AdvancedSearch.ascx" TagName="AdvancedSearch"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table4" class="moduleTitle">
        <tr>
            <td align="right" width="20">
                <img height="16" src="../../../DataImages/ClientManage.gif" style="width: 18px">
            </td>
            <td align="left" style="width: 77px">
                <h2>
                    物业列表</h2>
            </td>
            <td align="left">
                <asp:Label ID="lb_info" runat="server" ForeColor="Red"></asp:Label>&nbsp;
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" SelectedIndex="1"
                    OnOnTabClicked="MCSTabControl1_OnTabClicked">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="快捷查询" Description=""
                            Value="0" Enable="True" Visible="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="高级查询" Description=""
                            Value="1" Enable="True" Visible="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm" runat="server" id="tr_AdvancedSearch">
            <td>
                <uc1:AdvancedSearch ID="AdvancedSearch1" runat="server" PanelCode="Panel_PD_List_001"
                    ExtCondition="MCS_CM.dbo.CM_Client.ClientType=6" OnSelectedChanging="AdvancedSearch1_OnSelectedChanging" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
