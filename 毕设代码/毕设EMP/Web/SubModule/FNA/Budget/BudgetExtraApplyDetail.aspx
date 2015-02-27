<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="BudgetExtraApplyDetail.aspx.cs" Inherits="SubModule_FNA_Budget_BudgetExtraApplyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true"
        RenderMode="Inline">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="">费用预算扩增申请详细信息</asp:Label></h2>
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" id="tbl_BudgetInfo" runat="server"
                                        visible="false">
                                        <tr>
                                            <td class="dataLabel">
                                                预算余额
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_Balance" runat="server" Text="" ForeColor="Red"></asp:Label>元
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class="dataLabel" width="100px">
                                                部级预算余额
                                            </td>
                                            <td class="dataField" width="100px">
                                                <asp:Label ID="lb_DepartmentBalance" runat="server" Text="" ForeColor="Red"></asp:Label>元
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="dataLabel" width="120px">
                                    本申请单批复扩增金额
                                </td>
                                <td class="dataField" width="100px">
                                    <asp:Label ID="lb_ApproveAmount" runat="server" Text="" ForeColor="Red" Font-Bold="true"
                                        Font-Size="X-Large"></asp:Label>元
                                </td>
                                <%--  <td class="dataLabel" width="200">
                                    当月该费用类型累计已批复扩增金额
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lb_SumExtraInfo" runat="server" Text="" Font-Size="Large"></asp:Label>
                                </td>--%>
                                <td align="right">
                                    <asp:Button ID="bt_SaveAdjust" runat="server" OnClick="bt_SaveAdjust_Click" Text="保存扣减"
                                        Visible="False" Width="60px" />
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                                    <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="提 交" Width="60px" />
                                </td>
                            </tr>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                <tr>
                                    <td class="dataLabel">
                                        <h2>
                                            <asp:Label ID="Label1" runat="server" Text=""> 当月该费用类型累计已批复扩增金额</asp:Label></h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dataField" width="50%">
                                        <asp:Label ID="lb_SumExtraInfo1" runat="server" Text="作业区："></asp:Label>
                                    </td>
                                    <td class="dataField" width="50%">
                                        <asp:Label ID="lb_SumExtraInfo2" runat="server" Text="总部："></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_BudgetExtraApply_Detail">
                        </mcs:UC_DetailView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
