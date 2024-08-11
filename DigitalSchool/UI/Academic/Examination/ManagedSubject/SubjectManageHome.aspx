<%@ Page Title="Subject Managed" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SubjectManageHome.aspx.cs" Inherits="DS.UI.Academic.Examination.ManagedSubject.SubjectManageHome" %>

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
                <li><a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>
                <li class="active">Subject Management</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Examination/ManagedSubject/NewSubject.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/SubjectManagement/new subjject.ico" alt="newsubject" />
                    </span>
                    <span>New Subject</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Examination/ManagedSubject/AddCourseWithSubject.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/SubjectManagement/Course With Subject.ico" alt="subcourse" />
                    </span>
                    <span>Subject Wise Course</span>


                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Examination/ManagedSubject/ClassSubjectSetup.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/SubjectManagement/Subject setup.ico" alt="newsubject" />
                    </span>
                    <span>Class Wise Subject Setup</span>


                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Academic/Examination/ManagedSubject/StudentGroupSubSetup.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/SubjectManagement/optional Subject setup.ico" alt="newsubject" />
                    </span>
                    <span>Student Group Subject Setup</span>


                </div>
            </a>
        </div>      
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
