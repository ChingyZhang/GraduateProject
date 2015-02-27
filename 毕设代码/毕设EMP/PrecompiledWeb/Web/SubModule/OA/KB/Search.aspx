<%@ page language="C#" autoeventwireup="true" inherits="SubModule_OA_KB_Search, App_Web_z_fgc1lb" enableEventValidation="false" stylesheettheme="basic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>知识库搜索</title>
    <base target="_self">
    </base>
</head>
<body>
    <form id="Form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="moduleTitle">
                    <tr>
                        <td style="width: 25px">
                            <img height="16" src="../../../DataImages/help_book.gif" width="15">
                        </td>
                        <td align="left">
                            <h2>
                                知识库搜索</h2>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="left">
                            查找内容
                        </td>
                        <td>
                            <asp:TextBox ID="txt_keyword" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td align="right" rowspan="2">
                            <asp:ImageButton ImageUrl="~/Images/gif/gif-0072.gif" ID="btn_searcharticle" runat="server"
                                Text="搜索" OnClick="btn_searcharticle_Click" Width="60px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            查找范围
                        </td>
                        <td>
                            <asp:CheckBox ID="chb_title" Text="标题" runat="server" Checked="true" />
                            <asp:CheckBox ID="chb_keyword" Text="关键字" runat="server" Checked="true" />
                            <asp:CheckBox ID="chb_content" Text="内容" runat="server" Checked="true" />
                            <asp:CheckBox ID="chb_IncludePreCondition" Text="在结果中搜索" runat="server" Checked="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tabForm">
            <td>
                <mcs:UC_GridView ID="ud_grid" runat="server" PanelCode="Panel_KB_ArticleList" AutoGenerateColumns="False"
                    Width="100%" DataKeyNames="KB_Article_ID" AllowSorting="true" AllowPaging="True"
                    PageSize="18">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="KB_Article_ID" DataNavigateUrlFormatString="ArticleDetail.aspx?ID={0}"
                            HeaderText="标题" DataTextField="KB_Article_Title" ControlStyle-CssClass="listViewTdLinkS1"
                            >
                            <ControlStyle CssClass="listViewTdLinkS1"></ControlStyle>
                        </asp:HyperLinkField>
                    </Columns>
                </mcs:UC_GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
