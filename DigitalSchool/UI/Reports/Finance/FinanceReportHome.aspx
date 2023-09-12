<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FinanceReportHome.aspx.cs" Inherits="DS.UI.Reports.Finance.FinanceReportHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
         <div class="row">
             <div class="col-md-12">
                 <!--breadcrumbs start -->
                 <ul class="breadcrumb">
                     <li>
                         <a id="A1" runat="server" href="~/Dashboard.aspx">
                             <i class="fa fa-dashboard"></i>
                             Dashboard
                         </a>
                     </li>
                     <li><a id="A2" runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>

                     <li class="active">Finance Reports</li>
                 </ul>
                 <!--breadcrumbs end -->
             </div>
         </div>
    <div class="row">
        <div class="col-md-3">
            <a id="A5" runat="server" href="~/UI/Reports/Finance/FeeCollectionDetailsReport.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/profile.ico" />
                    </span>
                    <span>Fee Collection Report</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A4" runat="server" href="~/UI/Reports/Finance/FineReports.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/profile.ico" />
                    </span>
                    <span>Fine Report</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A3" runat="server" href="~/UI/Reports/Finance/DiscountReport.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/profile.ico" />
                    </span>
                    <span>Discount Report</span>
                </div>
            </a>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
