<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_FeeApply_ApplySummary_GetRTChannelDisplayFee, App_Web_-oousccy" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        var a = new Array();
        function checkonclick(o, v) {
            if (o.checked)
                Array.add(a, v);
            else
                Array.remove(a, v);
            var s = "";
            for (i = 0; i < a.length; i++) {
                s += a[i] + ",";
            }
            document.getElementById('ctl00_ContentPlaceHolder1_tbx_SelectedApplyDetailIDs').value = s;
        }        
    </script>

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                陈列费用提报表
                            </h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="90px" />
                        </td>
                        <td align="right" nowrap="noWrap" width="300px">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="tbx_SelectedApplyDetailIDs" Style="visibility: hidden" runat="server"
                                        Width="2px"></asp:TextBox>
                                    <asp:Button ID="bt_Approve" runat="server" ForeColor="Blue" OnClick="bt_Approve_Click"
                                        OnClientClick="return confirm('是否确认将该条件范围内所有申请单批量设为审批通过？注意该操作并不是对勾选项进行审批！')"
                                        Text="批量审批通过" Width="90px" />
                                    <asp:Button ID="bt_UnApprove" runat="server" ForeColor="Red" OnClick="bt_UnApprove_Click"
                                        OnClientClick="return confirm('是否确认将该条件范围内所有申请单批量设为审批不通过？该操作进行后将不可撤销，请仔细确认！注意该操作并不是对勾选项进行审批！')"
                                        Text="批量审批不通过" Width="90px" />
                                    <asp:Button ID="bt_ExcludeApplyDetail" runat="server" Text="扣除选中费用" Width="90px"
                                        OnClientClick="return confirm('是否确认要扣除列表中打勾选中的费用?')" OnClick="bt_ExcludeApplyDetail_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                                查询条件</h3>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="dataLabel">
                            管理片区
                        </td>
                        <td class="dataField">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="220px" />
                        </td>
                        <td class="dataLabel">
                            客户渠道/类别</td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_RTChannel" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddl_RTType" runat="server" DataTextField="Value" DataValueField="Key">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            申请月份
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            &nbsp;预提状态</td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_Flag" runat="server">
                                <asp:ListItem Value="0">所有</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">仅当月</asp:ListItem>
                                <asp:ListItem Value="2">仅预提</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="dataLabel">
                            审批状态
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_State" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_State_SelectedIndexChanged">
                                <asp:ListItem Value="0">所有(含已提交及已通过)</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">待我审批的申请单</asp:ListItem>
                                <asp:ListItem Value="2">已审批通过的申请单</asp:ListItem>
                                <asp:ListItem Value="3">由我审批通过的申请单</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            会计科目
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_AccountTitle" runat="server" DataTextField="Name" DataValueField="ID"
                                Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="dataLabel">
                            我司单店陈列费用
                        </td>
                        <td class="dataField">
                            <asp:DropDownList ID="ddl_OP" runat="server">
                                <asp:ListItem Value="&gt;">大于</asp:ListItem>
                                <asp:ListItem Value="&lt;">小于</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="tbx_ApplyCost" runat="server" Width="60px">0</asp:TextBox>
                            元
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                Display="Dynamic" ErrorMessage="必需为数字格式" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </td>
                        <td class="dataLabel">
                            我司费率
                        </td>
                        <td class="dataField" nowrap="nowrap">
                            <asp:DropDownList ID="ddl_FeeRateOP" runat="server">
                                <asp:ListItem Value="&gt;">大于</asp:ListItem>
                                <asp:ListItem Value="&lt;">小于</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txt_FeeRate" runat="server" Width="40px" Text="0"></asp:TextBox>%
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_FeeRate"
                                Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_FeeRate"
                                Display="Dynamic" ErrorMessage="必需为数字格式" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                            <asp:Button ID="bt_MoreThan" runat="server" OnClick="bt_MoreThan_Click" Text="&gt;16%"
                                Width="40px" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="3"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="按卖场类型汇总" Value="0" />
                                                <mcs:MCSTabItem Text="按陈列方式汇总" Value="1" />
                                                <mcs:MCSTabItem Text="按付款周期汇总" Value="2" />
                                                <mcs:MCSTabItem Text="陈列费用明细" Value="3" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" OnDataBound="gv_List_DataBound"
                                            GridLines="Both" CellPadding="1" BackColor="#BBBBBB" CellSpacing="1" CssClass=""
                                            BorderWidth="0px" AllowPaging="true" PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            <Columns>
                                            </Columns>
                                            <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_State" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Approve" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_UnApprove" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_ExcludeApplyDetail" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 40;
        divGridView.style.height = window.screen.availHeight - 400;
    </script>

    <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick">
    </asp:Timer>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
