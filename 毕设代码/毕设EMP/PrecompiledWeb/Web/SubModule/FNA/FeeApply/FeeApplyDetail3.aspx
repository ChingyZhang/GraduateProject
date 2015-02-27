<%@ page language="C#" autoeventwireup="true" masterpagefile="~/MasterPage/BasicMasterPage.master" inherits="SubModule_FNA_FeeApply_FeeApplyDetail3, App_Web_5zp237uh" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                        费用申请单详细信息
                                    </h2>
                                </td>
                                <td align="right">
                                    <asp:CheckBox ID="cbx_NoInsteadPayClient" runat="server" Text="确认无代垫客户" />
                                    <asp:Button ID="bt_ViewReport" runat="server" Text="查看全区月费用申请情况" Width="150px" Visible="false" />
                                    <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存草稿" Width="80px" />
                                    <asp:Button ID="bt_Submit" runat="server" Text="提交申请" Width="80px" OnClientClick="return confirm(&quot;提交申请后该项费用不可再修改了，确认提交申请?&quot;)"
                                        OnClick="bt_Submit_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Text="删除申请" Width="80px" OnClick="bt_Delete_Click"
                                        OnClientClick="return confirm(&quot;确定删除当前申请的费用?&quot;)" />
                                    <asp:Button ID="bt_Print" runat="server" OnClick="bt_Print_Click" Text="打印" Width="80px" />
                                    <asp:Button ID="bt_Cancel" runat="server" OnClientClick="return confirm(&quot;确认取消当前申请的费用?&quot;)"
                                        Text="取消费用" Width="80px" OnClick="bt_Cancel_Click" />
                                    <asp:Button ID="bt_Copy" runat="server" OnClick="bt_Copy_Click" OnClientClick="return confirm(&quot;是否确认重新激活该申请单?重新激活后将会自动生成一份完全相同的未提交的新申请单!&quot;)"
                                        Text="再次激活申请" Width="80px" />
                                    <asp:Button ID="bt_ViewWriteOff" runat="server" Text="查看关联核销记录" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr_BudgetInfo" visible="false">
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    预算额度信息</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tabForm">
                                        <tr>
                                            <td class="dataLabel" width="80px">
                                                预算分配总额
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_BudgetAmount" runat="server" ForeColor="Red"></asp:Label>
                                                元
                                            </td>
                                            <td class="dataLabel" width="120px">
                                                本月已申请费用总额
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_SubmitTotalCost" runat="server" ForeColor="Red"></asp:Label>
                                                元
                                            </td>
                                            <td class="dataLabel" width="120px">
                                                占预算总额比
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_TotalPercent" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td class="dataLabel" width="80px">
                                                剩余可用额度
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_BudgetBalance" runat="server" ForeColor="Red"></asp:Label>
                                                元
                                            </td>
                                            <td class="dataField" align="left">
                                                <asp:HyperLink ID="hl_ViewBudget" runat="server" ForeColor="#CC0000" NavigateUrl="~/SubModule/FNA/Budget/BudgetBalance.aspx">查看预算</asp:HyperLink>
                                            </td>
                                            <td class="dataField" align="left">
                                                <asp:Label ID="lb_GiftAmountBalance" runat="server" Text="0" Visible="false" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr_FeeRateInfo" visible="false">
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    指标销量与费用总额的费点信息</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tabForm">
                                        <tr>
                                            <td class="dataLabel" width="80px">
                                                指标销量
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_ForcastVolume" runat="server" ForeColor="Red"></asp:Label>
                                                元
                                            </td>
                                            <td class="dataLabel" width="120px">
                                                本月已申请费用总额
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_SubmitTotalCost1" runat="server" ForeColor="Red"></asp:Label>
                                                元
                                            </td>
                                            <td class="dataLabel" width="80px">
                                                预算余额
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_BudgetBalance1" runat="server" ForeColor="Red"></asp:Label>
                                                元
                                            </td>
                                            <td class="dataLabel" width="80px">
                                                <span style="color: #0000FF"><b>费点</b></span>
                                            </td>
                                            <td class="dataField" align="left">
                                                <asp:Label ID="lb_FeeRate" runat="server" ForeColor="Blue" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="pn_FeeApply" runat="server" DetailViewCode="Page_FNA_FeeApplyDetail">
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
                                        费用申请科目明细列表</h3>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_CancelWriteOff" runat="server" OnClick="bt_CancelWriteOff_Click"
                                        Text="取消费用" Width="88" />
                                    <asp:Button ID="bt_AddDetail" runat="server" OnClick="bt_AddDetail_Click" Text="新增明细"
                                        Width="80px" CausesValidation="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID,AccountTitle" OnRowDeleting="gv_List_RowDeleting" OnRowDataBound="gv_List_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" ToolTip="全选" onclick="javascript:SelectAll(this);">
                                        </asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cb_Selected" runat="server" Visible='<%# (int)Eval("Flag")==1 && (decimal)Eval("AvailCost")>0 %> ' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="发生客户">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_Client" runat="server" Text=""></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="联系人" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_RelateLinkMan" runat="server" NavigateUrl='' Text=""></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_RelateContract" runat="server" NavigateUrl='<%#GetContractPageURL((int)Eval("RelateContractDetail")) %>'
                                            Text="查看合同" Visible='<%#(int)Eval("RelateContractDetail")!=0 %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="listViewTdLinkS1" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AccountTitle" HeaderText="科目" SortExpression="AccountTitle" />
                                <asp:TemplateField HeaderText="陈列数量" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DisplayCount" Text='<%#GetDisplayCount((int)Eval("RelateContractDetail")) %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ApplyCost" HeaderText="申请额<br/>(我司)" SortExpression="ApplyCost"
                                    HtmlEncode="false" DataFormatString="{0:0.###元}" ItemStyle-ForeColor="Blue" />
                                <asp:BoundField DataField="DICost" HeaderText="经销商<br/>承担额" SortExpression="DICost"
                                    HtmlEncode="false" DataFormatString="{0:0.###元}" />
                                <asp:BoundField DataField="BeginMonth" HeaderText="发生月" SortExpression="BeginMonth" />
                                <asp:BoundField DataField="LastWriteOffMonth" HeaderText="最迟<br/>核销月" SortExpression="LastWriteOffMonth"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="Remark" HeaderText="说明" SortExpression="Remark" ItemStyle-Width="180px" />
                                <asp:TemplateField HeaderText="关联品牌" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_RelateBrand" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SalesForcast" HeaderText="预计销量" SortExpression="SalesForcast"
                                    HtmlEncode="false" DataFormatString="{0:0.###元}" />
                                <asp:TemplateField HeaderText="预计费率<br/>(我司)">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_PreCostRate" runat="server" Text='<%#(decimal)Eval("SalesForcast")==0?"0%":(((decimal)Eval("ApplyCost")+(decimal)Eval("AdjustCost"))/(decimal)Eval("SalesForcast")).ToString("0.0%") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="上月<br/>销量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_PreSalesVolumn" runat="server" Text='<%#GetPreSalesVolume((int)Eval("Client")).ToString("0.##") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="平均<br/>销量">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_PreSalesVolumn_Avage" runat="server" Text='<%#GetAvgSalesVolume((int)Eval("Client")).ToString("0.##") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="前一次<br/>申请信息" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_PreApplyInfo" runat="server" Text='<%#GetPreApplyInfo((int)Eval("ID"),(decimal)Eval("ApplyCost")) %>'
                                            ForeColor='<%#IsRed?System.Drawing.Color.Red:System.Drawing.Color.Black%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AdjustCost" HeaderText="调整额<br/>(我司)" DataFormatString="{0:0.###元}"
                                    HtmlEncode="False" SortExpression="AdjustCost" Visible="false" />
                                <asp:BoundField DataField="AdjustReason" HeaderText="调整原因<br/>(我司)" SortExpression="AdjustReason"
                                    HtmlEncode="False" Visible="false" />
                                <asp:BoundField DataField="DIAdjustCost" HeaderText="调整额<br/>(经销商)" DataFormatString="{0:0.###元}"
                                    HtmlEncode="False" SortExpression="DIAdjustCost" Visible="false" />
                                <asp:TemplateField HeaderText="调整原因<br/>(经销商)" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DIAdjustReason"  runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem,"[DIAdjustReason]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="批复额<br/>(我司)" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" ForeColor="Red" Font-Bold="true" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container.DataItem,"ApplyCost") + (decimal)DataBinder.Eval(Container.DataItem,"AdjustCost")).ToString("0.###元")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="批复额<br/>(经销商)" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ApproveDICost" ForeColor="Red" Font-Bold="true" runat="server"
                                            Text='<%# ((decimal)DataBinder.Eval(Container.DataItem,"DICost") + (decimal)DataBinder.Eval(Container.DataItem,"DIAdjustCost")).ToString("0.###元")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Flag" HeaderText="报销标志" SortExpression="Flag" Visible="false" />
                                <asp:BoundField DataField="AvailCost" HeaderText="可报销额" SortExpression="AvailCost"
                                    HtmlEncode="false" DataFormatString="{0:0.###元}" Visible="false" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_viewwriteofflist" runat="server" Text="查看核销" NavigateUrl='<%#Bind("ID","javascript:PopWriteOffListByDetailID({0})") %>'
                                            ControlStyle-CssClass="listViewTdLinkS1"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Button ID="bt_OpenAdjust" runat="server" Text="调整" OnClick="bt_OpenAdjust_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                </asp:CommandField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
                <tr>
                    <td align="Center" height="28" style="color: Blue">
                        本申请单费用合计：<asp:Label ID="lb_TotalCost" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>元
                        当月发生费用合计：<asp:Label ID="lb_TotalCost_ThisMonth" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>元
                        <asp:Label ID="lb_Percent" runat="server" ForeColor="Red" Font-Size="Larger" Text="占本月预算比："
                            Visible="false"></asp:Label>
                        提前预支费用合计：<asp:Label ID="lb_BorrowTotal" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>元
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="pn_Remark" runat="server" Visible="false" DetailViewCode="Page_FNA_FeeApplyDetail_Remark">
                                </mcs:UC_DetailView>
                                <table id="tbl_Remark" runat="server" visible="false" cellpadding="0" cellspacing="0"
                                    width="100%" border="0">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                                <tr>
                                                    <td>
                                                        <h3>
                                                            费用申请备注</h3>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" class="tabForm">
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="tbx_Remark" runat="server" TextMode="MultiLine" Width="90%" Rows="8"></asp:TextBox>
                                                        <span style="color: red">*</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Remark"
                                                            Display="Dynamic" ErrorMessage="必填" Enabled="false"></asp:RequiredFieldValidator>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="100" CanSetDefaultImage="false" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
