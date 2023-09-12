<%@ Page Title="Add Building Name" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ManagedBuildings.aspx.cs" Inherits="DS.UI.Academic.Timetable.RoomAllocation.ManagedBuildings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
         .dataTables_length, .dataTables_filter {
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
                <li><a runat="server" href="~/UI/Academic/Timetable/RoomAllocation/BuildingHome.aspx">Building and Classroom Settings</a></li>
                <li class="active">Add Building</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Building Name List</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                     </label>
                 </div>                
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:HiddenField ID="lblBuidlingId" ClientIDMode="Static" runat="server"/>  
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
                            <div class="tgPanelHead">Add Building Name</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Building Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBuildingName" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                    </td>
                                </tr> 
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" />
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
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblClassList', '');
            });
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateText('txtBuildingName', 1, 50, 'Enter a Building Name') == false) return false;
            return true;
        }
        function editBuilding(BuildingId) {
            var na = $('#lblBuidlingId').val(BuildingId);            
            var strBuildingName = $('#buildingName' + BuildingId).html();
            $('#txtBuildingName').val(strBuildingName);            
            $("#btnSubmit").val('Update');
        }
        function clearIt() {
            $('input[type=text]').val('');           
            $("#btnSave").val('Save');
            setFocus('txtEx_Name');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Update successfully', 'success');
            clearIt();
        }
        function SavedSuccess() {
            loaddatatable();
            showMessage('Save successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
