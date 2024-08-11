<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AttendanceHome.aspx.cs" Inherits="DS.UI.Reports.Attendance.AttendanceHome" %>
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
                <li><a runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>
                
                <li class="active">Attendance</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Attendance/MonthWiseAttendanceSheet.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/student attendence.ico" />
                    </span>
                    <span>Student Attendance</span>
                </div>       
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Attendance/MonthWiseAttendanceSheetSummary.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/stuff attendence.ico" />
                    </span>
                    <span>Teacher/Staff Attendance</span>
                </div>  
            </a>
        </div>
         <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Reports/Attendance/LeaveReport.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/stuff attendence.ico" />
                    </span>
                    <span>Teacher/Staff Leave Report</span>
                </div>  
            </a>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
