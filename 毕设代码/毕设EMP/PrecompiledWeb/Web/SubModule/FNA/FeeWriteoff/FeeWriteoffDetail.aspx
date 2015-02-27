<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_FeeWriteoff_FeeWriteoffDetail, App_Web_lxhzl6y2" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Import Namespace="MCSFramework.BLL.Pub" %>
<%@ Register Src="../../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
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
                                        费用核销详细信息
                                    </h2>
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" id="tbl_BudgetInfo"
                                        visible="false">
                                        <tr>
                                            <td class="dataLabel" width="80px">
                                                预算分配总额
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_BudgetAmount" runat="server" ForeColor="Red"></asp:Label>
                                                元
                                            </td>
                                            <td class="dataLabel" width="80px">
                                                剩余可用额度
                                            </td>
                                            <td class="dataField">
                                                <asp:Label ID="lb_BudgetBalance" runat="server" ForeColor="Red"></asp:Label>
                                                元
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Delete" runat="server" OnClientClick="return confirm(&quot;删除后将数据将不可恢复，是否确认删除?&quot;)"
                                        Text="删 除" Width="60px" OnClick="bt_Delete_Click" />
                                    <asp:Button ID="bt_Save" runat="server" Text="保 存" Width="60px" OnClick="bt_Save_Click" />
                                    <asp:Button ID="bt_Submit" runat="server" Text="提 交" Width="60px" OnClientClick="return confirm(&quot;提交申请后，且该费用核销单将不可再修改了，确认提交申请?&quot;)"
                                        OnClick="bt_Submit_Click" />
                                    <asp:Button ID="bt_Taxes" runat="server" Text="税 金" OnClick="bt_Taxes_Click" />
                                    <asp:Button ID="bt_Print" runat="server" OnClick="bt_Print_Click" Text="打印(业务报账)"
                                        Width="100px" />
                                    <asp:Button ID="bt_Print2" runat="server" Text="打印(会计审核)" Width="100px" OnClick="bt_Print2_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_DetailView ID="pn_FeeWriteOff" runat="server" DetailViewCode="Page_FNA_FeeWriteOffDetail">
                        </mcs:UC_DetailView>
                    </td>
                </tr>
                <tr>
                    <td height="28px">
                        <font color="red">注：如果费用由经销商代垫，则请选择代垫的客户，否则请选择费用代垫员工！</font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr height="28px">
                                        <td nowrap>
                                            <h3>
                                                报销单科目明细</h3>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="bt_EditEvectionRoute" runat="server" Text="查看行程明细" Width="88px" OnClick="bt_EditEvectionRoute_Click" />
                                            <asp:Button ID="bt_EditWriteOffDetail" runat="server" Text="编辑核销明细" Width="88px"
                                                OnClick="bt_EditWriteOffDetail_Click" CausesValidation="False" />
                                            <asp:Button ID="bt_AddTitleNoApply" runat="server" CausesValidation="False" OnClick="bt_AddTitleNoApply_Click"
                                                Text="编辑核销明细" Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="ID,ApplyDetailID" OnRowDeleting="gv_List_RowDeleting" OnRowDataBound="gv_List_RowDataBound"
                            OnRowCommand="gv_List_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="申请单备案号" SortExpression="ApplyDetailID">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_ApplySheetCode" runat="server" Text="" NavigateUrl="" ForeColor="#CC0000"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="发生客户" SortExpression="Client">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_Client" runat="server" Text="" NavigateUrl="" ForeColor="#CC0000"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AccountTitle" HeaderText="科目" SortExpression="AccountTitle" />
                                <asp:BoundField DataField="ApplyCost" DataFormatString="{0:0.###元}" HeaderText="可核销额"
                                    HtmlEncode="False" SortExpression="ApplyCost" Visible="false" />
                                <asp:BoundField DataField="WriteOffCost" DataFormatString="{0:0.###元}" HeaderText="申请核销额"
                                    HtmlEncode="False" SortExpression="WriteOffCost" />
                                <asp:BoundField DataField="BeginMonth" HeaderText="发生月份" SortExpression="BeginMonth" />
                                <asp:BoundField DataField="BalanceMode" HeaderText="结余方式" SortExpression="BalanceMode"
                                    Visible="false" />
                                <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" />
                                <asp:TemplateField HeaderText="批复核销额">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_CanWriteOffCost" ForeColor="Red" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container,"DataItem.WriteOffCost")+
                                        (decimal)DataBinder.Eval(Container,"DataItem.AdjustCost")).ToString("0.###元")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否逾期" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_DelayMonth" ForeColor="Red" runat="server" Text='<%#GetISDelay( (int)Eval("ApplyDetailID"))  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AdjustCost" HeaderText="扣减额" DataFormatString="{0:0.###元}"
                                    HtmlEncode="False" SortExpression="AdjustCost" Visible="false" />
                                <asp:BoundField DataField="AdjustMode" HeaderText="扣减方式" SortExpression="AdjustMode"
                                    Visible="false" />
                                <asp:TemplateField HeaderText="扣减原因" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_DeductReason" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem[\"DeductReason\"]")==null ? "" : GetDeductReason(DataBinder.Eval(Container,"DataItem[\"DeductReason\"]").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AdjustReason" HeaderText="扣减备注" SortExpression="AdjustReason"
                                    Visible="false" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Button ID="bt_OpenAdjust" runat="server" Text="调整" OnClick="bt_OpenAdjust_Click"
                                            Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hy_TaskApprove" runat="server" NavigateUrl="" Text="审批记录" Visible="false"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ControlStyle ForeColor="Red" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="附件及票号" Visible="false">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_Attachment" runat="server" Text="发票及附件" Width="70px" CommandName="RefreshDetail"
                                            OnClientClick='<%# "javascript:PopEditAttachment("+ Eval("ID").ToString() + ")"  %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_attachment" runat="server" OnClientClick='<%# "javascript:PopEditAttachment("+ Eval("ID").ToString() + ")"  %>'
                                            Visible='<%# new ATMT_AttachmentBLL()._GetModelList("RelateType=102 AND RelateID=" + Eval("ID").ToString()).Count > 0 %>'><img src="../../../Images/gif/gif-0651.gif" alt="查看附件" border="none" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="true" DeleteText="删除" ControlStyle-CssClass="listViewTdLinkS1">
                                    <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                </asp:CommandField>
                            </Columns>
                        </mcs:UC_GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center" height="28">
                        合计核销费用：<asp:Label ID="lb_TotalCost" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>元
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <mcs:UC_DetailView ID="pn_Remark" Visible="false" runat="server" DetailViewCode="Page_FNA_FeeWriteOffDetail_Remark">
                                </mcs:UC_DetailView>
                                <table id="tbl_Remark" runat="server" visible="false" cellpadding="0" cellspacing="0"
                                    width="100%" border="0">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                                                <tr>
                                                    <td>
                                                        <h3>
                                                            费用核销备注</h3>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" class="tabForm">
                                                <tr>
                                                    <td class="dataLabel" style="width: 80px; height: 18px;" nowrap="nowrap">
                                                        备注
                                                    </td>
                                                    <td class="dataField">
                                                        <asp:TextBox ID="tbx_Remark" runat="server" TextMode="MultiLine" Width="600px" Rows="8"></asp:TextBox>
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
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="101" CanSetDefaultImage="false" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
