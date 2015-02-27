<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PDT_ClassifyGiftCostRate.aspx.cs" Inherits="SubModule_Product_PDT_ClassifyGiftCostRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td align="right" width="20">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td align="left" width="150">
                                    <h2>
                                        赠品费率维护</h2>
                                </td>
                                <td style="color: Red">
                                    （提示：添加、删除、修改之后请按保存修改数据）
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" Text="查找"
                                        Width="60" />
                                    &nbsp;<asp:Button ID="btn_Save" runat="server" Text="保 存" Width="60px" OnClick="btn_Save_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel" width="100px">
                                    管理办事处
                                </td>
                                <td>
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AlwaysSelectChildNode="False" AutoPostBack="True"
                                        OnSelected="tr_OrganizeCity_Selected" SelectDepth="0" RootValue="0" DisplayRoot="True" />
                                </td>
                                <td class="dataLabel" width="100px">
                                    经销商
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                        Width="250px" OnSelectChange="select_Client_SelectChange" />
                                </td>
                                <%-- <td class="dataLabel" width="100px">
                                    会计月
                                </td>--%>
                                <%--  <td>
                                  
                                </td>--%>
                                <td class="dataLabel" width="100px">
                                    下级层级
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_Level" runat="server" DataTextField="Value" DataValueField="Key"
                                        Width="100px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" runat="server" id="tr_AddProduct">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                                                <tr>
                                                    <td>
                                                        <h3>
                                                            新增产品</h3>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tabForm">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr height="28px">
                                                            <td>
                                                                产品品牌
                                                            </td>
                                                            <td>
                                                                赠品费用类别
                                                            </td>
                                                            <td>
                                                                赠品费率
                                                            </td>
                                                            <td>
                                                                备注
                                                            </td>
                                                            <td>
                                                                起始会计月
                                                            </td>
                                                            <td>
                                                                使用周期
                                                            </td>
                                                            <td>
                                                                启用标志
                                                            </td>
                                                            <td align="right" rowspan="2">
                                                                <asp:Button ID="bt_Add" runat="server" OnClick="bt_Add_Click" Text="增加" Width="70px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_GiftCostClassify" runat="server" DataTextField="Value"
                                                                    DataValueField="Key">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbx_CostPrice" runat="server" Width="50px"></asp:TextBox>%
                                                                <asp:CompareValidator ID="CompareValidator17" runat="server" ControlToValidate="tbx_CostPrice"
                                                                    Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbx_Remark" runat="server" Width="142px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                                                    AppendDataBoundItems="true" Width="100px">
                                                                    <asp:ListItem Text="请选择" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Cycle" runat="server" Width="50px"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_Cycle"
                                                                    Display="Dynamic" ErrorMessage="必须为大于0的整数" Operator="DataTypeCheck" Type="Integer"
                                                                    ValueToCompare="0"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_Enabled" runat="server">
                                                                    <asp:ListItem Selected="True" Value="Y">启用</asp:ListItem>
                                                                    <asp:ListItem Value="N">停用</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <%--<asp:AsyncPostBackTrigger ControlID="gv_List" EventName="SelectedIndexChanging" />--%>
                                                    <asp:PostBackTrigger ControlID="gv_List" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="center">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                                Width="100%" OnRowDeleting="gv_List_RowDeleting" OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1" />
                                                    <asp:BoundField DataField="PDTBrand" HeaderText="产品品牌" />
                                                    <asp:BoundField DataField="OrganizeCity" HeaderText="管理片区" />
                                                    <asp:BoundField DataField="Client" HeaderText="客户" />
                                                    <asp:BoundField DataField="GiftCostClassify" HeaderText="赠品费用类别" />
                                                    <asp:TemplateField HeaderText="赠品费率(%)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%#  DataBinder.Eval(Container,"DataItem[\"GiftCostRate\"]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BeginMonth" HeaderText="会计月" />
                                                    <asp:BoundField DataField="Cycle" HeaderText="使用周期" />
                                                    <asp:TemplateField HeaderText="启用标志">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%#  DataBinder.Eval(Container,"DataItem[\"Enabled\"]").ToString()=="Y"?"启用":"禁用" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Remark" HeaderText="备注" />
                                                    <asp:BoundField DataField="ApproveFlag" HeaderText="审核标志" Visible="false" />
                                                    <asp:CommandField ShowDeleteButton="true" DeleteText="删除" ControlStyle-CssClass="listViewTdLinkS1" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    无数据
                                                </EmptyDataTemplate>
                                            </mcs:UC_GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="bt_Add" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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
