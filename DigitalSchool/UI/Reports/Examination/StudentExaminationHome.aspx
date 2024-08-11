<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentExaminationHome.aspx.cs" Inherits="DS.UI.Reports.Examination.StudentExaminationHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <a id="A6" runat="server" href="~/UI/StudentManage/StudentManage.aspx">
                        <i class="fa fa-dashboard"></i>
                        Student Manage
                    </a>
                </li> 
                   <li class="active">Examination</li>          
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Reports/Examination/ExamReports.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Examination</h5>
                    </div>                    
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A2" runat="server" href="~/UI/Reports/Examination/ExamOverView.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Exam Overview</h5>
                    </div>                    
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A3" runat="server" href="~/UI/Reports/Examination/AcademicTranscript.aspx">
                <div class="panel">
                    <div class="panel-body">
                        <h5 class="text-center">Academic Transcript</h5>
                    </div>                    
                </div>
            </a>
        </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
