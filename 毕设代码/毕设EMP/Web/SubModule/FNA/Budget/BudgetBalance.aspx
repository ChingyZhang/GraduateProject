<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="BudgetBalance.aspx.cs" Inherits="SubModule_FNA_Budget_BudgetBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        管理片区预算查询
                                    </h2>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        查询条件</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" />
                                </td>
                                <td class="dataLabel">
                                    区域级别
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Level" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        预算额度余额信息</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_BalanceList" runat="server" Width="100%" DataKeyNames="OrganizeCity"
                            AllowPaging="True" PageSize="15" OnSelectedIndexChanged="gv_BalanceList_SelectedIndexChanged"
                            OnPageIndexChanging="gv_BalanceList_PageIndexChanging" OnDataBound="gv_BalanceList_DataBound">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                            Text="查看明细" Visible='<%# ((int)Eval("OrganizeCity"))!=0%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
                <tr runat="server" id="tr_BalanceChangeList" visible="false">
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                                SelectedIndex="0" Width="100%">
                                                <Items>
                                                    <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="预算变动明细" Description=""
                                                        Value="0" Enable="True"></mcs:MCSTabItem>
                                                    <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="预算分配明细" Description=""
                                                        Value="1" Enable="True"></mcs:MCSTabItem>
                                                </Items>
                                            </mcs:MCSTabControl>
                                        </td>
                                    </tr>
                                    <tr class="tabForm">
                                        <td>
                                            <table cellpadding="0" cellspacing="0" border="0" >
                                                <tr>
                                                    <td class="dataLabel">
                                                        费用类型
                                                    </td>
                                                    <td class="dataField">
                                                        <asp:DropDownList ID="ddl_FeeType" runat="server" DataTextField="Value" DataValueField="Key"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddl_FeeType_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <mcs:UC_GridView ID="gv_ChangeList" runat="server" Width="100%" AutoGenerateColumns="False"
                                                PanelCode="Panel_FNA_BudgetBalanceChangeList" AllowPaging="True" 
                                                PageSize="15">
                                            </mcs:UC_GridView>
                                            <mcs:UC_GridView ID="gv_BudgetList" runat="server" Width="100%" AutoGenerateColumns="False"
                                                PanelCode="Panel_FNA_BudgetAllocateList" AllowPaging="True" PageSize="15">
                                            </mcs:UC_GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
