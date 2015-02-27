<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_StaffSalary_StaffSalaryDataObjectList, App_Web_llmqckq0" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function SelectAll(tempControl) {
            var theBox = tempControl;
            sState = theBox.checked;
            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++) {
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id && elem[i].id != "ctl00_ContentPlaceHolder1_chk_Header") {
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
                                Ӫ����Ա����Ŀ���趨</h2>
                        </td>
                        <td style="color: Red;">
                            <asp:Label ID="lbl_message" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chk_Header" runat="server" Text="ȫѡ(Ƭ����)" AutoPostBack="False">
                                    </asp:CheckBox>
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
                                        AutoPostBack="true" ParentColumnName="SuperID" Width="160px" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                                <td>
                                    �����
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_AccountMonth_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    Ӫ����Ա
                                </td>
                                <td>
                                    <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="../../StaffManage/Pop_Search_Staff.aspx"
                                        Width="260px" />
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
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                            SelectedIndex="0">
                            <Items>
                                <mcs:MCSTabItem Text="���´�����Ŀ��" Value="1" />
                                <mcs:MCSTabItem Text="ҵ��Ŀ��" Value="2" />
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
                <div id="divGridView" style="overflow: scroll; height: 800px" width="100%" align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" DataKeyNames="FNA_StaffSalaryDataObject_ID" PageSize="20"
                                AllowSorting="true" OrderFields="Org_Staff_OrganizeCity2,Org_Staff_OrganizeCity3,Org_Staff_OrganizeCity4"
                                PanelCode="Panel_FNA_StaffSalaryDataObject" Binded="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" Width="60px" Text="ȫѡ" AutoPostBack="False"
                                                onclick="javascript:SelectAll(this);"></asp:CheckBox>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbx" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ҵ��Ŀ��">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SalesTarget" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FNA_StaffSalaryDataObject_SalesTarget") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="������Ŀ��<br/>��������%" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_SalesTargetAdujstRate" runat="server" Text='<%# Bind("FNA_StaffSalaryDataObject_Data10","{0:0.###}") %>'
                                                Width="40px" AutoPostBack="true" ToolTip='<%#DataBinder.Eval(Container.DataItem,"Org_Staff_RealName").ToString()+"��������Ŀ�����(����5%����)" %>'
                                                Enabled='<%# DataBinder.Eval(Container.DataItem,"FNA_StaffSalaryDataObject_ApproveFlag").ToString()!="�����" %>'
                                                OnTextChanged="tbx_SalesTargetAdujstRate_TextChanged">
                                            </asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_SalesTargetAdujstRate"
                                                Display="Dynamic" ErrorMessage="����������" Operator="DataTypeCheck" Type="Double">
                                            </asp:CompareValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������Ŀ��<br/>�������" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbx_SalesTargetAdujst" runat="server" Text='<%# Bind("FNA_StaffSalaryDataObject_SalesTargetAdujst","{0:0.###}") %>'
                                                Enabled='<%# DataBinder.Eval(Container.DataItem,"FNA_StaffSalaryDataObject_ApproveFlag").ToString()!="�����" %>'
                                                Width="60px" AutoPostBack="true" ToolTip='<%#DataBinder.Eval(Container.DataItem,"Org_Staff_RealName").ToString()+"��������Ŀ�����(����5%����)" %>'
                                                OnTextChanged="tbx_SalesTargetAdujst_TextChanged">
                                            </asp:TextBox><asp:CompareValidator ID="CompareValidator31" runat="server" ControlToValidate="tbx_SalesTargetAdujst"
                                                Display="Dynamic" ErrorMessage="����������" Operator="DataTypeCheck" Type="Double">
                                            </asp:CompareValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="��" Visible="false">
                                        <ItemTemplate>
                                            <asp:Button ID="bt_refresh" runat="server" Text="����ˢ��" OnClick="bt_refresh_Click" />
                                        </ItemTemplate>
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
                            <asp:AsyncPostBackTrigger ControlID="MCSTabControl1" EventName="OnTabClicked" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_AccountMonth" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="tr_OrganizeCity" EventName="selected" />
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
