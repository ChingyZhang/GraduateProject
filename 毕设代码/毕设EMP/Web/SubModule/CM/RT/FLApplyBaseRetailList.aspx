<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="FLApplyBaseRetailList.aspx.cs" Inherits="SubModule_RT_FLApplyBaseRetailList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function check(txt) {
            if (txt.value>200) {
                txt.value = "";
                alert("本月进货门店数最大只能是200");
            }
        }
    </script>

    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                进货额管控零售商列表</h2>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr>
            <td class="h3Row" height="25">
               <h2>查询条件</h2>
            </td>
        </tr>--%>
        <tr class="tabForm">
            <td height="30px">
                <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0" height="60">
                    <tr runat="server" id="tr_basicsearch">
                        <td>
                            管理片区<mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                        </td>
                        <td>
                            门店返利类型
                            <asp:DropDownList ID="ddl_FLType" DataTextField="Value" DataValueField="Key" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Month" DataTextField="Name" DataValueField="ID" runat="server"
                                AppendDataBoundItems="true" Visible="false">
                                <asp:ListItem Text="请选择" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click"
                                ValidationGroup="0" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    快捷查询
                                    <asp:DropDownList ID="ddl_SearchType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SearchType_SelectedIndexChanged">
                                        <asp:ListItem Value="Code">门店编号</asp:ListItem>
                                        <asp:ListItem Value="DIName">经销商</asp:ListItem>
                                    </asp:DropDownList>
                                    相似于
                                    <asp:TextBox ID="tbx_Condition" runat="server"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_SearchType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            是否母婴大连锁店
                            <asp:DropDownList ID="ddl_IsMYD" DataTextField="Value" DataValueField="Key" runat="server">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="否" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_Save" runat="server" Text="保 存" Width="80" OnClick="btn_Save_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <asp:Panel ID="pl_detail" runat="server">
                    <h3>
                        ≡≡≡≡>编辑详情</h3>
                    <table width="100%">
                        <tr>
                            <td class="dataLabel">
                                零售商
                            </td>
                            <td class="dataField" width="240">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Always">
                                    <ContentTemplate>
                                        <mcs:MCSSelectControl ID="select_OrgSupplier" runat="server" Width="220px" OnSelectChange="select_OrgSupplier_SelectChange" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="dataLabel">
                                返利类型
                            </td>
                            <td class="dataField">
                                <asp:DropDownList ID="ddl_FLTypeDetail" DataTextField="Value" DataValueField="Key"
                                    runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="dataLabel">
                            </td>
                            <td class="dataField">
                                <asp:RadioButtonList ID="rbl_IsMYD" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                    Visible="false" Enabled="false" OnSelectedIndexChanged="rbl_IsMYD_SelectedIndexChanged">
                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="dataLabel">
                                进货限制额<br />
                                进货额坎级
                            </td>
                            <td class="dataField">
                                <asp:TextBox ID="tbx_Amount" runat="server"></asp:TextBox>
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_AddDetail" runat="server" Text="添 加" Width="70" OnClick="btn_AddDetail_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataKeyNames="ID" PageSize="15" Width="100%" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                    OnRowDeleting="gv_List_RowDeleting" OnRowDataBound="gv_List_RowDataBound" OnPageIndexChanging="gv_List_PageIndexChanging">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1">
                        </asp:CommandField>
                        <asp:BoundField DataField="OrganizeCity100" HeaderText="管理片区" />
                        <asp:BoundField DataField="Code" HeaderText="门店编号" />
                        <asp:BoundField DataField="FullName" HeaderText="门店全称" />
                        <asp:BoundField DataField="DIName" HeaderText="经销商名称" />
                        <asp:BoundField DataField="ClientManager" HeaderText="门店客户负责人名称" />
                        <asp:BoundField DataField="FLType" HeaderText="管控类型" />
                        <asp:BoundField DataField="FLBase" HeaderText="进货限制额（区域供价）" />
                        <asp:BoundField DataField="ISMYD" HeaderText="是否母婴大连锁" />
                        <asp:TemplateField HeaderText="本月进货门店数">
                            <ItemTemplate>
                                <asp:TextBox ID="tbx_RTCount" runat="server" Width="60" Text='<%# DataBinder.Eval(Container,"DataItem.RTCount")%>' onblur="check(this)"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_RTCount"
                                    ValueToCompare="0" Display="Dynamic" ErrorMessage="必须为大于等于1的整数" Operator="GreaterThan"
                                    Type="Integer"></asp:CompareValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="true" DeleteText="删除" ControlStyle-CssClass="listViewTdLinkS1">
                        </asp:CommandField>
                    </Columns>
                    <EmptyDataTemplate>
                        无数据
                    </EmptyDataTemplate>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
