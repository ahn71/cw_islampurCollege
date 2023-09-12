<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EvaHome.aspx.cs" Inherits="DS.UI.Administration.HR.TeacherEvaluation.home" %>
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
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a id="A2" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>
                <li class="active">Teacher Evaluation</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a id="A4" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaCategory.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/add document.ico" alt="adddepartment" />
                    </span>
                    <span>Add Category</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A5" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaSubCategory.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/add document.ico" alt="adddesignation" />
                    </span>
                    <span>Add Sub Category</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A6" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaNumberPattern.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/add document.ico" alt="registrationform" />
                    </span>
                    <span>Set Number Pattern</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A7" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaSession.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/employee detail.ico" alt="employeedetail" />
                    </span>
                    <span>Set Evaluation Session</span>
                </div>
            </a>
        </div>
        <div class="col-md-3"></div>
    </div>
   
         <div class="row">
        <%--<div class="col-md-3"></div>--%>
        <div class="col-md-3">
            <a id="A8" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaNumberEntry.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/Employee.ico" alt="Employee" />
                    </span>
                    <span>Evaluation Number Entry</span>
                </div>
            </a>
        </div>     
                     <div class="col-md-3">
            <a id="A9" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaReport.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/Employee.ico" alt="Employee" />
                    </span>
                    <span>Evaluation Report</span>
                </div>
            </a>
        </div>     
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
