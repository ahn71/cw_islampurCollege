<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DS.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="row">
      
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li class="active">
                    <%--<a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard" >
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li class="active"></li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>  

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    
    
</asp:Content>
