<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="BudgetSource.aspx.cs" Inherits="SubModule_FNA_Budget_BudgetSource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="table1" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                                </td>
                                <td nowrap="noWrap">
                                    <h2>
                                        各办事处可用预算
                                    </h2>
                                </td>
                                <td style="width: 60px" class="dataLabel">
                                    管理片区
                                </td>
                                <td align="left" style="width: 200px">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="220px" />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_AccountMonth" DataValueField="ID" DataTextField="Name"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataField" align="right">
                                    <asp:Button ID="btnFind" runat="server" Text="查看" OnClick="btnFind_Click" Width="60px" />
                                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" Width="60px" />
                                    <asp:Button ID="btnApprove" runat="server" Text="审核" OnClick="btnApprove_Click" Width="60px"
                                        OnClientClick="return confirm('是否确认将选中的记录审核通过?')" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="table3" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="15" DataKeyNames="ID,OrganizeCity,AccountMonth,BaseBudget,OverFullBudget"
                                        OnPageIndexChanging="gv_List_PageIndexChanging" OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Visible='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="bt_OpenSampleOfferConfirmResult" runat="server" CausesValidation="False"
                                                        CommandName="Select" Text="分配预算" OnClientClick='<%# "javascript:OpenBudgetAssign(" + Eval("OrganizeCity").ToString() + ","+ Eval("AccountMonth").ToString() + ")"  %>'
                                                        Visible='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="1"%>' />
                                                </ItemTemplate>
                                                <ControlStyle CssClass="button" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OrganizeCity" HeaderText="管理片区" />
                                            <asp:TemplateField HeaderText="销量目标">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbBaseVolume" runat="server" Width="70px" Text='<%# Bind("BaseVolume","{0:0.#}") %>'
                                                        Enabled='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="计划销量" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbPlanVolume" runat="server" Width="60px" Text='<%# Bind("PlanVolume","{0:0.#}") %>'
                                                        Enabled='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="计划增量" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabPlanAdd" runat="server" Width="60px" Text='<%# ((decimal)DataBinder.Eval(Container.DataItem,"PlanVolume")-(decimal)DataBinder.Eval(Container.DataItem,"BaseVolume")).ToString("0.##") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="预算金额">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbBaseBudget" runat="server" Width="70px" Text='<%# Bind("BaseBudget","{0:0.#}") %>'
                                                        Enabled='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OverFullBudget" HeaderText="增量费用" Visible="false" HtmlEncode="false"
                                                DataFormatString="{0:0.##}" />
                                            <asp:TemplateField HeaderText="预算费率">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_BudgetRate" runat="server" Text='<%# ((decimal)Eval("BaseBudget")/(decimal)Eval("BaseVolume")).ToString("0.0%")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="可用费用总计">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabTotalBudget" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="已分配费用">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_AssignedBudget" runat="server" Width="60px" Text='<%# GetAssignedBudget((int)DataBinder.Eval(Container.DataItem,"OrganizeCity"),(int)DataBinder.Eval(Container.DataItem,"AccountMonth")).ToString("0.#") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="自行支配费用" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbRetentionBudget" runat="server" Width="60px" Text='<%# Bind("RetentionBudget","{0:0.##}") %>'
                                                        Enabled='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="市场部预算额度" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbDepartmentBudget" runat="server" Width="60px" Enabled='<%#  DataBinder.Eval(Container,"DataItem.ApproveFlag").ToString()=="2"%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApproveFlag" HeaderText="审核标记" />
                                        </Columns>
                                    </mcs:UC_GridView>
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
