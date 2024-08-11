<%@ Page Title="Guide Teacher Home" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentGuideHome.aspx.cs" Inherits="DS.UI.Academic.StudentGuide.StudentGuideHome" %>
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
                <li><a id="A2" runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li class="active">Guide Teacher Module</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a id="A3" runat="server" href="~/UI/Academic/StudentGuide/AssignGuideTeacher.aspx">
                <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                    <span>
                        <img width="45" src="../../../Images/moduleicon/OldStudentEntry.ico" alt="oldstudent" />
                    </span>
                    <span>Assign Guide Teacher</span>

                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A4" runat="server" href="~/UI/Academic/StudentGuide/ListofGuideTeacher.aspx">
               <div class="mini-stat clearfix btn3d btn custom_menu_btn_acedemic">
                            <span>
                                <img width="45" src="../../../Images/moduleicon/Admission.ico" alt="stdAdmission" />
                            </span>                           
                                <span>Guide Teacher Wise Student List</span>                          
                                
                          
                        </div>
            </a>
        </div>           
        </div>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
