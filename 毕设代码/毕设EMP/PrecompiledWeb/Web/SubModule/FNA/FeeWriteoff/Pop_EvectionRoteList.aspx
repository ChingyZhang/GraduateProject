<%@ page language="C#" autoeventwireup="true" inherits="SubModule_FNA_FeeWriteoff_Pop_EvectionRoteList, App_Web_lxhzl6y2" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head id="Head1" runat="server">
    <title>差旅行程详细信息</title>
    <base target="_self">
    </base>

    <script src="../../../App_Themes/basic/meizzDate.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
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
                                        <asp:Button ID="bt_Add" runat="server" Text="加入行程" Width="60px" OnClick="bt_Add_Click" />
                                        <asp:Button ID="bt_Remove" runat="server" Text="移除行程" Width="60px" OnClick="bt_Remove_Click" />
                                        <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="返回" Width="60px" />&nbsp;
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
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                <Items>
                                    <mcs:MCSTabItem Text="包含的行程" Value="0" />
                                    <mcs:MCSTabItem Text="加入新行程" Value="1" />
                                    <mcs:MCSTabItem Text="已批复出差申请" Value="2" />
                                </Items>
                            </mcs:MCSTabControl>
                        </td>
                    </tr>
                    <tr class="tabForm">
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table id="tb_condition" runat="server" visible="false" border="0" cellpadding="0"
                                            cellspacing="0" width="100%">
                                            <tr>
                                                <td>
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
                                                                    <td align="right">
                                                                        <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                            PanelCode="Panel_FNA_EvectionRouteList_01" DataKeyNames="FNA_EvectionRoute_ID,Car_DispatchRide_ID"
                                            AllowPaging="True" PageSize="15">
                                            <Columns>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbx" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Button ID="bt_OpenJournal" runat="server" CausesValidation="False" CommandName="Select"
                                                            Text="查看日志" OnClientClick='<%# "javascript:OpenJournal(" + Eval("FNA_EvectionRoute_RelateJournal").ToString() +")"  %>'
                                                            Visible='<%#Eval("FNA_EvectionRoute_RelateJournal").ToString()!="" %>' Width="60px"/>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="button" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                        <mcs:UC_GridView ID="gv_WorkPlanDetailList" runat="server" Width="100%" AutoGenerateColumns="False"
                                            PanelCode="Panel_OA_WorkingPlan_DetailList" DataKeyNames="JN_WorkingPlanDetail_ID"
                                            AllowPaging="True" PageSize="15" Visible="false">
                                            <Columns>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Button ID="bt_OpenWorkPlan" runat="server" CausesValidation="False" CommandName="Select"
                                                            Text="查看计划" OnClientClick='<%# "javascript:OpenWorkingPlan(" + Eval("JN_WorkingPlanDetail_WorkingPlan").ToString() +")"  %>'
                                                            Visible='<%#Eval("JN_WorkingPlanDetail_ID").ToString()!="" %>' Width="60px" />
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="button" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html> 