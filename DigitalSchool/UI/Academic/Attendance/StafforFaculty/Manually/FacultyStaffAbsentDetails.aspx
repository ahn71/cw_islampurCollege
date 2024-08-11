<%@ Page Title="Faculty and Staff Absent Details" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FacultyStaffAbsentDetails.aspx.cs" Inherits="DS.UI.Academics.Attendance.StafforFaculty.Manually.FacultyStaffAbsentDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <style>
        .controlLength {
            width: 120px;
            margin: 5px;
        }
        .tgPanel {
            width: 100%;
        }
        #tblSetRollOptionalSubject {
            width: 100%;
        }
        #tblSetRollOptionalSubject th,
        #tblSetRollOptionalSubject td,
        #tblSetRollOptionalSubject td input,
        #tblSetRollOptionalSubject td select {
            padding: 5px 5px;
            margin-left: 10px;
            text-align: center;
        }
        .litleMargin {
            margin-right: 5px;
        }

        .boX { text-align: center;} 
        div.col-sm-10.col-sm-offset-1.boX div.form-inline div.form-group label{ text-align: left; }
        
        
         @media (max-width: 767px) {
            .controlLength {
              width: 90%;
            }
        }
          .table tr th{
            background-color: #23282C;
            color: white;
        }
        .table tr th:first-child, tr td:first-child, tr th:nth-child(3), tr td:nth-child(3), tr th:nth-child(4),tr td:nth-child(4), tr th:nth-child(5),tr td:nth-child(5) {
            text-align: center;        
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
                <li><a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/StafforFacultyHome.aspx">Staff or Faculty Attendance</a></li>
               <%-- <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/ManuallyHome.aspx">Attendance By Manually</a></li>--%>
                <li class="active">Faculty and Staff Attendance List</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="tgPanel">
            <div class="tgPanelHead">Faculty and Staff Attendance Details</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dlDesignation" />
                    <asp:AsyncPostBackTrigger ControlID="dlDepartment" />
                    <asp:AsyncPostBackTrigger ControlID="ddlShiftList" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayPresentList" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" />
                </Triggers>
                <ContentTemplate>
                     <div class="row tbl-controlPanel"> 
		                    <div class="col-sm-10 col-sm-offset-1 boX">
			                    <div class="form-inline">
				                     <div class="form-group">
					                     <label for="exampleInputName2">Shift</label>
					 <asp:DropDownList ID="ddlShiftList" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True">
                                </asp:DropDownList>
				                     </div>
				                    <div class="form-group">
					                     <label for="exampleInputName2">Department</label>
                                        <asp:DropDownList ID="dlDepartment" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" OnSelectedIndexChanged="dlDepartment_SelectedIndexChanged">
                                </asp:DropDownList>
				                     </div>
				                    <div class="form-group">
					                     <label for="exampleInputName2">Designation</label>
                                        <asp:DropDownList ID="dlDesignation" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" OnSelectedIndexChanged="dlDesignation_SelectedIndexChanged">
                                </asp:DropDownList>
				                     </div>
				                    <div class="form-group">
					                     <label for="exampleInputName2">Month</label>
                                        <asp:DropDownList ID="dlSheetName" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
				                     </div>
				                    <div class="form-group">
					                     <asp:DropDownList Visible="false" ID="dlName" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
				                     </div>
				
				                     <div class="form-group">
					                    <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-success litleMargin" ClientIDMode="Static" runat="server"
                                    OnClick="btnSearch_Click" />
				                     </div>
			                    </div>
	                       </div>
                     </div>
                    <%--<table class="tbl-controlPanel">
                        <tr> 
                              <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="ddlShiftList" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>                          
                            <td>Department</td>
                            <td>
                                <asp:DropDownList ID="dlDepartment" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" OnSelectedIndexChanged="dlDepartment_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>Designation</td>
                            <td>
                                <asp:DropDownList ID="dlDesignation" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" OnSelectedIndexChanged="dlDesignation_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                             <td>Month</td>
                            <td>
                                <asp:DropDownList ID="dlSheetName" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                           <td>Name</td>
                            <td>
                                <asp:DropDownList Visible="false" ID="dlName" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-success litleMargin" ClientIDMode="Static" runat="server"
                                    OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>--%>
             <%--   </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                     <div class="row tbl-controlPanel"> 
		                <div class="col-sm-8 col-sm-offset-2 boX">
			                <div class="form-inline">
				                 <div class="form-group">
					                 <asp:Button ID="btnTodayAttendanceList" runat="server" ClientIDMode="Static" Text="Todays Attendance List"
                            CssClass="btn btn-primary litleMargin form-control" OnClick="btnTodayAttendanceList_Click" />
					 
				                 </div>
				                <div class="form-group">
					                <asp:Button ID="btnTodayPresentList" runat="server" ClientIDMode="Static" Text="Todays Present List"
                            CssClass="btn btn-primary litleMargin form-control" OnClick="btnTodayPresentList_Click"/>
				                 </div>
				                <div class="form-group">
					                 <asp:Button ID="btnTodayAbsentList" runat="server" ClientIDMode="Static" Text="Todays Absent List"
                            CssClass="btn btn-primary litleMargin form-control" OnClick="btnTodayAbsentList_Click"/>
				                 </div>
				                <div class="form-group">
					                 <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                                    CssClass="btn btn-primary litleMargin form-control" OnClick="btnPrintPreview_Click1" />
				                 </div>
				                
			                </div>
	                   </div>
                 </div>
                    <%--<table class="tbl-controlPanel">
                         <tr>                  
                    <td>
                        <asp:Button ID="btnTodayAttendanceList" runat="server" ClientIDMode="Static" Text="Todays Attendance List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceList_Click" />
                    </td>
                      <td>
                        <asp:Button ID="btnTodayPresentList" runat="server" ClientIDMode="Static" Text="Todays Present List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayPresentList_Click"/>
                    </td>
                    <td>
                        <asp:Button ID="btnTodayAbsentList" runat="server" ClientIDMode="Static" Text="Todays Absent List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAbsentList_Click"/>
                    </td>
                    <td>                        
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                                    CssClass="btn btn-primary litleMargin" OnClick="btnPrintPreview_Click1" />                      
                    </td>
                </tr>--%>
                      <%--  <tr>
                            <td>
                                <asp:Button ID="btnTodayAttendanceSheet" runat="server" Text="Today Attendance Sheet"
                                    CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceSheet_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnTodayAttendanceList" runat="server" Text="Today Attendance List"
                                    CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceList_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnTodayAbsentList" runat="server" Text="Today Absent List"
                                    CssClass="btn btn-primary litleMargin" OnClick="btnTodayAbsentList_Click" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                                    CssClass="btn btn-primary litleMargin" OnClick="btnPrintPreview_Click" />
                            </td>
                        </tr>--%>
                    <%--</table>--%>
           <%--     </ContentTemplate>
            </asp:UpdatePanel>--%>
            <table runat="server" visible="false" class="tbl-controlPanel">
                <tr>
                    <td>From</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFromDate" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="d-M-yyyy"></asp:CalendarExtender>
                    </td>
                    <td>To</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtToDate" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="d-M-yyyy"></asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnDateRangeSearch" Text="Search" CssClass="btn btn-success litleMargin" ClientIDMode="Static"
                            OnClick="btnDateRangeSearch_Click" />
                    </td>
                </tr>
            </table>  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <br />
                            <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                <p>Wait attendance sheet is processing</p>
                            </span>
                            <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel> <br /><br />                                           
            <div runat="server" id="divHeadMsg" class="tgPanelHead">Find your attendance status</div>                                                
                        </ContentTemplate>
                </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                <ContentTemplate>                     
            <div class="widget">
                <div class="head">
                    <div class="dataTables_filter" style="float: right;">
                    </div>
                </div>
               <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnTodayPresentList" />
                        <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" />
                    </Triggers>
                    <ContentTemplate>--%>
                        <%--<div id="lblSectionDiv" style=" margin-top:5px; margin-bottom:10px; color:#1fb5ad; text-align:center; font-size:x-large;" runat="server">
                            <span style="font-size: 16px">
                                <asp:Label ID="lblMonthName" runat="server" CssClass="lblFontStyle"></asp:Label></span><br />
                            <span style="font-size: 16px">
                                <asp:Label ID="lblDepName" runat="server" CssClass="lblFontStyle"></asp:Label></span>
                            <br />
                            <span style="font-size: 16px">
                                <asp:Label ID="lblDesName" runat="server" CssClass="lblFontStyle"></asp:Label>
                            </span>
                        </div>--%>
                       <%-- <br />--%>              
                         <div id="div1" class="datatables_wrapper" runat="server" style="width: 100%; height: auto">  
                                 <div style="margin:0px auto; width:600px">
                                    <asp:GridView ID="gvAttList" CssClass="table table-bordered"  AutoGenerateColumns="False" ClientIDMode="Static" runat="server">                                   
                                    <Columns>                                       
                                        <asp:BoundField HeaderText="Card No" DataField="ECardNo" />
                                        <asp:BoundField HeaderText="Name"  DataField="EName"/>
                                        <asp:BoundField HeaderText="Department"  DataField="DName"/>
                                        <asp:BoundField HeaderText="Designation"  DataField="DesName"/>
                                        <asp:BoundField HeaderText="Status"  DataField="AttStatus" />                                       
                                    </Columns>
                                </asp:GridView> 
                                </div>                                                                                     
                            </div>                
                        <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto;margin-top:5px"></div>
                     </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    <%--</div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function viewEmployee(employeeId) {
            goToNewTab('/Report/IndividualEmployeeAttendanceReport.aspx?employeeId=' + employeeId); //for new tab open
        }
    </script>
</asp:Content>
