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
                                ��������ϸ��Ϣ</h2>
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="hl_ClientFeeList" runat="server" ForeColor="Red" NavigateUrl="~/SubModule/FNA/FeeApplyOrWriteoffByClientList.aspx"
                                Visible="False">��̨Ч�ѱ�</asp:HyperLink>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_Analysis" runat="server" Width="100px" Text="�����̷�������" OnClick="bt_Analysis_Click"
                                Visible="false" />
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="����" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="���" Width="60px" />
                            <asp:Button ID="bt_AddApply" runat="server" Text="������������" Width="80px" OnClick="bt_AddApply_Click"
                                OnClientClick="return confirm(&quot;ȷ��������������?&quot;)" />
                            <asp:Button ID="bt_RevocationApply" runat="server" Text="����������" Width="80px" OnClick="bt_RevocationApply_Click"
                                OnClientClick="return confirm(&quot;ȷ������������?&quot;)" />
                            <asp:Button ID="bt_Record" runat="server" Text="������¼" Width="60px" OnClick="bt_Record_Click" />
                            <asp:Button ID="bt_ReplaceClientManager" runat="server" OnClick="bt_ReplaceClientManager_Click"
                                Text="�滻���۴���" Width="80px" />
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
                                            �ŵ�Э����Ϣ
                                        </h3>
                                    </td>
                                    <td>  <asp:DropDownList ID="ddl_state" Width="100" runat="server"  DataTextField="Value" DataValueField="Key"
                                            AutoPostBack="true" onselectedindexchanged="ddl_state_SelectedIndexChanged"></asp:DropDownList> </td>
                                    <td>
                                     ��������:<asp:Label ID="lbl_preSales" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                      ƽ������:<asp:Label ID="lbl_AvageSales" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_AddContract" runat="server" Text="��������Э��" Width="80px" UseSubmitBehavior="False"
                                            OnClick="bt_AddContract_Click" />
                                        <asp:Button ID="bt_AddContract2" runat="server" Text="��������Э��" Width="80px" UseSubmitBehavior="False"
                                            OnClick="bt_AddContract2_Click" />
                                        <asp:Button ID="bt_AddContract3" runat="server" Text="��������Э��" Width="80px" UseSubmitBehavior="False"
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
                                            <asp:CommandField ShowSelectButton="true" SelectText="�鿴��ϸ" ControlStyle-CssClass="listViewTdLinkS1">
                                            </asp:CommandField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# "~/SubModule/EWF/TaskDetail.aspx?TaskID="+DataBinder.Eval(Container,"DataItem.CM_Contract_ApproveTask").ToString()%>'
                                                         Text='������¼' Visible='<%# DataBinder.Eval(Container,"DataItem.CM_Contract_ApproveTask").ToString()!="" %>'
                                                        CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������
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
                                            �ͻ���ϵ���б�
                                        </h3>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_AddLinkMan" runat="server" Text="�� ��" Width="60px" OnClick="bt_AddLinkMan_Click"
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
                                                DataTextField="CM_LinkMan_Name" HeaderText="��ϵ������" ControlStyle-CssClass="listViewTdLinkS1"
                                                 />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������</EmptyDataTemplate>
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
                                                    ����Ա�б�
                                                </h3>
                                            </td>
                                            <td align="right">
                                                ���������е���Ա:
                                                <asp:DropDownList ID="ddl_Promotor" runat="server" DataTextField="Name" DataValueField="ID"
                                                    Width="139px">
                                                </asp:DropDownList>
                                                <asp:Button ID="bt_AddPromotor" runat="server" Text="�� ��" Width="60px" Visible="true"
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
                                                DataTextField="PM_Promotor_Name" HeaderText="����Ա����" ControlStyle-CssClass="listViewTdLinkS1"
                                                >
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                            <%--
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                        Text="ɾ��" OnClientClick="return confirm('�Ƿ�ȷ�ϴӴ��ŵ���ɾ���õ���Ա?')"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������</EmptyDataTemplate>
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
