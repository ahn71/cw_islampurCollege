<%@ Page Title="Student Manually Attendance Entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manually_certain_attendance_count.aspx.cs" Inherits="DS.Forms.machine_attendance.manually_certain_attendance_count" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .alignment {
            text-align: center;
        }
        .controlLength {
            width: 196px;
        }
    </style>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server" UpdateMode="Always">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnImport" />
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="tgPanel">
                    <div class="tgPanelHead">Certain Student Attendance Entry</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Adm. No 
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdmissionNo" PLaceHolder="Type Admission No" runat="server"
                                    ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtAttendanceDate" runat="server" placeholder="Pick up Date"
                                    ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                    PopupButtonID="imgAttendanceDate" Enabled="True"
                                    TargetControlID="txtAttendanceDate" ID="CExtApplicationDate">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>In Time 
                            </td>
                            <td>
                                <asp:TextBox ID="txtInHur" class="input" type="text" Style="width: 60px; margin: 5px 0px; text-align: center; font-weight: bold;" runat="server"
                                    placeholder="HH" MaxLength="2" name="ctl00$MainContent$txtInHur" />
                                <asp:TextBox ID="txtInMin" class="input" type="text" Style="width: 60px; margin: 5px 5px; text-align: center; font-weight: bold;" runat="server"
                                    placeholder="MM" MaxLength="2" name="ctl00$MainContent$txtInMin" />
                                <asp:TextBox ID="txtInSec" class="input" type="text" Style="width: 60px; margin: 5px 0px; text-align: center; font-weight: bold;" runat="server"
                                    placeholder="SS" MaxLength="2" name="ctl00$MainContent$txtInSec" />
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
                                    ClientIDMode="Static" CssClass="input" Style="margin: 5px 0px; text-align: center; font-weight: bold;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Atten. Status 
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAttendanceTemplate" runat="server" ClientIDMode="Static"
                                    CssClass="input controlLength">
                                    <asp:ListItem Value="p">Present</asp:ListItem>
                                    <asp:ListItem Value="a">Absent</asp:ListItem>
                                    <asp:ListItem Value="l">Late</asp:ListItem>
                                    <asp:ListItem Value="s" Selected="True">Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkStudentAttList" runat="server" Text="Attendance List"
                                    PostBackUrl="~/Forms/machine_attendance/student_attendance_list.aspx"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:Button ID="btnImport" ValidationGroup="Impirt" CssClass="btn btn-primary" runat="server" Text="Save"
                                    OnClientClick=" return InputValidationBasket();" OnClick="btnImport_Click" />
                                <asp:Button ID="Button3" runat="server" Text="Close" PostBackUrl="~/default.aspx" CssClass="btn btn-default" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                            <p>Wait attendance&nbsp; processing</p>
                                        </span>
                                        <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function InputValidationBasket() {
            try {
                if ($('#txtAdmissionNo').val().trim().length < 1) {
                    showMessage('Please type valid admission no', 'error');
                    $('#txtAdmissionNo').focus(); return false;
                }
                if ($('#txtAttendanceDate').val().trim().length == 0) {
                    showMessage('Please select date for single attendance count', 'error');
                    $('#txtAttendanceDate').focus(); return false;
                }
                if ($("#txtInHur").val().trim().length == 0) {
                    showMessage('Please type login hour', 'error');
                    $('#txtInHur').focus(); return false;
                }
                if ($("#txtInMin").val().trim().length == 0) {
                    showMessage('Please type login Minute', 'error');
                    $('#txtInMin').focus(); return false;
                }
                if ($("#txtInSec").val().trim().length == 0) {
                    showMessage('Please type login Second', 'error');
                    $('#txtInSec').focus(); return false;
                }
                if ($("#txtOutHur").val().trim().length == 0) {
                    showMessage('Please type logout hour', 'error');
                    $('#txtOutHur').focus(); return false;
                }
                if ($("#txtOutMin").val().trim().length == 0) {
                    showMessage('Please type logout minute', 'error');
                    $('#txtOutMin').focus(); return false;
                }
                if ($("#txtOutSec").val().trim().length == 0) {
                    showMessage('Please type logout second', 'error');
                    $('#txtOutSec').focus(); return false;
                }
                if ($("#txtOutHur").val().trim().length == 0) {
                    showMessage('Please type login hour', 'error');
                    $('#txtInSec').focus(); return false;
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
        function ClearInputBox() {
            try {
                $('#txtAdmissionNo').val('');
                $('#txtAttendanceDate').val('');
                $('#txtInHur').val('');
                $('#txtInMin').val('');
                $('#txtInSec').val('');
                $('#txtOutHur').val('');
                $('#txtOutMin').val('');
                $('#txtOutSec').val('');
                $('#ddlAttendanceTemplate').val('s');
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
    </script>
</asp:Content>

