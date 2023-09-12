<%@ Page Title="Attendance Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AttendanceHome.aspx.cs" Inherits="DS.UI.Academics.Attendance.AttendanceHome" %>

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
                <li class="active">Attendance Module</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/Student/StdAttnHome.aspx">
                 <div class="mini-stat clearfix">
                            <span class="mini-stat-icon sub_mini_orange">
                                <img width="45" src="../../../Images/moduleicon/StudentAttendance.ico"  alt="students" />
                            </span>
                            <div class="mini-stat-info">
                                <span>Student Attendance</span>
                               Machine & Manually Attendance,Reports
                                
                            </div>
                        </div>
            </a>
        </div>        
        <div class="col-md-4">
            <a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/StafforFacultyHome.aspx">
                <div class="mini-stat clearfix">
                            <span class="mini-stat-icon sub_mini_orange">
                                <img width="45" src="../../../Images/moduleicon/StaffAttendance.ico"  alt="Staff" />
                            </span>
                            <div class="mini-stat-info">
                                <span>Teacher and Staff Attendance</span>
                               Machine & Manually Attendance,Reports
                                
                            </div>
                        </div>
            </a>
        </div> 
         <div class="col-md-2"></div>
                <%--<div class="col-md-4">
            <a id="A1" runat="server" href="~/UI/Academic/Attendance/Student/Manually/OffDaysSet.aspx">
                 <div class="mini-stat clearfix">
                            <span class="mini-stat-icon comtar">
                                <img width="45" src="../../../Images/moduleicon/OffDay.ico"  alt="OffDay" />
                            </span>
                         <div class="mini-stat-info">
                                <span>OFF Day Settings</span>  
                             </div>                             
                                
                           
                        </div>
            </a>
        </div>             --%>    
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="mini-stat clearfix">
                <img style="margin: 0px auto; display: block;max-width:100%;height:auto;" src="../../../Images/dashboard_icon/AttendanceHome.jpg" alt="AttendanceHome"/>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
