<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="app_page_role.aspx.cs" Inherits="SchoolTours.ApplicationsSettings.app_page_role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="page_id" runat="server" />
    <section class="main">
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
                                <h5 class="text-white">Employee / Role</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_evt');">
                                <h5 class="text-white">Event Maintenance</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_page_role');">
                                <h5 class="text-active">Page / Role</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_ref');">
                                <h5 class="text-white">Reference Tables</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_tags');">
                                <h5 class="text-white">Tag Maintenance</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('bounced');">
                                <h5 class="text-white">Bounced eMail</h5>
                            </a></li>
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
                                    <asp:GridView runat="server" ID="gv_div_roles" DataKeyNames="page_id" OnRowCommand="gv_div_roles_RowCommand"
                                        CssClass="table table-borderless table-sm bg-white" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="true" Style="border: 0px;" OnPageIndexChanging="gv_div_roles_PageIndexChanging" Class="mb-0">
                                        <Columns>
                                            <asp:TemplateField HeaderText="page" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk__page_nm" runat="server" Text='<%# Bind("page_descr") %>' CommandName="dtlPage" CommandArgument='<%#Eval("page_id")%>'></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Admin" SortExpression="">
                                                <ItemTemplate>
                                                    <%--<asp:CheckBox ID="chk_role_Admin" runat="server" CommandName="123" AutoPostBack="true" CommandArgument='<%#Eval("page_id") + ";" +Eval("admin_role_id")%>' />--%>
                                                    <asp:CheckBox ID="chk_role_Admin" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Checked='<%# Convert.ToString(Eval("admin_ind"))=="1"?true:false%>'
                                                        Text='<%#Eval("page_id") + ";" +Eval("admin_role_id")%>' />
                                                    <%--Checked='<%# Eval("admin_ind")=="1"?false:true%>'--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Manager" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Manager" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Text='<%#Eval("page_id") + ";" +Eval("manager_role_id")%>' Checked='<%# Convert.ToString(Eval("manager_ind"))=="1"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Financial" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Financial" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Text='<%#Eval("page_id") + ";" +Eval("financial_role_id")%>' Checked='<%# Convert.ToString(Eval("financial_ind"))=="1"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Producer" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Producer" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Text='<%#Eval("page_id") + ";" +Eval("producer_role_id")%>' Checked='<%# Convert.ToString(Eval("producer_ind"))=="1"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Associate" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_role_Associate" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged" Text='<%#Eval("page_id") + ";" +Eval("associate_role_id")%>' Checked='<%# Convert.ToString(Eval("associate_ind"))=="1"?true:false%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_page_descr" runat="server" MaxLength="50" class="form-control" placeholder="Page Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_page" ControlToValidate="input_page_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_menu_nm" runat="server" MaxLength="50" class="form-control" placeholder="Menu Name"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_page" ControlToValidate="input_menu_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />--%>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_page_url" runat="server" MaxLength="50" class="form-control" placeholder="URL"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_page" ControlToValidate="input_page_url" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                </div>
                            </div>
                            <div class="col-auto text-right">
                                <asp:ImageButton ID="img_del" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="36" OnClick="delPage" OnClientClick="return confirm('Are you sure you want to delete this record?');" title="Delete" />
                                <asp:ImageButton ID="img_new" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="36" OnClick="newPage" title="Add" />
                                <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="36" OnClick="setPage" ValidationGroup="validation_page" title="Save" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>
</asp:Content>
