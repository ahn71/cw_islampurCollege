<%@ Page Title="Employee Managed" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EmpHome.aspx.cs" Inherits="DS.UI.Administration.HR.Employee.EmpHome" %>
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>
                <li class="active">Employee Management</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/Employee/AddDepartment.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/add document.ico" alt="adddepartment" />
                    </span>
                    <span>Add Department</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/Employee/AddDesignation.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/add designation.ico" alt="adddesignation" />
                    </span>
                    <span>Add Designation</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/Employee/EmpRegForm.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/registration form.ico" alt="registrationform" />
                    </span>
                    <span>Employee Reg. Form</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/Employee/EmpDetails.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/employee detail.ico" alt="employeedetail" />
                    </span>
                    <span>Employee Reg. Details</span>
                </div>
            </a>
        </div>
          <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/HR/Employee/EmpActivation.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/HR/employee detail.ico" alt="employeedetail" />
                    </span>
                    <span>Employee Activation</span>
                </div>
            </a>
        </div>
        <div class="col-md-3"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
