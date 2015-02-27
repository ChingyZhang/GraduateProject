<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_UDM_FieldsInPanelDetail, App_Web_bl88rr1i" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
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
                                        页面布局字段设定
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Text="保 存" OnClick="bt_OK_Click" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    ID
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    数据表及字段
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_TableName" runat="server" DataTextField="DisplayName" DataValueField="TableID"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_TableName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddl_FieldID" runat="server" DataTextField="DisplayName" DataValueField="ID"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_FieldID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;" nowrap="nowrap">
                                    顺序编号
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_SortID" Width="60px" runat="server"></asp:TextBox>
                                    <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_SortID" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_SortID"
                                        Display="Dynamic" ErrorMessage="必需为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    显示方式
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DisplayMode" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    描述
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Description" Width="400px" runat="server"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    显示名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_LabelText" Width="120px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    可见标志
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_Visible" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="Y">可见</asp:ListItem>
                                        <asp:ListItem Value="N">不可见</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    可编辑标志
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_Enable" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="Y">可用</asp:ListItem>
                                        <asp:ListItem Value="N">不可用</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    可见权限号
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_VisibleActionCode" Width="120px" runat="server"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    可编辑权限号
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_EnableActionCode" Width="120px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    格式字符串
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_FormatString" runat="server" Width="400px"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;" nowrap="nowrap">
                                    树表层次
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_TreeLevel" Width="50px" runat="server" ToolTip="0为显示本级"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_TreeLevel"
                                        Display="Dynamic" ErrorMessage="必需为整数" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr id="tr_detail" runat="server">
                    <td class="tabForm">
                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    控件类型
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ControlType" runat="server" DataTextField="Value" DataValueField="Key"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_ControlType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    是否只读
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_ReadOnly" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y">是</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="N">否</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    是否必填
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_IsRequireField" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y">是</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="N">否</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    控件宽度
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_ControlWidth" Width="120px" runat="server" Enabled="False"></asp:TextBox>
                                    px
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    控件高度
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_ControlHeight" Width="120px" runat="server" Enabled="False"></asp:TextBox>
                                    px
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    控件样式
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_ControlStyle" Width="120px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    所跨列数
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_ColSpan" Width="120px" runat="server"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    正则表达式
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_RegularExpression" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 22px;">
                                    查询页面URL
                                </td>
                                <td class="dataField" colspan="5">
                                    <asp:TextBox ID="tbx_SearchPageURL" runat="server" Enabled="False" Width="500px"></asp:TextBox>
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
