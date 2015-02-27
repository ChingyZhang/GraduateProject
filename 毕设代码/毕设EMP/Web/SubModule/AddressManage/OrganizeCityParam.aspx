<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="OrganizeCityParam.aspx.cs" Inherits="SubModule_AddressManage_OrganizeCityParam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional" ChildrenAsTriggers="true">
   <ContentTemplate>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                            <tr>
                                <td align="right" width="20">
                                    <img height="16" src="../../DataImages/ClientManage.gif" width="16">
                                </td>
                                <td align="left" width="130">
                                    <h2>
                                        系统参数设定</h2>
                                </td>
                                <td style="color:Red"> &nbsp;</td>
                                <td align="right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tabForm">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="dataLabel" width="80px">
                                    管理片区
                                </td>
                                <td width="320">
                                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                        ParentColumnName="SuperID" Width="200px" AlwaysSelectChildNode="False" AutoPostBack="True"
                                        OnSelected="tr_OrganizeCity_Selected" SelectDepth="0" RootValue="0" DisplayRoot="True" Height="20" />
                                </td>
                                <td class="dataLabel" width="70px">系统参数类型</td>
                                <td width="120">
                                    <asp:DropDownList ID="ddl_ParamType" runat="server" DataTextField="Value" DataValueField="Key">
                                    </asp:DropDownList>
                                </td>
                                <td width="70">
                                    &nbsp;</td>
                                <td width="150" align="right">
                                    <asp:CheckBox ID="cb_include" runat="server" Text="包含下级片区" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" Text="查找"
                                        Width="60" />
                                    &nbsp;<asp:Button ID="btn_Add" runat="server" OnClick="btn_Add_Click" Text="新增"
                                        Width="60" />
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="div_detail" runat="server" visible="false">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" height="30" class="h3Row">
                                                <tr>
                                                    <td>
                                                        <h3>
                                                            新增系统参数/h3>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tabForm">
                                            
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" >
                                                        <tr height="28px">
                                                            <td width="23%">
                                                                系统参数类型</td>
                                                            <td width="23%">
                                                                系统参数值</td>
                                                            <td width="35%">备注</td>
                                                                <td align="right" rowspan="2">
                                                                <asp:Button ID="bt_Save" runat="server" OnClick="bt_Save_Click" Text="保存"
                                                                    Width="70px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_ParamType2" runat="server" DataTextField="Value" DataValueField="Key">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbx_ParamValue" runat="server" Width="50px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ControlToValidate="tbx_ParamValue" ErrorMessage="参数必填" ToolTip="必填">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbx_Remark" runat="server" Width="192px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                   
                                        </td>
                                    </tr>
                                </table>
                                 </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="center">
                                            <mcs:UC_GridView ID="gv_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                                Width="100%" OnRowDeleting="gv_List_RowDeleting" OnSelectedIndexChanging="gv_List_SelectedIndexChanging" Binded="true">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" SelectText="选择"  ControlStyle-CssClass="listViewTdLinkS1" />
                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                    <asp:BoundField DataField="OrganizeCity" HeaderText="管理片区" />
                                                    <asp:BoundField DataField="ParamType" HeaderText="系统参数类型" />
                                                    <asp:BoundField DataField="ParamValue" HeaderText="系统参数值" />
                                                    <asp:BoundField DataField="Remark" HeaderText="备注" />
                                                    <asp:BoundField DataField="InsertTime" HeaderText="录入时间"/>
                                                    <asp:BoundField DataField="InsertStaff" HeaderText="录入人"/>
                                                    <asp:BoundField DataField="UpdateTime" HeaderText="更新时间"/>
                                                    <asp:BoundField DataField="UpdateStaff" HeaderText="更新人"/>
                                                    <asp:CommandField ShowDeleteButton="true" DeleteText="删除" ControlStyle-CssClass="listViewTdLinkS1" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    无数据
                                                </EmptyDataTemplate>
                                            </mcs:UC_GridView>
                                        
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </ContentTemplate>
                                       
         </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>


