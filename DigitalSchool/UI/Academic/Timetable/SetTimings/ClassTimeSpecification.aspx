<%@ Page Title="Class Time Specification" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ClassTimeSpecification.aspx.cs" Inherits="DS.UI.Academic.Timetable.SetTimings.ClassTimeSpecification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .controlLength{
            width: 200px;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        input[type="checkbox"]{
            margin: 7px;
        }
        .bootstrap-timepicker-widget table td input {
          border: 1px solid #c6c6c6!important;
          padding: 2px !important;
          width: 35px !important;
        }
        .bootstrap-timepicker-widget table td a i {
          font-size: 18px;
          margin-top: 2px;
          font-weight: bold;
        }
         .dataTables_length, .dataTables_filter{
            display: none;
            padding: 15px;
        }
        #tblClassList_info {
             display: none;
            padding: 15px;
        }
         #tblClassList_paginate {
            display: none;
            padding: 15px;
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
        }
    </style>
    <link href="../../../../AssetsNew/css/bootstrap-timepicker.min.css" rel="stylesheet" />

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
                <li><a runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">Routine Module</a></li>
                <li class="active">Class Time Specification</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float:left">Class Time Specification List</h4>
                 <div class="dataTables_filter_New" style="float: right; margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
                     </label>
                 </div>                
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">            
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="dlShift" />
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">                        
                        <ContentTemplate>
                            <asp:HiddenField ID="lblClsTimeId" ClientIDMode="Static" runat="server"/>  
                            <asp:HiddenField ID="lblClsTimeSetNameId" ClientIDMode="Static" runat="server"/>  
                            <div class="tgPanelHead">Add New Class Time</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Shift</td>
                                    <td>
                                        <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static"
                                             CssClass="input controlLength" AutoPostBack="true"
                                            OnSelectedIndexChanged="dlShift_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Name</td>
                                    <td>
                                        <asp:DropDownList ID="ddlPeriod" runat="server" ClientIDMode="Static"
                                             CssClass="input controlLength" AutoPostBack="false">
                                            <asp:ListItem>Period 1</asp:ListItem>
                                             <asp:ListItem>Period 2</asp:ListItem>
                                             <asp:ListItem>Period 3</asp:ListItem>
                                             <asp:ListItem>Period 4</asp:ListItem>
                                             <asp:ListItem>Period 5</asp:ListItem>
                                             <asp:ListItem>Period 6</asp:ListItem>
                                             <asp:ListItem>Period 7</asp:ListItem>
                                             <asp:ListItem>Period 8</asp:ListItem>
                                             <asp:ListItem>Period 9</asp:ListItem>
                                             <asp:ListItem>Period 10</asp:ListItem>
                                        </asp:DropDownList>                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>Start Time</td>
                                    <td>                                      
                                        <div class="input-group">
                                            <asp:TextBox id="txtstartTime"   runat="server" class="input controlLength"></asp:TextBox>                                 

                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>End Time</td>
                                    <td>                                     
                                        <div class="input-group">
                                            <asp:TextBox id="txtEndTime"   runat="server" class="input controlLength"></asp:TextBox>                                         
                                            
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Order By</td>
                                    <td>
                                        <asp:TextBox ID="txtOrderBy" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="ChkBrkTime" runat="server" ClientIDMode="Static" Text="Is Break Time"/>                                                                     
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
    <script src="../../../../AssetsNew/js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblClassList', '');
            });
            loadtimepicker();
        });
        function loaddatatable() {
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
            //loadtimepicker2();
        }
        function validateInputs() {
            if (validateCombo('DrpClassTimeSetName', "0", 'Select a Class Time Set Name') == false) return false;
            if (validateText('txtOrderBy', 1, 50, 'Enter a Order By') == false) return false;            
            return true;
        }
        function editRow(Id,shiftID, brkChk) {
            $('#lblClsTimeId').val(Id);
            $('#dlShift').val(shiftID).prop("disabled", true);
            $('#ddlPeriod').val($('#ClsTimeName' + Id).html());
            var strtTime = $('#ClsStartTime' + Id).html();
            $('#MainContent_txtstartTime').val(strtTime);           
            var endTime = $('#ClsEndTime' + Id).html();
            $('#MainContent_txtEndTime').val(endTime);            
            $('#txtOrderBy').val($('#order' + Id).html());
           $('#ChkBrkTime').prop('checked', brkChk);
            $("#btnSave").val('Update');
        }        
        function clearIt() {           
            $('#dlShift').prop("disabled", false);
            $('#ChkBrkTime').prop('checked', false);
            $("#btnSave").val('Save');
            $('#lblClsTimeId').val('');
            setFocus('TxtName');
            loadtimepicker();
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SavedSuccess() {
            loaddatatable();
            showMessage('Saved successfully', 'success');
            clearIt();
        }
        function loadtimepicker() {
            $('#MainContent_txtstartTime').timepicker();
            $('#MainContent_txtEndTime').timepicker();
        }
        function LoadJS() {
            loadtimepicker();
            loaddatatable();
        }
    </script>
</asp:Content>
