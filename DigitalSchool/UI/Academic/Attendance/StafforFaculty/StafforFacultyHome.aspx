<%@ Page Title="Staff or Faculty Attendance" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StafforFacultyHome.aspx.cs" Inherits="DS.UI.Academics.Attendance.StafforFaculty.StafforFaculty" %>

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
                <li>Teacher and Staff Attendance</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Machine/emp_import_data.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                <span>
                    <img width="45" src="../../../../Images/moduleicon/stuffAttendance/attendance device.ico" alt="attendancedevice"/>
                </span>              
                    <span>Attendance Data Import From Machine</span>                
            </div>
            </a>
        </div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/FacultyAttendance.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                <span>
                    <img width="45" src="../../../../Images/moduleicon/stuffAttendance/attendance manual.ico" alt="attendancemanual"/>
                </span>              
                    <span>Attendance by Manual Sheet</span>                
            </div>
            </a>
        </div>
          <div class="col-md-4">
            <a id="A2" runat="server" href="~/UI/Reports/Attendance/AttendanceSlider/AttendanceStatusBoard.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                <span>
                    <img width="45" src="../../../../Images/moduleicon/stuffAttendance/attendance manual.ico" alt="attendancemanual"/>
                </span>              
                    <span>Attendance Status Dashboard</span>                
            </div>
            </a>
        </div>
       
<%--              <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/FacultyStaffAbsentDetails.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                <span>
                    <img width="45" src="../../../../Images/moduleicon/stuffAttendance/Details.ico" alt="Details"/>
                </span>              
                    <span>Attendance Details</span>                
            </div>
            </a>
        </div>--%>
         <div class="col-md-4">
            <a id="A1" runat="server" href="~/UI/Academic/Attendance/Leave/LeaveHome.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                <span>
                    <img width="45" src="../../../../Images/moduleicon/stuffAttendance/Leave.ico" alt="Leave"/>
                </span>              
                    <span>Leave Management</span>                
            </div>
            </a>
        </div>  
    </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
