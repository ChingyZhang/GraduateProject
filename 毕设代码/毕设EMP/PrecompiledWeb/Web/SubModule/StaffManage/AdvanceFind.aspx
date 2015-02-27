<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_AdvanceFind, App_Web_it73ecr1" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../Controls/AdvancedSearch.ascx" TagName="AdvancedSearch" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="15px">
                            <img height="16" src="../../DataImages/ClientManage.gif">
                        </td>
                        <td width="120px">
                            <h2>
                                员工高级查询</h2>
                        </td>
                        <td>
                            <asp:Label ID="lb_info" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
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
                            <uc1:AdvancedSearch ID="as_uc_staff" runat="server" PanelCode="Panel_Staff_List_001" OnSelectedChanging="as_uc_staff_OnSelectedChanging"  />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
