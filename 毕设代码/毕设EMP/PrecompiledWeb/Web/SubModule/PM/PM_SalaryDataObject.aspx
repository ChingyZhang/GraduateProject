<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_SalaryDataObject, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Import Namespace="MCSFramework.BLL.Promotor" %>
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

    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                ����Ա������������趨</h2>
                        </td>
                        <td style="color: Red;">
                            <a href="DataObjectModelFieldDescribe.htm" target="_blank">
                                <h3>
                                    �ᱨ˵��</h3>
                            </a>
                            <asp:Label ID="lbl_message" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkHeader" runat="server" Text="ȫѡ" Width="60px" AutoPostBack="False"
                                        onclick="javascript:SelectAll(this);"></asp:CheckBox>
                                    <asp:Button ID="BtnSelect" runat="server" Text="����" Width="60px" OnClick="BtnSelect_Click" />
                                    <asp:Button ID="BtnSave" runat="server" Text="����" Width="60px" OnClick="BtnSave_Click" />
                                    <asp:Button ID="BtnDelete" runat="server" Text="����ɾ��" OnClientClick="return confirm(&quot;ɾ���������޷��ָ����Ƿ�ȷ��ɾ��?&quot;)"
                                        OnClick="BtnDelete_Click" Visible="false" />
                                    <asp:Button ID="bt_Approve" runat="server" Text="���" Width="60px" ToolTip="�����ѡ����Ա���������"
                                        OnClick="bt_Approve_Click" />
                                    <asp:Button ID="bt_CancelApprove" runat="server" Text="ȡ�����" Width="60px" ToolTip="ȡ�������ѡ����Ա�����"
                                        OnClick="bt_CancelApprove_Click" />
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
                                    ����Ƭ��
                                </td>
                                <td align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="160px" AutoPostBack="true" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td>
                                    �����
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    ������
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl runat="server" ID="select_Client" Width="200px" />
                                </td>
                                <td class="dataLabel">
                                    ����Ա
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_promotor" Width="120px" PageUrl="Search_SelectPromotor.aspx" />
                                </td>
                                <td class="dataLabel">
                                    ��˱�־
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ApproveFlag" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
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
                <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="1800px" AutoGenerateColumns="False"
                                PanelCode="Panel_PM_DataObjectList" AllowPaging="True" DataKeyNames="PM_SalaryDataObject_ID"
                                PageSize="15" Binded="False" OnPageIndexChanging="gv_List_PageIndexChanging"
                                GridLines="Both" CellPadding="1" BackColor="#CCCCCC" BorderWidth="0px" CellSpacing="1"
                                CssClass="" OnDataBound="gv_List_DataBound">
                                <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                <RowStyle BackColor="White" Height="28px" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="-">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbx" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ˢ�����">
                                        <ItemTemplate>
                                            <asp:Button ID="bt_refresh" runat="server" Text="ˢ ��" OnClick="bt_refresh_Click"
                                                OnClientClick="return confirm(&quot;�Ƿ�ȷ�϶Ըõ�����ɽ���ˢ�£�&quot;)" Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�Ƿ�ִ��<br/>������н">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddl_ISFloating" Visible='<%#GetIsFloatingEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString())) %>'
                                                SelectedValue='<%# DataBinder.Eval(Container.DataItem,"[PM_SalaryDataObject_ISFloating]").ToString()==""?"0":DataBinder.Eval(Container.DataItem,"[PM_SalaryDataObject_ISFloating]").ToString()%>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="60px" ToolTip="�Ƿ�ִ�и�����н">
                                                <asp:ListItem Text="��ѡ��" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="��" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="��" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ʵ��<br/>��������">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_ActWorkDays" runat="server" Text='<%# Bind("PM_SalaryDataObject_ActWorkDays","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"��ʵ�ʹ�������" %>'>
                                            </asp:TextBox><asp:CompareValidator ID="CompareValidator31" runat="server" ControlToValidate="tbx_ActWorkDays"
                                                ValueToCompare="-1" Display="Dynamic" ErrorMessage="����Ϊ���ڵ���0������" Operator="GreaterThan"
                                                Type="Integer"></asp:CompareValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�籣<br/>������">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data17" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data17","{0:0.###}") %>'
                                                Enabled='<%# GetInsureEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"���籣������"%>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator17" runat="server" ControlToValidate="tbx_Data17" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��ɵ������������">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data18" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data18","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"����ɵ���" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator18" runat="server" ControlToValidate="tbx_Data18" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��ɵ�������ע">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark1" runat="server" Text='<%# Bind("PM_SalaryDataObject_Remark1") %>'
                                                Width="100px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"����ɵ�����ע" %>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="���͡����۽���">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data01" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data01","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"�����۽���" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator01" runat="server" ControlToValidate="tbx_Data01" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="���͡�VIP����">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data02" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data02","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"��VIP����" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator02" runat="server" ControlToValidate="tbx_Data02" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="���͡����㵼��">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data03" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data03","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"�����㵼��" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator03" runat="server" ControlToValidate="tbx_Data03" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="���͡�����">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data09" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data09","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"������������" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator09" runat="server" ControlToValidate="tbx_Data09" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="���͡�������ע">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark2" runat="server" Text='<%# Bind("PM_SalaryDataObject_Remark2") %>'
                                                Width="120px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"������������ע" %>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="���͡�����С��">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Data10" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data10","{0:0.###}") %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"������С��" %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ����������">
                                        <ItemTemplate>
                                            <asp:Label ID="PM_PromotorSalary_RTManageCost" runat="server" Text='<%# Bind("PM_PromotorSalary_RTManageCost","{0:0.###}") %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"�����������" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ��Ӽ����">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data20" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data20","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"������ѵ������" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator20" runat="server" ControlToValidate="tbx_Data20" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ�����(��)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data21" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data21","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"������(��)" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator21" runat="server" ControlToValidate="tbx_Data21" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ���ѵ��">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data22" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data22","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"����ѵ��" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator22" runat="server" ControlToValidate="tbx_Data22" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ�����֤">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data23" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data23","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"������֤" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator23" runat="server" ControlToValidate="tbx_Data23" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ��¹��">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data24" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data24","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"���¹��" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator24" runat="server" ControlToValidate="tbx_Data24" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ�����(��)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data25" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data25","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"������(��)" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator25" runat="server" ControlToValidate="tbx_Data25" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ�����">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data29" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data29","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"������" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator29" runat="server" ControlToValidate="tbx_Data29" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ���ע">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark4" runat="server" Text='<%# Bind("PM_SalaryDataObject_Remark4") %>'
                                                Width="120px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"����ע" %>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������������ѡ�С��">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Data30" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data30","{0:0.###}") %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"��С��" %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�����̳е��������������">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Data19" runat="server" Text='<%# Bind("PM_SalaryDataObject_Data19","{0:0.###}") %>'
                                                Enabled='<%# GetEnable(int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_Promotor").ToString()),int.Parse(DataBinder.Eval(Container.DataItem,"PM_SalaryDataObject_ApproveFlag").ToString())) %>'
                                                Width="30px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"�������̳е��������" %>'></asp:TextBox><asp:CompareValidator
                                                    ID="CompareValidator19" runat="server" ControlToValidate="tbx_Data19" Display="Dynamic"
                                                    ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�����̳е���������ע">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_Remark3" runat="server" Text='<%# Bind("PM_SalaryDataObject_Remark3") %>'
                                                Width="120px" ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"�������̳е�������ע" %>'></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </mcs:UC_GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSelect" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnDelete" EventName="Click" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
