<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_InventoryDifferencesDifferenceInput, App_Web_yabmfp6z" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="160">
                            <h2>
                                <asp:Label ID="lbl_Title" runat="server" Text="客户库存盘盈亏"></asp:Label></h2>
                        </td>
                        <td align="center">
                            <asp:Label ID="lb_DifferenceInfo" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
                        </td>
                        <td align="right">
                            &nbsp;<asp:Button ID="btn_Inventory" runat="server" Text="填报盈亏" Width="80px" OnClick="btn_Inventory_Click" />
                            <asp:Button ID="bt_Save" runat="server" Text="保 存" OnClick="bt_Save_Click" Width="80px" />
                            <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="提交申请" Width="80px" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审 核"
                                OnClientClick="return confirm('是否确认审核该盈亏记录?')" Width="80px" />
                            <asp:Button ID="bt_Del" runat="server" Text="删 除" Width="80px" OnClientClick="return confirm('是否确认删除该记录?')"
                                OnClick="bt_Del_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="DV_SVM_InventoryDifferencesDetail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30">
                            <tr>
                                <td align="right" height="28px">
                                    <font color="blue">提示！【盘点盈亏数量】填写方式，以线下20日实盘库存为标准，线下数量大于系统数量为盘盈，录入正数，相反为盘亏，则录入负数。</font>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" height="28">
                                    <table cellpadding="0" cellspacing="0" border="0" height="28">
                                        <tr>
                                            <td class="dataLabel" style="color: #FF0000">
                                                <asp:CheckBox ID="cb_OnlyDisplayUnZero" runat="server" AutoPostBack="True" OnCheckedChanged="cb_OnlyDisplayUnZero_CheckedChanged"
                                                    Text="仅显示数量不为零的产品" />
                                            </td>
                                            <td class="dataLabel">
                                                单品
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_Product1" runat="server" PageUrl="~/SubModule/Product/Pop_Search_Product.aspx"
                                                    Width="160px" />
                                            </td>
                                            <td class="dataLabel" style="color: #FF0000">
                                                品牌
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                    OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel" style="color: #FF0000">
                                                系列
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_Search" runat="server" Text="查 找" Width="60px" OnClick="bt_Search_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                                        <tr>
                                            <td nowrap style="width: 100px">
                                                <h3>
                                                    产品库存盘盈亏</h3>
                                            </td>
                                            <td><asp:Label ID="lbl_Notice" runat="server" style="color:Red"></asp:Label> </td>
                                            <td align="right">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" id="tb_AddProduct" runat="server">
                                                            <tr>
                                                                <td class="dataLabel">
                                                                    单品
                                                                </td>
                                                                <td class="dataField">
                                                                    <mcs:MCSSelectControl ID="select_Product" runat="server" PageUrl="~/SubModule/Product/Pop_Search_Product.aspx"
                                                                        Width="160px" />
                                                                </td>
                                                                <td class="dataLabel">
                                                                    盈亏数量
                                                                </td>
                                                                <td class="dataField">
                                                                    <asp:TextBox ID="tbx_Q1" runat="server" Text='0' Width="40px"></asp:TextBox>
                                                                    <asp:Label ID="Label5" runat="server" Text='箱'></asp:Label>
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Q1"
                                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_Q1" Display="Dynamic"
                                                                            ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbx_Q2" runat="server" Text='0' Width="20px"></asp:TextBox>
                                                                    <asp:Label ID="Label6" runat="server" Text='袋/盒/罐/个'></asp:Label>
                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_Q2"
                                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Q2" Display="Dynamic"
                                                                            ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="dataLabel">
                                                                    <asp:Button ID="bt_AddProduct" runat="server" Text="新增单品" OnClick="bt_AddProduct_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="SVM_InventoryDifferences_Detail_ID,SVM_InventoryDifferences_Detail_LotNumber"
                                        Width="100%" PanelCode="Panel_SVM_InventoryDifferences_DetailList" AllowPaging="false"
                                        OnRowDataBound="gv_List_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="实时库存额">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ComputInventory1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory").ToString()==""?0:(int)DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory")/(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'></asp:Label>
                                                    <asp:Label ID="lb_TrafficPackaging3" runat="server" Text='<%# Bind("PDT_Product_TrafficPackaging")%>'></asp:Label>
                                                    <asp:Label ID="lbl_ComputInventory2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory").ToString()==""?0:(int)DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory")%(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging4" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="上期（截止20日）库存额">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_20Inventory1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OPI_ClientInventory_Quantity").ToString()==""?0:(int)DataBinder.Eval(Container,"DataItem.OPI_ClientInventory_Quantity")/(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'></asp:Label>
                                                    <asp:Label ID="lb_TrafficPackaging7" runat="server" Text='<%# Bind("PDT_Product_TrafficPackaging")%>'></asp:Label>
                                                    <asp:Label ID="lbl_20Inventory2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OPI_ClientInventory_Quantity").ToString()==""?0:(int)DataBinder.Eval(Container,"DataItem.OPI_ClientInventory_Quantity")%(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging8" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="盘点盈亏数量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Quantity1" runat="server" Text='<%# (int)DataBinder.Eval(Container,"DataItem.SVM_InventoryDifferences_Detail_Quantity")/(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'
                                                        Width="40px" Enabled='<%#(bool)ViewState["EditEnable"]%>'></asp:TextBox><asp:Label ID="lb_TrafficPackaging" runat="server" Text='<%# Bind("PDT_Product_TrafficPackaging")%>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Quantity1"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Quantity1"
                                                        Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="tbx_Quantity2" runat="server" Text='<%# (int)DataBinder.Eval(Container,"DataItem.SVM_InventoryDifferences_Detail_Quantity")%(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'
                                                        Width="30px"></asp:TextBox><asp:Label ID="lb_Packaging" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_Quantity2"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Quantity2"
                                                        Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="盈亏原因">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_DifferencesReason" runat="server" DataTextField="Value"
                                                        DataValueField="Key">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="备注">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Remark" Width="120px" runat="server" Text='<%# Bind("SVM_InventoryDifferences_Detail_Remark") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="本次盈亏额">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SumPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SVM_InventoryDifferences_Detail_FactoryPrice").ToString()==""?"0":((int)DataBinder.Eval(Container.DataItem,"SVM_InventoryDifferences_Detail_Quantity")*(decimal)DataBinder.Eval(Container.DataItem,"SVM_InventoryDifferences_Detail_FactoryPrice")).ToString("f2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="盈亏率">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_DifferenceRate" runat="server" Text='<%#GetDifferenceRate(Eval("SVM_InventoryDifferences_Detail_Quantity").ToString(),Eval("SVM_JXCSummary_ComputInventory").ToString()) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_AddProduct" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="cb_OnlyDisplayUnZero" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <span style="color: #FF0000">数据处理中，请稍候...</span></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
