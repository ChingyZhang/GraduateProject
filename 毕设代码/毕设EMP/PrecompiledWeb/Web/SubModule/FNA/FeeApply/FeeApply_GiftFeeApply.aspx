<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_FNA_FeeApply_FeeApply_GiftFeeApply, App_Web_5zp237uh" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td width="24">
                                    <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td nowrap="noWrap" style="width: 280px">
                                    <h2>
                                        申请赠品费用申请单</h2>
                                </td>
                                <td align="center" style="font-size: large; color: #FF0000">
                                    可用申请赠品金额:<asp:Label ID="lb_GiftAmountBalance" runat="server" Text="0"></asp:Label>
                                    元
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr height="28px">
                                <td nowrap>
                                    <h3>
                                        申请条件</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm" align="center">
                        <table cellpadding="0" cellspacing="0" border="0" width="50%" height="100">
                            <tr>
                                <td class="dataLabel" height="22px">
                                    申请月份
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_ApplyMonth" runat="server" DataTextField="Name" 
                                        DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    管理片区
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="280px" AutoPostBack="True" OnSelected="tr_OrganizeCity_Selected" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    经销商
                                </td>
                                <td class="dataField" align="left">
                                    <mcs:MCSSelectControl ID="select_Client" runat="server" Width="280px" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2"
                                        OnSelectChange="select_Client_SelectChange" />
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    费用归属品牌
                                </td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_Brand" runat="server" AutoPostBack="True" DataTextField="Name"
                                        DataValueField="ID" OnSelectedIndexChanged="ddl_Brand_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    赠品费用类别
                                </td>
                                <td class="dataField" align="left">
                                    <asp:RadioButtonList ID="rbl_GiftClassify" runat="server" RepeatDirection="Horizontal"
                                        DataTextField="Value" DataValueField="Key" AutoPostBack="True" OnSelectedIndexChanged="rbl_GiftClassify_SelectedIndexChanged">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    赠品会计科目
                                </td>
                                <td class="dataField" align="left">
                                    <asp:DropDownList ID="ddl_AccountTitle" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                    <%--<asp:RadioButtonList ID="rbl_AccountTitle" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="155">赠品抵扣</asp:ListItem>
                                        <asp:ListItem Value="195">自购赠品</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    总申请金额
                                </td>
                                <td class="dataField" align="left">
                                    <asp:TextBox ID="tbx_ApplyCost" runat="server" Text="0"></asp:TextBox>元 <span style="color: Red">
                                        *</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                        Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_ApplyCost"
                                        Display="Dynamic" ErrorMessage="必须为数值型" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" style="height: 22px">
                                    经销商承担比例</td>
                                <td align="left" class="dataField" style="height: 22px">
                                    <asp:TextBox ID="tbx_DIPercent" runat="server" Width="40px">0</asp:TextBox>
                                    % <span style="color: Red">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="tbx_DIPercent" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                        ControlToValidate="tbx_DIPercent" Display="Dynamic" ErrorMessage="必须为数值型" 
                                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    预计销量</td>
                                <td align="left" class="dataField">
                                    <asp:TextBox ID="tbx_SalesForcast" runat="server" Text="0"></asp:TextBox>
                                    元&nbsp;<span style="color: Red">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="tbx_SalesForcast" Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                        ControlToValidate="tbx_SalesForcast" Display="Dynamic" ErrorMessage="必须为数值型" 
                                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    最迟核销月</td>
                                <td align="left" class="dataField">
                                    <asp:DropDownList ID="ddl_LastWriteOffMonth" runat="server" 
                                        DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dataLabel" height="22px">
                                    说明</td>
                                <td align="left" class="dataField">
                                    <asp:TextBox ID="tbx_Remark" runat="server" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" height="22px">
                                    <asp:Button ID="bt_Generate" runat="server" Text="生成申请单" Width="80px" OnClick="bt_Generate_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
