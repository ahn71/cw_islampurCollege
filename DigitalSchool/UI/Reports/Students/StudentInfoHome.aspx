<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentInfoHome.aspx.cs" Inherits="DS.UI.Reports.Students.StudentInfoHome" %>
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
                
                <li class="active">Student Information</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Reports/Students/IndivisualStudentList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/student profile.ico" />
                    </span>
                    <span>Individual Student profile</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/StudentList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/student list.ico" />
                    </span>
                    <span>Student List</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/GenderwiseStdList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/gender list.ico" />
                    </span>
                    <span>Gender Wise Student List</span>
                </div>
            </a>
        </div>
       <%-- <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/StudentContactList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/contact  list.ico" />
                    </span>
                    <span>Student Contact List</span>
                </div>
            </a>
        </div>--%>
        <%-- <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/CourseWiseStudent.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Course Wise Student </h5>
                    </div>                    
                </div>
            </a>
        </div>--%>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/GuardianInformation.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/gender list.ico" />
                    </span>
                    <span>Gurdian Information</span>
                </div>
            </a>
        </div>
        <%--<div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/GuardianContactList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/Guardian contact list.ico" />
                    </span>
                    <span>Gurdian Contact List</span>
                </div>
            </a>
        </div>--%>

        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/ParentsInformationList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/Parents info.ico" />
                    </span>
                    <span>Parents Information List</span>
                </div>
            </a>
        </div>

        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/AllContactList.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/Parents info.ico" />
                    </span>
                    <span>All Contact List</span>
                </div>
            </a>
        </div>

        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/AdmitCardGenerator.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Admit Id Card</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/StudentBusInfo.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Bus Information</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Reports/Students/StudentBusInformation.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_report">
                    <span>
                        <img width="45" alt="classroutine" src="../../../../Images/moduleicon/Reports/admid card.ico" />
                    </span>
                    <span>Student Bus Information</span>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
