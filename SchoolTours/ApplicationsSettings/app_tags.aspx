<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="app_tags.aspx.cs" Inherits="SchoolTours.ApplicationsSettings.app_tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="tag_id" runat="server" />
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
                                <h5 class="text-active">Tag Maintenance</h5>
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
                        <div class="row align-items-center h-100 justify-content-center">
                            <div class="col-md-5">
                                <div class="form-row">
                                    <div class="col-md-12">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_tag_search" runat="server" class="form-control" placeholder="Search" OnTextChanged="input_tag_search_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:ListBox ID="select_tag" runat="server" class="list-unstyled custom-list" SelectionMode="Multiple" Style="background-color: #00a9ee; border: 0px; width: 100%;" OnSelectedIndexChanged="dtlTag" AutoPostBack="true"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_tag_descr" runat="server" class="form-control" placeholder="Description"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-right">
                                        <asp:ImageButton ID="img_merge" runat="server" ImageUrl="../Content/images/icon_merge.png" alt="" Width="36" OnClick="mrgTag" OnClientClick="return confirm('Are you sure you want to merge these record?');" title="Merge" />
                                        <asp:ImageButton ID="img_delete" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="36" OnClick="delTag" OnClientClick="return confirm('Are you sure you want to delete this record?');" title="Delete" />
                                        <asp:ImageButton ID="img_add" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="36" OnClick="newTag" title="Add" />
                                        <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="36" OnClick="setTag" title="Save" />
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
