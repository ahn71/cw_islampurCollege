<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AccountsHome.aspx.cs" Inherits="DS.UI.Administration.Finance.Accounts.AccountsHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li class="active">Accounts Management</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/Accounts/AddTitle.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/admission fee.ico" alt="admissionfee" />
                    </span>
                    <span>Account Title</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/Accounts/Income.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/admission fee.ico" alt="admissionfee" />
                    </span>
                    <span>Income</span>
                </div>
            </a>      
        </div>   
        <div class="col-md-3"> 
            <a runat="server" href="~/UI/Administration/Finance/Accounts/Expenses.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/admission fee.ico" alt="admissionfee" />
                    </span>
                    <span>Expenses</span>
                </div>
            </a>           
        </div> 
        <div class="col-md-3"> 
            <a runat="server" href="~/UI/Administration/Finance/Accounts/AccountSetting.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/admission fee.ico" alt="admissionfee" />
                    </span>
                    <span>Account Setting</span>
                </div>
            </a>           
        </div>             
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/Accounts/AccountStatement.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/admission fee.ico" alt="admissionfee" />
                    </span>
                    <span>Account Statement</span>
                </div>
            </a>
        </div>  
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
