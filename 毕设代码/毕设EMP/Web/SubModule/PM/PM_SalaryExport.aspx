<%@ Page Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master" AutoEventWireup="true"
    CodeFile="PM_SalaryExport.aspx.cs" Inherits="SubModule_PM_PM_SalaryExport" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div>
        <p style='font-size:20px; color:red;'>��ѡ�����Ӫҵ����ֱ𵼳����к�ũ�й����ļ�</p>
        </div>
    
        
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td class="dataLabel">
                    Ӫҵ��
                </td>
                <td class="style1">
                    <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                        ParentColumnName="SuperID" Width="180px" />
                </td>
                <td class="dataLabel">
                    �����
                </td>
                <td class="dataField">
                    <asp:DropDownList ID="ddl_AccountMonth" runat="server" DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="ũ��Excel���ʵ�" Width="130px" oncommand="btn_Command" CommandName="ABCExcel" />
                    <asp:Button ID="btnABC" runat="server" Text="ũ��Txt���ʵ�" Width="130px" oncommand="btn_Command" CommandName="ABC" />
                    <asp:Button ID="btnCCB" runat="server" Text="���й��ʵ�" Width="130px" oncommand="btn_Command" CommandName="CCB" />
                </td>
            </tr>
        </table>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    </asp:Content>

