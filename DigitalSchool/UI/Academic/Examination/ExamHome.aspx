<%@ Page Title="Examination Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ExamHome.aspx.cs" Inherits="DS.UI.Academics.Examination.ExamHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                   <%-- <a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li>
                    <%--<a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a>--%>
                    <a runat="server" id="aAcademicHome" >Academic Module</a>

                </li>
                <li class="active">Examination Module</li>
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
            <a runat="server" href="~/UI/Academic/Examination/ManagedSubject/SubjectManageHome.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/Subject Manage.ico" alt="submanagement" />
                    </span>
                    <span>Subject Management</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/AddExam.aspx">--%>
            <a runat="server" id="aExamType">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/Exam type.ico" alt="examtype" />
                    </span>
                    <span>Exam Type</span>

                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/QuestionPattern.aspx">--%>
            <a runat="server" id="aQuestionPattern">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/Qestion Pattern.ico" alt="qpattern" />
                    </span>
                    <span>Question Pattern</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Academic/Examination/MonthlyTest.aspx">
           
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/Qestion Pattern.ico" alt="qpattern" />
                    </span>
                    <span>Monthly Test</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/SubjectQuestionPattern.aspx">--%>
             <a runat="server" id="aSubjectQuestionPattern">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/subject wise Qestion Pattern.ico" alt="subwiseQpattern" />
                    </span>
                    <span>Sub. Wise Ques. Pattern</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/ExamInfo.aspx">--%>
            <a runat="server" id="aExamInfo">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/exam info.ico" alt="examinfo" />
                    </span>
                    <span>Exam Info</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/ExamRoutine.aspx">--%>
            <a runat="server" id="aExamRoutine">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/exam info.ico" alt="examinfo" />
                    </span>
                    <span>Exam Routine</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/ExamineeSelection.aspx">--%>
            <a runat="server" id="aExamineeSelection">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/exam info.ico" alt="examinfo" />
                    </span>
                    <span>Examinee Selection</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/Grading.aspx">--%>
            <a runat="server" id="aExamGrading">
             <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/grading.ico" alt="grading" />
                    </span>
                    <span>Grading</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/MarksEntryPanel.aspx">--%>
            <a runat="server" id="aExamMarksEntry">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/marks entry.ico" alt="marksentry" />
                    </span>
                    <span>Marks Entry</span>
                </div>
            </a>
        </div>
                <div class="col-md-3">
            <%--<a runat="server" href="~/UI/Academic/Examination/ResultPublish.aspx">--%>
            <a runat="server" id="aResultPublish">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/marks entry.ico" alt="marksentry" />
                    </span>
                    <span>Result Publishing Panel</span>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
