<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_FeeApplyAndWriteoffSummaryByDistributor, App_Web_wwzcbdkp" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="下游零售商费用统计表"></asp:Label></h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="dataLabel" width="100px">
                                    经销商
                                </td>
                                <td class="dataField" width="230px">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../PopSearch/Search_SelectClient.aspx?ClientType=2"
                                        Width="200px" />
                                </td>
                                <td class="dataLabel" width="100px">
                                    费用类型
                                </td>
                                <td class="dataField" width="230px">
                                    <asp:DropDownList ID="ddl_FeeType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    申请开始月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    截至月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                                </td>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
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
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" GridLines="Both" CellPadding="1"
                                            BackColor="#BBBBBB" CellSpacing="1" CssClass="" BorderWidth="0px" AllowPaging="true"
                                            PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            <Columns>
                                            </Columns>
                                            <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                            PanelCode="Panel_FNA_FeeApplyList" DataKeyNames="FNA_FeeApply_ID" AllowPaging="True"
                                            PageSize="15" AllowSorting="true">
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="FNA_FeeApply_ID" DataNavigateUrlFormatString="FeeApply/FeeApplyDetail3.aspx?ID={0}"
                                                    DataTextField="FNA_FeeApply_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                                                    HeaderText="申请备案号"  SortExpression="FNA_FeeApply_SheetCode">
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField HeaderText="批复金额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.FNA.FNA_FeeApplyBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_FeeApply_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="可核销金额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_AvailCost" runat="server" Text='<%# MCSFramework.BLL.FNA.FNA_FeeApplyBLL.GetAvailCost(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_FeeApply_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("FNA_FeeApply_ApproveTask", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                            Text="审批记录" Visible='<%# Eval("FNA_FeeApply_ApproveTask").ToString()!="" %>'
                                                            ></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:TemplateField>
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

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 50;      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
