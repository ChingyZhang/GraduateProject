<%@ control language="C#" autoeventwireup="true" inherits="Controls_AdvancedSearch, App_Web_3-xbnske" %>
<style type="text/css">
    .style1
    {
        height: 26px;
    }
</style>
<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr runat="server" id="tr_AdvancedSearch">
        <td>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td height="28">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Block" UpdateMode="Conditional">
                            <ContentTemplate>
                                预存的查询:<asp:DropDownList ID="ddl_FindCondition" runat="server" AutoPostBack="True"
                                    DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="ddl_FindCondition_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Button ID="bt_SaveCondition" runat="server" OnClick="bt_SaveCondition_Click"
                                    Text="保存查询" />
                                <asp:Button ID="bt_CreateNewCondition" runat="server" OnClick="bt_CreateNewCondition_Click"
                                    Text="新建查询" />
                                <asp:Button ID="bt_ConditionList" runat="server" OnClick="bt_ConditionList_Click"
                                    Text="管理查询" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:ListBox ID="lbx_search" runat="server" Width="913px"></asp:ListBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_FindCondition" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btn_addsearch" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btn_Del" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td height="28">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                条件字段:
                                <asp:DropDownList ID="ddl_TableName" runat="server" DataTextField="DisplayName" DataValueField="TableID"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddl_TableName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddl_Field" runat="server" DataTextField="DisplayName" DataValueField="ID"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddl_op" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_op_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddl_TreeLevel" runat="server" Visible="false" DataTextField="Value"
                                    DataValueField="Key">
                                </asp:DropDownList>
                                <asp:TextBox ID="tbx_searchvalue" runat="server" Visible="False"></asp:TextBox>
                                <mcs:MCSTreeControl ID="MCSTreeControl1" runat="server" Visible="false" Width="200px"
                                    IDColumnName="ID" NameColumnName="Name" ParentColumnName="SuperID" />
                                <mcs:MCSSelectControl ID="MCSSelectControl1" runat="server" Visible="false"></mcs:MCSSelectControl>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_addsearch" runat="server" Text="添加条件" OnClick="btn_addsearch_Click"
                            Width="80px"></asp:Button>
                        <asp:Button ID="btn_Del" runat="server" Text="删除条件" OnClick="btn_Del_Click" Width="80px">
                        </asp:Button>
                        <asp:Button ID="btn_OK" runat="server" Text="查  询" OnClick="btn_OK_Click" Width="80px">
                        </asp:Button>
                        <asp:Button ID="btn_Export" runat="server" OnClick="btn_Export_Click" Text="导出为Excel"
                            Visible="true" Width="80px" />
                        <asp:Button ID="bt_TotalCount" runat="server" Text="显示结果数" Visible="true" Width="80px"
                            OnClick="bt_TotalCount_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="cbl_SearchValue" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                    DataTextField="Name" DataValueField="ID">
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_op" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr height="1px">
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <mcs:UC_GridView ID="gv_List" runat="server" PageSize="15" Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" Binded="False" ConditionString="" OnSelectedIndexChanging="gv_List_SelectedIndexChanging"
                PanelCode="" TotalRecordCount="0" OnRowDataBound="gv_List_RowDataBound">
                <EmptyDataTemplate>
                    无数据
                </EmptyDataTemplate>
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="查看" ControlStyle-CssClass="listViewTdLinkS1" />
                </Columns>
            </mcs:UC_GridView>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lbl_TotalRowsNumer" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
