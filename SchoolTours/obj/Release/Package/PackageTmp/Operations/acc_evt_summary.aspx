<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="acc_evt_summary.aspx.cs" Inherits="SchoolTours.Operations.acc_evt_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .DetailedResults td {
            padding: 2px 5px 2px 5px;
            margin: 0px;
            border: 1px solid black;
        }
    </style>
    <div class="container-fluid">
        <section class="main pt-4">
            <div class="row mx-0 align-items-stretch h-100">
                <div class="col-lg-12 col-md-12">
                    <div class="card bg-primary h-100 mb-0 overflow-hidden">
                        <div class="card-body">
                            <div class="w-100 h-100 position-relative">
                                <div class="row h-100">
                                    <div class="col-md-12">
                                        <div class="form-row h-100 position-relative align-content-start">
                                            <div class="col-3">
                                                <div class="form-group mb-2">

                                                    <asp:DropDownList ID="select_div" runat="server" class="custom-select custom-select-sm" AutoPostBack="true" OnSelectedIndexChanged="lstEvts"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group mb-2">
                                                    <asp:DropDownList ID="select_year" runat="server" class="custom-select custom-select-sm" AutoPostBack="true" OnSelectedIndexChanged="lstEvts"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="form-group mb-2">
                                                    <asp:DropDownList ID="select_evt" runat="server" class="custom-select custom-select-sm" AutoPostBack="true" OnSelectedIndexChanged="dtlEvt"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-12  py-2 mb-2 overflow-hidden">
                                                <div class="table-responsive overflow-auto bg-white border-rad" style="height: calc(100vh - 470px);">
                                                    <asp:GridView runat="server" ID="gv_summary" DataKeyNames="customer_id" OnRowCommand="gv_summary_RowCommand"
                                                        CssClass="table table-borderless table-sm bg-white DetailedResults" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="true" Style="border: 0px;">
                                                        <HeaderStyle BackColor="#a9a9a9" Font-Italic="false" ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="CMG-ID" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_CMG_ID" runat="server" Text='<%# Bind("customer_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contract" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_Contract" runat="server" Text='<%# Bind("contract_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rep" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_Rep" runat="server" Text='<%# Bind("producer_nm") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Group" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_Group" runat="server" Text='<%# Bind("group_nm") %>' CommandName="tour_id" CommandArgument='<%#Eval("tour_id")%>' style = "font-weight:bold" ToolTip="Redirect to Ops Center- Tour Detail page"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_State" runat="server" Text='<%# Bind("state_abbr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leader" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_Leader" runat="server" Text='<%# Bind("leader_nm") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="POB" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_POB" runat="server" Text='<%# Bind("pob_nr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Free" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_Free" runat="server" Text='<%# Bind("free_trip_nr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pax" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_Pax" runat="server" Text='<%# Bind("pax_nr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Depart" SortExpression="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnk_Depart" runat="server" Text='<%# Bind("start_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PKG" SortExpression="">
                                                                <ItemTemplate>
                                                                    $
                                                <asp:Label ID="lnk_PKG" runat="server" Text='<%# Bind("cost_amt" ,"{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Price" SortExpression="">
                                                                <ItemTemplate>
                                                                    $
                                                <asp:Label ID="lnk_Total_Price" runat="server" Text='<%# Bind("total_costs","{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paid" SortExpression="">
                                                                <ItemTemplate>
                                                                    $
                                                <asp:Label ID="lnk_Paid" runat="server" Text='<%# Bind("total_paid","{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-12 text-right bot-fix">
                                                <asp:ImageButton ID="img_print_inv" class="float-right" runat="server" ImageUrl="../Content/images/icon_print.png" alt="" Width="30" OnClick="prnEvt" title="Print" />

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

    </div>
    <div style="display: none;">
        <div style="height: calc(100% - 185px);" class="py-3">
            <div class="card mb-0 my-2 bg-primary p-3 h-100">
                <div class="row align-items-center">
                    <div class="d-flex flex-column col-md-12 h-100">
                        <div class="row">
                        </div>

                        <div class="row align-items-center" style="padding-top: 1%;">
                            <div class="d-flex flex-column col-md-12 h-100">
                                <div class="table-responsive">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
