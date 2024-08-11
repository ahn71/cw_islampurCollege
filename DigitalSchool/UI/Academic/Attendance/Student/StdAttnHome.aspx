<%@ Page Title="Student Attendance" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StdAttnHome.aspx.cs" Inherits="DS.UI.Academic.Attendance.Student.StdAttnHome" %>

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
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li> 
                <li><a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li> 
                <li class="active">Student Attendance</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
       
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/Student/Machine/import_data.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                <span>
                    <img width="45" src="../../../../Images/moduleicon/StudentAttendane/attendance device.ico" alt="attendancedevice"/>
                </span>              
                    <span>Attendance Data Import From Machine</span>                
            </div>
            </a>
        </div>      
           <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/Student/Manually/StudentAttendance.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                <span">
                     <img width="45" src="../../../../Images/moduleicon/StudentAttendane/attendance manual.ico" alt="attendancemanual"/>
                </span>                
                    <span>Attendance by Manual Sheet</span>               
            </div>
            </a>
        </div>

        <div class="col-md-4">
            <a runat="server" href="~/UI/Reports/Attendance/MonthWiseAttendanceSheet.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/StudentAttendane/attendance detail.ico" alt="attendancedetail" />
                    </span>
                    <span>Attendance Report</span>
                </div>
            </a>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
