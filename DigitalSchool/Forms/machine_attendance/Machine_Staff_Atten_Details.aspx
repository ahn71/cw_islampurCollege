<%@ Page Title="Staff Or Faculty Absent Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Machine_Staff_Atten_Details.aspx.cs" Inherits="DS.Forms.machine_attendance.WebForm1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    
    <style>        
        .controlLength{
            width:120px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Faculty and Staff Attendance Details</div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlDesignation" />
                        <asp:AsyncPostBackTrigger ControlID="dlDepartment" />
                    </Triggers>
                    <ContentTemplate>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>Month</td>
                                <td>
                                    <asp:DropDownList ID="dlMonth" CssClass="input controlLength" runat="server" ClientIDMode="Static">
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
                                <td>Name</td>
                                <td>
                                    <asp:DropDownList ID="dlName" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success litleMargin"
                                         OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>        
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="tbl-controlPanel">
                            <tr>
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
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table class="tbl-controlPanel">
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
                </table>
                <div class="tgPanelHead">Searching Result</div>
                <div class="widget">
                    <div class="head">
                        <div class="dataTables_filter" style="float: right;">
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceSheet" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="lblSectionDiv" runat="server">
                                <span style="font-size: 16px">
                                    <asp:Label ID="lblMonthName" runat="server" CssClass="lblFontStyle"></asp:Label></span><br />
                                <span style="font-size: 16px">
                                    <asp:Label ID="lblDepName" runat="server" CssClass="lblFontStyle"></asp:Label></span>
                                <br />
                                <span style="font-size: 16px">
                                    <asp:Label ID="lblDesName" runat="server" CssClass="lblFontStyle"></asp:Label>
                                </span>
                            </div>
                            <br />
                            <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
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
        function viewEmployee(employeeId) {
            goToNewTab('/Report/IndividualEmployeeAttendanceReport.aspx?employeeId=' + employeeId); //for new tab open
        }
    </script>
</asp:Content>

