<%@ Page Title="Session Name And Start-End Month" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SessionDateTime.aspx.cs" Inherits="DS.UI.Academic.Timetable.SetTimings.SessionDateTime" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .controlLength{
            width: 200px;
        }
        .ajax__calendar_footer {
            height: auto!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblSessionId" ClientIDMode="Static" runat="server"/>    
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
                <li><a runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">Timetable Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Timetable/SetTimings/SetTimesHome.aspx">Set Timings Managed</a></li>
                <li class="active">Session Start and End Month</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right">Session Name And Start-End Month</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">            
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="tgPanelHead">Add New Session Name</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Session Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSessionName" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Start Date Month</td>
                                    <td>
                                        <asp:TextBox ID="StartMonth" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd, MMMM" 
                                            TargetControlID="StartMonth"></asp:CalendarExtender>
                                    </td>
                                </tr> 
                                <tr>
                                    <td>End Date Month</td>
                                    <td>
                                        <asp:TextBox ID="EndMonth" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd, MMMM" 
                                            TargetControlID="EndMonth"></asp:CalendarExtender>
                                    </td>
                                </tr> 
                                <tr>
                                    <td>Session Description</td>
                                    <td>
                                        <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" ClientIDMode="Static" 
                                            CssClass="input controlLength" Rows="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                    </td>
                                </tr>                              
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtSessionName', 1, 50, 'Enter a Session Name') == false) return false;
            if (validateText('StartMonth', 1, 50, 'Enter a Start Month') == false) return false;
            if (validateText('EndMonth', 1, 50, 'Enter a End Month') == false) return false;
            return true;
        }
        function editRow(Id) {
            $('#lblSessionId').val(Id);
            $('#txtSessionName').val($('#Name' + Id).html());
            $('#StartMonth').val($('#StrMonth' + Id).html());
            $('#EndMonth').val($('#EndMonth' + Id).html());
            $('#txtDetails').val($('#Description' + Id).html());
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('input[type=text]').val('');
            $('textarea').val('');
            $('#lblSessionId').val('');
            $("#btnSave").val('Save');
            setFocus('TxtName');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SavedSuccess() {
            showMessage('Saved successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
