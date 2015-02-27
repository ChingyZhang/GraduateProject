<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="JXCSummary_Edit.aspx.cs" Inherits="SubModule_SVM_JXCSummary_Edit" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="客户进销存明细填报"></asp:Label>
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
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
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
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx"
                                        Width="250px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" DataValueField="ID" DataTextField="Name" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataField" align="right">
                                    <asp:Button ID="bt_Load" runat="server" Text="填 报" Width="60px" OnClick="bt_Load_Click" />
                                    <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" OnClick="bt_Save_Click"
                                        Enabled="false" />
                                    <asp:Button ID="bt_Delete" runat="server" Enabled="False" OnClick="bt_Delete_Click"
                                        Text="删除" Width="60px" OnClientClick="return confirm(&quot;是否确认删除该客户指定月份进销存数据?&quot;)"
                                        Visible="false" />
                                    <asp:Button ID="bt_Return" runat="server" Text="返 回" Width="60px" OnClick="bt_Return_Click"
                                        Enabled="false" OnClientClick="return confirm('您确定要返回么？返回之前请确定“保存”了已填报的数据!')" />
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
                                    数量单位：听\袋\盒
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
                        <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,Product"
                            Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="品牌" SortExpression="BrandName" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# GetPDTBrandName((int)Eval("Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="系列" SortExpression="ClassifyName" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# GetPDTClassifyName((int)Eval("Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProductName" HeaderText="产品简称" SortExpression="ProductName" />
                                <asp:TemplateField HeaderText="期初库存" SortExpression="BeginningInventory">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("BeginningInventory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本期进货" SortExpression="PurchaseVolume">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_PurchaseVolume" runat="server" Text='<%# Bind("PurchaseVolume")%>'
                                            Width="40px" Enabled="<%#bEditPurchaseVolume %>"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="在途库存" SortExpression="TransitInventory">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("TransitInventory")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="下游退货" SortExpression="RecallVolume">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_RecallVolume" runat="server" Text='<%# Bind("RecallVolume")%>'
                                            Width="40px" Enabled="<%#bEditRecallVolume %>"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="本期销售" SortExpression="SalesVolume">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_SalesVolume" runat="server" Text='<%# Bind("SalesVolume")%>'
                                            Width="40px" Enabled="<%#bEditSalesVolume %>"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="买赠" SortExpression="GiftVolume">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_GiftVolume" runat="server" Text='<%# Bind("GiftVolume")%>' Width="40px"
                                            Enabled="<%#bEditGiftVolume %>"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="退货" SortExpression="ReturnedVolume">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_ReturnedVolume" runat="server" Text='<%# Bind("ReturnedVolume")%>'
                                            Width="40px" Enabled="<%#bEditReturnedVolume %>"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="盘存盈亏" SortExpression="StaleInventory">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_StaleInventory" runat="server" Text='<%# Bind("StaleInventory")%>'
                                            Width="40px" Enabled="<%#bEditStaleInventory %>"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调入" SortExpression="TransferInVolume">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("TransferInVolume")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调出" SortExpression="TransferOutVolume">
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("TransferOutVolume")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="期末盘存" SortExpression="EndingInventory">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_EndingInventory" runat="server" Text='<%# Bind("EndingInventory")%>'
                                            Width="40px" Enabled="<%#bEditEndingInventory %>"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="实物库存" SortExpression="ComputInventory">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("ComputInventory")%>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                无数据
                            </EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Load" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="bt_Save" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
