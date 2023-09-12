<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ManageClassGroup.aspx.cs" Inherits="DS.UI.Administration.Settings.AcademicSettings.ManageClassGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .display  td:nth-child(3),th:nth-child(3){
            text-align:center;
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
                <li class="active">Add Class Group</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Class Group List</h4>
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
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                        </div>
                        <asp:HiddenField ID="lblClsGrpID" ClientIDMode="Static" runat="server"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" >  
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                             <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                        </Triggers>                       
                        <ContentTemplate>
                            <div class="tgPanelHead">Add Class Group Name</div>
                             <div class="row tbl-controlPanel">
                                <div class="col-sm-10 col-sm-offset-1">
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-6">Class Name</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="input controlLength form-control" AutoPostBack="false"
                                                ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-6">Group Name</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="input controlLength form-control" AutoPostBack="false"
                                            ClientIDMode="Static">
                                        </asp:DropDownList>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-6">Number of Mandatory Subject</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtnumofmandatorySub" runat="server" CssClass="input controlLength form-control" 
                                            ClientIDMode="Static">
                                        </asp:TextBox>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-6"></label>
                                        <div class="col-sm-6">
                                            <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSubmit_Click"  />
                                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                        </div>
                                      </div>
                                </div>
                            </div>

                           <%-- <table class="tbl-controlPanel">
                                <tr>
                                    <td>Class Name</td>
                                    <td>
                                        <asp:DropDownList ID="ddlClass" Width="150px" runat="server" CssClass="input controlLength" AutoPostBack="false"
                                            ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                               <tr>
                                    <td>Group Name</td>
                                    <td>
                                        <asp:DropDownList ID="ddlGroup" Width="150px" runat="server" CssClass="input controlLength" AutoPostBack="false"
                                            ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td>Number of Mandatory Subject</td>
                                    <td>
                                        <asp:TextBox ID="txtnumofmandatorySub" Width="150px" runat="server" CssClass="input controlLength" 
                                            ClientIDMode="Static">
                                        </asp:TextBox>
                                    </td>
                                </tr> 
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSubmit_Click"  />
                                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                    </td>
                                </tr>                               
                            </table> --%>                           
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
                showMessage('Select Class Name', 'warning');
                return false;
            }
            if ($('#ddlGroup').val() == '0') {
                showMessage('Select Group Name', 'warning');
                return false;
            }
            return true;
        }
        function editCG(ClsGrpID, ClassID, GroupID, numofmandatorysub) {
            $('#lblClsGrpID').val(ClsGrpID);
            $('#ddlClass').val(ClassID);
            $('#ddlGroup').val(GroupID);         
            $('#txtnumofmandatorySub').val(numofmandatorysub);
            $("#btnSubmit").val('Update');
        }
        function clearIt() {
            $('#ddlClass').val('0');
            $('#ddlGroup').val('0');
            $('#lblClsGrpID').val('');
            $('#txtnumofmandatorySub').val('');
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
