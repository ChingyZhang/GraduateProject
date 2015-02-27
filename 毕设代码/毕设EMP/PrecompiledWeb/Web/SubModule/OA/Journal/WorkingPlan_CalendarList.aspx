<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_OA_Journal_WorkingPlan_CalendarList, App_Web_n8pevkz9" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="~/Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                工作计划排期表<asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Create" runat="server" OnClick="bt_Create_Click" Text="生成计划" Width="60px" />
                            <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存计划" Width="60px" />
                            <asp:Button ID="bt_Submit" runat="server" OnClick="bt_Submit_Click" Text="提交审批" />
                            <asp:Button ID="bt_Delete" runat="server" CausesValidation="False" OnClick="bt_Delete_Click"
                                OnClientClick="return confirm(&quot;是否确认删除该工作计划?&quot;)" Text="删除计划" />
                            <asp:Button ID="bt_Journal" runat="server" OnClick="bt_Journal_Click" Text="实际工作日志"
                                Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                                        <tr>
                                            <td height="28px" width="60px" class="dataLabel">
                                                管理片区
                                            </td>
                                            <td class="dataField" width="160px">
                                                <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" Width="160px" DisplayRoot="True" AutoPostBack="True"
                                                    OnSelected="tr_OrganizeCity_Selected" />
                                            </td>
                                            <td width="60px" class="dataLabel">
                                                开始日期
                                            </td>
                                            <td class="dataField" width="200px">
                                                <asp:TextBox ID="tbx_begindate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begindate"
                                                    Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                至<asp:TextBox ID="tbx_enddate" runat="server" onfocus="setday(this)" Width="70px"></asp:TextBox>
                                                <span style="color: #FF0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                    runat="server" ControlToValidate="tbx_begindate" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_begindate"
                                                    Display="Dynamic" ErrorMessage="格式错误" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                            </td>
                                            <td width="60" class="dataLabel">
                                                计划员工
                                            </td>
                                            <td class="dataField" width="160px">
                                                <mcs:MCSSelectControl ID="select_PlanStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                                    Width="150" OnSelectChange="select_PlanStaff_SelectChange" />
                                            </td>
                                            <td align="left" class="dataLabel">
                                                <asp:CheckBox ID="cbx_GenarateSynergetic" runat="server" Text="生成所有下级员工的协同拜访计划" ToolTip="如果下级员工有很多,则会生成计划所需时间会比较久" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_OA_WorkingPlan_01">
                                                </mcs:UC_DetailView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tr_adddetail" runat="server">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="h3Row" height="28px">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                增加计划工作项</h3>
                                                        </td>
                                                        <td align="right">
                                                            <asp:CheckBox ID="cb_DisplayCheckedOnly" runat="server" AutoPostBack="True" OnCheckedChanged="cb_DisplayCheckedOnly_CheckedChanged"
                                                                Text="仅显示选中项" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="bt_AddDetail" runat="server" Text="新增工作项" Width="80px" UseSubmitBehavior="False"
                                                                OnClick="bt_AddDetail_Click" ValidationGroup="AddDetail" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                                                    <tr>
                                                        <td height="28px" class="dataLabel" width="60">
                                                            工作类型
                                                        </td>
                                                        <td class="dataField" width="100px">
                                                            <asp:DropDownList ID="ddl_WorkingClassify" runat="server" DataTextField="Value" DataValueField="Key"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddl_WorkingClassify_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="dataLabel" width="60">
                                                            <asp:Label ID="lb_WorkingContent" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td class="dataField">
                                                            <mcs:MCSSelectControl ID="select_RelateStaff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx?MultiSelected=N"
                                                                Width="200px" Visible="false" />
                                                            <mcs:MCSSelectControl ID="select_RelateClient" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx"
                                                                Width="240px" OnSelectChange="select_RelateClient_SelectChange" Visible="false" />
                                                        </td>
                                                        <td class="dataLabel" width="60">
                                                            目的城市
                                                        </td>
                                                        <td class="dataField" width="190px">
                                                            <mcs:MCSTreeControl ID="tr_OfficailCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                                ParentColumnName="SuperID" Width="180px" DisplayRoot="True" TableName="MCS_SYS.dbo.Addr_OfficialCity" />
                                                        </td>
                                                        <td class="dataLabel" width="60">
                                                            工作内容
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:TextBox ID="tbx_Description" Width="240px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataLabel" width="60px">
                                                            开始日期
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:TextBox ID="tbx_Begin" runat="server" Width="70px" onfocus="setday(this)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_Begin"
                                                                Display="Dynamic" ErrorMessage="必填" ValidationGroup="AddDetail"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_BeginDate"
                                                                Display="Dynamic" ErrorMessage="必需为日期型" Operator="DataTypeCheck" Type="Date"
                                                                ValidationGroup="AddDetail"></asp:CompareValidator>
                                                        </td>
                                                        <td class="dataLabel" width="60px">
                                                            截止日期
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:TextBox ID="tbx_End" runat="server" Width="70px" onfocus="setday(this)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbx_End"
                                                                Display="Dynamic" ErrorMessage="必填" ValidationGroup="AddDetail"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbx_BeginDate"
                                                                Display="Dynamic" ErrorMessage="必需为日期型" Operator="DataTypeCheck" Type="Date"
                                                                ValidationGroup="AddDetail"></asp:CompareValidator>
                                                        </td>
                                                        <td class="dataLabel" width="60">
                                                            预计交通费
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:TextBox ID="tbx_Cost1" runat="server" Width="70px"></asp:TextBox>
                                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="tbx_Cost1"
                                                                Display="Dynamic" ErrorMessage="必需为数字型" Operator="DataTypeCheck" Type="Double"
                                                                ValidationGroup="AddDetail"></asp:CompareValidator>
                                                        </td>
                                                        <td class="dataLabel" width="60">
                                                            预计住宿费
                                                        </td>
                                                        <td class="dataField">
                                                            <asp:TextBox ID="tbx_Cost2" runat="server" Width="70px"></asp:TextBox>
                                                            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="tbx_Cost2"
                                                                Display="Dynamic" ErrorMessage="必需为数字型" Operator="DataTypeCheck" Type="Double"
                                                                ValidationGroup="AddDetail"></asp:CompareValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr height="1px">
            <td>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div id="divGridView" style="overflow: scroll; height: 400px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="SortID"
                                Width="100%" OnRowCreated="gv_List_RowCreated" OnRowDeleting="gv_List_RowDeleting"
                                Binded="False" ConditionString="" PanelCode="" TotalRecordCount="0" OrderFields="">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                Text="删除" CssClass="listViewTdLinkS1" OnClientClick="return confirm('是否确定删除该行记录？')"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SortID" HeaderText="序号" SortExpression="SortID" ItemStyle-Width="30px">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="WorkingClassifyName" HeaderText="工作类型" ItemStyle-Width="60px">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="工作内容">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("RelateStaffName") %>'></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("RelateClientName") %>'></asp:Label>
                                            <asp:TextBox ID="tbx_Description" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="130px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="OfficialCityName" HeaderText="城市" ItemStyle-Width="130px">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="预计交通费" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Cost1" runat="server" Text='<%# Bind("Cost1") %>' Width="30px"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator_Cost1" runat="server" ControlToValidate="tbx_Cost1"
                                                Display="Dynamic" ErrorMessage="必需为数字型" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="AddDetail"></asp:CompareValidator>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="预计住宿费" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Cost2" runat="server" Text='<%# Bind("Cost2") %>' Width="30px"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator_Cost2" runat="server" ControlToValidate="tbx_Cost2"
                                                Display="Dynamic" ErrorMessage="必需为数字型" Operator="DataTypeCheck" Type="Double"
                                                ValidationGroup="AddDetail"></asp:CompareValidator>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Counts" HeaderText="天数" ItemStyle-Width="70px">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无数据
                                </EmptyDataTemplate>
                            </mcs:UC_GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_AddDetail" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cb_DisplayCheckedOnly" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="91" />
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 60;
        //divGridView.style.width = 1024;
        divGridView.style.height = window.screen.availHeight - 400;         
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
