<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PropertyDetail.aspx.cs" Inherits="SubModule_CM_PD_PropertyDetail" %>

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
                                ��ҵ��ϸ��Ϣ</h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="����" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="���" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_PD_PropertyDetail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_PropertyInOrganizeCity" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="30">
                                        <tr>
                                            <td>
                                                <h3>
                                                    ������������Ƭ��</h3>
                                            </td>
                                            <td align="right" class="dataField">
                                                Ƭ��<asp:DropDownList ID="ddl_PropertyInOrganizeCity" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                <asp:Button ID="bt_Add_PropertyInOrganizeCity" runat="server" Text="���Ӹ���" Width="80px"
                                                    ValidationGroup="2" OnClick="bt_Add_PropertyInOrganizeCity_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_PropertyInOrganizeCity" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" OnRowDeleting="gv_PropertyInOrganizeCity_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="Name" HeaderText="����" />
                                                        <asp:BoundField DataField="Code" HeaderText="����" />
                                                        <asp:ButtonField Text="ɾ��" CommandName="Delete" ControlStyle-CssClass="listViewTdLinkS1" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        ������</EmptyDataTemplate>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_Contract" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>
                                                    ���޺�ͬ�б�
                                                </h3>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddContract" runat="server" Text="�� ��" Width="60px" UseSubmitBehavior="False"
                                                    OnClick="bt_AddContract_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List_Contract" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="ID" Width="100%" OnSelectedIndexChanging="gv_List_Contract_SelectedIndexChanging"
                                        OnRowDataBound="gv_List_Contract_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="ѡ��" ControlStyle-CssClass="listViewTdLinkS1">
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="��ͬ����">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_Code" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem[\"Code\"]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BeginDate" HeaderText="��ʼ����" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                            <asp:BoundField DataField="EndDate" HeaderText="��ֹ����" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                            <asp:BoundField DataField="State" HeaderText="��ͬ״̬" />
                                            <asp:BoundField DataField="SignMan" HeaderText="��˾ǩ����" />
                                            <asp:TemplateField HeaderText="<table width=400 cellspacing=0 cellpadding=0><tr><th>��ƿ�Ŀ</th><th>�·��ý��(Ԫ)</th><th>֧����ʽ</th><th>�����ֹ����</th></tr></table>">
                                                <ItemTemplate>
                                                    <mcs:UC_GridView ID="gv_Detail" runat="server" DataKeyNames="ID,AccountTitle" AutoGenerateColumns="false"
                                                        Width="400px" ShowHeader="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="AccountTitle" HeaderText="��ƿ�Ŀ" SortExpression="AccountTitle" />
                                                            <asp:BoundField DataField="ApplyLimit" HeaderText="�·��ý��(Ԫ)" SortExpression="ApplyLimit" />
                                                            <asp:BoundField DataField="PayMode" HeaderText="֧����ʽ" SortExpression="PayMode" />
                                                            <asp:TemplateField HeaderText="�����ֹ����">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lb_PayEndDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem[\"PayEndDate\"]") %> '></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </mcs:UC_GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# "~/SubModule/EWF/TaskDetail.aspx?TaskID="+DataBinder.Eval(Container,"DataItem.ApproveTask").ToString()%>'
                                                         Text='������¼' Visible='<%# (int)Eval("ApproveTask")!=0 %>' CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_Telephone" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>
                                                    �̶��绰�б�
                                                </h3>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddTele" runat="server" Text="�����绰" Width="60px" Visible="true"
                                                    UseSubmitBehavior="False" OnClick="bt_AddTele_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_Telephone" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="CM_PropertyInTelephone_ID" PanelCode="Panel_PropertyInTelephoneList">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="CM_PropertyInTelephone_ID" DataNavigateUrlFormatString="PropertyInTelephoneDetail.aspx?TelephoneID={0}"
                                                Text="�鿴��ϸ" HeaderText="" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
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
        <tr id="tr_Staff" runat="server">
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
                                                    ס����Ա�б�
                                                </h3>
                                            </td>
                                            <td align="right">
                                                ѡ��Ҫס�޵�Ա��:
                                                <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx?MultiSelected=Y"
                                                    Width="300px" />
                                                <asp:Button ID="bt_AddStaff" runat="server" Text="����ס��" Width="60px" Visible="true"
                                                    UseSubmitBehavior="False" OnClick="bt_AddStaff_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_Staff" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="Org_Staff_ID" PanelCode="Panel_StaffInProperty_List" Binded="False"
                                        ConditionString="" TotalRecordCount="0" OnRowDeleting="gv_Staff_RowDeleting">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="Org_Staff_ID" DataNavigateUrlFormatString="../../StaffManage/StaffDetail.aspx?ID={0}"
                                                DataTextField="Org_Staff_RealName" HeaderText="Ա������" ControlStyle-CssClass="listViewTdLinkS1"
                                                >
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                        Text="ɾ��" OnClientClick="return confirm('�Ƿ�ȷ���Ƴ���Ա��?')"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
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
