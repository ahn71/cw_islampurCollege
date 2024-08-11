<%@ Page Title="Add Class" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddClass.aspx.cs" Inherits="DS.UI.Administration.Settings.AcademicSettings.AddClass" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        /*.controlLength{
            width: 200px;
        }*/
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
    <asp:HiddenField ID="lblClassId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfClassName" ClientIDMode="Static" runat="server" />
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
                <li class="active">Add Class</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Class List</h4>
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
                        <div id="divClassList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add Class</div>
                    
                     <div class="row tbl-controlPanel">
                        <div class="col-sm-8 col-sm-offset-2">
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Class Name</label>
                                <div class="col-sm-8">
                                     <asp:TextBox ID="txtClassName" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="F1" runat="server" Enabled="True" TargetControlID="txtClassName" 
                                    FilterType="Numbers, UppercaseLetters, LowercaseLetters"></asp:FilteredTextBoxExtender>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Order</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtOrder" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="F2" runat="server" Enabled="True" 
                                    TargetControlID="txtOrder" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4"></label>
                                <div class="col-sm-8">
                                    <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" OnClientClick="return validateInputs();"
                                    runat="server" Text="Save" OnClick="btnSave_Click" />
                                <input type="button" value="Clear" class="btn btn-default" onclick="clearIt();" />
                                </div>
                              </div>
                              
       
                        </div>
                    </div>
                   <%-- <table class="tbl-controlPanel">
                        <tr>
                            <td>Class Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtClassName" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="F1" runat="server" Enabled="True" TargetControlID="txtClassName" 
                                    FilterType="Numbers, UppercaseLetters, LowercaseLetters"></asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>Order
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrder" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="F2" runat="server" Enabled="True" 
                                    TargetControlID="txtOrder" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" OnClientClick="return validateInputs();"
                                    runat="server" Text="Save" OnClick="btnSave_Click" />
                                <input type="button" value="Clear" class="btn btn-default" onclick="clearIt();" />
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
                searchTable($(this).val(), 'tblClassList', '');
            });
            $('#tblClassList').dataTable({
                "iDisplayLength": 1000,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblClassList').dataTable({
                "iDisplayLength": 1000,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateText('txtClassName', 1, 50, 'Enter a Class Name') == false) return false;
            if (validateText('txtOrder', 1, 15, 'Enter a Order Number') == false) return false;
            return true;
        }
        function editClass(clsId) {
            $('#lblClassId').val(clsId);
            var strOrder = $('#r_' + clsId + ' td:first-child').html();
            var strClass = $('#r_' + clsId + ' td:nth-child(2)').html();            
            $('#hfClassName').val(strClass);
            $('#txtClassName').val(strClass);
            $('#txtOrder').val(strOrder);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#hfClassName').val('');
            $('#lblClassId').val('');
            $('#txtClassName').val('');
            $('#txtOrder').val('');
            setFocus('txtClassName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Update successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
