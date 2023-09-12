<%@ Page Title="Attendance Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Machine_AttendanceDetails.aspx.cs" Inherits="DS.Forms.machine_attendance.AbsentDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    
    <style>        
        .controlLength{
            width:120px;
            margin: 5px;
        }
        .tgPanel
        {
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <p class="message" id="P1" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="tgPanel">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dlBatchName" />
                    <asp:AsyncPostBackTrigger ControlID="dlShiftName" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceSheet" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" />
                    <asp:AsyncPostBackTrigger ControlID="dlRoll" />
                    <asp:AsyncPostBackTrigger ControlID="ddlSection" />
                    <asp:AsyncPostBackTrigger ControlID="btnByRollAndName" />
                </Triggers>
                <ContentTemplate>
                    <div class="tgPanelHead">Attendance Details</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Shift
                            </td>
                            <td>
                                <asp:DropDownList ID="dlShiftName" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Month
                            </td>
                            <td>
                                <asp:DropDownList ID="dlMonth" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                            <td>Batch
                            </td>
                            <td>
                                <asp:DropDownList ID="dlBatchName" CssClass="input controlLength" runat="server" ClientIDMode="Static" AutoPostBack="True"
                                    OnSelectedIndexChanged="dlBatchName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>Section
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSection" CssClass="input controlLength" runat="server" ClientIDMode="Static" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnByRollAndName" Text="By Name" CssClass="btn btn-warning litleMargin"
                                    OnClick="btnByRollAndName_Click" />
                            </td>
                            <td>
                                <asp:DropDownList ID="dlRoll" CssClass="input controlLength" runat="server" ClientIDMode="Static" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-success litleMargin"
                                    OnClick="btnSearch_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table class="tbl-controlPanel">
                <tr>
                    <td>
                        <asp:Button ID="btnTodayAttendanceSheet" runat="server" ClientIDMode="Static" Text="Today Attendance Sheet"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceSheet_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnTodayAttendanceList" runat="server" ClientIDMode="Static" Text="Today Attendance List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceList_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnTodayAbsentList" runat="server" ClientIDMode="Static" Text="Today Absent List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAbsentList_Click" />
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                                    CssClass="btn btn-primary litleMargin" OnClick="btnPrintPreview_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table class="tbl-controlPanel">
                <tr>
                    <td>From
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFromDate" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="d-M-yyyy"></asp:CalendarExtender>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtToDate" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="d-M-yyyy"></asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnDateRangeSearch" Text="Search" CssClass="btn btn-success litleMargin"
                            OnClick="btnDateRangeSearch_Click" ClientIDMode="Static" />
                    </td>
                </tr>
            </table>
            <div class="tgPanelHead">Searching Result</div>
            <div class="widget">
                <div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceSheet" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDateRangeSearch" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="lblSectionDiv" runat="server">
                                <span style="font-size: 16px">
                                    <asp:Label ID="lblMonthName" runat="server" CssClass="lblFontStyle"></asp:Label></span><br />
                                <span style="font-size: 16px">
                                    <asp:Label ID="lblBatchName" runat="server" CssClass="lblFontStyle"></asp:Label></span>
                                <br />
                                <span style="font-size: 16px">
                                    <asp:Label ID="lblShiftName" runat="server" CssClass="lblFontStyle"></asp:Label>
                                </span>
                                <br />
                                <span style="font-size: 16px">
                                    <asp:Label ID="lblSectionName" runat="server" CssClass="lblFontStyle"></asp:Label>
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
        function viewStudent(studentId) {
            goToNewTab('/Report/IndividualAttendanceReport.aspx?StudentId=' + studentId); //for new tab open
        }
    </script>
</asp:Content>

