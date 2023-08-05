<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="app_profile.aspx.cs" Inherits="SchoolTours.app_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script>
        $(document).ready(function () {
            $('.phone_us').mask('000.000.0000');
        });
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script src="../Scripts/CustomJS/jquery.mask.min.js"></script>
    <div style="height: calc(100vh - 185px);" class="py-4">
        <div class="card mb-0 bg-primary p-3 h-100">
            <div class="d-flex flex-column justify-content-center align-items-center col-md-5 m-auto align-self-stretch h-100">
                <div class="w-100">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group mb-2">
                                <asp:TextBox ID="input_given_nm" runat="server" class="form-control" MaxLength="50" placeholder="Given Name(s)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="input_given_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group mb-2">
                                <asp:TextBox ID="input_last_nm" runat="server" class="form-control" MaxLength="50" placeholder="Last Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="input_last_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group mb-2">
                                <asp:TextBox ID="input_phone" runat="server" class="form-control phone_us" MaxLength="13" placeholder="Phone" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                <%--<asp:TextBox ID="input_phone" runat="server" class="form-control" MaxLength="13" placeholder="Phone" AutoPostBack="true" OnTextChanged="input_phone_TextChanged"></asp:TextBox>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="input_phone" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtPhone" runat="server" ControlToValidate="input_phone"
                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="Enter number in 123.123.1234 format."
                                            ForeColor="Red" ValidationExpression="\d\d\d.\d\d\d.\d\d\d\d"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group mb-2">
                                <asp:TextBox ID="input_eMail" runat="server" class="form-control" MaxLength="50" placeholder="eMail"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="input_eMail" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="input_eMail" ForeColor="Red"
                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ErrorMessage="*" Class="validation-class"  />
                            </div>
                        </div>
                        <div class="w-100"></div>
                        <div class="col-md-6">
                            <div class="form-group mb-2 position-relative">
                                <asp:TextBox ID="input_passcode_old" runat="server" class="form-control" placeholder="Old Password" TextMode="Password" MaxLength="64"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="input_passcode_old" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                <a href="javascript:void(0);" class="pass-view-icon" onclick="javascript:shwPwd('old');">
                                    <img src="Content/images/password_eye.png" alt="" width="30">
                                </a>
                            </div>
                        </div>
                        <div class="w-100"></div>
                        <div class="col-md-6">
                            <div class="form-group mb-2 position-relative">
                                <asp:TextBox ID="input_passcode_new" runat="server" class="form-control" placeholder="New Password" TextMode="Password" MaxLength="64"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="input_passcode_new" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="input_passcode_new"
                                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}" ErrorMessage="*" ForeColor="Red" Class="validation-class" />

                                <a href="javascript:void(0);" class="pass-view-icon" onclick="javascript:shwPwd('new');">
                                    <img src="Content/images/password_eye.png" alt="" width="30">
                                </a>
                            </div>
                        </div>
                        <div class="w-100"></div>
                        <div class="col-md-6">
                            <div class="form-group mb-2 position-relative">
                                <asp:TextBox ID="input_passcode_vfy" runat="server" class="form-control" placeholder="Verify Password" TextMode="Password" MaxLength="64"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="input_passcode_vfy" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="input_passcode_vfy" CssClass="ValidationError" ControlToCompare="input_passcode_new"
                                    ErrorMessage="*" ToolTip="Password must be the same"  Class="validation-class"/>
                                <a href="javascript:void(0);" class="pass-view-icon" onclick="javascript:shwPwd('vfy');">
                                    <img src="Content/images/password_eye.png" alt="" width="30">
                                </a>
                            </div>
                        </div>

                        <div class="col-md-6 text-right">
                            <asp:ImageButton ID="img_sav" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="36" OnClick="setEmp"  title="Save"/>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>
    <script type="text/javascript">
        function shwPwd(val) {
            debugger;
            if ($('#MainContent_input_passcode_' + val).attr('type') == "text")
                $('#MainContent_input_passcode_' + val).attr('type', 'password');
            else
                $('#MainContent_input_passcode_' + val).attr('type', 'text');
        }
    </script>
</asp:Content>
