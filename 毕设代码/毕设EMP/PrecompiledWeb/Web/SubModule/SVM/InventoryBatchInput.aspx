<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_InventoryBatchInput, App_Web_yabmfp6z" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function keyDown(e) {
            //Tab:9 Shift:16
            if (event.keyCode == 9 || event.keyCode == 16) {
                if (e.value == "") e.value = "0";
                return;
            }
            //Up:38 Down:40
            if (event.keyCode == 38 || event.keyCode == 40) {
                if (e.value == "") e.value = "0";
                var id = e.id;
                if (id.endsWith("_Quantity1") || id.endsWith("_Quantity2") || id.endsWith("_Remark")) {
                    var p1 = id.indexOf("_ctl", 0) + 4;
                    var p2 = id.indexOf("_", p1);

                    var pre = id.substring(0, p1)
                    var xh = parseInt(id.substring(p1, p2), 10);
                    if (event.keyCode == 38)
                        xh--;
                    else
                        xh++;
                    xh = xh.toString();
                    if (xh.length < 2) xh = "0" + xh;
                    id = id.substring(0, p1) + xh + id.substring(p2, id.length);

                    if (document.getElementById(id) != null) {
                        document.getElementById(id).focus();
                        if (document.getElementById(id).value == "0")
                            document.getElementById(id).value = "";
                    }
                }
            }
        }        
    </script>

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
                                <asp:Label ID="lbl_Title" runat="server" Text="客户产品库存"></asp:Label></h2>
                        </td>
                        <td align="left" style="color: #FF0000">
                            注:库存提交操作，转移至库存汇总页面下统一提交!
                        </td>
                        <td align="right">
                            &nbsp;<asp:Button ID="btn_Inventory" runat="server" Text="填报库存" Width="80px" OnClick="btn_Inventory_Click" />
                            <asp:Button ID="bt_Save" runat="server" Text="暂 存" OnClick="bt_Save_Click" Width="80px"
                                UseSubmitBehavior="false" />
                            <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="提交申请" Width="80px"
                                Visible="false" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审 核"
                                OnClientClick="return confirm('是否确认审核该库存记录?')" Width="80px" Visible="false" />
                            <asp:Button ID="bt_Re_Approve" runat="server" Text="取消审核" Width="80px" OnClick="bt_Re_Approve_Click"
                                OnClientClick="return confirm('是否确认取消审核该库存记录?')" Visible="false" />
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
                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="DV_SVM_InventoryDetail">
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
                                <td align="right" height="28">
                                    <table cellpadding="0" cellspacing="0" border="0" height="28">
                                        <tr>
                                            <td style="color: Blue">
                                                键盘操作提示：键盘下↓：向下，键盘上↑：向上，Tab键：向右，Shift+Tab键：向左
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
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
                                                    库存列表</h3>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Notice" runat="server" Style="color: Red"></asp:Label>
                                            </td>
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
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="SVM_Inventory_Detail_ID,PDT_Product_Classify"
                                        Width="100%" PanelCode="Panel_SVM_Inventory_DetailList" PageSize="60" OnDataBound="gv_List_DataBound"
                                        GridLines="Horizontal">
                                        <Columns>
                                            <asp:TemplateField HeaderText="库存实盘数量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Quantity1" runat="server" Text='<%# (int)DataBinder.Eval(Container,"DataItem.SVM_Inventory_Detail_Quantity")/(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'
                                                        Width="40px" Enabled='<%#(bool)ViewState["EditEnable"]%>'></asp:TextBox><asp:Label ID="lb_TrafficPackaging" runat="server" Text='<%# Bind("PDT_Product_TrafficPackaging")%>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Quantity1"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Quantity1"
                                                        Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="tbx_Quantity2" runat="server" Text='<%# (int)DataBinder.Eval(Container,"DataItem.SVM_Inventory_Detail_Quantity")%(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'
                                                        Width="30px"></asp:TextBox><asp:Label ID="lb_Packaging" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_Quantity2"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Quantity2"
                                                        Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="实盘库存金额">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SumPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SVM_Inventory_Detail_FactoryPrice").ToString()==""?"0":((int)DataBinder.Eval(Container.DataItem,"SVM_Inventory_Detail_Quantity")*(decimal)DataBinder.Eval(Container.DataItem,"SVM_Inventory_Detail_FactoryPrice")).ToString("f2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="实物库存数量">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ComputInventory1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory").ToString()==""?0:(int)DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory")/(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'></asp:Label>
                                                    <asp:Label ID="lb_TrafficPackaging3" runat="server" Text='<%# Bind("PDT_Product_TrafficPackaging")%>'></asp:Label>
                                                    <asp:Label ID="lbl_ComputInventory2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory").ToString()==""?0:(int)DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory")%(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'></asp:Label>
                                                    <asp:Label ID="lb_Packaging4" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="实物库存金额">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ComputeSumPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SVM_Inventory_Detail_FactoryPrice").ToString()==""||DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory").ToString()==""?"0":((int)DataBinder.Eval(Container.DataItem,"SVM_JXCSummary_ComputInventory")*(decimal)DataBinder.Eval(Container.DataItem,"SVM_Inventory_Detail_FactoryPrice")).ToString("f2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="盘点盈亏值">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_DifferenceInventory1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory").ToString()==""?0:((int)DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory")-(int)DataBinder.Eval(Container,"DataItem.SVM_Inventory_Detail_Quantity"))/(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'></asp:Label>
                                                    <asp:Label ID="lb_DifferenceTrafficPackaging3" runat="server" Text='<%# Bind("PDT_Product_TrafficPackaging")%>'></asp:Label>
                                                    <asp:Label ID="lbl_DifferenceInventory2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory").ToString()==""?0:((int)DataBinder.Eval(Container,"DataItem.SVM_JXCSummary_ComputInventory")-(int)DataBinder.Eval(Container,"DataItem.SVM_Inventory_Detail_Quantity"))%(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'></asp:Label>
                                                    <asp:Label ID="lb_DifferencePackaging4" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="盈亏原因说明">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Remark" Width="120px" runat="server" Text='<%# Bind("SVM_Inventory_Detail_Remark") %>'></asp:TextBox>
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
