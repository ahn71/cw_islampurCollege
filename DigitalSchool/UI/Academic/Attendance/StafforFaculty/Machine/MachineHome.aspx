<%@ Page Title="Machine Attendance" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="MachineHome.aspx.cs" Inherits="DS.UI.Academics.Attendance.StafforFaculty.Machine.MachineHome" %>
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
                <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/StafforFacultyHome.aspx">Teacher and Staff  Attendance</a></li>
                <li class="active">Attendance By Machine</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">       
        <div class="col-md-4">            
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Machine/emp_import_data.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Import Data From Machine</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Machine/Machine_Staff_Atten_Details.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Attendance Details</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">            
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Machine/manually_certain_emp_attendance_count.aspx">
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
