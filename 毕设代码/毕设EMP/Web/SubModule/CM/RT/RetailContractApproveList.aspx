<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="RetailContractApproveList.aspx.cs" Inherits="SubModule_CM_RT_RetailContractApproveList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

    <script type="text/javascript">
       function SelectAll(tempControl) {
           var theBox = tempControl;
           sState = theBox.checked;
           elem = theBox.form.elements;
           for (i = 0; i < elem.length; i++) {
               if (elem[i].type == "checkbox" && elem[i].id != theBox.id) {
                   if (elem[i].checked != sState) {
                       elem[i].click();
                   }
               }
           }
       }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                                <asp:Label ID="lb_PageTitle" runat="server" Text="零售商协议批量审批(导购\返利)"></asp:Label>
                            </h2>
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Button ID="btn_Approve" Width="80px" Text="审批通过" runat="server" 
                                        OnClick="btn_Approve_Click" OnClientClick="return confirm('是否确认将选中的协议设为审批通过?')" />
                                    <asp:Button ID="btn_UnApprove" Width="80px" Text="审批不通过" runat="server" 
                                        OnClick="btn_UnApprove_Click" OnClientClick="return confirm('是否确认将选中的协议设为审批不通过?')" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right" width="85px">
                            <asp:Button ID="bt_Export" runat="server"  OnClick="bt_Export_Click"
                                Text="导出明细" Width="80px" />
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
                                <td class="dataLabel">
                                    管理片区
                                </td>
                                <td class="dataField">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name" ParentColumnName="SuperID" Width="220px" />
                                </td>
                                <td class="dataLabel">
                                    审批状态
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_State" runat="server" DataTextField="Value" DataValueField="Key"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_State_SelectedIndexChanged">
                                        <asp:ListItem Value="0">所有</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">待我审批</asp:ListItem>
                                        <asp:ListItem Value="2">我已审批</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="dataLabel">
                                    付款方式
                                </td>
                                <td class="dataField">
                                    <asp:DropDownList ID="ddl_PayMode" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Button ID="bt_Find" runat="server" Text="查找" Width="80px" OnClick="bt_Find_Click" />
                                </td>
                            </tr>
                            <tr>
                             <td class="dataLabel">
                                    零售商
                                </td>
                                <td class="dataField">
                                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Always" >
                                    <ContentTemplate>
                                        <mcs:MCSSelectControl ID="select_OrgSupplier" runat="server" PageUrl="../PopSearch/Search_SelectClient.aspx?ClientType=3"
                                            Width="220px"  />
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="dataLabel">
                                    协议ID
                                </td>
                                <td class="dataField">
                                     <asp:TextBox runat="server" ID="tbx_ContractID"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
   
            </td>
        </tr>
        <tr>
            <td height="10px">
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGridView" style="overflow: scroll; height: 500px;" align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <mcs:MCSTabControl ID="MCSTabControl1" runat="server" Width="100%" SelectedIndex="0"
                                            OnOnTabClicked="MCSTabControl1_OnTabClicked">
                                            <Items>
                                                <mcs:MCSTabItem Text="导购协议汇总" Value="3" />
                                                <mcs:MCSTabItem Text="返利协议汇总" Value="2" />
                                            </Items>
                                        </mcs:MCSTabControl>
                                    </td>
                                </tr>
                                <tr class="tabForm">
                                    <td>
                                        <mcs:UC_GridView ID="gv_List" runat="server" Width="96%" AllowPaging="True" PageSize="10"
                                            OnPageIndexChanging="gv_List_PageIndexChanging" AutoGenerateColumns="true"
                                            DataKeyNames="ID" onrowdatabound="gv_List_RowDataBound" >
                                         <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" ToolTip="全选" onclick="javascript:SelectAll(this);">
                                                        </asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_ID" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="120px" HeaderText="审批意见" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbx_Remark" runat="server" Text='' Width="120">
                                                         
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                         </Columns>
                                        </mcs:UC_GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_Approve" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btn_UnApprove" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_State" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <script language="javascript" type="text/javascript">
    divGridView.style.width = window.screen.availWidth - 50;      
    </script>
</asp:Content>

