<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderGiftApplyAmount.aspx.cs" Inherits="SubModule_Logistics_Order_OrderGiftApplyAmount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                调整赠品申请额度
                            </h2>
                        </td>
                        <td align="right">
                         <asp:Button ID="bt_downtemple" runat="server"  
                                Text="下载导入模版" Width="120px" onclick="bt_downtemple_Click" /> 
                          <asp:Button ID="bt_ImportTools" runat="server" onclick="bt_ImportTools_Click" 
                                Text="导入余额及抵扣金额" Width="120px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
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
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td class="dataLabel">
                                                管理片区
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" Width="180px" />
                                            </td>
                                            <td class="dataLabel">
                                                申请月份
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                经销商
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                                    Width="280px" />
                                            </td>
                                            <td class="dataLabel">
                                                品牌
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                赠品费用类别
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_GiftClassify" runat="server" DataTextField="Value" DataValueField="Key">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="tabForm">
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                        PanelCode="Panel_ORD_GiftApplyAmount_List" DataKeyNames="ORD_GiftApplyAmount_ID"                                                                             
                                        AllowPaging="True" PageSize="20" OnRowDataBound="gv_List_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="赠品费率%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_FeeRate" runat="server" Width="40px" Text='<%# Bind("ORD_GiftApplyAmount_FeeRate","{0:0.###}")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="本月生成<br/>请购额度">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_AvailableAmount" runat="server" Width="40px" Text='<%#Bind("ORD_GiftApplyAmount_AvailableAmount","{0:0.###}")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="已请购<br/>赠品额度">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_AppliedAmount" runat="server" Width="40px" Text='<%#Bind("ORD_GiftApplyAmount_AppliedAmount","{0:0.###}")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="还可请<br/>购赠品额度">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_BalanceAmount" runat="server" Width="40px" Text='<%#Bind("ORD_GiftApplyAmount_BalanceAmount","{0:0.###}")%>' ToolTip="本月生成请购额度+上月余额-赠品抵扣额-已请购赠品额度"> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button runat="server" ID="bt_Adjust" Text="调整" OnClick="bt_Adjust_Click" Visible='<%#bAdujst %>' />                                                  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button runat="server" ID="bt_AdjustHistory" Text="查看调整记录" />                                                  
                                                </ItemTemplate>
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
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
