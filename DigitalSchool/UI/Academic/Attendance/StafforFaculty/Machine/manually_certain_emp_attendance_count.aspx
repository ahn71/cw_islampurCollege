<%@ Page Title="Manually Attendance" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="manually_certain_emp_attendance_count.aspx.cs" Inherits="DS.UI.Academics.Attendance.StafforFaculty.Machine.manually_certain_emp_attendance_count" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tgPanel {
            width: 100%;
        }
        .alignment {
            text-align: center;
        }
        .controlLength {
            width: 196px;
        }
        input[type="radio"]{
            margin: 7px;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
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
                <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Machine/MachineHome.aspx">Attendance By Machine</a></li>
                <li class="active">Attendance Entry By Manually</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnImport" />
            <asp:AsyncPostBackTrigger ControlID="rblAttendanceCountType" />
        </Triggers>
        <ContentTemplate>
            <div class="tgPanel">
                <div class="tgPanelHead">Certain Teacher/Staff Attendance Entry</div>
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Count Type
                        </td>
                        <td>
                            <asp:RadioButtonList ClientIDMode="Static" CssClass="" ID="rblAttendanceCountType" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblAttendanceCountType_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="Single">Single</asp:ListItem>
                                <asp:ListItem Value="Multiple">Multiple</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>Card No 
                        </td>
                        <td>
                            <asp:TextBox ID="txtECardNo" PLaceHolder="Type Card No" runat="server" ClientIDMode="Static"
                                CssClass="input controlLength"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td id="tdFromDate" runat="server">From Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                PlaceHolder="Click For Calendar"></asp:TextBox>
                            <asp:CalendarExtender ID="CExtApplicationDate" runat="server" Enabled="True" Format="dd-MM-yyyy"
                                PopupButtonID="imgAttendanceDate" TargetControlID="txtFromDate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr id="trToDate" runat="server">
                        <td>To Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                PlaceHolder="Click For Calendar"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MM-yyyy"
                                    PopupButtonID="imgAttendanceDate" TargetControlID="txtToDate">
                                </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>In Time 
                        </td>
                        <td>
                            <asp:TextBox ID="txtInHur" MaxLength="2" Width="60px" PLaceHolder="HH" runat="server"
                                ClientIDMode="Static" CssClass="input" Style="margin: 5px 0px; text-align: center; font-weight: bold;"></asp:TextBox>
                            <asp:TextBox ID="txtInMin" MaxLength="2" Width="60px" PLaceHolder="MM" runat="server"
                                ClientIDMode="Static" CssClass="input" Style="margin: 5px 5px; text-align: center; font-weight: bold;"></asp:TextBox>
                            <asp:TextBox ID="txtInSec" MaxLength="2" Width="60px" PLaceHolder="SS" runat="server"
                                ClientIDMode="Static" CssClass="input" Style="margin: 5px 5px; text-align: center; font-weight: bold;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Out Time 
                        </td>
                        <td>
                            <asp:TextBox ID="txtOutHur" MaxLength="2" Width="60px" PLaceHolder="HH" runat="server"
                                ClientIDMode="Static" CssClass="input" Style="margin: 5px 0px; text-align: center; font-weight: bold;"></asp:TextBox>
                            <asp:TextBox ID="txtOutMin" MaxLength="2" Width="60px" PLaceHolder="MM" runat="server"
                                ClientIDMode="Static" CssClass="input" Style="margin: 5px 5px; text-align: center; font-weight: bold;"></asp:TextBox>
                            <asp:TextBox ID="txtOutSec" MaxLength="2" Width="60px" PLaceHolder="SS" runat="server"
                                ClientIDMode="Static" CssClass="input" Style="margin: 5px 5px; text-align: center; font-weight: bold;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Attendance                               
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAttendanceTemplate" runat="server" ClientIDMode="Static"
                                CssClass="input controlLength">
                                        <asp:ListItem Value="s" Selected="True">Select</asp:ListItem>
                                        <asp:ListItem Value="p">Present</asp:ListItem>
                                        <asp:ListItem Value="a">Absent</asp:ListItem>
                                        <asp:ListItem Value="l">Late</asp:ListItem>
                                        <asp:ListItem Value="w">Weekend</asp:ListItem>
                                        <asp:ListItem Value="h">Holiday</asp:ListItem>
                                        <asp:ListItem Value="lv">Leave</asp:ListItem>
                                                                     
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:LinkButton ID="lnkStudentAttList" runat="server" Text="Attendance List"
                                PostBackUrl="~/Forms/machine_attendance/emp_attendance_list.aspx"></asp:LinkButton>--%>
                        </td>
                        <td>
                            <asp:Button ID="btnImport" ValidationGroup="Impirt" CssClass="btn btn-primary" runat="server" Text="Import"
                                OnClientClick=" return InputValidationBasket();" OnClick="btnImport_Click" />
                            <asp:Button ID="Button3" runat="server" Text="Close" PostBackUrl="~/Dashboard.aspx" CssClass="btn btn-default" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <span style="font-family: 'Times New Roman'; font-size: 20px; color: #1fb5ad; font-weight: bold; float: left">
                                        <p>Wait attendance&nbsp; processing</p>
                                    </span>
                                    <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function InputValidationBasket() {
            try {
                if ($('#txtECardNo').val().trim().length < 3) {
                    showMessage('Please type valid card no', 'error');
                    $('#txtECardNo').focus(); return false;
                }
                if ($('#txtFromDate').val().trim().length == 0) {
                    showMessage('Please select date for partial attendance import', 'error');
                    $('#txtFromDate').focus(); return false;
                }
                if ($('#ddlAttendanceTemplate').val().trim() == "s") {
                    showMessage('Please select attendance type', 'error');
                    $('#ddlAttendanceTemplate').focus(); return false;
                }
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
    </script>
</asp:Content>

