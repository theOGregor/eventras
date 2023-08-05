<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="app_emp_role.aspx.cs" Inherits="SchoolTours.ApplicationsSettings.app_emp_role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/CustomJS/jquery.mask.min.js"></script>
    <script type="text/javascript">
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
    <section class="main">
        <asp:HiddenField id="employee_id" runat="server"/>
        <asp:HiddenField id="role_id" runat="server"/>
        <div class="row mx-0 align-items-stretch h-100 pt-4">
            <div class="col-lg-3 col-md-4">
                <div class="card bg-light-blue h-100 mb-0">
                    <div class="card-body">
                        <h4 class="text-white">Tools</h4>
                        <ul class="list-unstyled">
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_emp_div');">
                                <h5 class="text-white">Employee / Division</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_emp_role');">
                                <h5 class="text-active">Employee / Role</h5>
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
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="table-responsive mb-3" style="height: calc(100vh - 300px);">
                                    <asp:GridView runat="server" ID="gv_employee_roles" DataKeyNames="employee_id" OnRowCommand="gv_employee_roles_RowCommand"
                                        CssClass="table table-borderless table-sm bg-white" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="true" Style="border: 0px;" OnPageIndexChanging="gv_employee_roles_PageIndexChanging" Class="mb-0">
                                        <Columns>
                                            <asp:TemplateField HeaderText="page" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk__page_nm" runat="server" Text='<%# Bind("employee_nm") %>' CommandName="dtlemployee" CommandArgument='<%#Eval("employee_id")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Admin" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Admin" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Checked='<%# Convert.ToString(Eval("admin_ind"))=="1"?true:false%>' Text='<%#Eval("employee_id") + ";" +Eval("admin_role_id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Manager" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Manager" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Text='<%#Eval("employee_id") + ";" +Eval("manager_role_id")%>' Checked='<%# Convert.ToString(Eval("manager_ind"))=="1"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Financial" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Financial" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Text='<%#Eval("employee_id") + ";" +Eval("financial_role_id")%>' Checked='<%# Convert.ToString(Eval("financial_ind"))=="1"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Producer" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Producer" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Text='<%#Eval("employee_id") + ";" +Eval("producer_role_id")%>' Checked='<%# Convert.ToString(Eval("producer_ind"))=="1"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Associate" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Associate" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Text='<%#Eval("employee_id") + ";" +Eval("associate_role_id")%>' Checked='<%# Convert.ToString(Eval("associate_ind"))=="1"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_given_nm" runat="server" class="form-control" placeholder="First Name" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_employee" ControlToValidate="input_given_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_last_nm" runat="server" class="form-control" placeholder="Last Name" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_employee" ControlToValidate="input_last_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_phone" runat="server" class="form-control phone_us" placeholder="Phone" MaxLength="50" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_employee" ControlToValidate="input_phone" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtPhone" runat="server" ControlToValidate="input_phone" ValidationGroup="validation_employee"
                                        Display="Dynamic" SetFocusOnError="true" ErrorMessage="Enter number in 123.123.1234 format."
                                        ForeColor="Red" ValidationExpression="\d\d\d.\d\d\d.\d\d\d\d"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">

                                    <asp:TextBox ID="input_eMail" runat="server" class="form-control" placeholder="Email" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_employee" ControlToValidate="input_eMail" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="input_eMail" ValidationGroup="validation_employee" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ErrorMessage="*" Class="validation-class" />

                                </div>
                            </div>
                            <div class="col-auto text-right">
                                <asp:ImageButton ID="img_reset" runat="server" ImageUrl="../Content/images/icon_reset.png" alt="" Width="36" OnClick="rstPasscode" title="Reset" />
                                <asp:ImageButton ID="img_del" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="36" OnClick="delEmp" OnClientClick="return confirm('Are you sure you want to delete this record?');" title="Delete" />
                                <asp:ImageButton ID="img_new" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="36" OnClick="newEmp" title="Add" />
                                <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="36" OnClick="setEmp" ValidationGroup="validation_employee" title="Save" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>
</asp:Content>
