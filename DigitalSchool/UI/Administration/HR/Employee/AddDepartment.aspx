<%@ Page Title="Add Category" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddDepartment.aspx.cs" Inherits="DS.UI.Administration.HR.Employee.AddDepartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        input[type="checkbox"]{
            margin: 7px;
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>  
                <li><a runat="server" href="~/UI/Administration/HR/Employee/EmpHome.aspx">Employee Management</a></li> 
                <li class="active">Add Department</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float:left">Department List</h4>
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
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divDepartmentList" class="datatables_wrapper" runat="server" 
                            style="width: 100%;  height: 70vh; overflow-y: scroll;overflow-x: hidden; "></div>
                            </div>
                        <asp:HiddenField ID="lblDepartmentId" ClientIDMode="Static" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:Panel runat="server" ID="pnlDepartment" DefaultButton="btnsave">
                    <div class="col-md-6">
                <div class="tgPanel">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanelHead">Add Department</div>
                        <table class="tbl-controlPanel">                            
                            <tr>
                                <td>
                                    Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepartment" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    
                                </td>
                                <td>
                                    <asp:RadioButtonList RepeatLayout="Flow" CssClass="checkboxlist" RepeatDirection="Horizontal">
                                    <asp:CheckBox ID="chkStatus" class="checkbox-inline" ClientIDMode="Static" runat="server" Text="Status" Checked="True"/> 
                                    <asp:CheckBox ID="chkIsTeacher" class="checkbox-inline" ClientIDMode="Static" runat="server" Text="IsTeachers?" Checked="True" />
                                    </asp:RadioButtonList>                                    
                                </td>
                            </tr>                            
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static"
                                        OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                    <input id="tnReset" type="reset" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                </td>
                            </tr>                            
                        </table>                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            </div>
            </asp:Panel>
        
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
            if (validateText('txtDepartment', 1, 50, 'Enter Department Name') == false) return false;
            return true;
        }
        function editDepartment(Did) {
            $('#lblDepartmentId').val(Did);
            var strDepartment = $('#r_' + Did + ' td:first-child').html();
            $('#txtDepartment').val(strDepartment);
            var strS = $('#r_' + Did + ' td:nth-child(2)').html();
            var strIsTeacher = $('#r_' + Did + ' td:nth-child(3)').html();
            if (strS == 'True') {
                $("#chkStatus").removeProp('checked');
                $("#chkStatus").click();
            }
            else {
                $("#chkStatus").removeProp('checked');
            }
            if (strIsTeacher == 'True') {
                $("#chkIsTeacher").removeProp('checked');
                $("#chkIsTeacher").click();
            }
            else {
                $("#chkIsTeacher").removeProp('checked');
            }
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblDepartmentId').val('');
            $('#txtDes_Name').val('');
            setFocus('txtDes_Name');
            $("#chkStatus").removeProp('checked');
            $("#chkStatus").click();
            $("#chkIsTeacher").removeProp('checked');
            $("#chkIsTeacher").click();
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>
