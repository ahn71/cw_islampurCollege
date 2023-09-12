<%@ Page Title="Add Education Board" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddBoard.aspx.cs" Inherits="DS.UI.Administration.Settings.GeneralSettings.AddBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
         #tblDesignationList_length {
             display: none;
            padding: 15px;
        }
         #tblDesignationList_filter {
            display: none;
            padding: 15px;
        }
          #tblDesignationList_info {
             display: none;
            padding: 15px;
        }
         #tblDesignationList_paginate {
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">System Settings Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/GeneralSettingsHome.aspx">General Settings</a></li>
                <li class="active">Add Education Board</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Education Board List</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New" style="width:186px;"   placeholder="type here" />
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
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divBoard" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                        <asp:HiddenField ID="lblBoardId" ClientIDMode="Static" Value="" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Education Board</div>
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-8 col-sm-offset-2">
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Board Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtBoardName" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4"></label>
                                        <div class="col-sm-8">
                                            <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                             <input id="tnReset" type="reset" value="Reset" class="btn btn-default" />
                                        </div>
                                      </div>
                                    
                                </div>
                            </div>
                                                    
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblDesignationList', '');
            });
            $('#tblDesignationList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblDesignationList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateText('txtBoardName', 1, 50, 'Enter a Board Name') == false) return false;
            return true;
        }
        function editBoards(BoardId) {
            $('#lblBoardId').val(BoardId);
            var strBoardName = $('#r_' + BoardId + ' td:first-child').html();
            $('#txtBoardName').val(strBoardName);
            $("#btnSave").val('Update');
        }
    </script>
</asp:Content>
