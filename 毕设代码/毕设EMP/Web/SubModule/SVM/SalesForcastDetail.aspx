<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="SalesForcastDetail.aspx.cs" Inherits="SubModule_SVM_SalesForcastDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 39px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24" style="height: 24px">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" width="160" align="left">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="客户销量预估表"></asp:Label>
                            </h2>
                        </td>
                        <td align="left">
                        </td>
                        <td align="right" width="100%">
                            <asp:CheckBox ID="cbx_UpdateAccountMonth" runat="server" AutoPostBack="True" OnCheckedChanged="cbx_UpdateAccountMonth_CheckedChanged"
                                Text="修改会计月" />
                            <asp:Button ID="btn_SalesForcast" runat="server" Text="填报预估" Width="80px" OnClick="btn_SalesForcast_Click" />
                            <asp:Button ID="bt_Approve" runat="server" OnClick="bt_Approve_Click" Text="审 核"
                                OnClientClick="return confirm('是否确认审核该记录？审核后的数据将不可再更改！')" Width="60px" />
                            <asp:Button ID="bt_Save" runat="server" Text="保 存" OnClick="bt_Save_Click" Width="60px"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="bt_Del" runat="server" Text="删 除" Width="60px" OnClientClick="return confirm('是否确认删除该记录?')"
                                OnClick="bt_Del_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="UC_DetailView1" runat="server" DetailViewCode="DV_SalesForcast_Detail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                    <tr>
                        <td nowrap style="width: 100px" colspan="1">
                            <h3>
                                预估列表</h3>
                        </td>
                        <td>
                            <asp:Label ID="lb_Msg" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="dataLabel" style="color: #FF0000">
                                                品牌
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Brand" runat="server" DataTextField="Name" DataValueField="ID"
                                                    OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="dataLabel" style="color: #FF0000">
                                                系列
                                            </td>
                                            <td class="dataField">
                                                <asp:DropDownList ID="ddl_Classify" runat="server" DataTextField="Name" DataValueField="ID"
                                                    RepeatDirection="Horizontal">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="bt_Search" runat="server" Text="过 滤" Width="60px" OnClick="bt_Search_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="SVM_SalesForcast_Detail_ID"
                                        PanelCode="Panel_SVM_SalesForcast_DetailList" Width="100%" AllowPaging="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="上月进货">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_MonthSalesVolume" runat="server" Text='<%# GetSalesVolume((int)DataBinder.Eval(Container,"DataItem.PDT_Product_ID")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="预估数量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Quantity1" runat="server" Text='<%# (int)DataBinder.Eval(Container,"DataItem.SVM_SalesForcast_Detail_Quantity")/(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'
                                                        Width="40px"></asp:TextBox><asp:Label ID="lb_TrafficPackaging" runat="server" Text='<%# Bind("PDT_Product_TrafficPackaging")%>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_Quantity1"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_Quantity1"
                                                        Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="tbx_Quantity2" runat="server" Text='<%# (int)DataBinder.Eval(Container,"DataItem.SVM_SalesForcast_Detail_Quantity")%(int)DataBinder.Eval(Container,"DataItem.PDT_Product_ConvertFactor") %>'
                                                        Width="30px"></asp:TextBox><asp:Label ID="lb_Packaging" runat="server" Text='<%# Bind("PDT_Product_Packaging")%>'></asp:Label>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_Quantity2"
                                                        Display="Dynamic" ErrorMessage="必须为整数" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_Quantity2"
                                                        Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="预估额">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SumPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SVM_SalesForcast_Detail_FactoryPrice").ToString()==""?"0":((int)DataBinder.Eval(Container.DataItem,"SVM_SalesForcast_Detail_Quantity")*(decimal)DataBinder.Eval(Container.DataItem,"SVM_SalesForcast_Detail_FactoryPrice")).ToString("f2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="备注">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Remark" Width="100px" runat="server" Text='<%# Bind("SVM_SalesForcast_Detail_Remark") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            无数据
                                        </EmptyDataTemplate>
                                    </mcs:UC_GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_Search" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
        <ProgressTemplate>
            <span style="color: #FF0000">数据处理中，请稍候...</span></ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
