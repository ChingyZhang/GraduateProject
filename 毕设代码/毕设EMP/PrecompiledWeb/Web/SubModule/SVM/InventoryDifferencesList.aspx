<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_SVM_InventoryDifferencesDifferencesList, App_Web_yabmfp6z" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="Label"></asp:Label></h2>
                                </td>
                                <td align="right" width="100%">
                                    &nbsp;<asp:Button ID="bt_BatchInput" runat="server" Text="新增(成品)" Width="70px" OnClick="bt_BatchInput_Click" />
                                    <asp:Button ID="bt_BatchInput2" runat="server" OnClick="bt_BatchInput2_Click" Text="新增(赠品)"
                                        Width="70px" ForeColor="Blue" />
                                    <asp:Button ID="bt_BathApprove" runat="server" Text="批量审核" Width="70px" OnClick="bt_BathApprove_Click"
                                        OnClientClick="return confirm('是否确认将选中的数据批量设为已审核通过？')" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server"
                            id="tb_Head">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="250px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    客户
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx"
                                        Width="200px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    会计月
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_BeginMonth" DataValueField="ID" DataTextField="Name" runat="server">
                                    </asp:DropDownList>
                                    至<asp:DropDownList ID="ddl_EndMonth" DataValueField="ID" DataTextField="Name" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    是否赠品
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_IsCXP" runat="server">
                                        <asp:ListItem Text="所有" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="成品" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="赠品" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="60px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td nowrap>
                                    <h3>
                                        库存盘盈/亏列表</h3>
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rbl_ApproveFlag" runat="server" RepeatColumns="4" RepeatLayout="Flow"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_ApproveFlag_SelectedIndexChanged"
                                        DataTextField="Value" DataValueField="Key">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:MCSTabControl ID="MCSTabControl2" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl2_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="盈亏明细" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="盈亏汇总" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tr_detail">
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        PanelCode="Panel_SVM_InventoryDifferencesList" DataKeyNames="SVM_InventoryDifferences_ID"
                                        PageSize="15" Width="100%">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_ID" runat="server" Visible='<%#  DataBinder.Eval(Container,"DataItem.SVM_InventoryDifferences_ApproveFlag").ToString()=="未审核"%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField Text="查看详细" DataNavigateUrlFields="SVM_InventoryDifferences_ID"
                                                DataNavigateUrlFormatString="InventoryDifferenceInput.aspx?InventoryID={0}" 
                                                ItemStyle-Width="80px">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="盈亏额">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetTotalFactoryPriceValue(DataBinder.Eval(Container,"DataItem.SVM_InventoryDifferences_ID").ToString()) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("SVM_InventoryDifferences_TaskID", "../EWF/TaskDetail.aspx?TaskID={0}") %>'
                                                        Text="审批记录" Visible='<%# Eval("SVM_InventoryDifferences_TaskID").ToString()!="" %>'
                                                        ></asp:HyperLink>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tr_summary"
                            visible="false">
                            <tr>
                                <td>
                                    <asp:GridView ID="gv_Summary" runat="server" AutoGenerateColumns="False" Width="100%"
                                        ShowFooter="false">
                                        <Columns>
                                            <asp:BoundField DataField="ProductCode" HeaderText="产品代码" SortExpression="ProductCode" />
                                            <asp:BoundField DataField="ProductName" HeaderText="产品名称" SortExpression="ProductName" />
                                            <asp:BoundField DataField="SumQuantity" HeaderText="合计盈亏数量" SortExpression="SumQuantity"
                                                DataFormatString="{0:f0}" HtmlEncode="False" />
                                            <asp:BoundField DataField="SumMoney" HeaderText="合计盈亏金额" SortExpression="SumMoney"
                                                HtmlEncode="False" DataFormatString="{0:f2}" />
                                            <asp:BoundField DataField="SumComput" HeaderText="合计账面金额" SortExpression="SumComput"
                                                HtmlEncode="False" DataFormatString="{0:f2}" />
                                            <asp:BoundField DataField="SumDifferenceCost" HeaderText="合计差异额" SortExpression="SumDifferenceCost"
                                                HtmlEncode="False" DataFormatString="{0:f2}" />
                                            <asp:BoundField DataField="SumDifferenceRate" HeaderText="差异率" SortExpression="SumDifferenceRate"
                                                HtmlEncode="False" DataFormatString="{0:f2}" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <span style="color: #FF0000">数据处理中，请稍候...</span></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
