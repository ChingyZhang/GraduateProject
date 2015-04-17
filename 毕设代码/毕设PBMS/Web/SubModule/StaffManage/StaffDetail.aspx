<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="StaffDetail.aspx.cs" Inherits="SubModule_StaffManage_StaffDetail" %>

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
                                Ա������
                            </h2>
                        </td>
                        <td>
                            <asp:Label ID="lb_OverBudgetInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="80px" Text="����Ա��" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Approve" runat="server" Width="80px" Text="�� ��" OnClick="bt_Approve_Click"
                                OnClientClick="return confirm(&quot;ȷ������Ա�����ͨ��?&quot;)" />
                            <asp:Button ID="bt_AddApply" runat="server" Text="������ְ����" Width="80px" OnClick="bt_AddApply_Click"
                                OnClientClick="return confirm(&quot;ȷ��������������?&quot;)" Visible="false"/>
                            <asp:Button ID="bt_TaskDetail" runat="server" Text="������¼" Width="60px" OnClick="bt_TaskDetail_Click"
                                Visible="false" />
                            <asp:Button ID="bt_Print" runat="server" Text="��ӡ" Width="47px" OnClick="bt_Print_Click" Visible="false"/>
                            <asp:Button ID="bt_RevocationApply" runat="server" Text="������ְ����" PostBackUrl="~/SubModule/EWF/FlowAppInitList.aspx" Visible="false"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="panel1" runat="server" DetailViewCode="Page_SM_002" Visible="true">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_StaffInOrganizeCity" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="30">
                                        <tr>
                                            <td>
                                                <h3>
                                                    Ա����ܵ���������Ƭ��</h3>
                                            </td>
                                            <td align="right" class="dataField">
                                                Ƭ��<asp:DropDownList ID="ddl_StaffInOrganizeCity" runat="server">
                                                </asp:DropDownList>
                                                <asp:Button ID="bt_Add_StaffInOrganizeCity" runat="server" Text="���Ӽ��" Width="80px"
                                                    OnClick="bt_Add_StaffInOrganizeCity_Click" ValidationGroup="2" />
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
                                                <mcs:UC_GridView ID="gv_StaffInOrganizeCity" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" OnRowDeleting="gv_StaffInOrganizeCity_RowDeleting">
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
        <tr id="tr_LoginUser" runat="server">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="30">
                                <tr>
                                    <td>
                                        <h3>
                                            Ա����¼�ʻ�</h3>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_CreateUser" runat="server" Text="�����û�" Width="80px" OnClick="bt_CreateUser_Click" />
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
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                            DataKeyNames="UserName" Binded="False" ConditionString="" PanelCode="" TotalRecordCount="0">
                                            <Columns>
                                                <asp:BoundField DataField="UserName" HeaderText="�û���" SortExpression="UserName" />
                                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="��������" SortExpression="CreationDate" />
                                                <asp:BoundField DataField="LastLoginDate" HeaderText="����¼����" SortExpression="LastLoginDate" />
                                                <asp:CheckBoxField DataField="IsApproved" HeaderText="�Ƿ���Ч" SortExpression="IsOnline" />
                                                <asp:CheckBoxField DataField="IsLockedOut" HeaderText="�Ƿ�����" SortExpression="IsLockedOut" />
                                                <asp:TemplateField HeaderText="������ɫ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_UserInRoles" runat="server" Text='<%# GetRolesByUserName(Eval("UserName").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Submodule/Login/UserDetail.aspx?Username="+Server.UrlPathEncode(Eval("UserName").ToString()) %>'
                                                            Target="_blank" Text="��ϸ��Ϣ"></asp:HyperLink>
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr_UploadFile" runat="server">
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="10" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
