<%@ page language="C#" autoeventwireup="true" inherits="SubModule_FNA_FeeApply_Pop_AddFeeApplyDetailItem, App_Web_5zp237uh" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增费用申请明细</title>
    <base target="_self">
    </base>
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                    </td>
                                    <td nowrap="noWrap">
                                        <h2>
                                            新增费用申请明细
                                        </h2>
                                    </td>
                                    <td align="right" nowrap="noWrap">
                                        <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存并返回" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table id="tb_Client" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="dataLabel" height="26">
                                        费用发生客户
                                    </td>
                                    <td class="dataField">
                                        <mcs:MCSSelectControl runat="server" ID="select_Client" SelectValue='<%#  Bind("Client") %>'
                                            Width="260px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx" OnSelectChange="select_Client_SelectChange" />
                                    </td>
                                    <td class="dataLabel">
                                        客户联系人
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_LinkMan" runat="server">
                                            <asp:ListItem Text="请选择..." Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataLabel">
                                        预计销量
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_SalesForcast" runat="server" Width="60px" Text="0"></asp:TextBox>元<asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填" ControlToValidate="tbx_SalesForcast"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="必须为数字"
                                            ControlToValidate="tbx_SalesForcast" Display="Dynamic" Operator="DataTypeCheck"
                                            Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dataLabel" height="26">
                                        上月实际销量
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_PreSalesVolume" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                        历史平均销量
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_AvgSalesVolume" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                    </td>
                                    <td class="dataField">
                                    </td>
                                </tr>
                            </table>
                            <table id="tb_Car" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="dataLabel" height="26" width="80px">
                                        费用发生车辆
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_RelateCar" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="dataLabel" height="26">
                                        关联品牌
                                    </td>
                                    <td class="dataField" colspan="3">
                                        <asp:CheckBox ID="cbx_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged"
                                            Text="全选" />
                                        <asp:CheckBoxList ID="cbl_Brand" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td class="dataLabel">
                                        最迟核销月
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_LastWriteOffMonth" runat="server" DataTextField="Name"
                                            DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="ID" OnRowDeleting="gv_List_RowDeleting" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                Binded="False" ConditionString="" OrderFields="" PanelCode="" TotalRecordCount="0">
                                <Columns>
                                    <asp:TemplateField HeaderText="科目">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddl_AccountTitle" runat="server" DataTextField="Name" DataValueField="ID"
                                                SelectedValue='<%#  Bind("AccountTitle") %>' DataSource='<%# GetAccountTitleList() %>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请金额<br/>(我司承担)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_ApplyCost" runat="server" Text='<%#  Bind("ApplyCost") %>' Width="60px"></asp:TextBox>
                                            元
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请金额<br/>(经销商承担)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_DICost" runat="server" Text='<%#  Bind("DICost") %>' Width="60px"></asp:TextBox>
                                            元
                                            <asp:CompareValidator ID="CompareValidator_DICost" runat="server" ControlToValidate="tbx_DICost"
                                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="费用发生范围" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_BeginDate" runat="server" onfocus="setday(this)" Width="70px"
                                                Text='<%# Bind("BeginDate","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                            <span class="style1">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_BeginDate"
                                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator><asp:CompareValidator
                                                    ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_BeginDate"></asp:CompareValidator>至
                                            <asp:TextBox ID="tbx_EndDate" runat="server" onfocus="setday(this)" Width="70px"
                                                Text='<%# Bind("EndDate","{0:yyyy-MM-dd}")%>'></asp:TextBox>
                                            <span class="style1">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_EndDate"
                                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator><asp:CompareValidator
                                                    ID="CompareValidator3" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_EndDate"></asp:CompareValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="说明(必填)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark" runat="server" Width="320px" TextMode="SingleLine" Text='<%#  Bind("Remark") %>'></asp:TextBox>
                                            <span class="style1">*</span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                        <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                    </asp:CommandField>
                                </Columns>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="28">
                            合计费用：<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red"></asp:Label>
                            元
                            <asp:Button ID="bt_AddDetail" runat="server" Text="新增空行" Width="80px" ValidationGroup="1"
                                OnClick="bt_AddDetail_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
