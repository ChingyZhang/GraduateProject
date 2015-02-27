<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_FeeApplyOrWriteoffByClientList, App_Web_xbone0q1" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Assembly="MCSTabControl" Namespace="MCSControls.MCSTabControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
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
                                        费用代垫列表查询
                                    </h2>
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
                                        ParentColumnName="SuperID" AutoPostBack="true" Width="180px" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    经销商
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" Width="250px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" 
                                        DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    会计月份
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
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
                                        经销商费用代垫列表</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PanelCode="Panel_FNA_FeeApplyOrWriteoffInsteadPayClientList" DataKeyNames="FNA_FeeWriteOff_ID"
                                    PageSize="10" Width="100%" ondatabound="gv_List_DataBound">
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="FNA_FeeWriteOff_ID" DataNavigateUrlFormatString="~/SubModule/FNA/FeeWriteoff/FeeWriteOffDetail.aspx?ID={0}"
                                            HeaderText="核消单号" DataTextField="FNA_FeeWriteOff_SheetCode" ControlStyle-CssClass="listViewTdLinkS1"
                                             />
                                        <asp:TemplateField HeaderText="总金额">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_SumCost" runat="server" ForeColor="Red" Text='<%# MCSFramework.BLL.FNA.FNA_FeeWriteOffBLL.GetSumCost(int.Parse(DataBinder.Eval(Container,"DataItem.FNA_FeeWriteOff_ID").ToString())).ToString("0.###") %>'></asp:Label>元
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        无数据
                                    </EmptyDataTemplate>
                                </mcs:UC_GridView>
                                <p align=right>合计总金额：<asp:Label ID="lb_SumTotalCost" runat="server" ForeColor="Red" Text=''></asp:Label>元</p>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
