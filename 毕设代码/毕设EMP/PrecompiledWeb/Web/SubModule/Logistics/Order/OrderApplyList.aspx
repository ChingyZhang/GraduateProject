<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_ORD_OrderApplyList, App_Web_aozhikkk" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upl_Order" runat="server">
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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="订单申请列表查询"></asp:Label>
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" Text="新增申请" OnClick="bt_Add_Click" Width="80px" />
                                    <asp:Button ID="bt_SpecialAdd" runat="server" Text="新增申请" OnClick="bt_Add_Click" Width="80px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0" id="tbl_Condition_title"
                            runat="server">
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
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tbl_Condition"
                            runat="server">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" />
                                </td>
                                <td class="dataLabel">
                                    结算月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    订单类型
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Type" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    产品类型
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ProductType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    品牌
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    申请单号
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_SheetCode" runat="server" Width="140px"></asp:TextBox>
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
                                        申请列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            PanelCode="Panel_LGS_OrderApplyList_001" DataKeyNames="ORD_OrderApply_ID" AllowPaging="True"
                            PageSize="25">
                            <Columns>
                                <asp:TemplateField HeaderText="申请单号">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# GetUrl((int)DataBinder.Eval(Container,"DataItem.ORD_OrderApply_ID")) %>'
                                             Text='<%# Eval("ORD_OrderApply_SheetCode") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="总金额">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.Logistics.ORD_OrderApplyBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.ORD_OrderApply_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ORD_OrderApply_TaskID", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                            Text="审批记录" Visible='<%# Eval("ORD_OrderApply_TaskID").ToString()!="" %>' ></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <%--<asp:HyperLinkField DataNavigateUrlFields="ORD_OrderApply_ID" DataNavigateUrlFormatString="OrderDeliveryList.aspx?ApplyID={0}"
                            Text="发放记录" ControlStyle-CssClass="listViewTdLinkS1"  Visible="false">
                            <ControlStyle CssClass="listViewTdLinkS1" />
                        </asp:HyperLinkField>--%>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
