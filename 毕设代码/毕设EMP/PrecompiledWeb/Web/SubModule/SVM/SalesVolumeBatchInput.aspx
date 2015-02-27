<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_SalesVolumeBatchInput, App_Web_yabmfp6z" enableEventValidation="false" stylesheettheme="basic" %>

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
                if (id.endsWith("_Quantity1") || id.endsWith("_Quantity2")) {
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
                                <asp:Label ID="lb_PageTitle" runat="server"></asp:Label>
                            </h2>
                        </td>
                        <td align="left" style="color: #FF0000">
                            注:零售店的进货或销量提交操作，转移至"审批平台->进销存->进销数据审批"模块下的相应汇总表页面下统一提交!
                        </td>
                        <td align="right">
                            &nbsp;<asp:Button ID="bt_Save" runat="server" Text="暂 存" OnClick="bt_Save_Click"
                                Width="60px" />
                            <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="保存提交" Width="60px"  Visible="false"/>
                            <asp:Button ID="bt_ToForcast" runat="server" OnClick="bt_ToForcast_Click" Text="填报预估"
                                Width="60px" />
                            <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" OnClientClick="return confirm('是否确认删除该销量记录?')"
                                Text="删除" Width="60px" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" OnClientClick="return confirm('是否确认审核该销量记录？审核后的数据将不可再更改！')"
                                Text="审 核" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="dataLabel">
                                    <asp:Label ID="lb_SellOutTitle" runat="server" Text="供货客户"></asp:Label>
                                </td>
                                <td class="dataField">
                                    <asp:HyperLink ID="hy_SellOutClient" runat="server" CssClass="listViewTdLinkS1">[hy_SellOutClient]</asp:HyperLink>
                                    <asp:DropDownList ID="ddl_SellOutClient" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px;">
                                    <asp:Label ID="lb_SellInTitle" runat="server" Text="进货客户"></asp:Label>
                                </td>
                                <td class="dataField">
                                    <asp:HyperLink ID="hy_SellInClient" runat="server" CssClass="listViewTdLinkS1">[hy_SellInClient]</asp:HyperLink>
                                </td>
                                <td class="dataLabel">
                                    <asp:Label ID="lbl_Promotor" Text="导购员" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Promotor" DataValueField="ID" DataTextField="Name" runat="server"
                                        Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    发生日期
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_VolumeDate" Width="80px" runat="server" AutoPostBack="True"
                                        OnTextChanged="tbx_VolumeDate_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_VolumeDate"
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_VolumeDate"
                                        Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel">
                                    结算月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                        Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    进退货标志
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Flag" runat="server" DataTextField="Value" DataValueField="Key"
                                        OnSelectedIndexChanged="ddl_Flag_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    单据编号
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_sheetCode" runat="server" Width="180px"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    备注
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_Remark" runat="server" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
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
                                        OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged" RepeatDirection="Horizontal"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="color: #FF0000">
                                    系列
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID"
                                        RepeatDirection="Horizontal">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Search" runat="server" Text="查 找" Width="60px" OnClick="bt_Search_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                    <tr>
                        <td nowrap style="width: 100px" colspan="1">
                            <h3>
                                销量列表</h3>
                        </td>
                        <td align="right">
                            <asp:Label runat="server" ID="lbl_Notice" Style="color: Red">注:请折算为最小单位数量填报！</asp:Label>
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
                                                <asp:Label ID="Label6" runat="server" Text='袋/盒/罐'></asp:Label>
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
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,ClassifyName"
                                        Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging" PageSize="150" AllowPaging="true"
                                        OnDataBound="gv_List_DataBound" GridLines="Horizontal">
                                        <Columns>
                                            <asp:BoundField DataField="BrandName" HeaderText="产品品牌" SortExpression="BrandName"
                                                Visible="false" />
                                            <asp:BoundField DataField="ClassifyName" HeaderText="产品系列" SortExpression="ClassifyName" />
                                            <asp:BoundField DataField="Code" HeaderText="产品编码" SortExpression="Code" />
                                            <asp:BoundField DataField="ShortName" HeaderText="产品名称" SortExpression="ShortName">
                                                <ItemStyle Width="220px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Spec" HeaderText="规格" />
                                            <asp:BoundField DataField="FactoryPrice" HeaderText="出厂价" SortExpression="FactoryPrice" />
                                            <asp:TemplateField HeaderText="批发价" SortExpression="SalesPrice">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Price" runat="server" Text='<%# Bind("Price", "{0:0.##}") %>'
                                                        Enabled="false" Width="40px"></asp:TextBox><asp:CompareValidator ID="CompareValidator0"
                                                            runat="server" ControlToValidate="tbx_Price" Display="Dynamic" ErrorMessage="必须为数字"
                                                            Operator="DataTypeCheck" Type="Double"></asp:CompareValidator><asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator0" runat="server" ControlToValidate="tbx_Price" Display="Dynamic"
                                                                ErrorMessage="不能为空"></asp:RequiredFieldValidator></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="积分同步数量" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="tbx_SyncQuantity1" runat="server" Text='<%# ((int)DataBinder.Eval(Container.DataItem,"SyncQuantity")/(int)DataBinder.Eval(Container.DataItem,"ConvertFactor")).ToString() %>'
                                                        ></asp:Label>
                                                    <asp:Label ID="syncLabel5" runat="server" Text='<%# Bind("TrafficPackaging") %>'></asp:Label>
                                                    <asp:Label ID="tbx_SyncQuantity2" runat="server" Text='<%# ((int)DataBinder.Eval(Container.DataItem,"SyncQuantity")%(int)DataBinder.Eval(Container.DataItem,"ConvertFactor")).ToString() %>'
                                                        ></asp:Label>
                                                    <asp:Label ID="SyncLabel6" runat="server" Text='<%# Bind("Packaging") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="数量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Quantity1" runat="server" Text='<%# ((int)DataBinder.Eval(Container.DataItem,"Quantity")/(int)DataBinder.Eval(Container.DataItem,"ConvertFactor")).ToString() %>'
                                                        Width="40px"></asp:TextBox>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("TrafficPackaging") %>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Quantity1"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Quantity1"
                                                            Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="tbx_Quantity2" runat="server" Text='<%# ((int)DataBinder.Eval(Container.DataItem,"Quantity")%(int)DataBinder.Eval(Container.DataItem,"ConvertFactor")).ToString() %>'
                                                        Width="20px"></asp:TextBox>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Packaging") %>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_Quantity2"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Quantity2"
                                                            Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据</EmptyDataTemplate>
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
