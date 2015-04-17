<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BasicMasterPage.master"
    AutoEventWireup="true" CodeFile="RetailerAttachmentBatDownload.aspx.cs" Inherits="SubModule_CM_RT_RetailerAttachmentBatDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="moduleTitle" width="100%">
                    <tr>
                        <td width="24">
                            <img height="16" src="../../../DataImages/ClientManage.gif" width="16" />
                        </td>
                        <td nowrap="noWrap">
                            <h2>客户附件及图片列表
                            </h2>
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
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <h3>查询条件</h3>
                                    </td>
                                    <td></td>
                                    <td align="right">
                                        <asp:Button ID="btnDownAll" runat="server" Text="下载全部文件" Width="80px" OnClick="btnDownAll_Click" />
                                        <asp:Button ID="btnDownSelected" runat="server" Text="下载选中文件" Width="80px" OnClick="btnDownSelected_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tabForm">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="dataLabel" width="80px">管理片区
                                    </td>
                                    <td width="210">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <mcs:MCSTreeControl ID="tr_OrganizeCity" runat="server" IDColumnName="ID" NameColumnName="Name"
                                                    ParentColumnName="SuperID" Width="200px" DisplayRoot="True" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="dataLabel">门店
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <mcs:MCSSelectControl runat="server" ID="select_Client" PageUrl="../PopSearch/Search_SelectClient.aspx?ClientType=3"
                                                    Width="300px" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="dataLabel">上传日期
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_begin" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbx_begin"
                                            Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        至<asp:TextBox ID="tbx_end" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tbx_end"
                                            Display="Dynamic" ErrorMessage="日期格式不对" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    </td>
                                    <td class="dataLabel">附件名称
                                    </td>
                                    <td class="dataField">
                                        <asp:TextBox ID="tbx_FindName" runat="server"></asp:TextBox>
                                        <%--<asp:CheckBox ID="cb_OnlyPic" runat="server" Text="仅图片" />--%>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="bt_Find" runat="server" Text="查  找" Width="60px" OnClick="bt_Find_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridAttachment" runat="server" EnableModelValidation="True" DataKeyNames="ID,GUID" AllowSorting="true" PageSize="10" AutoGenerateColumns="false" OnRowDataBound="GridAttachment_RowDataBound" AllowPaging="true" OnPageIndexChanging="GridAttachment_PageIndexChanging" style="width:100%" >
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="AttachChecked"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="门店名称" />
                    <asp:BoundField HeaderText="门店编码" />
                    <asp:TemplateField HeaderText="图片名" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label ID="lb_Name" runat="server" Text='<%# Eval("Name").ToString()+"("+Eval("FileSize").ToString()+"KB)" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="上传时间" DataField="UploadTime"></asp:BoundField>
                    <asp:TemplateField HeaderText="图片">
                        <ItemTemplate>
                            <asp:Image ID="img" runat="server" Width="100" Height="100" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
