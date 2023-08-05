<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mytour_index.aspx.cs" Inherits="SchoolTours.mytour_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body class="d-flex align-items-center justify-content-center">
    <form id="form1" runat="server" class="form-signin">
        <div class="w-100 custom-border">
            <div class="div_minimizer2_logo row align-items-end">
                <img class="img-fluid" id="img_mytour_logo" src="Content/images/myTourLogo.png" alt="mytour" />
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="input_eMail" ForeColor="Red" Display="Dynamic" ErrorMessage="email is Required" />
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Password" ForeColor="Red" Display="Dynamic" ErrorMessage="password is required" />--%>
                            <asp:Label ID="div_error_msg" runat="server" Text="" Style="color: red;"></asp:Label>
                        </p>
                    </div>
                    <div class="form-group">
                        <label for="lbl_eMail" class="sr-only">Email address</label>
                        <asp:TextBox ID="input_eMail" Text="" runat="server" class="form-control" placeholder="Email address" MaxLength="100"></asp:TextBox>

                    </div>
                    <div class="form-group position-relative">
                        <label for="lbl_passcode" class="sr-only">Password</label>
                        <asp:TextBox ID="input_passcode" Text="" runat="server" TextMode="Password" class="form-control" placeholder="Password" MaxLength="32"></asp:TextBox>

                        <a href="#" id="img_shw_passcode" class="pass-view-icon" onclick="javascript:shwPwd('passcode');">
                            <img src="Content/images/password_eye.png" alt="" width="30"></a>
                    </div>
                    <div class="form-group position-relative">
						<div class="">
                            
							<asp:RadioButton ID="radio_ind_ind_leader" runat="server" GroupName="radio_ind" Checked="true"/>
                            
							<label class="text-dark" for="customRadio1">I am leader of a group tour </label>
						</div>
						<div class="">
							 <asp:RadioButton ID="radio_ind_ind_member" runat="server" GroupName="radio_ind" />
                            
							<label class="text-dark" for="customRadio2">I am a member of an independent tour</label>
						</div>
					</div>
                    <div class="w-100 mt-3 text-right">
                        <asp:LinkButton ID="lnk_reset" runat="server" OnClick="rstPasscode">Reset Password</asp:LinkButton>
                        <asp:Button ID="bttn_sign_in" runat="server" Text="Sign in" OnClick="signIn" class="btn btn-primary ml-3" />

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
        if ($('#input_passcode').attr('type') == "text")
            $('#input_passcode').attr('type', 'password');
        else
            $('#input_passcode').attr('type', 'text');

    }
    $(function () {
        localStorage.widget = "";
    })
</script>
<script type="text/javascript">
    $(function () {
        $('.form-control').focus(function () {
            $(this).addClass("focus");
        });

        $('.form-control').blur(function () {
            $(this).removeClass("focus");
        });
    });
</script>
