<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="app_emp_div.aspx.cs" Inherits="SchoolTours.ApplicationsSettings.app_emp_div" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>

      <script src="../Scripts/CustomJS/jquery.mask.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).ready(function () {
                $('.phone_us').mask('000.000.0000');
            });
            function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
        });
    </script>
    <style>
        .image-upload > .file-upload {
            display: block;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 9999;
            opacity: 0;
        }
    </style>
    <section class="main">
        <asp:HiddenField  ID="div_id" runat="server"/>
        <asp:HiddenField  ID="employee_id" runat="server"/>
        <asp:HiddenField  ID="employee_nm" runat="server"/>
        <asp:HiddenField  ID="div_emp_id" runat="server"/>
        <div class="row mx-0 align-items-stretch h-100 pt-4">
            <div class="col-lg-3 col-md-4">
                <div class="card bg-light-blue h-100 mb-0">
                    <div class="card-body">
                        <h4 class="text-white">Tools</h4>
                        <ul class="list-unstyled">
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_emp_div');">
                                <h5 class="text-active">Employee / Division</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_emp_role');">
                                <h5 class="text-white">Employee / Role</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_evt');">
                                <h5 class="text-white">Event Maintenance</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_page_role');">
                                <h5 class="text-white">Page / Role</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_ref');">
                                <h5 class="text-white">Reference Tables</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_tags');">
                                <h5 class="text-white">Tag Maintenance</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('bounced');"><h5 class="text-white">Bounced eMail</h5></a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="col-lg-9 col-md-8">
                <div class="card bg-primary h-100 mb-0">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-7">
                                <div class="form-row">
                                    <div class="col-md-12">
                                        <div class="form-group mb-2">
                                            <asp:DropDownList ID="select_div" runat="server" class="custom-select" OnSelectedIndexChanged="dtlDiv" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_div_nm" runat="server" class="form-control" placeholder="Division Name" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_div" ControlToValidate="input_div_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_toll_free_phone" runat="server" class="form-control phone_us" placeholder="Toll Free Phone" MaxLength="13" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <%--<asp:TextBox ID="input_toll_free_phone" runat="server" class="form-control" placeholder="Toll Free Phone" MaxLength="13" AutoPostBack="true" OnTextChanged="input_toll_free_phone_TextChanged"></asp:TextBox>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_div" ControlToValidate="input_toll_free_phone" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtPhone" runat="server" ControlToValidate="input_toll_free_phone" ValidationGroup="validation_div"
                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="Enter number in 123.123.1234 format."
                                            ForeColor="Red" ValidationExpression="\d\d\d.\d\d\d.\d\d\d\d"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_local_phone" runat="server" class="form-control phone_us" placeholder="Local Phone" MaxLength="13" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <%--<asp:TextBox ID="input_local_phone" runat="server" class="form-control" placeholder="Local Phone" MaxLength="13" AutoPostBack="true" OnTextChanged="input_local_phone_TextChanged"></asp:TextBox>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_div" ControlToValidate="input_local_phone" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="input_local_phone" ValidationGroup="validation_div"
                                            Display="Dynamic" SetFocusOnError="true" ErrorMessage="Enter number in 123.123.1234 format."
                                            ForeColor="Red" ValidationExpression="\d\d\d.\d\d\d.\d\d\d\d"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_url" runat="server" class="form-control" placeholder="URL" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validation_div" ControlToValidate="input_url" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-right my-3">
                                        <asp:ImageButton ID="img_delete" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="36" OnClick="delDiv" OnClientClick="return confirm('Are you sure you want to delete this record?');" title="Delete"/>
                                        <asp:ImageButton ID="img_new" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="36" OnClick="newDiv" title="Add" />
                                        <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="36" OnClick="setDiv" ValidationGroup="validation_div" title="Save"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="col-auto ml-auto">
                                        <div class="image-upload overflow-hidden position-relative">
                                            <label for="file_upload">
                                                <img src="../Content/images/icon_attach.png" alt="" width="30" title="Attachment">
                                            </label>
                                            <asp:FileUpload ID="file_upload" runat="server" Class="file-upload" accept="image/*" onchange="this.form.submit()" />
                                            <%--onchange="ShowImagePreview(this);"--%>
                                        </div>

                                    </div>
                                    <div class="col-md-12 text-center mt-3">
                                        <div class="C_logo">
                                            <asp:Image ID="div_logo" runat="server" alt="" class="img-fluid" Width="150" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:ListBox ID="select_employee" runat="server" class="list-unstyled custom-list" Style="border: 0px; width: 100%;" ></asp:ListBox>

                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group mb-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1 px-0">
                                <asp:ImageButton class="d-block mt-3 mx-auto" ID="img_new_emp" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="36" OnClick="setDivEmp" title="Add"/>
                                <asp:ImageButton class="d-block mt-3 mx-auto" ID="img_del" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" OnClick="delEmpDiv" Width="36" title="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                            </div>
                            <div class="col-md-3">
                                <div class="form-row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:ListBox ID="select_div_employee" runat="server" class="list-unstyled custom-list" Style="border: 0px; width: 100%;" OnSelectedIndexChanged="dtlEmp" AutoPostBack="true"></asp:ListBox>

                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_email" runat="server" class="form-control" placeholder="Email" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_emp_div" ControlToValidate="input_email" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="input_email" ValidationGroup="validation_emp_div" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-right mt-3">
                                        <asp:ImageButton ID="img_save_emp" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="36" OnClick="setDivEmpEmail" ValidationGroup="validation_emp_div" title="Save"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
