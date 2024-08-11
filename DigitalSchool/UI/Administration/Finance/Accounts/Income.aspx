<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Income.aspx.cs" Inherits="DS.UI.Administration.Finance.Accounts.Income" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        /*.controlLength{
            width: 250px;
        }*/
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
         .dataTables_length, .dataTables_filter {
          display: none;
          padding: 15px;
        }
        #tblParticularCategory_info {
             display: none;
            padding: 15px;
        }
        #tblParticularCategory_paginate {
            display: none;
            padding: 15px;
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
        }
        .group {
            margin-top:15px;
            

        }   
        
        @media (min-width: 320px) and (max-width: 480px) {
            .pagination {
            width:100%;
            float:left;
            margin-left:50px;
            }

            .dataTables_info {
            width:100%;
            }
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
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/Accounts/AccountsHome.aspx">Accounts Management</a></li>                          
                <li class="active">Income</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
  
            <asp:HiddenField ID="lblFeesCateId" ClientIDMode="Static" runat="server"/>
            <div class="">
                <div class="row">
                    <div class="col-md-7">
                        <h4 class="text-right" style="float:left">Income List</h4>
                         <div class="dataTables_filter_New" style="float: right;">
                     <label>
                         Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                     </label>
                 </div>                        
                    </div>
                    <div class="col-md-5"></div>
                </div>
                <div class="row">
                    <div class="col-md-7">
                        <asp:UpdatePanel runat="server" ID="updatepanelFeesSett" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />                                
                            </Triggers>
                            <ContentTemplate>
                                <div class="tgPanel">
                                    <div id="divExpensesList" class="datatables_wrapper" runat="server"
                                        style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                                    </div>
                                    <asp:HiddenField ID="lblExpensesID" Value="0" ClientIDMode="Static" runat="server" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-5">
                        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                            <Triggers>                              
                            </Triggers>
                            <ContentTemplate>
                                <div class="tgPanel">

                                    <div class="tgPanelHead">Income</div>
                                    <div class="row tbl-controlPanel">
                                        <div class="col-sm-10 col-sm-offset-1">
                                              <div class="row tbl-controlPanel">
                                                <label class="col-sm-4">Title</label>
                                                <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlTitle" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                    AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </div>
                                              </div>
                                              <div class="row tbl-controlPanel">
                                                <label class="col-sm-4">Amount</label>
                                                <div class="col-sm-8">
                                                 <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" Text="0" CssClass="input controlLength form-control"></asp:TextBox>
                                                </div>
                                              </div>
                                              <div class="row tbl-controlPanel">
                                                <label class="col-sm-4">Income Date</label>
                                                <div class="col-sm-8">
                                                   <asp:TextBox ID="txtExDate" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtExDate"></asp:CalendarExtender>
                                                </div>
                                              </div>
                                              <div class="row tbl-controlPanel">
                                                <label class="col-sm-4">Particular</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtParticular" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>                                                    
                                                </div>
                                              </div>
                                              <div class="row tbl-controlPanel">
                                                <div class="col-sm-offset-4 col-sm-8">
                                                  <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                                <input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
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
                searchTable($(this).val(), 'tblexpenseslist', '');
            });
            // $("#dlBatchName").select2();
            $('#tblexpenseslist').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblexpenseslist').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateCombo('ddlTitle.Text', 0, 20, 'Select Title') == false) return false;
            if (validateText('txtAmount', 1, 100, 'Enter a Amount') == false) return false;
            if (validateText('txtExDate', 1, 100, 'Select a Date') == false) return false;
            return true;
        }      
        function SavedSuccess() {
            clearIt();
            showMessage('Saved successfully', 'success');
            load();
        }
        function updateSuccess() {
            clearIt();
            showMessage('Updated successfully', 'success');
            load();
        }
        function editexpenses(expensesid,titleid) {
            $('#lblExpensesID').val(expensesid);            
            $('#ddlTitle').val(titleid);
            var amount = $('#amount' + expensesid).html();
            $('#txtAmount').val(amount);
            var date = $('#date' + expensesid).html();
            $('#txtExDate').val(date);
            var particular = $('#particular' + expensesid).html();
            $('#txtParticular').val(particular);
            $("#btnSave").val('Update');
        }
        function deleteincome(incomeid) {
            $.ajax({
                url: "/UI/Administration/Finance/Accounts/Income.aspx/Delete",
                data: "{incomeid: '" + incomeid + "'}",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset= utf-8",
                success: function OnSuccess(data) {
                    if (data.d == '') {
                        showMessage(data.d, 'error');
                        return false;
                    }
                    else {
                        $("#MainContent_divExpensesList").html(data.d);
                        showMessage('Delete successfully', 'success');

                    }
                }
            });

        }
        function clearIt() {
            $('#lblExpensesID').val('0');
            $('input[type=text]').val('');
            $("#btnSave").val('Save');
            setFocus('txtTitle');
        }
        function load() {
            loaddatatable();
            // $("#dlBatchName").select2();
        }
        </script>
</asp:Content>
