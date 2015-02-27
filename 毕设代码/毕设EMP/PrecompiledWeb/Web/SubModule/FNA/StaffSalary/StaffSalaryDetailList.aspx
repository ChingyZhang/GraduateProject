<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_StaffSalary_StaffSalaryDetailList, App_Web_llmqckq0" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                员工绩效列表</h2>
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" id="tbl_BudgetInfo"
                                visible="false">
                                <tr>
                                    <td class="dataLabel" width="80px">
                                        预算分配总额
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_BudgetAmount" runat="server" ForeColor="Red"></asp:Label>
                                        元
                                    </td>
                                    <td class="dataLabel" width="80px">
                                        剩余可用额度
                                    </td>
                                    <td class="dataField">
                                        <asp:Label ID="lb_BudgetBalance" runat="server" ForeColor="Red"></asp:Label>
                                        元
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Export" runat="server" Text="导出Excel" OnClick="bt_Export_Click"
                                Width="70px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" OnClick="bt_Save_Click"
                    ValidationGroup="1" />
                <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" OnClientClick="return confirm(&quot;删除后数据将不可恢复，是否确认删除工资数据?&quot;)"
                    Text="删除" Width="60px" />
                <asp:Button ID="bt_Submit" runat="server" Text="提 交" Width="60px" ValidationGroup="1"
                    OnClick="bt_Submit_Click" OnClientClick="return confirm(&quot;提交后工资信息将不可再修改，是否确认提交?&quot;)" />
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_DetailView ID="pn_detail" runat="server" DetailViewCode="Page_Staff_SalaryDetail">
                </mcs:UC_DetailView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    管理片区
                                </td>
                                <td align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="160px" />
                                </td>
                                <td class="dataLabel">
                                    营销人员
                                </td>
                                <td>
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="../../StaffManage/Pop_Search_Staff.aspx"
                                        Width="260px" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Search" runat="server" Text="查   询" Width="60px" OnClick="bt_Search_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="28px">
                            <h3>
                                工资明细列表</h3>
                        </td>
                        <td align="right" height="28">
                            合计奖金总额：<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red"></asp:Label>
                            元
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_SaveChange" runat="server" Text="保存调整" Visible="false" OnClick="bt_SaveChange_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" DataKeyNames="FNA_StaffSalaryDetail_ID"
                                AllowPaging="True" PageSize="25" BorderWidth="0px" GridLines="Both" CellPadding="1"
                                OrderFields="Org_Staff_OrganizeCity2,Org_Staff_OrganizeCity3,Org_Staff_OrganizeCity4"
                                Visible="false" BackColor="#CCCCCC" CellSpacing="1" CssClass="" AutoGenerateColumns="False"
                                TotalRecordCount="0" PanelCode="Panel_FNA_StaffSalary_Detail001">
                                <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                <RowStyle BackColor="#FFFFFF" CssClass="" Height="28px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="人员基本信息→姓名" SortExpression="Org_Staff_RealName">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hy_StaffName" runat="server" NavigateUrl='<%# "~/SubModule/StaffManage/StaffDetail.aspx?ID="+DataBinder.Eval(Container,"DataItem.Org_Staff_ID").ToString() %>'
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Org_Staff_RealName") %>' ToolTip="查看该员工详细资料"
                                                CssClass="listViewTdLinkS1"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="合计→奖金加项" SortExpression="FNA_StaffSalaryDetail_BonusAdd">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_BonusAdd" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_BonusAdd","{0:0.##}") %>'
                                                Width="50px"></asp:TextBox><asp:CompareValidator ID="CompareValidator51" runat="server"
                                                    ControlToValidate="tbx_BonusAdd" Display="Dynamic" ErrorMessage="必须是数字" Operator="DataTypeCheck"
                                                    Type="Double" ValidationGroup="1"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="合计→奖金减项" SortExpression="FNA_StaffSalaryDetail_Bounsdeduction">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Bounsdeduction" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_Bounsdeduction") %>'
                                                Width="50px"></asp:TextBox><asp:CompareValidator ID="CompareValidator13" runat="server"
                                                    ControlToValidate="tbx_Bounsdeduction" Display="Dynamic" ErrorMessage="必须是数字"
                                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="1"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_Remark") %>'
                                                Width="100px"></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <%--   <asp:TemplateField HeaderText="审批调整" Visible="false" SortExpression="FNA_StaffSalaryDetail_PayAdjust_Approve">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PayAdjust_Approve" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_PayAdjust_Approve") %>'
                                                        Width="50px"></asp:TextBox><asp:CompareValidator ID="CompareValidator14" runat="server"
                                                            ControlToValidate="tbx_PayAdjust_Approve" Display="Dynamic" ErrorMessage="必须是数字"
                                                            Operator="DataTypeCheck" Type="Double" ValidationGroup="1"></asp:CompareValidator></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="调整原因" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PayAdjust_Reason" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_PayAdjust_Reason") %>'
                                                        Width="100px"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>--%>
                                </Columns>
                            </mcs:UC_GridView>
                            <mcs:UC_GridView ID="gv_list2" runat="server" Width="100%" DataKeyNames="FNA_StaffSalaryDetail_ID"
                                AllowPaging="True" PageSize="25" BorderWidth="0px" GridLines="Both" CellPadding="1"
                                OrderFields="Org_Staff_OrganizeCity2,Org_Staff_OrganizeCity3,Org_Staff_OrganizeCity4"
                                Visible="false" BackColor="#CCCCCC" CellSpacing="1" CssClass="" AutoGenerateColumns="False"
                                TotalRecordCount="0" PanelCode="Panel_FNA_StaffSalary_Detail002">
                                <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                <RowStyle BackColor="#FFFFFF" CssClass="" Height="28px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="人员基本信息→姓名" SortExpression="Org_Staff_RealName">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hy_StaffName" runat="server" NavigateUrl='<%# "~/SubModule/StaffManage/StaffDetail.aspx?ID="+DataBinder.Eval(Container,"DataItem.Org_Staff_ID").ToString() %>'
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Org_Staff_RealName") %>' ToolTip="查看该员工详细资料"
                                                CssClass="listViewTdLinkS1"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="合计→奖金加项" SortExpression="FNA_StaffSalaryDetail_BonusAdd">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_BonusAdd" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_BonusAdd","{0:0.##}") %>'
                                                Width="50px"></asp:TextBox><asp:CompareValidator ID="CompareValidator51" runat="server"
                                                    ControlToValidate="tbx_BonusAdd" Display="Dynamic" ErrorMessage="必须是数字" Operator="DataTypeCheck"
                                                    Type="Double" ValidationGroup="1"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="合计→奖金减项" SortExpression="FNA_StaffSalaryDetail_Bounsdeduction">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Bounsdeduction" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_Bounsdeduction") %>'
                                                Width="50px"></asp:TextBox><asp:CompareValidator ID="CompareValidator13" runat="server"
                                                    ControlToValidate="tbx_Bounsdeduction" Display="Dynamic" ErrorMessage="必须是数字"
                                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="1"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_Remark") %>'
                                                Width="100px"></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <%--   <asp:TemplateField HeaderText="审批调整" Visible="false" SortExpression="FNA_StaffSalaryDetail_PayAdjust_Approve">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PayAdjust_Approve" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_PayAdjust_Approve") %>'
                                                        Width="50px"></asp:TextBox><asp:CompareValidator ID="CompareValidator14" runat="server"
                                                            ControlToValidate="tbx_PayAdjust_Approve" Display="Dynamic" ErrorMessage="必须是数字"
                                                            Operator="DataTypeCheck" Type="Double" ValidationGroup="1"></asp:CompareValidator></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="调整原因" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PayAdjust_Reason" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FNA_StaffSalaryDetail_PayAdjust_Reason") %>'
                                                        Width="100px"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>--%>
                                </Columns>
                            </mcs:UC_GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
