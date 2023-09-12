<%@ Page Title="Attendance Sheet Generator" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FacultyAttendanceSheetGenarate.aspx.cs" Inherits="DS.UI.Academics.Attendance.StafforFaculty.Manually.FacultyAttendanceSheetGenarate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .controlLength {
            width: 250px;
        }
        .litleMargin {
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblFeesSettId" ClientIDMode="Static" runat="server" />
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
                <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/ManuallyHome.aspx">Attendance By Manually</a></li>
                <li class="active">Attendance Sheet Generator</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <h4 class="text-right">Faculty Attendance Sheet List</h4>
                </div>
                <div class="col-md-6"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <div id="divAttendanceSheetList" class="datatables_wrapper" runat="server"
                        style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Attendance Sheet Generate</div>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>Month 
                                </td>
                                <td>
                                    <asp:DropDownList ID="dlMonths" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td
                                <td>
                                    <asp:Button CssClass="btn btn-primary litleMargin" ID="btnGenerator" ClientIDMode="Static" runat="server" Text="Generate"
                                        OnClientClick="return validateInputs();" OnClick="btnGenerator_Click" />
                                    &nbsp;<input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                </td>
                            </tr>
                        </table>                        
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlClassName").change(function () {
                var val1 = this.value;
                var val2 = $('#ddlSession').val();
                var val3 = val1 + "" + val2;
                $('#lblbatch').html(val3);
            });
            $("#ddlSession").change(function () {
                var val1 = this.value;
                var val2 = $('#ddlClassName').val();
                var val3 = val2 + "" + val1;
                $('#lblbatch').html(val3);
            });
        });
        function validateInputs() {
            if (validateText('ddlClassName.Text', 1, 20, 'Select Class') == false) return false;
            else if (validateText('ddlSession.Text', 1, 20, 'Select Session') == false) return false;
            return true;
        }
        function clearIt() {
            $('#ddlClassName').val('');
            $('#ddlSession').val('');
            $('#lblbatch').val('');
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>
