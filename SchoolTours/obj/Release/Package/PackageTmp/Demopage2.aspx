<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demopage2.aspx.cs" Inherits="SchoolTours.Demopage2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
<script type="text/javascript">
    function EnterEvent(e) {
        if (e.keyCode == 13) {
            __doPostBack('<%=Button2.UniqueID%>', "");
        }
    }
     <%-- function EnterEvent2(e) {
        if (e.keyCode == 13) {
            __doPostBack('<%=Button3.UniqueID%>', "");
        }
    }--%>
  
</script>
<body>
    <form id="form1" runat="server" defaultbutton="Button2">
        <div>
           <%-- <asp:TextBox ID="TextBox1" runat="server" onkeypress="return EnterEvent(event)"></asp:TextBox>--%>

            <asp:TextBox ID="TextBox2" ClientIDMode="Static" runat="server" onkeypress="return EnterEvent(event)"></asp:TextBox>
            <asp:Button ID="Button2" OnClick="Button2_Click" runat="server" Style="display: none" Text="Button" />
        </div>
    </form>

   
</body>
</html>
