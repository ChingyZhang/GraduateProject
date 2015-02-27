<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_Order_OrderQuery_OrderApplyPayTrack, App_Web_x_jdb-pz" enableEventValidation="false" stylesheettheme="basic" %>

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
                                月度订单到款追踪表
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
                                                    Width="280px"  />
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="dataLabel">
                                                产品系列
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Classify" runat="server">
                                                    <asp:ListItem Text="A.全品相" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="B.奶粉类" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="C.冲调类" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="D.安贝慧（含金装）" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="E.超级A金装" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="F.A金装" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="G.雅士利超级金装" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="H.雅士利金装" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="I.能慧金装" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="J.新配方" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="K.优怡" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="L.米粉" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="M.麦片" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="N.豆奶粉" Value="13"></asp:ListItem>
                                                </asp:DropDownList>
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
                                   
                                    <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                        GridLines="Horizontal" DataKeyNames="khid"
                                        AllowPaging="True" PageSize="30" 
                                        onpageindexchanging="gv_ListDetail_PageIndexChanging" >
                                        <Columns>
                                            <asp:BoundField DataField="dq1" HeaderText="大区1" />
                                             <asp:BoundField DataField="dq2" HeaderText="大区2" />
                                            <asp:BoundField DataField="yyb" HeaderText="营业部" />
                                            <asp:BoundField DataField="bsc" HeaderText="办事处" />
                                            <asp:BoundField DataField="khmc" HeaderText="客户名称" />
                                            <asp:BoundField DataField="khdm" HeaderText="客户代码" />
                                            <asp:BoundField DataField="yxxmb1" HeaderText="月销售目标1" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="yxxmb2" HeaderText="月销售目标2" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="yhkmb" HeaderText="月回款目标" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="byljmb" HeaderText="本月累计目标" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="qcwfdd" HeaderText="期初未发订单" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="ljyk" HeaderText="累计余款" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="byxx" HeaderText="备用信限" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="tsxx" HeaderText="特殊信限" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="bydk" HeaderText="本月到款" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="ychje" HeaderText="已出货金额" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="dzje" HeaderText="待装金额" DataFormatString="{0:0.##}" />
                                           <%-- <asp:BoundField DataField="调整数量" HeaderText="已释放未安排金额" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="调整数量" HeaderText="未释放金额" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="调整数量" HeaderText="砍单金额" DataFormatString="{0:0.##}" />--%>
                                            <asp:BoundField DataField="zhl" HeaderText="装货率" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="zdd" HeaderText="总订单" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="dddcl1" HeaderText="订单达成率1" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="dddcl2" HeaderText="订单达成率2" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="ddqk1" HeaderText="订单缺口1" DataFormatString="{0:0.##}"/>
                                            <asp:BoundField DataField="ddqk2" HeaderText="订单缺口2" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="qke1" HeaderText="欠款额1" DataFormatString="{0:0.##}" />
                                            <asp:BoundField DataField="qke2" HeaderText="欠款额2" DataFormatString="{0:0.##}" />
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
    <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick" Enabled="false">
    </asp:Timer>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

