<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_StaffSalary_StaffSalarySetGenerateFlag, App_Web_llmqckq0" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                绩效名单确认</h2>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Button ID="BtnSelect" runat="server" Text="查找" Width="60px" OnClick="BtnSelect_Click" />
                                    <asp:Button ID="BtnSave" runat="server" Text="保存" Width="60px" OnClick="BtnSave_Click" />
                                    <asp:Button ID="BtnSubmit" runat="server" Text="提交" OnClientClick="return confirm(&quot;是否确认提交?&quot;)"
                                        OnClick="BtnSubmit_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" Text="审核" Width="60px" ToolTip="审核所选数据"
                                        OnClick="bt_Approve_Click" />
                                    <asp:Button ID="bt_CancelApprove" runat="server" Text="取消审核" Width="60px" ToolTip="取消审核所选导购员与底量"
                                        OnClick="bt_CancelApprove_Click"  />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tabForm">
                            <tr>
                                <td>
                                    管理片区
                                </td>
                                <td align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="160px" />
                                </td>
                                <td>
                                    会计月
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    营销人员
                                </td>
                                <td>
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="../../StaffManage/Pop_Search_Staff.aspx"
                                        Width="260px" />
                                </td>
                                <td class="dataLabel">
                                    <asp:RadioButtonList ID="rbl_ApproveFlag" runat="server" RepeatColumns="4" RepeatLayout="Flow"
                                        DataTextField="Value" DataValueField="Key">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0">
                            <Items>
                                <mcs:MCSTabItem Text="办事处主管目标" Value="1" />
                                <mcs:MCSTabItem Text="业代目标" Value="2" />
                            </Items>
                        </mcs:MCSTabControl>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_AccountMonth" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 800px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" DataKeyNames="FNA_StaffSalaryDataObject_ID" PageSize="20"
                                AllowSorting="true" PanelCode="Panel_FNA_StaffSalaryObjectSelect" Binded="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" Width="60px" Text="全选" AutoPostBack="False"
                                                Visible="false" onclick="javascript:SelectAll(this);"></asp:CheckBox>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbx" runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否执行<br/>生成绩效">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddl_Flag" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"FNA_StaffSalaryDataObject_Flag").ToString()%>'
                                                Width="60px" ToolTip="是否执行浮动底薪" Enabled='<%# DataBinder.Eval(Container.DataItem,"FNA_StaffSalaryDataObject_SubmitFlag").ToString()!="已审核" %>'>
                                                <asp:ListItem Text="请选择" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="否" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark" runat="server" Text='<%# Bind("FNA_StaffSalaryDataObject_Remark") %>'
                                                Enabled='<%# DataBinder.Eval(Container.DataItem,"FNA_StaffSalaryDataObject_SubmitFlag").ToString()!="已审核" %>'
                                                Width="120px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"Org_Staff_RealName").ToString() %>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </mcs:UC_GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSelect" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Approve" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_CancelApprove" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 55;
        divGridView.style.height = window.screen.availHeight - 400;            
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
