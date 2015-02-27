<%@ page language="C#" autoeventwireup="true" inherits="SubModule_FNA_FeeWriteoff_Pop_AddFeeWriteOffDetail, App_Web_lxhzl6y2" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑费用核销申请明细</title>
    <base target="_self" />

    <script type="text/javascript">
        function SelectAll(tempControl) {
            var theBox = tempControl;
            sState = theBox.checked;
            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++) {
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id) {
                    if (elem[i].checked != sState) {
                        elem[i].click();
                    }
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="96%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                                <tr>
                                    <td width="24">
                                        <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                    </td>
                                    <td nowrap="noWrap">
                                        <h2>
                                            编辑费用核销申请明细
                                        </h2>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存并返回" Width="80px" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr height="28px">
                                    <td nowrap>
                                        <h3>
                                            查询可核销的费用申请列表</h3>
                                    </td>
                                    <td align="right" nowrap>
                                        <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="70px" />
                                        <asp:Button ID="bt_AddToWriteOffList" runat="server" OnClick="bt_AddToWriteOffList_Click"
                                            Text="加入核销" Width="70px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tabForm">
                                <tr>
                                    <td class="dataLabel">
                                        &nbsp;备案号
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_SheetCode" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="dataLabel" nowrap="nowrap">
                                        发生开始月份
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataLabel">
                                        发生截止月份
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dataLabel">
                                        发生客户
                                    </td>
                                    <td class="dataField">
                                        <mcs:MCSSelectControl ID="select_Client" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=3"
                                            Width="200px" />
                                        &nbsp;
                                    </td>
                                    <td class="dataLabel">
                                        费用类型
                                    </td>
                                    <td class="dataField">
                                        <asp:DropDownList ID="ddl_FeeType" runat="server" DataTextField="Value" AutoPostBack="true"
                                            DataValueField="Key" OnSelectedIndexChanged="ddl_FeeType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dataLabel">
                                        会计科目(含有)
                                    </td>
                                    <td class="dataField" colspan="3">
                                        <asp:DropDownList ID="ddl_AccountTitle" runat="server" DataTextField="Name" DataValueField="ID"
                                            Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="1px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_FeeAplyList" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowSorting="true" DataKeyNames="FNA_FeeApply_ID,FNA_FeeApplyDetail_ID" AllowPaging="True"
                                PanelCode="Panel_FNA_FeeApplyList_NoWriteOff">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="FNA_FeeApply_ID" HeaderText="申请备案号" DataNavigateUrlFormatString="../FeeApply/FeeApplyDetail3.aspx?ID={0}"
                                        DataTextField="FNA_FeeApply_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                                        SortExpression="FNA_FeeApply_SheetCode">
                                        <ControlStyle CssClass="listViewTdLinkS1" />
                                    </asp:HyperLinkField>
                                    <asp:TemplateField HeaderText="批复金额">
                                        <ItemTemplate>
                                            <asp:Label ID="lb_CanWriteOffCost" ForeColor="Red" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.FNA_FeeApplyDetail_ApplyCost")+(decimal)DataBinder.Eval(Container,"DataItem.FNA_FeeApplyDetail_AdjustCost")).ToString("0.###元")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="核销">
                                        <HeaderTemplate>
                                            核销<asp:CheckBox ID="chkHeader" runat="server" ToolTip="全选" onclick="javascript:SelectAll(this);">
                                            </asp:CheckBox>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cb_Selected" runat="server" OnCheckedChanged="cb_Selected_CheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据</EmptyDataTemplate>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr height="28px">
                                    <td nowrap>
                                        <h3>
                                            报销单科目明细</h3>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <font color="blue">注:请仔细核对【实际核销金额】必须与实际发票金额完全一致，否则会被退票。</font>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="ID,ApplyDetailID" OnRowDeleting="gv_List_RowDeleting" OnRowDataBound="gv_List_RowDataBound"
                                Binded="False" ConditionString="" OrderFields="" PanelCode="" TotalRecordCount="0">
                                <Columns>
                                    <asp:TemplateField HeaderText="申请单备案号" SortExpression="ApplyDetailID">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hy_ApplySheetCode" runat="server" Text="" NavigateUrl="" ForeColor="#CC0000"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="发生客户(门店或经销商)" SortExpression="Client">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hy_Client" runat="server" Text="" NavigateUrl="" ForeColor="#CC0000"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AccountTitle" HeaderText="科目" SortExpression="AccountTitle" />
                                    <asp:BoundField DataField="ApplyCost" DataFormatString="{0:0.###元}" HeaderText="可核销金额"
                                        HtmlEncode="False" SortExpression="ApplyCost" />
                                    <asp:TemplateField HeaderText="实际核销金额" SortExpression="WriteOffCost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_WriteOffCost" Width="50px" runat="server" Text='<%# Bind("WriteOffCost","{0:0.###}") %>'
                                                AutoPostBack="True" OnTextChanged="tbx_WriteOffCost_TextChanged"></asp:TextBox>
                                            元<span style="color: #FF0000">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_WriteOffCost"
                                                Display="Dynamic" ErrorMessage="必填" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_WriteOffCost"
                                                Display="Dynamic" ErrorMessage="必需为数字" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="2"></asp:CompareValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BeginMonth" HeaderText="发生月" />
                                    <asp:TemplateField HeaderText="核销日期范围" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_BeginDate" runat="server" Width="70px" onfocus="setday(this)"
                                                Text='<%# Bind("BeginDate","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                            <span style="color: #FF0000;">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_BeginDate"
                                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator><asp:CompareValidator
                                                    ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对" Display="Dynamic"
                                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_BeginDate"></asp:CompareValidator>至
                                            <asp:TextBox ID="tbx_EndDate" runat="server" Width="70px" onfocus="setday(this)"
                                                Text='<%# Bind("EndDate","{0:yyyy-MM-dd}")%>'></asp:TextBox>
                                            <span style="color: #FF0000;">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_EndDate"
                                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="日期格式不对"
                                                Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_EndDate"></asp:CompareValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="结余方式" Visible="false">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddl_BalanceMode" runat="server" DataTextField="Value" DataValueField="Key"
                                                DataSource="<%# GetBalanceMode() %>" Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注" SortExpression="Remark">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark" Width="150px" runat="server" Text='<%# Bind("Remark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" DeleteText="删除" ControlStyle-CssClass="listViewTdLinkS1">
                                        <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                    </asp:CommandField>
                                </Columns>
                            </mcs:UC_GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="28">
                            合计核销费用：<asp:Label ID="lb_TotalCost" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>元
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
