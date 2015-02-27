<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_RM_RetailerDetail, App_Web_hv25c18v" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap" style="height: 24px;">
                            <h2>
                                零售商详细信息</h2>
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="hl_ClientFeeList" runat="server" ForeColor="Red" NavigateUrl="~/SubModule/FNA/FeeApplyOrWriteoffByClientList.aspx"
                                Visible="False">柜台效费比</asp:HyperLink>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_Analysis" runat="server" Width="100px" Text="零售商分析数据" OnClick="bt_Analysis_Click"
                                Visible="false" />
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审核" Width="60px" />
                            <asp:Button ID="bt_AddApply" runat="server" Text="发起新增流程" Width="80px" OnClick="bt_AddApply_Click"
                                OnClientClick="return confirm(&quot;确定发起新增流程?&quot;)" />
                            <asp:Button ID="bt_RevocationApply" runat="server" Text="发起撤销流程" Width="80px" OnClick="bt_RevocationApply_Click"
                                OnClientClick="return confirm(&quot;确定发起撤销流程?&quot;)" />
                            <asp:Button ID="bt_Record" runat="server" Text="审批记录" Width="60px" OnClick="bt_Record_Click" />
                            <asp:Button ID="bt_ReplaceClientManager" runat="server" OnClick="bt_ReplaceClientManager_Click"
                                Text="替换销售代表" Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_RT_RetailDetail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_Contract" runat="server">
            <td>
                <table id="Table1" runat="server" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td nowrap>
                                        <h3>
                                            门店协议信息
                                        </h3>
                                    </td>
                                    <td>  <asp:DropDownList ID="ddl_state" Width="100" runat="server"  DataTextField="Value" DataValueField="Key"
                                            AutoPostBack="true" onselectedindexchanged="ddl_state_SelectedIndexChanged"></asp:DropDownList> </td>
                                    <td>
                                     上月销量:<asp:Label ID="lbl_preSales" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                      平均销量:<asp:Label ID="lbl_AvageSales" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_AddContract" runat="server" Text="新增陈列协议" Width="80px" UseSubmitBehavior="False"
                                            OnClick="bt_AddContract_Click" />
                                        <asp:Button ID="bt_AddContract2" runat="server" Text="新增返利协议" Width="80px" UseSubmitBehavior="False"
                                            OnClick="bt_AddContract2_Click" />
                                        <asp:Button ID="bt_AddContract3" runat="server" Text="新增导购协议" Width="80px" UseSubmitBehavior="False"
                                            OnClick="bt_AddContract3_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_list01" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="CM_Contract_ID" PageSize="15" Width="100%"
                                        PanelCode="Panel_RT_RetailDetail_ContracttList" OnSelectedIndexChanging="gv_list01_SelectedIndexChanging">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="查看详细" ControlStyle-CssClass="listViewTdLinkS1">
                                            </asp:CommandField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# "~/SubModule/EWF/TaskDetail.aspx?TaskID="+DataBinder.Eval(Container,"DataItem.CM_Contract_ApproveTask").ToString()%>'
                                                         Text='审批记录' Visible='<%# DataBinder.Eval(Container,"DataItem.CM_Contract_ApproveTask").ToString()!="" %>'
                                                        CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_state" EventName="selectedindexchanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr_LinkMan" runat="server">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
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
                                        <asp:Button ID="bt_AddLinkMan" runat="server" Text="新 增" Width="60px" OnClick="bt_AddLinkMan_Click"
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
                                                DataTextField="CM_LinkMan_Name" HeaderText="联系人姓名" ControlStyle-CssClass="listViewTdLinkS1"
                                                 />
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
        <tr id="tr_Promotor" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>
                                                    导购员列表
                                                </h3>
                                            </td>
                                            <td align="right">
                                                本地区现有导购员:
                                                <asp:DropDownList ID="ddl_Promotor" runat="server" DataTextField="Name" DataValueField="ID"
                                                    Width="139px">
                                                </asp:DropDownList>
                                                <asp:Button ID="bt_AddPromotor" runat="server" Text="加 入" Width="60px" Visible="true"
                                                    UseSubmitBehavior="False" OnClick="bt_AddPromotor_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_Promotor" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="PM_PromotorInRetailer_ID,PM_Promotor_ID" PanelCode="Panel_PromotorInRetail_List_001"
                                        Binded="False" ConditionString="" OnRowDeleting="gv_Promotor_RowDeleting" TotalRecordCount="0">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="PM_Promotor_ID" DataNavigateUrlFormatString="../../PM/PM_PromotorDetail.aspx?PromotorID={0}"
                                                DataTextField="PM_Promotor_Name" HeaderText="导购员姓名" ControlStyle-CssClass="listViewTdLinkS1"
                                                >
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                            <%--
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                        Text="删除" OnClientClick="return confirm('是否确认从此门店中删除该导购员?')"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据</EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
