<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_UDM_ModelFieldDetail, App_Web_bl88rr1i" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="T_PageContent">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        数据表字段详细信息</h2>
                                </td>
                                <td align="left">
                                    所属表名:<asp:Label ID="lbl_TableName" runat="server"></asp:Label>
                                    &nbsp;ID:<asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
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
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    字段名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_FieldName" Width="120px" runat="server"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_FieldName" 
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    显示名称
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_DisplayName" Width="120px" runat="server"></asp:TextBox>
                                    <span style="color: #FF0000">*</span><asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_DisplayName" 
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    数据类型
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_DataType" runat="server" DataTextField="Value" DataValueField="Key"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_DataType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    数据长度
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Length" Width="60px" runat="server" Enabled="False"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lb_Precision" runat="server" Text="精度"></asp:Label>
                                    <asp:TextBox ID="tbx_Precision" Width="60px" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    默认值
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_DefaultValue" Width="120px" runat="server"></asp:TextBox>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    最后更新日期
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lbl_LastUpdateTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    描述
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_Description" Width="350px" runat="server"></asp:TextBox>
                                </td>
                                <td class="dataLabel">
                                    查询页链接
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_SearchPageURL" runat="server" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    关联类型
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_RelationType" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rbl_RelationType_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="1">字典</asp:ListItem>
                                        <asp:ListItem Value="2">表</asp:ListItem>
                                        <asp:ListItem Value="3" Selected="True">不关联</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="tr_1" runat="server" visible="false">
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    关联表名
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RelationTableName" runat="server" DataTextField="TableName"
                                        DataValueField="TableName" OnSelectedIndexChanged="ddl_RelationTableName_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="tr_2" runat="server" visible="false">
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    关联表值字段
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RelationValueField" runat="server" DataTextField="DisplayName"
                                        DataValueField="FieldName">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 120px; height: 30px;">
                                    关联表文本字段
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RelationTextField" runat="server" DataTextField="DisplayName"
                                        DataValueField="FieldName">
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
