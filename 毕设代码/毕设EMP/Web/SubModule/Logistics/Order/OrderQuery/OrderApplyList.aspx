<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="OrderApplyList.aspx.cs" Inherits="SubModule_Logistics_Order_OrderQuery_OrderApplyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                订单出货查询
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
                                                <asp:Button ID="btn_OK" runat="server" Text="查找" Width="60px" 
                                                    onclick="btn_OK_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tabForm">
                                    <table cellpadding="0" cellspacing="0" border="0" width="80%">
                                        <tr>
                                            <td class="dataLabel">
                                                管理片区
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" Width="220px" />
                                            </td>
                                           <td class="dataLabel">
                                                发货日期
                                            </td>
                                            <td class="dataField" width="220px">
                                                <asp:TextBox ID="tbx_BeginDate" runat="server" Width="80px" onfocus="setday(this)"></asp:TextBox>
                                                到<asp:TextBox ID="tbx_EndDate" runat="server" Width="80px" onfocus="setday(this)"></asp:TextBox>
                                            </td>
                                            <td class="dataLabel">
                                                经销商
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                                    Width="280px" />
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="dataLabel">
                                                发货单号
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbx_OrderCode" runat="server" Width="160px"></asp:TextBox>
                                            </td>
                                            <td></td><td></td>
                                            <td>
                                            </td>
                                            <td>
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
                                   
                                    <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="No"
                                        AllowPaging="True" PageSize="30" 
                                        onpageindexchanging="gv_ListDetail_PageIndexChanging" >
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="序号" />
                                            <asp:BoundField DataField="fdate" HeaderText="发货日期"  DataFormatString="{0:yyyy-MM-dd}" />
                                            <asp:BoundField DataField="fbillno" HeaderText="发货单号" Visible="false" />
                                            <asp:HyperLinkField DataTextField="fbillno" HeaderText="发货单号" Target="_blank" DataNavigateUrlFields="fbillno" DataNavigateUrlFormatString="~/SubModule/Logistics/Order/OrderQuery/OrderApplyDetail.aspx?SheetCode={0}" ControlStyle-CssClass="listViewTdLinkS1" />
                                            <asp:BoundField DataField="diname" HeaderText="经销商名称" />
                                            <asp:BoundField DataField="fnumber" HeaderText="经销商代码" />
                                            <asp:BoundField DataField="fdq" HeaderText="大区" />
                                            <asp:BoundField DataField="fyyb" HeaderText="营业部" />
                                            <asp:BoundField DataField="fbsc" HeaderText="办事处"/>
                                            <asp:BoundField DataField="ffetchadd" HeaderText="收货地址"  />
                                            <asp:BoundField DataField="carrydi" HeaderText="货运商" />
                                            <asp:BoundField DataField="fqty" HeaderText="产品总件数" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="fconsignamount" HeaderText="总金额/元" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="fheadselfB0167" HeaderText="备注"  />
                                        </Columns>
                                        <HeaderStyle Wrap="false" />
                                        <RowStyle  Wrap="false" />
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
    </asp:Timer>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

