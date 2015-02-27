<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CSO_CSO_OfferBalance_Summary, App_Web_quved-rv" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="提取费用汇总"></asp:Label></h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td nowrap>
                            <h3>
                                查询条件
                            </h3>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="btn_Approve" Width="100px" Font-Bold="true" ForeColor="Red" Text="批量审批通过"
                                Enabled="false" runat="server" OnClick="btn_Approve_Click" OnClientClick="return confirm('是否确认将该管理区域内的所有申请,批量设为审批通过?')" />
                            <asp:Button ID="btn_UnApprove" Width="100px" Font-Bold="true" ForeColor="Blue" Text="审批不通过"
                                runat="server" Enabled="false" OnClick="btn_UnApprove_Click" OnClientClick="return confirm('是否确认将该管理区域内的所有申请,批量设为审批不通过?')" />
                            <asp:Button ID="btn_Export" runat="server" OnClick="btn_Export_Click" Text="导出为Excel"
                                Width="80px" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="dataLabel" width="80px">
                            管理片区
                        </td>
                        <td class="dataField" width="320px">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                        </td>
                        <td class="dataLabel" width="80px">
                            查看层级
                        </td>
                        <td class="dataField" width="320px">
                            <asp:DropDownList ID="ddl_Level" runat="server" DataValueField="Key" DataTextField="Value">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" width="80px">
                            结算月份
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_AccountMonth" runat="server" AutoPostBack="True" DataTextField="Name"
                                DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" width="80px">
                            审批状态
                        </td>
                        <td class="dataField" width="320px">
                            <asp:DropDownList ID="ddl_State" runat="server">
                                <asp:ListItem Selected="True" Value="0">所有(含已提交及已通过)</asp:ListItem>
                                <asp:ListItem Value="1">待我审批的申请单</asp:ListItem>
                                <asp:ListItem Value="2">已审批通过的申请单</asp:ListItem>
                                <asp:ListItem Value="3">由我审批通过的申请单</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" width="80px">
                            <asp:DropDownList ID="ddl_ComparerField" runat="server">
                                <asp:ListItem Value="1">本月派样185数量</asp:ListItem>
                                <asp:ListItem Value="2">本月派样400数量</asp:ListItem>
                                <asp:ListItem Value="3">本月新客数量</asp:ListItem>
                                <asp:ListItem Value="4" Selected="True">本月支付费用</asp:ListItem>
                                <asp:ListItem Value="5">较上月新客增长量</asp:ListItem>
                                <asp:ListItem Value="6">较上月新客增长率</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dataField" width="320px">
                            <asp:DropDownList ID="ddl_ApplyCostOP" runat="server">
                                <asp:ListItem Value="&gt;">大于</asp:ListItem>
                                <asp:ListItem Value="&lt;">小于</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="tbx_ApplyCost" runat="server" Width="80px">0</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                Display="Dynamic" ErrorMessage="必需为数字格式" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </td>
                        <td class="dataLabel">
                        </td>
                        <td class="dataField">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left">
                                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                    OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                    <Items>
                                        <mcs:MCSTabItem Text="查看汇总" Value="1" />
                                        <mcs:MCSTabItem Text="查看明细" Value="2" />
                                    </Items>
                                </mcs:MCSTabControl>
                            </td>
                        </tr>
                        <tr class="tabForm">
                            <td align="left">
                                <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" GridLines="Both" CellPadding="0"
                                    BackColor="#BBBBBB" CellSpacing="0" CssClass="" BorderWidth="1px" BorderStyle="Solid"
                                    BorderColor="Black" AllowPaging="true" PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging"
                                    OnRowDataBound="gv_List_RowDataBound">
                                    <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                    <Columns>
                                    </Columns>
                                    <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                </mcs:UC_GridView>
                                <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                    PanelCode="Panel_CSO_OfferBalanceDetailList_01" AllowPaging="True" PageSize="15"
                                    Visible="false">
                                    <Columns>
                                    </Columns>
                                </mcs:UC_GridView>
                            </td>
                        </tr>
                    </table>
                    <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_Approve" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btn_UnApprove" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 40;
        divGridView.style.height = window.screen.availHeight - 450;            
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
