<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="EndDateInventorySummary.aspx.cs" Inherits="SubModule_SVM_EndDateInventorySummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lbl_Message" runat="server">全国经销商每月截止20号（含）期末实物库存SKU数量</asp:Label>
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="60px" />
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    类别
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_IsCXP" runat="server">
                                        <asp:ListItem Text="成品" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="赠品" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    活跃标志
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ActiveFlag" DataTextField="Value" DataValueField="Key"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_Summary" runat="server" Width="96%" CellPadding="1" CellSpacing="1"
                                            BorderWidth="0px" AllowPaging="true" PageSize="50" GridLines="Both" BackColor="#CCCCCC"
                                            OnRowDataBound="gv_Summary_RowDataBound" CssClass="" OnPageIndexChanging="gv_Summary_PageIndexChanging">
                                            <HeaderStyle BackColor="#EEEEEE" CssClass="" Height="28px" />
                                            <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                            <Columns>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
