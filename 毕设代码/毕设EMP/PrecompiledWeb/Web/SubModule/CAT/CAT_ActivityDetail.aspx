<%@ page language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_CAT_CAT_ActivityDetail, App_Web_k8itolrs" enableEventValidation="false" stylesheettheme="basic" %>

<%@ Register Src="../../Controls/UploadFile.ascx" TagName="UploadFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                        <asp:Label ID="lb_PageTitle" runat="server" Text="活动详细信息"></asp:Label></h2>
                                </td>
                                <td>
                                    <asp:Label ID="lb_ActivityID" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                                    <asp:Button ID="bt_Stage" runat="server" OnClick="bt_Stage_Click" Text="准备举办" Width="60px"
                                        Visible="false" />
                                    <asp:Button ID="bt_Submit" runat="server" Text="提交审批" OnClick="bt_Submit_Click" />
                                    <asp:Button ID="bt_Approve" runat="server" Text="审核通过" Width="60px" OnClientClick="return confirm('是否确认将该活动设为审批通过?')"
                                        OnClick="bt_Approve_Click" Visible="False" />
                                    <asp:Button ID="bt_Complete" runat="server" OnClientClick="return confirm('是否确认完成举办?在完成举办前请确认已录入了活动举办信息，且只有在确认完参与客户的状态下，才可完成活动举办!')"
                                        Text="完成举办" OnClick="bt_Complete_Click" />
                                    <asp:Button ID="bt_Cancel" runat="server" CausesValidation="False" OnClientClick="return confirm('是否确认取消举办?')"
                                        Text="取消举办" OnClick="bt_Cancel_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                            <Items>
                                <mcs:MCSTabItem Text="活动基本信息" Value="0" />
                                <mcs:MCSTabItem Text="活动费用申请" Value="1" Visible="false" />
                                <mcs:MCSTabItem Text="参与客户情况" Value="2"  Visible="false"/>
                            </Items>
                        </mcs:MCSTabControl>
                    </td>
                </tr>
                <tr class="tabForm">
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_CAT_ActivityDetail_01">
                                    </mcs:UC_DetailView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table class="h3Row" height="28" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                活动赠品请单</h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tabForm" runat="server" id="tb_giftAdd">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="dataLabel" height="26" style="width: 260px">
                                                            产品名称
                                                        </td>
                                                        <td class="dataLabel">
                                                            <p align="center">
                                                                申请数量</p>
                                                        </td>
                                                        <td class="dataLabel">
                                                            说明
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataField" style="height: 23px">
                                                            <mcs:MCSSelectControl ID="select_GiftProduct" runat="server" PageUrl="~/SubModule/Product/Pop_Search_Product.aspx?IsOpponent=9&ExtCondition=Brand=4"
                                                                Width="260px" />
                                                        </td>
                                                        <td class="dataField" align="center" style="height: 23px">
                                                            <asp:TextBox ID="txt_ApplyCount" Text="0" runat="server" Width="60px"></asp:TextBox>
                                                            &nbsp;<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txt_ApplyCount"
                                                                Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                        </td>
                                                        <td class="dataField" style="height: 23px">
                                                            <asp:TextBox ID="txt_GiftRemark" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td class="dataField" align="right" style="height: 23px">
                                                            <asp:Button ID="btn_AddGift" runat="server" Text="新增赠品" Width="60px" ValidationGroup="1"
                                                                OnClick="btn_AddGift_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_GiftListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    CellPadding="1" BackColor="#CCCCCC" CellSpacing="1" DataKeyNames="ID" OnRowDeleting="gv_GiftListDetail_RowDeleting">
                                                    <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Product" HeaderText="产品名称" />
                                                        <asp:BoundField DataField="ApplyQuantity" HeaderText="申请数量" />
                                                        <asp:TemplateField HeaderText="调整数量">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_AdjustQuantity" runat="server" Text='<%#  Bind("AdjustQuantity") %>'
                                                                    Width="60px"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator_AdjustQuantity" runat="server" ControlToValidate="txt_AdjustQuantity"
                                                                    Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>                                                                     
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="使用数量">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_UsedQuantity" runat="server" Text='<%#  Bind("UsedQuantity") %>'
                                                                    Width="60px"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator_UsedQuantity" runat="server" ControlToValidate="txt_UsedQuantity"
                                                                    Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="剩余数量">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_BalanceQuantity" runat="server" Text='<%#  Bind("BalanceQuantity") %>'
                                                                    Width="60px"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator_BalanceQuantity" runat="server" ControlToValidate="txt_BalanceQuantity"
                                                                    Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="备注">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tbx_Remark" runat="server" Text='<%#  Bind("Remark") %>' Width="300px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                             <asp:Button ID="bt_Change" runat="server" Text="修改"/>
                                                                <asp:Button ID="bt_Adjust" runat="server" Text="保存" onclick="bt_Adjust_Click"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                                            <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        无数据
                                                    </EmptyDataTemplate>
                                                </mcs:UC_GridView>
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
                                                <table class="h3Row" height="28" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                活动费用申请单</h3>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="bt_AddFeeApply" runat="server" Text="编辑费用" Width="60px" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tabForm" runat="server" id="tr_FeeApply">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="dataLabel" height="26" style="width: 250px">
                                                            费用发生客户
                                                        </td>
                                                        <td class="dataLabel">
                                                            <p align="center">
                                                                会计科目</p>
                                                        </td>
                                                        <td class="dataLabel">
                                                            <p align="center">
                                                                申请金额<br />
                                                                (我司承担)</p>
                                                        </td>
                                                        <td class="dataLabel">
                                                            <p align="center">
                                                                申请金额<br />
                                                                (经销商承担)</p>
                                                        </td>
                                                        <td class="dataLabel">
                                                            说明
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataField" style="width: 250px">
                                                            <mcs:MCSSelectControl runat="server" ID="select_Client" Width="250px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx" />
                                                        </td>
                                                        <td class="dataField" align="center" style="height: 23px">
                                                            <asp:DropDownList ID="ddl_AccountTitle" runat="server" DataTextField="Name" DataValueField="ID">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="dataField" align="center" style="height: 23px">
                                                            <asp:TextBox ID="tbx_ApplyCost" runat="server" Text="0" Width="60px"></asp:TextBox>
                                                            元
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                        </td>
                                                        <td class="dataField" align="center" style="height: 23px">
                                                            <asp:TextBox ID="tbx_DICost" Text="0" runat="server" Width="60px"></asp:TextBox>
                                                            元
                                                            <asp:CompareValidator ID="CompareValidator_DICost" runat="server" ControlToValidate="tbx_DICost"
                                                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                        </td>
                                                        <td class="dataField" style="height: 23px">
                                                            <asp:TextBox ID="tbx_Remark" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td class="dataField" align="right" style="height: 23px">
                                                            <asp:Button ID="bt_AddDetail" runat="server" Text="新增费用" Width="60px" ValidationGroup="1"
                                                                OnClick="bt_AddDetail_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_FeeListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    CellPadding="1" BackColor="#CCCCCC" CellSpacing="1" DataKeyNames="ID,AccountTitle"
                                                    OnRowDeleting="gv_FeeListDetail_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="Client" HeaderText="发生客户" />
                                                        <asp:BoundField DataField="AccountTitle" HeaderText="科目" />
                                                        <asp:BoundField DataField="ApplyCost" HeaderText="申请额<br/>(我司)" HtmlEncode="false"
                                                            DataFormatString="{0:0.###元}" ItemStyle-ForeColor="Blue" />
                                                        <asp:BoundField DataField="DICost" HeaderText="经销商<br/>承担额" HtmlEncode="false" DataFormatString="{0:0.###元}" />
                                                        <asp:BoundField DataField="BeginMonth" HeaderText="发生月" />
                                                        <asp:BoundField DataField="Remark" HeaderText="说明" ItemStyle-Width="180px" />
                                                        <asp:TemplateField HeaderText="扣减额">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tbx_AdjustCost" runat="server" Text='<%#  Bind("AdjustCost") %>'
                                                                    Width="60px"></asp:TextBox>
                                                                元
                                                                <asp:CompareValidator ID="CompareValidator_AdjustCost" runat="server" ControlToValidate="tbx_AdjustCost"
                                                                    Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="扣减原因" >
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tbx_AdjustReason" runat="server" Text='<%#  Bind("AdjustReason") %>'
                                                                    Width="300px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="批复额<br/>(我司)" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" ForeColor="Red" Font-Bold="true" runat="server" Text='<%# ((decimal)DataBinder.Eval(Container.DataItem,"ApplyCost") + (decimal)DataBinder.Eval(Container.DataItem,"AdjustCost")).ToString("0.###元")  %>'></asp:Label>
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
                                                    <EmptyDataTemplate>
                                                        无数据
                                                    </EmptyDataTemplate>
                                                </mcs:UC_GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tb_ActivitySales"
                                        runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <table class="h3Row" height="28" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                活动销量</h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tabForm" runat="server" id="td_AddSales">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="dataLabel" height="26">
                                                           
                                                                品牌</p></td>
                                                        <td class="dataLabel">
                                                            <p align="center">
                                                                销售金额
                                                            </p>
                                                        </td>
                                                        <td class="dataLabel">
                                                            说明
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataField"  style="height: 23px">
                                                            <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="dataField" align="center" style="height: 23px">
                                                            <asp:TextBox ID="txt_Amount" runat="server" Text="0" Width="60px"></asp:TextBox>
                                                            元
                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_Amount"
                                                                Display="Dynamic" ErrorMessage="必须为数字" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                        </td>
                                                        <td class="dataField" style="height: 23px">
                                                            <asp:TextBox ID="txt_salesremark" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td class="dataField" align="right" style="height: 23px">
                                                            <asp:Button ID="btn_AddSales" runat="server" Text="新增销量" Width="60px" ValidationGroup="1"
                                                                OnClick="btn_AddSales_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <mcs:UC_GridView ID="gv_SalesList" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    CellPadding="1" BackColor="#CCCCCC" CellSpacing="1" DataKeyNames="ID" OnRowDeleting="gv_SalesList_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="Brand" HeaderText="品牌" />
                                                        <asp:BoundField DataField="Amount" HeaderText="销售金额" DataFormatString="{0:0.###元}" />
                                                        <asp:BoundField DataField="Remark" HeaderText="备注" />
                                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="listViewTdLinkS1">
                                                            <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        无数据
                                                    </EmptyDataTemplate>
                                                </mcs:UC_GridView>
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
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <uc1:UploadFile ID="UploadFile001" runat="server" RelateType="95" CanSetDefaultImage="false" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
