<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PropertyDetail.aspx.cs" Inherits="SubModule_CM_PD_PropertyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap" style="height: 24px;">
                            <h2>
                                物业详细信息</h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审核" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_PD_PropertyDetail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_PropertyInOrganizeCity" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" class="h3Row" height="30">
                                        <tr>
                                            <td>
                                                <h3>
                                                    覆盖其他管理片区</h3>
                                            </td>
                                            <td align="right" class="dataField">
                                                片区<asp:DropDownList ID="ddl_PropertyInOrganizeCity" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                <asp:Button ID="bt_Add_PropertyInOrganizeCity" runat="server" Text="增加覆盖" Width="80px"
                                                    ValidationGroup="2" OnClick="bt_Add_PropertyInOrganizeCity_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_PropertyInOrganizeCity" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" OnRowDeleting="gv_PropertyInOrganizeCity_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="Name" HeaderText="名称" />
                                                        <asp:BoundField DataField="Code" HeaderText="代码" />
                                                        <asp:ButtonField Text="删除" CommandName="Delete" ControlStyle-CssClass="listViewTdLinkS1" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        无数据</EmptyDataTemplate>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_Contract" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>
                                                    租赁合同列表
                                                </h3>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddContract" runat="server" Text="新 增" Width="60px" UseSubmitBehavior="False"
                                                    OnClick="bt_AddContract_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List_Contract" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="ID" Width="100%" OnSelectedIndexChanging="gv_List_Contract_SelectedIndexChanging"
                                        OnRowDataBound="gv_List_Contract_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1">
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="合同编码">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_Code" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem[\"Code\"]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BeginDate" HeaderText="开始日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                            <asp:BoundField DataField="EndDate" HeaderText="截止日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                            <asp:BoundField DataField="State" HeaderText="合同状态" />
                                            <asp:BoundField DataField="SignMan" HeaderText="我司签定人" />
                                            <asp:TemplateField HeaderText="<table width=400 cellspacing=0 cellpadding=0><tr><th>会计科目</th><th>月费用金额(元)</th><th>支付方式</th><th>付款截止日期</th></tr></table>">
                                                <ItemTemplate>
                                                    <mcs:UC_GridView ID="gv_Detail" runat="server" DataKeyNames="ID,AccountTitle" AutoGenerateColumns="false"
                                                        Width="400px" ShowHeader="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="AccountTitle" HeaderText="会计科目" SortExpression="AccountTitle" />
                                                            <asp:BoundField DataField="ApplyLimit" HeaderText="月费用金额(元)" SortExpression="ApplyLimit" />
                                                            <asp:BoundField DataField="PayMode" HeaderText="支付方式" SortExpression="PayMode" />
                                                            <asp:TemplateField HeaderText="付款截止日期">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lb_PayEndDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem[\"PayEndDate\"]") %> '></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </mcs:UC_GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# "~/SubModule/EWF/TaskDetail.aspx?TaskID="+DataBinder.Eval(Container,"DataItem.ApproveTask").ToString()%>'
                                                         Text='审批记录' Visible='<%# (int)Eval("ApproveTask")!=0 %>' CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_Telephone" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>
                                                    固定电话列表
                                                </h3>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddTele" runat="server" Text="新增电话" Width="60px" Visible="true"
                                                    UseSubmitBehavior="False" OnClick="bt_AddTele_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_Telephone" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="CM_PropertyInTelephone_ID" PanelCode="Panel_PropertyInTelephoneList">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="CM_PropertyInTelephone_ID" DataNavigateUrlFormatString="PropertyInTelephoneDetail.aspx?TelephoneID={0}"
                                                Text="查看详细" HeaderText="" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据</EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_Staff" runat="server">
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap>
                                                <h3>
                                                    住宿人员列表
                                                </h3>
                                            </td>
                                            <td align="right">
                                                选择要住宿的员工:
                                                <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx?MultiSelected=Y"
                                                    Width="300px" />
                                                <asp:Button ID="bt_AddStaff" runat="server" Text="加入住宿" Width="60px" Visible="true"
                                                    UseSubmitBehavior="False" OnClick="bt_AddStaff_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_Staff" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="Org_Staff_ID" PanelCode="Panel_StaffInProperty_List" Binded="False"
                                        ConditionString="" TotalRecordCount="0" OnRowDeleting="gv_Staff_RowDeleting">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="Org_Staff_ID" DataNavigateUrlFormatString="../../StaffManage/StaffDetail.aspx?ID={0}"
                                                DataTextField="Org_Staff_RealName" HeaderText="员工姓名" ControlStyle-CssClass="listViewTdLinkS1"
                                                >
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                        Text="删除" OnClientClick="return confirm('是否确认移除该员工?')"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据</EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
