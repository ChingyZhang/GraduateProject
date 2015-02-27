<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="HR_SpecialApplyList.aspx.cs" Inherits="SubModule_FNA_HR_SpecialApplyList"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td style="width: 16">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="noWrap" style="width: 150px">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="">��������</asp:Label></h2>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="300">
                            ����Ƭ��:<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="200px" DisplayRoot="True" Height="17px" />
                        </td>
                        <td>
                            �����:
                            <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td> 
                        <td align="right">
                            ��Ŀ: <asp:DropDownList ID="ddl_AccountTitleType" runat="server" Width ="200px" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>&nbsp;
                        </td>
                        
                        <td align="right">
                            ���뵥��:<asp:TextBox ID="tbx_SheetCode" runat="server"></asp:TextBox>&nbsp;&nbsp;
                        </td>
                        
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    ¼���ˣ�
                                    <mcs:MCSSelectControl ID="Select_InsertStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="150px"></mcs:MCSSelectControl>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                          <td align="right">
                            ���״̬: <asp:DropDownList ID="ddl_ApproveFlag" runat="server" Width ="80px" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="�� ��" Width="60px" OnClick="bt_Find_Click" />
                            <asp:Button ID="bt_Add" runat="server" Text="�� ��" Width="60px" OnClick="bt_Add_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel_List" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="ID" PageSize="15" Width="100%" PanelCode="" Binded="False" ConditionString=""
                                        OrderFields="" TotalRecordCount="0" OnPageIndexChanging="gv_List_PageIndexChanging">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="HR_SpecialApplyDetail.aspx?ID={0}"
                                                Text="�鿴��ϸ" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                            <asp:BoundField DataField="SheetCode" HeaderText="���뵥��" />
                                            <asp:TemplateField HeaderText="����Ƭ��">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lb_OrganizeCity" Text='<%# MCSFramework.BLL.TreeTableBLL.GetFullPathName("Addr_OrganizeCity",(int)DataBinder.Eval(Container,"DataItem.OrganizeCity")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AccountMonth" HeaderText="�����" />
                                            <asp:BoundField DataField="AccountTitleType" HeaderText="��Ŀ" />
                                            <asp:BoundField DataField="ApproveFlag" HeaderText="����״̬" />
                                            <asp:BoundField DataField="InsertStaff" HeaderText="¼����" />
                                            <asp:BoundField DataField="InsertTime" HeaderText="¼��ʱ��" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Task", "~/SubModule/EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                        Text="������¼" Visible='<%# Eval("Task").ToString()!="0" %>' ></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            ������
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                                </Triggers>
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
