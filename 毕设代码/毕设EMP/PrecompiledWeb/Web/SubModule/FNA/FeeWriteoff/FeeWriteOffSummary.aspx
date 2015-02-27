<%@ page language="C#" autoeventwireup="true" masterpagefile="~/MasterPage/BasicMasterPage.master" inherits="SubModule_FNA_FeeWriteoff_FeeWriteOffSummary, App_Web_lxhzl6y2" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                费用核销审批汇总单信息
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
                            <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
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
                                    核销月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key">
                                        <asp:ListItem Value="0">所有核销单</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">待我审批的核销单</asp:ListItem>
                                        <asp:ListItem Value="2">已审批通过的核销单</asp:ListItem>
                                        <asp:ListItem Value="3">由我审批通过的核销单</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    费用代垫客户
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" Width="220px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2" />
                                </td>
                                <td class="dataLabel">
                                    费用代垫员工
                                </td>
                                <td class="dataField" colspan="3">
                                    <mcs:MCSSelectControl ID="Select_InsteadPayStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="220px"></mcs:MCSSelectControl>
                                </td>
                                <td class="dataLabel">
                                    核销申请人
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="Select_InsertStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="220px"></mcs:MCSSelectControl>
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
                                    会计科目(含有)
                                </td>
                                <td class="dataField" colspan="3">
                                    <mcs:MCSTreeControl ID="tr_AccountTitle" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="260px" TableName="MCS_Pub.dbo.AC_AccountTitle"
                                        RootValue="0" />
                                </td>
                                <td class="dataLabel">
                                    单笔核销金额
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_WriteOffCostOP" runat="server">
                                        <asp:ListItem Value="&gt;">大于</asp:ListItem>
                                        <asp:ListItem Value="&lt;">小于</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbx_WriteOffCost" runat="server" Width="80px">0</asp:TextBox>
                                    元
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_WriteOffCost"
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_WriteOffCost"
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
                                            GridLines="Both" CellPadding="1" BackColor="#BBBBBB" CellSpacing="1" CssClass=""
                                            AllowPaging="true" PageSize="50" BorderWidth="0px" OnPageIndexChanging="gv_List_PageIndexChanging"
                                            OnRowCommand="gv_List_RowCommand">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" ID="bt_Approved" CommandName="Approved" Text="审批通过" Width="70px"
                                                            CommandArgument='<%#(int)Eval("ID") %>' OnClientClick="return confirm('是否确认将该区域下所有申请单审批通过?')"
                                                            Visible='<%# ((int)Eval("ID"))!=0%>' /><br />
                                                        <br />
                                                        <asp:Button runat="server" ID="bt_UnApproved" CommandName="UnApproved" Text="审批不通过"
                                                            Width="70px" CommandArgument='<%#(int)Eval("ID") %>' OnClientClick="return confirm('是否确认将该区域下所有申请单全部设为审批不通过?')"
                                                            Visible='<%# ((int)Eval("ID"))!=0%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" HorizontalAlign="Right" Height="28px" />
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="98%" AutoGenerateColumns="False"
                                            PanelCode="Panel_FNA_FeeWriteOffList" DataKeyNames="FNA_FeeWriteOff_ID" AllowPaging="True"
                                            PageSize="15" Visible="false" OnRowDataBound="gv_ListDetail_RowDataBound">
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="FNA_FeeWriteOff_ID" DataNavigateUrlFormatString="FeeWriteoffDetail.aspx?ID={0}"
                                                    DataTextField="FNA_FeeWriteOff_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                                                    HeaderText="核销单号" >
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField HeaderText="<table cellspacing=0 cellpadding=0><tr><th width=120px>客户</th><th width=120px>会计科目</th><th width=60px>发生月</th><th width=60px>原申请额</th><th width=60px>原申请核销额</th><th width=60px>扣款金额</th><th width=60px>批复核销金额</th><th width=60px>逾期明细</th></tr></table>">
                                                    <ItemTemplate>
                                                        <mcs:UC_GridView ID="gv_Detail" runat="server" DataKeyNames="ID,AccountTitle" AutoGenerateColumns="false"
                                                            ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="Client" HeaderText="客户" ItemStyle-Width="120px" />
                                                                <asp:BoundField DataField="AccountTitle" HeaderText="会计科目" ItemStyle-Width="120px" />
                                                                <asp:BoundField DataField="BeginMonth" HeaderText="发生月" ItemStyle-Width="60px" />
                                                                
                                                                <asp:BoundField DataField="ApplyCost" HeaderText="原申请额" ItemStyle-Width="60px" DataFormatString="{0:0.##}" />
                                                                <asp:BoundField DataField="WriteOffCost" HeaderText="原核销额" ItemStyle-Width="60px"
                                                                    DataFormatString="{0:0.##}" />
                                                                <asp:BoundField DataField="AdjustCost" HeaderText="扣款额" ItemStyle-Width="60px" DataFormatString="{0:0.##}"
                                                                    ItemStyle-ForeColor="Blue" />
                                                                <asp:TemplateField ItemStyle-Width="60px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lb_WriteOffCost" runat="server" ForeColor="Red" Text='<%# ((decimal)Eval("WriteOffCost")+ (decimal)Eval("AdjustCost")).ToString("0.##")%> '></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="是否逾期">
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lb_DelayMonth" ForeColor="Red" runat="server" Text='<%# GetISDelay((int)Eval("ApplyDetailID"))%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </mcs:UC_GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="本单总核销额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_SumCost" runat="server" Text='<%# MCSFramework.BLL.FNA.FNA_FeeWriteOffBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_FeeWriteOff_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("FNA_FeeWriteOff_ApproveTask", "../../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                            Text="审批记录" Visible='<%# Eval("FNA_FeeWriteOff_ApproveTask").ToString()!="" %>'
                                                            ></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 50;      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
