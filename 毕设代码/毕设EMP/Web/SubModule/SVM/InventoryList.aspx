<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="InventoryList.aspx.cs" Inherits="SubModule_SVM_InventoryList" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="Label"></asp:Label></h2>
                                </td>
                                <td align="left" style="color: #FF0000">
                                    ע:���������˲�����ת�������ܱ�ҳ����ͳһ���!
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Button ID="bt_BatchInput" runat="server" Text="����(��Ʒ)" Width="70px" OnClick="bt_BatchInput_Click" />
                                    <asp:Button ID="bt_BatchInput2" runat="server" OnClick="bt_BatchInput2_Click" Text="����(��Ʒ)"
                                        Width="70px" ForeColor="Blue" />
                                    <asp:Button ID="bt_BathApprove" runat="server" Text="�������" Width="70px" OnClick="bt_BathApprove_Click"
                                        OnClientClick="return confirm('�Ƿ�ȷ�Ͻ�ѡ�е�����������Ϊ�����ͨ����')" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="��ݲ�ѯ" Description=""
                                    Value="0" Enable="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="�߼���ѯ" Description=""
                                    Value="1" Enable="True"></mcs:MCSTabItem>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server"
                            id="tb_Head">
                            <tr>
                                <td class="dataLabel">
                                    ����Ƭ��
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="250px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    �ͻ�
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx"
                                        Width="200px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    �����
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_BeginMonth" DataValueField="ID" DataTextField="Name" runat="server">
                                    </asp:DropDownList>
                                    ��<asp:DropDownList ID="ddl_EndMonth" DataValueField="ID" DataTextField="Name" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    �Ƿ���Ʒ
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_IsCXP" runat="server">
                                        <asp:ListItem Text="����" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="��Ʒ" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="��Ʒ" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="�� ��" Width="60px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td nowrap>
                                    <h3>
                                        ����б�</h3>
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rbl_ApproveFlag" runat="server" RepeatColumns="4" RepeatLayout="Flow"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_ApproveFlag_SelectedIndexChanged"
                                        DataTextField="Value" DataValueField="Key">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:MCSTabControl ID="MCSTabControl2" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl2_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="�����ϸ" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="������" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tr_detail">
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        PanelCode="Panel_SVM_InventoryList_1" DataKeyNames="SVM_Inventory_ID" PageSize="25"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_ID" runat="server" Visible='<%#  DataBinder.Eval(Container,"DataItem.SVM_Inventory_ApproveFlag").ToString()=="δ���"&& 
                                                   DataBinder.Eval(Container,"DataItem.SVM_Inventory_SubmitFlag").ToString()!="��"%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField Text="�鿴��ϸ" DataNavigateUrlFields="SVM_Inventory_ID" DataNavigateUrlFormatString="InventoryBatchInput.aspx?InventoryID={0}"
                                                 ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="����">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetTotalFactoryPriceValue(DataBinder.Eval(Container,"DataItem.SVM_Inventory_ID").ToString()) %>'></asp:Label>
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
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tr_summary"
                            visible="false">
                            <tr>
                                <td>
                                    <asp:GridView ID="gv_Summary" runat="server" AutoGenerateColumns="False" Width="100%"
                                        ShowFooter="false">
                                        <Columns>
                                            <asp:BoundField DataField="ProductCode" HeaderText="��Ʒ����" SortExpression="ProductCode" />
                                            <asp:BoundField DataField="ProductName" HeaderText="��Ʒ����" SortExpression="ProductName" />
                                            <asp:BoundField DataField="SumQuantity" HeaderText="�ϼ�����" SortExpression="SumQuantity"
                                                DataFormatString="{0:f0}" HtmlEncode="False" />
                                            <asp:BoundField DataField="SumMoney" HeaderText="�ϼƽ��" SortExpression="SumMoney"
                                                HtmlEncode="False" DataFormatString="{0:f2}" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <span style="color: #FF0000">���ݴ����У����Ժ�...</span></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
