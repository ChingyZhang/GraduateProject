<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="StandardPriceDetail.aspx.cs" Inherits="SubModule_PBM_Product_StandardPriceDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16"></td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="商品价表详细信息"></asp:Label></h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_TDP_PDT_StandardPrice_Detail_01">
                                </mcs:UC_DetailView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                                                    <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="价表目录"
                                                        Value="0" Visible="True" />
                                                    <mcs:MCSTabItem Description="" Enable="True" ImgURL="" Target="_self" Text="非价表目录"
                                                        Value="1" Visible="True" />
                                                </Items>
                                            </mcs:MCSTabControl>
                                        </td>
                                        <td align="right" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #999999">
                                            <table cellpadding="0" cellspacing="0" border="0" width="480px" id="tbl_Find"
                                                runat="server">
                                                <tr>
                                                    <td align="left">
                                                        <mcs:MCSTreeControl ID="tr_Category" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                            ParentColumnName="SuperID" Width="200px" DisplayRoot="True" RootValue="0" />
                                                    </td>
                                                    <td></td>
                                                    <td>关键字:<asp:TextBox ID="tbx_ProductText" runat="server" Width="160px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="bt_Find" runat="server" Text="查 找" OnClick="bt_Find_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #999999">
                                            <asp:CheckBox ID="cb_SelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="cb_SelectAll_CheckedChanged"
                                                Text="全选" />
                                            <asp:Button ID="bt_In" runat="server" OnClick="bt_In_Click" Text="加入价表目录" Visible="False" />
                                            <asp:Button ID="bt_Out" runat="server" OnClick="bt_Out_Click" Text="移出价表目录" />
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
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID,Product" AllowPaging="true" PageSize="15" OnPageIndexChanging="gv_List_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="商品名称">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_ProductName" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="规格">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_Spec" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"Spec") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="包装系数">
                                    <ItemTemplate>
                                        每<asp:Label ID="lb_Packaging_T1" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        含<asp:Label ID="lb_ConvertFactor" runat="server" Text='<%# GetProductInfo((int)Eval("Product"),"ConvertFactor") %>'></asp:Label>
                                        <asp:Label ID="lb_Packaging_P1" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="销售单价">
                                    <ItemTemplate>
                                        每<asp:Label ID="lb_Packaging_T2" runat="server" Text='<%# GetTrafficeName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        <asp:TextBox ID="tbx_Price" runat="server" Width="50px" Text='<%#((decimal)Eval("Price") * decimal.Parse(GetProductInfo((int)Eval("Product"),"ConvertFactor"))).ToString("0.##") %>'></asp:TextBox>元
                                        每<asp:Label ID="lb_Packaging_P2" runat="server" Text='<%# GetPackagingName((int)DataBinder.Eval(Container.DataItem,"Product")) %>'></asp:Label>
                                        <asp:Label ID="lb_Price_T" runat="server" Text='<%# Eval("Price","{0:0.##}")  %>'></asp:Label>元
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_Price"
                                            Display="Dynamic" ErrorMessage="必须为数值" Operator="DataTypeCheck" Type="Double"
                                            SetFocusOnError="True"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Price"
                                            Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tbx_Price"
                                            Display="Dynamic" ErrorMessage="必须大于或等于0" MinimumValue="0" SetFocusOnError="True"
                                            MaximumValue="1000000" Type="Double"></asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注说明">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_Remark" runat="server" Width="200px" Text='<%# Bind("Remark") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
                <tr class="tabForm" runat="server" id="tr_NotInList">
                    <td>
                        <mcs:UC_GridView ID="gv_NotInList" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID" AllowPaging="true" PageSize="15" OnPageIndexChanging="gv_NotInList_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cb_Check" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Brand" HeaderText="品牌" />
                                <asp:BoundField DataField="Category" HeaderText="分类" />
                                <asp:BoundField DataField="Fullname" HeaderText="商品名称" />
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
