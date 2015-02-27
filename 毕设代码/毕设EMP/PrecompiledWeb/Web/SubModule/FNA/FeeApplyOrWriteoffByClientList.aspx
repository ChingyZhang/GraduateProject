<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_FeeApplyOrWriteoffByClientList, App_Web_wwzcbdkp" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        费用申请及核销列表查询
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
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
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
                                    客户
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx"
                                        Width="250px" />
                                    <asp:HyperLink ID="lb_ClientInfo" runat="server" NavigateUrl="~/SubModule/CM/RT/RetailerDetail.aspx"
                                        CssClass="listViewTdLinkS1">查看客户资料</asp:HyperLink>
                                </td>
                                <td>
                                    费用类型
                                    <asp:DropDownList ID="ddl_FeeType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <%--<td>
                                   会计科目
                                    <asp:DropDownList ID="ddl_AccountTitle" runat="server"  DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>--%>
                                <td class="dataLabel">
                                    费用申请开始月
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
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
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
                                        费用申请核销列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gv_List" runat="server" AutoGenerateColumns="False" Width="100%"
                            GridLines="Both">
                            <Columns>
                                <asp:HyperLinkField HeaderText="申请单号" DataNavigateUrlFields="ApplyID" DataNavigateUrlFormatString="FeeApply/FeeApplyDetail3.aspx?ID={0}"
                                    DataTextField="ApplySheetCode" ControlStyle-CssClass="listViewTdLinkS1" />
                                <asp:BoundField DataField="ApplyStateName" HeaderText="申请状态" SortExpression="ApplyStateName" />
                                <asp:BoundField DataField="ApplyMonth" HeaderText="申请月份" SortExpression="ApplyMonth" />
                                <asp:BoundField DataField="FeeType" HeaderText="费用类型" SortExpression="AccountTitleName" />
                                <asp:BoundField DataField="AccountTitleName" HeaderText="会计科目" SortExpression="AccountTitleName" />
                                <asp:BoundField DataField="BeginDate" HeaderText="开始日期" SortExpression="BeginDate"
                                    HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="EndDate" HeaderText="截止日期" SortExpression="EndDate" HtmlEncode="false"
                                    DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="SumApplyCost" HeaderText="申请批复额" SortExpression="sumApplyCost"
                                    DataFormatString="{0:f2}" />
                                <asp:BoundField DataField="AvailCost" HeaderText="可核销结余额" SortExpression="AvailCost"
                                    DataFormatString="{0:f2}" />
                                <asp:HyperLinkField HeaderText="核销单号" DataNavigateUrlFields="WriteOffID" DataNavigateUrlFormatString="FeeWriteOff/FeeWriteOffDetail.aspx?ID={0}"
                                    DataTextField="WriteOffSheetCode" ControlStyle-CssClass="listViewTdLinkS1" />
                                <asp:BoundField DataField="FeeApplyDetailFlag" HeaderText="报销标志" SortExpression="FeeApplyDetailFlag" />
                                <asp:BoundField DataField="WriteOffStateName" HeaderText="核销状态" SortExpression="WriteOffStateName" />
                                <asp:BoundField DataField="SumWriteOffCost" HeaderText="已核销额" SortExpression="SumWriteOffCost"
                                    DataFormatString="{0:f2}" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
