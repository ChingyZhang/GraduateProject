<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="PM_StdInsuranceCost.aspx.cs" Inherits="SubModule_PM_PM_StdInsuranceCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="height: 33px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap" style="width: 180px">
                            <h2>
                                导购员保险设定</h2>
                        </td>
                        <td align="right">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server" height="30" class="tabForm">
                            <tr>
                                <td class="dataLabel" style="width: 56px">社保模式</td>
                                <td style="width: 282px">
                                    <asp:DropDownList ID="ddl_InsuranceMode" runat="server" 
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">请选择</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel" style="width: 56px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td class="dataLabel">
                                    &nbsp;</td>
                                <td class="dataField">
                                    &nbsp;</td>
                                <td class="dataField" align="right">
                                    <asp:Button ID="bt_Search" runat="server" Text="查 看" Width="60px" 
                                        onclick="bt_Search_Click"  />
                                    <asp:Button ID="btn_Add" runat="server" onclick="btn_Add_Click" Text="新 增" 
                                        Width="60px" />
                                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="保 存" 
                                        Width="60px" />
                                    <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" Text="删 除" 
                                        Width="60px" />
                                </td>
                            </tr>
                        </table>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                     <ContentTemplate>
                       <mcs:UC_DetailView ID="dvBasePay" runat="server" DetailViewCode="Page_PM_StdInsuranceCost">
                       </mcs:UC_DetailView>
                     </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                     <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td nowrap style="width: 100px" colspan="1">
                                    <h3>保险设定列表
                                        </h3>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <mcs:UC_GridView ID="gvList" PanelCode="PM_StdInsuranceCost_List" runat="server" 
                            AllowPaging="True" ConditionString=""  Width="100%" 
                            AutoGenerateColumns="False" AutoGenerateSelectButton="True" Binded="False" 
                             onselectedindexchanging="gvList_SelectedIndexChanging" OrderFields="" 
                             TotalRecordCount="0" DataKeyNames="PM_StdInsuranceCost_ID" 
                             onrowdatabound="gvList_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="适用区域">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lb_InCity" runat="server" Font-Underline="True"  ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

