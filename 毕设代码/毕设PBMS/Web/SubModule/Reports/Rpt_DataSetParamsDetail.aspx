<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt_DataSetParamsDetail.aspx.cs" Inherits="SubModule_Reports_Rpt_DataSetParamsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="报表数据集参数详细信息"></asp:Label></h2>
                        </td>
                        <td align="right" id="td_TemParam" runat="server">
                            预定参数:<asp:DropDownList ID="ddl_TemParam" runat="server">
                                <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                                <asp:ListItem Value="OrganizeCity">管理片区</asp:ListItem>
                                <asp:ListItem Value="AccountMonth">会计月</asp:ListItem>
                                <asp:ListItem Value="Staff">员工</asp:ListItem>
                                <asp:ListItem Value="Retailer">零售商</asp:ListItem>
                                <asp:ListItem Value="Distributor">经销商</asp:ListItem>
                                <asp:ListItem Value="BeginDate">开始日期</asp:ListItem>
                                <asp:ListItem Value="EndDate">截止日期</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="bt_AddTemParam" runat="server" Text="添加参数" OnClick="bt_AddTemParam_Click"
                                CausesValidation="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_OK" runat="server" Width="60px" Text="保 存" OnClick="bt_OK_Click" />
                            <asp:Button ID="bt_Delete" runat="server" Width="60px" Text="删 除" OnClick="bt_Delete_Click"
                                OnClientClick="return confirm('是否确认删除?')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="true"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <mcs:UC_DetailView ID="pl_detail" runat="server" DetailViewCode="DV_Rpt_DataSetParams_Detail">
                        </mcs:UC_DetailView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                            <tr>
                                <td>
                                    <h3>
                                        参数数据源关联信息</h3>
                                </td>
                            </tr>
                        </table>
                        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="dataLabel" style="width: 100px; height: 30px;">
                                    关联类型
                                </td>
                                <td class="dataField">
                                    <asp:RadioButtonList ID="rbl_RelationType" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rbl_RelationType_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="1">字典</asp:ListItem>
                                        <asp:ListItem Value="2">表</asp:ListItem>
                                        <asp:ListItem Value="3" Selected="True">不关联</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="tr_1" runat="server" visible="false">
                                <td class="dataLabel" style="width: 100px; height: 30px;">
                                    关联表名
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RelationTableName" runat="server" DataTextField="TableName"
                                        DataValueField="TableName" OnSelectedIndexChanged="ddl_RelationTableName_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="tr_2" runat="server" visible="false">
                                <td class="dataLabel" style="width: 100px; height: 30px;">
                                    关联表值字段
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RelationValueField" runat="server" DataTextField="DisplayName"
                                        DataValueField="FieldName">
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 100px; height: 30px;">
                                    关联表文本字段
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_RelationTextField" runat="server" DataTextField="DisplayName"
                                        DataValueField="FieldName">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" height="28px" border="0" class="h3Row">
                    <tr>
                        <td>
                            <h3>
                                参数默认值宏定义</h3>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="tabDetailView">
                    <tr>
                        <td class=" tabDetailViewDL">
                            $CurrentAccountMonth$
                        </td>
                        <td class="tabDetailViewDF">
                            当前会计月
                        </td>
                        <td class="tabDetailViewDL">
                            $Today$
                        </td>
                        <td class="tabDetailViewDF">
                            今天 yyyy-MM-dd
                        </td>
                        <td class="tabDetailViewDL">
                            $Now$
                        </td>
                        <td class="tabDetailViewDF">
                            当前时间 yyyy-MM-dd HH:mm
                        </td>
                    </tr>
                    <tr>
                        <td class=" tabDetailViewDL">
                            $ThisMonthFirstDay$
                        </td>
                        <td class="tabDetailViewDF">
                            本月第1天(当月1日)
                        </td>
                        <td class="tabDetailViewDL">
                            $ThisYearFirstDay$
                        </td>
                        <td class="tabDetailViewDF">
                            本年度第1天
                        </td>
                        <td class="tabDetailViewDL">
                            &nbsp;
                        </td>
                        <td class="tabDetailViewDF">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tabDetailViewDL">
                            $StaffID$
                        </td>
                        <td class="tabDetailViewDF">
                            当前员工ID
                        </td>
                        <td class=" tabDetailViewDL">
                            $StaffOrganizeCity$
                        </td>
                        <td class="tabDetailViewDF">
                            当前员工所在管理片区
                        </td>
                        <td class="tabDetailViewDL">
                            $StaffOfficialCity$
                        </td>
                        <td class="tabDetailViewDF">
                            当前员工所在行政城市
                        </td>
                    </tr>
                    <tr>
                        <td class=" tabDetailViewDL">
                            $TDPClient$
                        </td>
                        <td class="tabDetailViewDF">
                            当前TDP客户ID
                        </td>
                        <td class="tabDetailViewDL">
                            
                        </td>
                        <td class="tabDetailViewDF">
                            
                        </td>
                        <td class="tabDetailViewDL">
                            &nbsp;
                        </td>
                        <td class="tabDetailViewDF">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
