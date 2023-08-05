<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EASend.aspx.cs" Inherits="SchoolTours.EASend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


    <div>
        <asp:FileUpload ID="file_upload" runat="server" AllowMultiple="true" />
    <asp:Button ID="btnSendmail" runat="server" OnClick="btnSendmail_Click" Text="Send mail" />
        <asp:Button ID="btn" runat="server" OnClick="btn_Click" Text="Send mail" />
    </div>

        <asp:Button  ID="btnSendGrid" runat ="server" Text="Send Grid" OnClick="btnSendGrid_Click"/>


        <asp:TextBox ID="txtchange" runat="server" AutoPostBack="true" OnTextChanged="txtchange_TextChanged"></asp:TextBox>
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
        <%--https://www.googleapis.com/oauth2/v4/token--%>
    </form>
</body>
</html>
