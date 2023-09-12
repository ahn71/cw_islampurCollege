<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentAttDetails.aspx.cs" Inherits="DS.UI.Reports.Attendance.StudentAttDetails" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
        .table tr th{
            background-color: #23282C;
            color: white;
        }
        .table tr th:first-child,tr td:first-child,tr th:nth-child(3),tr td:nth-child(3){
            text-align:center;
        }
    </style>
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
                   <li class="active">Attendance</li>          
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <p class="message" id="P1" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
   
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Students Attendance Details</div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlShiftList" />
                    <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                    <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
                    <asp:AsyncPostBackTrigger ControlID="ddlSection" />                  
                    <asp:AsyncPostBackTrigger ControlID="dlRoll" />              
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayPresentList" EventName="Click" />      
                    
                </Triggers>
                <ContentTemplate>
                    <table class="tbl-controlPanel">
                           <tr>
                <td>Shift
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlShiftList" Width="100px" runat="server" class="input controlLength" AutoPostBack="True" >
                                   
                                </asp:DropDownList>
                            </td>
                            <td>Batch
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBatch"  runat="server" class="input controlLength" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"   ></asp:DropDownList>
                            </td>
                             <td>Group
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlgroup" runat="server" class="input controlLength" Width="100px" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                            <td>Section
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSection" runat="server"  class="input controlLength" Width="100px" AutoPostBack="True"> </asp:DropDownList>
                            </td>
                               <td>Roll
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRoll" runat="server"  class="input controlLength" Width="100px" AutoPostBack="True"> </asp:DropDownList>
                            </td>
                        <td>Month</td>
                        <td>
                            <asp:TextBox ID="dtpMonth" ClientIDMode="Static" runat="server" CssClass="input controlLength"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM-yyyy"
                                    TargetControlID="dtpMonth">
                                </asp:CalendarExtender>
                        </td>
                            <td>
                                <asp:Button Visible="false" runat="server" ID="btnByRollAndName" Text="By Roll" CssClass="btn btn-warning litleMargin"
                                     />
                            </td>
                            <td>
                                <asp:DropDownList Visible="false" ID="dlRoll" CssClass="input controlLength" runat="server" ClientIDMode="Static" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-success litleMargin" ClientIDMode="Static" runat="server" OnClientClick="validateInputs()"
                                    OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
            <%--    </ContentTemplate>
            </asp:UpdatePanel>--%>
            <table class="tbl-controlPanel">
                <tr>                  
                    <td>
                        <asp:Button ID="btnTodayAttendanceList" runat="server" ClientIDMode="Static" Text="Todays Attendance List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceList_Click"  />
                    </td>
                      <td>
                        <asp:Button ID="btnTodayPresentList" runat="server" ClientIDMode="Static" Text="Todays Present List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayPresentList_Click"  />
                    </td>
                    <td>
                        <asp:Button ID="btnTodayAbsentList" runat="server" ClientIDMode="Static" Text="Todays Absent List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAbsentList_Click"  />
                    </td>
                    <td>                        
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                                    CssClass="btn btn-primary litleMargin" OnClick="btnPrintPreview_Click"   />                      
                    </td>
                </tr>
            </table>
            <%--<table class="tbl-controlPanel">
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

                        <asp:Button runat="server" ID="btnDateRangeSearch" Text="Search" CssClass="btn btn-success litleMargin"
                            ClientIDMode="Static"  />
                    </td>
                </tr>
            </table>--%>                    
            <div class="tgPanelHead">Searching Result</div>
            <div class="widget">
                <div>
                  <%--  <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayPresentList" EventName="Click" />                            
                        </Triggers>
                        <ContentTemplate> --%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
            </asp:UpdatePanel>
                            <div id="lblSectionDiv" style=" margin-top:5px; margin-bottom:10px; color:#1fb5ad; text-align:center; font-size:x-large;" runat="server">                                                              
                            </div>
                             <div id="div1" class="datatables_wrapper" runat="server" style="width: 100%; height: auto">  
                                 <div style="margin:0px auto; width:600px">
                                    <asp:GridView ID="gvAttList" CssClass="table table-bordered"  AutoGenerateColumns="False" ClientIDMode="Static" runat="server">                                   
                                    <Columns>                                       
                                        <asp:BoundField HeaderText="Roll No" DataField="RollNo" />
                                        <asp:BoundField HeaderText="Name"  DataField="FullName"/>
                                        <asp:BoundField HeaderText="Status"  DataField="AttStatus" />                                       
                                    </Columns>
                                </asp:GridView> 
                                </div>                                                                                     
                            </div>                                                             
                            <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto">                                                                                    
                            </div>
                          </div>
                          </div>   
                        </ContentTemplate>
                    </asp:UpdatePanel>
                <%--</div>--%>
               <%-- </div> --%>       
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
     <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function viewStudent(studentId) {
            goToNewTab('/Report/IndividualAttendanceReport.aspx?StudentId=' + studentId); //for new tab open
        }
        function validateInputs() {           
        };
    </script>
</asp:Content>
