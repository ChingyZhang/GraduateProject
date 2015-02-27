<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_SalaryDetail_Search, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="~/Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 180px">
                                    <h2>
                                        导购员工资列表</h2>
                                </td>
                                <td class="dataLabel" style="width: 60px">
                                    管理片区
                                </td>
                                <td class="dataField" style="width: 268px">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="225px" />
                                </td>
                                <td style="width: 59px">
                                    会计月
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_BeginMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_AccountMonth_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    至
                                    <asp:DropDownList ID="ddl_EndMonth" runat="server" DataTextField="Name" DataValueField="ID"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_AccountMonth_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    导购员类别
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_PMClassify" DataTextField="Value" DataValueField="Key"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    导购员
                                </td>
                                <td>
                                    <mcs:MCSSelectControl runat="server" ID="select_promotor" Width="200px" PageUrl="Search_SelectPromotor.aspx" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_search" runat="server" Text="查 找" Width="60px" OnClick="bt_search_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr height="28px">
                        <td nowrap>
                            <h3>
                                工资明细列表</h3>
                        </td>
                        <td align="right" height="28">
                            合计工资总额：<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red"></asp:Label>
                            元
                        </td>
                        <td style="color: #FF0000" align="right">
                            <asp:CheckBox ID="cb_OnlyDisplayZero" runat="server" Text="仅显示提成为0的导购员" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px;" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" Width="2000px"
                                DataKeyNames="PM_SalaryDetail_ID" PanelCode="Panel_PM_Salary_Detail001" Binded="False"
                                ConditionString="" TotalRecordCount="0" AllowSorting="True" AllowPaging="True"
                                GridLines="Both" CellPadding="1" BackColor="#BBBBBB" CellSpacing="1" CssClass=""
                                BorderWidth="0px" PageSize="20" OrderFields="PM_Promotor_Name">
                                <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                <RowStyle BackColor="#FFFFFF" CssClass="" Height="28px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" >
                                        <ItemTemplate>
                                             
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="姓名" SortExpression="PM_Promotor_Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hy_PromotorName" runat="server" NavigateUrl='<%# "PM_PromotorDetail.aspx?PromotorID="+DataBinder.Eval(Container,"DataItem.PM_Promotor_ID").ToString() %>'
                                                Text='<%# DataBinder.Eval(Container,"DataItem.PM_Promotor_Name") %>' ToolTip="查看该导购员详细资料"
                                                CssClass="listViewTdLinkS1"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="身份证号">
                                        <ItemTemplate>
                                            <asp:Label ID="lt_IDCode" Text='<%# "A"+DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_IDCode").ToString() %>'
                                                Width="130px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="银行账号">
                                        <ItemTemplate>
                                            <asp:Label ID="lt_AccountCode" Text='<%# "A"+DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_AccountCode").ToString() %>'
                                                Width="135px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="所在门店">
                                        <ItemTemplate>
                                            <asp:Label ID="lt_PromotorInClient" Text='<%#PromotorInClient(Eval("PM_SalaryDetail_RetailerS").ToString()) %>'
                                                Width="120px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="我司薪酬明细→审批调整">
                                        <ItemTemplate>
                                            <asp:Label ID="tbx_PayAdjust_Approve" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_PayAdjust_Approve") %>'
                                                Enabled="false" Width="50px" ToolTip="审批调整"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="我司薪酬明细→调整原因">
                                        <ItemTemplate>
                                            <asp:Label ID="tbx_PayAdjust_Reason" runat="server" Enabled="false" Text="" Width="100px"
                                                ToolTip="调整原因"></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="我司薪酬明细→调整信息">
                                        <ItemTemplate>
                                            <input id="btn_ApproveRec" type="button" value="查看调整" visible="false" style="display: <%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_Remark").ToString() ==""?"none":"inherit" %>"
                                                onclick="MessageShow('<%#DataBinder.Eval(Container,"DataItem.PM_SalaryDetail_Remark").ToString() %>')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="费用A">
                                        <ItemTemplate>
                                            <asp:Label ID="lt_FeeA" Text='<%#((decimal)Eval("PM_SalaryDetail_Sum2")+(decimal)Eval("PM_SalaryDetail_CoPMFee")).ToString("#,#.#") %>'
                                                Width="60px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="费率A%">
                                        <ItemTemplate>
                                            <asp:Label ID="lt_PerFeeA" Text='<%# (decimal)DataBinder.Eval(Container.DataItem,"PM_SalaryDetail_ActSalesVolume")==0?"0":(((decimal)Eval("PM_SalaryDetail_Sum2")+(decimal)Eval("PM_SalaryDetail_CoPMFee"))/(decimal)Eval("PM_SalaryDetail_ActSalesVolume")*100).ToString("#,#.#") %>'
                                                Width="60px" runat="server" ToolTip="费率A百分比"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="奖惩与调整">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hy_dataobject" runat="server" NavigateUrl='<%# "PM_SalaryDataObject.aspx?PromotorID="+DataBinder.Eval(Container,"DataItem.PM_Promotor_ID").ToString()+"&AccountMonth="+ Eval("PM_Salary_AccountMonth") %>'
                                                Text="查看" ToolTip="查看奖惩与调整" CssClass="listViewTdLinkS1" Width="40px"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </mcs:UC_GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="bt_search" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
