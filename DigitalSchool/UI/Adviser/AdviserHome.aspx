<%@ Page Title="" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="AdviserHome.aspx.cs" Inherits="DS.UI.Adviser.AdviserHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                    <a id="A6" runat="server" onserverclick="A6_ServerClick">
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
                                        <asp:Label ID="lblName" Font-Bold="true" Text="My DashBoard" Font-Size="16px" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Card No</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblCardNo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Teacher Code</td>
                                     <td>:</td>
                                    <td>
                                        <asp:Label ID="lblTCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Department</td>
                                      <td>:</td>
                                    <td>
                                        <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Designation</td>
                                      <td>:</td>
                                    <td>
                                        <asp:Label ID="lblDesignation" runat="server"></asp:Label>
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
                                Adviser Profile Information
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a id="A2" runat="server" href="~/UI/Adviser/GuideStudentList.aspx">
                       <div class="mini-stat clearfix">
                            <span class="mini-stat-icon tar">
                                <img width="45" src="../../Images/attendance.ico" alt="attendence" />
                            </span>
                            <div class="mini-stat-info">
                                <span>Guide Student</span>
                               Guide Student list,Attendence,Result
                            </div>
                        </div>
                    </a>
                </div>
                 <div class="col-md-3">
                    <a id="A9" runat="server" onserverclick="A9_ServerClick">
                       <div class="mini-stat clearfix">
                            <span class="mini-stat-icon green">
                                <i class="glyphicon glyphicon-time"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>Guide Student Result</span>
                              Student Wise Result
                            </div>
                        </div>
                    </a>
                </div> 
                 <div class="col-md-3">
                    <a id="A10" runat="server" onserverclick="A10_ServerClick">
                       <div class="mini-stat clearfix">
                            <span class="mini-stat-icon green">
                                <i class="glyphicon glyphicon-time"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>Guide Student Attendance</span>
                              Student Wise Attendance
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
                    <a id="A4" runat="server" href="~/UI/Adviser/ClassRoutine.aspx">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon green">
                                <i class="glyphicon glyphicon-time"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>My Routine</span>
                                Batch Wise Class Routine
                            </div>
                        </div>
                    </a>
                </div>
                <%-- <div class="col-md-3">
                    <a id="A5" runat="server" href="~/UI/StudentManage/StudentTimetable.aspx">
                       <div class="mini-stat clearfix">
                            <span class="mini-stat-icon green">
                                <i class="glyphicon glyphicon-time"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>Exam Duty</span>
                                Batch Wise Exam Date
                            </div>
                        </div>
                    </a>
                </div>     --%>  
                <div class="col-md-3">
                    <a id="A7" runat="server" href="~/UI/Adviser/AttLeaveHome.aspx">
                        <div class="mini-stat clearfix">
                            <span class="mini-stat-icon green-theme">
                                <%--<img width="45" src="../../Images/finance.png" alt="finance" />--%>
                                <i class="fa fa-book"></i>
                            </span>
                            <div class="mini-stat-info">
                                <span>Attendance & Leave</span>
                                Daily and Monthly Attendance
                            </div>
                        </div>
                    </a>
                </div>
                </ContentTemplate>
             </asp:UpdatePanel>
       
        <div class="col-md-3">
            <a id="A8" runat="server" href="~/UI/Adviser/ChangePassword.aspx">
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
