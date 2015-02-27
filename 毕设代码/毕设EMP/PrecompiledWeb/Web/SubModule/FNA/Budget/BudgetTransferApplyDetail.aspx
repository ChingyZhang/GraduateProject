<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_Budget_BudgetTransferApplyDetail, App_Web_gigc93-l" enableEventValidation="false" stylesheettheme="basic" %>

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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="">预算调拨申请详细信息</asp:Label></h2>
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" id="tbl_BudgetInfo" runat="server"
                                        visible="false">
                                        <tr>
                                            <td class="dataLabel">
                                                源预算余额
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_FromBalance" runat="server" Text="" ForeColor="Red"></asp:Label>元
                                            </td>
                                            <td class="dataLabel">
                                                目的预算余额
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_ToBalance" runat="server" Text="" ForeColor="Red"></asp:Label>元
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    批复调拨额度:<asp:Label ID="lb_ApproveAmount" runat="server" Text="" ForeColor="Red" Font-Bold="true"
                                        Font-Size="X-Large"></asp:Label>元
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_SaveAdjust" runat="server" OnClick="bt_SaveAdjust_Click" Text="保存扣减"
                                        Visible="False" Width="60px" />
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                                    <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="提 交" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_BudgetTransferApply_Detail">
                        </mcs:UC_DetailView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
