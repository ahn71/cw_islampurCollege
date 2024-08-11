<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdmissionFeesCategories.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.AdmissionFeesCategories" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .controlLength{
            /*width: 250px;*/
        }
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
        #tblParticularCategory {
            margin-top:0px!important;
            margin-bottom:0px!important;
        
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblFeesCateId" Value="0" ClientIDMode="Static" runat="server"/>
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
                <li><a id="A3" runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a id="A4" runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">Fee Management</a></li>
                <li class="active">Admission Fees Category</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="ddlClass" />
        </Triggers>
        <ContentTemplate>
            <div class="">
                <div class="row">
                    <div class="col-md-7">
                        <h4 class="text-right" style="float:left">Admission Fees Category Information</h4>
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
                        <div class="tgPanel">
                        <div id="divFeesCategoryList" class="datatables_wrapper" runat="server" 
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                    </div>
                    <div class="col-md-5">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fees Category</div>
                            <div class="row tbl-controlPanel">
	                            <div class="col-sm-10 col-sm-offset-1">
		                            <div class="form-group row">
			                            <label class="col-sm-4">Class Name</label>
			                            <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static"
                                             AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                        </asp:DropDownList>
			                            </div>
		                            </div>
                                    <div class="form-group row">
			                            <label class="col-sm-4">Fees Category</label>
			                            <div class="col-sm-8">
                                            <asp:TextBox ID="txtFeesCatName" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
			                            </div>
		                            </div>
                                    <div class="form-group row">
			                            <label class="col-sm-4">Date of Start</label>
			                            <div class="col-sm-8">
                                            <asp:TextBox ID="txtDateStart" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                        <asp:CalendarExtender Format="dd-MM-yyyy"  ID="CalendarExtender1" runat="server" TargetControlID="txtDateStart"></asp:CalendarExtender>
			                            </div>
		                            </div>
                                    <div class="form-group row">
			                            <label class="col-sm-4">Date of End</label>
			                            <div class="col-sm-8">
                                            <asp:TextBox ID="txtDateEnd" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                        <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtDateEnd"></asp:CalendarExtender>
			                            </div>
		                            </div>
                                    <div class="form-group row">
			                            <label class="col-sm-4"></label>

			                            <div class="col-sm-8">
                                            <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
			                            </div>
		                            </div>
                                    
	                            </div>
                            </div>
                      <%-- <table class="tbl-controlPanel">
                                <tr>
                                    <td>Class Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="input controlLength" ClientIDMode="Static"
                                             AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fees Category
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFeesCatName" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of Start
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateStart" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                        <asp:CalendarExtender Format="dd-MM-yyyy"  ID="CalendarExtender1" runat="server" TargetControlID="txtDateStart"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of End
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateEnd" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                        <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtDateEnd"></asp:CalendarExtender>
                                    </td>
                                </tr>
                             
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                    </td>
                                </tr>
                            </table>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">     
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblParticularCategory', '');
            });
            $("#ddlClass").select2();
            $('#tblParticularCategory').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblParticularCategory').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateCombo('ddlClass',0,'Select Class Name') == false) return false;
            if (validateText('txtFeesCatName', 1, 20, 'Type Fees Category') == false) return false;
            if (validateText('txtDateStart', 1, 20, 'Select Payment Start Date') == false) return false;
            if (validateText('txtDateEnd', 1, 20, 'Select Payment End Date') == false) return false;
            return true;
        }
        function editFeesCategory(feesCatId,ClassId) {
            $('#lblFeesCateId').val(feesCatId);
            var strBatch = $('#r_' + feesCatId + ' td:nth-child(1)').html();            
            var strFeesCatName = $('#r_' + feesCatId + ' td:nth-child(2)').html();
            var startDate = $('#r_' + feesCatId + ' td:nth-child(3)').html();
            var EndDate = $('#r_' + feesCatId + ' td:nth-child(4)').html();           
            $('#ddlClass').val(ClassId);
            $('#txtFeesCatName').val(strFeesCatName);
            $('#txtDateStart').val(startDate);
            $('#txtDateEnd').val(EndDate);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblFeesCateId').val('');
            $('#txtFeesCatName').val('');
            $('#txtFeesFine').val('');
            setFocus('txtFeesCatName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Update successfully', 'success');
            clearIt();
        }
        function load() {
            loaddatatable();
            $("#ddlClass").select2();
        }
    </script>
</asp:Content>
