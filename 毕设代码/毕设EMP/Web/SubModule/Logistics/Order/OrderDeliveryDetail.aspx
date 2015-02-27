<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderDeliveryDetail.aspx.cs" Inherits="SubModule_Logistics_Order_OrderDeliveryDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        ���ŵ���ϸ��Ϣ
                                    </h2>
                                </td>
                                <td align="right" nowrap="noWrap">
                                    <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="�� ��" Width="60px" />
                                    <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" OnClientClick="return confirm(&quot;�Ƿ�ȷ��ɾ���÷�����?&quot;)"
                                        Text="ɾ��" Width="70px" />
                                    <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="���" Width="70px" />
                                    <asp:Button ID="bt_ConfirmDelivery" runat="server" OnClick="bt_ConfirmDelivery_Click"
                                        Text="ȷ�Ϸ���" Width="70px" />
                                    <asp:Button ID="bt_ConfirmSignIn" runat="server" Text="ȷ��ǩ��" Width="70px" OnClick="bt_ConfirmSignIn_Click" />
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="pn_OrderDelivery" runat="server" DetailViewCode="DV_LGS_OrderDelivery_Detail01">
                                </mcs:UC_DetailView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tr_OrganizeCity" EventName="Selected" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr runat="server" id="tr_FeeApplyList">
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr height="28px">
                                                    <td nowrap>
                                                        <h3>
                                                            �ɷ��ŵĶ����б�</h3>
                                                    </td>
                                                    <td align="right">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tabForm">
                                                <tr>
                                                    <td class="dataLabel">
                                                        ����Ƭ��
                                                    </td>
                                                    <td class="dataField">
                                                        <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                            ParentColumnName="SuperID" Width="200px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                                    </td>
                                                    <td class="dataLabel" nowrap="nowrap">
                                                        �ͻ�
                                                    </td>
                                                    <td class="dataField">
                                                        <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                                            Width="200px" />
                                                    </td>
                                                    <td class="dataLabel">
                                                        ���뵥��
                                                    </td>
                                                    <td class="dataField">
                                                        <asp:TextBox ID="tbx_SheetCode" runat="server" Width="120px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Button ID="bt_Find" runat="server" Text="����" Width="70px" OnClick="bt_Find_Click" />
                                                        <asp:Button ID="bt_AddToDelivery" runat="server" Text="���뷢����" Width="70px" OnClick="bt_AddToDeliveryList_Click" />
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
                                            <mcs:UC_GridView ID="gv_OrderAplyList" runat="server" Width="100%" AutoGenerateColumns="False"
                                                DataKeyNames="ORD_OrderApply_ID,ORD_OrderApplyDetail_ID" AllowPaging="True" PanelCode="Panel_LGS_OrderApplyList_CanDelivery">
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="ORD_OrderApply_ID" HeaderText="���뵥��" DataNavigateUrlFormatString="OrderApplyDetail.aspx?ID={0}"
                                                        DataTextField="ORD_OrderApply_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                                                        >
                                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                                    </asp:HyperLinkField>
                                                    <asp:TemplateField HeaderText="��Ʒ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lb_Product" runat="server" Text='<%# new MCSFramework.BLL.Pub.PDT_ProductBLL((int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_Product")).Model.FullName%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="��������">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lb_ApplyQuantity" ForeColor="Red" runat="server" Text='<%# GetQuantityString((int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_Product"),(int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_BookQuantity")+(int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_AdjustQuantity"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�ѷ�������">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lb_DeliveredQuantity" ForeColor="Red" runat="server" Text='<%# GetQuantityString((int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_Product"),(int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_DeliveryQuantity"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="���ɷ�������">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lb_CanDeliveryQuantity" ForeColor="Red" runat="server" Text='<%# GetQuantityString((int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_Product"),(int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_BookQuantity")+(int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_AdjustQuantity")-(int)DataBinder.Eval(Container,"DataItem.ORD_OrderApplyDetail_DeliveryQuantity"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="����">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cb_Selected" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    ������</EmptyDataTemplate>
                                            </mcs:UC_GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gv_OrderList" EventName="RowDeleting" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        ���ż�¼�б�</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <mcs:UC_GridView ID="gv_OrderList" runat="server" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="ID,ApplyDetailID" OnRowDeleting="gv_OrderList_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="���뵥������" SortExpression="ApplyDetailID">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hy_ApplySheetCode" runat="server" Text='<%# MCSFramework.BLL.Logistics.ORD_OrderApplyBLL.GetSheetCodeByDetailID(int.Parse(DataBinder.Eval(Container,"DataItem.ApplyDetailID").ToString())) %>'
                                                    NavigateUrl='<%# "OrderApplyDetail.aspx?SheetCode="+MCSFramework.BLL.Logistics.ORD_OrderApplyBLL.GetSheetCodeByDetailID(int.Parse(DataBinder.Eval(Container,"DataItem.ApplyDetailID").ToString())) %>'
                                                     ForeColor="#CC0000"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Client" HeaderText="�ͻ�" SortExpression="Client" />
                                        <asp:BoundField DataField="Product" HeaderText="��Ʒ����" SortExpression="Product" />
                                        <asp:BoundField DataField="Price" HeaderText="�۸�" SortExpression="Price" DataFormatString="{0:0.00}" />
                                        <asp:TemplateField HeaderText="ʵ������">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbx_DeliveryQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity"))).ToString() %>'
                                                    AutoPostBack="true" OnTextChanged="tbx_DeliveryQuantity_TextChanged" Enabled="<%#bNoDelivery %>"></asp:TextBox>
                                                <asp:Label ID="Label1" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                <asp:TextBox ID="tbx_DeliveryQuantity" runat="server" Width="20px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"DeliveryQuantity"))).ToString() %>'
                                                    AutoPostBack="true" OnTextChanged="tbx_DeliveryQuantity_TextChanged" Enabled="<%#bNoDelivery %>"></asp:TextBox>
                                                <asp:Label ID="Label2" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ǩ������" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbx_SignInQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"SignInQuantity"))).ToString() %>'
                                                    Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_SignInQuantity_T"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_SignInQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label3" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                <asp:TextBox ID="tbx_SignInQuantity" runat="server" Width="20px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"SignInQuantity"))).ToString() %>'
                                                    Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                                <asp:Label ID="Label4" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_SignInQuantity"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_SignInQuantity"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��������" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbx_BadQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BadQuantity"))).ToString() %>'
                                                    Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="tbx_BadQuantity_T"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbx_BadQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label5" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                <asp:TextBox ID="tbx_BadQuantity" runat="server" Width="20px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"BadQuantity"))).ToString() %>'
                                                    Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                                <asp:Label ID="Label6" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="tbx_BadQuantity"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbx_BadQuantity"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��ʧ����" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbx_LostQuantity_T" runat="server" Width="30px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"LostQuantity"))).ToString() %>'
                                                    Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="tbx_LostQuantity_T"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbx_LostQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label7" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                <asp:TextBox ID="tbx_LostQuantity" runat="server" Width="20px" Text='<%# (GetPackagingQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"LostQuantity"))).ToString() %>'
                                                    Enabled="<%#bNoSignIn %>"></asp:TextBox>
                                                <asp:Label ID="Label8" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="tbx_LostQuantity"
                                                    Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbx_LostQuantity"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="true" DeleteText="ɾ��" ControlStyle-CssClass="listViewTdLinkS1"
                                            Visible="false">
                                            <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                        </asp:CommandField>
                                    </Columns>
                                </mcs:UC_GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bt_AddToDelivery" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" height="28">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                �ϼƷ��ã�<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red"></asp:Label>
                                Ԫ
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
