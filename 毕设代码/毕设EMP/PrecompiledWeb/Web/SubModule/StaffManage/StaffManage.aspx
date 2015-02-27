<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" stylesheettheme="basic" autoeventwireup="true" inherits="SubModule_StaffManage_StaffManage, App_Web_it73ecr1" enableEventValidation="false" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td align="right" width="20">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td align="left">
                                    <h2>
                                        Ա������</h2>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:MCSTabControl ID="MCSTabControl2" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl2_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="��ݲ�ѯ" Description=""
                                    Value="0" Enable="True" Visible="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="�߼���ѯ" Description=""
                                    Value="1" Enable="True"></mcs:MCSTabItem>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td align="left">
                                    ����Ƭ��<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" DisplayRoot="True" IDColumnName="ID"
                                        NameColumnName="Name" ParentColumnName="SuperID" Width="260px" />
                                </td>
                                <td align="left">
                                    ְλ<mcs:MCSTreeControl ID="tr_Position" runat="server" DisplayRoot="True" IDColumnName="ID"
                                        NameColumnName="Name" ParentColumnName="SuperID" Width="220px" />
                                </td>
                                <td class="dataLabel">
                                    <asp:CheckBox ID="chb_ToPositionChild" runat="server" Text="����ְλ" />
                                </td>
                                <td class="dataLabel">
                                    �ؼ���<asp:TextBox ID="tbx_Search" Width="80" runat="server"></asp:TextBox>
                                    <asp:RadioButtonList ID="ddl_ApproveFlag" runat="server" DataTextField="Value" DataValueField="Key"
                                        RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                 <asp:CheckBox ID="chb_IsDI" runat="server" Text="�Ƿ������˻�" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btn_Search" OnClick="btn_Search_Click" runat="server" Width="60"
                                        Text="����" />
                                    <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="��Ա��" Width="60px" />
                                    <asp:Button ID="cmdOnPosition" runat="server" Text="��ְ" OnClick="cmdOnPosition_Click"
                                        OnClientClick="javascript:return confirm('�Ƿ���ѡ�е��˸�ְ��');" Width="60px" Visible="false">
                                    </asp:Button>
                                    <asp:Button ID="cmdOffPosition" runat="server" Text="��ְ" OnClick="cmdOffPosition_Click"
                                        OnClientClick="javascript:return confirm('�Ƿ���ѡ�е�����ְ��');" Width="60px"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="��ְԱ��" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="��ְԱ��" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <mcs:UC_GridView ID="ud_grid" runat="server" PanelCode="Panel_Staff_List" AutoGenerateColumns="False"
                            Width="100%" DataKeyNames="Org_Staff_ID" AllowPaging="true" PageSize="15" AllowSorting="True">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkStaff_ID" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="Org_Staff_ID"  DataNavigateUrlFormatString="StaffDetail.aspx?ID={0}"
                                    DataTextField="Org_Staff_RealName" ControlStyle-CssClass="listViewTdLinkS1" HeaderText="����">
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:HyperLinkField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# Bind("Org_Staff_ID","StaffDetail.aspx?ID={0}")%>'
                                            Text="��Ƭ����" CssClass="listViewTdLinkS1"  ImageUrl='<%# MCSFramework.BLL.Pub.ATMT_AttachmentBLL.GetFirstPreviewPicture(10,(int)DataBinder.Eval(Container,"DataItem.Org_Staff_ID"))%>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
