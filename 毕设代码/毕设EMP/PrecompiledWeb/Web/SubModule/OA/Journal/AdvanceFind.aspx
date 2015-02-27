﻿<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CM_RT_AdvanceFind, App_Web_n8pevkz9" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../../Controls/AdvancedSearch.ascx" TagName="AdvancedSearch"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="Table4" class="moduleTitle">
        <tr>
            <td align="right" style="width: 11px">
                <img height="16" src="../../../DataImages/ClientManage.gif" style="width: 15px">
            </td>
            <td align="left" style="width: 90px">
                <h2>
                    日志查询</h2>
            </td>
            <td align="left" style="width: 496px">
                <asp:Label ID="lb_info" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td align="right" style="width: 101px">
            </td>
            <td align="right" style="width: 101px">
            </td>
        </tr>
    </table>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" SelectedIndex="1"
                    OnOnTabClicked="MCSTabControl1_OnTabClicked" Width="100%">
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
                <uc1:AdvancedSearch ID="as_uc_Journal" runat="server" PanelCode="Panel_Journal_list" OnSelectedChanging ="as_uc_Journal_OnSelectedChanging"
                     />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
