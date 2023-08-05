<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unsubscribe_Endpoint.aspx.cs" Inherits="SchoolTours.Unsubscribe_Endpoint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
<webopt:BundleReference runat="server" Path="~/Content/css" />
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
<link href="Content/style.css" rel="stylesheet" />
<script type="text/javascript">
    var myGeeksforGeeksWindow;
    function closeWin11() {
        myGeeksforGeeksWindow.close();
    }
    function closeWin() {
        alert(11);
        var win = window.open('', '', 'width=200,height=100');
        win.focus();
        win.onunload = function () {
            window.location.search = "querystring"
        };
    }
</script>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        
        <div id="exampleModal" runat="server" visible="false">
            
        <div class="modal-dialog" role="document" style="position: absolute; top: 2%; left: 34%; width: 500px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: black;">Confirmation</h5>
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>--%>
                </div>
                <div class="modal-body" id="divContent" runat="server" style="color: black;">
                    Mail sent successfully
                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>--%>
                   <%-- <button onclick="closeWin()" class="btn btn-primary">Ok</button>--%>
                    <asp:Button ID="bttn_Ok" runat="server" OnClick="bttn_Ok_Click" Text="Ok" class="btn btn-primary" Style="display: block;" />
                </div>
            </div>
        </div>
        </div>
    </form>
</body> 
</html>
