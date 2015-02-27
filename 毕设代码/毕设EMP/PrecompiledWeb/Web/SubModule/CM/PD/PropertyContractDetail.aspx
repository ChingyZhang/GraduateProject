<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CM_PD_PropertyContractDetail, App_Web_u_bdbxcq" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="height: 24px;">
                            <h2>
                                物业协议详细信息</h2>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_OK" runat="server" Width="80px" Text="保存" 
                                OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Submit" runat="server" Text="提交协议" Width="80px" 
                                OnClick="bt_Submit_Click" />
                            <asp:Button ID="bt_del" runat="server" Width="80px" Text="删除" 
                                OnClick="bt_del_Click" />
                            <asp:Button ID="bt_FeeApply" runat="server" onclick="bt_FeeApply_Click" 
                                Text="租赁费用申请" Width="80px" />
                            <asp:Button ID="bt_Disable" runat="server" OnClick="bt_Disable_Click" Text="中止协议"
                                OnClientClick="return confirm('是否确认中止该协议?')" Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_PD_ContractDetail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    协议费用明细列表</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_AddDetail">
                                <td class="tabForm">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="dataLabel">
                                                会计科目
                                            </td>
                                            <td class="dataLabel">
                                                月费用金额(元)
                                            </td>
                                            <td class="dataLabel">
                                                支付方式
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_AccountTitle" runat="server" DataTextField="Name" DataValueField="ID"
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataField">
                                                <asp:TextBox ID="tbx_ApplyLimit" runat="server" Width="40px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_ApplyLimit"
                                                    Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_ApplyLimit"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_PayMode" runat="server" DataTextField="Value" DataValueField="Key">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataField" align="right">
                                                <asp:Button ID="bt_AddDetail" runat="server" Text="新 增" Width="60px" ValidationGroup="1"
                                                    OnClick="bt_AddDetail_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <mcs:UC_GridView ID="gv_Detail" runat="server" Width="100%" DataKeyNames="ID,AccountTitle"
                                        AutoGenerateColumns="false" OnRowDeleting="gv_Detail_RowDeleting" OnSelectedIndexChanging="gv_Detail_SelectedIndexChanging">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" SelectText="选择" ControlStyle-CssClass="listViewTdLinkS1" />
                                            <asp:BoundField DataField="AccountTitle" HeaderText="会计科目" SortExpression="AccountTitle" />
                                            <asp:BoundField DataField="ApplyLimit" HeaderText="月费用金额(元)" SortExpression="ApplyLimit" />
                                            <asp:BoundField DataField="PayMode" HeaderText="支付方式" SortExpression="PayMode" />
                                            <asp:TemplateField HeaderText="付款截止日期">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_PayEndDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem[\"PayEndDate\"]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:CommandField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UploadFile ID="UploadFile1" runat="server" RelateType="35" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
