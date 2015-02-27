<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PDT_StandardPriceDetail.aspx.cs" Inherits="SubModule_Product_PDT_StandardPriceDetail" %>
<%@ Import Namespace="MCSFramework.BLL.Pub" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script type="text/javascript">
         function SelectAll(tempControl) {
             var theBox = tempControl;
             sState = theBox.checked;
             elem = theBox.form.elements;
             for (i = 0; i < elem.length; i++) {
                 if (elem[i].type == "checkbox" && elem[i].id != theBox.id && elem[i].id.indexOf("cb_Check") >= 0) {
                     if (elem[i].checked != sState) {
                         elem[i].click();
                     }
                 }
             }
         }    
    </script>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
        <Triggers>
            <asp:PostBackTrigger ControlID="bt_Export" />
        </Triggers>
        <ContentTemplate>
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
                                    ����Ϊ���۵�λ(��\��\��)�ļ۸�
                                </td>
                                <td>
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
                                    <asp:Label ID="lb_MaxRate" runat="server"></asp:Label>
                                    <asp:Button ID="bt_CompareStdPrice" runat="server" Text="�Աȱ�׼�۱�" OnClick="bt_CompareStdPrice_Click" />
                                    <asp:Button ID="btn_Save" runat="server" Text="�� ��" Width="60px" OnClick="btn_Save_Click" />
                                    <asp:Button ID="btn_Apply" runat="server" Text="��������" Width="60px" OnClientClick='return confirm("�Ƿ�ȷ�Ϸ����������̣�");'
                                        OnClick="btn_Apply_Click" />
                                    <asp:Button ID="btn_Approve" runat="server" Text="��˼۱�" Width="60px" OnClick="btn_Approve_Click"
                                        OnClientClick='return confirm("�Ƿ�ȷ����˼۱�?��˺�۱������������޸ģ�");' />
                                    <asp:Button ID="btn_PublishProduct" runat="server" Text="������Ʒ" OnClientClick="return confirm('�Ƿ�ȷ�Ͻ�ѡ�еĲ�Ʒ���ͷ��������й����˱�׼�۱�Ŀͻ��۱�Ŀ¼��?')"
                                        Width="60px" OnClick="btn_PublishProduct_Click" />
                                    <asp:Button ID="btn_UnApprove" runat="server" Text="�������" Width="60px" OnClick="btn_UnApprove_Click"
                                        OnClientClick='return confirm("�Ƿ�ȷ�ϳ����۱����ˣ�������˺�۱���������!");' />
                                    <asp:Button ID="btn_UnActive" runat="server" Text="ʧЧͣ��" Width="60px" OnClientClick='return confirm("��׼�۱�ͣ�ú��������ô˼۱�Ŀͻ��ļ۱���һ��ͣ�ã�������������Ƿ�ȷ��ͣ�ã�");'
                                        OnClick="btn_UnActive_Click" />
                                    <asp:Button ID="btn_ApplyCity" runat="server" Text="��������" OnClientClick="javascript:PopApplyCity();" />
                                    <asp:Button ID="Button1" runat="server" Text="�޸ļ�¼" OnClientClick="javascript:PopChangeHistory();" />
                                </td>
                                <td align="right" nowrap="noWrap">
                                    <asp:Button ID="bt_Export" runat="server" Text="����Excel" OnClick="bt_Export_Click"
                                        Width="60px" />
                                </td>
                            </tr>
                        </table>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="panel1" runat="server" DetailViewCode="DV_StandardPrice" Visible="true">
                                </mcs:UC_DetailView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="tr_tab">
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <mcs:MCSTabControl ID="MCSTabControl1" runat="server" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                                                Width="100%">
                                                <Items>
                                                    <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="������Ʒ"
                                                        Value="0" Visible="True" />
                                                    <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="δ������Ʒ"
                                                        Value="1" Visible="True" />
                                                </Items>
                                            </mcs:MCSTabControl>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="bt_In" runat="server" OnClick="bt_In_Click" Text="���������Ʒ" Visible="False" />
                                            <asp:Button ID="bt_Out" runat="server" OnClick="bt_Out_Click" Text="�Ƴ�������Ʒ" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="tabForm" runat="server" id="tr_Product">
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            Binded="False" DataKeyNames="ID,Product" PageSize="150" OnPageIndexChanging="gv_List_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" Width="60px" AutoPostBack="False" onclick="javascript:SelectAll(this);">
                                        </asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cb_Check" runat="server" name="cbx"></asp:CheckBox></ItemTemplate>
                                </asp:TemplateField>
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
                               
                                <asp:TemplateField HeaderText="������(Ԫ)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_FactoryPrice" runat="server" Width="60px" Text='<%# Bind("FactoryPrice","{0:0.###}") %>'
                                            Visible='<%#!(bool)ViewState["LabelVisiable"] %>' ReadOnly="<%#!btn_Save.Visible %>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_FactoryPrice"
                                            Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_FactoryPrice"
                                            Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        <asp:Label ID="lb_FactoryPrice_Rate" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_FactoryPrice" runat="server" Width="60px" Text='<%#Bind("FactoryPrice","{0:0.###}") %>'
                                            Visible='<%#(bool)ViewState["LabelVisiable"] %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="������(Ԫ)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_TradeOutPrice" runat="server" Width="60px" Text='<%# Bind("TradeOutPrice","{0:0.###}") %>'
                                            Visible='<%#!(bool)ViewState["LabelVisiable"] %>' ReadOnly="<%#!btn_Save.Visible %>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_TradeOutPrice"
                                            Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_TradeOutPrice"
                                            Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        <asp:Label ID="lb_TradeOutPrice_Rate" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_TradeOutPrice" runat="server" Width="60px" Text='<%#Bind("TradeOutPrice","{0:0.###}") %>'
                                            Visible='<%#(bool)ViewState["LabelVisiable"] %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�㹩��(Ԫ)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_TradeInPrice" runat="server" Width="60px" Text='<%# Bind("TradeInPrice","{0:0.###}") %>'
                                            Visible='<%#!(bool)ViewState["LabelVisiable"] %>' ReadOnly="<%#!btn_Save.Visible %>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_TradeInPrice"
                                            Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_TradeInPrice"
                                            Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        <asp:Label ID="lb_TradeInPrice_Rate" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_TradeInPrice" runat="server" Width="60px" Text='<%#Bind("TradeInPrice","{0:0.###}") %>'
                                            Visible='<%#(bool)ViewState["LabelVisiable"] %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="���ۼ�(Ԫ)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_StdPrice" runat="server" Width="60px" Text='<%# Bind("StdPrice","{0:0.###}") %>'
                                            Visible='<%#!(bool)ViewState["LabelVisiable"] %>' ReadOnly="<%#!btn_Save.Visible %>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_StdPrice"
                                            Display="Dynamic" ErrorMessage="����"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_StdPrice"
                                            Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        <asp:Label ID="lb_StdPrice_Rate" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_StdPrice" runat="server" Width="60px" Text='<%#Bind("StdPrice","{0:0.###}") %>'
                                            Visible='<%#(bool)ViewState["LabelVisiable"] %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="��˾����(Ԫ)" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_RebatePrice" runat="server" Width="60px" Text='<%# Bind("RebatePrice","{0:0.###}") %>'
                                            Visible='<%#!(bool)ViewState["LabelVisiable"] %>' ReadOnly="<%#!btn_Save.Visible %>"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="tbx_RebatePrice"
                                            Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        <asp:Label ID="lb_RebatePrice_Rate" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_RebatePrice" runat="server" Width="60px" Text='<%#Bind("RebatePrice","{0:0.###}") %>'
                                            Visible='<%#(bool)ViewState["LabelVisiable"] %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�����̷���(Ԫ)" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_DIRebatePrice" runat="server" Width="60px" Text='<%# Bind("DIRebatePrice","{0:0.###}") %>'
                                            Visible='<%#!(bool)ViewState["LabelVisiable"] %>' ReadOnly="<%#!btn_Save.Visible %>"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="tbx_DIRebatePrice"
                                            Display="Dynamic" ErrorMessage="����Ϊ����" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        <asp:Label ID="lb_DIRebatePrice" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_DIRebatePrice" runat="server" Width="60px" Text='<%#Bind("DIRebatePrice","{0:0.###}") %>'
                                            Visible='<%#(bool)ViewState["LabelVisiable"] %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�Ƿ������˾����" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbx_ISFL" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"ISFL").ToString()=="1" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�Ƿ���뾭���̷���" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbx_ISJH" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"ISJH").ToString()=="1" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�Ƿ���Ҫ�жϻ��ֵ�״̬" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbx_ISCheckJF" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"ISCheckJF").ToString()=="1" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
                <tr class="tabForm" runat="server" id="tr1">
                    <td>
                        <mcs:UC_GridView ID="gv_List_FacProd" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID" PageSize="150" OnPageIndexChanging="gv_List_FacProd_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" Width="60px" Text="ȫѡ" AutoPostBack="False"
                                            onclick="javascript:SelectAll(this);"></asp:CheckBox>
                                    </HeaderTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
