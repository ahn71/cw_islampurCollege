<%@ Page Title="" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="AttLeaveHome.aspx.cs" Inherits="DS.UI.Adviser.AttLeaveHome" %>
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
                    <a id="A1" runat="server" href="~/UI/Adviser/AdviserHome.aspx">
                        <i class="fa fa-dashboard"></i>
                        DashBoard
                    </a>
                </li>                
                <li class="active">Attendance And Leave</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
      <div class="row">       
        <div class="col-md-3">
            <a id="A3" runat="server" href="~/UI/Adviser/AdviserAttendance.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center"> Attendance</h5>
                    </div>                    
                </div>
            </a>
        </div>
         <div class="col-md-3">
            <a id="A4" runat="server" href="~/UI/Adviser/AdviserLeave.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Leave</h5>
                    </div>                    
                </div>
            </a>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
