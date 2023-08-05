<%@ Page Title="Productivity Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rpt_sales_prod.aspx.cs" Inherits="SchoolTours.Sales.rpt_sales_prod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <div style="height: calc(100% - 185px);" class="py-3">
        <div class="card mb-0 bg-primary p-3 h-100">
            <div class="d-flex flex-column justify-content-center align-items-center col-md-5 m-auto align-self-stretch h-100">
                <div class="w-100">
                    <div class="form-group">
                        <asp:DropDownList ID="select_emp_id" runat="server" class="custom-select">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="w-100">
                    <div class="row">
                        <div class="col-md-5">
                            <div class="form-group mb-5">
                                <asp:TextBox ID="input_start_dt" runat="server" class="form-control" placeholder="Start Date" type="date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group mb-5">
                                <asp:TextBox ID="input_end_dt" runat="server" class="form-control" placeholder="Start Date" type="date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:ImageButton ID="img_save" runat="server" ImageUrl="../Content/images/icon_save.png" alt="" Width="36" OnClick="shwStats" ToolTip="Save"/>
                        </div>
                    </div>
                </div>
                <div class="w-100">
                    <div class="row">
                        <div class="col-md-6">
                            <ul class="w-100 list-unstyled">
                                <li class="text-white bg-light-blue p-3 mb-2">Sales Calls 
                                    <h5 class="mb-0 float-right d-inline-block"><asp:Label ID="div_sales_calls" runat="server" Text="" Class="badge badge-pill badge-light float-right" ></asp:Label></h5>
                                </li>
                                <li class="text-white bg-light-blue p-3 mb-2">Sales Email 
                                    <h5 class="mb-0 float-right d-inline-block"><asp:Label ID="div_sales_mail" runat="server" Text="" Class="badge badge-pill badge-light float-right" ></asp:Label></h5>
                                </li>
                                <li class="text-white bg-light-blue p-3 mb-2">Mass Emails
                                    <h5 class="mb-0 float-right d-inline-block"><asp:Label ID="div_mass_mail" runat="server" Text="" Class="badge badge-pill badge-light float-right" ></asp:Label></h5>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-6">
                            <ul class="w-100 list-unstyled">
                                <li class="text-white bg-light-blue p-3 mb-2">Completed Hotlist
                                    <h5 class="mb-0 float-right d-inline-block"><asp:Label ID="div_comp_hotlist" runat="server" Text="" Class="badge badge-pill badge-light float-right" ></asp:Label></h5>
                                </li>
                                <li class="text-white bg-light-blue p-3 mb-2">Queued Hotlist
                                    <h5 class="mb-0 float-right d-inline-block"><asp:Label ID="div_future_hotlist" runat="server" Text="" Class="badge badge-pill badge-light float-right" ></asp:Label></h5>
                                </li>
                                <li class="text-white bg-light-blue p-3 mb-2">Past Due Hotlist
                                    <h5 class="mb-0 float-right d-inline-block"><asp:Label ID="div_past_hotlist" runat="server" Text="" Class="badge badge-pill badge-light float-right" ></asp:Label></h5>
                                </li>
                                <li class="text-white bg-light-blue p-3 mb-2">Contract
                                    <h5 class="mb-0 float-right d-inline-block"><asp:Label ID="div_contracts" runat="server" Text="" Class="badge badge-pill badge-light float-right"></asp:Label></h5>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
