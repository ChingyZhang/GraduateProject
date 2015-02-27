<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="JXCSummary_DetailSub.aspx.cs" Inherits="SubModule_SVM_JXCSummary_DetailSub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="260" align="left">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="客户进销存明细统计"></asp:Label>
                            </h2>
                        </td>
                        <td align="right" width="100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTab_DisplayMode" runat="server" Width="100%" SelectedIndex="0"
                    OnOnTabClicked="MCSTab_DisplayMode_OnTabClicked">
                    <Items>
                        <mcs:MCSTabItem Text="当前客户进销存" Value="0" />
                        <mcs:MCSTabItem Text="下游客户进销存" Value="1" />
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
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
                                        ParentColumnName="SuperID" Width="200px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    客户
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx?NoParent=Y"
                                        Width="250px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" DataValueField="ID" DataTextField="Name" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_Month_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_IsOpponent" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_IsOpponent_SelectedIndexChanged">
                                        <asp:ListItem Text="成品" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="赠品" Value="9"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataField" align="right">
                                    <asp:Button ID="bt_Search" runat="server" Text="查 看" Width="60px" OnClick="bt_Search_Click" />
                                    <asp:Button ID="bt_Edit" runat="server" Text="修 改" Width="60px" OnClick="bt_Edit_Click"
                                        Enabled="false" />
                                    <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" OnClientClick="return confirm('是否将进销存数据确认设为审核通过? 通过后进销存数据将不可再更改,且该过程不可撤消!')"
                                        Text="审核通过" Width="80px" Enabled="False" />
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
                                        进销存明细列表</h3>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="dataLabel">
                                                品牌
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                    OnSelectedIndexChanged="rbl_Brand_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                系列
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID"
                                                    RepeatDirection="Horizontal">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                数量单位：听\袋\盒，金额单位：元RMB
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rbl_IsOpponent" EventName="SelectedIndexChanged" />
                    </Triggers>
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
                                    <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                        <Items>
                                            <mcs:MCSTabItem ID="MCSTabItem0" Text="按数量统计" runat="server" Value="0" />
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
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Product,FactoryPrice,SalesPrice,RetailPrice"
                                        PageSize="15" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="品牌" SortExpression="BrandName" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# GetPDTBrandName((int)Eval("Product")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="系列" SortExpression="ClassifyName" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# GetPDTClassifyName((int)Eval("Product")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductName" HeaderText="产品简称" SortExpression="ProductName" />
                                            <asp:TemplateField HeaderText="期初库存" SortExpression="BeginningInventory">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#GetJXCData((int)Eval("Product"),(int)Eval("BeginningInventory"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="本期进货" SortExpression="PurchaseVolume">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("PurchaseVolume"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="在途库存" SortExpression="TransitInventory">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("TransitInventory"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="下游退货" SortExpression="RecallVolume" ItemStyle-ForeColor="Green"
                                                HeaderStyle-ForeColor="Green">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("RecallVolume"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="本期销售" SortExpression="SalesVolume" ItemStyle-ForeColor="Blue"
                                                HeaderStyle-ForeColor="Blue">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("SalesVolume"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="买赠" SortExpression="GiftVolume" ItemStyle-ForeColor="Red"
                                                HeaderStyle-ForeColor="Red">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("GiftVolume"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="退货" SortExpression="ReturnedVolume">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("ReturnedVolume"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="盘存盈亏" SortExpression="StaleInventory">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label12" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("StaleInventory"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="调入" SortExpression="TransferInVolume">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("TransferInVolume"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="调出" SortExpression="TransferOutVolume">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label14" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("TransferOutVolume"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="期末盘存" SortExpression="EndingInventory" ItemStyle-ForeColor="#993300"
                                                HeaderStyle-ForeColor="#993300" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("EndingInventory"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="实物库存" SortExpression="ComputInventory" ItemStyle-ForeColor="#993300"
                                                HeaderStyle-ForeColor="#993300">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("ComputInventory"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="账面库存" SortExpression="PaperInventory" ItemStyle-ForeColor="#993300"
                                                HeaderStyle-ForeColor="#993300">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# GetJXCData((int)Eval("Product"),(int)Eval("ComputInventory")+(int)Eval("TransitInventory"),(decimal)Eval("FactoryPrice"),(decimal)Eval("SalesPrice"),(decimal)Eval("RetailPrice")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="下游退货" HeaderStyle-ForeColor="Green" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubReturnedVolume" ForeColor="Green" runat="server" Text='<%# GetSubJXC("ReturnedVolume",(int)Eval("Product")).ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="下游进货" HeaderStyle-ForeColor="Blue" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubPurchaseVolume" ForeColor="Blue" runat="server" Text='<%# GetSubJXC("PurchaseVolume",(int)Eval("Product")).ToString("0.##")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="下游买赠" HeaderStyle-ForeColor="Red" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubGiftVolume" ForeColor="Red" runat="server" Text='<%# GetSubJXC("GiftVolume",(int)Eval("Product")).ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        <asp:AsyncPostBackTrigger ControlID="ddl_Month" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_IsOpponent" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
