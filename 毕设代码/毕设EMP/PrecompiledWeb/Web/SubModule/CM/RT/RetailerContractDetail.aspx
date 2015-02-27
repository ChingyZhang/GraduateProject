<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CM_RT_RetailerContractDetail, App_Web_hv25c18v" enableEventValidation="false" stylesheettheme="basic" %>

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text='零售商协议详细信息'></asp:Label></h2>
                        </td>
                        <td align="right" style="height: 24px">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保存草稿" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Submit" runat="server" Text="提交审批" Width="60px" OnClick="bt_Submit_Click" />
                            <asp:Button ID="bt_Approve" runat="server" Text="审核通过" Width="60px" OnClick="bt_Approve_Click" />
                            <asp:Button ID="bt_FeeApply" runat="server" OnClick="bt_FeeApply_Click" Text="合同费用申请"
                                Width="80px" CausesValidation="False" />
                            <asp:Button ID="bt_Delete" runat="server" Width="60px" Text="删除协议" OnClick="bt_Delete_Click"
                                OnClientClick="return confirm('是否确认删除该协议?')" CausesValidation="False" />
                            <asp:Button ID="bt_Disable" runat="server" OnClick="bt_Disable_Click" Text="中止协议"
                                OnClientClick="return confirm('是否确认中止该协议?')" Width="60px" CausesValidation="False" />
                            <asp:Button ID="bt_print" runat="server" Text="打印" OnClick="bt_print_Click" ValidationGroup="0"
                                Width="41px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_RT_RetailDetail_Contract">
                        </mcs:UC_DetailView>
                        <br />
                        <asp:Literal ID="lt_RebateRemark" runat="server" Text=""></asp:Literal>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr runat="server" id="tr_AddDetail">
            <td>
                <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    新增协议费用</h3>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_AddDetail" runat="server" OnClick="bt_AddDetail_Click" Text="新 增"
                                                    ValidationGroup="1" Width="60px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tabForm">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    支付模式</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    会计科目</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    数量</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    付款周期</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    月请款金额(元)</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    协议月数</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    协议期费用总额(元)</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    占月目标销售额比%</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    我司承担比例%</p>
                                            </td>
                                            <td class="dataLabel">
                                                <p align="center">
                                                    是否CA</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:DropDownList ID="ddl_BearMode" runat="server" DataTextField="Value" DataValueField="Key">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:DropDownList ID="ddl_AccountTitle" runat="server" DataTextField="Name" DataValueField="ID">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:TextBox ID="txt_count" runat="server" Width="40px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txt_count"
                                                    Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_count"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:DropDownList ID="ddl_PayMode" runat="server" DataTextField="Value" DataValueField="Key">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:TextBox ID="tbx_ApplyLimit" runat="server" Width="40px" AutoPostBack="true"
                                                    OnTextChanged="tbx_ApplyLimit_TextChanged"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbx_ApplyLimit"
                                                    Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbx_ApplyLimit"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:Label ID="lbl_FeeCycle" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:TextBox ID="tbx_Amount" runat="server" Width="40px" AutoPostBack="true" OnTextChanged="tbx_Amount_TextChanged"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_Amount"
                                                    Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                    ValidationGroup="1" ControlToValidate="tbx_Amount"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:Label ID="lbl_SalesPercent" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:TextBox ID="tbx_BearPercent" runat="server" Width="40px" Text="100"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_BearPercent"
                                                    Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                    Text="*" ValidationGroup="1" ControlToValidate="tbx_BearPercent"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="dataField" align="center" style="height: 23px">
                                                <asp:DropDownList runat="server" ID="ddl_YesNO" DataTextField="Value" DataValueField="Key"
                                                    Width="80px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr valign="bottom">
                                            <td align="center" class="dataField" height="28" valign="middle">
                                                说明
                                            </td>
                                            <td align="left" class="dataField" valign="middle" colspan="6">
                                                <asp:TextBox ID="tbx_Remark" runat="server" Width="450px"></asp:TextBox>
                                            </td>
                                            <td align="left" class="dataField">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr valign="bottom">
                                            <td align="center" class="dataField" height="28" valign="middle">
                                                关联品牌
                                            </td>
                                            <td align="left" class="dataField" colspan="6">
                                                <asp:CheckBoxList ID="cbl_Brand" runat="server" CellPadding="2" CellSpacing="2" RepeatColumns="5"
                                                    RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                            </td>
                                            <td align="center" class="dataField" valign="middle">
                                                <asp:CheckBox ID="cbx_CheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_CheckAll_CheckedChanged"
                                                    Text="全选" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gv_Detail" EventName="SelectedIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="tr_ContractDetail" runat="server">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
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
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_Detail" runat="server" Width="100%" DataKeyNames="ID,AccountTitle"
                                        AutoGenerateColumns="false" OnRowDeleting="gv_Detail_RowDeleting" OnSelectedIndexChanging="gv_Detail_SelectedIndexChanging"
                                        OnRowDataBound="gv_Detail_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" SelectText="修改" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="BearMode" HeaderText="支付模式" SortExpression="BearMode" />
                                            <asp:BoundField DataField="AccountTitle" HeaderText="会计科目" SortExpression="AccountTitle" />                                    
                                            <asp:TemplateField HeaderText="数量">
                                                <ItemTemplate>
                                                    <asp:Label ID="lab_count" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[DiaplayCount]")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PayMode" HeaderText="付款周期" SortExpression="PayMode" />
                                            <asp:BoundField DataField="ApplyLimit" HeaderText="月请款金额(元)" SortExpression="ApplyLimit" />
                                            <asp:TemplateField HeaderText="协议月数">
                                                <ItemTemplate>
                                                    <asp:Label ID="lab_FeeCycle" runat="server" Text="<%#GetContractcycle() %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Amount" HeaderText="协议期费用总额" SortExpression="Amount" />
                                            <asp:TemplateField HeaderText="占月目标销售额比%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SalesPercent" runat="server" Text='<%#GetSalesPercent(decimal.Parse(DataBinder.Eval(Container,"DataItem.Amount").ToString())) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BearPercent" HeaderText="我司承担比例%" SortExpression="BearPercent" />
                                            <asp:TemplateField HeaderText="是否CA">
                                                <ItemTemplate>
                                                    <asp:Label ID="lab_ISCA" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[ISCA]").ToString()!="0"? MCSFramework.BLL.DictionaryBLL.GetDicCollections("PUB_YesOrNo")[DataBinder.Eval(Container.DataItem, "[ISCA]").ToString()].ToString():""%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Remark" HeaderText="说明" SortExpression="Remark" />
                                            <asp:TemplateField HeaderText="关联品牌">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_RelateBrand" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                                <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                            </asp:CommandField>
                                        </Columns>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_AddDetail" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
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
