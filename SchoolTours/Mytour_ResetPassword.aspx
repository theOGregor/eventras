<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mytour_ResetPassword.aspx.cs" Inherits="SchoolTours.Mytour_ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body class="d-flex align-items-center justify-content-center">
    <form id="form1" runat="server" class="form-signin">
        <div class="w-100 custom-border">
            <div class="div_minimizer2_logo">
                <img class="img-fluid" src="Content/images/myTourLogo.png" alt="mytour" />
            </div>
            <div class="row align-items-end">
                <div class="col-md-3 mb-3">
                    <img class="img-fluid" src="Content/images/bds-logo.png" alt="bds" />
                </div>
                <div class="col-md-8 ml-auto">
                    <div class="div_error_msg text-center">
                        <p>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="input_eMail" ForeColor="Red"
                                ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ErrorMessage="Invalid email address" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="input_eMail" ForeColor="Red" Display="Dynamic" ErrorMessage="Email is Required" />

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="input_tempcode" ForeColor="Red" Display="Dynamic" ErrorMessage="Temporary Password is Required" />

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="input_passcode"
                                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}"
                                ErrorMessage="Your new password must be at least eight charactors and have upper and lower case letters and at least one number or symbol." ForeColor="Red" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="input_passcode" ForeColor="Red" Display="Dynamic" ErrorMessage="password is Required" />
                            <asp:Label ID="lblErrorMsg" runat="server" Text="" Style="color: red;"></asp:Label>
                        </p>
                    </div>
                    <div class="form-group">
                        <label for="email" class="sr-only">Email address</label>
                        <asp:TextBox ID="input_eMail" runat="server" class="form-control" placeholder="Email address" MaxLength="100"></asp:TextBox>

                    </div>
                    <div class="form-group position-relative">
                        <label for="password" class="sr-only">Temporary Password</label>
                        <asp:TextBox ID="input_tempcode" runat="server" class="form-control" placeholder="Temporary Password" MaxLength="32" TextMode="password"></asp:TextBox>

                        <a href="#" id="img_shw_tempcode" class="pass-view-icon" onclick="shwPwd('tempcode')">
                            <img src="Content/images/password_eye.png" alt="" width="30"></a>
                    </div>
                    <div class="form-group position-relative">
                        <label for="password" class="sr-only">Passcode</label>
                        <asp:TextBox ID="input_passcode" runat="server" TextMode="Password" class="form-control" placeholder="Password"></asp:TextBox>
                        <a href="#" id="img_shw_passcode" class="pass-view-icon" onclick="shwPwd('passcode')">
                            <img src="Content/images/password_eye.png" alt="" width="30"></a>
                    </div>
                    <div class="w-100 mt-3 text-right">
                        <asp:Button ID="bttn_save" runat="server" OnClick="savPasscode" Text="Save" class="btn btn-primary ml-3" />

                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>

<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
<link href="Content/style.css" rel="stylesheet" />

<script type="text/javascript">
    function shwPwd(val) {
        if ($('#input_' + val).attr('type') == "text")
            $('#input_' + val).attr('type', 'password');
        else
            $('#input_' + val).attr('type', 'text');

    }
</script>