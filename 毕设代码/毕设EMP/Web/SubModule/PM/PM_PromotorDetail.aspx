<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="PM_PromotorDetail.aspx.cs" Inherits="PM_PM_PromotorDetail" %>

<%@ Register Src="~/Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 140px">
                            <h2>
                                导购员详细信息
                            </h2>
                        </td>
                        <td>
                            <asp:Label ID="lb_OverBudgetInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Del" runat="server" Width="60px" Text="删 除" OnClick="bt_Del_Click"
                                Visible="false" OnClientClick="return confirm(&quot;删除后数据无法恢复，是否确认删除该导购员?&quot;)" />
                            <asp:Button ID="bt_Approve" runat="server" Width="60px" Text="审 核" OnClick="bt_Approve_Click"
                                OnClientClick="return confirm(&quot;确定将该员工审核通过?&quot;)" Visible="false" />
                            <asp:Button ID="bt_AddApply" runat="server" Text="发起入职审批" Width="80px" OnClick="bt_AddApply_Click"
                                OnClientClick="return confirm(&quot;确定发起新增流程?&quot;)" />
                            <asp:Button ID="bt_TaskDetail" runat="server" Text="审批记录" Width="60px" Visible="false"
                                OnClick="bt_TaskDetail_Click" />
                            <asp:Button ID="bt_RevocationApply" runat="server" Text="发起离职审批" PostBackUrl="~/SubModule/EWF/FlowAppInitList.aspx" CausesValidation="False" />
                            <asp:Button ID="bt_ChangeClassify" runat="server" Text="转为兼职" Visible="false" 
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                SelectedIndex="0" Width="100%">
                                <Items>
                                    <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="基本资料" Description=""
                                        Value="0" Enable="True"></mcs:MCSTabItem>
                                    <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="薪酬定义" Description=""
                                        Value="1" Enable="True"></mcs:MCSTabItem>
                                </Items>
                            </mcs:MCSTabControl>
                        </td>
                    </tr>
                    <tr class="tabForm">
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="Page_PM_002">
                                                </mcs:UC_DetailView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="tbl_Promotor" cellspacing="0" cellpadding="0" width="100%" border="0"
                                            runat="server">
                                            <tr>
                                                <td>
                                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td nowrap>
                                                                <h3>
                                                                    导购员所属门店列表
                                                                </h3>
                                                            </td>
                                                            <td align="right">
                                                                导购员所属门店:
                                                                <asp:DropDownList ID="ddl_CM" runat="server" DataTextField="Name" DataValueField="ID">
                                                                </asp:DropDownList>
                                                                <asp:Button ID="bt_AddCM" runat="server" Text="新 增" Width="60px" Visible="true" UseSubmitBehavior="False"
                                                                    OnClick="bt_AddCM_Click" CausesValidation="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <mcs:UC_GridView ID="gv_list" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                DataKeyNames="PM_PromotorInRetailer_ID" PanelCode="Panel_PromotorInRetail_List_002"
                                                                OnRowDeleting="gv_list_RowDeleting">
                                                                <Columns>
                                                                    <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="../CM/RT/RetailerDetail.aspx?ClientID={0}"
                                                                        DataTextField="CM_Client_FullName" HeaderText="客户全称" ControlStyle-CssClass="listViewTdLinkS1"
                                                                        >
                                                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                                                    </asp:HyperLinkField>
                                                                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    无数据</EmptyDataTemplate>
                                                            </mcs:UC_GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="50" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                </table>
</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
