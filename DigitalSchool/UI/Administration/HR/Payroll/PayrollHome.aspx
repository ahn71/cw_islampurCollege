<%@ Page Title="Payroll Managed" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="PayrollHome.aspx.cs" Inherits="DS.UI.Administration.HR.Payroll.PayrollHome" %>

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
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>
                <li class="active">Payroll Management</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">        
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/Payroll/SalarySetDetails.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/salary set.ico" alt="salaryset" />
                    </span>
                    <span>Salary Set Details</span>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Administration/HR/Payroll/SalaryAllowanceType.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/Allowance.ico" alt="Allowance" />
                    </span>
                    <span>Salary Allowance Type</span>
                </div>
                </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Administration/HR/Payroll/SalaryDetailsInfo.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/view.ico" alt="view" />
                    </span>
                    <span>View Salary Details Info</span>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
