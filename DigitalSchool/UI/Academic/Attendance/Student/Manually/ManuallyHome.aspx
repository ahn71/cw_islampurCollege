<%@ Page Title="Student Attendance By Manually" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ManuallyHome.aspx.cs" Inherits="DS.UI.Academic.Attendance.Student.Manually.ManuallyHome" %>

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
                <li class="active">Student Attendance By Manually</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/Student/Manually/StudentAttendance.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Attendance Count</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/Student/Manually/AbsentDetails.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Attendance Details</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/Student/Manually/OffDaysSet.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Off Days Settings</h5>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/Student/Manually/AttendanceSheetGenerate.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Sheet Generator</h5>
                    </div>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
