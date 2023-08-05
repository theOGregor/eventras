<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="pmt_finder.aspx.cs" Inherits="SchoolTours.Accounting.pmt_finder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <style>
        .DetailedResults td {
            padding: 2px 5px 2px 5px;
            margin: 0px;
            border: 1px solid black;
        }
    </style>
    <div class="container-fluid p-3" style="height: calc(100% - 185px);">
        <div class="card mb-0 bg-primary p-3 h-100">
            <div class="d-flex flex-column justify-content-center align-items-center col-md-10 m-auto align-self-stretch h-100">
                <div class="w-100 position-relative">
                    <div class="w-100">
                        <div class="form-row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:DropDownList ID="select_tour" runat="server" Class="custom-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:DropDownList ID="select_pmt_method" runat="server" Class="custom-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_pmt_amt" runat="server" Class="form-control number" placeholder="Amount" MaxLength="10" data-v-max="999999.0" data-m-dec="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="w-100">
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_start_date" runat="server" Type="date" placeholder="Start Date" Class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_end_date" runat="server" Type="date" placeholder="End Date" Class="form-control"></asp:TextBox>

                                     <asp:CompareValidator ID="CompareValidator1" ValidationGroup="validation_pmt" ForeColor="Red"
                                                    runat="server" ControlToValidate="input_start_date" ControlToCompare="input_end_date" Operator="LessThan" Type="Date" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>

                                </div>
                            </div>
                            <div class="col">
                                <div class="form-group">
                                    <asp:TextBox ID="input_memo" runat="server" placeholder="Memo" Class="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="w-100 text-right mb-3">
                        <a href="javascript:void(0);">
                            <asp:ImageButton ID="img_cancel" runat="server" ImageUrl="../Content/images/icon_cancel.png" OnClick="clrFields" Width="40" ToolTip="Cancel"/></a>
                        <a href="javascript:void(0);">
                            <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" OnClick="pmtSearch" Width="40" ToolTip="Save" ValidationGroup="validation_pmt"/></a>
                    </div>
                </div>

                <div class="w-100">
                    <div class="table-responsive overflow-auto bg-white border-rad" style="height: calc(100vh - 470px);">
                        <asp:GridView runat="server" ID="gv_pmt_finder"
                            CssClass="table table-borderless table-sm bg-white DetailedResults" HeaderStyle-CssClass="gridStyle" AutoGenerateColumns="false" ShowHeader="true" Style="border: 0px;" >
                            <HeaderStyle BackColor="#a9a9a9" Font-Italic="false" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="PMT-ID" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_pmt_id" runat="server" Text='<%# Bind("pmt_id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Event" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Event" runat="server" Text='<%# Bind("evt_nm") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Group" runat="server" Text='<%# Bind("group_nm") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rec Date" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_rec_date" runat="server" Text='<%# Bind("rec_date") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PMT Date" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_pmt_date" runat="server" Text='<%# Bind("pmt_date") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PMT Method" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_descr_method" runat="server" Text='<%# Bind("pmt_method_descr") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PMT Amt" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_pmt_amt" runat="server" Text='<%# Bind("pmt_amt") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Memo" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_memo" runat="server" Text='<%# Bind("memo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/autoNumeric-1.9.21.js"></script>
<script type="text/javascript">
    $(".number").autoNumeric();
</script>
</asp:Content>
