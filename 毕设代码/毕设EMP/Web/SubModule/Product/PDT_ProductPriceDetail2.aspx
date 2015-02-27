<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDT_ProductPriceDetail2.aspx.cs"
    Inherits="SubModule_Product_PDT_ProductPriceDetail2" MasterPageFile="~/MasterPage/BasicMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                        <tr>
                                            <td width="24">
                                                <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td nowrap="noWrap">
                                                <h2>
                                                    <asp:Label ID="lb_PageTitle" runat="server" Text="�ͻ���Ʒ�۸��"></asp:Label></h2>
                                            </td>
                                            <td class="dataLabel" style="color: Red; font-weight: bold;">
                                                ����Ϊ��С���۵�λ(��\��\��)�ļ۸�
                                            </td>
                                            <td align="right">
                                                <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                    OnSelectedIndexChanged="rbl_Brand_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID"
                                                    RepeatDirection="Horizontal">
                                                </asp:DropDownList>
                                                <asp:Button ID="bt_Find" runat="server" Text="�� ��" OnClick="bt_Find_Click" />
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_Save" runat="server" Text="�� ��" Width="60px" OnClick="btn_Save_Click" />
                                                <asp:Button ID="btn_Delete" runat="server" Text="ɾ��" Width="60px" OnClick="btn_Delete_Click"
                                                    OnClientClick="return confirm(&quot;�Ƿ�ȷ��Ҫɾ���ü۱�?&quot;)" />
                                                <asp:Button ID="btn_Approve" runat="server" Text="��˼۱�" Width="60px" OnClick="btn_Approve_Click"
                                                    OnClientClick='return confirm("�Ƿ�ȷ����˼۱�?��˺�۱������������޸ģ�");' />
                                                <asp:Button ID="btn_UnApprove" runat="server" Text="�������" Width="60px" OnClick="btn_UnApprove_Click"
                                                    OnClientClick='return confirm("�Ƿ�ȷ�ϳ����۱����ˣ�������˺�۱���������!");' />
                                                <asp:Button ID="bt_Stop" runat="server" Text="ͣ�ü۱�" Width="60px" OnClick="bt_Stop_Click"
                                                    OnClientClick="return confirm(&quot;�Ƿ�ȷ��Ҫͣ�øü۱�?&quot;)" />
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td class="tabForm">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td class="dataLabel">
                                                �ͻ�
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lbl_Client" runat="server"></asp:Label>
                                            </td>
                                            <td class="dataLabel">
                                                �̳б�׼�۱�
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lbl_StandardPrice" runat="server"></asp:Label>
                                            </td>
                                            <td class="dataLabel">
                                                �۱���Ч����
                                            </td>
                                            <td class="dataLabel">
                                                <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="���ڸ�ʽ����"
                                                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>��<asp:TextBox
                                                        ID="tbx_end" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="���ڸ�ʽ����"
                                                    Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                            </td>
                                            <td class="dataLabel">
                                                ��˱�־
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_ApproveFlag" runat="server" Text="δ���"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_tab">
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                                            <Items>
                                                                <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="��Ӫ��Ʒ"
                                                                    Value="0" />
                                                                <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="�Ǿ�Ӫ��Ʒ"
                                                                    Value="1" />
                                                            </Items>
                                                        </mcs:MCSTabControl>
                                                    </td>
                                                    <td align="right">
                                                        <asp:CheckBox ID="cb_SelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_SelectAll_CheckedChanged"
                                                            Text="ȫѡ" />
                                                        <asp:Button ID="bt_In" runat="server" OnClick="bt_In_Click" Text="���뾭Ӫ��Ʒ" Visible="false" />
                                                        <asp:Button ID="bt_Out" runat="server" OnClick="bt_Out_Click" Text="�Ƴ���Ӫ��Ʒ" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="tabForm" runat="server" id="tr_Product">
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                        Binded="False" DataKeyNames="ID,Product" AllowPaging="true" PageSize="150" OnPageIndexChanging="gv_List_PageIndexChanging1">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��Ʒ����">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetERPCode(DataBinder.Eval(Container,"DataItem.Product").ToString()) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Product" HeaderText="��Ʒ" />
                                            <%-- <asp:TemplateField HeaderText="������(Ԫ)">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# GetFactoryPrice(DataBinder.Eval(Container,"DataItem.Product").ToString()) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="�����(Ԫ)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_BuyingPrice" runat="server" Width="60px" ReadOnly="true" Text='<%# Bind("BuyingPrice","{0:0.###}") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���ۼ�(Ԫ)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbl_FactoryPrice" runat="server" Width="60px" ReadOnly="true" Text='<%# Bind("SalesPrice","{0:0.###}") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                            <tr class="tabForm" runat="server" id="tr1" visible="false">
                                <td>
                                    <mcs:UC_GridView ID="gv_List_FacProd" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="ID" AllowPaging="true" PageSize="150" OnPageIndexChanging="gv_List_FacProd_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Code" HeaderText="����" />
                                            <asp:BoundField DataField="Brand" HeaderText="Ʒ��" />
                                            <asp:BoundField DataField="Classify" HeaderText="����" />
                                            <asp:BoundField DataField="ShortName" HeaderText="��Ʒ" />
                                        </Columns>
                                    </mcs:UC_GridView>
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
