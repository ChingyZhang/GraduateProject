<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="PM_PromotorApproveList.aspx.cs" Inherits="SubModule_PM_PM_PromotorApproveList" %>

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
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="导购员批量审批(入离职\调薪)"></asp:Label>
                            </h2>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Button ID="btn_Approve" Width="80px" Text="审批通过" runat="server" Visible="false"
                                        OnClick="btn_Approve_Click" OnClientClick="return confirm('是否确认将选中的导购员设为审批通过?')" />
                                    <asp:Button ID="btn_UnApprove" Width="80px" Text="审批不通过" runat="server" Visible="false"
                                        OnClick="btn_UnApprove_Click" OnClientClick="return confirm('是否确认将选中的导购员设为审批不通过?')" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right" width="85px">
                            <asp:Button ID="bt_Export" runat="server"  OnClick="bt_Export_Click"
                                Text="导出明细" Width="80px" />
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel">
                                    查询流程
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_App" runat="server" Width="200px">
                                        <asp:ListItem Value="Add_Promotor">导购员入职流程</asp:ListItem>
                                        <asp:ListItem Value="Revocation_Promotor">导购员离职流程</asp:ListItem>
                                        <asp:ListItem Value="Apply_PromotorSalary">导购员修改薪酬福利流程</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        AutoPostBack="true" ParentColumnName="SuperID" Width="220px" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td class="dataLabel">
                                    查看层级
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Level" runat="server" DataValueField="Key" DataTextField="Value"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_State_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">待我审批的导购</asp:ListItem>
                                        <asp:ListItem Value="2">所有在职导购</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td height="10px">
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px;" align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="导购明细列表" Value="1" />
                                                <mcs:MCSTabItem Text="片区内宏观统计" Value="2" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" AllowPaging="True" PageSize="10"
                                            OnPageIndexChanging="gv_List_PageIndexChanging" OnDataBound="gv_List_DataBound"
                                            DataKeyNames="流程ID">
                                            <Columns>
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
                                        <mcs:UC_GridView ID="gv_Consult" runat="server" Width="96%" AllowPaging="true" PageSize="50"
                                            Visible="false">
                                            <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                            <Columns>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_Approve" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btn_UnApprove" EventName="Click" />
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
