<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" stylesheettheme="basic" autoeventwireup="true" inherits="SubModule_Product_PDT_ProductCost, App_Web_r43usu2p" enableEventValidation="false" %>

<%@ Import Namespace="MCSFramework.BLL.Pub" %>
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
                                <td align="left" width="150">
                                    <h2>
                                        �ͻ���Ʒ���ʱ�</h2>
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" Text="����"
                                        Width="60" />
                                    &nbsp;<asp:Button ID="btn_Save" runat="server" Text="�� ��" Width="60px" OnClick="btn_Save_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel" width="100px">
                                    ������´�
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AlwaysSelectChildNode="False" AutoPostBack="True"
                                        OnSelected="tr_OrganizeCity_Selected" SelectDepth="0" RootValue="0" DisplayRoot="True" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" runat="server" id="tr_AddProduct">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                                                <tr>
                                                    <td>
                                                        <h3>
                                                            ������Ʒ</h3>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tabForm">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr height="28px">
                                                            <td>
                                                                ��Ʒ����
                                                            </td>
                                                            <td>
                                                                ��Ʒ����
                                                            </td>
                                                            <td>
                                                                ������
                                                            </td>
                                                            <td>
                                                                ���з���
                                                            </td>
                                                            <td>
                                                                ���´�����
                                                            </td>
                                                            <td>
                                                                �ܲ��������
                                                            </td>
                                                            <td>
                                                                ��׼��ϵ��
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <mcs:MCSSelectControl ID="select_ProductCode" runat="server" OnSelectChange="select_ProductCode_SelectChange"
                                                                    OnTextChange="select_ProductCode_TextChange" PageUrl="Serarch_SelectProduct.aspx?IsOpponent=1"
                                                                    TextBoxEnabled="true" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lb_ProductName" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_FactoryPrice" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbx_CostPrice" runat="server" Width="50px" Enabled="false"></asp:TextBox>%
                                                                <asp:CompareValidator ID="CompareValidator17" runat="server" ControlToValidate="tbx_CostPrice"
                                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbx_CostPrice2" runat="server" Width="50px" Enabled="false"></asp:TextBox>%
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_CostPrice2"
                                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbx_CostPrice3" runat="server" Width="50px" Enabled="false"></asp:TextBox>%
                                                                <asp:CompareValidator ID="CompareValidator19" runat="server" ControlToValidate="tbx_CostPrice3"
                                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbx_QuantityFactor" runat="server" Width="50px"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="tbx_QuantityFactor"
                                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button ID="bt_AddProduct" runat="server" OnClick="bt_AddProduct_Click" Text="���Ӳ�Ʒ"
                                                                    Width="70px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="gv_List" EventName="SelectedIndexChanging" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
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
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="center">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Product"
                                                Width="100%" OnRowDeleting="gv_List_RowDeleting" OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" SelectText="ѡ��" ControlStyle-CssClass="listViewTdLinkS1" />
                                                    <asp:TemplateField HeaderText="��Ʒ����">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# GetERPCode(DataBinder.Eval(Container,"DataItem.Product").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="��Ʒ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_product" runat="server" Text='<%# new PDT_ProductBLL((int)DataBinder.Eval(Container,"DataItem.Product")).Model.ShortName  %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="������">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# GetFactoryPrice(DataBinder.Eval(Container,"DataItem.Product").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CostPrice" HeaderText="���з���(%)" DataFormatString="{0:0.##}" />
                                                    <asp:TemplateField HeaderText="���´�����(%)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#  DataBinder.Eval(Container,"DataItem[\"CostPrice2\"]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�ܲ����۷���(%)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%#  DataBinder.Eval(Container,"DataItem[\"CostPrice3\"]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="��׼��ϵ��">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%#  DataBinder.Eval(Container,"DataItem[\"QuantityFactor\"]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="true" DeleteText="ɾ��" ControlStyle-CssClass="listViewTdLinkS1" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    ������
                                                </EmptyDataTemplate>
                                            </mcs:UC_GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="bt_AddProduct" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
