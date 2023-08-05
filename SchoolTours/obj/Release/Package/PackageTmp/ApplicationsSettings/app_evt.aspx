<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="app_evt.aspx.cs" Inherits="SchoolTours.ApplicationsSettings.app_evt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="main">
        <asp:HiddenField id="evt_id" runat="server"/>
        <asp:HiddenField id="div_id" runat="server"/>
        <asp:HiddenField id="venue_id" runat="server"/>

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
                                <h5 class="text-active">Event Maintenance</h5>
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
                            <div class="col-md-12">
                                <div class="form-row">
                                    <div class="col-md-6">
                                        <div class="form-group mb-2">
                                            <asp:DropDownList ID="select_div" runat="server" AutoPostBack="true" class="custom-select" OnSelectedIndexChanged="select_div_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group mb-2">
                                            <%--<asp:TextBox ID="select_evt" runat="server" class="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>--%>
                                            <asp:ListBox ID="select_evt" runat="server" class="list-unstyled custom-list" Style="border: 0px; width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="dtlEvt"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_evt_nm" runat="server" class="form-control" placeholder="Event Name" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation_evt" ControlToValidate="input_evt_nm" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group mb-2">
                                            <asp:DropDownList ID="select_venue" runat="server" AutoPostBack="true" class="custom-select" OnSelectedIndexChanged="select_venue_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validation_evt" ControlToValidate="select_venue" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="input_evt_descr" runat="server" class="form-control" TextMode="MultiLine" Rows="3" placeholder="Event Descr"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validation_evt" ControlToValidate="input_evt_descr" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                <asp:TextBox ID="input_evt_memo" runat="server" class="form-control" TextMode="MultiLine" Rows="3" placeholder="Event Memo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validation_evt" ControlToValidate="input_evt_memo" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />

                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group mb-2">
                                                    <asp:TextBox ID="input_start_date" runat="server" type="date" class="form-control" placeholder="Start Date" value="MM/dd/yyyy"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="validation_evt" ControlToValidate="input_start_date" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                                </div>
                                                <div class="form-group mb-2">
                                                    <asp:TextBox ID="input_end_date" runat="server" type="date" class="form-control" placeholder="End Date" value="MM/dd/yyyy"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="validation_evt" ControlToValidate="input_end_date" ForeColor="Red" Display="Dynamic" ErrorMessage="*" Class="validation-class" />
                                                </div>
                                                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="validation_evt" ForeColor="Red"
                                                    runat="server" ControlToValidate="input_start_date" ControlToCompare="input_end_date" Operator="LessThan" Type="Date" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                                            </div>

                                        </div>
                                        <div class="w-100 text-right mt-3">
                                            <asp:ImageButton ID="img_copy" runat="server" ImageUrl="../Content/images/icon_copy.png" alt="" Width="36" OnClick="cpyEvts" title="Copy" OnClientClick="return confirm('Are you sure you want to copy all events ?');" />
                                            <asp:ImageButton ID="img_delete" runat="server" ImageUrl="../Content/images/icon_delete.png" alt="" Width="36" OnClick="delEvt" title="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            <asp:ImageButton ID="img_new" runat="server" ImageUrl="../Content/images/icon_add.png" alt="" Width="36" OnClick="newEvt" title="Add" />
                                            <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="36" OnClick="setEvt" ValidationGroup="validation_evt" title="Save" />
                                        </div>
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
