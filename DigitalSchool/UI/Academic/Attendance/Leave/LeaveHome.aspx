<%@ Page Title="Leave Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="LeaveHome.aspx.cs" Inherits="DS.UI.Academics.Attendance.Leave.LeaveHome" %>

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
                 <li><a id="A1" runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a id="A2" runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Academic/Attendance/StafforFaculty/StafforFacultyHome.aspx">Teacher and Staff Attendance</a></li>
                <li class="active">Leave Management</li>          
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">       
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Attendance/Leave/application.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Application For Leave</h5>
                    </div>                    
                </div>
            </a>
        </div>
        <div class="col-md-3">
             <a runat="server" href="~/UI/Academic/Attendance/Leave/for_approve_leave_list.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Leave Approved</h5>
                    </div>                    
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Attendance/Leave/leave_configuration.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Leave Configuration</h5>
                    </div>                    
                </div>
            </a>
        </div>                
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
