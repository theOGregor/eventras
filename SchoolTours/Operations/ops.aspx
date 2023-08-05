<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ops.aspx.cs" Inherits="SchoolTours.Operations.ops" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" />--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <section class="main">
        <asp:HiddenField ID="inv_type_ind" runat="server" />
        <div class="row mx-0 align-items-stretch h-100 pt-4">
            <div class="col-lg-3 col-md-4">
                <div class="card bg-light-blue h-100 mb-0">
                    <div class="card-body p-2">
                        <div class="w-100 w-100 h-100 position-relative">
                            <div class="form-group mb-1">
                                <asp:Label ID="lbl_base" runat="server"></asp:Label>
                                <asp:DropDownList ID="select_div" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;" OnSelectedIndexChanged="lstEvts" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-8">
                                    <label for="" class="sr-only">Operators</label>
                                    <asp:DropDownList ID="select_emp" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;" OnSelectedIndexChanged="lstEvts" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="" class="sr-only">Year</label>
                                    <asp:DropDownList ID="select_year" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;" OnSelectedIndexChanged="lstEvts" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group mb-1">
                                <asp:DropDownList ID="select_evt" runat="server" class="custom-select" Style="height: 35px !important; line-height: 25px !important;" OnSelectedIndexChanged="srchEvts" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group mb-1">
                                <asp:ListBox ID="select_tour" runat="server" class="form-control form-control-sm" Style="border: 0px; width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="getTourType" Rows="12"></asp:ListBox>
                                <div id="tooltip_container"></div>
                            </div>
                            <div class="w-100 bot-fix">
                                <ul class="list-unstyled d-flex bot-fix justify-content-between mb-0">
                                    <li>
                                        <asp:ImageButton ID="img_details" runat="server" ImageUrl="../Content/images/icon_details.png" data-placement="bottom" title="details" Width="40" OnClick="img_details_Click" /></li>
                                    <li>
                                        <asp:ImageButton ID="img_pax" runat="server" ImageUrl="../Content/images/icon_pax.png" data-placement="bottom" title="pax" Width="40" OnClick="img_pax_Click" /></li>
                                    <li>
                                        <asp:ImageButton ID="img_billing" runat="server" ImageUrl="../Content/images/icon_inv.png" data-placement="bottom" title="billing" Width="40" OnClick="img_billing_Click" /></li>
                                    <li>
                                        <asp:ImageButton ID="img_contracts" runat="server" ImageUrl="../Content/images/icon_threshold.png" data-placement="bottom" title="Deadlines" Width="40" OnClick="img_contracts_Click" /></li>
                                    <li>
                                        <asp:ImageButton ID="img_cost" runat="server" ImageUrl="../Content/images/icon_costs.png" data-placement="bottom" title="Costs" Width="40" OnClick="img_cost_Click" /></li>
                                </ul>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-9 col-md-8">
                <div class="card bg-primary h-100 mb-0 overflow-hidden">
                    <div class="card-header bg-blue">
                        <h4 class="py-3"></h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </section>
</asp:Content>
