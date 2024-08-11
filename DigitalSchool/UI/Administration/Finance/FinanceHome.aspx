<%@ Page Title="Finance Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FinanceHome.aspx.cs" Inherits="DS.UI.Administration.Finance.FinanceHome" %>
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
                <li class="active">Finance Module</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <%--<h2 class="text-center" style="color:red">Finance Modeule are not Register for your Account</h2>--%>
    <div class="row">       
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/Fee Management.ico" alt="FeeManagement" />
                    </span>
                    <span>Fee Management</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FineManaged/StudentFineCollection.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/fine menagment.ico" alt="finemenagment" />
                    </span>
                    <span>Fine Management</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
             <a id="A1" runat="server" href="~/UI/Reports/Finance/FinanceReportHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/fine menagment.ico" alt="finemenagment" />
                    </span>
                    <span>Finace Reports</span>
                </div>
            </a>
        </div> 
        <div class="col-md-3">
             <a id="A2" runat="server" href="~/UI/Administration/Finance/Accounts/AccountsHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/fine menagment.ico" alt="finemenagment" />
                    </span>
                    <span>Accounts</span>
                </div>
            </a>
        </div>            
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
