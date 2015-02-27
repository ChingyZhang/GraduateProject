<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pop_AddAccountTitleNoApply.aspx.cs"
    Inherits="SubModule_FNA_FeeWriteoff_Pop_AddAccountTitleNoApply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增实报实销科目</title>
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
                                            新增费用报销科目
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
                        <td>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="ID" OnRowDeleting="gv_List_RowDeleting" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                                OnRowDataBound="gv_List_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="科目">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddl_AccountTitle" runat="server" DataTextField="Name" DataValueField="ID"
                                                SelectedValue='<%#  Bind("AccountTitle") %>' DataSource='<%# GetAccountTitleList() %>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="发生月">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                                SelectedValue='<%#  Bind("BeginMonth") %>' DataSource='<%# GetAccountMonth() %>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请核销金额">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_WriteOffCost" runat="server" Text='<%#  Bind("WriteOffCost") %>'
                                                Width="50px"></asp:TextBox>
                                            元
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_WriteOffCost"
                                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
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
                                    <asp:BoundField DataField="ID" Visible="false" />
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
                <hr />
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tb_TeleFee" runat="server"
                    visible="false">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="h3Row" width="100%">
                                <tr>
                                    <td nowrap="noWrap">
                                        <h3>
                                            报销电话费
                                        </h3>
                                    </td>
                                    <td align="right" nowrap="noWrap">
                                        <asp:Button ID="bt_AddTeleFee" runat="server" Text="加入报销" Width="80px" ValidationGroup="tele"
                                            OnClick="bt_AddTeleFee_Click" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="dataLabel">
                                        电话号码
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_Tele" runat="server" AutoPostBack="True" DataTextField="TeleNumber"
                                            DataValueField="ID" OnSelectedIndexChanged="ddl_Tele_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataLabel">
                                        报销限额
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_TeleApplyCost" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td class="dataLabel">
                                        &nbsp;限额说明
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_TeleApplyInfo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dataLabel">
                                        报销金额
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_TeleCost" runat="server" Width="60px"></asp:TextBox>
                                        元<span class="style1">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                            runat="server" ControlToValidate="tbx_TeleCost" Display="Dynamic" ErrorMessage="必填"
                                            ValidationGroup="tele"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_TeleCost"
                                            Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"
                                            ValidationGroup="tele"></asp:CompareValidator>
                                    </td>
                                    <td class="dataLabel">
                                        月份
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_TeleCostMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataLabel">
                                        说明
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_TeleRemark" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <hr />
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tb_MobileFee"
                    runat="server" visible="false">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="h3Row" width="100%">
                                <tr>
                                    <td nowrap="noWrap">
                                        <h3>
                                            报销手机费
                                        </h3>
                                    </td>
                                    <td align="right" nowrap="noWrap">
                                        <asp:Button ID="bt_AddMobileFee" runat="server" Text="加入报销" Width="80px" ValidationGroup="Mobile"
                                            OnClick="bt_AddMobileFee_Click" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="dataLabel">
                                        员工
                                    </td>
                                    <td class="dataField">
                                        <mcs:MCSSelectControl ID="select_MobileStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                            Width="120" OnSelectChange="select_MobileStaff_SelectChange" />
                                    </td>
                                    <td class="dataLabel">
                                        手机号码
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_MobileNumber" runat="server"></asp:Label>
                                    </td>
                                    <td class="dataLabel">
                                        报销限额
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_MobileApplyCost" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dataLabel">
                                        报销金额
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_MobileCost" runat="server" Width="60px"></asp:TextBox>
                                        元<span class="style1">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="tbx_MobileCost" Display="Dynamic" ErrorMessage="必填"
                                            ValidationGroup="Mobile"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_MobileCost"
                                            Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"
                                            ValidationGroup="Mobile"></asp:CompareValidator>
                                    </td>
                                    <td class="dataLabel">
                                        月份
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_MobileCostMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataLabel">
                                        说明
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_MobileRemark" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
