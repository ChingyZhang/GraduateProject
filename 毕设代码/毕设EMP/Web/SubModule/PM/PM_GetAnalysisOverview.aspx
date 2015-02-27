<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PM_GetAnalysisOverview.aspx.cs" Inherits="SubModule_PM_PM_GetAnalysisOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                导购投产明细
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
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" >
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px"  />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                    <td align="left">
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="4"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="薪资统计分析" Value="0" />
                                                <mcs:MCSTabItem Text="薪资合计汇总" Value="1" />
                                                <mcs:MCSTabItem Text="查看薪资明细" Value="2" />
                                                <mcs:MCSTabItem Text="导购薪资明细" Value="3" />
                                                <mcs:MCSTabItem Text="导购投产明细"  Value="4" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AllowPaging="True" PageSize="50"
                                            OnPageIndexChanging="gv_List_PageIndexChanging" BorderWidth="0px" GridLines="Both"
                                            CellPadding="1" BackColor="#CCCCCC" CellSpacing="1" CssClass="" TotalRecordCount="0">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            <Columns>
                                            </Columns>
                                            <RowStyle BackColor="White" Height="28px" />
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

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 55;
        divGridView.style.height = window.screen.availHeight - 400;            
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
