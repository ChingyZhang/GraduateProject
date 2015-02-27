<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_PM_PM_PromotorSalaryBasePay, App_Web_ajc2-uew" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                导购员底薪模式维护</h2>
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
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server" height="30" class="tabForm">
                            <tr>
                                <td class="dataLabel" style="width: 56px">行政城市</td>
                                <td style="width: 282px">
                                    <mcs:MCSTreeControl ID="tr_OfficialCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AutoPostBack="True" 
                                        DisplayRoot="true" onselected="tr_OfficialCity_Selected" 
                                        TableName="Addr_OfficialCity" />
                                </td>
                                <td class="dataLabel" style="width: 56px">
                                    市场类别</td>
                                <td>
                                    &nbsp;</td>
                                <td class="dataLabel">
                                    <asp:DropDownList ID="ddlMarketType" runat="server" 
                                        AppendDataBoundItems="True" >
                                        <asp:ListItem Value="0">请选择</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
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
                       <mcs:UC_DetailView ID="dvBasePay" runat="server" DetailViewCode="Page_PM_BasePay">
                       </mcs:UC_DetailView>
                     </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                     <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                            <tr>
                                <td nowrap style="width: 100px" colspan="1">
                                    <h3>底薪模式列表
                                        </h3>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <mcs:UC_GridView ID="gvList" PanelCode="Panel_PM_SalaryBasePay01" runat="server" 
                            AllowPaging="True" ConditionString=""  Width="100%" 
                            AutoGenerateColumns="False" AutoGenerateSelectButton="True" Binded="False" 
                             onselectedindexchanging="gvList_SelectedIndexChanging" OrderFields="" 
                             TotalRecordCount="0" DataKeyNames="PM_StdBasePay_ID">
                            
                        </mcs:UC_GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
