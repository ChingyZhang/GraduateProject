<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt_DataSetFieldsDetail.aspx.cs" Inherits="SubModule_Reports_Rpt_DataSetFieldsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="报表数据集字段详细信息"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" OnClientClick="return confirm('是否确认删除该字段?')"
                                Text="删 除" Width="60px" />
                            <asp:Button ID="bt_Expand" Visible="false" runat="server" Text="展开层级" 
                                Width="60px" onclick="bt_Expand_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            字段名称
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_FieldName" Width="180px" runat="server"></asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ControlToValidate="tbx_FieldName" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                        </td>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            显示名称
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_DisplayName" Width="120px" runat="server"></asp:TextBox>
                            <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server" ControlToValidate="tbx_DisplayName" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            是否计算列
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_IsComputeField" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_IsComputeField_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="N" Text="否"></asp:ListItem>
                                <asp:ListItem Value="Y" Text="是"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            计算列表达式
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_Expression" Width="350px" runat="server" Enabled="false"></asp:TextBox><br />
                            <asp:HyperLink ID="hy_ExpressionHelp" Target="_blank" runat="server" ForeColor="Red"
                                CssClass="listViewTdLinkS1">表达式帮助</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            数据类型
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_DataType" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            顺序编号
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_SortID" Width="60px" runat="server"></asp:TextBox>
                            <span style="font-size: 11pt; color: #ff0000">*</span><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_SortID" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_SortID"
                                Display="Dynamic" ErrorMessage="必需为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 100px; height: 22px;">
                            显示方式
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_DisplayMode" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel" style="width: 100px; height: 22px;">
                            树表层次
                        </td>
                        <td class="dataField">
                            <asp:TextBox ID="tbx_TreeLevel" Width="50px" runat="server" ToolTip="0为显示本级"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_TreeLevel"
                                Display="Dynamic" ErrorMessage="必需为整数" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel" style="width: 120px; height: 30px;">
                            描述
                        </td>
                        <td class="dataField" colspan="3">
                            <asp:TextBox ID="tbx_Description" Width="350px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
