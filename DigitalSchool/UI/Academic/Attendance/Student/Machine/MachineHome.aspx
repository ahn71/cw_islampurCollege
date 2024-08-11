<%@ Page Title="Student Attendance By Machine" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="MachineHome.aspx.cs" Inherits="DS.UI.Academic.Attendance.Student.Machine.MachineHome" %>

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
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/Student/StdAttnHome.aspx">Student Attendance</a></li>
                <li class="active">Student Attendance By Machine</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">       
        <div class="col-md-4">            
            <a runat="server" href="~/UI/Academic/Attendance/Student/Machine/import_data.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Import Data From Machine</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/Student/Machine/Machine_AttendanceDetails.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Attendance Details</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">            
            <a runat="server" href="~/UI/Academic/Attendance/Student/Machine/manually_certain_attendance_count.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Attendance Input By Manually</h5>
                    </div>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
