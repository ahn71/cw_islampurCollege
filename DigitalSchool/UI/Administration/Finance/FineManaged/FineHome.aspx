<%@ Page Title="Fine Managed" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FineHome.aspx.cs" Inherits="DS.UI.Administration.Finance.FineManaged.FineHome" %>

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
                <li class="active">Fine Management</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Administration/Finance/FineManaged/StudentFineCollection.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Student Fine Collection</h5>
                    </div>
                </div>
            </a>
        </div> 
        <div class="col-md-4"></div>       
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
