<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="MYSaleInFlagEdit.aspx.cs" Inherits="SubModule_SVM_MYSaleInFlagEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                月度母婴进货门店提报
                            </h2>
                        </td>
                        <td align="right" nowrap="noWrap">
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr height="28px">
                                            <td nowrap>
                                                <h3>
                                                    查询条件</h3>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_OK" runat="server" Text="查找" Width="60px" 
                                                    onclick="btn_OK_Click" />
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
                                                <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" Width="220px" />
                                            </td>
                                           <td class="dataLabel">
                                                会计月
                                            </td>
                                            <td class="dataField" width="220px">
                                                <asp:DropDownList ID="ddl_Month" runat="server" DataTextField="Name" DataValueField="ID" Enabled="false" ></asp:DropDownList>
                                            </td>
                                            <td class="dataLabel">
                                                责任业务
                                            </td>
                                            <td class="dataField">
                                                <mcs:MCSSelectControl ID="select_Staff" runat="server" PageUrl="~/SubModule/StaffManage/Pop_Search_Staff.aspx"
                                                    Width="180px"  />
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="dataLabel">
                                                <%--<asp:DropDownList ID="ddl_DI" runat="server" AutoPostBack="true"
                                                    onselectedindexchanged="ddl_DI_SelectedIndexChanged">
                                                    <asp:ListItem Selected="true" Text="指定经销商" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="经销商名称" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="经销商编码" Value="2"></asp:ListItem>
                                                </asp:DropDownList>--%>
                                                经销商
                                            </td>
                                            <td>
                                                <mcs:MCSSelectControl ID="select_DI" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2" Width="280px"  />
                                                <asp:TextBox ID="tbx_DICode" runat="server" Width="160px" Visible="false"></asp:TextBox>
                                            </td>
                                            <td><%--<asp:DropDownList ID="ddl_RT" runat="server" AutoPostBack="true"
                                                    onselectedindexchanged="ddl_RT_SelectedIndexChanged">
                                                    <asp:ListItem Selected="true" Text="指定母婴店" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="母婴店名称" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="母婴店编码" Value="2"></asp:ListItem>
                                                </asp:DropDownList>--%>
                                                母婴店
                                                </td>
                                            <td>
                                                <mcs:MCSSelectControl ID="select_RT" runat="server" PageUrl="~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=3" Width="280px"  />
                                                <asp:TextBox ID="tbx_RTCode" runat="server" Width="160px" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
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
                        <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
        <td>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
        <table class="h3Row" height="30" cellspacing="0" cellpadding="0" width="100%" border="0">
             <tr height="28px">
                 <td nowrap>
                  <h3>门店标识填报</h3>
                 </td>
                 <td align="right">
                    当月生效母婴门店数：
                     <asp:Literal ID="lt_RTCount" runat="server"></asp:Literal>
                 </td>
                 <td align="right">
                    <asp:CheckBox runat="server" ID="cbx_Head" Text="全选" onclick="SelectAll(this)" Visible="false" />
                 </td>
                 <td align="right">
                    <asp:Button ID="bt_Submit" runat="server" Text="确认提交" Width="60px" 
                         onclick="bt_Submit_Click" Visible="false"  />
                 </td>
             </tr>
         </table>
         </ContentTemplate>
         <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click" />
                    </Triggers>
         </asp:UpdatePanel>
         
        </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="tabForm">
                                <td>
                                    <mcs:UC_GridView ID="gv_ListDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                        GridLines="Horizontal" DataKeyNames="FlagID,ClientID"
                                        AllowPaging="true" PageSize="30" 
                                        onpageindexchanging="gv_ListDetail_PageIndexChanging"  >
                                        <Columns>
                                            <asp:BoundField DataField="ClientCode" HeaderText="门店代码" />
                                            <asp:HyperLinkField HeaderText="母婴店" ItemStyle-CssClass="listViewTdLinkS1" DataTextField="ClientName" DataNavigateUrlFields="ClientID" DataNavigateUrlFormatString="../CM/RT/RetailerDetail.aspx?ClientID={0}" />
                                            <asp:BoundField DataField="DICode" HeaderText="经销商代码" />
                                            <asp:HyperLinkField HeaderText="经销商名称" ItemStyle-CssClass="listViewTdLinkS1" DataTextField="DIName" DataNavigateUrlFields="DIID" DataNavigateUrlFormatString="../CM/DI/DistributorDetail.aspx?ClientID={0}" />
                                            <asp:BoundField DataField="ActiveFlagName" HeaderText="门店状态" />
                                            <asp:BoundField DataField="ManagerName" HeaderText="责任业代" />
                                            <asp:BoundField DataField="ManagerCode" HeaderText="业代工号" />
                                           <asp:TemplateField HeaderText="本月有进货">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="cbx_id" Checked='<%# (int)Eval("FlagID") !=0 %>'  />
                                            </ItemTemplate>
                                           </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Wrap="false" />
                                        <RowStyle  Wrap="false" />
                                    </mcs:UC_GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

