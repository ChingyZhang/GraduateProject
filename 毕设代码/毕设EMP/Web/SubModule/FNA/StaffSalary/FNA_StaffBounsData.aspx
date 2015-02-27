<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true" CodeFile="FNA_StaffBounsData.aspx.cs" Inherits="SubModule_FNA_StaffSalary_FNA_StaffBounsDetail" %>

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
                <h2>办事处主管月度考核明细表界面</h2>
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
                        <asp:Button ID="bt_Submit" runat="server" Text="提 交" Width="60px" OnClick="bt_Submit_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </td>
</tr>
<tr>
    <td  align="center"><div id="div_header" runat="server" style="font-size:16pt">XX年第XX季度全国办事处绩效考核明细表</div></td>
</tr>
<tr><td align="right">审批标志：<asp:Label ID="lb_ApproveFlag" runat="server" Text="" Width="100"></asp:Label></td></tr>
<tr>
<td>
    <mcs:UC_GridView ID="gv_List" runat="server"  Width="100%"  GridLines="Both" 
        CellPadding="1" BackColor="#BBBBBB" CellSpacing="1" CssClass=""
       BorderWidth="0px" AllowPaging="true" PageSize="50" 
        OnPageIndexChanging="gv_List_PageIndexChanging" ondatabound="gv_List_DataBound">
     <HeaderStyle BackColor="#DDDDDD" CssClass="" Height="28px" />
     
    </mcs:UC_GridView>
</td>
</tr>
</table>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

