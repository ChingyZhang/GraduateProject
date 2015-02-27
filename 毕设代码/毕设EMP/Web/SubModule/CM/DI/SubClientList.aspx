<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="SubClientList.aspx.cs" Inherits="SubModule_CM_DI_SubClientList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="���������ͻ��б�"></asp:Label></h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="dataLabel" width="100px">
                                    ������
                                </td>
                                <td width="230px">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../PopSearch/Search_SelectClient.aspx?ClientType=2"
                                        Width="200px" />
                                </td>
                                <td width="220px">
                                    <asp:RadioButtonList ID="rbl_SupplierMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_SupplierMode_SelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">���ͳ�Ʒ�ŵ�</asp:ListItem>
                                        <asp:ListItem Value="2">������Ʒ�ŵ�</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    ��ݲ�ѯ
                                    <asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Code">�ͻ����</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.FullName" Selected="True">�ͻ�ȫ��</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.ShortName" Enabled="false">�ͻ����</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.TeleNum" Enabled="false">�绰����</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_Client.Address">�ͻ���ַ</asp:ListItem>
                                        <asp:ListItem Value="MCS_CM.dbo.CM_LinkMan.Name" Enabled="false">��Ҫ��ϵ��</asp:ListItem>
                                    </asp:DropDownList>
                                    ���ڻ�������
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                    <asp:Button ID="bt_Find" runat="server" Text="��ѯ" Width="60px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_SearchType" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="������" Description=""
                            Value="0" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="������" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" PanelCode="Panel_CM_DI_SubClientList"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" DataKeyNames="CM_Client_ID"
                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnRowDataBound="gv_List_RowDataBound">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/SalesVolumeList.aspx?Type=2&SellInClientID={0}"
                                    Text="����" ControlStyle-CssClass="listViewTdLinkS1"  />
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/SalesVolumeList.aspx?Type=3&SellOutClientID={0}"
                                    Text="����" ControlStyle-CssClass="listViewTdLinkS1"  />
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/InventoryList.aspx?ClientID={0}"
                                    Text="���" ControlStyle-CssClass="listViewTdLinkS1"  />
                                <%--<asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="~/SubModule/SVM/JXCSummary_List.aspx?ClientID={0}"
                                    Text="������" ControlStyle-CssClass="listViewTdLinkS1"  />--%>
                            </Columns>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_SupplierMode" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
