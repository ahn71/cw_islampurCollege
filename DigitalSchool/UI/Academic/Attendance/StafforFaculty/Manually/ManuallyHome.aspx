<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ManuallyHome.aspx.cs" Inherits="DS.UI.Academics.Attendance.StafforFaculty.Manually.ManuallyHome" %>

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
                <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/StafforFacultyHome.aspx">Staff or Faculty Attendance</a></li>
                <li class="active">Attendance By Manually</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
        <div class="row">       
        <div class="col-md-4">            
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/FacultyAttendance.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Attendance Count</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/FacultyStaffAbsentDetails.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Attendance Details</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">            
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/FacultyAttendanceSheetGenarate.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Sheet Generator</h5>
                    </div>
                </div>
            </a>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
