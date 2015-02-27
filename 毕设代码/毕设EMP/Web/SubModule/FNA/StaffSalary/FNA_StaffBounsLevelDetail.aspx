<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="FNA_StaffBounsLevelDetail.aspx.cs" Inherits="SubModule_FNA_StaffSalary_FNA_StaffBounsLevelDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
<tr>
    <td width="100%">
        <table  cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
            <tr>
            <td width="18">
                   <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
             </td>
             <td nowrap="noWrap">
                <h2>办事处的季度销量及层级明细表</h2>
             </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
    <td>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
                height="30" class="tabForm">
                <tr>
                    <td class="dataLabel" width="100px">
                        管理片区
                    </td>
                    <td>
                        <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                            ParentColumnName="SuperID" Width="200px" AlwaysSelectChildNode="False" SelectDepth="0"
                            RootValue="0" DisplayRoot="True" />
                    </td>
                    <td class="dataLabel" style="width: 56px">
                        季度</td>
                    <td class="dataField">
                    <asp:DropDownList ID="ddl_Quarter" runat="server" DataTextField="Name"  DataValueField="ID"></asp:DropDownList>
                    
                    </td>
                    <td class="dataLabel">
                        &nbsp;
                    </td>
                    <td class="dataField">
                        &nbsp;
                    </td>
                    <td class="dataField" align="right">
                        <asp:Button ID="bt_Search" runat="server" Text="查 看" Width="60px" OnClick="bt_Search_Click" />
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保 存" Width="60px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </td>
</tr>
<tr>
    <td>
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                <tr>
                    <td nowrap colspan="1">
                        <h3>
                            季度销量及层级明细表
                        </h3>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <mcs:UC_GridView ID="gvList" runat="server" AllowPaging="True" ConditionString=""
                PanelCode="Panel_FNA_StaffBonusLevelDetail" Width="100%" AutoGenerateColumns="False"
                Binded="False" OrderFields="FNA_StaffBounsLevelDetail_OrganizeCity"
                TotalRecordCount="0" DataKeyNames="FNA_StaffBounsLevelDetail_ID" >
                <Columns>
                    <asp:TemplateField HeaderText="季度销量调整">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_SalesAdjust" runat="server" Width="80" Text='<%#Eval("FNA_StaffBounsLevelDetail_SalesAdjust") %>' ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="季度预算费率(%)">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_BudgetFeeRate" runat="server" Width="80" Text='<%#Eval("FNA_StaffBounsLevelDetail_BudgetFeeRate") %>' ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="季度实际费率(%)">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_ActFeeRate" runat="server" Width="80" Text='<%#Eval("FNA_StaffBounsLevelDetail_ActFeeRate") %>' ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="原因/备注">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_Remark" runat="server" Width="80" Text='<%#Eval("FNA_StaffBounsLevelDetail_Remark") %>' ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </mcs:UC_GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </td>
</tr>
</table>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

