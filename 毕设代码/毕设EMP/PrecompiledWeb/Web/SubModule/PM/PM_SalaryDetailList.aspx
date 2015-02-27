<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_SalaryDetailList, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="~/Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function MessageShow(mesg) {
            alert(mesg);
        }
    
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="bt_Export" />
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        ����Ա�����б�</h2>
                                </td>
                                <td class="dataLabel">
                                    ����Ƭ��
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="180px" />
                                </td>
                                <td class="dataLabel">
                                    ����Ա
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_promotor" Width="200px" PageUrl="Search_SelectPromotor.aspx" />
                                    <asp:TextBox ID="txt_Promotor" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_search" runat="server" Text="�� ��" Width="60px" OnClick="bt_search_Click" />
                                    <asp:Button ID="bt_Save" runat="server" Text="�� ��" Width="60px" OnClick="bt_Save_Click"
                                        Visible="false" ValidationGroup="1" />
                                    <asp:Button ID="bt_Merge" runat="server" Text="�ϲ����ϼ����й��ʵ�" Width="124px" OnClientClick="return confirm(&quot;�ϲ����ʵ��󲻿ɻָ����Ƿ�ȷ�Ϻϲ����ʵ�?&quot;)"
                                        OnClick="bt_Merge_Click" Visible="false" />
                                    <asp:Button ID="bt_Delete" runat="server" OnClick="bt_Delete_Click" OnClientClick="return confirm(&quot;ɾ�������ݽ����ɻָ����Ƿ�ȷ��ɾ����������?&quot;)"
                                        Text="ɾ��" Width="60px" />
                                    <asp:Button ID="bt_Submit" runat="server" Text="�� ��" Width="60px" ValidationGroup="1"
                                        OnClick="bt_Submit_Click" OnClientClick="return confirm(&quot;���ٴ�ȷ�ϵ�����������������������������Ŀ��Ϣ��&quot;)" />
                                    <asp:Button ID="bt_Export" runat="server" Text="����Excel" OnClick="bt_Export_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" id="tbl_BudgetInfo">
                            <tr>
                                <%--  <td class="dataLabel" width="80px">
                                    Ԥ������ܶ�
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lb_BudgetAmount" runat="server" ForeColor="Red"></asp:Label>
                                    Ԫ
                                </td>
                                <td class="dataLabel" width="80px">
                                    ʣ����ö��
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lb_BudgetBalance" runat="server" ForeColor="Red"></asp:Label>
                                    Ԫ
                                </td>--%>
                                <td class="dataLabel" width="80px">
                                    ����Ա����
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lbl_PromotorCount" runat="server" ForeColor="Red"></asp:Label>
                                    ��
                                </td>
                                <td class="dataLabel" width="80px">
                                    �ŵ������۶�
                                </td>
                                <td class="dataField">
                                    <asp:Label ID="lbl_SalesAmount" runat="server" ForeColor="Red"></asp:Label>
                                    Ԫ
                                </td>
                                <td class="dataLabel" style="color: #FF0000" align="right">
                                    <asp:CheckBox ID="cb_OnlyDisplayZero" runat="server" Text="����ʾ���Ϊ0�ĵ���Ա" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="pn_detail" runat="server" DetailViewCode="Page_PM_SalaryDetail">
                                </mcs:UC_DetailView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        ������ϸ�б�</h3>
                                </td>
                                <td align="right" height="28">
                                    �ϼƹ����ܶ<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red"></asp:Label>
                                    Ԫ
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnCancel" runat="server" Text="ȡ�������ɵĵ�����Ա��������" Visible="false" OnClick="btnCancel_Click" />
                                    <asp:Button ID="bt_SaveChange" runat="server" Text="�������" OnClick="bt_SaveChange_Click"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divGridView" style="overflow: scroll; height: 500px" align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" Width="2800"
                                        DataKeyNames="PM_SalaryDetail_ID" PanelCode="Panel_PM_Salary_Detail001" Binded="False"
                                        ConditionString="" TotalRecordCount="0" AllowSorting="True" AllowPaging="True"
                                        GridLines="Both" CellPadding="1" BackColor="#BBBBBB" CellSpacing="1" CssClass=""
                                        BorderWidth="0px" PageSize="20" OrderFields="PM_Promotor_Name" OnRowDataBound="gv_List_RowDataBound">
                                        <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                        <RowStyle BackColor="#FFFFFF" CssClass="" Height="28px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ѡ��" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_ID" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="����" SortExpression="PM_Promotor_Name">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hy_PromotorName" runat="server" NavigateUrl='<%# "PM_PromotorDetail.aspx?PromotorID="+DataBinder.Eval(Container,"DataItem.PM_Promotor_ID").ToString() %>'
                                                        Text='<%# DataBinder.Eval(Container,"DataItem.PM_Promotor_Name") %>' ToolTip="�鿴�õ���Ա��ϸ����"
                                                        CssClass="listViewTdLinkS1"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���֤��">
                                                <ItemTemplate>
                                                    <asp:Label ID="lt_IDCode" Text='<%# "A"+DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_IDCode").ToString() %>'
                                                        Width="130px" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�����˺�">
                                                <ItemTemplate>
                                                    <asp:Label ID="lt_AccountCode" Text='<%# "A"+DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_AccountCode").ToString() %>'
                                                        Width="135px" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�����ŵ�">
                                                <ItemTemplate>
                                                    <asp:Label ID="lt_PromotorInClient" Text='<%#PromotorInClient(Eval("PM_SalaryDetail_RetailerS").ToString()) %>'
                                                        Width="120px" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��˾н����ϸ����������">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PayAdjust_Approve" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PayAdjust_Approve") %>'
                                                        Enabled='<%#enableflag %>' Width="50px" Visible='<%#!visibleflag %>' ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"����������"%>'></asp:TextBox><asp:CompareValidator
                                                            ID="CompareValidator14" runat="server" ControlToValidate="tbx_PayAdjust_Approve"
                                                            Display="Dynamic" ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"
                                                            ValidationGroup="1"></asp:CompareValidator>
                                                    <asp:Label ID="lt_PayAdjust_Approve" Text='<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PayAdjust_Approve")%>'
                                                        runat="server" Visible='<%#visibleflag %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��˾н����ϸ������ԭ��">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PayAdjust_Reason" runat="server" Enabled='<%#enableflag  %>'
                                                        Text="" Width="100px" Visible='<%#!visibleflag %>' ToolTip="����ԭ��"></asp:TextBox>
                                                    <asp:Label ID="lt_PayAdjust_Reason" Text='<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_Remark")%>'
                                                        runat="server" Visible='<%#visibleflag %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="��˾н����ϸ��������Ϣ">
                                                <ItemTemplate>
                                                    <input id="btn_ApproveRec" type="button" value="�鿴����" visible="false" style="display: <%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_Remark").ToString() ==""?"none":"inherit" %>"
                                                        onclick="MessageShow('<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_Remark").ToString() %>')" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�����̽ṹ����������">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_DIPayAdjust_Approve" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_DIPayAdjust_Approve") %>'
                                                        Enabled='<%#enableflag %>' Width="50px" Visible='<%#!visibleflag %>' ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"����������"%>'></asp:TextBox><asp:CompareValidator
                                                            ID="CompareValidator15" runat="server" ControlToValidate="tbx_DIPayAdjust_Approve"
                                                            Display="Dynamic" ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"
                                                            ValidationGroup="1"></asp:CompareValidator>
                                                    <asp:Label ID="lt_DIPayAdjust_Approve" Text='<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_DIPayAdjust_Approve")%>'
                                                        runat="server" Visible='<%#visibleflag %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�����̽ṹ������ԭ��">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_DIPayAdjust_Reason" runat="server" Enabled='<%#enableflag  %>'
                                                        Text="" Width="100px" Visible='<%#!visibleflag %>' ToolTip="����ԭ��"></asp:TextBox>
                                                    <asp:Label ID="lt_DIPayAdjust_Reason" Text='<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_DI_Remark")%>'
                                                        runat="server" Visible='<%#visibleflag %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�����̽ṹ��������Ϣ">
                                                <ItemTemplate>
                                                    <input id="btn_ApproveRec2" type="button" value="�鿴����" visible="false" style="display: <%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_DI_Remark").ToString() ==""?"none":"inherit" %>"
                                                        onclick="MessageShow('<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_DI_Remark").ToString() %>')" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�������ϸ����������">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PMFee_Adjust" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PMFee_Adjust") %>'
                                                        Enabled='<%#enableflag %>' Width="50px" Visible='<%#!visibleflag %>' ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"����������"%>'></asp:TextBox><asp:CompareValidator
                                                            ID="CompareValidator122" runat="server" ControlToValidate="tbx_PMFee_Adjust"
                                                            Display="Dynamic" ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"
                                                            ValidationGroup="1"></asp:CompareValidator>
                                                    <asp:Label ID="lt_PMFee_Adjust" Text='<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PMFee_Adjust")%>'
                                                        runat="server" Visible='<%#visibleflag %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�������ϸ����������">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PMFee1_Adjust" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PMFee1_Adjust") %>'
                                                        Enabled='<%#enableflag %>' Width="50px" Visible='<%#!visibleflag %>' ToolTip='<%#DataBinder.Eval(Container.DataItem,"PM_Promotor_Name").ToString()+"����������"%>'></asp:TextBox><asp:CompareValidator
                                                            ID="CompareValidator312" runat="server" ControlToValidate="tbx_PMFee1_Adjust"
                                                            Display="Dynamic" ErrorMessage="����������" Operator="DataTypeCheck" Type="Double"
                                                            ValidationGroup="1"></asp:CompareValidator>
                                                    <asp:Label ID="lt_PMFee1_Adjust" Text='<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PMFee1_Adjust")%>'
                                                        runat="server" Visible='<%#visibleflag %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�������ϸ������ԭ��">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_PMFee_Adjust_Reason" runat="server" Enabled='<%#enableflag  %>'
                                                        Text="" Width="100px" Visible='<%#!visibleflag %>' ToolTip="����ԭ��"></asp:TextBox>
                                                    <asp:Label ID="lt_PMFee_Remark" Text='<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PMFee_Remark")%>'
                                                        runat="server" Visible='<%#visibleflag %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="�������ϸ��������Ϣ">
                                                <ItemTemplate>
                                                    <input id="btn_ApproveRec3" type="button" value="�鿴����" visible="false" style="display: <%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PMFee_Remark").ToString() ==""?"none":"inherit" %>"
                                                        onclick="MessageShow('<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PMFee_Remark").ToString() %>')" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="����A">
                                                <ItemTemplate>
                                                    <asp:Label ID="lt_FeeA" Text='<%#((decimal)Eval("PM_SalaryDetail_Sum2")+(decimal)Eval("PM_SalaryDetail_CoPMFee")).ToString("#,#.#") %>'
                                                        Width="60px" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="����A%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lt_PerFeeA" Text='<%# (decimal)DataBinder.Eval(Container.DataItem,"PM_SalaryDetail_ActSalesVolume")==0?"0":(((decimal)Eval("PM_SalaryDetail_Sum2")+(decimal)Eval("PM_SalaryDetail_CoPMFee"))/(decimal)Eval("PM_SalaryDetail_ActSalesVolume")*100).ToString("#,#.#") %>'
                                                        Width="60px" runat="server" ToolTip="����A�ٷֱ�"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="���������">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hy_dataobject" runat="server" NavigateUrl='<%# "PM_SalaryDataObject.aspx?PromotorID="+DataBinder.Eval(Container,"DataItem.PM_Promotor_ID").ToString()+"&AccountMonth="+ GetAccountMonth() %>'
                                                        Text="�鿴" ToolTip="�鿴���������" CssClass="listViewTdLinkS1" Width="40px"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_SaveChange" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_search" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript">
        divGridView.style.width = window.screen.availWidth - 40;
        divGridView.style.height = window.screen.availHeight - 450;            
    </script>

    <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="110" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
