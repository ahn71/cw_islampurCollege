<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ManageClassSection.aspx.cs" Inherits="DS.UI.Administration.Settings.AcademicSettings.ManageClassSection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
          #tblClassList_length {
             display: none;
            padding: 15px;
        }
         #tblClassList_filter {
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
    <asp:HiddenField ID="lblClsSecID" ClientIDMode="Static" runat="server"/>    
    <asp:HiddenField ID="lblBuidlingId" ClientIDMode="Static" runat="server"/>  
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                 <li><a id="A2" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">System Settings Module</a></li>
                <li><a id="A4" runat="server" href="~/UI/Administration/Settings/AcademicSettings/AcdSettingsHome.aspx">Academic Settings</a></li>
                <li class="active">Add Class Section</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Class Section List</h4>
                  <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
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
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                             <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                        </Triggers>                       
                        <ContentTemplate>
                            <div class="tgPanelHead">Add Class Section Name</div>
                             <div class="row tbl-controlPanel">
                                <div class="col-sm-8 col-sm-offset-2">
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Class Name</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="input form-control" AutoPostBack="true"
                                            ClientIDMode="Static" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Group Name</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="input controlLength form-control" AutoPostBack="false"
                                            ClientIDMode="Static">
                                        </asp:DropDownList>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Section Name</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="input controlLength form-control" AutoPostBack="false"
                                            ClientIDMode="Static">
                                        </asp:DropDownList>
                                        </div>
                                      </div>
                                       <div class="row tbl-controlPanel">
                                        <label class="col-sm-4"></label>
                                        <div class="col-sm-8">
                                            <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" />
                                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                        </div>
                                      </div>
                                </div>
                            </div>
                          
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
                "iDisplayLength": 100,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblClassList').dataTable({
                "iDisplayLength": 100,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if ($('#ddlClass').val() == '0') {
                showMessage('Select Class Name','warning');
                return false;
            }
            if ($('#ddlSection').val() == '0') {
                showMessage('Select Section Name', 'warning');
                return false;
            }
            return true;
        }
        function editCS(ClsSecID, ClassID,GroupID,SectionID) {
            $('#lblClsSecID').val(ClsSecID);            
            $('#ddlClass').val(ClassID);
            if (GroupID == 0) {
                $('#ddlGroup').prop('disabled', true);
            }
            else {
                $('#ddlGroup').prop('disabled', false);
            }
            $('#ddlGroup').val(GroupID);
            $('#ddlSection').val(SectionID);
            $("#btnSubmit").val('Update');
        }
        function clearIt() {
            $('#ddlClass').val('0');
            $('#ddlGroup').val('0');
            $('#ddlSection').val('0');
            $('#lblClsSecID').val('');
            $('#lblBuidlingId').val('');           
            $("#btnSubmit").val('Save');           
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
