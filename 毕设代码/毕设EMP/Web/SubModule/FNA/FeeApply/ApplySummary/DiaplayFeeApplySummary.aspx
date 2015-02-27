<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="DiaplayFeeApplySummary.aspx.cs" Inherits="SubModule_FNA_FeeApply_ApplySummary_DiaplayFeeApplySummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                陈列提报汇总信息
                            </h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="90px" />
                        </td>
                        <td align="right" nowrap="noWrap" width="300px">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="bt_Approve" runat="server" ForeColor="Blue" OnClick="bt_Approve_Click"
                                        OnClientClick="return confirm('是否确认将该条件范围内所有申请单批量设为审批通过？注意该操作可能耗时较长，请耐心等待！')"
                                        Text="批量审批通过" Width="90px" Visible="false" />
                                    <asp:Button ID="bt_UnApprove" runat="server" ForeColor="Red" OnClick="bt_UnApprove_Click"
                                        OnClientClick="return confirm('是否确认将该条件范围内所有申请单批量设为审批不通过？该操作请谨慎操作！！！注意该操作可能耗时较长，请耐心等待')"
                                        Text="批量审批不通过" Width="90px" Visible="false" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                <asp:UpdatePanel ID="upl_conditon" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    查看层级
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Level" runat="server" DataValueField="Key" DataTextField="Value">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    申请月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                        <asp:ListItem Value="0">所有(含已提交及已通过)</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">待我审批的申请单</asp:ListItem>
                                        <asp:ListItem Value="2">已审批通过的申请单</asp:ListItem>
                                        <asp:ListItem Value="3">由我审批通过的申请单</asp:ListItem>
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
                                <tr>
                                    <td align="left" height="28px" valign="bottom">
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="按卖场类型汇总" Value="0"/>
                                                <mcs:MCSTabItem Text="按陈列方式汇总" Value="1" />
                                                <mcs:MCSTabItem Text="按付款周期汇总" Value="2" />
                                                <mcs:MCSTabItem Text="陈列费用明细" Value="3" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" OnDataBound="gv_List_DataBound"
                                            GridLines="Both" CellPadding="1" BackColor="#CCCCCC" CellSpacing="1" CssClass=""
                                            BorderWidth="0px" AllowPaging="true" PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging">
                                            <HeaderStyle BackColor="#EEEEEE" CssClass="" Height="28px" />
                                            <Columns>
                                            </Columns>
                                            <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Approve" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_UnApprove" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 40;
        divGridView.style.height = window.screen.availHeight - 350;         
    </script>

    <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick">
    </asp:Timer>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
