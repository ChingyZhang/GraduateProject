<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_TransferVolumeDetail, App_Web_yabmfp6z" enableEventValidation="false" stylesheettheme="basic" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server">经销商库存调拨记录</asp:Label>
                            </h2>
                        </td>
                        <td align="right">
                            &nbsp;<asp:Button ID="bt_Save" runat="server" Text="保 存" OnClick="bt_Save_Click"
                                Width="60px" UseSubmitBehavior="False" />
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
                                    调出客户
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl runat="server" ID="select_Supplier" Width="200px" />
                                </td>
                                <td class="dataLabel" style="width: 120px;">
                                    调入客户
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" Width="200px" />
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
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    备注
                                </td>
                                <td class="dataField" colspan="5">
                                    <asp:TextBox ID="tbx_Remark" runat="server" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td nowrap style="width: 100px" colspan="1">
                                    <h3>
                                        销量列表</h3>
                                </td>
                                <td>
                                    <asp:Label ID="lb_Msg" ForeColor="Red" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="dataLabel" style="color: #FF0000">
                                                <asp:CheckBox ID="cb_OnlyDisplayUnZero" runat="server" AutoPostBack="True" OnCheckedChanged="cb_OnlyDisplayUnZero_CheckedChanged"
                                                    Text="仅显示数量不为零的产品" />
                                            </td>
                                            <td class="dataLabel" style="color: #FF0000">
                                                产品编号
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_ProductCode" Width="60px" runat="server"></asp:TextBox>
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
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                        Width="100%" OnPageIndexChanging="gv_List_PageIndexChanging" PageSize="25">
                                        <Columns>
                                            <asp:BoundField DataField="BrandName" HeaderText="产品品牌" SortExpression="BrandName"
                                                Visible="false" />
                                            <asp:BoundField DataField="ClassifyName" HeaderText="产品系列" SortExpression="ClassifyName" />
                                            <asp:BoundField DataField="Code" HeaderText="产品编码" SortExpression="Code" />
                                            <asp:BoundField DataField="ShortName" HeaderText="产品简称" SortExpression="ShortName">
                                                <ItemStyle Width="220px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FactoryPrice" HeaderText="出厂价" SortExpression="FactoryPrice" />
                                            <asp:TemplateField HeaderText="批发价" SortExpression="SalesPrice" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Price" runat="server" Text='<%# Bind("Price", "{0:0.##}") %>'
                                                        Width="40px"></asp:TextBox><asp:CompareValidator ID="CompareValidator0" runat="server"
                                                            ControlToValidate="tbx_Price" Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck"
                                                            Type="Double"></asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator0"
                                                                runat="server" ControlToValidate="tbx_Price" Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator></ItemTemplate>
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
</asp:Content>
