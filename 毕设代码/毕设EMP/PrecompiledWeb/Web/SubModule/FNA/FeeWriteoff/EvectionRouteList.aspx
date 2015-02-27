<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_FeeWriteoff_EvectionRouteList, App_Web_lxhzl6y2" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function SelectAll(tempControl) {
            var theBox = tempControl;
            sState = theBox.checked;
            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++) {
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id && !elem[i].id.endsWith("cb_NoPage")) {
                    if (elem[i].checked != sState) {
                        elem[i].click();
                    }
                }
            }
        }
    
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="差旅行程列表查询"></asp:Label>
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="80px" />
                                    <asp:Button ID="bt_Add" runat="server" Text="新增行程" OnClientClick="javascript:NewDetail();"
                                        OnClick="bt_Add_Click" Width="80px" />
                                    <asp:Button ID="bt_WriteOff" runat="server" Text="生成报销单" OnClick="bt_WriteOff_Click"
                                        Width="80px" />
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
                                    日期范围
                                </td>
                                <td class="dataField">
                                    <asp:TextBox ID="tbx_begin" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_begin"
                                        Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    至<asp:TextBox ID="tbx_end" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_end"
                                        Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </td>
                                <td class="dataLabel">
                                    交通工具
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Transport" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    车牌号
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_Car" runat="server" DataTextField="CarNo" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server">
                                        <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                                        <asp:ListItem Value="1">未报销</asp:ListItem>
                                        <asp:ListItem Value="2">已报销</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" OnSelected="tr_OrganizeCity_Selected"
                                        AutoPostBack="True" />
                                </td>
                                <td class="dataLabel">
                                    员工
                                </td>
                                <td class="dataField">
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                        Width="120px" />
                                </td>
                                <td class="dataLabel">
                                    核销单号
                                </td>
                                <td class="dataField" colspan="3">
                                    <asp:TextBox ID="tbx_SheetCode" runat="server" Width="140px"></asp:TextBox>
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
                                        差旅行程列表</h3>
                                </td>
                                <td nowrap>
                                    <asp:CheckBox ID="cb_NoPage" runat="server" AutoPostBack="True" OnCheckedChanged="cb_NoPage_CheckedChanged"
                                        Text="不分页" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            PanelCode="Panel_FNA_EvectionRouteList_01" DataKeyNames="FNA_EvectionRoute_ID,Car_DispatchRide_ID"
                            AllowPaging="True" PageSize="15" OnSelectedIndexChanging="gv_List_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" ToolTip="全选" AutoPostBack="False" onclick="javascript:SelectAll(this);">
                                        </asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbx" runat="server" Visible='<%#  Eval("FNA_EvectionRoute_WriteOffID").ToString()==""%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="bt_OpenDetail" runat="server" CausesValidation="False" CommandName="Select"
                                            Text="查看" OnClientClick='<%# "javascript:OpenDetail(" + Eval("FNA_EvectionRoute_ID").ToString() +")"  %>' />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="button" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="行程公里数">
                                    <ItemTemplate>
                                        <%# getTotalKilometres( Eval("Car_DispatchRide_KilometresStart"),Eval("Car_DispatchRide_KilometresEnd")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="核销单号" SortExpression="FNA_FeeWriteOff_SheetCode">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_Client" runat="server" Text='<%# Bind("FNA_FeeWriteOff_SheetCode") %>'
                                            NavigateUrl='<%# "FeeWriteoffDetail.aspx?ID="+Eval("FNA_EvectionRoute_WriteOffID").ToString() %>'
                                             ForeColor="#CC0000"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
