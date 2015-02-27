<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FeeWriteOffList.aspx.cs" Inherits="SubModule_FNA_FeeWriteoff_FeeWriteOffList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="费用核销列表查询"></asp:Label>
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" Text="新增费用核销" OnClick="bt_Add_Click" Width="100px" />
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
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="160px" />
                                </td>
                                <td class="dataLabel">
                                    核销月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                    至
                                    <asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddl_FeeType" runat="server" DataTextField="Value" DataValueField="Key"
                                        Visible="false">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    费用代垫客户
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" Width="180px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2" />
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    核销单号
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_SheetCode" runat="server" Width="120px"></asp:TextBox>
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
                                        费用核销列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            PanelCode="Panel_FNA_FeeWriteOffList" DataKeyNames="FNA_FeeWriteOff_ID" AllowPaging="True"
                            PageSize="15" AllowSorting="true">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="FNA_FeeWriteOff_ID" DataNavigateUrlFormatString="FeeWriteOffDetail.aspx?ID={0}"
                                    DataTextField="FNA_FeeWriteOff_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                                    HeaderText="核销单号"  SortExpression="FNA_FeeWriteOff_SheetCode">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="总金额">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.FNA.FNA_FeeWriteOffBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_FeeWriteOff_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("FNA_FeeWriteOff_ApproveTask", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                            Text="审批记录" Visible='<%# Eval("FNA_FeeWriteOff_ApproveTask").ToString()!="" %>'
                                            ></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
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
