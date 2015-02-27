<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="OrderAlibraryList.aspx.cs" Inherits="SubModule_Logistics_Order_OrderAlibraryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="经销商出库反馈记录"></asp:Label>
                                    </h2>
                                </td>
                                <td align="right">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td style="height: 28px">
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
                                   开始日期
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="txtsDate" Width="140px" runat="server" onfocus="setday(this)"></asp:TextBox><span
                                        style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                            runat="server" ControlToValidate="txtsDate" Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator><span
                                                style="color: #ff0000"></span><span style="color: #ff0000"></span><asp:CompareValidator
                                                    ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="txtsDate"></asp:CompareValidator><span
                                                        style="color: #ff0000"></span>
                                </td>
                                <td class="dataLabel">
                                   截止日期
                                </td>
                                <td class="dataField">
                                       <asp:TextBox ID="txteDate" Width="140px" runat="server" onfocus="setday(this)"></asp:TextBox><span
                                        style="color: #ff0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="txteDate" Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator><span
                                                style="color: #ff0000"></span><span style="color: #ff0000"></span><asp:CompareValidator
                                                    ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="txteDate"></asp:CompareValidator><span
                                                        style="color: #ff0000"></span>
                                </td>
                                <td class="dataLabel">
                                    归属经销商
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" Width="200px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2" />
                                </td>             
                                 <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td style="height: 28px">
                                    <h3>
                                       经销商出库反馈记录列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                     <mcs:UC_GridView ID="gv_OrderList" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="OrderNo"
                            AllowPaging="true" PageSize="15" AllowSorting="true" 
                            onpageindexchanging="gv_OrderList_PageIndexChanging">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="OrderNo" DataNavigateUrlFormatString="OrderAlibraryDetail.aspx?OrderNo={0}"
                                    DataTextField="OrderNo" ControlStyle-CssClass="listViewTdLinkS1"
                                    HeaderText="发货单号">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="下级客户全称" HeaderText="收货客户" SortExpression="下级客户全称" />
                                <asp:BoundField DataField="经销商名称" HeaderText="供货经销商" SortExpression="经销商名称" />
                                <asp:BoundField DataField="ADate" HeaderText="发货日期" SortExpression="ADate" />
                                <asp:BoundField DataField="CountMun" HeaderText="实发数量" SortExpression="CountMun" />
                                <asp:BoundField DataField="SumPrice" HeaderText="发货金额(厂价)" SortExpression="SumPrice" />
                            </Columns>
                            <EmptyDataTemplate>
                                无数据
                            </EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

