<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="SalesVolumeList.aspx.cs" Inherits="SubModule_SVM_SalesVolumeList" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <asp:Label ID="lb_PageTitle" runat="server" Text=""></asp:Label></h2>
                        </td>
                        <td>
                            查询日期范围:
                            <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="日期格式不对"
                                Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator>至<asp:TextBox
                                    ID="tbx_end" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="日期格式不对"
                                Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                        </td>
                        <td align="left" style="color: #FF0000">
                            注:销量批量审核操作，转移至"审批平台->进销存->进销数据审批"模块下的相应汇总表页面下统一审核!
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_BatchInput" runat="server" Text="新增(成品)" Width="70px" OnClick="bt_BatchInput_Click" />
                            <asp:Button ID="bt_BatchInput2" runat="server" OnClick="bt_BatchInput2_Click" Text="新增(赠品)"
                                Width="70px" ForeColor="Blue" />
                            <asp:Button ID="bt_BathApprove" runat="server" Text="批量审核" Width="60px" OnClick="bt_BathApprove_Click"
                                OnClientClick="return confirm('是否确认将选中的数据批量设为已审核通过？')" Visible="false" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl2" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl2_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="快捷查询" Description=""
                            Value="0" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="高级查询" Description=""
                            Value="1" Enable="True"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">                
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    <asp:Label ID="lb_Supplier" runat="server" Text="出货客户"></asp:Label>
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Supplier" Width="200px" OnSelectChange="select_Supplier_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    <asp:Label ID="lb_Client" runat="server" Text="进货客户"></asp:Label>
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" Width="200px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    销量类别
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_SalesFlag" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="bt_Find" runat="server" Text="查 找" Width="70px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td nowrap>
                            <h3>
                                记录列表</h3>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="rbl_ApproveFlag" runat="server" RepeatColumns="4" RepeatLayout="Flow"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_ApproveFlag_SelectedIndexChanged"
                                        DataTextField="Value" DataValueField="Key">
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cc1:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0" Width="100%">
                            <Items>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="记录明细" Description=""
                                    Value="0" Enable="True" Visible="True"></cc1:MCSTabItem>
                                <cc1:MCSTabItem Target="_self" NavigateURL="" ImgURL="" Text="销量汇总" Description=""
                                    Value="1" Enable="True" Visible="True"></cc1:MCSTabItem>
                            </Items>
                        </cc1:MCSTabControl>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tr_detail"
                            visible="true">
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        PanelCode="Panel_SVM_SalesVolumnList_1" DataKeyNames="SVM_SalesVolume_ID" PageSize="25"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_ID" runat="server" Visible='<%#  DataBinder.Eval(Container,"DataItem.SVM_SalesVolume_ApproveFlag").ToString()=="未审核" && 
                                                   DataBinder.Eval(Container,"DataItem.SVM_SalesVolume_SubmitFlag").ToString()!="否"%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField Text="查看详细" DataNavigateUrlFields="SVM_SalesVolume_ID" DataNavigateUrlFormatString="SalesVolumeBatchInput.aspx?VolumeID={0}"
                                                ControlStyle-CssClass="listViewTdLinkS1" ItemStyle-Width="80px">
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="厂价金额">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetTotalFactoryPriceValue(DataBinder.Eval(Container,"DataItem.SVM_SalesVolume_ID").ToString()) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="批发价金额">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# GetTotalValue(DataBinder.Eval(Container,"DataItem.SVM_SalesVolume_ID").ToString()) %>'></asp:Label>
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
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="bt_BathApprove" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                        <asp:AsyncPostBackTrigger ControlID="rbl_ApproveFlag" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="tr_summary"
                            visible="false">
                            <tr>
                                <td>
                                    <asp:GridView ID="gv_Summary" runat="server" AutoGenerateColumns="False" Width="100%"
                                        ShowFooter="false">
                                        <Columns>
                                            <asp:BoundField DataField="BrandName" HeaderText="品牌" SortExpression="BrandName" />
                                            <asp:BoundField DataField="ClassifyName" HeaderText="品类" SortExpression="ClassifyName" />
                                            <asp:BoundField DataField="ProductCode" HeaderText="产品代码" SortExpression="ProductCode" />
                                            <asp:BoundField DataField="ProductName" HeaderText="产品名称" SortExpression="ProductName" />
                                            <asp:BoundField DataField="SumQuantity" HeaderText="合计数量" SortExpression="SumQuantity"
                                                DataFormatString="{0:f0}" HtmlEncode="False" />
                                            <asp:BoundField DataField="SumFactoryMoney" HeaderText="合计厂价金额" SortExpression="SumFactoryMoney"
                                                HtmlEncode="False" DataFormatString="{0:f2}" />
                                            <asp:BoundField DataField="SumMoney" HeaderText="合计批发价金额" SortExpression="SumMoney"
                                                HtmlEncode="False" DataFormatString="{0:f2}" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <span style="color: #FF0000">数据处理中，请稍候...</span></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
