<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StaffFacultyHome.aspx.cs" Inherits="DS.UI.Reports.StafforFaculty.StaffFacultyHome" %>
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
                
                <li class="active">Employees Info</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/StafforFaculty/ProfileStafforFaculty.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                            <span>
                                <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/profile.ico"/>
                            </span>
                            <span>Profile</span>
                        </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/StafforFaculty/EmployeeList.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                            <span>
                                <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/stuff list.ico"/>
                            </span>
                            <span>All List</span>
                        </div>
            </a>
        </div>
         <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/StafforFaculty/DepartmentwiseReport.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                            <span>
                                <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/department.ico"/>
                            </span>
                            <span>Department Wise List</span>
                        </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/StafforFaculty/DesignationwiseReport.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                            <span>
                                <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/employee.ico"/>
                            </span>
                            <span>Designation Wise List</span>
                        </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/StafforFaculty/BloodGroup.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                            <span>
                                <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/blood group.ico"/>
                            </span>
                            <span>Blood Group Wise List</span>
                        </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
