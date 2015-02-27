<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_PromotorSummary, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                导购员工资单审批汇总单信息
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="60px" />
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
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                                    <asp:Button runat="server" ID="bt_Approved" Text="审批通过" Width="70px" OnClientClick="return confirm('是否确认将该区域下所有工资单审批通过?')"
                                        OnClick="bt_Approved_Click" />
                                    <asp:Button runat="server" ID="bt_UnApproved" Text="审批不通过" Width="70px" OnClientClick="return confirm('是否确认将该区域下所有工资单全部设为审批不通过?')"
                                        OnClick="bt_UnApproved_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    查看层级
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Level" runat="server" DataValueField="Key" DataTextField="Value">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    申请月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    工资类型
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_SalaryClassify" runat="server" DataValueField="Key" DataTextField="Value">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                        <asp:ListItem Value="0">所有(含已提交及已通过)</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">待我审批的申请单</asp:ListItem>
                                        <asp:ListItem Value="2">已审批通过的申请单</asp:ListItem>
                                        <asp:ListItem Value="3">由我审批通过的申请单</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    费率
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
                                </td>
                                <td class="dataLabel">
                                    门店渠道
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RTChannel" DataTextField="Value" DataValueField="Key" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="薪资统计分析" Value="0" />
                                                <mcs:MCSTabItem Text="薪资合计汇总" Value="1" />
                                                <mcs:MCSTabItem Text="查看薪资明细" Value="2" />
                                                <mcs:MCSTabItem Text="导购薪资明细" Value="3" />
                                                <mcs:MCSTabItem Text="导购投产明细" Value="4" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" DataKeyNames="OrganizeCity"
                                            AllowPaging="True" PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging"
                                            BorderWidth="0px" GridLines="Both" CellPadding="1" BackColor="#CCCCCC" CellSpacing="1"
                                            CssClass="" TotalRecordCount="0" OnDataBound="gv_List_DataBound">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" Height="28px" />
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                            PanelCode="Panel_PM_Salary_List001" DataKeyNames="PM_Salary_ID" AllowPaging="True"
                                            PageSize="15" Visible="false">
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="PM_Salary_ID" DataNavigateUrlFormatString="PM_SalaryDetailList.aspx?ID={0}"
                                                    ControlStyle-CssClass="listViewTdLinkS1" Text="查看" HeaderText=" ">
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField HeaderText="工资总额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.Promotor.PM_SalaryBLL.GetSumSalary(int.Parse(DataBinder.Eval(Container,"DataItem.PM_Salary_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("PM_Salary_TaskID", "../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                            Text="审批记录" Visible='<%# Eval("PM_Salary_TaskID").ToString()!="" %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_PromotorSalary" runat="server" Width="100%" AllowPaging="True"
                                            PageSize="50" BorderWidth="0px" GridLines="Both" CellPadding="1" BackColor="#CCCCCC"
                                            CellSpacing="1" CssClass="" TotalRecordCount="0" DataKeyNames="SalaryDetailID"
                                            OnDataBound="gv_PromotorSalary_DataBound" OnPageIndexChanging="gv_PromotorSalary_PageIndexChanging">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            <RowStyle BackColor="#FFFFFF" CssClass="" Height="28px" />
                                            <Columns>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Approved" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_UnApproved" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 55;
        divGridView.style.height = window.screen.availHeight - 400;            
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
