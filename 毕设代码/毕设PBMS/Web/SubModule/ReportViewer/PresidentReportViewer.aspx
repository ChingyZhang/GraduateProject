<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PresidentReportViewer.aspx.cs" Inherits="SubModule_ReportViewer_PresidentReportViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            color: #272727;
            text-align: center;
            font-family: "微软雅黑";
            margin: 0px;
            padding: 0px;
        }



        .header {
            background: -moz-linear-gradient(top,#30aefb,#279de5); /*Mozilla*/
            background: -webkit-gradient(linear,0% 0%,0% 100%,from(#30aefb),to(#279de5)); /*Old gradient for webkit*/
            background: -webkit-linear-gradient(top,#30aefb,#279de5); /*new gradient for Webkit*/
            background: -o-linear-gradient(top,#30aefb,#279de5); /*Opera11*/
            background: -ms-linear-gradient(top,#30aefb, #279de5); /*For IE10*/
            background: linear-gradient(top,#30aefb, #279de5); /*CSS3*/
            text-align: left;
            min-width: 800px;
            max-width: 1024px;
            margin: 0px auto;
            overflow: auto;
        }

            .header a {
                display: block;
                float: left;
                height: 76px;
                width: 244px;
                line-height: 76px;
                margin: 5px;
                background: url(../../App_Themes/basic/images/toptitle01.png) no-repeat;
            }

        .main {
            min-width: 800px;
            max-width: 1024px;
            margin: 0px auto;
            /*background: rgb(255,255,255);*/
            background: #f3faff;
            overflow: auto;
        }

            .main div {
                overflow: auto;
                padding: 0px 0px;
                margin: 0px;
                border: 0px solid #ccc;
            }

            .main ul {
                list-style: none;
            }

                .main ul li {
                    display: block;
                    float: left;
                    position: relative;
                    padding: 10px 0px;
                    width: 32%;
                }

                    .main ul li a {
                        font-size: 20px;
                    }

        .footer {
            min-width: 800px;
            max-width: 1024px;
            text-align: center;
            background: #ddd;
            border-top: 1px solid #ccc;
            margin: 0px auto;
            padding: 0px;
            overflow: auto;
        }

        .pubTit {
            color: #fff;
            font-weight: bold;
            font-size: 16px;
            height: 30px;
            line-height: 30px;
            padding-left: 15px;
            text-align: left;
        }

        .pubTit1 {
            background: #f38121;
        }

        .pubTit2 {
            background: #8cbf1a;
        }

        .pubTit3 {
            background: #036eb8;
        }

        .pubTit4 {
            background: #d72e8a;
        }

        .pubTit5 {
            background: #d6000f;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <a href="#"></a>
        </div>
        <div class="main">
            <div>
                <h2 class="pubTit pubTit1">>><%=pathname.ToString() %></h2>
            </div>
            <div>
                <ul>
                    <li>
                        <%--<a href='PresidentReportViewer.aspx?FolderID=<%=superid.ToString() %>'>--%>
                        <a href='<%=superid==1?"../Login/index.aspx?ReturnUrl=%2fWeb%2fSubModule%2fReportViewer%2fPresidentReportViewer.aspx":"PresidentReportViewer.aspx?FolderID="+superid.ToString() %>'>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/basic/images/folder.png" /><br />
                            <asp:Label ID="Label1" runat="server" Text="返回上层"></asp:Label>
                        </a>
                    </li>
                    <asp:Repeater runat="server" ID="rp_1">
                        <ItemTemplate>
                            <li>
                                <a href='PresidentReportViewer.aspx?FolderID=<%# Eval("ID") %>'>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/basic/images/folder.png" /><br />
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div>
                <h2 class="pubTit pubTit4">>>目录报表</h2>
            </div>
            <div>
                <ul>
                    <asp:Repeater runat="server" ID="rp_2">
                        <ItemTemplate>
                            <li>
                                <a href='../ReportViewer/ReportViewer.aspx?Report=<%# Eval("ID") %>'>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/basic/images/file02.png" /><br />
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="footer">
            <p>
                @ 2004-2011 <a class="copyRightLink" href="http://www.meichis.com" style="color: #990033"
                    target="_blank">南京美驰资讯科技开发有限公司 </a>版权所有.
            </p>
        </div>
    </form>
</body>
</html>
