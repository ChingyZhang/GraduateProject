<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="KPI_SchemeDetail.aspx.cs" Inherits="SubModule_OA_KPI_KPI_SchemeDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upl_KPI_Item" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <h2>
                                        员工职位和其关联的考核项目维护</h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" OnClick="bt_Save_Click" />
                                    <asp:Button ID="bt_del" runat="server" Text="删 除" Width="60px" OnClick="bt_del_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" Text="审 核" Width="60px" OnClick="bt_Approve_Click" />
                                    <asp:Button ID="bt_CancelApprove" runat="server" Text="取消审核" Width="80px" OnClick="bt_CancelApprove_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <mcs:UC_DetailView ID="DV_KPIScheme" runat="server" DetailViewCode="DV_KPI_Scheme">
                        </mcs:UC_DetailView>
                        <mcs:UC_DetailView ID="DV_KPISchemeDetail" runat="server" DetailViewCode="DV_KPI_SchemeDetail"
                            Visible="false">
                        </mcs:UC_DetailView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td nowrap>
                                    <h3>
                                        员工考核指标
                                    </h3>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" UseSubmitBehavior="False"
                                        OnClick="bt_Add_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td colspan="4">
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="ID" AllowPaging="true" PageSize="15" OnRowDeleting="gv_List_RowDeleting"
                                        OnSelectedIndexChanging="gv_List_SelectedIndexChanging" onpageindexchanging="gv_List_PageIndexChanging">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1" />
                                            <asp:BoundField DataField="Name" HeaderText="指标名称" />
                                            <asp:BoundField DataField="FullScore" HeaderText="基准分" />
                                            <asp:BoundField DataField="CheckPosition" HeaderText="考核职位" />
                                            <asp:BoundField DataField="ApprovePosition" HeaderText="审核职位" />
                                            <asp:BoundField DataField="AllowSelfcheck" HeaderText="是否允许自评" />
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                        Text="删除" OnClientClick="javascript:return confirm('确认是否删除?');" CssClass="listViewTdLinkS1"></asp:LinkButton>
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
