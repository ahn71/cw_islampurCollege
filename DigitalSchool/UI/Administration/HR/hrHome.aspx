<%@ Page Title="HR Module" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="hrHome.aspx.cs" Inherits="DS.UI.Administration.HR.hrHome" %>
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li class="active">Human Resource Module</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <%--<div class="col-md-3"></div>--%>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/Employee/EmpHome.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/Employee.ico" alt="Employee" />
                    </span>
                    <span>Employee</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/Payroll/PayrollHome.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/Payrool.ico" alt="Payrool" />
                    </span>
                    <span>Payroll</span>
                </div>
            </a>
        </div>
          <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaHome.aspx">
              <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/Employee.ico" alt="Payrool" />
                    </span>
                    <span>Teacher Evaluation</span>
                </div>
            </a>
        </div>
        <div class="col-md-3"></div>
    </div>

   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
