<%@ Page Title="Classroom Allocated To Building" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Allocated.aspx.cs" Inherits="DS.UI.Academic.Timetable.RoomAllocation.Allocated" %>

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
                <li class="active">Classroom Allocated To Building</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Classroom Allocated List</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New" style="width:150px;" placeholder="type here" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnSubmit"/>
                            <asp:AsyncPostBackTrigger ControlID="drpBuildingName"/>                                                  
                        </Triggers>                   
                    <ContentTemplate>
                        <asp:HiddenField ID="lblRmId" ClientIDMode="Static" runat="server"/>    
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">  
                                             
                        <ContentTemplate>
                            <div class="tgPanelHead">Class Room Allocate</div>
                                       <table class="tbl-controlPanel">
                                <tr>
                                    <td>Building Name</td>
                                    <td>
                                        <asp:DropDownList ID="drpBuildingName" runat="server" CssClass="input form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpBuildingName_SelectedIndexChanged" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Room Name/Code</td>
                                    <td>
                                        <asp:TextBox ID="TxtRName" runat="server" CssClass="input form-control" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Room Capacity</td>
                                    <td>
                                        <asp:TextBox ID="TxtRCapacity" runat="server" CssClass="input form-control" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr> 
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" />
                                        &nbsp;<asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn btn-default" OnClientClick="clearIt()" />
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
            if (validateCombo('drpBuildingName', 0, 'Select a Building Name') == false) return false;
            if (validateText('TxtRName', 1, 50, 'Enter a Room Name') == false) return false;
            if (validateText('TxtRCapacity', 1, 50, 'Enter a Room Capacity') == false) return false;
            return true;
        }
        function editRM(RmId, BId) {
            $('#lblRmId').val(RmId);
            $('#lblBuidlingId').val(BId);
            $('#drpBuildingName').val(BId).prop("disabled", true);          
            var rmName = $('#roomName' + RmId).html();
            $('#TxtRName').val(rmName);       
            var capcity = $('#capacity' + RmId).html();            
            $('#TxtRCapacity').val(capcity);
            $("#btnSubmit").val('Update');
        }
        function clearIt() {
            $('#drpBuildingName').val(0);
            $('#lblRmId').val('');
            $('#lblBuidlingId').val('');
            $('input[type=text]').val('');
            $("#btnSubmit").val('Save');            
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Update successfully', 'success');
            //clearIt();
            $('#lblRmId').val('');
            $('#lblBuidlingId').val('');
            $('input[type=text]').val('');
            $("#btnSubmit").val('Save');
        }
        function SavedSuccess() {
            loaddatatable();
            showMessage('Save successfully', 'success');
            //clearIt();
            $('#lblRmId').val('');
            $('#lblBuidlingId').val('');
            $('input[type=text]').val('');
            $("#btnSubmit").val('Save');
        }
    </script>
</asp:Content>
