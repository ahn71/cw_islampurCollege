<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentManage.aspx.cs" Inherits="DS.UI.StudentManage.StudentManage1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .profile-pic text-center{
            margin-left: -127px;
            margin-top: 0;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <section class="panel">
                <div class="panel-body profile-information">
                     
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="profile-pic text-center" style="margin-left:-28px; margin-top:-18px">
                                    <a id="A6" runat="server" onserverclick="A1_ServerClick">
                                        <asp:Image ID="stImage" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                                    </a>                                    
                                </div>
                                <div>
                                    <asp:Label ID="lblPicName" runat="server" style="text-align:center" Font-Bold="true" Font-Size="16px"></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
               
                    <div class="col-md-6">                        
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblName" Font-Bold="true"  Text="My DashBoard" Font-Size="16px" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Shift</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label class="" ID="lblShift" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Batch</td>
                                     <td>:</td>
                                    <td>
                                        <asp:Label ID="lblBatch" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Group</td>
                                      <td>:</td>
                                    <td>
                                        <asp:Label ID="lblGroup" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Section</td>
                                      <td>:</td>
                                    <td>
                                        <asp:Label ID="lblSection" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Roll</td>
                                     <td>:</td>
                                    <td>
                                        <asp:Label ID="lblRoll" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>                       
                    </div>
                    <div class="col-md-3"></div>
                </div>
            </section>
    </div>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-md-3">
                    <a id="A1" runat="server" onserverclick="A1_ServerClick">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon orange">
                                <i class="fa  ico-profile"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>Profile</span>
                                Indivisual Student Information
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a id="A2" runat="server" href="~/UI/StudentManage/StudentAttDetails.aspx">
                       <div class="mini-stat clearfix">
                            <span class="mini-stat-icon tar">
                                <img width="45" src="../../Images/dashboard_icon/StudentAttendance.png" alt="attendence" />
                            </span>
                            <div class="mini-stat-info">
                                <span>Attendance</span>
                                Daily And Monthly Attendance
                            </div>
                        </div>
                    </a>
                </div>               
                <div class="col-md-3">
            <a id="A5" runat="server" href="~/UI/StudentManage/StudentFinance.aspx">
                 <div class="mini-stat clearfix">
                            <span class="mini-stat-icon blue-theme">
                               <img width="45" src="../../Images/finance.png" alt="finance" />
                            </span>
                            <div class="mini-stat-info">
                                <span>Finance</span>
                                Category Wise Report,Payment Details,Due & Fine List
                            </div>
                        </div>
            </a>
        </div>
                <div class="col-md-3">
                    <a id="A4" runat="server" onserverclick="A4_ServerClick">
                       <div class="mini-stat clearfix">
                            <span class="mini-stat-icon green">
                                <i class="glyphicon glyphicon-time"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>My Routine</span>
                                Class Routine,Exam Routine
                            </div>
                        </div>
                    </a>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="row">
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>        
        <div class="col-md-3">
            <a id="A7" runat="server" onserverclick="A7_ServerClick">
                 <div class="mini-stat clearfix">
                            <span class="mini-stat-icon green-theme">
                               <%--<img width="45" src="../../Images/finance.png" alt="finance" />--%>
                                <i class="fa fa-book"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>Taught List</span>
                                Running Class Book List
                            </div>
                        </div>
            </a>
        </div>
                </ContentTemplate>
             </asp:UpdatePanel>
         <div class="col-md-3">
                    <a id="A3" runat="server" href="~/UI/StudentManage/StudentExamination.aspx">
                         <div class="mini-stat clearfix">
                            <span class="mini-stat-icon orange">
                                <i class="fa fa-bookmark"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>My Result</span>
                                TabulationSheet,Academic Transcript & Others
                            </div>
                        </div>
                    </a>
                </div>
        <div class="col-md-3">
                    <a runat="server" href="~/UI/StudentManage/ChangePassword.aspx">
                         <div class="mini-stat clearfix">
                            <span class="mini-stat-icon  pink">
                           <i class="fa fa-key"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>Change Password</span>
                                only change your password
                            </div>
                        </div>
                    </a>
                </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
