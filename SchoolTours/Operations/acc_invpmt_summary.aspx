<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="acc_invpmt_summary.aspx.cs" Inherits="SchoolTours.Operations.acc_invpmt_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .DetailedResults td {
            padding: 2px 5px 2px 5px;
            margin: 0px;
            border: 1px solid black;
        }
    </style>
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
                                                        <asp:TemplateField HeaderText="Rep" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnk_Rep" runat="server" Text='<%# Bind("producer_nm") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Group" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk_group_nm" runat="server" Text='<%# Bind("group_nm") %>' CommandName="tour_id" CommandArgument='<%#Eval("tour_id")%>' style = "font-weight:bold" ToolTip="Redirect to Ops Center- Tour Detail page"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PAX" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnk_PAX" runat="server" Text='<%# Bind("pax_nr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PPA" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnk_PPA" runat="server" Text='<%# Bind("ppa_ind") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inv to Date" SortExpression="">
                                                            <ItemTemplate>
                                                                $
                                                <asp:Label ID="lnk_total_inv" runat="server" Text='<%# Bind("total_inv","{0:N2}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Next Inv Date" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnk_inv_date" runat="server" Text='<%# Bind("inv_date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Next Inv Amt" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnk_inv_descr" runat="server" Text='<%# Bind("inv_descr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Paid To Date" SortExpression="">
                                                            <ItemTemplate>
                                                                $
                                                <asp:Label ID="lnk_paid_to_date" runat="server" Text='<%# Bind("total_paid","{0:N2}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="past due" SortExpression="">
                                                            <ItemTemplate>
                                                                $
                                                <asp:Label ID="lnk_past_due" runat="server" Text='<%# Bind("total_past_due","{0:N2}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FIN" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnk_FIN" runat="server" Text='<%# Bind("fin_ind") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-12 text-right bot-fix">
                                            <asp:ImageButton ID="img_print_inv" class="float-right" runat="server" ImageUrl="../Content/images/icon_print.png" alt="" Width="40" OnClick="prnEvt" title="Print" />
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
