<%@ Page Title="Add Section" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddSection.aspx.cs" Inherits="DS.UI.Administration.Settings.AcademicSettings.AddSection" %>
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
          #tblSectionList_length {
             display: none;
            padding: 15px;
        }
         #tblSectionList_filter {
            display: none;
            padding: 15px;
        }
          #tblSectionList_info {
             display: none;
            padding: 15px;
        }
         #tblSectionList_paginate {
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
                <li><a runat="server" href="~/UI/Administration/Settings/AcademicSettings/AcdSettingsHome.aspx">Academic Settings</a></li>
                <li class="active">Add Section</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Section List</h4>
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
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divSectionList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                        </div>
                         <asp:HiddenField ID="lblSectionID" ClientIDMode="Static" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add Section</div>
                    
                     <div class="row tbl-controlPanel">
                        <div class="col-sm-8 col-sm-offset-2">
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Section Name</label>
                                <div class="col-sm-8">
                                     <asp:TextBox ID="txtSectionName" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4"></label>
                                <div class="col-sm-8">
                                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static"
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="Cleartext();" />
                                </div>
                              </div>
                             
      
                        </div>
                         
                    </div>
                    <%--<table class="tbl-controlPanel">
                        <tr>
                            <td>Section Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtSectionName" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static"
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="Cleartext();" />
                            </td>
                        </tr>
                    </table> --%>                   
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblSectionList', '');
            });
            $('#tblSectionList').dataTable({
                "iDisplayLength": 1000,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblSectionList').dataTable({
                "iDisplayLength": 1000,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateText('txtSectionName', 1, 30, 'Enter a Section Name') == false) return false;
            return true;
        }
        function editSection(SecId) {
            $('#lblSectionID').val(SecId);
            var strSec = $('#r_' + SecId + ' td:first-child').html();
            $('#txtSectionName').val(strSec);
            $("#btnSave").val('Update');
        }
        function Cleartext() {
            $('#txtSectionName').val('');
            $('#lblSectionID').val('');
            setFocus('txtSectionName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Update successfully', 'success');
            Cleartext();
        }
    </script>
</asp:Content>
