<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="bounced.aspx.cs" Inherits="SchoolTours.ApplicationsSettings.bounced" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                                <h5 class="text-white">Page / Role</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_ref');">
                                <h5 class="text-white">Reference Tables</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('app_tags');">
                                <h5 class="text-white">Tag Maintenance</h5>
                            </a></li>
                            <li><a href="#" onclick="javascript:redirectToAppSetting('bounced');"><h5 class="text-active">Bounced eMail</h5></a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="col-lg-9 col-md-8">
                <div class="card bg-primary h-100 mb-0">
                    <div class="card-body">
                        <div class="row align-items-center h-100 justify-content-center">
                            <div class="col-md-7">
                                <div class="form-row">
                                    <div class="col-md-12">
                                        <div class="form-group mb-2" style="color:black;">
                                            Provide a list of eMail addresses to remove from the database. Sperate eMail addresses with a comma. 
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_eMail_string" runat="server" class="form-control" placeholder="Description" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_del" ControlToValidate="input_eMail_string" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-right">
                                        <asp:ImageButton ID="img_delete" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="36" OnClick="img_delete_Click" ValidationGroup="validation_del" OnClientClick="return confirm('Are you sure you want to delete this record?');" title="Delete" />
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
