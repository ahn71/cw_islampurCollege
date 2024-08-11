<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DS.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="control-panel">    
    
        <div id="divMainContent" style="width: 100%; margin: 0px auto; text-align: center; height: auto; padding-top: 20px;">
            <a class="boxMenu" href="/Admin/Register.aspx">
                <img src="/Images/home/user-icon.png">
                <p>Member</p>
             </a>

            <a class="boxMenu" href="#">
                <img src="/Images/home/menu-icon.png">
                <p>Menu</p>
             </a>

            <a class="boxMenu" href="/Admin/AddDistrict.aspx">
                <img src="/Images/home/district-icon.png">
                <p>District</p>
             </a>

            <a class="boxMenu" href="/Admin/AddThana.aspx">
                <img src="/Images/home/thana-icon.png">
                <p>Thana/Upazila</p>
             </a>


            <a class="boxMenu" href="/Admin/AddClass.aspx">
                <img src="/Images/home/class.jpg">
                <p>Class</p>
             </a>

            <a class="boxMenu" href="/Admin/AddSection.aspx">
                <img src="/Images/home/section.png">
                <p>Section</p>
             </a>

            <a class="boxMenu" href="/Admin/AddBoard.aspx">
                <img src="/Images/home/grants-icon2.png">
                <p>Board</p>
             </a>



            
            <a class="boxMenu" href="/Admin/AddDepartment.aspx">
                <img src="/Images/home/department.jpg">
                <p>Department</p>
             </a>

            <a class="boxMenu" href="/Admin/AddDesignation.aspx">
                <img src="/Images/home/thana-icon.png">
                <p>Designation</p>
             </a>

            <a class="boxMenu" href="/Admin/AddFeesType.aspx">
                <img src="/Images/home/fees.jpg">
                <p>Fees Type</p>
             </a>

            <a class="boxMenu" href="/Admin/FeesSettings.aspx">
                <img src="/Images/home/grants-icon2.png">
                <p>Fees Settings</p>
             </a>

            <a class="boxMenu" href="/Admin/SalaryAllowanceType.aspx">
                <img src="/Images/home/allowance.jpg">
                <p>Salary Allowance Type</p>
             </a>

            <a class="boxMenu" href="/Admin/AddExam.aspx">
                <img src="/Images/home/exam.jpg">
                <p>Exam</p>
             </a>

            <a class="boxMenu" href="/Admin/ExamSettings.aspx">
                <img src="/Images/home/grants-icon2.png">
                <p>Exam Settings</p>
             </a>


             
            

        </div>
    </div>
</asp:Content>
