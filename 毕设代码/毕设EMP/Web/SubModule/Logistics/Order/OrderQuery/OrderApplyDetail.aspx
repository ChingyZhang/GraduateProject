<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="OrderApplyDetail.aspx.cs" Inherits="SubModule_Logistics_Order_OrderQuery_OrderApplyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%">
    <tr>
        <td>
        <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                订单出货明细</h2>
                        </td>
                        
                    </tr>
         </table>
         <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr height="28px">
                    <td nowrap>
                        <h3>
                            发货单信息</h3>
                    </td>
                    
                </tr>
            </table>
             <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tabDetailView">
                <tr>
                    <td class="tabDetailViewDL">
                        发货日期
                    </td>
                    <td class="tabDetailViewDF">
                        <asp:Label ID="lb_Fdate" runat="server" Text="" ></asp:Label>
                    </td>
                    <td class="tabDetailViewDL">
                        客户编码
                    </td>
                    <td class="tabDetailViewDF">
                        <asp:Label ID="lb_DICode" runat="server" Text="" ></asp:Label>
                    </td>
                    <td class="tabDetailViewDL">
                        收货地址
                    </td>
                    <td class="tabDetailViewDF">
                        <asp:Label ID="lb_Addr" runat="server" Text="" ></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="tabDetailViewDL">
                        发货单号
                    </td>
                    <td class="tabDetailViewDF">
                        <asp:Label ID="lb_SheetCode" runat="server" Text=""></asp:Label>
                        
                    </td>
                    <td class="tabDetailViewDL">
                        客户名称
                    </td>
                    <td class="tabDetailViewDF">
                        <asp:Label ID="lb_DIName" runat="server" Text="" ></asp:Label>
                    </td>
                    <td class="tabDetailViewDL">
                        货运商
                    </td>
                    <td class="tabDetailViewDF">
                        <asp:Label ID="lb_CarryDI" runat="server" Text="" ></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td class="tabDetailViewDL">
                        备注
                    </td>
                    <td class="tabDetailViewDF" colspan="5">
                        <asp:Label ID="lb_Remark" runat="server" Text="" ></asp:Label>
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
                            发货条目明细</h3>
                    </td>
                    
                </tr>
            </table>
        </td>    
    </tr>
    <tr>
        <td>
        <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="No"
            AllowPaging="False"  >
            <Columns>
                <asp:BoundField DataField="No" HeaderText="序号" />
                <asp:BoundField DataField="物料编码" HeaderText="产品编码" />
                <asp:BoundField DataField="物料名称" HeaderText="产品名称" />
                
                <asp:BoundField DataField="规格型号" HeaderText="规格型号" />
                <asp:BoundField DataField="出库数量" HeaderText="数量" DataFormatString="{0:0.##}"  />
                <asp:BoundField DataField="销售金额" HeaderText="金额" DataFormatString="{0:0.##}"  />
                <asp:BoundField DataField="订单号" HeaderText="订单号" />
                <asp:BoundField DataField="订单说明" HeaderText="订单说明"/>
                <asp:BoundField DataField="备注" HeaderText="备注"  />
                
            </Columns>
            <HeaderStyle Wrap="false" />
            <RowStyle  Wrap="false" />
        </mcs:UC_GridView>
        </td>
    </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

