<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_UDM_PanelDetail, App_Web_bl88rr1i" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        板块列表
                                    </h2>
                                </td>
                                <td align="left">
                                    &nbsp; ID:
                                    <asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Text="保 存" OnClick="bt_OK_Click" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="详细信息维护" Description=""
                                    Value="0" Enable="True" Visible="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="包含数据表维护" Description=""
                                    Value="1" Enable="True" Visible="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="数据表关系维护" Description=""
                                    Value="2" Enable="True" Visible="True"></mcs:MCSTabItem>
                                <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="模块字段维护" Description=""
                                    Value="3" Enable="True" Visible="True"></mcs:MCSTabItem>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    &nbsp;板块代码</td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Code" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    显示类型
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_DisplayType" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rbl_DisplayType_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="1">详细信息</asp:ListItem>
                                        <asp:ListItem Value="2">列表信息</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    所属详细视图
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DetailView" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    &nbsp;板块名称</td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Name" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    可见标志
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_Enable" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="Y">可见</asp:ListItem>
                                        <asp:ListItem Value="N">不可见</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    用于高级查询
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_AdvanceFind" runat="server" Enabled="False" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y">是</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="N">否</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    &nbsp;数据库连接
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_DBConnectString" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    &nbsp;默认排序字段
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_DefaultSortFields" runat="server" Width="180px"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    每行列数
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_FieldCount" runat="server" Width="60px">3</asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_FieldCount"
                                        Display="Dynamic" ErrorMessage="格式为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    描述</td>
                                <td class="dataField" colspan="1">
                                    <asp:TextBox ID="tbx_Description" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    &nbsp;显示顺序
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_SortID" runat="server" Width="60px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_SortID"
                                        Display="Dynamic" ErrorMessage="格式为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel" style="width: 80px; height: 30px;">
                                    表格样式
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_TableStyle" runat="server">
                                        <asp:ListItem Selected="True">请选择...</asp:ListItem>
                                        <asp:ListItem Value="tabForm,dataLabel,dataField">编辑风格</asp:ListItem>
                                        <asp:ListItem Value="tabDetailView,tabDetailViewDL,tabDetailViewDF">显示风格</asp:ListItem>
                                    </asp:DropDownList>
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
