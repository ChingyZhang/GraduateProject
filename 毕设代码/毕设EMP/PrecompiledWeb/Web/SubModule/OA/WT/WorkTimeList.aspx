﻿<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_WT_index, App_Web_qacqtfgx" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                --工作考勤--
                            </h2>
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="我的考勤" Description=""
                            Value="0" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="他人考勤" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td height="30px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr runat="server" id="tr_basicsearch">
                                <td class="dataLabel">
                                    开始日期
                                    <asp:TextBox ID="tbx_begindate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox><span
                                        style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begindate"
                                        Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    至
                                    <asp:TextBox ID="tbx_enddate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox><span
                                        style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                            runat="server" ControlToValidate="tbx_enddate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_enddate"
                                        Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </td>
                                
                                <td class="dataLabel" id="td_OrganizeCity" runat="server">
                                    管理片区
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="160px" DisplayRoot="True" />
                                </td>
                                <td class="dataLabel" id="td_SelectStaff" runat="server">
                                    填报人
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="120" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查 询" Width="60px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr height="1px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="WT_WorkTime_ID" PageSize="15" Width="100%"
                            PanelCode="Panel_OA_WT_WorkTimeList001">
                            <Columns>
                                <%--<asp:HyperLinkField DataNavigateUrlFields="WT_WorkTime_ID" DataNavigateUrlFormatString="WorkTimeDetail.aspx?ID={0}"
                                    Text="详细信息" ControlStyle-CssClass="listViewTdLinkS1"  />--%>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

