<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="RetailerList.aspx.cs" Inherits="SubModule_RM_RetailerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function searchtext_Onfocus(sender) {
            if (sender.value == "输入客户编号/ 名称/ 联系人/ 电话查询") {
                sender.style.color = "rgb(0,0,0)";
                sender.value = "";
            }


        }
        function searchtext_OnBlur(sender) {
            if (sender.value == "") {
                sender.style.color = "rgb(170,170,170)";
                sender.value = "输入客户编号/ 名称/ 联系人/ 电话查询";
            }
        }
    </script>
    <table cellspacing="0" cellpadding="0" width="100%" border="0" style="float: left;">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16">
                        </td>
                        <td nowrap="noWrap">
                            <h2>
                                <asp:Label ID="lb_PageTitle" runat="server" Text="零售商列表"></asp:Label><asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bt_Add" runat="server" Text="新 增" Width="60px" OnClick="bt_Add_Click"
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <mcs:MCSTabControl ID="MCSTabControl1" runat="server" CssSelectedLink="current" OnOnTabClicked="MCSTabControl1_OnTabClicked"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <mcs:MCSTabItem Target="_self" Text="快捷查询" Description=""
                            Value="0" Enable="True"></mcs:MCSTabItem>
                        <mcs:MCSTabItem Target="_self" Text="高级查询" Description=""
                            Value="1" Enable="True" Visible="false"></mcs:MCSTabItem>
                    </Items>
                </mcs:MCSTabControl>
            </td>
        </tr>
        <tr class="tabForm">
            <td height="30px">
                <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr runat="server" id="tr_basicsearch">
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>关键字：<asp:TextBox runat="server" ID="tbx_Condition" Text="输入客户编号/ 名称/ 联系人/ 电话查询" Width="280"
                                        onfocus="searchtext_Onfocus(this)" Style="color: rgb(170,170,170)" onblur="searchtext_OnBlur(this)"></asp:TextBox>
                                    </td>
                                    <td>新增日期:<asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="65px"></asp:TextBox>
                                        <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator1"
                                            runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                            Type="Date" ControlToValidate="tbx_begin"></asp:CompareValidator><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填" ControlToValidate="tbx_begin"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        至
                                    <asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="65px"></asp:TextBox>
                                        <span style="color: #FF0000">*</span><asp:CompareValidator ID="CompareValidator2"
                                            runat="server" ErrorMessage="日期格式不对" Display="Dynamic" Operator="DataTypeCheck"
                                            Type="Date" ControlToValidate="tbx_end"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbx_end"
                                            Display="Dynamic" ErrorMessage="必填"></asp:RequiredFieldValidator></td>
                                    <td>合作状态：
                                        <asp:DropDownList ID="ddl_ActiveFlag" DataTextField="Value" DataValueField="Key"
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="bt_Find" runat="server" Text="快捷查询" Width="60px" OnClick="bt_Find_Click" /></td>
                                </tr>
                            </table>

                        </td>
                        <td align="right">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="1px"></td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcs:UC_GridView ID="gv_List" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="CM_Client_ID" PageSize="15" Width="100%"
                            PanelCode="Panel_TDP_RetailerList">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="CM_Client_ID" DataNavigateUrlFormatString="RetailerDetail.aspx?ClientID={0}"
                                    DataTextField="CM_Client_FullName" HeaderText="门店名称" ControlStyle-CssClass="listViewTdLinkS1"
                                    SortExpression="CM_Client_FullName" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                       <%-- <asp:HyperLink ID="view" runat="server" NavigateUrl='<%# Bind("CM_Client_ID","~/SubModule/CM/ClientPictureList.aspx?ClientID={0}")%>'
                                            Text="图片管理" CssClass="listViewTdLinkS1">
                                        </asp:HyperLink>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <a style="cursor: pointer; color: #990033" onclick='<%#Eval("CM_Client_ID","PopShow({0})") %>'>地图</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                无数据
                            </EmptyDataTemplate>
                        </mcs:UC_GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="bt_Find" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
