<%@ page title="" language="C#" masterpagefile="~/MasterPage/BasicMasterPage.master" autoeventwireup="true" inherits="SubModule_Logistics_Order_OrderApplyOrderTimes, App_Web_aozhikkk" enableEventValidation="false" stylesheettheme="basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" UpdateMode="Always">
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
                                经销商每月可以申请赠品次数维护
                            </h2>
                        </td>
                        <td align="right">
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="height: 28px">
                            <h3>
                                查询条件</h3>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tabForm">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="dataLabel" width="80">
                            管理片区
                        </td>
                        <td class="dataField" width="250">
                            <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                ParentColumnName="SuperID" Width="200"  AutoPostBack="true"
                                onselected="tr_OrganizeCity_Selected" />
                        </td>
                        <td class="dataLabel" width="60">
                            经销商
                        </td>
                        <td class="dataField" width="250">
                            <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../../CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=MCS_SYS.dbo.UF_Spilt2(~MCS_CM.dbo.CM_Client~,CM_Client.ExtPropertys,~DIClassify~)=~1~"
                                        Width="200px" />
                        </td> 
                        <td align="right">
                            <asp:Button ID="bt_Find" runat="server" OnClick="bt_Find_Click" Text="查找" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>         
        <tr id="tr_Detail" runat="server" visible="false">
        <td>
        <table class="tabForm" width="100%" height="40" >
            <tr>           
            <td class="dataLabel" width="80">经销商</td>
            <td class="dataField" width="250"> 
            <mcs:MCSSelectControl runat="server" ID="select_Client2" PageUrl="../../CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=MCS_SYS.dbo.UF_Spilt2(~MCS_CM.dbo.CM_Client~,CM_Client.ExtPropertys,~DIClassify~)=~1~"
                                        Width="200px" /></td>
            <td class="dataLabel" width="80">可申请次数</td>
            <td class="dataField" width="250"><asp:TextBox ID="tbx_Times" runat="server" ></asp:TextBox>
            <asp:RegularExpressionValidator  ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbx_Times" ValidationExpression="[0-9]{1}[\d]*$"  Text=" " ErrorMessage="请输入正整数！">
            </asp:RegularExpressionValidator></td>
            <td align="right"><asp:Button ID="bt_Save" runat="server" Text="保存" Width="60" 
                    onclick="bt_Save_Click" /></td>
            </tr>
        </table>
        </td>
         </tr>
        <tr>
            <td>
                <table class="h3Row" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="height: 28px">
                            <h3>
                                申请赠品次数列表</h3>
                        </td>
                        <td align="right">
                        <asp:Button ID="bt_Add" runat="server" Text="新 增" OnClick="bt_Add_Click" Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td>
                <mcs:UC_GridView ID="gv_List" runat="server" Width="100%" 
                    AutoGenerateColumns="False"  AllowPaging="True"
                    onselectedindexchanged="gv_List_SelectedIndexChanged" 
                    AutoGenerateSelectButton="True" PageSize="15" DataKeyNames="ID" Binded="False"
                    OrderFields="" PanelCode="" TotalRecordCount="0" ConditionString="" 
                    onrowdeleting="gv_List_RowDeleting" >
                    <Columns>
                        <asp:BoundField HeaderText="客户ID"  DataField="ClientID" />
                        <asp:BoundField HeaderText="客户编码"  DataField="ClientCode" />
                        <asp:BoundField HeaderText="所在片区" DataField="City" />
                        <asp:HyperLinkField DataNavigateUrlFields="ClientID" DataNavigateUrlFormatString="../../CM/DI/DistributorDetail.aspx?ClientID={0}"
                            DataTextField="ClientName" ControlStyle-CssClass="listViewTdLinkS1" 
                            HeaderText="客户全称" >
                            <ControlStyle CssClass="listViewTdLinkS1" />
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="可申请次数" DataField="OrderTimes" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                    CommandName="Delete" Text="删除" CommandArgument= '<%#Bind("ID") %> ' OnClientClick="return confirm('是否确定删除该行？')"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
 </ContentTemplate>
 <Triggers>
    <asp:PostBackTrigger ControlID="gv_List" />
    <asp:AsyncPostBackTrigger ControlID="bt_Save" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
 </Triggers>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

