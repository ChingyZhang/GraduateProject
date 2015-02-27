<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="DistributorDetail.aspx.cs" Inherits="CM_Distributor_DistributorDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                经销商详细信息</h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Analysis" runat="server" Width="100px" Text="经销商分析数据" OnClick="bt_Analysis_Click"
                                Visible="false" />
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审核" Width="60px" />
                            <asp:Button ID="bt_AddApply" runat="server" Text="发起新增流程" Width="80px" OnClick="bt_AddApply_Click"
                                OnClientClick="return confirm(&quot;确定发起新增流程?&quot;)" />
                            <asp:Button ID="bt_DIUP" runat="server" OnClientClick="return confirm('您是否确认系统中有相应原分销商!')"
                                Text="原分销商升级" Width="80px" OnClick="bt_DIUP_Click" />
                            <asp:Button ID="bt_RevocationApply" runat="server" Text="发起撤销流程" Width="80px" OnClick="bt_RevocationApply_Click"
                                OnClientClick="return confirm(&quot;是否已处理账户上积分,确定发起撤销流程?&quot;)" />
                            <asp:Button ID="bt_Record" runat="server" Text="审批记录" Width="60px" OnClick="bt_Record_Click" />
                            <asp:Button ID="bt_ViewSubClient" runat="server" Text="查看下游客户" Width="80px" OnClick="bt_ViewSubClient_Click"
                                CausesValidation="False" />
                            <asp:Button ID="bt_ReplaceSupplier" runat="server" OnClick="bt_ReplaceSupplier_Click"
                                Text="替换产品供货商" CausesValidation="False" Width="100px" />
                            <asp:Button ID="bt_ReplaceSupplier2" runat="server" Text="替换赠品供货商" OnClick="bt_ReplaceSupplier2_Click"
                                CausesValidation="False" Width="100px" />
                            <asp:Button ID="bt_ReplaceClientManager" runat="server" OnClick="bt_ReplaceSupplier_Click"
                                Text="替换责任业代" Visible="False" Width="80px" />
                            <asp:Button ID="bt_DIACUpgrade" runat="server" OnClick="bt_DIACUpgrade_Click" OnClientClick="return confirm('您是否确认要将该经销商子户头升级为主户头,并转移产品与赠品对应的库存数据!')"
                                Text="升级为主户头" Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="Page_DI_DistributorDetail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tbl_LinkMan" runat="server" cellspacing="0" cellpadding="0" width="100%"
                    border="0">
                    <tr>
                        <td>
                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td nowrap>
                                        <h3>
                                            客户联系人列表
                                        </h3>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Add" runat="server" Text="新增联系人" Width="80px" OnClick="bt_Add_Click"
                                            UseSubmitBehavior="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="CM_LinkMan_ID" PanelCode="Panel_LM_List_001">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="CM_LinkMan_ID" DataNavigateUrlFormatString="../LM/LinkManDetail.aspx?ID={0}"
                                                DataTextField="CM_LinkMan_Name" HeaderText="联系人姓名" ControlStyle-CssClass="listViewTdLinkS1" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据</EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
