<%@ Page Title="Attendance Sheet Generate" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AttendanceSheetGenerate.aspx.cs" Inherits="DS.UI.Academics.Attendance.Student.Manually.AttendanceSheetGenerate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .controlLength {
            width: 250px;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
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
                <li><a runat="server" href="~/UI/Academic/Attendance/Student/StdAttnHome.aspx">Student Attendance</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/Student/Manually/ManuallyHome.aspx">Student Attendance By Manually</a></li>
                <li class="active">Attendance Sheet Generate</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <h4 class="text-right">Attendance Sheet List</h4>
                </div>
                <div class="col-md-6"></div>
            </div>
            <div class="row">                
                <div class="col-md-6">
                    <div id="divAttendanceSheetList" class="datatables_wrapper" runat="server"
                        style="width: 100%; height: auto; max-height: 400px; overflow: auto; overflow-x: hidden;">
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
                                    <asp:DropDownList ID="dlMonths" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                        <asp:ListItem Value="0">...Select Month...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Class 
                                </td>
                                <td>
                                    <asp:DropDownList ID="dlClass" runat="server" AutoPostBack="True" CssClass="input controlLength" ClientIDMode="Static"
                                        OnSelectedIndexChanged="dlClass_SelectedIndexChanged">
                                        <asp:ListItem Value="0">...Select Class...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Section
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dlClass" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                                <asp:ListItem Value="0">...Select Section...</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button CssClass="btn btn-primary litleMargin" ID="btnGenerator" ClientIDMode="Static" runat="server"
                                        Text="Generate" OnClientClick="return validateInputs();" OnClick="btnGenerator_Click" />
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
        function validateInputs() {
            if (validateCombo('dlMonths', "0", 'Select a Month') == false) return false;
            if (validateCombo('dlClass', "0", 'Select a class') == false) return false;
            if (validateCombo('dlSection', "0", 'Select a selection') == false) return false;
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


