<%@ Page Title="Salary Allowance Type" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SalaryAllowanceType.aspx.cs" Inherits="DS.UI.Administration.HR.Payroll.SalaryAllowanceType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        input[type="checkbox"]
        {
            margin: 5px;
        }
        .controlLength{
            width: 200px;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
         .dataTables_length, .dataTables_filter {
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
    <asp:HiddenField ID="lblAId" ClientIDMode="Static" Value="" runat="server" />
    <asp:HiddenField ID="lblOldPercentage" ClientIDMode="Static" Value="" runat="server" />
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
                <li><a runat="server" href="~/UI/Administration/HR/Payroll/PayrollHome.aspx">Payroll Management</a></li>
                <li class="active">Salary Allowance Type</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float:left">Salary Allowance Type Details</h4>
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
                        <div id="divAllowanceType" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                         <asp:HiddenField ID="HiddenField1" ClientIDMode="Static" Value="" runat="server" />
                        <asp:HiddenField ID="HiddenField2" ClientIDMode="Static" Value="" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Salary Allowance Type</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Allowance Type
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAllowanceType" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Percentage
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPercentage" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                         <asp:RadioButtonList RepeatLayout="Flow" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
                                        <asp:CheckBox ID="chkStatus" class="checkbox-inline" runat="server" Text="Is Active" ClientIDMode="Static" Checked="True" />
                                   </asp:RadioButtonList>   
                                              </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                    </td>
                                </tr>
                            </table>
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
            if (validateText('txtAllowanceType', 1, 50, 'Enter Allowance Type') == false) return false;
            if (validateText('txtPercentage', 1, 30, 'Enter Percentage') == false) return false;
            return true;
        }
        function editEmployee(empId) {
            $('#lblAId').val(empId);
            var strAT = $('#r_' + empId + ' td:first-child').html();        
            $('#txtAllowanceType').val(strAT);

            var strP = $('#r_' + empId + ' td:nth-child(2)').html();
            $('#txtPercentage').val(strP);
            $('#lblOldPercentage').val(strP);
            $("#btnSave").val('Update');
            var strAS = $('#r_' + empId + ' td:nth-child(3)').html();
            if (strAS == 'True') {
                $("#chkStatus").removeProp('checked');
                $("#chkStatus").click();
            }
            else {
                $("#chkStatus").removeProp('checked');
            }
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('input[type=text]').val('');
            var n = $("#chkStatus:checked").length;
            if (n == 0) {
                $('#chkStatus').click();
            }
            $('#lblAId').val('');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SavedSuccess() {
            loaddatatable();
            showMessage('Saved successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
