<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="FeeApplySummary.aspx.cs" Inherits="SubModule_FNA_FeeApply_ApplySummary_FeeApplySummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                                费用申请审批汇总单信息
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
                                    <asp:Button ID="btn_Approve" Width="80px" Text="审批通过" runat="server" Visible="false"
                                        OnClick="btn_Approve_Click" OnClientClick="return confirm('是否确认将选中的费用申请设为审批通过?')" />
                                    <asp:Button ID="btn_UnApprove" Width="80px" Text="审批不通过" runat="server" Visible="false"
                                        OnClick="btn_UnApprove_Click" OnClientClick="return confirm('是否确认将选中的费用申请设为审批不通过?')" />
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
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    费用类型
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_FeeType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server">
                                        <asp:ListItem Value="0">所有(含已提交及已通过)</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">待我审批的申请单</asp:ListItem>
                                        <asp:ListItem Value="2">已审批通过的申请单</asp:ListItem>
                                        <asp:ListItem Value="3">由我审批通过的申请单</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    费用预支状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Flag" runat="server">
                                        <asp:ListItem Selected="True" Value="0">0.所有(含1,2,3项)</asp:ListItem>
                                        <asp:ListItem Value="1">1.当月申请的费用(含2项)</asp:ListItem>
                                        <asp:ListItem Value="2">2.当月申请的仅预支费用</asp:ListItem>
                                        <asp:ListItem Value="3">3.仅发生在当月(含先前月批复)</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    会计科目(含有)
                                </td>
                                <td class="dataField" colspan="3">
                                    <mcs:MCSTreeControl ID="tr_AccountTitle" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="260px" TableName="MCS_Pub.dbo.AC_AccountTitle"
                                        RootValue="0" />
                                </td>
                                <td class="dataLabel">
                                    单笔申请金额
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_ApplyCostOP" runat="server">
                                        <asp:ListItem Value="&gt;">大于</asp:ListItem>
                                        <asp:ListItem Value="&lt;">小于</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbx_ApplyCost" runat="server" Width="80px">0</asp:TextBox>
                                    元
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                        Display="Dynamic" ErrorMessage="必需为数字格式" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
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
                                                <mcs:MCSTabItem Text="查看汇总" Value="1" />
                                                <mcs:MCSTabItem Text="查看明细" Value="2" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" DataKeyNames="ID" OnDataBound="gv_List_DataBound"
                                            OnSelectedIndexChanging="gv_List_SelectedIndexChanging" OnRowDeleting="gv_List_RowDeleting"
                                            GridLines="Both" CellPadding="1" BackColor="#BBBBBB" CellSpacing="1" CssClass=""
                                            BorderWidth="0px" AllowPaging="true" PageSize="50" OnPageIndexChanging="gv_List_PageIndexChanging">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" ID="bt_Approved" CommandName="select" Text="审批通过" Width="70px"
                                                            OnClientClick="return confirm('是否确认将该区域下所有申请单审批通过?')" Visible='<%# ((int)Eval("ID"))!=0%>' /><br />
                                                        <br />
                                                        <asp:Button runat="server" ID="bt_UnApproved" CommandName="delete" Text="审批不通过" Width="70px"
                                                            OnClientClick="return confirm('是否确认将该区域下所有申请单全部设为审批不通过?')" Visible='<%# ((int)Eval("ID"))!=0%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                            PanelCode="Panel_FNA_FeeApplyList" DataKeyNames="FNA_FeeApply_ID,FNA_FeeApply_ApproveTask"
                                            AllowPaging="True" PageSize="15" Visible="false" OnRowDataBound="gv_ListDetail_RowDataBound">
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="FNA_FeeApply_ID" DataNavigateUrlFormatString="../FeeApplyDetail3.aspx?ID={0}"
                                                    DataTextField="FNA_FeeApply_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                                                    HeaderText="申请备案号" >
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField HeaderText="<table cellspacing=0 cellpadding=0><tr><th width=120px>客户</th><th width=120px>会计科目</th><th width=60px>发生月</th><th width=60px>批复金额</th></tr></table>">
                                                    <ItemTemplate>
                                                        <mcs:UC_GridView ID="gv_Detail" runat="server" DataKeyNames="ID,AccountTitle" AutoGenerateColumns="false"
                                                            ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="Client" HeaderText="客户" ItemStyle-Width="120px" />
                                                                <asp:BoundField DataField="AccountTitle" HeaderText="会计科目" ItemStyle-Width="120px" />
                                                                <asp:BoundField DataField="BeginMonth" HeaderText="发生月" ItemStyle-Width="60px" />
                                                                <asp:TemplateField ItemStyle-Width="60px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_ApplyCost" runat="server" Text='<%# ((decimal)Eval("ApplyCost")+ (decimal)Eval("AdjustCost")).ToString("0.##")%> '></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </mcs:UC_GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="总金额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.FNA.FNA_FeeApplyBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_FeeApply_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("FNA_FeeApply_ApproveTask", "../../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                            Text="审批记录" Visible='<%# Eval("FNA_FeeApply_ApproveTask").ToString()!="" %>'
                                                            ></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="20px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" ToolTip="全选" onclick="javascript:SelectAll(this);">
                                                        </asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_ID" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btn_Approve" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btn_UnApprove" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />                          
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 40;
        divGridView.style.height = window.screen.availHeight - 350;            
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
