<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="OrderLimitFactor.aspx.cs" Inherits="SubModule_Logistics_Order_ORD_SalesFactor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                                    <tr>
                                        <td width="24">
                                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                        </td>
                                        <td nowrap="noWrap" style="width: 180px">
                                            <h2>
                                                动销量系数设定</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="12">
                                            <asp:CheckBox ID="CBAll" runat="server" Text="全选" OnCheckedChanged="CBAll_CheckedChanged"
                                                AutoPostBack="True" />
                                            <asp:Button ID="BtnAdd" runat="server" Text="新增" Width="60px" OnClick="BtnAdd_Click" />
                                            <asp:Button ID="BtnSelect" runat="server" Text="查找" Width="60px" OnClick="BtnSelect_Click" />
                                            <asp:Button ID="btn_Approve" runat="server" Text="审核" Width="60px" OnClick="btn_Approve_Click" />
                                            <asp:Button ID="btn_CancleApprove" runat="server" Text="取消审核" Width="60px" OnClick="btn_CancleApprove_Click" />
                                            <asp:Button ID="BtnSave" runat="server" Text="保存" Width="60px" OnClick="BtnSave_Click" />
                                            <asp:Button ID="BtnDelete" runat="server" Text="批量删除" OnClientClick="return confirm(&quot;删除后数据无法恢复，是否确认删除?&quot;)"
                                                OnClick="BtnDelete_Click" />
                                        </td>
                                    </tr>
                                    <tr class="tabForm">
                                        <td>
                                            管理片区
                                        </td>
                                        <td align="left">
                                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                ParentColumnName="SuperID" Width="200px" AutoPostBack="true" OnSelected="tr_OrganizeCity_Selected" />
                                        </td>
                                        <td>
                                            会计月
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddl_AccountMonth_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            经销商
                                        </td>
                                        <td>
                                            <mcs:MCSSelectControl runat="server" ID="select_client" Width="200px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1"
                                                OnSelectChange="select_client_SelectChange" />
                                        </td>
                                        <td class="dataLabel">
                                            品牌
                                        </td>
                                        <td class="dataField">
                                            <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" 
                                                DataValueField="ID" AutoPostBack="True" 
                                                onselectedindexchanged="ddl_Brand_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            品系
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="dataLabel">
                                            品项
                                        </td>
                                        <td>
                                            <mcs:MCSSelectControl runat="server" ID="select_Product" PageUrl="~/SubModule/Product/Pop_Search_Product.aspx?IsOpponent=1"
                                                Width="250px" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" DataKeyNames="ORD_OrderLimitFactor_ID" PanelCode="Panel_ORD_OrderLimitFactor"
                            OnPageIndexChanging="gv_List_PageIndexChanging" PageSize="20">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbx" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="进货系数" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_Factor" runat="server" Text='<%# Bind("ORD_OrderLimitFactor_Factor","{0:0.###}") %>'
                                            Enabled='<%#  DataBinder.Eval(Container,"DataItem.ORD_OrderLimitFactor_ApproveFlag").ToString()=="未审核"%>'
                                            Width="50px"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server"
                                                ControlToValidate="tbx_Factor" Display="Dynamic" ErrorMessage="必须是数字" Operator="DataTypeCheck"
                                                Type="Double"></asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="期初库存">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_PreInve" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_EndingInventory").ToString()==""?0:DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_EndingInventory")%>'></asp:Label>
                                        <asp:Label ID="lb_Packaging" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="上期进货">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_PrePurchase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_PurchaseVolume").ToString()==""?0:DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_PurchaseVolume")%>'></asp:Label>
                                        <asp:Label ID="lb_PurchasePackaging" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="理论订单量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_Quantity" runat="server" Text='<%# Bind("ORD_OrderLimitFactor_TheoryQuantity") %>'
                                            OnTextChanged="CheckChange" AutoPostBack="true" Enabled='<%#  DataBinder.Eval(Container,"DataItem.ORD_OrderLimitFactor_ApproveFlag").ToString()=="未审核"%>'
                                            ToolTip="理论订货量应该在理论存货量上下限之间！" Width="50px"></asp:TextBox>
                                        <asp:Label ID="lab_Packaging" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_Quantity"
                                            Display="Dynamic" ErrorMessage="必须是数字" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="理论存货下限">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_LowerLimit" runat="server" Text='<%# Bind("ORD_OrderLimitFactor_LowerLimit") %>'
                                            OnTextChanged="CheckChange" AutoPostBack="true" Width="50px" Enabled='<%#  DataBinder.Eval(Container,"DataItem.ORD_OrderLimitFactor_ApproveFlag").ToString()=="未审核"%>'></asp:TextBox>
                                        <asp:Label ID="lb_LowerPackaging" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_LowerLimit"
                                            Display="Dynamic" ErrorMessage="必须是数字" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="理论存货上限">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbx_UpperLimit" runat="server" Text='<%# Bind("ORD_OrderLimitFactor_UpperLimit" )%>'
                                            OnTextChanged="CheckChange" AutoPostBack="true" Width="50px" Enabled='<%#  DataBinder.Eval(Container,"DataItem.ORD_OrderLimitFactor_ApproveFlag").ToString()=="未审核"%>'></asp:TextBox>
                                        <asp:Label ID="lb_UpPackaging" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_UpperLimit"
                                            Display="Dynamic" ErrorMessage="必须是数字" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
