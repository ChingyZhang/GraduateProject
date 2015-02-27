<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_JXCSummary_ListSub, App_Web_yabmfp6z" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="160" align="left">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="客户进销存统计"></asp:Label>
                            </h2>
                        </td>
                        <td class="dataLabel" width="60px">
                            会计月
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_BeginMonth" DataValueField="ID" DataTextField="Name" runat="server">
                            </asp:DropDownList>
                            至<asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataField" align="right">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server"
                            id="tb_Head">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="160px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    客户
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx?NoParent=Y"
                                        Width="200px" />
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_IsOpponent" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_IsOpponent_SelectedIndexChanged">
                                        <asp:ListItem Text="成品" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="赠品" Value="9"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Search" runat="server" Text="查 看" Width="60px" OnClick="bt_Search_Click" />
                                    <asp:Button ID="bt_Edit" runat="server" Text="填 报" Width="60px" OnClick="bt_Edit_Click" />
                                    <asp:Button ID="bt_BathApprove" runat="server" Text="批量审核" Width="60px" OnClick="bt_BathApprove_Click"
                                        OnClientClick="return confirm('是否确认将选中的数据批量设为已审核通过？')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td nowrap style="width: 100px" colspan="1">
                                    <h3>
                                        进销存列表</h3>
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rbl_ApproveFlag" runat="server" RepeatColumns="4" RepeatLayout="Flow"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_ApproveFlag_SelectedIndexChanged"
                                        DataTextField="Value" DataValueField="Key">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                        OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                        <Items>
                                            <mcs:MCSTabItem ID="MCSTabItem1" Text="实际经销进价核算" runat="server" Value="1" />
                                            <mcs:MCSTabItem ID="MCSTabItem2" Text="按批发价金额统计" runat="server" Value="2" />
                                            <mcs:MCSTabItem ID="MCSTabItem3" Text="按零售价金额统计" runat="server" Value="3" Visible="false" />
                                            <mcs:MCSTabItem ID="MCSTabItem4" Text="公司标准厂价核算" runat="server" Value="4" Visible="false" />
                                        </Items>
                                    </mcs:MCSTabControl>
                                </td>
                            </tr>
                            <tr class="tabForm">
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Client,AccountMonth" PageSize="15" Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_ID" runat="server" Visible='<%#  Eval("ApproveFlag").ToString()=="未审核"%>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "JXCSummary_DetailSub.aspx?ClientID="+Eval("Client").ToString() +"&AccountMonth="+Eval("AccountMonth").ToString()+"&IsOpponent="+ rbl_IsOpponent.SelectedValue%>'
                                                        Text="详细"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OrganizeCityName" HeaderText="管理区域" SortExpression="OrganizeCityName" />
                                            <asp:BoundField DataField="AccountMonthName" HeaderText="月份" SortExpression="AccountMonthName" />
                                            <asp:BoundField DataField="ClientName" HeaderText="客户全称" SortExpression="ClientName" />
                                            <asp:BoundField DataField="ClientTypeName" HeaderText="客户类型" SortExpression="ClientTypeName"
                                                Visible="false" />
                                            <asp:BoundField DataField="BeginningInventory" HeaderText="期初库存" SortExpression="BeginningInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="PurchaseVolume" HeaderText="本期进货" SortExpression="PurchaseVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransitInventory" HeaderText="在途库存" SortExpression="TransitInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="RecallVolume" HeaderText="下游退货" SortExpression="RecallVolume"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="Green" HeaderStyle-ForeColor="Green">
                                                <HeaderStyle ForeColor="Green" />
                                                <ItemStyle ForeColor="Green" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SalesVolume" HeaderText="本期销售" SortExpression="SalesVolume"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="Blue" HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle ForeColor="Blue" />
                                                <ItemStyle ForeColor="Blue" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GiftVolume" HeaderText="买赠" SortExpression="GiftVolume"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="Red" HeaderStyle-ForeColor="Red" Visible="false">
                                                <HeaderStyle ForeColor="Red" />
                                                <ItemStyle ForeColor="Red" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ReturnedVolume" HeaderText="退回公司" SortExpression="ReturnedVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="StaleInventory" HeaderText="盘存盈亏" SortExpression="StaleInventory"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransferInVolume" HeaderText="调入" SortExpression="TransferInVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="TransferOutVolume" HeaderText="调出" SortExpression="TransferOutVolume"
                                                DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="EndingInventory" HeaderText="期末盘存" SortExpression="EndingInventory" Visible="false"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="#993300" HeaderStyle-ForeColor="#993300">
                                                <HeaderStyle ForeColor="#993300" />
                                                <ItemStyle ForeColor="#993300" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ComputInventory" HeaderText="实物库存" SortExpression="ComputInventory"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="#993300" HeaderStyle-ForeColor="#993300">
                                                <HeaderStyle ForeColor="#993300" />
                                                <ItemStyle ForeColor="#993300" />
                                            </asp:BoundField>


                                            <asp:BoundField DataField="PaperInventory" HeaderText="账面库存合计（实物+在途）" SortExpression="PaperInventory"
                                                DataFormatString="{0:0.##}" ItemStyle-ForeColor="#993300" HeaderStyle-ForeColor="#993300">
                                                <HeaderStyle ForeColor="#993300" />
                                                <ItemStyle ForeColor="#993300" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="下游退货" HeaderStyle-ForeColor="Green" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubReturnedVolume" ForeColor="Green" runat="server" Text='<%# GetSubJXC((int)Eval("AccountMonth"),(int)Eval("Client"),"ReturnedVolume").ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle ForeColor="Green" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="下游进货" HeaderStyle-ForeColor="Blue" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubPurchaseVolume" ForeColor="Blue" runat="server" Text='<%# GetSubJXC((int)Eval("AccountMonth"),(int)Eval("Client"),"PurchaseVolume").ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle ForeColor="Blue" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="下游买赠" HeaderStyle-ForeColor="Red" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubGiftVolume" ForeColor="Red" runat="server" Text='<%# GetSubJXC((int)Eval("AccountMonth"),(int)Eval("Client"),"GiftVolume").ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle ForeColor="Red" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApproveFlag" HeaderText="审核标志" SortExpression="ApproveFlag" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="bt_BathApprove" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_ApproveFlag" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_IsOpponent" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>


