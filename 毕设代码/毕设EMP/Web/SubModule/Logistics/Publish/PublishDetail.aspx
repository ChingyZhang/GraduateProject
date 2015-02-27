<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PublishDetail.aspx.cs" Inherits="SubModule_Logistics_Publish_PublishDetail" %>

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
                                                <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                            </td>
                                            <td nowrap="noWrap">
                                                <h2>
                                                    <asp:Label ID="lb_PageTitle" runat="server" Text="�빺Ŀ¼����"></asp:Label></h2>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_AlertInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_Save" runat="server" Text="�� ��" Width="60px" OnClick="btn_Save_Click" />
                                                <asp:Button ID="btn_Delete" runat="server" Text="ɾ��" Width="60px" OnClick="btn_Delete_Click"
                                                    OnClientClick="return confirm(&quot;�Ƿ�ȷ��Ҫɾ���÷����빺Ŀ¼?&quot;)" CausesValidation="False" />
                                                <asp:Button ID="bt_Publish" runat="server" CausesValidation="False" OnClick="bt_Publish_Click"
                                                    OnClientClick="return confirm(&quot;�Ƿ�ȷ�Ͽ��������������������ƷĿ¼�������ٸ��ģ�&quot;);" Text="ȷ�Ϸ���"
                                                    ToolTip="��ʼ�����빺" Width="60px" />
                                                <asp:Button ID="bt_Modify" runat="server" CausesValidation="False" Text="�޸ķ���" ToolTip="�����޸ķ���"
                                                    Width="60px" OnClick="bt_Modify_Click" OnClientClick="return confirm(&quot;�Ƿ�ȷ���޸���ȷ�ϵķ��������޸ĺ�ķ�����Ϣ����Ӱ��֮ǰ�ѳɹ�������빺����&quot;);" />
                                                <asp:Button ID="bt_Close" runat="server" CausesValidation="False" OnClientClick="return confirm(&quot;�Ƿ�ȷ���رշ�������������빺��������Ч��&quot;)"
                                                    Text="�رշ���" ToolTip="ֹͣ�빺����,���빺����ı�����Ч" Width="60px" OnClick="bt_Close_Click" />
                                                <asp:Button ID="bt_Cancel" runat="server" CausesValidation="False" OnClientClick="return confirm(&quot;�Ƿ�ȷ��ֹͣ�빺���룬��ȡ��������������빺����&quot;)"
                                                    Text="ȡ������" ToolTip="ֹͣ�빺���룬��������빺ȫ��ȡ��" Width="60px" Visible="False" OnClick="bt_Cancel_Click" />
                                                <asp:Button ID="bt_ViewApplyList" runat="server" Text="�鿴�빺�б�" OnClick="bt_ViewApplyList_Click" />
                                                <asp:Button ID="bt_Copy" runat="server" Text="����Ŀ¼" Width="60px" OnClick="bt_Copy_Click" />
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_DetailView ID="pl_ApplyPublish" runat="server" DetailViewCode="DV_ORD_ApplyPublish_Detail">
                                    </mcs:UC_DetailView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" height="28px" runat="server"
                                                id="tbl_publish">
                                                <tr>
                                                    <td valign="bottom">
                                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                                            Width="100%">
                                                            <Items>
                                                                <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="����Ŀ¼"
                                                                    Value="0" Visible="True" />
                                                                <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="�Ƿ���Ŀ¼"
                                                                    Value="1" Visible="True" />
                                                            </Items>
                                                        </mcs:MCSTabControl>
                                                    </td>
                                                    <td align="right" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #999999">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="480px" id="tbl_Find" visible="false"
                                                            runat="server">
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                                        OnSelectedIndexChanged="rbl_Brand_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                        AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID"
                                                                        RepeatDirection="Horizontal">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    ��Ʒ�ؼ���:<asp:TextBox ID="tbx_ProductText" runat="server" Width="60px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="bt_Find" runat="server" Text="�� ��" OnClick="bt_Find_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="right" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #999999">
                                                        <asp:CheckBox ID="cb_SelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_SelectAll_CheckedChanged"
                                                            Text="ȫѡ" />
                                                        <asp:Button ID="bt_In" runat="server" OnClick="bt_In_Click" Text="���뷢��Ŀ¼" Visible="False" />
                                                        <asp:Button ID="bt_Out" runat="server" OnClick="bt_Out_Click" Text="�Ƴ�����Ŀ¼" />
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
                            <tr class="tabForm" runat="server" id="tr_List">
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" 
                                        AutoGenerateColumns="False"   PageSize="30"
                                        DataKeyNames="ID,Product" AllowPaging="True" onpageindexchanging="gv_List_PageIndexChanging" 
                                        >
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Check" runat="server" Enabled="<%#CanEnabled %>"></asp:CheckBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ʒ�����">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ʒ������">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_ProductName" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"FullName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_Spec" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"Spec") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��װ<br/>ϵ��">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_ConvertFactor" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"ConvertFactor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Price" HeaderText="����<br/>�۸�" HtmlEncode="false" DataFormatString="{0:0.##}"  />
                                            <asp:TemplateField HeaderText="����<br/>�۸�">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_CasePrice" runat="server" Text='<%# (int.Parse(GetProductInfo((int)Eval("Product"),"ConvertFactor"))*(decimal)Eval("Price")).ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�ϼƿɹ�<br/>�빺����">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_AvailableQuantity_T" runat="server" Width="40px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"AvailableQuantity"))).ToString() %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                    <asp:Label ID="lb_Packaging_T1" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_AvailableQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"
                                                        SetFocusOnError="True"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_AvailableQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tbx_AvailableQuantity_T"
                                                        Display="Dynamic" ErrorMessage="������ڻ����0" MinimumValue="0" SetFocusOnError="True"
                                                        MaximumValue="1000000" Type="Integer"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��������<br/>�빺��">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_MinQuantity_T" runat="server" Width="40px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"MinQuantity"))).ToString() %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                    <asp:Label ID="lb_Packaging_T2" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'>
                                                    </asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_MinQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_MinQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="tbx_MaxQuantity_T"
                                                        Display="Dynamic" ErrorMessage="������ڻ����0" MinimumValue="0" MaximumValue="1000000"
                                                        SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�������<br/>�빺��">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_MaxQuantity_T" runat="server" Width="40px" Text='<%# (GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),(int)DataBinder.Eval(Container.DataItem,"MaxQuantity"))).ToString() %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                    <asp:Label ID="lb_Packaging_T3" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_MaxQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_MaxQuantity_T"
                                                        Display="Dynamic" ErrorMessage="����Ϊ��">
                                                    </asp:RequiredFieldValidator><asp:RangeValidator ID="RangeValidator3" runat="server"
                                                        ControlToValidate="tbx_MaxQuantity_T" Display="Dynamic" ErrorMessage="������ڻ����0"
                                                        MinimumValue="0" MaximumValue="1000000" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���Ϳ���">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_GiveLevel" runat="server" Width="120px" Text='<%# DataBinder.Eval(Container,"DataItem[\"GiveLevel\"]") %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��ע˵��">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Remark" runat="server" Width="200px" Text='<%# Bind("Remark") %>'
                                                        ReadOnly="<%#!CanEnabled %>"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���빺����">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_SubmitQuantity" runat="server" Font-Bold="true" ForeColor="Red"
                                                        Text='<%# GetTrafficeQuantity((int)DataBinder.Eval(Container.DataItem,"Product"),GetSubmitQuantity((int)DataBinder.Eval(Container,"DataItem.Product"))).ToString()%>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging_T5" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                    <font color="red">*���ɹ��빺������Ϊ0ʱ��������������Ƭ��ͬʱ�����빺����������������빺����Ϊ0ʱ���������Ƶ��������빺������<br />
                                    </font>
                                </td>
                            </tr>
                            <tr class="tabForm" runat="server" id="tr_NotInList">
                                <td>
                                    <mcs:UC_GridView ID="gv_NotInList" runat="server" Width="100%" AutoGenerateColumns="False" 
                                        DataKeyNames="ID" AllowPaging="true" PageSize="30" OnPageIndexChanging="gv_NotInList_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Code" HeaderText="����" />
                                            <asp:BoundField DataField="Brand" HeaderText="Ʒ��" />
                                            <asp:BoundField DataField="Classify" HeaderText="����" />
                                            <asp:BoundField DataField="Fullname" HeaderText="��Ʒ" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
